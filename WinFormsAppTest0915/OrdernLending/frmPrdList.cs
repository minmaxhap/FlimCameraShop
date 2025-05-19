using System;
using System.Collections;
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

    public partial class frmPrdList : Form
    {
        //ucProduct ctrl;
        DataTable dtPrdList;
        DataTable dtCart;

        public frmPrdList()
        {
            InitializeComponent();
        }



        private void frmPrdList_Load(object sender, EventArgs e)
        {

            //등록된 제품목록을 조회
            ProductDAC dac = new ProductDAC();
            dtPrdList = dac.GetPrdInfo();
            dac.Dispose();

            //전달받은 DataTable로부터 제품user control 바인딩
            ShowProductList(dtPrdList);

            //장바구니 datatable 생성
            dtCart = new DataTable();
            dtCart.Columns.Add(new DataColumn("code", typeof(string)));
            dtCart.Columns.Add(new DataColumn("name", typeof(string)));
            dtCart.Columns.Add(new DataColumn("price", typeof(int)));
            dtCart.Columns.Add(new DataColumn("qty", typeof(int)));
            dtCart.Columns.Add(new DataColumn("total", typeof(int)));
            dtCart.Columns.Add(new DataColumn("image", typeof(string)));
            


        }

        private void ShowProductList(DataTable dt)
        {
            int iRows = (int)Math.Ceiling(dt.Rows.Count / 2.0);
            int idx = 0;
            
            for (int r = 0; r < iRows; r++)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (idx >= dt.Rows.Count) break;

                    ucProduct ctrl = new ucProduct();
                    ctrl.Location = new Point(30 + (440 * i), 16 + (130 * r));
                    ctrl.Size = new Size(430, 128);
                    ctrl.ProductInfo = new Product()
                    {
                        Code = dt.Rows[idx]["code"].ToString(),
                        Name = dt.Rows[idx]["name"].ToString(),
                        Detail = dt.Rows[idx]["detail"].ToString(),
                        Price = Convert.ToInt32(dt.Rows[idx]["price"]),
                        Image = dt.Rows[idx]["image"].ToString()
                        
                    };

                    ctrl.AddtoCart += OnAddtoCart; 
                    panel1.Controls.Add(ctrl);
                    idx++;

                }
            }
            
        }

        public void OnAddtoCart(object sender, EventArgs e)
        {
            //장바구니 담기를 클릭하면 장바구니 테이블에 값 추가하기(dtCart)
            ucProduct uc = (ucProduct)sender;

            //장바구니에 담은 적이 있으면 수량과 금액이 증가, 신규로 담으면 행이 추가
            //string str = uc.ProductInfo.Code;

            DataRow[] rows = dtCart.Select($"code='{uc.ProductInfo.Code}'");
            if (rows.Length>0)
            {
                
                rows[0]["qty"] = Convert.ToInt32(rows[0]["qty"]) + 1;
                rows[0]["price"] =  uc.ProductInfo.Price;
                rows[0]["total"] = Convert.ToInt32(rows[0]["qty"]) * uc.ProductInfo.Price;
            }
            else
            {
                DataRow dr = dtCart.NewRow();
                dr["code"] = uc.ProductInfo.Code;
                dr["name"] = uc.ProductInfo.Name;
                dr["price"] = uc.ProductInfo.Price;
                dr["qty"] = 1;
                dr["image"] = uc.ProductInfo.Image;     
                dr["total"] = Convert.ToInt32(dr["price"]) * Convert.ToInt32(dr["qty"]);
                dtCart.Rows.Add(dr);
            }
            dtCart.AcceptChanges(); //DataTable 변경내역 반영(저장)
            if (dtCart!=null)
            {

              ((frmMainCtmr) MdiParent).CurrentCart = dtCart;
                MessageBox.Show("장바구니에 담았습니다.");
                //dataGridView1.DataSource = dtCart; //여기서는 dtCart에 값이 잘 들어간다. 
            }

        }
    }
}
