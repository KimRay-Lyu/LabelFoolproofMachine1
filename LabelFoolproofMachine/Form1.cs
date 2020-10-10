using ConfigManager;
using HalconDotNet;
using HkCamera;
using LabelFoolproofMachine.Halcon;
using System;
using System.Windows.Forms;

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
            //serialPort1.Open();
        }


        private void Runthread_TheadWorkResultEvent1(object sender, RunThread.TheadWorkResultEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                byte[] buffer = new byte[3] { 0x8f, 0x01, 0x7f };
                serialPort1.Write(buffer, 0, 3);
            }
            HOperatorSet.WriteImage(PublicData.CheckModel.ModelImage, "bmp", 0, Application.StartupPath + "\\ErrorImage\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"));
            MessageBox.Show(e.sResult);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out PublicData.WindowsHandle);
            HalconCommonFunc.SetPart(PublicData.WindowsHandle, 1920, 1200, pictureBox1.Width, pictureBox1.Height);
            PublicData.settingMessage = IniManager.ReadFromIni<SettingMessage>(Application.StartupPath + "\\Config" + "\\SettingMessage.Jason");
            HkCameraCltr.EnumDevices();
           PublicData.OpenCrame = PublicData.hkCameraCltr.OpenDevices(PublicData.settingMessage.CaremerName);
            if (0 == PublicData.OpenCrame)
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
            if (label2.Text == "xxxx")
            {
                MessageBox.Show("未选择模板");
                return;
            }
            if (StartCheckButon.Text != "开 始 检 测")
            {
                MessageBox.Show("当前检测未停止");
                return;
            }
            if (PublicData.OpenCrame != 0)
            {
                MessageBox.Show("相机未开启");

            }
            else
            {
                PublicData.CheckModel.ModelImage.Dispose();
                int Res2 = PublicData.hkCameraCltr.Capture(out PublicData.CheckModel.ModelImage);
                if (Res2 == 0)
                {
                    HalconCommonFunc.DisplayImage(PublicData.CheckModel.ModelImage, PublicData.WindowsHandle);
                    PublicData.调试模式 = 2;//获取相机单次检测
                    runthread.StartWork();
                }
                else
                {
                    MessageBox.Show("相机图像获取失败");
                }
            }
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
                return;
            }
            if (serialPort1.IsOpen==false)
            {
                MessageBox.Show("串口未开启，重启软件重试");
                return;
            }
            if (PublicData.OpenCrame !=0)
            {
                MessageBox.Show("相机未开启");
            }
            else
            {
                if (StartCheckButon.Text == "开 始 检 测")
                {
                    PublicData.调试模式 = 0;
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
            try
            {
                if (serialPort1.IsOpen)
                {
                    byte[] buffer = new byte[3] { 0x8f, 0x00, 0x7f };
                    serialPort1.Write(buffer, 0, 3);
                }
                else
                {
                    MessageBox.Show("串口未连接");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    byte[] buffer = new byte[3] { 0x8f, 0x01, 0x7f };
                    serialPort1.Write(buffer, 0, 3);
                }
                else
                {
                    MessageBox.Show("串口未连接");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void 打开本地图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label2.Text == "xxxx")
            {
                MessageBox.Show("未选择模板");
                return;
            }
            if (StartCheckButon.Text != "开 始 检 测")
            {
                MessageBox.Show("当前检测未停止");
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    string file = dialog.FileNames[i];
                    PublicData.CheckModel.ModelImage.Dispose();
                    HOperatorSet.ReadImage(out PublicData.CheckModel.ModelImage, file);
                    HalconCommonFunc.DisplayImage(PublicData.CheckModel.ModelImage, PublicData.WindowsHandle);
                }


                PublicData.调试模式 = 1;//1是本地单次检测
                runthread.StartWork();
            }





        }

        private void 调试ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
