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
    public partial class frmResMg : Form
    {
        Reserve res;
        DataTable dt;
        DataTable dtCode;
        
        public frmResMg()
        {
            InitializeComponent();
        }

        private void frmResMg_Load(object sender, EventArgs e)
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

            ucSearch.SeachInfo += OnSearch;


            string[] category = { "휴대전화", "촬영 유형", "결제 방식" };
            CodeDAC dac = new CodeDAC();
            dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboType, "촬영 유형", dtCode, true);
            CommonUtil.ComboBinding(cboPayopt, "결제 방식", dtCode, true);


            DataGridViewUtil.SetInitGridView(dataGridView1);
            // r.name , concat(c.name, '-', phone2,'-', phone3) phone, r.code,  ismem, memid, date, empname, type, style, price, paydate, payopt, isfinished
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "예약 코드", "code");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "예약자명", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대폰 앞자리", "phone1", visibility:false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일 주소", "email");

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "회원 여부", "ismem");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "회원 ID", "memid");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "촬영 날짜", "date");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "촬영 유형 코드", "type", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "촬영 유형", "typename");

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "촬영 스타일", "style");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "금액", "price");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 방식 코드", "payopt", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 방식", "payoptname");
    
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 날짜", "paydate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 직원 이름", "empname");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "촬영 종료 여부", "isfinished");
            LoadData();

        }
        private void LoadData()
        {
            ResDAC dac = new ResDAC();
            dt = dac.GetResInfo();
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            //txtCode.Clear();
            //txtDetail.Clear();
            //txtImageFile.Clear();
            //txtName.Clear();
            //txtPrice.Clear();
            //txtQty.Clear();


        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            int rowIndex = dataGridView1.CurrentRow.Index;
            res = new Reserve()
            {
                Code = txtCode.Text,
                Phone1 = cboPhone1.SelectedValue.ToString(),
                Name = txtName.Text,
                Date = dtpDate.Value,
                Phone2 = txtPhone2.Text,
                Phone3 = txtPhone3.Text,
                IsMem = txtIsMem.Text,
                MemID = txtMemID.Text,
                Style = txtStyle.Text,
                Price = Convert.ToInt32(lblPrice.Text),
                Payopt = cboPayopt.SelectedValue.ToString(),
                Paydate = dtpDate.Value,
                Empname = txtEmpName.Text,
                Isfinished = txtIsFinished.Text,
                Email = txtEmail.Text
        };
            ResDAC dac = new ResDAC();
            bool bResult = dac.Update(res);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("촬영 예약 내역이 수정되었습니다.");
            }



        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            ResDAC dac = new ResDAC();
            bool bResult = dac.Delete(txtCode.Text);
            if (bResult)
            {
                LoadData();
                MessageBox.Show("촬영 예약 내역이 삭제되었습니다.");
            }
        }

        private void OnCreate(object sender, EventArgs e)
        {

            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            Random rnd = new Random();
            string code = string.Concat('R', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //14자리
            res = new Reserve()
            {
                Code = txtCode.Text,
                Phone1 = cboPhone1.SelectedValue.ToString(),
                Name = txtName.Text,
                Date = dtpDate.Value,
                Phone2 = txtPhone2.Text,
                Phone3 = txtPhone3.Text,
                IsMem = txtIsMem.Text,
                MemID = txtMemID.Text,
                Style = txtStyle.Text,
                Price = Convert.ToInt32(lblPrice.Text),
                Payopt = cboPayopt.SelectedValue.ToString(),
                Paydate = dtpDate.Value,
                Empname = txtEmpName.Text,
                Isfinished = txtIsFinished.Text,
                Email = txtEmail.Text
            };
            ResDAC dac = new ResDAC();
            bool bResult = dac.Insert(res);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("촬영 예약 내역이 등록되었습니다.");
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
            string[] phone = dataGridView1["phone", rowIndex].Value.ToString().Split('-');
            string phone1 = phone[0].ToString();

            int phone2 = Convert.ToInt32(phone[1]);
            int phone3 = Convert.ToInt32(phone[2]);

            //string type = dataGridView1[6, rowIndex].Value.ToString();

            //string payopt = dataGridView1[9, rowIndex].Value.ToString();
            //DataRow[] dr = dtCode.Select($"name = '{phone1}'");
            //DataRow[] dr1 = dtCode.Select($"name = '{type}'");
            //DataRow[] dr2 = dtCode.Select($"name = '{payopt}'");


            txtCode.Text = dataGridView1["code", rowIndex].Value.ToString();
            txtName.Text = dataGridView1["name", rowIndex].Value.ToString();
            //cboPhone1.SelectedValue = dr[0]["code"].ToString();
            cboPhone1.SelectedValue = dataGridView1["phone1", rowIndex].Value;
            txtPhone2.Text = phone2.ToString();
            txtPhone3.Text = phone3.ToString();
            txtIsMem.Text = dataGridView1["ismem", rowIndex].Value.ToString();
            txtMemID.Text = dataGridView1["memid", rowIndex].Value.ToString();
            txtEmail.Text = dataGridView1["email", rowIndex].Value.ToString();
            dtpDate.Value = (DateTime)dataGridView1["date", rowIndex].Value;
            txtEmpName.Text = dataGridView1["empname", rowIndex].Value.ToString();
            cboType.SelectedValue = dataGridView1["type", rowIndex].Value.ToString();
            txtStyle.Text = dataGridView1["style", rowIndex].Value.ToString();
            lblPrice.Text = dataGridView1["price", rowIndex].Value.ToString();
            dtpPayDate.Value = (DateTime)dataGridView1["paydate", rowIndex].Value;
            cboPayopt.SelectedValue = dataGridView1["payopt", rowIndex].Value.ToString();
            txtIsFinished.Text = dataGridView1["isfinished", rowIndex].Value.ToString();



        }
        private void LoadData(DataTable dt)
        {

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }
        private void OnSearch(object sender, TextSearchEventArgs e)
        {
            ucSearch search = (ucSearch)sender;
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);

                dv.Sort = "name";
                int rowIdx = dv.Find(search.textBox);
                if (rowIdx < 0)
                    MessageBox.Show("검색된 회원이 없습니다.");
                else
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowIdx].Cells[0];

                }
            }


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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            frmResEmpMg frm = new frmResEmpMg();
            if (frm.ShowDialog()==DialogResult.Yes)
            {
                string empInfo = $"{frm.ResEmpInfo.Name}({frm.ResEmpInfo.Memo})";
                ResDAC dac = new ResDAC();
                    dac.AddEmp(txtCode.Text, empInfo,frm.ResEmpInfo);

                    txtEmpName.Text = empInfo;
                
            }
        }
    }
}
