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
    public partial class frmResList : Form
    {
        DataTable dt;
        ListView.SelectedListViewItemCollection items;
        public Member CurrentMem { get; set; }
        public frmResList()
        {
            InitializeComponent();
        }

        private void frmResList_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;



           // CommonUtil.AddListTextColumn(listView1, "예약자명", "name");
           //CommonUtil.AddListTextColumn(listView1, "휴대전화", "phone");
           //CommonUtil.AddListTextColumn(listView1, "이메일 주소", "email");
           CommonUtil.AddListTextColumn(listView1, "촬영 날짜", "date");
           CommonUtil.AddListTextColumn(listView1, "촬영 유형", "typename");
           CommonUtil.AddListTextColumn(listView1, "촬영 스타일", "style");
           CommonUtil.AddListTextColumn(listView1, "금액", "price");
            CommonUtil.AddListTextColumn(listView1, "결제 날짜", "paydate");
            CommonUtil.AddListTextColumn(listView1, "결제 방식", "payoptname");
           CommonUtil.AddListTextColumn(listView1, "담당 직원", "empname");
           CommonUtil.AddListTextColumn(listView1, "촬영 종료 여부", "isfinished" );
         



            LoadData();
        }

        private void LoadData()
        {
            ResDAC dac = new ResDAC();
            dt = dac.GetResInfo(CurrentMem.ID);
            dac.Dispose();
            DataBinding(dt, listView1);
            //txtCode.Clear();
            //txtDetail.Clear();
            //txtImageFile.Clear();
            //txtName.Clear();
            //txtPrice.Clear();
            //txtQty.Clear();

        }

        public void DataBinding(DataTable dt, ListView lv)
        {

           // lv.BeginUpdate();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];


                ListViewItem listitem = new ListViewItem();
                listitem.Text = dr["date"].ToString();
                listitem.Tag = dr["code"].ToString();
                listitem.SubItems.Add(dr["typename"].ToString());
                listitem.SubItems.Add(dr["style"].ToString());
                listitem.SubItems.Add(dr["price"].ToString());
                listitem.SubItems.Add(dr["paydate"].ToString());
                listitem.SubItems.Add(dr["payoptname"].ToString());
                listitem.SubItems.Add(dr["empname"].ToString());
                listitem.SubItems.Add(dr["isfinished"].ToString());


                listView1.Items.Add(listitem);
            }

           // lv.EndUpdate();


        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            items = listView1.SelectedItems;
            ListViewItem lvItem = items[0];
            dtpDate.Value = Convert.ToDateTime(lvItem.Text);
            txtType.Text = lvItem.SubItems[1].Text;
            txtStyle.Text= lvItem.SubItems[2].Text;
            lblPrice.Text = lvItem.SubItems[3].Text;
            dtpPayDate.Value = Convert.ToDateTime(lvItem.SubItems[4].Text);
            txtPayOpt.Text= lvItem.SubItems[5].Text;
            txtEmpName.Text = lvItem.SubItems[6].Text;
            txtIsFinshed.Text = lvItem.SubItems[7].Text;
            txtName.Text = CurrentMem.Name;
            txtPhone2.Text = CurrentMem.Phone2;
            txtPhone3.Text = CurrentMem.Phone3;
            txtEmail.Text = CurrentMem.Email;
            cboPhone1.SelectedValue = CurrentMem.Phone1;

        }
    }
}
