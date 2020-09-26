using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabelFoolproofMachine.Halcon;
using ConfigManager;

namespace LabelFoolproofMachine
{
    public partial class BigLableDlg : Form
    {
        public BigLableDlg()
        {
            InitializeComponent();
        }

         private HalconDotNet.HTuple WindowsHandle = new HalconDotNet.HTuple();
         private HObject Image = new HObject();
         public static HObject HRegion = new HObject();
        HTuple Area = new HTuple();
        HObject ho_SelectedRegions;

        private void BigLableDlg_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox1.Width, pictureBox1.Height, pictureBox1.Handle, "visible", "", out WindowsHandle);
            Image.Dispose();
            HOperatorSet.GenEmptyObj(out Image);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HalconCommonFunc.ReadImage(out Image,WindowsHandle,pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///qqqqq
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

            
            HalconCommonFunc.Blob(Image,HRegion, WindowsHandle, out Area,out ho_SelectedRegions);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string subPath = Application.StartupPath + "\\Config";
            //PublicData.settingMessage = IniManager.ReadFromIni<SettingMessage>(subPath + "\\SettingMessage.Jason");
            PublicData.settingMessage.定位模板保存地址 = IniManager.Read(subPath + "\\SettingMessage.Jason");
            string localFilePath = PublicData.settingMessage.定位模板保存地址+"大标签";
            //SaveFileDialog sfd = new SaveFileDialog();
            ////设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;
            //sfd.Title = "保存模板";
            ////保存对话框是否记忆上次打开的目录 
            //sfd.RestoreDirectory = true;
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    localFilePath = sfd.FileName.ToString();
            //}
            
            //HOperatorSet.WriteImage(Image, "bmp", 0, localFilePath);
            HOperatorSet.WriteRegion(HRegion ,localFilePath);
           

        }
    }
}
