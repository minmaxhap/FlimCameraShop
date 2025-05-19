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
    public partial class frmLendingList : Form
    {
        DataTable dt;
        ListView.SelectedListViewItemCollection items;
        public Member CurrentMem { get; set; }
        public frmLendingList()
        {
            InitializeComponent();
        }

        private void frmLendingList_Load(object sender, EventArgs e)
        {
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.CustomFormat = "yyyy년 MM월 dd일 HH시 mm분";
            dtpStartDate.Visible = false;

            dtpReturnDate.Format = DateTimePickerFormat.Custom;
            dtpReturnDate.CustomFormat = "tt hh시 mm분";
            dtpReturnDate.Visible = false;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            listView1.View = View.Details;


            CommonUtil.AddListTextColumn(listView1, "대여 날짜", "lendingdate");
            //CommonUtil.AddListTextColumn(listView1, "카메라 코드", "cameracode");
            CommonUtil.AddListTextColumn(listView1, "카메라 이름", "cameraname");
             CommonUtil.AddListTextColumn(listView1, "카메라 설명", "detail");
             //CommonUtil.AddListTextColumn(listView1, "카메라 수량", "qty");
             CommonUtil.AddListTextColumn(listView1, "대여 금액", "price");
             CommonUtil.AddListTextColumn(listView1, "이미지 파일", "image");
            //CommonUtil.AddListTextColumn(listView1, "대여 상태", "lendingstate");
            //CommonUtil.AddListTextColumn(listView1, "대여 코드", "lendingcode");
            //CommonUtil.AddListTextColumn(listView1, "회원ID", "memid");

            //CommonUtil.AddListTextColumn(listView1, "카메라 코드", "camera");
            //CommonUtil.AddListTextColumn(listView1, "결제 방식 코드", "payopt");
            CommonUtil.AddListTextColumn(listView1, "결제일", "paydate");
            CommonUtil.AddListTextColumn(listView1,"결제 방식", "payoptname");
           
            CommonUtil.AddListTextColumn(listView1, "반납 여부", "isreturn");
            CommonUtil.AddListTextColumn(listView1, "대여 시작일", "startdate");
            CommonUtil.AddListTextColumn(listView1, "반납완료일", "returndate");



            LoadData();
        }

        private void LoadData()
        {
            LendingDAC dac = new LendingDAC();
            dt = dac.GetLendingInfo(CurrentMem.ID);
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
                listitem.Text = dr["lendingdate"].ToString();
                listitem.Tag = dr["lendingcode"].ToString();
                //listitem.SubItems.Add(dr["cameracode"].ToString());
                //listitem.SubItems.Add(dr["lendingdate"].ToString());
                listitem.SubItems.Add(dr["cameraname"].ToString());
                listitem.SubItems.Add(dr["detail"].ToString());
                 listitem.SubItems.Add(dr["price"].ToString());
                listitem.SubItems.Add(dr["image"].ToString());
                listitem.SubItems.Add(dr["paydate"].ToString());
                listitem.SubItems.Add(dr["payoptname"].ToString());
                listitem.SubItems.Add(dr["isreturn"].ToString());
                listitem.SubItems.Add(dr["startdate"].ToString());
                listitem.SubItems.Add(dr["returndate"].ToString());


                listView1.Items.Add(listitem);
            }

            // lv.EndUpdate();


        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            items = listView1.SelectedItems;
            ListViewItem lvItem = items[0];
            dtpLendingDate.Value = Convert.ToDateTime(lvItem.SubItems[0].Text);
            txtCamName.Text = lvItem.SubItems[1].Text;
            txtDetail.Text = lvItem.SubItems[2].Text;
            lblPrice.Text = lvItem.SubItems[3].Text;
            pictureBox1.Image = Image.FromFile(lvItem.SubItems[4].Text);
            dtpPayDate.Value = Convert.ToDateTime(lvItem.SubItems[5].Text);
            txtPayOpt.Text = lvItem.SubItems[6].Text;
            txtIsReturn.Text = lvItem.SubItems[7].Text;
            if (!string.IsNullOrWhiteSpace(lvItem.SubItems[8].Text))
            {
                dtpStartDate.Value = Convert.ToDateTime(lvItem.SubItems[8].Text);
                dtpStartDate.Visible = true;
            }

           
            if (!string.IsNullOrWhiteSpace(lvItem.SubItems[9].Text))
            { dtpReturnDate.Value= Convert.ToDateTime(lvItem.SubItems[9].Text);
            dtpReturnDate.Visible = true;
                }
        }
    }
}
