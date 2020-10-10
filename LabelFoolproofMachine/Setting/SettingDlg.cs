using ConfigManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelFoolproofMachine
{
    public partial class SettingDlg : Form
    {
        public SettingDlg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("相机名字不能为空");
            }
            else
            {


                PublicData.settingMessage.CaremerName = textBox1.Text;
                IniManager.WriteToIni(PublicData.settingMessage, Application.StartupPath + "\\Config" + "\\SettingMessage.Jason");
                MessageBox.Show("保存成功！请重启软件！");
            }
        }

        private void SettingDlg_Load(object sender, EventArgs e)
        {
            if (PublicData.settingMessage == null)
            {
                PublicData.settingMessage = new SettingMessage();
            }
            textBox1.Text = PublicData.settingMessage.CaremerName.ToString();
        }

    

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
