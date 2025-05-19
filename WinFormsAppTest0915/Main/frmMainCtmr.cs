using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public partial class frmMainCtmr : Form
    {
        public DataTable CurrentCart { get; set; }
        public Member CurrentMem{ get; set; }
        public NonMember CurrentNonMem { get; set; }


        public Member MemberInfo(Member mem) 
        {
            //property에서 바로 get, set하면 될 것 같은데 usercontrol 처럼! usercontrol 먼저 해보고 변경하자.
            this.CurrentMem = mem;
            return CurrentMem;
        }
        public NonMember NonMemberInfo(NonMember nonmem)
        {
            this.CurrentNonMem = nonmem;
            return CurrentNonMem;
        }
        public frmMainCtmr()
        {
            InitializeComponent();
        }
        

        private void FrmCtmr_Load(object sender, EventArgs e)
        {
           
            tsAsk.Tag = "frmAdv";
            tsCamera.Tag = "frmLending";
            tsCart.Tag = "frmCart";
            tsInfo.Tag = "frmMemMyInfo";
            tsLendingList.Tag = "frmLendingList";
            tsOrderList.Tag = "frmOrderList";
            tsProduct.Tag = "frmPrdList";
            tsReply.Tag = "frmAdvList";
            tsReserve.Tag = "frmRes";
            tsResList.Tag = "frmResList";

            foreach (Control control in this.Controls)
            {
           
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {

                    client.BackColor = Color.AliceBlue;

                    break;
                }
            }


            

            toolStrip1.BackColor = Color.AliceBlue;

            if (CurrentMem != null)
                label1.Text = $"{CurrentMem.Name}님";
            else if(CurrentNonMem !=null)
                label1.Text = $"{CurrentNonMem.Name}님";


        }

  




        private void tsProduct_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmPrdList frm = new frmPrdList();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;


            frm.Show();




        }

        private void tsCamera_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmLending frm = new frmLending();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            if (CurrentNonMem != null)
                frm.CurrentNonMem = this.CurrentNonMem;


            frm.Show();
        }

        private void tsReserve_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmRes frm = new frmRes();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            if (CurrentNonMem != null)
                frm.CurrentNonMem = this.CurrentNonMem;


            frm.Show();
        }

        private void tsCart_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmCart frm = new frmCart();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            frm.CurrentCart = this.CurrentCart;
            if(CurrentMem!=null)
                frm.CurrentMem = this.CurrentMem;
            if (CurrentNonMem != null)
                frm.CurrentNonMem = this.CurrentNonMem;
 

            frm.Show();
        }

        private void tsInfo_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            if (CurrentMem != null)
            {
                MemDAC dac = new MemDAC();
                Member loginMem = dac.Login(CurrentMem.ID);
                dac.Dispose();
                CurrentMem = loginMem; //dac 연결해서 로그인한 정보 불러오기



                frmMemMyInfo frm = new frmMemMyInfo();
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.MdiParent = this;
                frm.TopLevel = false;


                frm.MemInfo(CurrentMem);
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

        private void tsAsk_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmAdv frm = new frmAdv();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            else
            { MessageBox.Show("비회원인 고객은 다른 플랫폼으로 문의하거나 회원가입을 해주세요."); }


            frm.Show();
        }

        private void tsReply_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmAdvList frm = new frmAdvList();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            else
            { MessageBox.Show("비회원인 고객은 다른 플랫폼으로 문의하거나 회원가입을 해주세요."); }


            frm.Show();
        }

        private void tsOrderList_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmOrderList frm = new frmOrderList();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            else
            { MessageBox.Show("비회원인 고객은 다른 플랫폼으로 문의하거나 회원가입을 해주세요.");
                return;
            }


            frm.Show();
        }

        private void tsResList_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmResList frm = new frmResList();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            else
            { MessageBox.Show("비회원인 고객은 다른 플랫폼으로 문의하거나 회원가입을 해주세요.");
                return;
            }


            frm.Show();
        }

        private void tsLendingList_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            frmLendingList frm = new frmLendingList();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.TopLevel = false;
            if (CurrentMem != null)
                frm.CurrentMem = this.CurrentMem;
            else
            { MessageBox.Show("비회원인 고객은 다른 플랫폼으로 문의하거나 회원가입을 해주세요.");
                return;
            }


            frm.Show();
        }

        private void frmMainCtmr_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료합니다.", "종료확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
