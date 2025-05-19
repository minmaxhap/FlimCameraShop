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
    public partial class csDataGridView : DataGridView
    {
        public csDataGridView()
        {
            InitializeComponent();
            this.RowHeadersWidth = 30;
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.ColumnHeadersHeight = 30;
            this.BackgroundColor = Color.AliceBlue;

            this.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            this.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 52, 52);
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            this.DefaultCellStyle.BackColor = Color.White;
            this.DefaultCellStyle.ForeColor = Color.FromArgb(52, 52, 52);
            this.DefaultCellStyle.SelectionBackColor = Color.Tomato;
            this.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            this.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.ControlDark;
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
