using HalconDotNet;
using LabelFoolproofMachine.Halcon;
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
    public partial class SmallLableDlg : Form
    {
        public SmallLableDlg()
        {
            InitializeComponent();
        }
        private HalconDotNet.HTuple WindowsHandle = new HalconDotNet.HTuple();
        private HObject Image = new HObject();
        private HObject HRegion = new HObject();
        public static HObject 小标签有无的框 = new HObject();
        public static HObject 小标签圆弧的框 = new HObject();
        public static HObject 小标签翘起的框 = new HObject();
        public static HTuple RegionNumber = new HTuple();
        public static HObject SmallSelectedRegions = new HObject();
        public static HTuple mean = new HTuple();//灰度平均值
        private void SmallLableDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            Image.Dispose();
            HOperatorSet.GenEmptyObj(out Image);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            HalconCommonFunc.ReadImage(out Image, WindowsHandle, pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            HalconCommonFunc.SmallLableNothing(Image, HRegion, WindowsHandle, out RegionNumber, out SmallSelectedRegions);
            小标签有无的框 = HRegion;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            HalconCommonFunc.SmallLableCircle(Image, HRegion, WindowsHandle, out mean);
            小标签圆弧的框 = HRegion;
        }
        public void DrawRegionCheck()
        {
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

        }
    }
}
