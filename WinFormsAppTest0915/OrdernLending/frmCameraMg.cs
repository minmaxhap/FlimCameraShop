using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public partial class frmCameraMg : Form
    {
        Camera cam;
        public frmCameraMg()
        {
            InitializeComponent();
        }

        private void frmCameraMg_Load(object sender, EventArgs e)
        {
            frmMainEmp main = (frmMainEmp)this.MdiParent;


            main.Read += OnRead;
            main.Create += OnCreate;
            main.Delete += OnDelete;
            main.Update += OnUpdate;


            DataGridViewUtil.SetInitGridView(dataGridView1);

            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 코드", "code");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 이름", "name");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 설명", "detail");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "카메라 수량", "qty");
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 금액", "price");         
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "이미지 파일", "image", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "대여 상태", "lendingstate",visibility:false);



            LoadData();


        }
        private void LoadData()
        {
            LendingDAC dac = new LendingDAC();
            DataTable dt = dac.GetCamInfo();
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


            //변경을 한 경우는 먼저 이미지파일을 파일저장하셔야 한다.
            string destFileName = "";
            if (txtImageFile.Text.Length > 1)
            {
                //지정된 폴더로 파일을 복사(
                string sPath = ConfigurationManager.AppSettings["uploadPath"] + "Camera/";
                //string sPath = Application.StartupPath + "Image/";
                string localFile = txtImageFile.Text;
                string sExt = localFile.Substring(localFile.LastIndexOf("."));
                string newFileName = txtCode.Text + sExt;
                destFileName = sPath + newFileName;

                if (!Directory.Exists(sPath)) //디렉토리가 없다면 디렉토리를 생성
                {
                    Directory.CreateDirectory(sPath);
                }
                File.Copy(localFile, destFileName, true); //파일을 복사
            }

            cam = new Camera();
            cam.Code = txtCode.Text;
            cam.Name = txtName.Text;
            cam.Detail = txtDetail.Text;
            cam.Qty = int.Parse(txtQty.Text); // 발주넣기 
            cam.Price = int.Parse(txtPrice.Text);

            if (txtImageFile.Text.Length > 1) //이미지변경을 한 경우
                cam.Image = destFileName;
            else //이미지변경을 하지 않은 경우
            {
                if (string.IsNullOrWhiteSpace(pictureBox1.ImageLocation))
                    cam.Image = string.Empty;
                else
                    cam.Image = pictureBox1.ImageLocation;
            }

            LendingDAC dac = new LendingDAC();
            bool bResult = dac.Update(cam);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("상품 정보가 수정되었습니다.");
            }



        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            ProductDAC dac = new ProductDAC();
            bool bResult = dac.Delete(txtCode.Text);
            if (bResult)
            {
                LoadData();
                MessageBox.Show("해당 상품이 삭제되었습니다.");
            }
        }

        private void OnCreate(object sender, EventArgs e)
        {


            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;

            cam = new Camera();
            //카메라 대여 코드 L+날짜 난수
            //비회원 로그인 코드 N+날짜 난수


            Random rnd = new Random();
            rnd.Next(1, 100);

            cam.Code = string.Concat('C', DateTime.Now.ToString("yyMMddHHmm"), rnd.Next(1, 100).ToString().PadLeft(3, '0')); //14자리
            cam.Name = txtName.Text;
            cam.Detail = txtDetail.Text;
            cam.Qty = int.Parse(txtQty.Text);
            cam.Price = int.Parse(txtPrice.Text);
            cam.Image = " ";
            LendingDAC dac = new LendingDAC();
            dac.Insert(cam);
            string destFileName = "";
            if (!String.IsNullOrWhiteSpace(cam.Code))
            {
                //지정된 폴더로 파일을 복사(
                string sPath = ConfigurationManager.AppSettings["uploadPath"] + "Camera/";
                //string sPath = Application.StartupPath + "Image/";
                string localFile = txtImageFile.Text;
                string sExt = localFile.Substring(localFile.LastIndexOf("."));
                string newFileName = cam.Code + sExt;
                destFileName = sPath + newFileName;

                if (!Directory.Exists(sPath)) //디렉토리가 없다면 디렉토리를 생성
                {
                    Directory.CreateDirectory(sPath);
                }
                File.Copy(localFile, destFileName, true); //파일을 복사
            }

            bool bResult = dac.InsertImage(cam.Code, destFileName);
            dac.Dispose();

            if (bResult)
            {
                LoadData();
                MessageBox.Show("상품 정보가 등록되었습니다.");
            }

        }

        private void OnRead(object sender, EventArgs e)
        {
            if (((frmMainEmp)this.MdiParent).ActiveMdiChild != this) return;
            LoadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtQty.Text))
                txtQty.Text = (int.Parse(txtQty.Text) + 10).ToString();

        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images File(*.gif;*.jpg;*.jpeg;*.png;*.bmp)|*.gif;*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtImageFile.Text = dlg.FileName; //전체경로
                pictureBox1.Image = Image.FromFile(dlg.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int rowIndex = dataGridView1.CurrentRow.Index;
            txtCode.Text = dataGridView1[0, rowIndex].Value.ToString();
            txtName.Text = dataGridView1[1, rowIndex].Value.ToString();
            txtDetail.Text = dataGridView1[2, rowIndex].Value.ToString();
            txtQty.Text = dataGridView1[3, rowIndex].Value.ToString();
            txtPrice.Text = dataGridView1[4, rowIndex].Value.ToString();
            string imgFile = dataGridView1[5, rowIndex].Value.ToString();
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
        }
    }
}
