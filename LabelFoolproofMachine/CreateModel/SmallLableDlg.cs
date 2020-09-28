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
            HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    PublicData.createNewCheckModel.chickMineLableModel.SmallLableImage.Dispose();
        //    HalconCommonFunc.ReadImage(out PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            PublicData.createNewCheckModel.chickMineLableModel.LableNothingRegion = HRegion;
            HalconCommonFunc.SmallLableNothing(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableNothingRegion,
                out HTuple LableNothingNumber, out PublicData.createNewCheckModel.chickMineLableModel.SmallSelectedRegions);
            HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.chickMineLableModel.SmallSelectedRegions,"blue",WindowsHandle,2);
            PublicData.createNewCheckModel.chickMineLableModel.LableNothingNumber = LableNothingNumber.D;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            PublicData.createNewCheckModel.chickMineLableModel.LableCircleRegion = HRegion;
            HalconCommonFunc.SmallLableCircle(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableCircleRegion,
                out HTuple SmallLableMean);
            PublicData.createNewCheckModel.chickMineLableModel.SmallLableMean = SmallLableMean.D;

        }
        public void DrawRegionCheck()
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

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion1 = HRegion;
            HalconCommonFunc.SmallLabledistance(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion1,
                out HTuple DistanceMin, out Eigs);
            PublicData.createNewCheckModel.chickMineLableModel.DistanceMin = DistanceMin.D;
            //HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle, pictureBox1);
            HalconCommonFunc.DisplayRegionOrXld(HRegion, "blue", WindowsHandle, 2);
            HalconCommonFunc.DisplayRegionOrXld(Eigs, "blue", WindowsHandle, 2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion2 = HRegion;
            HalconCommonFunc.SmallLabledistance(PublicData.createNewCheckModel.ModelImage, PublicData.createNewCheckModel.chickMineLableModel.LableDistanceRegion2,
                out HTuple DistanceMin1, out Eigs);
            PublicData.createNewCheckModel.chickMineLableModel.DistanceMin1 = DistanceMin1.D;
            HalconCommonFunc.DisplayRegionOrXld(HRegion, "blue", WindowsHandle, 2);
            HalconCommonFunc.DisplayRegionOrXld(Eigs, "blue", WindowsHandle, 2);
        }
    }
}
