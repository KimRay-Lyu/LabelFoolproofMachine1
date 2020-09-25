using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 向导界面
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
        Frm1 frm1 ;
        Frm2 frm2 ;
        Frm3 frm3 ;
        Frm4 frm4 ;
        private int TabSelectIndex = -1;
        private List<Button> buttons = new List<Button>();


        private void Form1_Load(object sender, EventArgs e)
        {
             frm1 = new Frm1();
             frm2 = new Frm2();
             frm3 = new Frm3();
             frm4 = new Frm4();
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
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 隐藏旧窗口，显示新窗口
        /// </summary>
        /// <param name="HideForm"></param>
        /// <param name="HideForm1"></param>
        /// <param name="HideForm2"></param>
        /// <param name="ObjForm"></param>
        public void DisForm(Form HideForm,Form HideForm1, Form HideForm2, Form ObjForm)
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
            DisForm(frm2,frm3,frm4,frm1);
        }

        private void MuBtn02_Click(object sender, EventArgs e)
        {
            DisForm(frm1, frm3, frm4, frm2);
        }
        private void MuBtn03_Click(object sender, EventArgs e)
        {
            DisForm(frm1, frm2, frm4, frm3);
        }
        private void MuBtn04_Click(object sender, EventArgs e)
        {
            DisForm(frm1, frm2, frm3, frm4);
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

       
    }
}
