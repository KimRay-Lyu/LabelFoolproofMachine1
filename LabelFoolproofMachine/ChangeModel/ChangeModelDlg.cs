using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelFoolproofMachine
{
    public partial class ChangeModelDlg : Form
    {
        public string sChangeModelPath="";
        public ChangeModelDlg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            sChangeModelPath = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            sChangeModelPath = Application.StartupPath + "\\Model\\"+comboBox1.SelectedItem.ToString();
        }

        private void ChangeModelDlg_Load(object sender, EventArgs e)
        {
            string folderFullName = Application.StartupPath + "\\Model\\";
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            comboBox1.Items.Clear();
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                comboBox1.Items.Add(NextFolder.Name);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            sChangeModelPath = "";
        }
    }
}
