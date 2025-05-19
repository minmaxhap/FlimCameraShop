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
    
    public partial class frmCart : Form
    {
        DataTable dtCart;
        //Customer person;
        int totPrice;
        int totQty;
        Member mem;
        NonMember nonmem;
        StringBuilder sb;

        public DataTable CurrentCart { get; set; }
        public Member CurrentMem { get; set; }
        public NonMember CurrentNonMem { get; set; }
        public OrderMaster Master { get; set; }
        public OrderDetail Detail { get; set; }

        public frmCart()
        {
            InitializeComponent();


        }

    

        private void frmCart_Load(object sender, EventArgs e)
        {
           
            if (CurrentCart != null)
            {
                dtCart = CurrentCart;

                ShowCartItem();
            }
            string[] category = { "휴대전화","결제 방식", "현상약품 종류", "스캐너 종류", "스캔 파일 화질", "인화지 종류", "인화지 크기" };
            CodeDAC dac = new CodeDAC();
            DataTable dtCode = dac.GetCommonCodes(category);
            dac.Dispose();

            CommonUtil.ComboBinding(cboPhone1, "휴대전화", dtCode, false);
            CommonUtil.ComboBinding(cboPayopt, "결제 방식", dtCode);
            CommonUtil.ComboBinding(cboDevelop, "현상약품 종류", dtCode,true, "현상약품 종류");
            CommonUtil.ComboBinding(cboScanner, "스캐너 종류", dtCode,true, "스캐너 종류");
            CommonUtil.ComboBinding(cboScanFile, "스캔 파일 화질", dtCode,true,"스캔 파일 화질");
            CommonUtil.ComboBinding(cboPrintType, "인화지 종류", dtCode, true, "인화지 종류");
            CommonUtil.ComboBinding(cboPrintSize, "인화지 크기", dtCode, true, "인화지 크기");
            if (CurrentMem!=null)
            {
                mem = CurrentMem;
                txtName.Text = mem.Name;
                txtZip.Text =mem.Zip;
                txtAddr1.Text = mem.Addr1;
                txtAddr2.Text = mem.Addr2;
                txtEmail.Text = mem.Email;
                cboPhone1.SelectedValue = mem.Phone1;
                txtPhone2.Text = mem.Phone2;
                txtPhone3.Text = mem.Phone3;

            }
            if (CurrentNonMem != null)
            {
                nonmem = CurrentNonMem;
                txtName.Text = nonmem.Name;
                cboPhone1.SelectedValue = nonmem.Phone1;
                txtPhone2.Text = nonmem.Phone2;
                txtPhone3.Text = nonmem.Phone3;
            }

            groupBox1.Visible = groupBox2.Visible = false;

        }

        public void ShowCartItem(bool ck = true)
        {
            panel1.Controls.Clear();

            
            int idx = 0;
            totPrice = 0;
            totQty = 0;
            if (dtCart != null)
            {
                foreach (DataRow dr in dtCart.Rows)
                {
                    ucCartItem item = new ucCartItem();
                    item.Location = new Point(9, 5 + (idx * 68));
                    item.Size = new Size(466, 73);
                    item.IsChecked = ck;
                    item.ProductInfo = new Product()
                    {
                        Code = dr["code"].ToString(),
                        Name = dr["name"].ToString(),
                        Price = Convert.ToInt32(dr["price"]),
                        Image = dr["image"].ToString()

                    };
                   
                     item.ItemQty = Convert.ToInt32(dr["qty"]);
                    item.Total = Convert.ToInt32(dr["total"]);


                    item.DelCartItem += Item_DelCartItem;
                    item.UpdateQty += Item_UpdateQty;
                    item.ChangeChecked += Item_ChangeChecked;
                    if (item.IsChecked)
                    {
                        totPrice += Convert.ToInt32(dr["total"]);//총 가격
                        totQty += Convert.ToInt32(dr["qty"]);//총 수량 (카트 안 모두)
                    }
                    panel1.Controls.Add(item);

                    idx++;

                    

                }


                lblTotal.Text = $"{totPrice.ToString("#,##0")} 원";
                lblQty.Text = $"총 {totQty} 개";
            }
        }

        private void Item_ChangeChecked(object sender, CartItemQtyEventArgs e)
        {
            ucCartItem uc = (ucCartItem)sender;
            if (uc.IsChecked)
            {
                uc.IsChecked = true;
               dtCart.AcceptChanges();
                //ShowCartItem();
                totPrice += uc.Total;
                totQty += uc.ItemQty;

            }
            else
           {
                uc.IsChecked = false;
                dtCart.AcceptChanges();
                //ShowCartItem(ck:false);
                totPrice -= uc.Total;
                totQty -= uc.ItemQty;
            }
            lblTotal.Text = $"{totPrice.ToString("#,##0")} 원";
            lblQty.Text = $"총 {totQty} 개";

        }

        private void Item_UpdateQty(object sender, CartItemQtyEventArgs e)
        {
            ucCartItem uc = (ucCartItem)sender;
 
            DataRow[] rows = dtCart.Select($"code='{uc.ProductInfo.Code}'");
            if (rows.Length > 0)
            {                

                    rows[0]["qty"] = e.Qty;
                    rows[0]["total"] = Convert.ToInt32(rows[0]["price"]) * Convert.ToInt32(rows[0]["qty"]);

                    dtCart.AcceptChanges();
                    ShowCartItem();
            }

        }

        private void Item_DelCartItem(object sender, CartItemEventArgs e)
        {
            ucCartItem uc = (ucCartItem)sender;
            DataRow[] rows = dtCart.Select($"code='{uc.ProductInfo.Code}'");
            if (rows.Length > 0)
            {
                dtCart.Rows.Remove(rows[0]);
                dtCart.AcceptChanges();

                ShowCartItem();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ucCartItem item in panel1.Controls)
            {
                item.IsChecked = ckAll.Checked;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
             foreach (ucCartItem item in panel1.Controls)
                {
                    if (item.IsChecked)
                    {
                        DataRow[] rows = dtCart.Select($"code='{item.ProductInfo.Code}'");
                        if (rows.Length > 0)
                        {
                            dtCart.Rows.Remove(rows[0]);
                        }
                    }
                }
                dtCart.AcceptChanges();
                ShowCartItem();

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

            //주소 유효성 검사
            if (string.IsNullOrWhiteSpace(txtAddr2.Text))
            {
                MessageBox.Show("상세주소를 입력해주세요.");
                return;
            }


            Random rnd = new Random();
            int code = rnd.Next(1, 1000);
            string mastercode = string.Concat('M', DateTime.Now.ToString("yyMMddHHmm"), code.ToString().PadLeft(3, '0'));
            string detailcode = string.Concat('D', mastercode.TrimStart('M'));
            if (mem !=null) //회원 주문 내역
            {

                OrderMaster master = new OrderMaster()
                {

                    Code = mastercode,
                    Payopt = cboPayopt.SelectedValue.ToString(),
                    TotQty = totQty,
                    TotPrice = totPrice,
                    Detail = sb.ToString(),
                    MemID = CurrentMem.ID
                    
                };
                DataRow[] rows;
                //OrderDetail detail;
                OrderDetail[] products = new OrderDetail[dtCart.Rows.Count];

                bool bResult = false;
                int i = 0;
                if( i < dtCart.Rows.Count )
                {
                    foreach (ucCartItem item in panel1.Controls)
                    {


                        bResult = item.IsChecked;
                        if (item.IsChecked)
                        {
                            rows = dtCart.Select($"code='{item.ProductInfo.Code}'");





                            OrderDetail detail = new OrderDetail()
                            {
                                DetailCode = string.Concat(detailcode, i),
                                MasterCode = mastercode,
                                Product = rows[0]["code"].ToString(),
                                Price = item.ProductInfo.Price,
                                Qty = Convert.ToInt32(rows[0]["qty"])

                            };

                            string a = rows[0]["code"].ToString();

                            products[i] = detail;
                            i++;

                        }

                    }
                }


                if (!bResult)
                {
                    MessageBox.Show("구매할 상품을 선택해주세요.");
                    return;
                }

                OrderDAC dac = new OrderDAC();
                dac.MemOrder(master, products);
                dac.Dispose();
                MessageBox.Show("주문이 성공적으로 완료되었습니다.");
                //오류 나도 메시지 박스 뜸. 오류 처리하자.
              

            }

            if(nonmem!=null) //비회원 주문내역
            {
                nonmem.Email = txtEmail.Text;
                nonmem.Zip = txtZip.Text;
                nonmem.Addr1 = txtAddr1.Text;
                nonmem.Addr2 = txtAddr2.Text;



                OrderMaster master = new OrderMaster()
                {

                    Code = mastercode,
                    Payopt = cboPayopt.SelectedValue.ToString(),
                    TotQty = totQty,
                    TotPrice = totPrice,
                    Detail = sb.ToString()
                };
                DataRow[] rows;
                //OrderDetail detail;
                OrderDetail[] products = new OrderDetail[dtCart.Rows.Count];
                bool bResult = false;
                foreach (ucCartItem item in panel1.Controls)
                {
                    bResult = item.IsChecked;
                    if (item.IsChecked)
                    {
                        rows = dtCart.Select($"code='{item.ProductInfo.Code}'");


                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            OrderDetail detail = new OrderDetail()
                            {
                                DetailCode = string.Concat(detailcode, i),
                                MasterCode = mastercode,
                                Product = rows[0]["code"].ToString(),
                                Price = item.ProductInfo.Price,
                                Qty = item.ItemQty

                            };
                            products[i] = detail;
                        }


                    }

                }
                if (!bResult)
                {
                    MessageBox.Show("구매할 상품을 선택해주세요.");
                    return;
                }

                OrderDAC dac = new OrderDAC();
                dac.NonMemOrder(nonmem, master, products);
                dac.Dispose();
                MessageBox.Show("주문이 성공적으로 완료되었습니다.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmZipSearchPopUp frm = new frmZipSearchPopUp();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtZip.Text = frm.ZipCode.ToString();
                txtAddr1.Text = frm.Addr1;
                txtAddr2.Text = frm.Addr2;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            
                panel1.Enabled = ckAll.Enabled = btnDelete.Enabled = false;
                 groupBox2.Visible = true;
                foreach (ucCartItem item in panel1.Controls)
                {
                    if (item.ProductInfo.Name.Contains("현상")) cboDevelop.Visible = true;
                    if (item.ProductInfo.Name.Contains("스캔")) cboScanFile.Visible = cboScanner.Visible = true;
                    if (item.ProductInfo.Name.Contains("인화")) cboPrintSize.Visible = cboPrintType.Visible = true; 
                }


            
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 상품을 주문하시겠습니까?", "주문 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sb = new StringBuilder();
                sb.Append(cboDevelop.SelectedValue + "/");
                sb.Append(cboScanFile.SelectedValue + "/");
                sb.Append(cboScanner.SelectedValue + "/");
                sb.Append(cboPrintSize.SelectedValue + "/");
                sb.Append(cboPrintType.SelectedValue + "/");
                groupBox1.Visible = true;
                groupBox2.Enabled = false;
            }


        }
    }
}
