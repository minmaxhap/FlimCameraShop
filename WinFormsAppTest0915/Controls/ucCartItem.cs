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
    public delegate void CartItemHandler(object sender, CartItemEventArgs e);
    public delegate void CartItemQtyHandler(object sender, CartItemQtyEventArgs e);
    public partial class ucCartItem : UserControl
    {
        Product curProduct;


        public event CartItemHandler DelCartItem; //삭제버튼을 클릭할때 발생하는 이벤트
        public event CartItemQtyHandler UpdateQty; //수량이 변경될때 발생하는 이벤트
        public event CartItemQtyHandler ChangeChecked;

        public int ItemQty
        {
            get { return (int)numQty.Value; }
            set { numQty.Value = value; }
        }

        public int Total
        {
            
                get { return Convert.ToInt32(lblTotal.Text.Replace(",", "").Replace("원", "")); }
                set { lblTotal.Text = value.ToString("#,##0") + " 원"; }
            
        }

        public Product ProductInfo
        {
            get
            {
                return curProduct;
            }
            set
            {
                curProduct = value;
                pictureBox1.ImageLocation = value.Image;
                lblName.Text = value.Name;
                if(value.Qty==0)
                {
                    value.Qty = 1;
                }
                numQty.Value = value.Qty;
                lblPrice.Text = value.Price.ToString("#,##0") + " 원";
                lblTotal.Text = (numQty.Value * value.Price).ToString("#,##0") + " 원";

            }
        }
        public ucCartItem()
        {
            InitializeComponent();
        }

        public bool IsChecked
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DelCartItem != null)
            {
                CartItemEventArgs args = new CartItemEventArgs();
                args.Code = curProduct.Code;
                DelCartItem(this, args); 
            }
        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {
            if (UpdateQty != null)
            {
                CartItemQtyEventArgs args = new CartItemQtyEventArgs();
                args.Code = curProduct.Code;
                args.Qty = (int)numQty.Value;
                args.Total = args.Qty * curProduct.Price;

                UpdateQty(this, args);
            }

            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChangeChecked != null)
            {
                CartItemQtyEventArgs args = new CartItemQtyEventArgs();
                args.Code = curProduct.Code;
                args.Qty = (int)numQty.Value;
                args.Total = args.Qty * curProduct.Price;

                ChangeChecked(this, args);
            }
        }
    }

    public class CartItemQtyEventArgs
    {
        public string Code { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }

    public class CartItemEventArgs
    {
        public string Code { get; set; }
       
    }
}
