using ConfigManager;
using HalconDotNet;
using HkCamera;
using LabelFoolproofMachine.Halcon;
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
    public partial class Form1 : Form
    {
        private CreateModelDlg createModelDlg = new CreateModelDlg();
        private ChangeModelDlg changeModelDlg = new ChangeModelDlg();
        private SettingDlg settingDlg = new SettingDlg();
        HTuple WindowsHandle = new HTuple();
        public Form1()
        {
            InitializeComponent();

        }
        private void Receiver(string ModelName)
        {
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //string subPath = Application.StartupPath + "\\Config";
            //PublicData.settingMessage = IniManager.ReadFromIni<SettingMessage>(subPath + "\\SettingMessage.Jason");
            //连接相机
            //HkCameraCltr.EnumDevices();
            //if (0 == PublicData.hkCameraCltr.OpenDevices(PublicData.settingMessage.CaremerName))
            //{
            //    MessageBox.Show("连接成功");
            //}
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);

        }

        private void 新建视觉模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createModelDlg.ShowDialog();
        }

        private void 切换模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //changeModelDlg.SendModelName = this.Receiver;
            if(changeModelDlg.ShowDialog() == DialogResult.OK)
            {
                PublicData.CheckModel = IniManager.ReadFromIni<CheckModel>(changeModelDlg.sChangeModelPath + "\\SettingMessage.jason");
                if (PublicData.CheckModel == null)
                {
                    PublicData.CheckModel = new CheckModel();
                }
                PublicData.CheckModel.ReadModel(changeModelDlg.sChangeModelPath);
                label2.Text = changeModelDlg.ModelName;
            }        
        }

        private void 获取相机图片测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingDlg.ShowDialog();
        }
        private void StartCheckButon_Click(object sender, EventArgs e)
        {
            PublicData.createNewCheckModel.ModelImage.Dispose();
            HalconCommonFunc.ReadImage(out PublicData.CheckModel.ModelImage, WindowsHandle, pictureBox1);
            HalconCommonFunc.LableCheck(WindowsHandle,out HObject Trancontors);
            HalconCommonFunc.DisplayRegionOrXld(Trancontors, "green", WindowsHandle, 2);
        }


    }
}
