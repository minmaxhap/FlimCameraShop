
namespace WinFormsAppTest0915
{
    partial class frmMainEmp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainEmp));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsEmpMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMemMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAdvMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCameraMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPrdMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLendingMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsResMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMemOrderMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNonMemOrderMg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCodeMg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.toolUpdate = new System.Windows.Forms.ToolStripButton();
            this.tooLCreate = new System.Windows.Forms.ToolStripButton();
            this.toolRead = new System.Windows.Forms.ToolStripButton();
            this.toolExcel = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new WinFormsAppTest0915.csTabControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEmpMg,
            this.tsMemMg,
            this.tsAdvMg,
            this.tsLogout,
            this.tsInfo,
            this.tsCameraMg,
            this.tsPrdMg,
            this.tsLendingMg,
            this.tsResMg,
            this.tsOrder,
            this.tsCodeMg});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1575, 35);
            this.menuStrip1.TabIndex = 32;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // tsEmpMg
            // 
            this.tsEmpMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsEmpMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsEmpMg.Name = "tsEmpMg";
            this.tsEmpMg.Size = new System.Drawing.Size(71, 29);
            this.tsEmpMg.Text = "직원 정보";
            this.tsEmpMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsMemMg
            // 
            this.tsMemMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsMemMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsMemMg.Name = "tsMemMg";
            this.tsMemMg.Size = new System.Drawing.Size(67, 29);
            this.tsMemMg.Text = "회원관리";
            this.tsMemMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsAdvMg
            // 
            this.tsAdvMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsAdvMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsAdvMg.Name = "tsAdvMg";
            this.tsAdvMg.Size = new System.Drawing.Size(71, 29);
            this.tsAdvMg.Text = "문의 내역";
            this.tsAdvMg.Click += new System.EventHandler(this.tsAdvMg_Click);
            // 
            // tsLogout
            // 
            this.tsLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsLogout.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsLogout.Margin = new System.Windows.Forms.Padding(1);
            this.tsLogout.Name = "tsLogout";
            this.tsLogout.Size = new System.Drawing.Size(71, 29);
            this.tsLogout.Text = " 로그아웃";
            this.tsLogout.Click += new System.EventHandler(this.tsLogout_Click);
            // 
            // tsInfo
            // 
            this.tsInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsInfo.Margin = new System.Windows.Forms.Padding(1);
            this.tsInfo.Name = "tsInfo";
            this.tsInfo.Size = new System.Drawing.Size(59, 29);
            this.tsInfo.Text = "내 정보";
            this.tsInfo.Click += new System.EventHandler(this.tsInfo_Click);
            // 
            // tsCameraMg
            // 
            this.tsCameraMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsCameraMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsCameraMg.Name = "tsCameraMg";
            this.tsCameraMg.Size = new System.Drawing.Size(83, 29);
            this.tsCameraMg.Text = "카메라 관리";
            this.tsCameraMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsPrdMg
            // 
            this.tsPrdMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsPrdMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsPrdMg.Name = "tsPrdMg";
            this.tsPrdMg.Size = new System.Drawing.Size(71, 29);
            this.tsPrdMg.Text = "상품 관리";
            this.tsPrdMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsLendingMg
            // 
            this.tsLendingMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsLendingMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsLendingMg.Name = "tsLendingMg";
            this.tsLendingMg.Size = new System.Drawing.Size(111, 29);
            this.tsLendingMg.Text = "카메라 대여 내역";
            this.tsLendingMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsResMg
            // 
            this.tsResMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsResMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsResMg.Name = "tsResMg";
            this.tsResMg.Size = new System.Drawing.Size(99, 29);
            this.tsResMg.Text = "촬영 예약 내역";
            this.tsResMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsOrder
            // 
            this.tsOrder.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMemOrderMg,
            this.tsNonMemOrderMg});
            this.tsOrder.Margin = new System.Windows.Forms.Padding(1);
            this.tsOrder.Name = "tsOrder";
            this.tsOrder.Size = new System.Drawing.Size(99, 29);
            this.tsOrder.Text = "상품 주문 내역";
            // 
            // tsMemOrderMg
            // 
            this.tsMemOrderMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsMemOrderMg.Name = "tsMemOrderMg";
            this.tsMemOrderMg.Size = new System.Drawing.Size(166, 22);
            this.tsMemOrderMg.Text = "회원 주문 내역";
            this.tsMemOrderMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsNonMemOrderMg
            // 
            this.tsNonMemOrderMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsNonMemOrderMg.Name = "tsNonMemOrderMg";
            this.tsNonMemOrderMg.Size = new System.Drawing.Size(166, 22);
            this.tsNonMemOrderMg.Text = "비회원 주문 내역";
            this.tsNonMemOrderMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // tsCodeMg
            // 
            this.tsCodeMg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsCodeMg.Margin = new System.Windows.Forms.Padding(1);
            this.tsCodeMg.Name = "tsCodeMg";
            this.tsCodeMg.Size = new System.Drawing.Size(71, 29);
            this.tsCodeMg.Text = "코드 관리";
            this.tsCodeMg.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelete,
            this.toolUpdate,
            this.tooLCreate,
            this.toolRead,
            this.toolExcel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(1323, 55);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(243, 58);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolDelete
            // 
            this.toolDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolDelete.AutoSize = false;
            this.toolDelete.Image = global::WinFormsAppTest0915.Properties.Resources.delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(40, 55);
            this.toolDelete.Text = "삭제";
            this.toolDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // toolUpdate
            // 
            this.toolUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolUpdate.AutoSize = false;
            this.toolUpdate.Image = global::WinFormsAppTest0915.Properties.Resources.update;
            this.toolUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUpdate.Name = "toolUpdate";
            this.toolUpdate.Size = new System.Drawing.Size(40, 55);
            this.toolUpdate.Text = "수정";
            this.toolUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolUpdate.Click += new System.EventHandler(this.toolUpdate_Click);
            // 
            // tooLCreate
            // 
            this.tooLCreate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tooLCreate.AutoSize = false;
            this.tooLCreate.Image = global::WinFormsAppTest0915.Properties.Resources.create;
            this.tooLCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooLCreate.Name = "tooLCreate";
            this.tooLCreate.Size = new System.Drawing.Size(40, 55);
            this.tooLCreate.Text = "추가";
            this.tooLCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tooLCreate.Click += new System.EventHandler(this.toolCreate_Click);
            // 
            // toolRead
            // 
            this.toolRead.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolRead.AutoSize = false;
            this.toolRead.BackColor = System.Drawing.Color.Transparent;
            this.toolRead.Image = global::WinFormsAppTest0915.Properties.Resources.twocirclingarrows1_120592;
            this.toolRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRead.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.toolRead.Name = "toolRead";
            this.toolRead.Size = new System.Drawing.Size(40, 55);
            this.toolRead.Text = "조회";
            this.toolRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRead.Click += new System.EventHandler(this.toolRead_Click);
            // 
            // toolExcel
            // 
            this.toolExcel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolExcel.AutoSize = false;
            this.toolExcel.BackColor = System.Drawing.Color.Transparent;
            this.toolExcel.Image = global::WinFormsAppTest0915.Properties.Resources.KakaoTalk_20211024_190303162;
            this.toolExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExcel.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.toolExcel.Name = "toolExcel";
            this.toolExcel.Size = new System.Drawing.Size(40, 55);
            this.toolExcel.Text = "엑셀";
            this.toolExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExcel.Click += new System.EventHandler(this.toolExcel_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(980, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 23);
            this.label1.TabIndex = 35;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 35);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1575, 17);
            this.tabControl1.TabIndex = 41;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // frmMainEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1575, 736);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainEmp";
            this.Text = "필름 사진관";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainEmp_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmMainEmp_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMemMg;
        private System.Windows.Forms.ToolStripMenuItem tsAdvMg;
        private System.Windows.Forms.ToolStripMenuItem tsLogout;
        private System.Windows.Forms.ToolStripMenuItem tsInfo;
        private System.Windows.Forms.ToolStripMenuItem tsPrdMg;
        private System.Windows.Forms.ToolStripMenuItem tsResMg;
        private System.Windows.Forms.ToolStripMenuItem tsOrder;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolUpdate;
        private System.Windows.Forms.ToolStripButton tooLCreate;
        private System.Windows.Forms.ToolStripButton toolRead;
        private System.Windows.Forms.ToolStripMenuItem tsCameraMg;
        private System.Windows.Forms.ToolStripMenuItem tsLendingMg;
        private csTabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem tsEmpMg;
        private System.Windows.Forms.ToolStripMenuItem tsCodeMg;
        private System.Windows.Forms.ToolStripMenuItem tsMemOrderMg;
        private System.Windows.Forms.ToolStripMenuItem tsNonMemOrderMg;
        private System.Windows.Forms.ToolStripButton toolExcel;
    }
}