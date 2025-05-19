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
    public partial class ucBtnCrud : UserControl
    {
        public ucBtnCrud()
        {
            InitializeComponent();
        }

        public delegate void CreateHandler(object sender, EventArgs e);
        public delegate void ReadHandler(object sender, EventArgs e);
        public delegate void UpdateHandler(object sender, EventArgs e);
        public delegate void DeleteHandler(object sender, EventArgs e);


        public event CreateHandler ClickCreate;
        public event ReadHandler ClickRead;
        public event UpdateHandler ClickUpdate;
        public event DeleteHandler ClickDelete;


    }
}
