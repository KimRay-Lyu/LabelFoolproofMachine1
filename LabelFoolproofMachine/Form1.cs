using ConfigManager;
using HkCamera;
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
    public partial class Form1 : Form
    {
        private CreateModelDlg createModelDlg = new CreateModelDlg();
        private ChangeModelDlg changeModelDlg = new ChangeModelDlg();
        private SettingDlg settingDlg = new SettingDlg();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string subPath = Application.StartupPath + "\\Config";
            PublicData.settingMessage = IniManager.ReadFromIni<SettingMessage>(subPath + "\\SettingMessage.Jason");
            //连接相机
            //HkCameraCltr.EnumDevices();
            //if (0 == PublicData.hkCameraCltr.OpenDevices(PublicData.settingMessage.CaremerName))
            //{
            //    MessageBox.Show("连接成功");
            //}
        }

        private void 新建视觉模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createModelDlg.ShowDialog();
        }

        private void 切换模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeModelDlg.ShowDialog();
        }

        private void 获取相机图片测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingDlg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "1";
        }
    }
}
