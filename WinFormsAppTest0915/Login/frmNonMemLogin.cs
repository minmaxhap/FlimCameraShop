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
    public partial class frmNonMem : Form
    {
        public frmNonMem()
        {
            InitializeComponent();
        }

        public NonMember NonMemInfo
        {
            get
            {
                

                Random rnd = new Random();
                rnd.Next(1, 100);

                NonMember nonMem = new NonMember();
                nonMem.Code = string.Concat('N', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //13자리
                nonMem.Name = txtName.Text;
                nonMem.Phone1 = cboPhone1.SelectedValue.ToString();
                nonMem.Phone2 = txtPhone2.Text;
                nonMem.Phone3 = txtPhone3.Text;

                return nonMem;
            }
        }

        private void frmNonMem_Load(object sender, EventArgs e)
        {
            string[] category = { "휴대전화" };
            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            cboPhone1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //유효성 검사
            ValidationUtil valid = new ValidationUtil();
            valid.NameValid(txtName.Text);
            valid.PhoneValid(cboPhone1.SelectedValue.ToString(), txtPhone2.Text, txtPhone3.Text);
            
            //처리
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
