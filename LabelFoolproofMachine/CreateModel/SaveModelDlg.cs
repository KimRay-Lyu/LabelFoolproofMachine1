using ConfigManager;
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
using HalconDotNet;

namespace LabelFoolproofMachine
{
    public partial class SaveModelDlg : Form
    {
        public SaveModelDlg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("输入名称不能为空");
            }
            else
            {
                string path = Application.StartupPath + "\\Model\\" + textBox1.Text;
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                PublicData.createNewCheckModel.WriteModel(path);
                IniManager.WriteToIni(PublicData.createNewCheckModel, path + "\\CheckModel.jason");
                MessageBox.Show("模板保存成功");
            }
           
        }

        private void SaveModelDlg_Load(object sender, EventArgs e)
        {
         
        }

     

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
