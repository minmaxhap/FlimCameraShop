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
    public partial class frmRes : Form
    {
        int price;
        Member mem;
        NonMember nonmem;
        public Member CurrentMem { get; set; }
        public NonMember CurrentNonMem { get; set; }
        public frmRes()
        {
            InitializeComponent();
        }

        public Reserve CurrentRes
        {
            get
            {
                Random rnd = new Random();
                rnd.Next(1, 100);

                Reserve res = new Reserve();

                res.Code = string.Concat('R', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //14자리
                res.Name = txtName.Text;
                res.Email = txtEmail.Text;
                res.Date = dtpDate.Value;
                res.Phone1 = cboPhone1.SelectedValue.ToString();
                res.Phone2 = txtPhone2.Text;
                res.Phone3 = txtPhone3.Text;
                res.Style = txtStyle.Text;
                res.Payopt = cboPayopt.SelectedValue.ToString();
                res.Type = cboType.SelectedValue.ToString();
                res.Price = price;
                res.Email = txtEmail.Text;
                res.MemID = CurrentMem.ID;
                res.IsMem = "Y";
    
                return res;
            }

           
        


    }

        private void button2_Click(object sender, EventArgs e)
        {

            //트랜잭션 처리할 게 있을까? 만약 member 테이블에 결제 금액을 포함시키려면 필요하겠다. 근데 계획으로는 상품 구매 금액만 넣을 생각이라..넣으면 좋긴 한데 일단 보류!! 
            //일단 여기는 트랜잭션 처리할 게 없다.

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

            if (string.IsNullOrWhiteSpace(cboType.SelectedValue.ToString()))
            {
                MessageBox.Show("촬영 유형을 선택해주세요.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cboPayopt.SelectedValue.ToString()))
            {
                MessageBox.Show("결제 방식을 선택해주세요.");
                return;
            }



            ResDAC dac = new ResDAC();
            bool bResult = dac.Insert(CurrentRes);
            dac.Dispose();

            if (bResult)
            {
                MessageBox.Show("촬영 예약이 정상적으로 처리되었습니다.");
            }
            


        }

        private void frmRes_Load(object sender, EventArgs e)
        {
            //핸드폰 앞자리 콤보박스
            //촬영 유형 콤보박스
            //결제 방식 콤보박스
            string[] category = { "휴대전화","촬영 유형","결제 방식" };
            

            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboType, "촬영 유형", dtCode);
            CommonUtil.ComboBinding(cboPayopt, "결제 방식", dtCode);
            lblPrice.Text = "";
            dtpDate.Format = DateTimePickerFormat.Short;
            if (CurrentMem != null)
            {
                mem = CurrentMem;
                txtName.Text = mem.Name;
                txtEmail.Text = mem.Email;
                cboPhone1.SelectedValue = mem.Phone1;
                txtPhone2.Text = mem.Phone2;
                txtPhone3.Text = mem.Phone3;

            }
            if (CurrentNonMem != null)
            {
                nonmem = CurrentNonMem;
                txtName.Text = nonmem.Name;
                cboPhone1.SelectedValue = nonmem.Phone1;
                txtPhone2.Text = nonmem.Phone2;
                txtPhone3.Text = nonmem.Phone3;
            }

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

        private void cboType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedValue != null) //며칠 뒤 다시 보니까 이 부분은 수정해야겠는데..
            {
                if (cboType.SelectedValue.ToString() == "J0102")
                {
                    price = 300000;
                    lblPrice.Text = price.ToString("#,##0"); 
                }
                else if (cboType.SelectedValue.ToString() == "J0101")
                {
                    price = 150000;
                    lblPrice.Text = price.ToString("#,##0"); 
                }
                else
                {
                    price = 40000;
                    lblPrice.Text = price.ToString("#,##0"); 
                }
            }
        }
    
    }
}
