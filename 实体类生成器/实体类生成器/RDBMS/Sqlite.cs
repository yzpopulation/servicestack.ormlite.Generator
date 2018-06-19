using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite.Sqlite;
using 实体类生成器.Table;

namespace 实体类生成器.RDBMS
{
   public class Sqlite
    {
        public Rdbms rdbm { get; set; }
        public OrmLiteConnectionFactory fac { get; set; }
        public Sqlite(OrmLiteConnectionFactory _fac)
        {
             fac=_fac;
            rdbm = new Rdbms();
            rdbm.Db=new List<DataBases>();
            rdbm.Db.Add(new DataBases());
        }

      

        public void GetTables()
        {
            using (var db=fac.OpenDbConnection())
            {
                var li = db.Select<string>(
                    $"SELECT name AS ObjectName FROM sqlite_master WHERE type = \'table\' ORDER BY type");
               rdbm.Db[0].Tableses=new List<Tables>();
                foreach (string s in li)
                {
                    rdbm.Db[0].Tableses.Add(new Tables() { TableName =s });
                }
               
            }
        }

        public void GetColumns(string tablename)
        {
            var first = rdbm.Db[0].Tableses.First(s => s.TableName == tablename);
            if (first==null)
            {return;  
            }
            if (first.Columnses==null)
            {
                first.Columnses=new List<Columns>();
            }
            first.Columnses.Clear();
            using (var db=fac.OpenDbConnection())
            {
                var table_infos = db.ExecuteReader($"pragma table_info({tablename})").ConvertToList<TableInfo>(SqliteOrmLiteDialectProvider.Instance);
                var pks = table_infos.Where(s => s.pk > 0).ToList();
                if (pks.Count==1)
                {
                   
                    foreach (TableInfo info in table_infos)
                    {
                        var col = new Columns();
                        col.ColumnName = info.name;
                        col.Required = info.pk == 0 && info.notnull;
                        col.Default = info.dflt_value;
                        col.PrimaryKey = info.pk > 0;
                        if (col.PrimaryKey)
                        {
                            int seq = db.Single<int>($"select count(1) from sqlite_sequence where name='{tablename}'");
                            col.AutoIncrement = seq == 1;
                        }
                        first.Columnses.Add(col);
                    }
                }
                if (pks.Count>1)
                {
                    foreach (TableInfo info in table_infos)
                    {
                        var col = new Columns();
                        col.ColumnName = info.name;
                        col.Default = info.dflt_value;
                        first.Columnses.Add(col);
                        if (info.pk > 0)
                        {
                            first.CompositeKey+=$"\"{info.name}\",";
                        }
                        else
                        {
                            col.Required = info.notnull;
                        }
                      
                    }
                    first.CompositeKey= first.CompositeKey.Remove(first.CompositeKey.Length - 1);
                }
                if (pks.Count==0)
                {
                    foreach (TableInfo info in table_infos)
                    {
                        var col = new Columns();
                        col.ColumnName = info.name;
                        col.Required = info.notnull;
                        col.Default = info.dflt_value;
                        first.Columnses.Add(col);
                    }
                }


            }
        }

        #region SqliteClass

        private class DatabaseList
        {
            public long seq { get; set; }
            public string name { get; set; }
            public string file { get; set; }
        }
        private class TableInfo
        {
            public int cid { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public bool notnull { get; set; }
            public string dflt_value { get; set; }
            public int pk { get; set; }
        }
        #endregion
    }
}
