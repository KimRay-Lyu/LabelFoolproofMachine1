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
        public HObject HRegion = new HObject();
        HObject TransContours = new HObject();
        private bool CreatModel = false;
        
        private void button1_Click(object sender, EventArgs e)
        {
            CreatModel = false;
            pictureBox1.Focus();
            if (PublicData.GetImage == false)
            {
                MessageBox.Show("未获取到图片");
                return;
            }
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
            HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
            HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.VisualModelRegion, "blue", WindowsHandle, 2);
            CreatModel = true;

        }

        private void OrientationModelDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            HalconCommonFunc.SetPart(WindowsHandle, 1920, 1200, pictureBox1.Width, pictureBox1.Height);
            //Image.Dispose();
            //HOperatorSet.GenEmptyObj(out Image);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Image.Dispose();
            if (PublicData.OpenCrame != 0)
            {
                MessageBox.Show("相机未开启");
            }
            else
            {
                PublicData.createNewCheckModel.ModelImage.Dispose();
                int Res2 = PublicData.hkCameraCltr.Capture(out PublicData.createNewCheckModel.ModelImage);
                if (Res2 == 0)
                {

                    HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
                    PublicData.GetImage = true;
                }
                else
                {
                    MessageBox.Show("相机图像获取失败");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CreatModel==true)
            {
                HalconCommonFunc.CreateModel(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.VisualModelRegion,
                out PublicData.createNewCheckModel.VisualModelID, out TransContours);
                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
                HalconCommonFunc.DisplayRegionOrXld(TransContours, "blue", WindowsHandle, 2);
            }
            else
            {
                MessageBox.Show("还未框定区域");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {          
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    string file = dialog.FileNames[i];
                    PublicData.createNewCheckModel.ModelImage.Dispose();
                    HOperatorSet.ReadImage(out PublicData.createNewCheckModel.ModelImage, file);
                    HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
                }
            }
            PublicData.GetImage = true;
        }
       
    }
}
