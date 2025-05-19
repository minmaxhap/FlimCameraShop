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
    public partial class frmMemMg : Form
    {
        DataTable dt;
        public frmMemMg()
        {
            InitializeComponent();
        }

        private void frmMemMg_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;
            main.ToolStripEnable(c: false, d: false,u:false);



            date.Show1Week += On1Week;
            date.Show1Month += On1Month;
            date.Show3Months += On3Months;
            date.Show6Months += On6Months;
            date.ShowControl += OnCotrol;

            ucSearch.SeachInfo += OnSearch;


            DataGridViewUtil.SetInitGridView(dataGridView1);


            DataGridViewUtil.AddGridTextColumn(dataGridView1, "아이디", "id");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이름", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "생일", "birth");

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일주소", "email");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "우편번호", "zipcode");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "도로명주소", "addr1");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "상세주소", "addr2");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "추천인", "recom");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "가입일", "signupdate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "탈퇴여부", "isquit");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "탈퇴일", "quitdate");

            //recom, points, signupdate, delac, delacdate
            MemDAC dac = new MemDAC();
            dt = dac.GetMemInfo();
            dac.Dispose();

            LoadData(dt);


            //ucBtnReadDate date = new ucBtnReadDate();
            //date.Location = new Point(54, 6);
            //date.Size = new Size(516, 26);

            

       
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
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void LoadData(DataTable dt)
        {

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }

        private void On6Months(object sender, EventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;
            
            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-6);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On3Months(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;
          
            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-3);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Month(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;
           
            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-1);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Week(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;
           
            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddDays(-7);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //WHITE등급은 1000포인트
            //   YELLOW 등급은 5000포인트
            //BLUE 등급은 50000포인트 
            DataView dv = new DataView(dt);
            dv.RowFilter = $"points>=50000";
            lblBlue.Text = dv.Count.ToString();
            dv = new DataView(dt);
            dv.RowFilter = $"points>=5000 and points <50000";
            lblYellow.Text = dv.Count.ToString();
            dv = new DataView(dt);
            dv.RowFilter = $"points >=1000 and points<5000";
            lblWhite.Text = dv.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DataView dv = dt.DefaultView;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddDays(-7);
            dv.RowFilter = $"signupdate>= '{date.DtpLeft}' AND signupdate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }
    }
}
