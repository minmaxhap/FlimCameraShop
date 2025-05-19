using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public partial class frmSignUp : Form
    {
        
        public frmSignUp()
        {
            InitializeComponent();
        }

        public Member MemberInfo
        {
            get
            {
                Member mem = new Member();
                mem.ID = txtID.Text;
                mem.Pwd = txtPwd.Text;
                mem.Name = txtName.Text;
                mem.Birth = dtpBirth.Value.ToString("yyyy-MM-dd");
                mem.Phone1 = cboPhone1.SelectedValue.ToString();
                mem.Phone2 = txtPhone2.Text;
                mem.Phone3 = txtPhone3.Text;
                mem.Zip = txtZip.Text;
                mem.Addr1 = txtAddr1.Text;
                mem.Addr2 = txtAddr2.Text;
                mem.Email = txtEmail.Text;
                if (txtRecom.Text.Length > 0)
                { mem.Recom = txtRecom.Text; }
                return mem;
            }

        }

        private void frmSignUp_Load(object sender, EventArgs e)
        {
            //핸드폰 앞자리 콤보박스 데이터 바인딩
            string[] category = { "휴대전화" };
            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            //cboPhone1.DropDownStyle = ComboBoxStyle.DropDownList;
            //핸드폰 앞자리 기본값 010 설정
            //cboPhone1.SelectedIndex = 1;

            //생일 값 날짜로만 전달하기.
            dtpBirth.Format = DateTimePickerFormat.Short;




        }
  


        private void button2_Click(object sender, EventArgs e)
        {
            //유효성 검사

            
            //ID 유효성 검사 : 알파벳 소문자, 숫자만. 7자리 이상 15자리 이하
            //if (txtID.Text.Trim().Length < 1)
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
            //if (txtID.Text.Length<7 || txtID.Text.Length > 15)
            //    MessageBox.Show("아이디는 7자리 이상 15자리 이하여야 한니다.");
                
            //Pwd 유효성 검사
            //if (txtPwd.Text.Length < 8 || txtPwd.Text.Length > 15)
            //    MessageBox.Show("비밀번호는 8자리 이상 15자리 이하여야 합니다.");    
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
            {   MessageBox.Show("필수 입력입니다.");
                 return; 
            }
            if (!(CommonUtil.NameCheck(txtName.Text)))
            {   MessageBox.Show("유효한 이름을 입력해주세요.");
                    return; 
            }
            if (txtName.Text.Length < 2 || txtName.Text.Length > 30)
            {   MessageBox.Show("이름은 2자리 이상 30자리 이하여야 합니다.");
                return; 
            }

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

            //추천인 id 유효성 검사
            if (!CommonUtil.IDCheck(txtID.Text))
             {               
                MessageBox.Show("유효한 ID를 입력해주세요.");
                    return; 
            }

            //개인정보 제공 동의
            if (!checkBox1.Checked)
            {               
                MessageBox.Show("개인정보 제공에 동의해주세요.");
                return; 
            }
            
  
            //처리
            this.DialogResult = DialogResult.OK;
            this.Close();


        }
        private void txtID_Validating(object sender, CancelEventArgs e)
        {

        }
        //ID 중복체크 팝업 열기
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
        //휴대폰 유효성 검사(숫자, 백스페이스)
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

        //주소 검색 팝업 열고 값 가져오기
        private void button1_Click(object sender, EventArgs e)
        {
            frmZipSearchPopUp frm = new frmZipSearchPopUp();
            if(frm.ShowDialog()==DialogResult.OK)
            {
                txtZip.Text = frm.ZipCode.ToString();
                txtAddr1.Text = frm.Addr1;
                txtAddr2.Text = frm.Addr2;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtAddr1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddr2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
