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
    public partial class frmAdvList : Form
    {
        DataTable dt;
        ListView.SelectedListViewItemCollection items;
        public Member CurrentMem { get; set; }

        public frmAdvList()
        {
            InitializeComponent();
        }

        private void frmAdvList_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            CommonUtil.AddListTextColumn(listView1, "문의 내용", "contents");
            CommonUtil.AddListTextColumn(listView1, "문의 날짜", "date");
            CommonUtil.AddListTextColumn(listView1, "답변여부", "isreply" );
            CommonUtil.AddListTextColumn(listView1, "답변 날짜", "repdate");
            CommonUtil.AddListTextColumn(listView1, "답변 직원", "repname");
            CommonUtil.AddListTextColumn(listView1, "답변 내용", "repcontents");

            LoadData();
        }

        public void LoadData()
        {
            AdvDAC dac = new AdvDAC();
            dt = dac.GetAdvInfo(CurrentMem.ID);
            dac.Dispose();
            DataBinding(dt, listView1);


        }

        public void DataBinding(DataTable dt, ListView lv)
        {

            lv.BeginUpdate();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];


                ListViewItem listitem = new ListViewItem();
                listitem.Text = dr["contents"].ToString();
                listitem.Tag = dr["code"].ToString();
                listitem.SubItems.Add(dr["date"].ToString());
                listitem.SubItems.Add(dr["isreply"].ToString());
                listitem.SubItems.Add(dr["repdate"].ToString());
                listitem.SubItems.Add(dr["repname"].ToString());
                listitem.SubItems.Add(dr["repcontents"].ToString());

                listView1.Items.Add(listitem);
            }

            lv.EndUpdate();


        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            items = listView1.SelectedItems;
            ListViewItem lvItem = items[0];
            txtContents.Text = lvItem.Text;
            dtpDate.Value = Convert.ToDateTime(lvItem.SubItems[1].Text);
            if (lvItem.SubItems[2].Text == "Y")
                txtIsReply.Text = "답변 완료";
            else
                txtIsReply.Text = "답변 중";
            dtpReplyDate.Value = Convert.ToDateTime(lvItem.SubItems[3].Text);
            txtRepName.Text = lvItem.SubItems[4].Text;
            txtRepContents.Text = lvItem.SubItems[5].Text;

        }
    }
}
