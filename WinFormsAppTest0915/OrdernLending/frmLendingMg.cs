using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace WinFormsAppTest0915
{
    public partial class frmLendingMg : Form
    {

        Camera cam;
        DataTable dt;
        public frmLendingMg()
        {
            InitializeComponent();
        }

        private void frmLendingMg_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;
            main.ToolStripEnable(c: false);
            

            main.Read += OnRead;
            //main.Create += OnCreate;
            main.Delete += OnDelete;
            main.Update += OnUpdate;


            DataGridViewUtil.SetInitGridView(dataGridView1);

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 코드", "cameracode");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 이름", "cameraname");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 설명", "detail");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 수량", "qty");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 금액", "price");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이미지 파일", "image", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 상태", "lendingstate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 코드", "lendingcode");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "회원 여부", "ismem");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "회원ID", "memid");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "비회원 코드", "logincode");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 날짜", "lendingdate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "반납 상태", "isreturn");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 코드", "camera", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 방식 코드", "payopt", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 방식", "payoptname");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제 날짜", "paydate");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "고객명", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대폰 앞자리", "phone1", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "휴대전화", "phone");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이메일 주소", "email");


            LoadData();


        }
        private void LoadData()
        {
            LendingDAC dac = new LendingDAC();
            dt = dac.GetLendingInfo();
            dac.Dispose();
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            //txtCode.Clear();
            //txtDetail.Clear();
            //txtImageFile.Clear();
            //txtName.Clear();
            //txtPrice.Clear();
            //txtQty.Clear();

        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            cam = new Camera();
            cam.Code = txtCode.Text;
            cam.Name = txtCamName.Text;
            cam.Detail = txtDetail.Text;
            cam.Qty = int.Parse(txtQty.Text); 
            cam.Price = int.Parse(txtPrice.Text);


            LendingDAC dac = new LendingDAC();
            bool bResult = dac.Update(cam);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("대여 정보가 수정되었습니다.");
            }



        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            LendingDAC dac = new LendingDAC();
            bool bResult = dac.Delete(txtCode.Text);
            if (bResult)
            {
                LoadData();
                MessageBox.Show("대여 내역이 삭제되었습니다.");
            }
        }


        private void OnRead(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            LoadData();

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int rowIndex = dataGridView1.CurrentRow.Index;
            txtCode.Text = dataGridView1["cameracode", rowIndex].Value.ToString();
            txtCamName.Text = dataGridView1["cameraname", rowIndex].Value.ToString();
            txtDetail.Text = dataGridView1["detail", rowIndex].Value.ToString();
            txtQty.Text = dataGridView1["qty", rowIndex].Value.ToString();
            txtPrice.Text = dataGridView1["price", rowIndex].Value.ToString();
            string imgFile = dataGridView1["image", rowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(imgFile))
            {
                pictureBox1.ImageLocation = imgFile;
            }
            else
            {
                pictureBox1.ImageLocation = string.Empty;
                pictureBox1.Image = null;
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            txtLending.Text = dataGridView1["lendingstate", rowIndex].Value.ToString();

            string[] phone = dataGridView1["phone", rowIndex].Value.ToString().Split('-');
            string phone1 = phone[0].ToString();

            int phone2 = Convert.ToInt32(phone[1]);
            int phone3 = Convert.ToInt32(phone[2]);

            txtName.Text = dataGridView1["name", rowIndex].Value.ToString();
            //cboPhone1.SelectedValue = dr[0]["code"].ToString();
            cboPhone1.SelectedValue = dataGridView1["phone1", rowIndex].Value;
            txtPhone2.Text = phone2.ToString();
            txtPhone3.Text = phone3.ToString();
            txtLoginCode.Text = dataGridView1["logincode", rowIndex].Value.ToString();
            txtMemID.Text = dataGridView1["memid", rowIndex].Value.ToString();
            txtEmail.Text = dataGridView1["email", rowIndex].Value.ToString();
            dtpLendingDate.Value = (DateTime)dataGridView1["lendingdate", rowIndex].Value;
            lblPrice.Text = dataGridView1["price", rowIndex].Value.ToString();
            dtpPayDate.Value = (DateTime)dataGridView1["paydate", rowIndex].Value;
            cboPayopt.SelectedValue = dataGridView1["payopt", rowIndex].Value.ToString();
            txtIsReturn.Text = dataGridView1["isreturn", rowIndex].Value.ToString();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            LendingDAC dac = new LendingDAC();
            dac.StartLending(dataGridView1["cameracode", rowIndex].Value.ToString());
            MessageBox.Show("대여를 시작합니다.");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            LendingDAC dac = new LendingDAC();
            dac.ReturnCamera(dataGridView1["cameracode", rowIndex].Value.ToString());

            MessageBox.Show("반납을 완료했습니다.");
        }

        //create없다
        //private void OnCreate(object sender, EventArgs e)
        //{


        //    if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

        //    cam = new Camera();
        //    //카메라 대여 코드 L+날짜 난수
        //    //비회원 로그인 코드 N+날짜 난수


        //    Random rnd = new Random();
        //    rnd.Next(1, 100);

        //    cam.Code = string.Concat('C', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //14자리
        //    cam.Name = txtName.Text;
        //    cam.Detail = txtDetail.Text;
        //    cam.Qty = int.Parse(txtQty.Text);
        //    cam.Price = int.Parse(txtPrice.Text);
        //    cam.Image = " ";
        //    LendingDAC dac = new LendingDAC();
        //    dac.Insert(cam);
        //    string destFileName = "";
        //    if (!String.IsNullOrWhiteSpace(cam.Code))
        //    {
        //        //지정된 폴더로 파일을 복사(
        //        string sPath = ConfigurationManager.AppSettings["uploadPath"] + "Camera/";
        //        //string sPath = Application.StartupPath + "Image/";
        //        string localFile = txtImageFile.Text;
        //        string sExt = localFile.Substring(localFile.LastIndexOf("."));
        //        string newFileName = cam.Code + sExt;
        //        destFileName = sPath + newFileName;

        //        if (!Directory.Exists(sPath)) //디렉토리가 없다면 디렉토리를 생성
        //        {
        //            Directory.CreateDirectory(sPath);
        //        }
        //        File.Copy(localFile, destFileName, true); //파일을 복사
        //    }

        //    bool bResult = dac.InsertImage(cam.Code, destFileName);
        //    dac.Dispose();

        //    if (bResult)
        //    {
        //        LoadData();
        //        MessageBox.Show("상품 정보가 등록되었습니다.");
        //    }

        //}
    }
}
