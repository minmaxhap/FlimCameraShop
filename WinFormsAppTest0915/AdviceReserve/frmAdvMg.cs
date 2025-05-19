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
    public partial class frmAdvMg : Form
    {
        Advice adv;
        DataTable dt;
        DataTable dtCode;
        public Employee CurrentEmp { get; set; }
        public frmAdvMg()
        {
            InitializeComponent();
        }

        private void FrmAdv_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;


            main.Read += OnRead;
            main.Create += OnCreate;
            main.Delete += OnDelete;
            main.Update += OnUpdate;

            date.Show1Week += On1Week;
            date.Show1Month += On1Month;
            date.Show3Months += On3Months;
            date.Show6Months += On6Months;
            date.ShowControl += OnCotrol;

            string[] category = { "휴대전화", "상담 플랫폼" };
            CodeDAC dac = new CodeDAC();
            dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboPlatform, "상담 플랫폼", dtCode, true);


            DataGridViewUtil.SetInitGridView(dataGridView1);


            DataGridViewUtil.AddGridTextColumn(dataGridView1, "문의 코드", "code");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "질문자명", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone" );


            DataGridViewUtil.AddGridTextColumn(dataGridView1, "문의 플랫폼", "platformname");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "문의 날짜", "date");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "문의내용", "contents");

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "답변 여부", "isreply");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "답변 날짜", "repdate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "답변 직원ID", "repid");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "답변 직원명", "repname");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "답변 내용", "repcontents");


            LoadData();

            
        
        }

        private void LoadData()
        {
            AdvDAC dac = new AdvDAC();
            dt = dac.GetAdvInfo();
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            txtContents.Clear();
            txtPhone2.Clear();
            txtPhone3.Clear();
            txtName.Clear();


        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            int rowIndex = dataGridView1.CurrentRow.Index;
            adv = new Advice()
            {
                Code = txtCode.Text,
                Platform = cboPlatform.SelectedValue.ToString(),
                Phone1 = cboPhone1.SelectedValue.ToString(),
                Name = txtName.Text,
                Contents = txtContents.Text,
                RepName = txtRepName.Text,
                RepContents = txtRepContents.Text,
                RepDate = dtpReplyDate.Value,
                Date = dtpDate.Value,
                Phone2 = txtPhone2.Text,
                Phone3 = txtPhone3.Text,
                IsReply = txtIsReply.Text,
                RepID = txtRepID.Text
                
                

            };
            AdvDAC dac = new AdvDAC();
            bool bResult = dac.Update(adv);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("문의 답변이 수정되었습니다.");
            }



        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            AdvDAC dac = new AdvDAC();
            bool bResult = dac.Delete(txtCode.Text);
            if (bResult)
            {
                LoadData();
                MessageBox.Show("해당 문의 답변이 삭제되었습니다.");
            }
        }

        private void OnCreate(object sender, EventArgs e)
        {

            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            Random rnd = new Random();
            string code = string.Concat('A', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //14자리
            Advice adv = new Advice()
            {

                Code = code,
                Platform = cboPlatform.SelectedValue.ToString(),
                Phone1 = cboPhone1.SelectedValue.ToString(),
                Contents = txtContents.Text,
                Name = txtName.Text,
                RepName = txtRepName.Text,
                RepContents = txtRepContents.Text,
                RepDate = dtpReplyDate.Value,
                Date = dtpDate.Value,
                Phone2 = txtPhone2.Text,
                Phone3 = txtPhone3.Text,
                IsReply = txtIsReply.Text,
                RepID = txtRepID.Text

            };

            AdvDAC dac = new AdvDAC();
            bool bResult = dac.Insert(adv);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("문의 답변이 등록되었습니다.");
            }

        }

        private void OnRead(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            LoadData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = dataGridView1.CurrentRow.Index;
            string[] phone = dataGridView1[2, rowIndex].Value.ToString().Split('-');
            string phone1 = phone[0].ToString();
            
            int phone2 = Convert.ToInt32(phone[1]);
            int phone3 = Convert.ToInt32(phone[2]);

            string platform = dataGridView1[3, rowIndex].Value.ToString();
            DataRow[] dr = dtCode.Select($"name = '{phone1}'");
            DataRow[] dr1 = dtCode.Select($"name = '{platform}'");

            txtCode.Text = dataGridView1[0, rowIndex].Value.ToString();
            txtName.Text = dataGridView1[1, rowIndex].Value.ToString();
            cboPhone1.SelectedValue = dr[0]["code"].ToString();
            txtPhone2.Text = phone2.ToString();
            txtPhone3.Text = phone3.ToString();
            cboPlatform.SelectedValue = dr1[0]["code"].ToString();
            dtpDate.Value = (DateTime)dataGridView1[4, rowIndex].Value;
            txtContents.Text = dataGridView1[5, rowIndex].Value.ToString();
            txtIsReply.Text = dataGridView1[6, rowIndex].Value.ToString();
            if (txtIsReply.Text == "Y")
            {
                dtpReplyDate.Value = (DateTime)dataGridView1[7, rowIndex].Value;
                txtRepID.Text = dataGridView1[8, rowIndex].Value.ToString();
                txtRepName.Text = dataGridView1[9, rowIndex].Value.ToString();
                txtRepContents.Text = dataGridView1[10, rowIndex].Value.ToString();
            }
            else
            {
                txtRepID.Text = CurrentEmp.ID;
                txtRepName.Text = CurrentEmp.Name;
            }

        }

        private void LoadData(DataTable dt)
        {

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }
        private void OnCotrol(object sender, DateTimeEventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"date>= '{date.DtpLeft}' AND date <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }


        private void On6Months(object sender, EventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-6);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"date>= '{date.DtpLeft}' AND date <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On3Months(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-3);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"date>= '{date.DtpLeft}' AND date <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Month(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-1);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"date>= '{date.DtpLeft}' AND date <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Week(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddDays(-7);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"date>= '{date.DtpLeft}' AND date <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }
    }
}
