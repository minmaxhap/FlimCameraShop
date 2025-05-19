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
    public partial class frmLending : Form
    {
        //ListView 정리
        //ColumnHeader == dgv의 headertext
        //item == dgv의 row
        //sub item == 한 row마다 추가되는 데이터
    
        DataTable dt;
        Image image;
        NonMember nonmem;
        Member mem;
        ListView.SelectedListViewItemCollection items;
        public Member CurrentMem { get; set; }
        public NonMember CurrentNonMem { get; set; }

        public frmLending()
        {
            InitializeComponent();
        }

        private void frmLending_Load(object sender, EventArgs e)
        {

            if (CurrentMem != null)
            {
                mem = CurrentMem;
                txtName.Text = mem.Name;
                cboPhone1.SelectedValue = mem.Phone1;
                txtPhone2.Text = mem.Phone2;
                txtPhone3.Text = mem.Phone3;
                txtEmail.Text = mem.Email;
            }
            if (CurrentNonMem != null)
            {
                nonmem = CurrentNonMem;
                txtName.Text = nonmem.Name;
                cboPhone1.SelectedValue = nonmem.Phone1;
                txtPhone2.Text = nonmem.Phone2;
                txtPhone3.Text = nonmem.Phone3;
            }

            listView1.View = View.Details;
            CommonUtil.AddListTextColumn(listView1, "이름", "name",120,HorizontalAlignment.Center);
            CommonUtil.AddListTextColumn(listView1, "설명", "detail",150);
            CommonUtil.AddListTextColumn(listView1, "금액", "price",  80, HorizontalAlignment.Right);

            comboBox1.Visible = true;
            comboBox1.Text = "정렬";
            comboBox1.Items.Add(View.LargeIcon);
            comboBox1.Items.Add(View.SmallIcon);
            comboBox1.Items.Add(View.Details);
            comboBox1.Items.Add(View.List);
            comboBox1.Items.Add(View.Tile);
            LoadData();

            txtCamName.BackColor = txtCamDetail.BackColor = Color.AliceBlue;
            txtCamName.Enabled = txtCamDetail.Enabled = false;

            listView1.LargeImageList = imageList1;
            imageList1.ImageSize = new Size(64, 64);

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            dateTimePicker1.MinDate = DateTime.Now;

            string[] category = { "휴대전화", "결제 방식" };
            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboPayopt, "결제 방식", dtCode);


        }

        public void LoadData()
        {
            LendingDAC dac = new LendingDAC();
            dt = dac.GetCamInfo();
            dt.Dispose();
            DataBinding(dt, listView1);
            

        }

        public void DataBinding(DataTable dt, ListView lv)
        {

            lv.BeginUpdate();
            
          
           
        
            for(int i = 0; i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[i];


                string filePath = dt.Rows[i]["image"].ToString();
                image = Image.FromFile(filePath);
                imageList1.Images.Add(image);
                imageList1.Images.SetKeyName(i, filePath);


                ListViewItem listitem = new ListViewItem();
                listitem.Text = dr["name"].ToString();
                listitem.ImageIndex = 0;
                listitem.Tag = dr["code"].ToString();
                //listitem.SubItems.Add(dr["name"].ToString());
                listitem.SubItems.Add(dr["detail"].ToString());
                int price = Convert.ToInt32(dr["price"]);
                listitem.SubItems.Add(price.ToString("#,##0")+"원");
                listitem.SubItems.Add(dr["image"].ToString());
                
                listView1.Items.Add(listitem);
         

                //int idx = imageList1.Images.Keys.Count - 1;
               

                

            }

            //lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lv.EndUpdate();


        }


       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.View = (View)comboBox1.SelectedItem;
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            items = listView1.SelectedItems;
            ListViewItem lvItem = items[0];
            txtCamName.Text   = lvItem.SubItems[0].Text;
            txtCamDetail.Text = lvItem.SubItems[1].Text;
            lblTotal.Text = lvItem.SubItems[2].Text;
            pictureBox1.Image = Image.FromFile(lvItem.SubItems[3].Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            //이메일 유효성 검사
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("필수 입력입니다.");
                return;
            }
            if (!CommonUtil.EmailCheck(txtEmail.Text))
            {
                MessageBox.Show("유효한 이메일을 입력해주세요.");
                return;
            }


            Random rnd = new Random();
            int code = rnd.Next(1, 1000);
            string lendingcode = string.Concat('L', DateTime.Now.ToString("yyMMddHHmm"), code.ToString().PadLeft(3, '0'));

            if (mem != null) //회원 주문 내역
            {

                Camera camera = new Camera()
                {
                    Code = dt.Rows[listView1.SelectedItems.IndexOf(items[0])]["code"].ToString(),
                    Price = Convert.ToInt32(dt.Rows[listView1.SelectedItems.IndexOf(items[0])]["price"])
                    
                };
                Lending lending = new Lending()
                {

                    Code = lendingcode,
                    lendingdate = dateTimePicker1.Value,
                    Payopt = cboPayopt.SelectedValue.ToString(),
                    Camera = camera.Code,
                    //lendingcode, ismem, memid, logincode, lendingdate, isreturn,
                    //camera, payopt, paydate,  payopt, phone1, phone, email, name
                };

                LendingDAC dac = new LendingDAC();
                dac.MemOrder(mem, lending, camera);
                dac.Dispose();


                MessageBox.Show("주문이 성공적으로 완료되었습니다.");


            }

            if (nonmem != null) //비회원 주문내역
            {
                nonmem.Email = txtEmail.Text;
                Camera camera = new Camera()
                {
                    Code = dt.Rows[listView1.SelectedItems.IndexOf(items[0])]["code"].ToString(),
                    Price = Convert.ToInt32(dt.Rows[listView1.SelectedItems.IndexOf(items[0])]["price"])

                };
                Lending lending = new Lending()
                {

                    Code = lendingcode,
                    lendingdate = dateTimePicker1.Value,
                    Payopt = cboPayopt.SelectedValue.ToString(),
                    Camera = camera.Code
                };

               
                LendingDAC dac = new LendingDAC();
                dac.NonMemOrder(nonmem, lending, camera);
                dac.Dispose();


                MessageBox.Show("주문이 성공적으로 완료되었습니다.");
            }
        }

        //private void ImageBinding()
        //{
        //    DataRow dr = dt.Rows[i];




        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    { image = Image.FromFile(dr[i]["image"].ToString()); }
        //}


        //if (lv.Columns.Count != dt.Columns.Count)
        //{
        //    

        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        lv.Columns.Add(column.ColumnName);

        //    }
        //}

        //lv.Items.Clear();
        //foreach (DataRow row in dt.Rows)
        //{
        //    ListViewItem item = new ListViewItem(row["columnTag"].ToString());
        //    for (int i = 1; i < ch.i++)
        //    {
        //        item.SubItems.Add(row[i].ToString());
        //    }
        //    lv.Items.Add(item);
        //}


        //AddListItem(dt, lv, "name");
        //AddListItem(dt, lv, "detail");
        //AddListItem(dt, lv, "price");

        //public void AddListItem(DataTable dt,
        //    ListView lv,
        //    string cameracode

        //    )
        //{
        //        DataRow row = dt.Rows.Find("cameracode");

        //        ListViewItem item = new ListViewItem(row["cameracode"].ToString());
        //        for (int i = 1; i < 3; i++)
        //        {
        //            item.SubItems.Add(row[i].ToString());
        //        }
        //        lv.Items.Add(item);

        //}

        //public static void AddGridTextColumn(DataGridView dgv,
        //    string headerText,
        //    string propertyName,
        //     DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
        //    int colWidth = 100,
        //    bool visibility = true)
        //{
        //    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
        //    col.Name = propertyName;
        //    col.HeaderText = headerText;
        //    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    col.DataPropertyName = propertyName;
        //    col.DefaultCellStyle.Alignment = align;
        //    col.Width = colWidth;
        //    col.Visible = visibility;
        //    col.ReadOnly = true;
        //    dgv.Columns.Add(col);
        //}


    }
}
