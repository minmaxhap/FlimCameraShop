using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace WinFormsAppTest0915
{
    public partial class frmEmpMg : Form
    {
        DataTable dt;
        int rowIndex;
   

        public frmEmpMg()
        {
            InitializeComponent();
        
        }

       
  

        private void frmEmpMg_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;
            
           
            

            date.Show1Week += On1Week;
            date.Show1Month += On1Month;
            date.Show3Months += On3Months;
            date.Show6Months += On6Months;

            main.Read += OnRead;
            main.Create += OnCreate;
            main.Delete += OnDelete;
            main.Update += OnUpdate;
            main.Excel += OnExcel;


            ucSearch.SeachInfo += OnSearch;
            DataGridViewUtil.SetInitGridView(dataGridView1);
            if (main.CurrentEmp.ID == "admin01")
            {    
                main.ToolStripEnable(c: false, d: false,e:true);
            


                DataGridViewUtil.AddGridTextColumn(dataGridView1, "아이디", "id");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "이름", "name");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone1", "phone1", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone2", "phone2", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone3", "phone3", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "생일", "birth", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일주소", "email");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "우편번호", "zipcode");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "도로명주소", "addr1");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "상세주소", "addr2");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "task", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "taskname");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "월급", "salary");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "인센티브", "incen");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "sns계정", "sns");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "입사일", "hiredate");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "퇴사여부", "isquit");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "퇴사일", "quitdate");
            }

            else
            { 
                main.ToolStripEnable(c: false, d: false, u: false, e: true);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "아이디", "id");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "이름", "name");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone1", "phone1", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone2", "phone2", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "phone3", "phone3", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "생일", "birth", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일주소", "email");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "우편번호", "zipcode", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "도로명주소", "addr1", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "상세주소", "addr2", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "task", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "담당 업무", "taskname");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "월급", "salary", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "인센티브", "incen", visibility: false);
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "sns계정", "sns");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "입사일", "hiredate");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "퇴사여부", "isquit");
                DataGridViewUtil.AddGridTextColumn(dataGridView1, "퇴사일", "quitdate");
            }

            LoadData();


        }

        private void OnExcel(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel Files(*.xls)|*.xls";
            dlg.Title = "파일로 내보내기";
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

               
                DataTable dtE = (DataTable)dataGridView1.DataSource;

                for (int c = 0; c < dtE.Columns.Count; c++)
                {
                    xlWorkSheet.Cells[1, c + 1] = dtE.Columns[c].ColumnName;

                }

                for (int r = 0; r < dtE.Rows.Count; r++)
                {
                    for (int c = 0; c < dtE.Columns.Count; c++)
                    {
                        xlWorkSheet.Cells[r + 2, c + 1] = dtE.Rows[r][c].ToString();
                    }
                }

                xlWorkBook.SaveAs(dlg.FileName, Excel.XlFileFormat.xlWorkbookNormal);
                xlWorkBook.Close();
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("엑셀다운로드 완료");
            }

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception er)
            {
                obj = null;
                MessageBox.Show("엑셀로 내보내기 중에 문제가 생겼습니다. 다시 시도해주세요." + er.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        private void LoadData()
        {
            EmpDAC dac = new EmpDAC();
            dt = dac.GetEmpInfo();
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }
        private void LoadData(DataTable dt)
        {
           
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }  

        private void OnRead(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            LoadData();
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
           
            Employee emp = new Employee();

            emp.ID = dataGridView1["id", rowIndex].Value?.ToString();
            emp.Name = dataGridView1["name", rowIndex].Value?.ToString();
            emp.Zip = dataGridView1["zipcode", rowIndex].Value?.ToString();
            emp.Addr1 = dataGridView1["addr1", rowIndex].Value?.ToString();
            emp.Addr2 = dataGridView1["addr2", rowIndex].Value?.ToString();
            emp.Email = dataGridView1["email", rowIndex].Value?.ToString();
            emp.HireDate = Convert.ToDateTime(dataGridView1["hiredate", rowIndex].Value.ToString());
            emp.Phone1 = dataGridView1["phone1", rowIndex].Value?.ToString();
            emp.Phone2 = dataGridView1["phone2", rowIndex].Value?.ToString();
            emp.Phone3 = dataGridView1["phone3", rowIndex].Value?.ToString();
            emp.SNS = dataGridView1["sns", rowIndex].Value?.ToString();
            emp.Salary = Convert.ToInt32(dataGridView1["salary", rowIndex].Value);
            emp.Incen = Convert.ToInt32(dataGridView1["incen", rowIndex].Value);
            emp.Task = dataGridView1["task", rowIndex].Value?.ToString();
            emp.Birth = Convert.ToDateTime(dataGridView1["birth", rowIndex].Value.ToString());
            emp.IsQuit = dataGridView1["isquit", rowIndex].Value?.ToString();
            if (!string.IsNullOrEmpty(dataGridView1["quitdate", rowIndex].Value.ToString()))
                emp.QuitDate= Convert.ToDateTime(dataGridView1["quitdate", rowIndex].Value.ToString());



            frmEmpCRUD frm = new frmEmpCRUD();
            frm.SelectedEmp = emp;
            if(frm.ShowDialog()==DialogResult.OK)
            {
                EmpDAC dac = new EmpDAC();
                bool bResult = dac.UpdateByAdmin(frm.SelectedEmp);
                dac.Dispose();

                if (bResult)
                {
                    LoadData(dt);
                    MessageBox.Show("직원 정보가 수정되었습니다.");
                }
                else
                {
                    MessageBox.Show("다시 시도해주세요.");
                }
                
            }

        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            
        }

        private void OnCreate(object sender, EventArgs e)
        {



        }

        private void OnSearch(object sender, TextSearchEventArgs e)
        {
            ucSearch search = (ucSearch)sender;
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);

                dv.Sort = "name";
                int rowIdx = dv.Find(search.textBox);
                if (rowIdx < 0)
                    MessageBox.Show("검색된 직원이 없습니다.");
                else
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowIdx].Cells[0];

                }
            }


        }

        private void OnCotrol(object sender, DateTimeEventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"hiredate>= '{date.DtpLeft}' AND hiredate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }



        private void On6Months(object sender, EventArgs e)
        {
            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-6);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"hiredate>= '{date.DtpLeft}' AND hiredate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On3Months(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-3);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"hiredate>= '{date.DtpLeft}' AND hiredate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Month(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddMonths(-1);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"hiredate>= '{date.DtpLeft}' AND hiredate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void On1Week(object sender, EventArgs e)
        {

            ucBtnReadDate date = (ucBtnReadDate)sender;

            date.DtpRight = DateTime.Now;
            date.DtpLeft = date.DtpRight.AddDays(-7);

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"hiredate>= '{date.DtpLeft}' AND hiredate <= '{date.DtpRight}' ";
            LoadData(dv.ToTable());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = dataGridView1.CurrentRow.Index;

            

        }
    }
}
