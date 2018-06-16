namespace 实体类生成器
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.modernTabControl1 = new DevLib.ModernUI.Forms.ModernTabControl();
            this.modernTabPage1 = new DevLib.ModernUI.Forms.ModernTabPage();
            this.modernButton2 = new DevLib.ModernUI.Forms.ModernButton();
            this.modernButton1 = new DevLib.ModernUI.Forms.ModernButton();
            this.modernTextBox1 = new DevLib.ModernUI.Forms.ModernTextBox();
            this.modernLabel1 = new DevLib.ModernUI.Forms.ModernLabel();
            this.modernTabPage2 = new DevLib.ModernUI.Forms.ModernTabPage();
            this.modernTabPage3 = new DevLib.ModernUI.Forms.ModernTabPage();
            this.modernTabPage4 = new DevLib.ModernUI.Forms.ModernTabPage();
            this.modernTabPage5 = new DevLib.ModernUI.Forms.ModernTabPage();
            this.tips = new DevLib.ModernUI.Forms.ModernLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.StatusStrip.SuspendLayout();
            this.modernTabControl1.SuspendLayout();
            this.modernTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip
            // 
            this.StatusStrip.Controls.Add(this.tips);
            this.StatusStrip.Location = new System.Drawing.Point(1, 600);
            this.StatusStrip.Size = new System.Drawing.Size(793, 20);
            // 
            // modernTabControl1
            // 
            this.modernTabControl1.Controls.Add(this.modernTabPage1);
            this.modernTabControl1.Controls.Add(this.modernTabPage2);
            this.modernTabControl1.Controls.Add(this.modernTabPage3);
            this.modernTabControl1.Controls.Add(this.modernTabPage4);
            this.modernTabControl1.Controls.Add(this.modernTabPage5);
            this.modernTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.modernTabControl1.Location = new System.Drawing.Point(20, 60);
            this.modernTabControl1.Name = "modernTabControl1";
            this.modernTabControl1.SelectedIndex = 0;
            this.modernTabControl1.Size = new System.Drawing.Size(774, 129);
            this.modernTabControl1.TabIndex = 0;
            this.modernTabControl1.UseSelectable = true;
            // 
            // modernTabPage1
            // 
            this.modernTabPage1.Controls.Add(this.modernButton2);
            this.modernTabPage1.Controls.Add(this.modernButton1);
            this.modernTabPage1.Controls.Add(this.modernTextBox1);
            this.modernTabPage1.Controls.Add(this.modernLabel1);
            this.modernTabPage1.HorizontalScrollBarSize = 10;
            this.modernTabPage1.Location = new System.Drawing.Point(4, 38);
            this.modernTabPage1.Name = "modernTabPage1";
            this.modernTabPage1.Size = new System.Drawing.Size(766, 87);
            this.modernTabPage1.TabIndex = 0;
            this.modernTabPage1.Text = "hyfgvb";
            this.modernTabPage1.UseHorizontalBarColor = true;
            this.modernTabPage1.UseStyleColors = false;
            this.modernTabPage1.UseVerticalBarColor = true;
            this.modernTabPage1.VerticalScrollBarSize = 10;
            // 
            // modernButton2
            // 
            this.modernButton2.Location = new System.Drawing.Point(674, 49);
            this.modernButton2.Name = "modernButton2";
            this.modernButton2.Size = new System.Drawing.Size(75, 23);
            this.modernButton2.TabIndex = 5;
            this.modernButton2.Text = "连接";
            this.modernButton2.UseSelectable = true;
            this.modernButton2.UseStyleColors = false;
            this.modernButton2.UseVisualStyleBackColor = true;
            this.modernButton2.Click += new System.EventHandler(this.modernButton2_Click);
            // 
            // modernButton1
            // 
            this.modernButton1.Location = new System.Drawing.Point(674, 20);
            this.modernButton1.Name = "modernButton1";
            this.modernButton1.Size = new System.Drawing.Size(75, 23);
            this.modernButton1.TabIndex = 4;
            this.modernButton1.Text = "浏览";
            this.modernButton1.UseSelectable = true;
            this.modernButton1.UseStyleColors = false;
            this.modernButton1.UseVisualStyleBackColor = true;
            this.modernButton1.Click += new System.EventHandler(this.modernButton1_Click);
            // 
            // modernTextBox1
            // 
            this.modernTextBox1.Lines = new string[0];
            this.modernTextBox1.Location = new System.Drawing.Point(95, 20);
            this.modernTextBox1.MaxLength = 2147483647;
            this.modernTextBox1.Name = "modernTextBox1";
            this.modernTextBox1.PasswordChar = '\0';
            this.modernTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.modernTextBox1.SelectedText = "";
            this.modernTextBox1.Size = new System.Drawing.Size(573, 23);
            this.modernTextBox1.TabIndex = 3;
            this.modernTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.modernTextBox1.UseSelectable = true;
            this.modernTextBox1.UseStyleColors = false;
            this.modernTextBox1.UseSystemPasswordChar = false;
            this.modernTextBox1.WordWrap = true;
            // 
            // modernLabel1
            // 
            this.modernLabel1.AutoSize = true;
            this.modernLabel1.Location = new System.Drawing.Point(3, 20);
            this.modernLabel1.Name = "modernLabel1";
            this.modernLabel1.Size = new System.Drawing.Size(65, 19);
            this.modernLabel1.TabIndex = 2;
            this.modernLabel1.Text = "文件路径";
            this.modernLabel1.UseStyleColors = false;
            // 
            // modernTabPage2
            // 
            this.modernTabPage2.HorizontalScrollBarSize = 10;
            this.modernTabPage2.Location = new System.Drawing.Point(4, 38);
            this.modernTabPage2.Name = "modernTabPage2";
            this.modernTabPage2.Size = new System.Drawing.Size(766, 81);
            this.modernTabPage2.TabIndex = 1;
            this.modernTabPage2.Text = "MySql";
            this.modernTabPage2.UseHorizontalBarColor = true;
            this.modernTabPage2.UseStyleColors = false;
            this.modernTabPage2.UseVerticalBarColor = true;
            this.modernTabPage2.VerticalScrollBarSize = 10;
            // 
            // modernTabPage3
            // 
            this.modernTabPage3.HorizontalScrollBarSize = 10;
            this.modernTabPage3.Location = new System.Drawing.Point(4, 38);
            this.modernTabPage3.Name = "modernTabPage3";
            this.modernTabPage3.Size = new System.Drawing.Size(766, 81);
            this.modernTabPage3.TabIndex = 2;
            this.modernTabPage3.Text = "SqlServer";
            this.modernTabPage3.UseHorizontalBarColor = true;
            this.modernTabPage3.UseStyleColors = false;
            this.modernTabPage3.UseVerticalBarColor = true;
            this.modernTabPage3.VerticalScrollBarSize = 10;
            // 
            // modernTabPage4
            // 
            this.modernTabPage4.HorizontalScrollBarSize = 10;
            this.modernTabPage4.Location = new System.Drawing.Point(4, 38);
            this.modernTabPage4.Name = "modernTabPage4";
            this.modernTabPage4.Size = new System.Drawing.Size(766, 81);
            this.modernTabPage4.TabIndex = 3;
            this.modernTabPage4.Text = "PostgreSQL";
            this.modernTabPage4.UseHorizontalBarColor = true;
            this.modernTabPage4.UseStyleColors = false;
            this.modernTabPage4.UseVerticalBarColor = true;
            this.modernTabPage4.VerticalScrollBarSize = 10;
            // 
            // modernTabPage5
            // 
            this.modernTabPage5.HorizontalScrollBarSize = 10;
            this.modernTabPage5.Location = new System.Drawing.Point(4, 38);
            this.modernTabPage5.Name = "modernTabPage5";
            this.modernTabPage5.Size = new System.Drawing.Size(766, 81);
            this.modernTabPage5.TabIndex = 4;
            this.modernTabPage5.Text = "Oracle";
            this.modernTabPage5.UseHorizontalBarColor = true;
            this.modernTabPage5.UseStyleColors = false;
            this.modernTabPage5.UseVerticalBarColor = true;
            this.modernTabPage5.VerticalScrollBarSize = 10;
            // 
            // tips
            // 
            this.tips.AutoSize = true;
            this.tips.Location = new System.Drawing.Point(17, 1);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(0, 0);
            this.tips.TabIndex = 0;
            this.tips.UseCustomBackColor = true;
            this.tips.UseStyleColors = false;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(29, 195);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点1";
            treeNode1.Text = "节点1";
            treeNode2.Name = "节点0";
            treeNode2.Text = "节点0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView1.Size = new System.Drawing.Size(172, 399);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 621);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.modernTabControl1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Resizable = false;
            this.ShowStatusStrip = true;
            this.Text = "实体类生成器";
            this.UseMaximizeBox = false;
            this.Controls.SetChildIndex(this.modernTabControl1, 0);
            this.Controls.SetChildIndex(this.StatusStrip, 0);
            this.Controls.SetChildIndex(this.treeView1, 0);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.modernTabControl1.ResumeLayout(false);
            this.modernTabPage1.ResumeLayout(false);
            this.modernTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevLib.ModernUI.Forms.ModernTabControl modernTabControl1;
        private DevLib.ModernUI.Forms.ModernTabPage modernTabPage1;
        private DevLib.ModernUI.Forms.ModernTabPage modernTabPage2;
        private DevLib.ModernUI.Forms.ModernTabPage modernTabPage3;
        private DevLib.ModernUI.Forms.ModernTabPage modernTabPage4;
        private DevLib.ModernUI.Forms.ModernTabPage modernTabPage5;
        private DevLib.ModernUI.Forms.ModernLabel modernLabel1;
        private DevLib.ModernUI.Forms.ModernTextBox modernTextBox1;
        private DevLib.ModernUI.Forms.ModernButton modernButton1;
        private DevLib.ModernUI.Forms.ModernButton modernButton2;
        private DevLib.ModernUI.Forms.ModernLabel tips;
        private System.Windows.Forms.TreeView treeView1;
    }
}

