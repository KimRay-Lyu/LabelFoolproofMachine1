using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
    public partial class CreateModelDlg : Form
    {
        public CreateModelDlg()
        {
            InitializeComponent();
            buttons.Add(MuBtn01);
            buttons.Add(MuBtn02);
            buttons.Add(MuBtn03);
            buttons.Add(MuBtn04);
        }
        OrientationModelDlg orientationModelDlg= new OrientationModelDlg();
        BigLableDlg bigLableDlg=new BigLableDlg();
        SmallLableDlg smallLableDlg= new SmallLableDlg();
        OtherLocationDlg otherLocationDlg= new OtherLocationDlg();
        SaveModelDlg saveModelDlg = new SaveModelDlg();
        private int TabSelectIndex = -1;
        private List<Button> buttons = new List<Button>();
        private HObject Image;
        HTuple WindowsHandle = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            MuBtn01_Click(sender, new EventArgs());
            TabSelectIndex = 0;
            buttons[TabSelectIndex].BackColor = Color.LightGray;

        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
            
        }
        /// <summary>
        /// 隐藏旧窗口，显示新窗口
        /// </summary>
        /// <param name="HideForm"></param>
        /// <param name="HideForm1"></param>
        /// <param name="HideForm2"></param>
        /// <param name="ObjForm"></param>
        public void DisForm(Form HideForm, Form HideForm1, Form HideForm2, Form ObjForm)
        {
            HideForm.Hide();
            HideForm1.Hide();
            HideForm2.Hide();
            ObjForm.TopLevel = false;
            ObjForm.FormBorderStyle = FormBorderStyle.None;
            ObjForm.Parent = this.panel4;
            ObjForm.Size = panel4.Size;
            ObjForm.Show();
        }
        private void MuBtn01_Click(object sender, EventArgs e)
        {
            DisForm(bigLableDlg, smallLableDlg, otherLocationDlg, orientationModelDlg);
        }

        private void MuBtn02_Click(object sender, EventArgs e)
        {
            DisForm(orientationModelDlg, smallLableDlg, otherLocationDlg, bigLableDlg);
        }
        private void MuBtn03_Click(object sender, EventArgs e)
        {
            DisForm(orientationModelDlg, otherLocationDlg, bigLableDlg, smallLableDlg);
        }
        private void MuBtn04_Click(object sender, EventArgs e)
        {
            DisForm(orientationModelDlg, bigLableDlg, smallLableDlg, otherLocationDlg);
          
        }
        private void MuBtn_MouseLeave(object sender, EventArgs e)
        {
            Button sendButton = (Button)sender;
            string ch = sendButton.Name.Substring(5);
            int nowSelectIndex = int.Parse(ch, new CultureInfo("en-GB")) - 1;
            if (nowSelectIndex != TabSelectIndex)
            {
                sendButton.BackColor = System.Drawing.Color.White;
            }

        }

        private void MuBtn_MouseMove(object sender, MouseEventArgs e)
        {
            Button sendButton = (Button)sender;
            string ch = sendButton.Name.Substring(5);
            int nowSelectIndex = int.Parse(ch, new CultureInfo("en-GB")) - 1;
            if (nowSelectIndex != TabSelectIndex)
            {
                sendButton.BackColor = Color.LightGray;

            }
        }

        public void MuBtn_MouseUp(object sender, MouseEventArgs e)
        {
            Button sendButton = (Button)sender;
            string ch = sendButton.Name.Substring(5);
            TabSelectIndex = int.Parse(ch, new CultureInfo("en-GB")) - 1;
            foreach (var tem in buttons)
            {

                tem.BackColor = Color.White;
            }
            buttons[TabSelectIndex].BackColor = Color.LightGray;
            //buttons[TabSelectIndex].ForeColor = Color.FromArgb(35, 39, 48);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveModelDlg.ShowDialog();
        }
    }
}
