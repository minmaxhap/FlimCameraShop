
namespace WinFormsAppTest0915
{
    partial class ucBtnReadDate
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpLeft = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRight = new System.Windows.Forms.DateTimePicker();
            this.btn1Week = new System.Windows.Forms.Button();
            this.btn1Month = new System.Windows.Forms.Button();
            this.btn3Months = new System.Windows.Forms.Button();
            this.btn6Months = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpLeft
            // 
            this.dtpLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpLeft.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLeft.Location = new System.Drawing.Point(3, 3);
            this.dtpLeft.Name = "dtpLeft";
            this.dtpLeft.Size = new System.Drawing.Size(117, 21);
            this.dtpLeft.TabIndex = 0;
            this.dtpLeft.Value = new System.DateTime(2021, 9, 1, 0, 0, 0, 0);
            this.dtpLeft.ValueChanged += new System.EventHandler(this.dtpLeft_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "~";
            // 
            // dtpRight
            // 
            this.dtpRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpRight.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRight.Location = new System.Drawing.Point(145, 3);
            this.dtpRight.Name = "dtpRight";
            this.dtpRight.Size = new System.Drawing.Size(109, 21);
            this.dtpRight.TabIndex = 2;
            this.dtpRight.Value = new System.DateTime(2021, 9, 15, 0, 0, 0, 0);
            this.dtpRight.ValueChanged += new System.EventHandler(this.dtpRight_ValueChanged);
            // 
            // btn1Week
            // 
            this.btn1Week.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn1Week.FlatAppearance.BorderSize = 0;
            this.btn1Week.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1Week.Location = new System.Drawing.Point(335, 2);
            this.btn1Week.Name = "btn1Week";
            this.btn1Week.Size = new System.Drawing.Size(43, 23);
            this.btn1Week.TabIndex = 3;
            this.btn1Week.Text = "1주";
            this.btn1Week.UseVisualStyleBackColor = false;
            this.btn1Week.Click += new System.EventHandler(this.btn1Week_Click);
            // 
            // btn1Month
            // 
            this.btn1Month.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn1Month.FlatAppearance.BorderSize = 0;
            this.btn1Month.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1Month.Location = new System.Drawing.Point(386, 2);
            this.btn1Month.Name = "btn1Month";
            this.btn1Month.Size = new System.Drawing.Size(55, 23);
            this.btn1Month.TabIndex = 4;
            this.btn1Month.Text = "1개월";
            this.btn1Month.UseVisualStyleBackColor = false;
            this.btn1Month.Click += new System.EventHandler(this.btn1Month_Click);
            // 
            // btn3Months
            // 
            this.btn3Months.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn3Months.FlatAppearance.BorderSize = 0;
            this.btn3Months.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3Months.Location = new System.Drawing.Point(449, 2);
            this.btn3Months.Name = "btn3Months";
            this.btn3Months.Size = new System.Drawing.Size(55, 23);
            this.btn3Months.TabIndex = 5;
            this.btn3Months.Text = "3개월";
            this.btn3Months.UseVisualStyleBackColor = false;
            this.btn3Months.Click += new System.EventHandler(this.btn3Months_Click);
            // 
            // btn6Months
            // 
            this.btn6Months.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn6Months.FlatAppearance.BorderSize = 0;
            this.btn6Months.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn6Months.Location = new System.Drawing.Point(512, 2);
            this.btn6Months.Name = "btn6Months";
            this.btn6Months.Size = new System.Drawing.Size(55, 23);
            this.btn6Months.TabIndex = 6;
            this.btn6Months.Text = "6개월";
            this.btn6Months.UseVisualStyleBackColor = false;
            this.btn6Months.Click += new System.EventHandler(this.btn6Months_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(273, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "조회";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucBtnReadDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn6Months);
            this.Controls.Add(this.btn3Months);
            this.Controls.Add(this.btn1Month);
            this.Controls.Add(this.btn1Week);
            this.Controls.Add(this.dtpRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpLeft);
            this.Name = "ucBtnReadDate";
            this.Size = new System.Drawing.Size(570, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRight;
        private System.Windows.Forms.Button btn1Week;
        private System.Windows.Forms.Button btn1Month;
        private System.Windows.Forms.Button btn3Months;
        private System.Windows.Forms.Button btn6Months;
        private System.Windows.Forms.Button button1;
    }
}
