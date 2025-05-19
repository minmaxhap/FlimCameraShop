using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormsAppTest0915
{
    public partial class frmMainEmp : Form
    {
        public event EventHandler Read;
        public event EventHandler Create;
        public event EventHandler Update;
        public event EventHandler Delete;
        public event EventHandler Excel;


        public Employee CurrentEmp { get; set; }
        public frmMainEmp()
        {
            InitializeComponent();
        }
        public Employee EmpInfo(Employee emp)
        {
            this.CurrentEmp = emp;
            return CurrentEmp;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tsEmpMg.Tag = "frmEmpMg";
            tsAdvMg.Tag = "frmAdvMg";
            tsCameraMg.Tag = "frmCameraMg";
            tsInfo.Tag = "frmEmpMyInfo";
            tsMemMg.Tag = "frmMemMg";
            tsNonMemOrderMg.Tag = "frmNonMemOrderMg";
            tsMemOrderMg.Tag = "frmMemOrderMg";
            tsLendingMg.Tag = "frmLendingMg";
            tsPrdMg.Tag = "frmPrdMg";
            tsResMg.Tag = "frmResMg";
            tsCodeMg.Tag = "frmCodeMg";


            tabControl1.Visible = false;
            foreach (Control control in this.Controls)
            {

                MdiClient client = control as MdiClient;
                if (!(client == null))
                {

                    client.BackColor = Color.AliceBlue;

                    return;
                }
            }

            if (CurrentEmp != null)
            { label1.Text = $"{CurrentEmp.Name}님"; }

       


        }

        public void ToolStripEnable(bool tool = true, bool c= true, bool r= true,
            bool u = true,bool d = true,bool e=false)
        {
            this.toolStrip1.Visible = tool;
            this.tooLCreate.Enabled = c;
            this.toolRead.Enabled = r;
            this.toolUpdate.Enabled = u;
            this.toolDelete.Enabled = d;
            this.toolExcel.Enabled = e;
        }

        private void OpenCreateForm(string prgName)
        {
            //if (ActiveMdiChild != null)
            //    ActiveMdiChild.Hide();
            string appName = Assembly.GetEntryAssembly().GetName().Name;

            Type frmType = Type.GetType($"{appName}.{prgName}");
            //PropertyInfo property =  frmType.GetProperty("CurrentEmp");
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == frmType)
                {
                    form.Activate();
                    form.BringToFront();
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;
                    form.ControlBox = false;
                    
                    
                    return;
                }

            }

    

            Form frm = (Form)Activator.CreateInstance(frmType);

            frm.MdiParent = this;
            frm.Show();
        }
       
        private void tsInfo_Click(object sender, EventArgs e)
        {
            //if (ActiveMdiChild != null)
            //    ActiveMdiChild.Close();

            if (CurrentEmp != null)
            {
                EmpDAC dac = new EmpDAC();
                Employee loginAdmin = dac.Login(CurrentEmp.ID);
                dac.Dispose();
                CurrentEmp = loginAdmin; //dac 연결해서 로그인한 정보 불러오기

                frmEmpMyInfo frm = new frmEmpMyInfo();

                frm.FormBorderStyle = FormBorderStyle.None;
                frm.MdiParent = this;



                frm.EmpInfo(CurrentEmp); //child form에 정보 전달
                frm.Show();
            }

        }

        private void tsAdvMg_Click(object sender, EventArgs e)
        {
            if (CurrentEmp != null)
            {
                frmAdvMg frm = new frmAdvMg();

                frm.FormBorderStyle = FormBorderStyle.None;
                frm.MdiParent = this;
                frm.CurrentEmp = CurrentEmp;
                frm.Show();
            }

        }

        private void tsLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그아웃하시겠습니까?", "로그아웃 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Hide();
            }
        }

        private void toolRead_Click(object sender, EventArgs e)
        {
            //4가지 메서드를 하나로 줄일 수 있을 것 같은데. 앞에 Tool이랑 _Click 잘라서
           if(Read!=null)
            {
                Read(this, null);
            }
        }

        private void toolCreate_Click(object sender, EventArgs e)
        {
            if (Create!= null)
            {
                Create(this, null);
            }
        }

        private void toolUpdate_Click(object sender, EventArgs e)
        {
            if (Update != null)
            {
                Update(this, null);
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (Delete != null)
            {
                Delete(this, null);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            OpenCreateForm(menu.Tag.ToString());

        }

        

        private void frmMainEmp_MdiChildActivate(object sender, EventArgs e)
        {
           
            if (this.ActiveMdiChild == null)
            {
                tabControl1.Visible = false;
            }
            else 
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
           
                if (this.ActiveMdiChild.Tag == null)
                {
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text+"    ");
                    tp.Parent = tabControl1;
                    tp.Tag = this.ActiveMdiChild;
                    tabControl1.SelectedTab = tp;

                    this.ActiveMdiChild.FormClosed += ActiveMdiChild_FormClosed;

                    this.ActiveMdiChild.Tag = tp;
                }

                if(!tabControl1.Visible)
                tabControl1.Visible = true;
            }
         


        }

        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frm =  (Form)sender;
            ((TabPage)frm.Tag).Dispose();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                Form frm = (Form)tabControl1.SelectedTab.Tag;
                frm.Select();
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var r = tabControl1.GetTabRect(i);
                var closeImage = Properties.Resources.close;
                var closeRect = new Rectangle((r.Right - closeImage.Width), r.Top + (r.Height - closeImage.Height) / 2, closeImage.Width, closeImage.Height);
                if (closeRect.Contains(e.Location))
                { this.ActiveMdiChild.Close();
                    break;
                }
            }
            
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text == ""
                  || e.Item.Text == "닫기(&C)"
                  || e.Item.Text == "최소화(&N)"
                  || e.Item.Text == "이전 크기로(&R)")
                e.Item.Visible = false;
        }
         
        private void toolExcel_Click(object sender, EventArgs e)
        {
            if (Excel != null)
            {
                Excel(this, null);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void frmMainEmp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("프로그램을 종료합니다.","종료확인",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {

                this.Dispose(true);
                Application.Exit();

            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
