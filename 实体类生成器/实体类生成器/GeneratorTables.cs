#region using
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ConsoleApp3;
using DevLib.ExtensionMethods;
#endregion

namespace 实体类生成器
{
    public class GeneratorTables
    {
        private readonly bool
            AddNamedConnection =
                false; //if true: Adds NamedConnection attribute so AutoQuery will override default IDbConnection
        private readonly string ConnectionStringName = "";
        private readonly bool
            CreateAutoQueryTypes =
                false; //if true: Will create <TypeName>Query types with all possible search fields explicitly typed
        private  Dictionary<string, string> dic { get; set; }
        private readonly bool                       IncludeReferences  = false; //if true: Addes References(typeof(ReferenceTableType)) to FKs
        private readonly bool                       UseIdAsPK          = false; // if true: Changes the primary key property name to Id
        private readonly bool                       UseSchemaAttribute = true;  // if true: Adds explicit '[Schema]' attribute
        private readonly string
            UseSpecificNamedConnection =
                ""; //if not null: Will use name provided as NamedConnection and AddNamedConnection = true, else ConnectionStringName is used as default NamedConnection
        /// <summary>
        /// </summary>
        /// <param name="Namespace">命名空间</param>
        /// <param name="MakeSingular">使用前后缀函数</param>
        /// <param name="ClassPrefix">Class前缀</param>
        /// <param name="ClassSuffix">Class后缀</param>
        /// <param name="SplitIntoMultipleFiles">创建单独文件</param>
        /// <param name="MultipleFileName">创建单独文件文件名</param>
        /// <param name="GenerateConstructor">创建构造函数</param>
        public GeneratorTables(string _Namespace = "OrmLitePoco", bool _MakeSingular = true, string _ClassPrefix = "", string _ClassSuffix = "", bool _SplitIntoMultipleFiles = true, string _MultipleFileName = "OrmLitePoco", bool _GenerateConstructor = false)
        {
            Namespace              = _Namespace;
            MakeSingular           = _MakeSingular;
            ClassPrefix            = _ClassPrefix;
            ClassSuffix            = _ClassSuffix;
            SplitIntoMultipleFiles = _SplitIntoMultipleFiles;
            MultipleFileName       = _MultipleFileName;
            GenerateConstructor    = _GenerateConstructor;
        }
        // Uses last connection string in config if not specified
        //        string Namespace = "";
        //        string ClassPrefix = "";
        //        string ClassSuffix = "";
        //        bool SplitIntoMultipleFiles = false; // if true: Generates one file for every class
        //        string MultipleFileName = "MultipleFileName.cs";
        //        bool MakeSingular = true; // if true: Changes the classname to singular if tablename is not singular
        //        bool UseIdAsPK = false; // if true: Changes the primary key property name to Id
        //        bool GenerateConstructor = false; // if true: Generates the default empty constructor
        private string Namespace              { get; }
        private string ClassPrefix            { get; }
        private string ClassSuffix            { get; }
        private bool   SplitIntoMultipleFiles { get; } // if true: Generates one file for every class
        private string MultipleFileName       { get; }
        private bool   MakeSingular           { get; } // if true: Changes the classname to singular if tablename is not singular
        private bool   GenerateConstructor    { get; } // if true: Generates the default empty constructor

        // Read schema
        public void Generator(DbProviderFactory fac, DbConnection con)
        {
            dic = new Dictionary<string, string>();
            var sr     = new SchemaReaderClass();
            var tables = sr.LoadTables(MakeSingular, fac, con);

//            if (string.IsNullOrEmpty(Namespace)) Namespace = ConnectionStringName;
//            if (string.IsNullOrEmpty(Namespace)) Namespace = "OrmLitePoco";
            var Headerstring = $@"using System;

using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using ServiceStack;

namespace {Namespace}
{{";
            var Footerstring = "}";
            foreach (var tbl in from t in tables
                                where !t.Ignore
                                select t)
            {
                var sb = new StringBuilder();
                //                StreamWriter sw = new StreamWriter(tbl.Name + ".cs");
                //                sb.AppendLine(Headerstring);
                sb.AppendLine();
                if (CreateAutoQueryTypes && AddNamedConnection)
                    sb.AppendLine(
                                  $"    [NamedConnection(\" {(!string.IsNullOrEmpty(UseSpecificNamedConnection) ? UseSpecificNamedConnection : ConnectionStringName)}\")]");
                if (MakeSingular) sb.AppendLine($"    [Alias(\"{tbl.Name}\")]");
                if (UseSchemaAttribute && !string.IsNullOrEmpty(tbl.Schema) && tbl.Schema != "dbo") sb.AppendLine($"    [Schema(\"{tbl.Schema}\")]");
                sb.Append($"    public partial class {tbl.ClassName}");
                if (tbl.HasPK() && UseIdAsPK) sb.Append($" : IHasId<{tbl.PK.PropertyType}>");
                sb.AppendLine("");
                sb.AppendLine("    {");
                if (GenerateConstructor)
                {
                    sb.AppendLine($"    public {tbl.ClassName}()");
                    sb.AppendLine("    {");
                    sb.AppendLine("    }");
                }
                var priorProperyNames = new List<string>();
                foreach (var col in from c in tbl.Columns
                                    where !c.Ignore
                                    select c)
                {
                    if (priorProperyNames.Contains(col.PropertyName)) //Change duplicate style names
                        col.PropertyName = "_" + col.PropertyName;
                    priorProperyNames.Add(col.PropertyName);
                    if (col.Name         != col.PropertyName || col.IsPK && UseIdAsPK) sb.AppendLine($"        [Alias(\"{col.Name}\")]");
                    if (col.PropertyType == "string" && col.Size > 0) sb.AppendLine($"        [StringLength({col.Size})]");
                    if (col.IsAutoIncrement) sb.AppendLine("        [AutoIncrement]");
                    if (col.IsComputed) sb.AppendLine("        [Compute]");
                    if (IncludeReferences && tbl.FKeys != null && tbl.FKeys.Any(x => x.FromColumn == col.PropertyName))
                    {
                        var toTable = MakeSingular
                                          ? tbl.FKeys.First(x => x.FromColumn == col.PropertyName).ToTableSingular
                                          : tbl.FKeys.First(x => x.FromColumn == col.PropertyName).ToTable;
                        sb.AppendLine($"        [References(typeof({ClassPrefix + toTable + ClassSuffix}))]");
                    }
                    if (col.IsNullable != true && col.IsAutoIncrement != true) sb.AppendLine("        [Required]");
                    if (!col.IsPK) sb.AppendLine($"        public {col.ProperPropertyType} {col.PropertyName} {{ get; set; }}");
                    if (col.IsPK && UseIdAsPK) sb.AppendLine($"        public {col.ProperPropertyType} Id {{ get; set; }}");
                    if (col.IsPK && !UseIdAsPK)
                        sb.AppendLine($@"        [PrimaryKey]
        public {col.ProperPropertyType} {col.PropertyName} {{ get; set; }}");
                }
                if (CreateAutoQueryTypes)
                {
                    sb.AppendLine("    }");
                    sb.AppendLine($@"    public partial class {tbl.ClassName}Query: QueryDb<{tbl.ClassName}>
    {{");
                    foreach (var col in from c in tbl.Columns
                                        where !c.Ignore
                                        select c)
                    {
                        var ormName          = col.IsPK && UseIdAsPK ? "Id" : col.PropertyName;
                        var isString         = col.ProperPropertyType == "string";
                        var nullablePropType = col.ProperPropertyType.Replace("?", "") + "?";
                        var isArray          = col.ProperPropertyType.Contains("[]");
                        var isBool           = col.ProperPropertyType.Contains("bool");
                        var isGuid           = col.ProperPropertyType.Contains("Guid");
                        if (!col.IsPK)
                            sb.AppendLine(
                                          $"        public {(!isArray && !isString ? nullablePropType : col.ProperPropertyType)} {ormName} {{ get; set; }}");
                        if (col.IsPK && UseIdAsPK) sb.AppendLine($"	    public {(isString ? "string" : nullablePropType)} Id {{ get; set;}}");
                        if (col.IsPK && !UseIdAsPK)
                            sb.AppendLine(
                                          $"        public {(isString ? "string" : nullablePropType)} {col.PropertyName} {{ get; set; }}");
                        if (isString)
                            sb.AppendLine(
                                          $@"
        public {col.ProperPropertyType} {ormName}StartsWith {{ get; set; }}
        public {col.ProperPropertyType} {ormName}EndsWith {{ get; set; }}
        public {col.ProperPropertyType} {ormName}Contains {{ get; set; }}
        public {col.ProperPropertyType} {ormName}Like {{ get; set; }} 
        public {col.ProperPropertyType}[] {ormName}Between {{ get; set; }}
        public {col.ProperPropertyType}[] {ormName}In {{ get; set; }}
");
                        else
                            if (!isArray && !isBool)
                            {
                                if (!isGuid)
                                    sb.AppendLine($@"
        public {nullablePropType} {ormName}GreaterThanOrEqualTo {{ get; set; }}
        public {nullablePropType} {ormName}GreaterThan {{ get; set; }}
        public {nullablePropType} {ormName}LessThan {{ get; set; }}
        public {nullablePropType} {ormName}LessThanOrEqualTo {{ get; set; }}
        public {nullablePropType} {ormName}NotEqualTo {{ get; set; }}
        public {col.ProperPropertyType}[] {ormName}Between {{ get; set; }}"
                                                  );
                                sb.AppendLine($@"
        public {col.ProperPropertyType}[] {ormName}In {{ get; set; }}"
                                              );
                            }
                    }
                }
                sb.AppendLine("    }");
                sb.AppendLine();
                if (SplitIntoMultipleFiles)
                {
                    if (!dic.ContainsKey(MultipleFileName)) dic.Add(MultipleFileName, "");
                    dic[MultipleFileName] = dic[MultipleFileName] + sb;
                }
                else
                {
                    string schema = "default";
                    if (!tbl.Schema.IsNotNullNorWhiteSpace())
                    {
                        schema = tbl.Schema;
                    }
                    dic.Add(schema+"."+tbl.Name + ".cs", sb.ToString());
                }
            }
        }
    }
}