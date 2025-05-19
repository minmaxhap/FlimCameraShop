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
    public partial class frmEmpMyInfo : Form
    {
        public Employee CurrentEmp 
        { 
            get; 
            set; 
        
        }
        public frmEmpMyInfo()
        {
            InitializeComponent();
        }

        public Employee EmpInfo(Employee emp)
        {
            this.CurrentEmp = emp;
            return CurrentEmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmZipSearchPopUp frm = new frmZipSearchPopUp();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtZip.Text = frm.ZipCode.ToString();
                txtAddr1.Text = frm.Addr1;
                txtAddr2.Text = frm.Addr2;
            }
        }

        private void frmEmpMyInfo_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;
            main.ToolStripEnable(tool:false);

            EmpDAC dac = new EmpDAC();
            DataTable dt = dac.MyInfo(CurrentEmp.ID);
            DataRow dr = dt.Rows[0];
            txtID.Text = dr["id"].ToString();
              txtPwd.Text = dr["pwd"].ToString();
            txtName.Text = dr["name"].ToString();
            txtZip.Text = dr["zipcode"].ToString();
            txtAddr1.Text = dr["addr1"].ToString();
            txtAddr2.Text = dr["addr2"].ToString();
            txtEmail.Text = dr["email"].ToString();
            txtHireDate.Text = dr["hiredate"].ToString();
            cboPhone1.SelectedValue = dr["phone1"].ToString();
            txtPhone2.Text = dr["phone2"].ToString();
            txtPhone3.Text = dr["phone3"].ToString();
            txtSNS.Text = dr["sns"].ToString();
            lblSalary.Text = $"{dr["salary"]}원";
              lblIncen.Text = $"{dr["incen"]}%";
              cboTask.SelectedValue = dr["task"].ToString();
            dtpBirth.Value = Convert.ToDateTime(dr["birth"]);


            txtID.ReadOnly = true;
            txtName.ReadOnly = true;
            //핸드폰 앞자리 콤보박스 데이터 바인딩
            string[] category = { "휴대전화", "담당업무" };
            CodeDAC dac1 = new CodeDAC();
            DataTable dtCode = dac1.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboTask, "담당업무", dtCode);

            //cboPhone1.DropDownStyle = ComboBoxStyle.DropDownList;
            //핸드폰 앞자리 기본값 010 설정
            //cboPhone1.SelectedIndex = 1;
            

        }

        private void txtPhone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txtPhone3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            //휴대폰 유효성 검사
            if (string.IsNullOrWhiteSpace(cboPhone1.SelectedValue.ToString()))
            {
                MessageBox.Show("휴대전화 앞자리를 다시 한번 선택해주세요.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone2.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone3.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }

            //이메일 유효성 검사
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (!CommonUtil.EmailCheck(txtEmail.Text))
            {
                MessageBox.Show("유효한 이메일을 입력해주세요.");
                return;
            }

            //주소 유효성 검사
            if (string.IsNullOrWhiteSpace(txtAddr2.Text))
            {
                MessageBox.Show("상세주소를 입력해주세요.");
                return;
            }

            EmpDAC dac = new EmpDAC();
            CurrentEmp.ID = txtID.Text;
            CurrentEmp.Pwd = txtPwd.Text;
            CurrentEmp.Zip = txtZip.Text;
            CurrentEmp.Addr1 = txtAddr1.Text;
            CurrentEmp.Addr2 = txtAddr2.Text;
            CurrentEmp.Email = txtEmail.Text;
            CurrentEmp.Birth = dtpBirth.Value;
            CurrentEmp.Phone1 = cboPhone1.SelectedValue.ToString();
            CurrentEmp.Phone2 = txtPhone2.Text;
            CurrentEmp.Phone3 = txtPhone3.Text;
            CurrentEmp.SNS = txtSNS.Text;
            CurrentEmp.Birth = dtpBirth.Value;

            bool bResult = dac.Update(CurrentEmp);
            dac.Dispose();
            if (bResult)
            { MessageBox.Show("회원정보가 수정되었습니다."); }



        }

        private void button3_Click(object sender, EventArgs e)
        {
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

            MessageBox.Show("비밀번호가 성공적으로 변경되었습니다. 수정하기 버튼을 다시 눌러주세요.");
        }

   

        private void cboPhone1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
