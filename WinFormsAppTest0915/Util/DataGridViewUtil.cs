using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public class DataGridViewUtil
    {
        public static void SetInitGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AutoGenerateColumns = false;
        }

        public static void AddGridTextColumn(DataGridView dgv,
            string headerText,
            string propertyName,
             DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
            int colWidth = 100,
            bool visibility = true)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();  
            col.HeaderText = headerText;
            col.Name = propertyName;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.DataPropertyName = propertyName;
            col.DefaultCellStyle.Alignment = align;
            col.Width = colWidth;
            col.Visible = visibility;
            col.ReadOnly = true;
            dgv.Columns.Add(col);
        }

    }
}
