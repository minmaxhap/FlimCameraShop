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
    public partial class frmResEmpMg : Form
    {
        DataTable dt;
       

        public frmResEmpMg()
        {
            InitializeComponent();
        }
        public Employee ResEmpInfo 
        {
            get 
            {
                Employee emp = new Employee();
                emp.ID = txtID.Text;
                emp.Name = txtName.Text;
                emp.Memo = txtTask.Text;
                return emp;
            
            }
            
        
        }

        private void frmResEmpMg_Load(object sender, EventArgs e)
        {
            ucSearch.SeachInfo += OnSearch;

            DataGridViewUtil.SetInitGridView(dataGridView1);

      
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "아이디", "id");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이름", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "task", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "taskname");


            LoadData();


           
        }

        private void LoadData()
        {
            EmpDAC dac = new EmpDAC();
            dt = dac.GetEmpInfo();
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }

        private void OnSearch(object sender, TextSearchEventArgs e)
        {
            ucSearch search = (ucSearch)sender;
            DataTable dt = dataGridView1.DataSource as DataTable;

                DataView dv = new DataView(dt);

                dv.Sort = "name";
                int rowIdx = dv.Find(search.textBox);
                if (rowIdx < 0)
                    MessageBox.Show("검색된 직원이 없습니다.");
                else
                {

                        LoadData();
                    //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    //chk.HeaderText = "";
                    //chk.Width = 30;
                    //chk.Name = "chk";
                    //checkColumnIdx = dataGridView1.Columns.Add(chk);
                    //dataGridView1.CurrentCell = dataGridView1.Rows[rowIdx].Cells[0];
                    //dataGridView1.CellClick += dataGridView1_CellClick;
                }



        }




        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;

            txtID.Text = dataGridView1["id", rowIndex].Value.ToString();
            txtName.Text = dataGridView1["name", rowIndex].Value.ToString();
            txtTask.Text = dataGridView1["taskname", rowIndex].Value.ToString();
        }
        
    }
}