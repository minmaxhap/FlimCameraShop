using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public partial class frmCodeMg : Form
    {
        DataTable dt;
        CodeDAC dac;

        public frmCodeMg()
        {
            InitializeComponent();
        }

        private void frmCodeMg_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;

            main.ToolStripEnable(tool:false);
            //오류 해결 => 코드 위치만 바꾸었을 뿐인데.
            LoadData();


            //dac = new CodeDAC();
            //dt = dac.GetAllCommonCode();

            //bindingSource1.DataSource = dt;

            //bindingNavigator1.BindingSource = bindingSource1;
            //dataGridView1.DataSource = bindingSource1;

    

            // dataGridView1.ReadOnly = false;
        }
        private void LoadData()
        {
            dac = new CodeDAC();
            dt = dac.GetAllCommonCode();
            dataGridView1.DataSource = dt;
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            textBox1.DataBindings.Add("Text", dt, "code");
            textBox2.DataBindings.Add("Text", dt, "name");
            textBox3.DataBindings.Add("Text", dt, "category");
        }

       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DataRow[] upRows = dt.Select(null,null, DataViewRowState.ModifiedCurrent);
            foreach (DataRow dr in upRows)
            {
                dac.UpdateCode(dr["Code"].ToString(), dr["Name"].ToString(), dr["Category"].ToString());
            }

            
            DataRow[] insRows = dt.Select(null, null, DataViewRowState.Added);
            foreach (DataRow dr in insRows)
            {
                dac.InsertCode(dr["Code"].ToString(), dr["Name"].ToString(), dr["Category"].ToString());
            }
            
            DataRow[] delRows = dt.Select(null, null, DataViewRowState.Deleted);
            foreach (DataRow dr in delRows)
            {
                dac.DeleteCode(dr["Code", DataRowVersion.Original].ToString());
            }

            dt.AcceptChanges();
            MessageBox.Show("성공적으로 저장되었습니다.");
            LoadData();
       
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
            //int rowIndex = dataGridView1.CurrentCell.RowIndex;
            //DataRow dr = dt.Rows[rowIndex];
            //if (rowIndex > 0)
            //{ dt.Rows.Remove(dr); }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
