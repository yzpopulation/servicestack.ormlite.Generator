using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevLib.ModernUI.Forms;
using MySql.Data.MySqlClient;
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
//            fac           = new OrmLiteConnectionFactory(modernTextBox1.Text, SqliteOrmLiteDialectProvider.Instance);
           
            try
            {
//                using (var db = fac.OpenDbConnection())
//                {
                   GeneratorTables gt=new GeneratorTables();
                MySqlConnectionStringBuilder sb=new MySqlConnectionStringBuilder();
                sb.Server = "127.0.0.1";
                sb.UserID = "root";
                sb.Password = "root";
                   DbConnection p =new MySqlConnection(sb.ConnectionString); 
                    p.Open();
                    gt.Generator(MySqlClientFactory.Instance,  p );
//                }
            }
            catch (Exception ex)
            {
                tips.Text=ex.Message;
            }
            
        }
    }



}
