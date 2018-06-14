using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
using 实体类生成器.Common;

namespace 实体类生成器.Sqlite
{
    public  class Tables: ITables
    {
        public string objname { get; set; }
        public List<object> infos { get; set; }
        public string ToClassString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($" [Alias\"({objname})\"]");
            sb.AppendLine($"public class T_{objname}");
            sb.AppendLine("{");
            foreach (Itable_info info in infos)
            {
                if (info.PrimaryKey== "PrimaryKey")
                {
                    sb.AppendLine("[PrimaryKey]");
                }
                if (info.Unique == "Unique")
                {
                    sb.AppendLine("[Unique]");
                }
                if (info.AutoIncrement == "AutoIncrement")
                {
                    sb.AppendLine("[AutoIncrement]");
                }
                switch (info.Type.ToUpper())
                {
                    case "INTEGER":
                        sb.AppendLine($"public int {info.Name} {{get;set;}}");
                        break;
                    case "TEXT":
                        sb.AppendLine($"public string {info.Name} {{get;set;}}");
                        break;
                    case "BLOB":
                        sb.AppendLine($"public byte[] {info.Name} {{get;set;}}");
                        break;
                    case "REAL":
                        sb.AppendLine($"public double {info.Name} {{get;set;}}");
                        break;

                }
             
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class table_info: Common.Itable_info
    {
        public int cid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool notnull { get; set; }
        public string dflt_value { get; set; }
        public bool pk { get; set; }
        public string Name => name;
        public string AutoIncrement { get; set; }
        public string Type => type;
        public string PrimaryKey => pk? "PrimaryKey":null;
        public string Unique { get; set; }

    }

    public class index_list
    {
        public int seq { get; set; }
        public string name { get; set; }
        public bool unique { get; set; }
    }
    public class index_info
    {
        public int seqno { get; set; }
        public int cid { get; set; }
        public string name { get; set; }
    }
}
