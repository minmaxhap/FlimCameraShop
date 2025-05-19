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
    public partial class frmOrderList : Form
    {
        OrderMaster ordM;
        DataTable dt;
        DataTable dtDetail;
        DataTable dtCode;
        public Member CurrentMem { get; set; }
        public frmOrderList()
        {
            InitializeComponent();
        }



        private void frmOrderList_Load(object sender, EventArgs e)
        {
           

            date.Show1Week += On1Week;
            date.Show1Month += On1Month;
            date.Show3Months += On3Months;
            date.Show6Months += On6Months;
            date.ShowControl += OnCotrol;


            string[] category = { "휴대전화", "결제 방식", "배송상태", "현상약품 종류", "스캐너 종류", "스캔 파일 화질", "인화지 종류", "인화지 크기" };
            CodeDAC dac = new CodeDAC();
            dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboPayopt, "결제 방식", dtCode, true);
            CommonUtil.ComboBinding(cboDelivery, "배송상태", dtCode, true);

            listView1.View = View.Details;




            CommonUtil.AddListTextColumn(listView1, "상품 코드", "product");
            CommonUtil.AddListTextColumn(listView1, "상품명", "name");
            CommonUtil.AddListTextColumn(listView1, "수량", "qty");
            CommonUtil.AddListTextColumn(listView1, "가격", "price");


            DataGridViewUtil.SetInitGridView(dataGridView1);



            DataGridViewUtil.AddGridTextColumn(dataGridView1, "주문코드", "code");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "주문자명", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "수량", "totqty");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 금액", "totprice");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 방식", "payopt");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제일", "paydate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "송장번호", "tracking");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "배송상태", "delivery");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대폰 앞자리", "phone1", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일", "email", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "우편번호", "zipcode", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "주소1", "addr1", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "주소2", "addr2", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "상세 옵션", "detail", visibility: false);



            LoadData();
        }

        private void LoadData()
        {
            OrderDAC dac = new OrderDAC();
            dt = dac.GetOrderInfo(CurrentMem.ID);
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();

        }
   

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible = true;
            listView1.Items.Clear();
            listView1.BeginUpdate();
            //code, name, ismem, memid, totqty, totprice, paydate, payoptname, payopt, tracking, deliveryname,
            //delivery, detailcode, mastercode, product, qty, price, phone1, phone, zipcode, addr1, addr2, email
            int rowIndex = dataGridView1.CurrentRow.Index;
            string[] phone = dataGridView1["phone", rowIndex].Value.ToString().Split('-');
            string phone1 = phone[0].ToString();

            int phone2 = Convert.ToInt32(phone[1]);
            int phone3 = Convert.ToInt32(phone[2]);


            string[] str = new string[] { "/" };
            string[] details = dataGridView1["detail", rowIndex].Value?.ToString().Split(str, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();
            if (details != null)
            {
                for (int i = 0; i < details.Length; i++)
                {
                    foreach (DataRow row in dtCode.Select($"code = '{details[i]}'"))
                    {
                        sb.Append(row["name"] + ",");
                    }

                }
            }


            txtCode.Text = dataGridView1["code", rowIndex].Value.ToString();
            txtName.Text = dataGridView1["name", rowIndex].Value.ToString();
            cboPhone1.SelectedValue = dataGridView1["phone1", rowIndex].Value;
            txtPhone2.Text = phone2.ToString();
            txtPhone3.Text = phone3.ToString();
            txtEmail.Text = dataGridView1["email", rowIndex].Value.ToString();
            txtQty.Text = dataGridView1["totqty", rowIndex].Value.ToString();
            txtPrice.Text = dataGridView1["totprice", rowIndex].Value.ToString();
            dtpPayDate.Value = (DateTime)dataGridView1["paydate", rowIndex].Value;
            cboPayopt.SelectedValue = dataGridView1["payopt", rowIndex].Value.ToString();
            txtZip.Text = dataGridView1["zipcode", rowIndex].Value.ToString();
            txtAddr1.Text = dataGridView1["addr1", rowIndex].Value.ToString();
            txtAddr2.Text = dataGridView1["addr2", rowIndex].Value.ToString();
            if (cboDelivery.SelectedValue != null)
                cboDelivery.SelectedValue = dataGridView1["delivery", rowIndex].Value.ToString();
            txtTraking.Text = dataGridView1["tracking", rowIndex].Value.ToString();
            txtDetail.Text = sb.ToString();
            txtdelivery.Text = "CJ대한통운";

            OrderDAC dac = new OrderDAC();
            dtDetail = dac.GetDetailOrder(txtCode.Text);
            dac.Dispose();


            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                DataRow dr = dtDetail.Rows[i];


                ListViewItem listitem = new ListViewItem();
                listitem.Text = dr["name"].ToString();
                listitem.Tag = dr["detailcode"].ToString();
                listitem.SubItems.Add(dr["qty"].ToString());
                listitem.SubItems.Add(dr["price"].ToString());


                listView1.Items.Add(listitem);
            }
            listView1.EndUpdate();

        }


        private void On6Months(object sender, EventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-6);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"paydate>= '{date.DtpLeft}' AND paydate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On3Months(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-3);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"paydate>= '{date.DtpLeft}' AND paydate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Month(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-1);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"paydate>= '{date.DtpLeft}' AND paydate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Week(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddDays(-7);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"paydate>= '{date.DtpLeft}' AND paydate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }
        //메서드 간단하게 만들 수 있겠다!!(숫자랑 변수 받아서)

        private void OnCotrol(object sender, DateTimeEventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"paydate>= '{date.DtpLeft}' AND paydate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }
        private void LoadData(DataTable dt)
        {

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }


    }


}
