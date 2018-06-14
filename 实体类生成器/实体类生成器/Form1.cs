using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevLib.ModernUI.Forms;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite.Sqlite;
using 实体类生成器.Sqlite;

namespace 实体类生成器
{
    public partial class Form1 : ModernForm
    {
        #region field
        public OrmLiteConnectionFactory fac { get; set; }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }



        private void modernButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdg = new SaveFileDialog();
            fdg.CreatePrompt    = true;
            fdg.OverwritePrompt = false;
            if (fdg.ShowDialog() == DialogResult.OK)
            {
                modernTextBox1.Text = fdg.FileName;
            }


        }

        private void modernButton2_Click(object sender, EventArgs e)
        {
            fac           = new OrmLiteConnectionFactory(modernTextBox1.Text, SqliteOrmLiteDialectProvider.Instance);
            try
            {
                using (var db = fac.OpenDbConnection())
                {
                    tips.Text = db.State.ToString();

                    var tables= db.Select<Sqlite.Tables>("SELECT name as objname FROM \"main\".sqlite_master WHERE type = \'table\' and name <> \'sqlite_sequence\' ORDER BY type;");
                    foreach (Tables table in tables)
                    {
                      var table_infos= db.ExecuteReader($"pragma table_info({table.objname})").ConvertToList<table_info>(SqliteOrmLiteDialectProvider.Instance);

                      var index_lists= db.ExecuteReader($"pragma index_list({table.objname});").ConvertToList<index_list>(SqliteOrmLiteDialectProvider.Instance).Where(s=>s.unique).ToList();
                        foreach (index_list indexList in index_lists)
                        {
                          var indexinfos= db.ExecuteReader($"pragma index_info({table.objname});").ConvertToList<index_info>(SqliteOrmLiteDialectProvider.Instance);
                            foreach (index_info info in indexinfos)
                            {
                                foreach (var tableinfo in table_infos.Where(s => s.name == info.name))
                                {
                                    tableinfo.Unique = "Unique";
                                }
                                
                            }
                          
                        }
                        if (db.RowCount($"select * from sqlite_sequence where name =\'{table.objname}\'")==1)
                        {
                            var a=table_infos.FirstOrDefault(s => s.pk == true && s.type.ToUpper() == "INTEGER");
                            if (a!=null)
                            {
                                a.AutoIncrement = "AutoIncrement";
                            }
                               
                        }
                        table.infos = new List<object>(table_infos);

                    }
                  var ab=  tables[0].ToClassString();
                }
            }
            catch (Exception ex)
            {
                tips.Text=ex.Message;
            }
            
        }
    }



}
