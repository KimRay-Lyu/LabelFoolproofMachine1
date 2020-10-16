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
        private HObject HRegion = new HObject();
        public static HTuple RegionNumber = new HTuple();
        HObject Eigs;
        private void SmallLableDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            HalconCommonFunc.SetPart(WindowsHandle, 1920, 1200, pictureBox1.Width, pictureBox1.Height);
            HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    PublicData.createNewCheckModel.chickMineLableModel.SmallLableImage.Dispose();
        //    HalconCommonFunc.ReadImage(out PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (DrawRegionCheck() == 1)
            {
                PublicData.createNewCheckModel.chickMineLableModel.LableNothingRegion = HRegion;
                HalconCommonFunc.SmallLableNothing(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableNothingRegion,
                    out HTuple LableNothingMean /*out PublicData.createNewCheckModel.chickMineLableModel.SmallSelectedRegions*/);
                HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.chickMineLableModel.LableNothingRegion, "blue", WindowsHandle, 2);
                PublicData.createNewCheckModel.chickMineLableModel.LableNothingMean = LableNothingMean.D;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (DrawRegionCheck() == 1)
            {
                PublicData.createNewCheckModel.chickMineLableModel.LableCircleRegion = HRegion;
                HalconCommonFunc.SmallLableCircle(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableCircleRegion,
                    out HTuple SmallLableMean);
                //PublicData.createNewCheckModel.chickMineLableModel.SmallLableMean = SmallLableMean.D;
            }


        }
        public int DrawRegionCheck()
        {

            pictureBox1.Focus();
            if (PublicData.GetImage == false)
            {
                MessageBox.Show("未获取到图片");
                return 0;
            }
            else
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

                }
                return 1;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DrawRegionCheck() == 1)
            {
                PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion1 = HRegion;
                HalconCommonFunc.SmallLabledistance(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion1,
                    out HTuple DistanceMin, out Eigs);
                HalconCommonFunc.DisplayRegionOrXld(HRegion, "blue", WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(Eigs, "blue", WindowsHandle, 2);
                PublicData.createNewCheckModel.chickMineLableModel.DistanceMin = DistanceMin.D;
                //double x = DistanceMin.D / 18.18;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (DrawRegionCheck() == 1)
            {
                PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion2 = HRegion;
                HalconCommonFunc.SmallLabledistance(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion2,
                    out HTuple DistanceMin1, out Eigs);
                HalconCommonFunc.DisplayRegionOrXld(HRegion, "blue", WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(Eigs, "blue", WindowsHandle, 2);
                PublicData.createNewCheckModel.chickMineLableModel.DistanceMin1 = DistanceMin1.D;
            }
        }

        private void SmallLableDlg_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
            }
        }
    }
}
