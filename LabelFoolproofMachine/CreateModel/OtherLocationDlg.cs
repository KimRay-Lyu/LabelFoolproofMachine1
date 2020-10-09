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
    public partial class OtherLocationDlg : Form
    {
        public OtherLocationDlg()
        {
            InitializeComponent();
        }
        private HalconDotNet.HTuple WindowsHandle = new HalconDotNet.HTuple();
        private HObject HRegion = new HObject();

        private void OtherLocationDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            HalconCommonFunc.SetPart(WindowsHandle, 1920, 1200, pictureBox1.Width, pictureBox1.Height);
            HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawRegionCheck();
            PublicData.createNewCheckModel.checkOtherModel.OtherRegion = HRegion;
            HalconCommonFunc.BigLableblob(PublicData.createNewCheckModel.ModelImage,
                 PublicData.createNewCheckModel.checkOtherModel.OtherRegion,
                out HTuple OtherNumber,
                out PublicData.createNewCheckModel.checkOtherModel.OtherSelect);
            HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.checkOtherModel.OtherSelect, "blue", WindowsHandle, 2);
            PublicData.createNewCheckModel.checkOtherModel.OtherNumber = OtherNumber.D;
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void OtherLocationDlg_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
            }
        }
    }
}
