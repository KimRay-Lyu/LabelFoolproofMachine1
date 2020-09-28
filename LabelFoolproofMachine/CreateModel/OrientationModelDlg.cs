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
using ConfigManager;
using HalconDotNet;
using LabelFoolproofMachine.Halcon;

namespace LabelFoolproofMachine
{
    public partial class OrientationModelDlg : Form
    {
        public OrientationModelDlg()
        {
            InitializeComponent();
        }
        private HTuple WindowsHandle = new HTuple();
        public static HObject Image = new HObject();
        public HObject HRegion = new HObject();
       // private HObject ModelRegion = new HObject();
        public static HTuple modelID = new HTuple();
        HObject TransContours = new HObject();

        //PublicModelParam publicModel = new PublicModelParam();

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    HalconCommonFunc.DrawRegion(WindowsHandle, DrawModel.Rectangle1, out HRegion);
                    break;
                case 1:
                    HalconCommonFunc.DrawRegion(WindowsHandle, DrawModel.Rectangle2, out HRegion);
                    break;
                case 2:
                    HalconCommonFunc.DrawRegion(WindowsHandle, DrawModel.Circle, out HRegion);
                    break;
                default: return;
            }
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    HalconCommonFunc.RegionOperatorset(PublicData.createNewCheckModel.VisualModelRegion, HRegion, OperatorModel.Union, out PublicData.createNewCheckModel.VisualModelRegion);
                    break;
                case 1:
                    HalconCommonFunc.RegionOperatorset(PublicData.createNewCheckModel.VisualModelRegion, HRegion, OperatorModel.Difference, out PublicData.createNewCheckModel.VisualModelRegion);
                    break;
                case 2:
                    HalconCommonFunc.RegionOperatorset(PublicData.createNewCheckModel.VisualModelRegion, HRegion, OperatorModel.Intersection, out PublicData.createNewCheckModel.VisualModelRegion);
                    break;
                default: return;
            }
            HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
            HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.VisualModelRegion, "blue", WindowsHandle, 2);
            

        }

        private void OrientationModelDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            //Image.Dispose();
            //HOperatorSet.GenEmptyObj(out Image);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Image.Dispose();
            int Res1 = PublicData.hkCameraCltr.DoSoftwareOnce();
            PublicData.createNewCheckModel.ModelImage.Dispose();
            int Res2 = PublicData.hkCameraCltr.Capture(out PublicData.createNewCheckModel.ModelImage);
            if (Res1 == 0 && Res2 == 0)
            {

                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
            }
            else
            {
                MessageBox.Show("相机图像获取失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HalconCommonFunc.CreateModel(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.VisualModelRegion, 
                out PublicData.createNewCheckModel.VisualModelID,out  TransContours);
            HalconCommonFunc.DisplayRegionOrXld(TransContours, "blue", WindowsHandle, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PublicData.createNewCheckModel.ModelImage.Dispose();
            HalconCommonFunc.ReadImage(out PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
        

        }
        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

            //Directory.CreateDirectory(PublicData.settingMessage.定位模板保存地址 + "\\" + DateTime.Now.ToString("yy_MM_dd_hh_mm"));
            //string localFilePath = PublicData.settingMessage.定位模板保存地址 + "\\" + DateTime.Now.ToString("yy_MM_dd_hh_mm");
            //PublicData.settingMessage.定位模板保存地址 = localFilePath;
            //IniManager.WriteToIni(PublicData.settingMessage, Application.StartupPath + "\\Config" + "\\SettingMessage.Jason");
            //SaveFileDialog sfd = new SaveFileDialog();
            ////设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;
            //sfd.Title = "保存模板";
            ////保存对话框是否记忆上次打开的目录 
            //sfd.RestoreDirectory = false;
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    localFilePath = sfd.FileName.ToString();
            //}
            //HOperatorSet.WriteShapeModel(ModelID, localFilePath + ".shm");
            //HOperatorSet.WriteImage(Image, "bmp", 0, localFilePath);

        }
    }
}
