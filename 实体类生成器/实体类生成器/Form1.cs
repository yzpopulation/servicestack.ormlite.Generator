using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevLib.ExtensionMethods;
using DevLib.ModernUI.Forms;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace 实体类生成器
{
    public partial class Form1 : ModernForm
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered(treeView1, true);
            modernTextBox_Saveto.Text = AppDomain.CurrentDomain.BaseDirectory;
        }

        #region field

        public string connectionstring { get; set; }

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
            connectionstring = "datasource=" + modernTextBox1.Text;
            try
            {
                NewGenerator();
                SchemaReaderClass.Tables tbs= GeneratorTables.GetInstance().GetTables(SQLiteFactory.Instance, connectionstring);
                LoadTreeNodes(tbs);
            }
            catch (Exception ex)
            {
                tips.Text = ex.Message;
            }
        }
        private new static void DoubleBuffered(Control dgv, bool setting)
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

        private void modernButton_Generate_Click(object sender, EventArgs e)
        {
            NewGenerator();
            try
            {
                List<SchemaReaderClass.Table> tbs = new List<SchemaReaderClass.Table>();
                foreach (TreeNode node in treeView1.Nodes)
                {
                    foreach (TreeNode n in node.Nodes)
                    {
                        if (n.Checked)
                        {
                            tbs.Add((SchemaReaderClass.Table)n.Tag);
                        }
                    }
                }
                if (tbs.Count>0)
                {
                    GeneratorTables.GetInstance().Generator(tbs, modernTextBox_Saveto.Text);
                    MessageBox.Show("生成成功！");
                    string path = Path.GetFullPath(modernTextBox_Saveto.Text);
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("请选择待生成表");
                }
               
            }
            catch (Exception exception)
            {
                MessageBox.Show("生成失败！");
                MessageBox.Show(exception.Message);
             
            }
        
        

        }
        private void NewGenerator()
        {
            GeneratorTables.GetNewInstance(modernTextBox_namespace.Text,
                                           modernCheckBox_MakeSingular.Checked,
                                           modernTextBox_ClassPrefix.Text,
                                           modernTextBox_ClassSuffix.Text,
                                           modernCheckBox_SplitIntoMultipleFiles.Checked,
                                           modernTextBox_MultipleFileName.Text,
                                           modernCheckBox_GenerateConstructor.Checked);
        }
        private void LoadTreeNodes(SchemaReaderClass.Tables tbs)
        {
            treeView1.Nodes.Clear();
            List<TreeNode> tns = new List<TreeNode>();
            foreach (var tb in tbs)
            {

                string schema = tb.Schema.IsNullOrWhiteSpace() ? "NULL" : tb.Schema;
                string table  = tb.Name;
                var    find   = tns.Find(s => s.Text == schema);
                if (find == null)
                {
                    TreeNode tn = new TreeNode(schema);
                    find = tn;
                    tns.Add(find);
                }
                TreeNode it = new TreeNode(table);
                it.Tag = tb;
                find.Nodes.Add(it);
            }
            foreach (TreeNode tn in tns)
            {
                treeView1.Nodes.Add(tn);
            }
        }

        private void modernButton_Saveto_Click(object sender, EventArgs e)
        {
            var fdg = new FolderBrowserDialog();
          if (fdg.ShowDialog() == DialogResult.OK) modernTextBox_Saveto.Text = fdg.SelectedPath;
        }

        private void modernButton3_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder sb=new MySqlConnectionStringBuilder();
            sb.Server = modernTextBox2.Text;
            sb.Port =uint.Parse(modernTextBox3.Text);
            sb.UserID = modernTextBox4.Text;
            sb.Password = modernTextBox5.Text;


            connectionstring = sb.ConnectionString;
            try
            {
                NewGenerator();
                SchemaReaderClass.Tables tbs = GeneratorTables.GetInstance().GetTables(MySqlClientFactory.Instance, connectionstring);
                LoadTreeNodes(tbs);
            }
            catch (Exception ex)
            {
                tips.Text = ex.Message;
            }
        }

        private void modernButton4_Click(object sender, EventArgs e)
        {
            OracleConnectionStringBuilder sb = new OracleConnectionStringBuilder();
            sb.DataSource = modernTextBox9.Text;
            sb.UserID = modernTextBox8.Text;
            sb.Password = modernTextBox6.Text;
            sb.ConnectionTimeout = 99999;



            connectionstring = sb.ConnectionString;
            try
            {
                NewGenerator();
                SchemaReaderClass.Tables tbs = GeneratorTables.GetInstance().GetTables(OracleClientFactory.Instance, connectionstring);
                LoadTreeNodes(tbs);
            }
            catch (Exception ex)
            {
                tips.Text = ex.Message;
            }
        }
    }
}