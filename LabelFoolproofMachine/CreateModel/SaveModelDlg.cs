using ConfigManager;
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
    public partial class SaveModelDlg : Form
    {
        public SaveModelDlg()
        {
            InitializeComponent();
        }
        //HTuple ModelID;
        //HObject Image;
        //HObject HRegion;
        //OrientationModelDlg OrientationModelDlg;
        //BigLableDlg BigLableDlg = new BigLableDlg();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("输入名称不能为空");
            }
            else
            {
                string path = Application.StartupPath + "\\Model\\" + textBox1.Text;
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                PublicData.createNewChickModel.WriteModel(path);
                //HOperatorSet.WriteShapeModel(ModelID, path);
                //HOperatorSet.WriteImage(Image, "bmp", 0, path);
                //HOperatorSet.WriteRegion(HRegion, path);
            }
           
        }

        private void SaveModelDlg_Load(object sender, EventArgs e)
        {
            //ModelID = OrientationModelDlg.modelID;
            //Image = OrientationModelDlg.Image;
            //HRegion = BigLableDlg.HRegion;
        }
    }
}
