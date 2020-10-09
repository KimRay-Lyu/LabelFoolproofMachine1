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
using HalconDotNet;

namespace LabelFoolproofMachine
{
    public partial class Form1 : Form
    {

        private ChangeModelDlg changeModelDlg = new ChangeModelDlg();
        private SettingDlg settingDlg = new SettingDlg();
        //public HTuple WindowsHandle;

        private RunThread runthread = new RunThread();
        public Form1()
        {
            InitializeComponent();
            runthread.TheadWorkResultEvent += Runthread_TheadWorkResultEvent1;
            serialPort1.Open();
        }


        private void Runthread_TheadWorkResultEvent1(object sender, RunThread.TheadWorkResultEventArgs e)
        {
            byte[] buffer = new byte[3] { 0x8f, 0x01, 0x7f };
            serialPort1.Write(buffer, 0, 3);
            HOperatorSet.WriteImage(runthread.CameraImage, "bmp", 0, Application.StartupPath + "\\ErrorImage\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"));
            MessageBox.Show(e.sResult);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out PublicData.WindowsHandle);
            HalconCommonFunc.SetPart(PublicData.WindowsHandle, 1920, 1200, pictureBox1.Width, pictureBox1.Height);
            PublicData.settingMessage = IniManager.ReadFromIni<SettingMessage>(Application.StartupPath + "\\Config" + "\\SettingMessage.Jason");
            HkCameraCltr.EnumDevices();
            if (0 == PublicData.hkCameraCltr.OpenDevices(PublicData.settingMessage.CaremerName))
            {
                MessageBox.Show("相机连接成功");
            }
            else
            {
                MessageBox.Show("相机连接失败");
            }


        }

        private void 新建视觉模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateModelDlg createModelDlg = new CreateModelDlg();
            PublicData.createNewCheckModel = new CheckModel(); 
            createModelDlg.ShowDialog();
            createModelDlg.Dispose();
        }

        private void 切换模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //changeModelDlg.SendModelName = this.Receiver;
            if (changeModelDlg.ShowDialog() == DialogResult.OK)
            {
                PublicData.CheckModel = IniManager.ReadFromIni<CheckModel>(changeModelDlg.sChangeModelPath + "\\CheckModel.jason");
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
            if (label2.Text == "xxxx")
            {
                MessageBox.Show("未选择模板");

            }
            else
            {
                if (StartCheckButon.Text == "开 始 检 测")
                {
                    runthread.StartWork();
                    StartCheckButon.Text = "检测中";
                    StartCheckButon.Enabled = false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runthread.StopWork();
            StartCheckButon.Text = "开 始 检 测";
            StartCheckButon.Enabled = true;

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            runthread.StopWork();
            PublicData.hkCameraCltr.CloseDevices();
            //serialPort1.Close();

        }

        private void 启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[3] { 0x8f, 0x00, 0x7f };
            serialPort1.Write(buffer, 0, 3);
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[3] { 0x8f, 0x01, 0x7f };
            serialPort1.Write(buffer, 0, 3);
        }

        private void 打开本地图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PublicData.createNewCheckModel.ModelImage.Dispose();
            int Res2 = PublicData.hkCameraCltr.Capture(out PublicData.createNewCheckModel.ModelImage);
            if (Res2 == 0)
            {

                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, PublicData.WindowsHandle);
            }
            else
            {
                MessageBox.Show("相机图像获取失败");
            }
        }
    }
}
