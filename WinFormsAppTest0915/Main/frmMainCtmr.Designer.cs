
using System;

namespace WinFormsAppTest0915
{
    partial class frmMainCtmr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainCtmr));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.tsReserve = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOrderList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsResList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLendingList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAdvice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAsk = new System.Windows.Forms.ToolStripMenuItem();
            this.tsReply = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRead = new System.Windows.Forms.ToolStripButton();
            this.toolCreate = new System.Windows.Forms.ToolStripButton();
            this.toolUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(432, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 23);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProduct,
            this.tsCamera,
            this.tsReserve,
            this.tsLogout,
            this.tsInfo,
            this.tsList,
            this.tsCart,
            this.tsAdvice});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1231, 35);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsProduct
            // 
            this.tsProduct.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsProduct.Margin = new System.Windows.Forms.Padding(1);
            this.tsProduct.Name = "tsProduct";
            this.tsProduct.Size = new System.Drawing.Size(71, 29);
            this.tsProduct.Text = "상품 주문";
            this.tsProduct.Click += new System.EventHandler(this.tsProduct_Click);
            // 
            // tsCamera
            // 
            this.tsCamera.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsCamera.Margin = new System.Windows.Forms.Padding(1);
            this.tsCamera.Name = "tsCamera";
            this.tsCamera.Size = new System.Drawing.Size(83, 29);
            this.tsCamera.Text = "카메라 대여";
            this.tsCamera.Click += new System.EventHandler(this.tsCamera_Click);
            // 
            // tsReserve
            // 
            this.tsReserve.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsReserve.Margin = new System.Windows.Forms.Padding(1);
            this.tsReserve.Name = "tsReserve";
            this.tsReserve.Size = new System.Drawing.Size(71, 29);
            this.tsReserve.Text = "촬영 예약";
            this.tsReserve.Click += new System.EventHandler(this.tsReserve_Click);
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
            // tsList
            // 
            this.tsList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOrderList,
            this.tsResList,
            this.tsLendingList});
            this.tsList.Margin = new System.Windows.Forms.Padding(1);
            this.tsList.Name = "tsList";
            this.tsList.Size = new System.Drawing.Size(100, 29);
            this.tsList.Text = "주문/예약 내역";
            // 
            // tsOrderList
            // 
            this.tsOrderList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsOrderList.Name = "tsOrderList";
            this.tsOrderList.Size = new System.Drawing.Size(166, 22);
            this.tsOrderList.Text = "상품 주문 내역";
            this.tsOrderList.Click += new System.EventHandler(this.tsOrderList_Click);
            // 
            // tsResList
            // 
            this.tsResList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsResList.Name = "tsResList";
            this.tsResList.Size = new System.Drawing.Size(166, 22);
            this.tsResList.Text = "촬영 예약 내역";
            this.tsResList.Click += new System.EventHandler(this.tsResList_Click);
            // 
            // tsLendingList
            // 
            this.tsLendingList.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsLendingList.Name = "tsLendingList";
            this.tsLendingList.Size = new System.Drawing.Size(166, 22);
            this.tsLendingList.Text = "카메라 대여 내역";
            this.tsLendingList.Click += new System.EventHandler(this.tsLendingList_Click);
            // 
            // tsCart
            // 
            this.tsCart.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsCart.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsCart.Margin = new System.Windows.Forms.Padding(1);
            this.tsCart.Name = "tsCart";
            this.tsCart.Size = new System.Drawing.Size(67, 29);
            this.tsCart.Text = "장바구니";
            this.tsCart.Click += new System.EventHandler(this.tsCart_Click);
            // 
            // tsAdvice
            // 
            this.tsAdvice.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsAdvice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAsk,
            this.tsReply});
            this.tsAdvice.Margin = new System.Windows.Forms.Padding(1);
            this.tsAdvice.Name = "tsAdvice";
            this.tsAdvice.Size = new System.Drawing.Size(64, 29);
            this.tsAdvice.Text = "1:1 문의";
            // 
            // tsAsk
            // 
            this.tsAsk.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsAsk.Name = "tsAsk";
            this.tsAsk.Size = new System.Drawing.Size(126, 22);
            this.tsAsk.Text = "문의하기";
            this.tsAsk.Click += new System.EventHandler(this.tsAsk_Click);
            // 
            // tsReply
            // 
            this.tsReply.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsReply.Name = "tsReply";
            this.tsReply.Size = new System.Drawing.Size(126, 22);
            this.tsReply.Text = "답변 확인";
            this.tsReply.Click += new System.EventHandler(this.tsReply_Click);
            // 
            // toolRead
            // 
            this.toolRead.AutoSize = false;
            this.toolRead.BackColor = System.Drawing.Color.AliceBlue;
            this.toolRead.Image = global::WinFormsAppTest0915.Properties.Resources.twocirclingarrows1_120592;
            this.toolRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRead.Name = "toolRead";
            this.toolRead.Size = new System.Drawing.Size(52, 67);
            this.toolRead.Text = "조회";
            this.toolRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolCreate
            // 
            this.toolCreate.Image = global::WinFormsAppTest0915.Properties.Resources.create;
            this.toolCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCreate.Name = "toolCreate";
            this.toolCreate.Size = new System.Drawing.Size(52, 53);
            this.toolCreate.Text = "추가";
            this.toolCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolUpdate
            // 
            this.toolUpdate.Image = global::WinFormsAppTest0915.Properties.Resources.update;
            this.toolUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUpdate.Name = "toolUpdate";
            this.toolUpdate.Size = new System.Drawing.Size(52, 53);
            this.toolUpdate.Text = "수정";
            this.toolUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolDelete
            // 
            this.toolDelete.Image = global::WinFormsAppTest0915.Properties.Resources.delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(52, 53);
            this.toolDelete.Text = "삭제";
            this.toolDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRead,
            this.toolCreate,
            this.toolUpdate,
            this.toolDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 35);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(941, 56);
            this.toolStrip1.TabIndex = 33;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // frmMainCtmr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1231, 727);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainCtmr";
            this.Text = "필름 사진관";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainCtmr_FormClosing);
            this.Load += new System.EventHandler(this.FrmCtmr_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsProduct;
        private System.Windows.Forms.ToolStripMenuItem tsCamera;
        private System.Windows.Forms.ToolStripMenuItem tsReserve;
        private System.Windows.Forms.ToolStripMenuItem tsLogout;
        private System.Windows.Forms.ToolStripMenuItem tsInfo;
        private System.Windows.Forms.ToolStripMenuItem tsList;
        private System.Windows.Forms.ToolStripMenuItem tsCart;
        private System.Windows.Forms.ToolStripButton toolRead;
        private System.Windows.Forms.ToolStripButton toolCreate;
        private System.Windows.Forms.ToolStripButton toolUpdate;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsAdvice;
        private System.Windows.Forms.ToolStripMenuItem tsAsk;
        private System.Windows.Forms.ToolStripMenuItem tsReply;
        private System.Windows.Forms.ToolStripMenuItem tsOrderList;
        private System.Windows.Forms.ToolStripMenuItem tsResList;
        private System.Windows.Forms.ToolStripMenuItem tsLendingList;

    }
}