using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public partial class frmIDChk : Form
    {
        public frmIDChk()
        {
            InitializeComponent();
        }

        bool bCheck = false;

        public string MemID
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }


        public frmIDChk(string userID)
        {
            InitializeComponent();

            textBox1.Text = userID;
        }

        private void frmIDCheckPopup_Load(object sender, EventArgs e)
        {
            //Form1 frm = (Form1)this.Owner;
            //textBox1.Text = frm.txtUserID.Text;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bCheck)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("ID 중복체크를 다시 해주세요.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("사용하실 아이디를 입력하세요.");
                return;
            }

            MemDAC dac = new MemDAC();
            int chk = dac.IDCheck(textBox1.Text);
            dac.Dispose();

            if (chk > 0)
            {
                label1.Text = "이미 사용중인 아이디입니다.";
                bCheck = false;
            }
            else
            {
                label1.Text = "사용 가능한 아이디입니다.";
                bCheck = true;
            }
        }
    }
}
