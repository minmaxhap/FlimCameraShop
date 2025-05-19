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
    public partial class frmAdv : Form
    {
        DataTable dtCode;
        Member mem;
        public Member CurrentMem { get; set; }
        public frmAdv()
        {
            InitializeComponent();
        }

        private void frmAdv_Load(object sender, EventArgs e)
        {
           

            string[] category = { "휴대전화", "상담 플랫폼"};
            CodeDAC dac = new CodeDAC();
             dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
          

            if (CurrentMem != null)
            {
                mem = CurrentMem;
                txtName.Text = mem.Name;
                cboPhone1.SelectedValue = mem.Phone1;
                txtPhone2.Text = mem.Phone2;
                txtPhone3.Text = mem.Phone3;

            }
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContetns.Text))
            {
                MessageBox.Show("문의 내용을 입력해주세요");
                return;
            }
            Random rnd = new Random();
            string code =  string.Concat('A', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 1000).ToString().PadLeft(3, '0'));
            DataView dv = new DataView(dtCode);
            dv.RowFilter = "name = '" + "프로그램" + "'";
            string platform = dv.ToTable().Rows[0]["code"].ToString();
            Advice adv = new Advice()
            {

                Code = code,
                ID = mem.ID,
                Name = mem.Name,
                Phone1 = mem.Phone1,
                Phone2 = mem.Phone2,
                Phone3 = mem.Phone3,
                Contents = txtContetns.Text,
                Date = DateTime.Now,
                Platform = platform

            };

            AdvDAC dac = new AdvDAC();
            dac.AskAdvice(adv);
            dac.Dispose();
            MessageBox.Show("1:1문의가 성공적으로 등록되었습니다. 답변은 1~2일 후 문의 내역 확인을 눌러 확인해주세요.");
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            
        }
    }
}
