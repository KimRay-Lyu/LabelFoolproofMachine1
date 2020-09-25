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
            PublicData.settingMessage.CaremerName = textBox1.Text;
            PublicData.settingMessage.定位模板保存地址 = textBox2.Text;
            IniManager.WriteToIni(PublicData.settingMessage, Application.StartupPath + "\\Config" + "\\SettingMessage.Jason");
            MessageBox.Show("保存成功！请重启软件！");
        }

        private void SettingDlg_Load(object sender, EventArgs e)
        {
            if (PublicData.settingMessage == null)
            {
                PublicData.settingMessage = new SettingMessage();
            }
            textBox1.Text = PublicData.settingMessage.CaremerName.ToString();
            textBox2.Text = PublicData.settingMessage.定位模板保存地址.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择所在文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textBox2.Text = dialog.SelectedPath;
            }
        }
    }
}
