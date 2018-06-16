using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;

namespace 实体类生成器
{
    public class GeneratorTables
    {
        string ConnectionStringName = ""; // Uses last connection string in config if not specified
        string Namespace = "";
        string ClassPrefix = "";
        string ClassSuffix = "";
        bool SplitIntoMultipleFiles = false; // if true: Generates one file for every class
        bool MakeSingular = true; // if true: Changes the classname to singular if tablename is not singular
        bool UseIdAsPK = true; // if true: Changes the primary key property name to Id
        bool GenerateConstructor = false; // if true: Generates the default empty constructor
        bool UseSchemaAttribute = true; // if true: Adds explicit '[Schema]' attribute

        bool
            CreateAutoQueryTypes =
                true; //if true: Will create <TypeName>Query types with all possible search fields explicitly typed

        bool
            AddNamedConnection =
                false; //if true: Adds NamedConnection attribute so AutoQuery will override default IDbConnection

        bool IncludeReferences = false; //if true: Addes References(typeof(ReferenceTableType)) to FKs

        string
            UseSpecificNamedConnection =
                ""; //if not null: Will use name provided as NamedConnection and AddNamedConnection = true, else ConnectionStringName is used as default NamedConnection

        // Read schema
        public void Generator(DbProviderFactory fac, DbConnection con)
        {
            SchemaReaderClass sr = new SchemaReaderClass();

            var tables = sr.LoadTables(MakeSingular, fac, con);

            if (string.IsNullOrEmpty(Namespace)) Namespace = ConnectionStringName;
            if (string.IsNullOrEmpty(Namespace)) Namespace = "OrmLitePoco";
            string Headerstring = $@"using System;

using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using ServiceStack;

namespace {Namespace}
{{";
            foreach (SchemaReaderClass.Table tbl in from t in tables where !t.Ignore select t)
            {
                StreamWriter sw = new StreamWriter(tbl.Name + ".cs");
                sw.WriteLine(Headerstring);
                if (CreateAutoQueryTypes && AddNamedConnection)
                {
                    sw.WriteLine(
                        $"    [NamedConnection(\" {(!string.IsNullOrEmpty(UseSpecificNamedConnection) ? UseSpecificNamedConnection : ConnectionStringName)}\")]");
                }

                if (MakeSingular)
                {
                    sw.WriteLine($"    [Alias(\"{tbl.Name}\")]");
                }

                if (UseSchemaAttribute && !string.IsNullOrEmpty(tbl.Schema) && tbl.Schema != "dbo")
                {
                    sw.WriteLine($"    [Schema(\"{tbl.Schema}\")]");
                }

                sw.Write($"    public partial class {tbl.ClassName}");
                if (tbl.HasPK() && UseIdAsPK)
                {
                    sw.Write($" : IHasId<{tbl.PK.PropertyType}>");
                }
                sw.WriteLine("");
                sw.WriteLine("    {");
                if (GenerateConstructor)
                {
                    sw.WriteLine($"    public {tbl.ClassName}()");
                    sw.WriteLine("    {");
                    sw.WriteLine("    }");
                }

                var priorProperyNames = new List<string>();
                foreach (SchemaReaderClass.Column col in from c in tbl.Columns where !c.Ignore select c)
                {
                    if (priorProperyNames.Contains(col.PropertyName)) //Change duplicate style names
                    {
                        col.PropertyName = "_" + col.PropertyName;
                    }

                    priorProperyNames.Add(col.PropertyName);
                    if ((col.Name != col.PropertyName) || (col.IsPK && UseIdAsPK))
                    {
                        sw.WriteLine($"        [Alias(\"{col.Name}\")]");
                    }

                    if (col.PropertyType == "string" && col.Size > 0)
                    {
                        sw.WriteLine($"        [StringLength({col.Size})]");
                    }

                    if (col.IsAutoIncrement) sw.WriteLine("        [AutoIncrement]");
                    if (col.IsComputed) sw.WriteLine("        [Compute]");
                    if (IncludeReferences && tbl.FKeys != null && tbl.FKeys.Any(x => x.FromColumn == col.PropertyName))
                    {
                        var toTable = MakeSingular
                            ? tbl.FKeys.First(x => x.FromColumn == col.PropertyName).ToTableSingular
                            : tbl.FKeys.First(x => x.FromColumn == col.PropertyName).ToTable;
                        sw.WriteLine($"        [References(typeof({ClassPrefix + toTable + ClassSuffix}))]");

                    }

                    if (col.IsNullable != true && col.IsAutoIncrement != true)
                    {
                        sw.WriteLine("        [Required]");
                    }

                    if (!col.IsPK) sw.WriteLine($"        public {col.ProperPropertyType} {col.PropertyName} {{ get; set; }}");
                    if (col.IsPK && UseIdAsPK)
                    {
                        sw.WriteLine($"        public {col.ProperPropertyType} Id {{ get; set; }}");
                    }

                    if (col.IsPK && !UseIdAsPK)
                    {
                        sw.WriteLine($@"		[PrimaryKey]
		public {col.ProperPropertyType} {col.PropertyName} {{ get; set; }}");
                    }



                }

                if (CreateAutoQueryTypes)
                {
                    sw.WriteLine("    }");
                    sw.WriteLine($@"	public partial class {tbl.ClassName}Query: QueryDb<{tbl.ClassName}>
	{{");
                    foreach (SchemaReaderClass.Column col in from c in tbl.Columns where !c.Ignore select c)
                    {
                        var ormName = (col.IsPK && UseIdAsPK) ? "Id" : col.PropertyName;
                        var isString = col.ProperPropertyType == "string";
                        var nullablePropType = col.ProperPropertyType.Replace("?", "") + "?";
                        var isArray = col.ProperPropertyType.Contains("[]");
                        var isBool = col.ProperPropertyType.Contains("bool");
                        var isGuid = col.ProperPropertyType.Contains("Guid");
                        if (!col.IsPK)
                        {
                            sw.WriteLine(
                                $"	    public {(!isArray && !isString ? nullablePropType : col.ProperPropertyType)} {ormName} {{ get; set; }}");
                        }

                        if (col.IsPK && UseIdAsPK)
                        {
                            sw.WriteLine($"	    public {(isString ? "string" : nullablePropType)} Id {{ get; set;}}");
                        }

                        if (col.IsPK && !UseIdAsPK)
                        {
                            sw.WriteLine(
                                $"	    public {(isString ? "string" : nullablePropType)} {col.PropertyName} {{ get; set; }}");
                        }

                        if (isString)
                        {
                            sw.WriteLine(
                                $@"	    public {col.ProperPropertyType} {ormName}StartsWith {{ get; set; }}
		public {col.ProperPropertyType} {ormName}EndsWith {{ get; set; }}
		public {col.ProperPropertyType} {ormName}Contains {{ get; set; }}
		public {col.ProperPropertyType} {ormName}Like {{ get; set; }} 
		public {col.ProperPropertyType}[] {ormName}Between {{ get; set; }}
		public {col.ProperPropertyType}[] {ormName}In {{ get; set; }}
");

                        }
                        else if (!isArray && !isBool)
                        {
                            if (!isGuid)
                            {
                                sw.WriteLine(
                                    $@"	    public {nullablePropType} {ormName}GreaterThanOrEqualTo {{ get; set; }}
		public {nullablePropType} {ormName}GreaterThan {{ get; set; }}
		public {nullablePropType} {ormName}LessThan {{ get; set; }}
		public {nullablePropType} {ormName}LessThanOrEqualTo {{ get; set; }}
		public {nullablePropType} {ormName}NotEqualTo {{ get; set; }}
		public {col.ProperPropertyType}[] {ormName}Between {{ get; set; }}");

                            }

                            sw.WriteLine($"		public {col.ProperPropertyType}[] {ormName}In {{ get; set; }}");
                        }
                    }


                }
                sw.WriteLine("    }");
                sw.WriteLine("}");
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
    }
}
