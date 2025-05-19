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
    public delegate void DateTimeEventHandler(object sender, DateTimeEventArgs e);
    public partial class ucBtnReadDate : UserControl
    {

        public event DateTimeEventHandler Show1Week;
        public event DateTimeEventHandler Show1Month;
        public event DateTimeEventHandler Show3Months;
        public event DateTimeEventHandler Show6Months;
        public event DateTimeEventHandler ShowControl;




        public ucBtnReadDate()
        {
            InitializeComponent();
        }
        public DateTime DtpLeft
        {
            get { return dtpLeft.Value; }
            set { dtpLeft.Value = value; }
        }

        public DateTime DtpRight
        {
            get { return dtpRight.Value; }
            set { dtpRight.Value = value; }
        }


        private void btn6Months_Click(object sender, EventArgs e)
        {
            if(Show6Months!=null)
            {
                DateTimeEventArgs args = new DateTimeEventArgs();
                args.DtpLeft = dtpLeft.Value;
                args.DtpRight = dtpRight.Value;
                Show6Months(this, args);
            }
        }

        private void btn3Months_Click(object sender, EventArgs e)
        {
            if (Show3Months != null)
            {
                DateTimeEventArgs args = new DateTimeEventArgs();
                args.DtpLeft = dtpLeft.Value;
                args.DtpRight = dtpRight.Value;
                Show3Months(this, args);
            }
        }


        private void btn1Week_Click(object sender, EventArgs e)
        {
            if (Show1Week != null)
            {
                DateTimeEventArgs args = new DateTimeEventArgs();
                args.DtpLeft = dtpLeft.Value;
                args.DtpRight = dtpRight.Value;
                Show1Week(this, args);
            }
        }

        private void btn1Month_Click(object sender, EventArgs e)
        {
            if (Show1Month != null)
            {
                DateTimeEventArgs args = new DateTimeEventArgs();
                args.DtpLeft = dtpLeft.Value;
                args.DtpRight = dtpRight.Value;
                Show1Month(this, args);
            }
        }

        private void dtpLeft_ValueChanged(object sender, EventArgs e)
        {
            dtpRight.MinDate = dtpLeft.Value;
        }

        private void dtpRight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ShowControl != null)
            {
                DateTimeEventArgs args = new DateTimeEventArgs();
                args.DtpLeft = dtpLeft.Value;
                args.DtpRight = dtpRight.Value;
                ShowControl(this, args);
            }
        }
    }

    public class DateTimeEventArgs : EventArgs
    {
        public DateTime DtpLeft { get; set; }
        public DateTime DtpRight { get; set; }
    }

}
