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
    public partial class frmEmpSignUp : Form
    {
        public frmEmpSignUp()
        {
            InitializeComponent();
        }

        public Employee EmpInfo
        {
            get
            {
                Employee emp = new Employee();
                emp.ID = txtID.Text;
                emp.Pwd = txtPwd.Text;
                emp.Name = txtName.Text;
                emp.HireDate = DateTime.Now;

                return emp;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //ID 유효성 검사 : 알파벳 소문자, 숫자만. 7자리 이상 15자리 이하
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }

            if (!CommonUtil.IDCheck(txtID.Text))
            {
                MessageBox.Show("알파벳 소문자와 숫자를 한 번씩 사용하여 만들어주세요.");
                return;

            }


            //Pwd 유효성 검사

            if (!CommonUtil.PwdCheck(txtPwd.Text))
            {
                MessageBox.Show("비밀번호는 알파벳 소문자, 숫자, 특수문자를 사용하여 만들어주세요.");
                return;
            }
            if (txtPwd.Text != txtPwd2.Text)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            //이름 유효성 검사 : 한글이랑 알파벳 소문자, 대문자만. 2자리 이상 30자리 이하
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (!(CommonUtil.NameCheck(txtName.Text)))
            {
                MessageBox.Show("유효한 이름을 입력해주세요.");
                return;
            }
            if (txtName.Text.Length < 2 || txtName.Text.Length > 30)
            {
                MessageBox.Show("이름은 2자리 이상 30자리 이하여야 합니다.");
                return;
            }

            //처리
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpSignUp_Load(object sender, EventArgs e)
        {
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmIDChk frm = new frmIDChk();
            //frm.Parent = this;
            frm.MemID = txtID.Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtID.Text = frm.MemID;
                txtID.Enabled = false;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
