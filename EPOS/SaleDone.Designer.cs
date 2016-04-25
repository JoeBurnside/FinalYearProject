namespace EPOS
{
    partial class SaleDone
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
            this.components = new System.ComponentModel.Container();
            this.labelStaffName = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelChange = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReceipt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStaffName
            // 
            this.labelStaffName.AutoSize = true;
            this.labelStaffName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStaffName.Location = new System.Drawing.Point(160, 0);
            this.labelStaffName.Name = "labelStaffName";
            this.labelStaffName.Padding = new System.Windows.Forms.Padding(20);
            this.labelStaffName.Size = new System.Drawing.Size(160, 65);
            this.labelStaffName.TabIndex = 135;
            this.labelStaffName.Text = "Pub Name";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Name = "labelName";
            this.labelName.Padding = new System.Windows.Forms.Padding(20);
            this.labelName.Size = new System.Drawing.Size(160, 65);
            this.labelName.TabIndex = 134;
            this.labelName.Text = "Pub Name";
            // 
            // buttonExit
            // 
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(500, 610);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(8);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(366, 100);
            this.buttonExit.TabIndex = 133;
            this.buttonExit.TabStop = false;
            this.buttonExit.Text = "New Sale";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(1261, 0);
            this.labelDate.Margin = new System.Windows.Forms.Padding(0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Padding = new System.Windows.Forms.Padding(20);
            this.labelDate.Size = new System.Drawing.Size(105, 64);
            this.labelDate.TabIndex = 132;
            this.labelDate.Text = "DATE";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelChange
            // 
            this.labelChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChange.Location = new System.Drawing.Point(-3, 224);
            this.labelChange.Name = "labelChange";
            this.labelChange.Size = new System.Drawing.Size(1369, 141);
            this.labelChange.TabIndex = 136;
            this.labelChange.Text = "£00.00";
            this.labelChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1369, 141);
            this.label1.TabIndex = 137;
            this.label1.Text = "Change:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReceipt
            // 
            this.buttonReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonReceipt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReceipt.Location = new System.Drawing.Point(500, 373);
            this.buttonReceipt.Margin = new System.Windows.Forms.Padding(8);
            this.buttonReceipt.Name = "buttonReceipt";
            this.buttonReceipt.Size = new System.Drawing.Size(366, 100);
            this.buttonReceipt.TabIndex = 138;
            this.buttonReceipt.TabStop = false;
            this.buttonReceipt.Text = "Print Receipt";
            this.buttonReceipt.UseVisualStyleBackColor = true;
            this.buttonReceipt.Click += new System.EventHandler(this.buttonReceipt_Click);
            // 
            // SaleDone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.buttonReceipt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelChange);
            this.Controls.Add(this.labelStaffName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaleDone";
            this.Text = "SaleDone";
            this.Load += new System.EventHandler(this.SaleDone_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStaffName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelChange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReceipt;
    }
}