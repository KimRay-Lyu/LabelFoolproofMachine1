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

            if (DrawRegionCheck()==1)
            {
                //HOperatorSet.GenRectangle2(out HRegion, 698.174, 420.195, -1.58345, 292.862, 11.1591);
                PublicData.createNewCheckModel.checkOtherModel.OtherRegion = HRegion;
                HalconCommonFunc.四周bleblob(PublicData.createNewCheckModel.ModelImage,
                     PublicData.createNewCheckModel.checkOtherModel.OtherRegion, out HTuple OtherNumber);
                HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.checkOtherModel.OtherRegion, "blue", WindowsHandle, 2);
                PublicData.createNewCheckModel.checkOtherModel.OtherNumber = OtherNumber.D;
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
                        //HOperatorSet.GenRectangle2(out HRegion, 488.094, 1483.98, -1.59531, 302.478, 27.5584);
                        break;
                    case 2:
                        HalconCommonFunc.DrawRegion(WindowsHandle, DrawModel.Circle, out HRegion);
                        break;
                   
                }
                return 1;
            }
        }

        private void OtherLocationDlg_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                HalconCommonFunc.DisplayImage(PublicData.createNewCheckModel.ModelImage, WindowsHandle);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DrawRegionCheck() == 1)
            {
               // HOperatorSet.GenRectangle2(out HRegion, 710.906, 1428.38, -1.59505, 305.66, 11.2292);
                PublicData.createNewCheckModel.checkOtherModel.OtherRegion1 = HRegion;
                HalconCommonFunc.四周bleblob(PublicData.createNewCheckModel.ModelImage,
                     PublicData.createNewCheckModel.checkOtherModel.OtherRegion1, out HTuple OtherNumber
                   );
                HalconCommonFunc.DisplayRegionOrXld(PublicData.createNewCheckModel.checkOtherModel.OtherRegion1, "blue", WindowsHandle, 2);
                
            }
        }
    }
}
