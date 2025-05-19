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
    public partial class frmMemMyInfo : Form
    {
        public Member CurrentMem { get; set; }
        public frmMemMyInfo()
        {
            InitializeComponent();
        }

        public Member  MemInfo(Member mem)
        {
            this.CurrentMem = mem;
            return CurrentMem;
        }

        private void frmMyInfo_Load(object sender, EventArgs e)
        {
            if (CurrentMem != null)
            {
                txtID.Text = CurrentMem.ID;
                txtPwd.Text = CurrentMem.Pwd;
                txtName.Text = CurrentMem.Name;
                txtZip.Text = CurrentMem.Zip;
                txtAddr1.Text = CurrentMem.Addr1;
                txtAddr2.Text = CurrentMem.Addr2;
                txtEmail.Text = CurrentMem.Email;
                dtpBirth.Text = CurrentMem.Birth;
                cboPhone1.SelectedValue = CurrentMem.Phone1;
                txtPhone2.Text = CurrentMem.Phone2;
                txtPhone3.Text = CurrentMem.Phone3;
                lblPoints.Text = CurrentMem.Points.ToString();
               
                if (CurrentMem.Recom != null)
                    txtRecom.Text = CurrentMem.Recom;


            }
            txtID.ReadOnly = true;
            txtName.ReadOnly = true;
            txtRecom.ReadOnly = true;
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

        private void button7_Click(object sender, EventArgs e)
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

            MemDAC dac = new MemDAC();
            CurrentMem.ID = txtID.Text;
            CurrentMem.Pwd = txtPwd.Text;
            CurrentMem.Zip = txtZip.Text;

            CurrentMem.Addr1 = txtAddr1.Text;
            CurrentMem.Addr2 = txtAddr2.Text;
            CurrentMem.Email = txtEmail.Text;
            CurrentMem.Birth = dtpBirth.Text;
            CurrentMem.Phone1 = cboPhone1.SelectedValue.ToString();
            CurrentMem.Phone2 = txtPhone2.Text;
            CurrentMem.Phone3 = txtPhone3.Text;

            bool bResult = dac.Update(CurrentMem);
            dac.Dispose();
            if (bResult)
            { MessageBox.Show("회원정보가 수정되었습니다."); }

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

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("정말 탈퇴하시겠습니까?","탈퇴하기",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                MemDAC dac = new MemDAC();
                dac.Quit(CurrentMem.ID);
                dac.Dispose();
                MessageBox.Show("탈퇴 처리를 완료했습니다.");
            }
        }
    }
}
