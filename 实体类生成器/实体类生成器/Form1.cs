using System;
using System.Data.Common;
using System.Reflection;
using System.Windows.Forms;
using DevLib.ExtensionMethods;
using DevLib.ModernUI.Forms;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;

namespace 实体类生成器
{
    public partial class Form1 : ModernForm
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered(treeView1, true);
//            treeView1.SetPropertyValue("DoubleBuffered", true, false);
        }

        #region field

        public OrmLiteConnectionFactory fac { get; set; }

        #endregion


        private void modernButton1_Click(object sender, EventArgs e)
        {
            var fdg = new SaveFileDialog();
            fdg.CreatePrompt = true;
            fdg.OverwritePrompt = false;
            if (fdg.ShowDialog() == DialogResult.OK) modernTextBox1.Text = fdg.FileName;
        }

        private void modernButton2_Click(object sender, EventArgs e)
        {
//            fac           = new OrmLiteConnectionFactory(modernTextBox1.Text, SqliteOrmLiteDialectProvider.Instance);

            try
            {
//                using (var db = fac.OpenDbConnection())
//                {
                var gt = new GeneratorTables();
                var sb = new MySqlConnectionStringBuilder();
                sb.Server = "127.0.0.1";
                sb.UserID = "root";
                sb.Password = "root";
                DbConnection p = new MySqlConnection(sb.ConnectionString);
                p.Open();
                gt.Generator(MySqlClientFactory.Instance, p);
//                }
            }
            catch (Exception ex)
            {
                tips.Text = ex.Message;
            }
        }
        public static void DoubleBuffered(Control dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
        #region check选择事件


        //     afterCheck
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)    //当选中或取消选中树节点上的复选框时发生
        {
            try
            {
                if (e.Node.Nodes.Count > 0)
                {
                    bool NoFalse = true;
                    foreach (TreeNode tn in e.Node.Nodes)
                    {
                        if (tn.Checked == false)
                        {
                            NoFalse = false;
                        }
                    }
                    if (e.Node.Checked == true || NoFalse)
                    {
                        foreach (TreeNode tn in e.Node.Nodes)
                        {
                            if (tn.Checked != e.Node.Checked)
                            {
                                tn.Checked = e.Node.Checked;
                            }
                        }
                    }
                }
                if (e.Node.Parent != null && e.Node.Parent is TreeNode)
                {
                    bool ParentNode = true;
                    foreach (TreeNode tn in e.Node.Parent.Nodes)
                    {
                        if (tn.Checked == false)
                        {
                            ParentNode = false;
                        }
                    }
                    if (e.Node.Parent.Checked != ParentNode && (e.Node.Checked == false || e.Node.Checked == true && e.Node.Parent.Checked == false))
                    {
                        e.Node.Parent.Checked = ParentNode;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        #endregion

  }
}