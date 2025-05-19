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
    public partial class frmLogin : Form
    {
        public Member LoginMem{ get; set; }
        public Employee LoginEmp { get; set; }
        public NonMember LoginNonMem { get; set; }
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            txtID.Text = "filmcamera01";
            txtPwd.Text = "filmcamera01!";
            radioButton1.Checked = true;


            ////디버그
            //txtID.Text = "abcdef1";
            //txtPwd.Text = "yahooo1!";
            //radioButton2.Checked = true;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            frmNonMem frm = new frmNonMem();
            if(frm.ShowDialog()==DialogResult.OK)
            {
                NonMember nonMem = frm.NonMemInfo;
                NonMemDAC dac = new NonMemDAC();
                int iResult = dac.Insert(nonMem);
                dac.Dispose();
                if (iResult > 0)
                {
                    this.LoginNonMem = nonMem;
                    MessageBox.Show("비회원으로 접속하였습니다.");
                    this.Hide();
                    frmMainCtmr ctmr = new frmMainCtmr();
                    ctmr.NonMemberInfo(LoginNonMem);
                    ctmr.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //직원, 회원 확인
            if (radioButton2.Checked)
            {
                frmSignUp frm = new frmSignUp();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Member mem = frm.MemberInfo;
                    MemDAC dac = new MemDAC();
                    int iResult = dac.Insert(mem);
                    dac.Dispose();
                    if (iResult > 0)
                    {

                        MessageBox.Show("회원가입이 성공적으로 완료되었습니다.");
                    }
                }
            }

            else if (radioButton1.Checked)
            {
                frmEmpSignUp frm = new frmEmpSignUp();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Employee emp = frm.EmpInfo;
                    EmpDAC dac = new EmpDAC();
                    int iResult = dac.Insert(emp);
                    dac.Dispose();
                    if (iResult > 0)
                    {

                        MessageBox.Show("회원가입이 성공적으로 완료되었습니다.");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //유효성체크
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtPwd.Text))
            {
                MessageBox.Show("아이디와 비밀번호를 입력하세요.");
                return;
            }
            //입력된 아이디와 비밀번호를 DB에 전달해서, 유효한 로그인인지 체크

            if (radioButton2.Checked)
            {
                MemDAC dac = new MemDAC();
                Member loginMem = dac.Login(txtID.Text);
                dac.Dispose();
                if (loginMem == null)
                {
                    MessageBox.Show("등록된 아이디가 아닙니다. 회원가입을 해주십시오.");
                }
                else if (loginMem.Pwd != txtPwd.Text)
                {
                    MessageBox.Show("비밀번호를 다시 입력하여 주십시오.");
                }
                else
                {
                    this.LoginMem = loginMem;
                   // this.DialogResult = DialogResult.OK;
                    frmMainCtmr ctmr = new frmMainCtmr();
                    ctmr.MemberInfo(LoginMem);
                    ctmr.Show();
                    this.Hide();

                    
                }
            }

            else if (radioButton1.Checked)
            {
                if (txtID.Text.Equals("admin01"))
                {
                    EmpDAC dac = new EmpDAC();
                    Employee loginAdmin = dac.Login(txtID.Text);
                    dac.Dispose();
                    if (loginAdmin == null)
                    {
                        MessageBox.Show("등록된 아이디가 아닙니다. 회원가입을 해주십시오.");
                        return;
                    }
                    else if (loginAdmin.Pwd != txtPwd.Text)
                    {
                        MessageBox.Show("비밀번호를 다시 입력하여 주십시오.");
                        return;
                    }
                    else
                    {
                        this.LoginEmp = loginAdmin;
                        //this.DialogResult = DialogResult.OK;
                        frmMainEmp emp = new frmMainEmp();
                        emp.EmpInfo(LoginEmp);
                        emp.Show();
                        this.Hide();

                       
                    }
                }

                else
                {
                    EmpDAC dac1 = new EmpDAC();
                    Employee loginEmp = dac1.Login(txtID.Text);
                    dac1.Dispose();
                    if (loginEmp == null)
                    {
                        MessageBox.Show("등록된 아이디가 아닙니다. 회원가입을 해주십시오.");
                    }
                    else if (loginEmp.Pwd != txtPwd.Text)
                    {
                        MessageBox.Show("비밀번호를 다시 입력하여 주십시오.");
                    }
                    else
                    {
                        this.LoginEmp = loginEmp;
                        //this.DialogResult = DialogResult.OK;
                        frmMainEmp emp = new frmMainEmp();
                        emp.EmpInfo(LoginEmp);
                        emp.Show();
                        this.Hide();

                        
                        
                    }
                }
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
