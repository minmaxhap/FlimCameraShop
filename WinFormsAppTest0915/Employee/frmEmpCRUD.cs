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
    public partial class frmEmpCRUD : Form
    {


        public Employee SelectedEmp
        {
            get
            {
                try
                {
                    Employee emp = new Employee();
                    emp.ID = txtID.Text;
                    emp.Task = cboTask.SelectedValue.ToString();
                    emp.Salary = Convert.ToInt32(txtSalary.Text);
                    emp.Incen = Convert.ToInt32(txtIncen.Text);
                    emp.IsQuit = txtisquit.Text;
                    emp.QuitDate = Convert.ToDateTime(txtquitdate.Text);

                    return emp;
                }
                catch
                {
                    MessageBox.Show("필수 정보(담당업무, 월급, 인센티브)를 모두 입력해주세요.");
                    return null;
                }
            }
            set
            {
                 txtID.Text = value.ID;
              
                txtName.Text = value.Name;
                txtZip.Text = value.Zip;
                txtAddr1.Text = value.Addr1;
                txtAddr2.Text = value.Addr2;
                txtEmail.Text = value.Email;

                dtpHireDate.Value = value.HireDate;
                cboPhone1.SelectedValue = value.Phone1;
                txtPhone2.Text = value.Phone2;
                txtPhone3.Text = value.Phone3;
                txtSNS.Text = value.SNS;
                txtSalary.Text = value.Salary.ToString();
                txtIncen.Text = value.Incen.ToString();
                cboTask.SelectedValue = value.Task;
                 dtpBirth.Value = value.Birth;
                txtisquit.Text = value.IsQuit;
                txtquitdate.Text = value.QuitDate.ToString();
            }

        }

        public frmEmpCRUD()
        {
            InitializeComponent();

            this.Text = "직원정보 수정";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            txtisquit.Text = "Y";
            txtquitdate.Text = DateTime.Now.ToString();
            MessageBox.Show("퇴사 처리 되었습니다. 확인을 눌러주세요.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void frmEmpCRUD_Load(object sender, EventArgs e)
        {
            string[] category = {"담당업무"};
            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboTask, "담당업무", dtCode, false);

        }
    }
}
