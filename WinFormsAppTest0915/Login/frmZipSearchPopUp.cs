using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Xml;

namespace WinFormsAppTest0915

{
    public partial class frmZipSearchPopUp : Form
    {
        public int ZipCode { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public frmZipSearchPopUp()
        {
            InitializeComponent();
        }
        private void frmZipSearch_Load(object sender, EventArgs e)
        {
            DataGridViewUtil.SetInitGridView(dgvAddr);
            DataGridViewUtil.AddGridTextColumn(dgvAddr, "우편번호", "zipNo", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvAddr, "도로명주소", "roadAddr", colWidth: 320);
            DataGridViewUtil.AddGridTextColumn(dgvAddr, "도주1", "roadAddrPart1", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvAddr, "도주2", "roadAddrPart2", visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvAddr, "건물이름", "bdNm", visibility: false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string apiKey = ConfigurationManager.AppSettings["zipAPIKey"];
            string apiURL = $"https://www.juso.go.kr/addrlink/addrLinkApi.do?confmKey={apiKey}&currentPage=1&countPerPage=200&keyword={txtSearch.Text}";

            try
            {
                DataSet ds = new DataSet();

                WebClient wc = new WebClient();
                XmlReader read = new XmlTextReader(wc.OpenRead(apiURL));

                ds.ReadXml(read);

                if (ds.Tables[0].Rows[0]["totalCount"].ToString() != "0")
                {
                    dgvAddr.DataSource = ds.Tables[1];
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSearch.Text.Length > 0 && e.KeyChar == 13)
                btnSearch.PerformClick();
        }

        private void dgvAddr_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtZip.Text = dgvAddr["zipNo", e.RowIndex].Value.ToString();

            txtAddr1.Text = dgvAddr["roadAddrPart1", e.RowIndex].Value.ToString();

            string bdNm = dgvAddr["bdNm", e.RowIndex].Value.ToString();
            string addr2 = dgvAddr["roadAddrPart2", e.RowIndex].Value.ToString();

            if (addr2.Contains(bdNm))
                txtAddr2.Text = addr2;
            else
                txtAddr2.Text = addr2 + bdNm;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtZip.Text))
            {
                ZipCode = int.Parse(txtZip.Text);
                Addr1 = txtAddr1.Text;
                Addr2 = txtAddr2.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
