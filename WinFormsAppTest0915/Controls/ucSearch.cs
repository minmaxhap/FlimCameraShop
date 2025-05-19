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
    public delegate void TextHandler(object sender, TextSearchEventArgs e);
    public partial class ucSearch : UserControl
    {
        public string textBox
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public event TextHandler SeachInfo;
        public ucSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (SeachInfo != null)
            {
                TextSearchEventArgs args = new TextSearchEventArgs();
                args.Text = txtSearch.Text;

                SeachInfo(this, args);
            }
        }
    }

    public class TextSearchEventArgs
    {
        public string Text { get; set; }

    }
}
