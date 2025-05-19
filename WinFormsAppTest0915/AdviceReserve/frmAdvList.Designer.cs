
namespace WinFormsAppTest0915
{
    partial class frmAdvList
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtIsReply = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtRepContents = new System.Windows.Forms.TextBox();
            this.txtRepName = new System.Windows.Forms.TextBox();
            this.dtpReplyDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContents = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtIsReply);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Controls.Add(this.txtRepContents);
            this.panel2.Controls.Add(this.txtRepName);
            this.panel2.Controls.Add(this.dtpReplyDate);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtContents);
            this.panel2.Location = new System.Drawing.Point(55, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(691, 594);
            this.panel2.TabIndex = 235;
            // 
            // txtIsReply
            // 
            this.txtIsReply.BackColor = System.Drawing.SystemColors.Window;
            this.txtIsReply.Location = new System.Drawing.Point(123, 378);
            this.txtIsReply.Name = "txtIsReply";
            this.txtIsReply.ReadOnly = true;
            this.txtIsReply.Size = new System.Drawing.Size(50, 21);
            this.txtIsReply.TabIndex = 270;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(60, 381);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 12);
            this.label15.TabIndex = 269;
            this.label15.Text = "답변 여부";
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(123, 243);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(160, 21);
            this.dtpDate.TabIndex = 122;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 121;
            this.label1.Text = "문의 날짜";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(47, 31);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(610, 194);
            this.listView1.TabIndex = 120;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // txtRepContents
            // 
            this.txtRepContents.BackColor = System.Drawing.SystemColors.Window;
            this.txtRepContents.Location = new System.Drawing.Point(123, 482);
            this.txtRepContents.Multiline = true;
            this.txtRepContents.Name = "txtRepContents";
            this.txtRepContents.ReadOnly = true;
            this.txtRepContents.Size = new System.Drawing.Size(534, 102);
            this.txtRepContents.TabIndex = 119;
            // 
            // txtRepName
            // 
            this.txtRepName.BackColor = System.Drawing.SystemColors.Window;
            this.txtRepName.Location = new System.Drawing.Point(123, 444);
            this.txtRepName.Name = "txtRepName";
            this.txtRepName.ReadOnly = true;
            this.txtRepName.Size = new System.Drawing.Size(102, 21);
            this.txtRepName.TabIndex = 118;
            // 
            // dtpReplyDate
            // 
            this.dtpReplyDate.Enabled = false;
            this.dtpReplyDate.Location = new System.Drawing.Point(123, 414);
            this.dtpReplyDate.Name = "dtpReplyDate";
            this.dtpReplyDate.Size = new System.Drawing.Size(160, 21);
            this.dtpReplyDate.TabIndex = 117;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 485);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 116;
            this.label5.Text = "답변 내용";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 450);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 12);
            this.label4.TabIndex = 115;
            this.label4.Text = "답변 직원 이름";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 421);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 114;
            this.label6.Text = "답변 날짜";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 113;
            this.label7.Text = "문의 내용";
            // 
            // txtContents
            // 
            this.txtContents.BackColor = System.Drawing.SystemColors.Window;
            this.txtContents.Location = new System.Drawing.Point(123, 270);
            this.txtContents.Multiline = true;
            this.txtContents.Name = "txtContents";
            this.txtContents.ReadOnly = true;
            this.txtContents.Size = new System.Drawing.Size(534, 102);
            this.txtContents.TabIndex = 112;
            // 
            // frmAdvList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 646);
            this.Controls.Add(this.panel2);
            this.Name = "frmAdvList";
            this.Text = "문의 내역 확인";
            this.Load += new System.EventHandler(this.frmAdvList_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtIsReply;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txtRepContents;
        private System.Windows.Forms.TextBox txtRepName;
        private System.Windows.Forms.DateTimePicker dtpReplyDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContents;
    }
}