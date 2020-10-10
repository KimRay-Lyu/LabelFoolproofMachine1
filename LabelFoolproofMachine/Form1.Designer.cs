namespace LabelFoolproofMachine
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.新建视觉模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开本地图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取相机图片测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.传送带控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartCheckButon = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建视觉模板ToolStripMenuItem,
            this.切换模板ToolStripMenuItem,
            this.调试ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 新建视觉模板ToolStripMenuItem
            // 
            this.新建视觉模板ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.新建视觉模板ToolStripMenuItem.Name = "新建视觉模板ToolStripMenuItem";
            this.新建视觉模板ToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.新建视觉模板ToolStripMenuItem.Text = "新建视觉模板";
            this.新建视觉模板ToolStripMenuItem.Click += new System.EventHandler(this.新建视觉模板ToolStripMenuItem_Click);
            // 
            // 切换模板ToolStripMenuItem
            // 
            this.切换模板ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.切换模板ToolStripMenuItem.Name = "切换模板ToolStripMenuItem";
            this.切换模板ToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.切换模板ToolStripMenuItem.Text = "切换视觉模板";
            this.切换模板ToolStripMenuItem.Click += new System.EventHandler(this.切换模板ToolStripMenuItem_Click);
            // 
            // 调试ToolStripMenuItem
            // 
            this.调试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开本地图片ToolStripMenuItem,
            this.获取相机图片测试ToolStripMenuItem,
            this.toolStripSeparator1,
            this.传送带控制ToolStripMenuItem});
            this.调试ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.调试ToolStripMenuItem.Name = "调试ToolStripMenuItem";
            this.调试ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.调试ToolStripMenuItem.Text = "调试";
            this.调试ToolStripMenuItem.Click += new System.EventHandler(this.调试ToolStripMenuItem_Click);
            // 
            // 打开本地图片ToolStripMenuItem
            // 
            this.打开本地图片ToolStripMenuItem.Name = "打开本地图片ToolStripMenuItem";
            this.打开本地图片ToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.打开本地图片ToolStripMenuItem.Text = "导入本地图片测试";
            this.打开本地图片ToolStripMenuItem.Click += new System.EventHandler(this.打开本地图片ToolStripMenuItem_Click);
            // 
            // 获取相机图片测试ToolStripMenuItem
            // 
            this.获取相机图片测试ToolStripMenuItem.Name = "获取相机图片测试ToolStripMenuItem";
            this.获取相机图片测试ToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.获取相机图片测试ToolStripMenuItem.Text = "获取相机图片测试";
            this.获取相机图片测试ToolStripMenuItem.Click += new System.EventHandler(this.获取相机图片测试ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // 传送带控制ToolStripMenuItem
            // 
            this.传送带控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动ToolStripMenuItem,
            this.停止ToolStripMenuItem});
            this.传送带控制ToolStripMenuItem.Name = "传送带控制ToolStripMenuItem";
            this.传送带控制ToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.传送带控制ToolStripMenuItem.Text = "传送带控制";
            // 
            // 启动ToolStripMenuItem
            // 
            this.启动ToolStripMenuItem.Name = "启动ToolStripMenuItem";
            this.启动ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.启动ToolStripMenuItem.Text = "启动";
            this.启动ToolStripMenuItem.Click += new System.EventHandler(this.启动ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // StartCheckButon
            // 
            this.StartCheckButon.BackColor = System.Drawing.Color.Gainsboro;
            this.StartCheckButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartCheckButon.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartCheckButon.Location = new System.Drawing.Point(791, 533);
            this.StartCheckButon.Name = "StartCheckButon";
            this.StartCheckButon.Size = new System.Drawing.Size(207, 52);
            this.StartCheckButon.TabIndex = 2;
            this.StartCheckButon.Text = "开 始 检 测";
            this.StartCheckButon.UseVisualStyleBackColor = false;
            this.StartCheckButon.Click += new System.EventHandler(this.StartCheckButon_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gainsboro;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(830, 607);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 42);
            this.button2.TabIndex = 3;
            this.button2.Text = "停止检测";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(797, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "当前模板：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(826, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "xxxx";
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM10";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(8, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(777, 661);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 699);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.StartCheckButon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标签纸视觉防呆系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新建视觉模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开本地图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取相机图片测试ToolStripMenuItem;
        private System.Windows.Forms.Button StartCheckButon;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 传送带控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.IO.Ports.SerialPort serialPort1;
    }
}

