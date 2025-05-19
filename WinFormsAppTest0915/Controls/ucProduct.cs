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
    public partial class ucProduct : UserControl
    {
        Product curProduct;
        public event EventHandler AddtoCart;


        public Product ProductInfo
        {
            get
            {
                return curProduct;
            }
            set
            {
                
                curProduct = value;
                curProduct.Code = value.Code;
                pictureBox1.ImageLocation = value.Image;
                lblName.Text = value.Name;
                lblDetail.Text = value.Detail;
                lblPrice.Text = value.Price.ToString("#,##0") + " 원";
                lblName1.Text = value.Name;

            }
        }
        public ucProduct()
        {
            InitializeComponent();
        }

        


        


        private void btnCart_Click(object sender, EventArgs e)
        {
            if(AddtoCart!=null)
            {
                AddtoCart(this, null);
            }
        }

       
    }
}
