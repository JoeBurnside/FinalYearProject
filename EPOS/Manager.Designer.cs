namespace EPOS
{
    partial class Manager
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
            this.labelDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonShowTrans = new System.Windows.Forms.Button();
            this.buttonNoSale = new System.Windows.Forms.Button();
            this.buttonPaymentMan = new System.Windows.Forms.Button();
            this.buttonCategoryMan = new System.Windows.Forms.Button();
            this.buttonButtonMan = new System.Windows.Forms.Button();
            this.buttonProductMan = new System.Windows.Forms.Button();
            this.buttonUserMan = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonSalesTot = new System.Windows.Forms.Button();
            this.buttonStaffTot = new System.Windows.Forms.Button();
            this.buttonTables = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(1249, 0);
            this.labelDate.Margin = new System.Windows.Forms.Padding(0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Padding = new System.Windows.Forms.Padding(20);
            this.labelDate.Size = new System.Drawing.Size(105, 64);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "DATE";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // buttonBack
            // 
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Location = new System.Drawing.Point(508, 630);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(350, 100);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.TabStop = false;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.Location = new System.Drawing.Point(100, 100);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(254, 120);
            this.buttonSettings.TabIndex = 4;
            this.buttonSettings.TabStop = false;
            this.buttonSettings.Text = "System Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonShowTrans
            // 
            this.buttonShowTrans.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonShowTrans.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowTrans.Location = new System.Drawing.Point(404, 100);
            this.buttonShowTrans.Name = "buttonShowTrans";
            this.buttonShowTrans.Size = new System.Drawing.Size(254, 120);
            this.buttonShowTrans.TabIndex = 5;
            this.buttonShowTrans.TabStop = false;
            this.buttonShowTrans.Text = "Show Transactions";
            this.buttonShowTrans.UseVisualStyleBackColor = true;
            this.buttonShowTrans.Click += new System.EventHandler(this.buttonShowTrans_Click);
            // 
            // buttonNoSale
            // 
            this.buttonNoSale.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNoSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNoSale.Location = new System.Drawing.Point(708, 100);
            this.buttonNoSale.Name = "buttonNoSale";
            this.buttonNoSale.Size = new System.Drawing.Size(254, 120);
            this.buttonNoSale.TabIndex = 6;
            this.buttonNoSale.TabStop = false;
            this.buttonNoSale.Text = "No Sale";
            this.buttonNoSale.UseVisualStyleBackColor = true;
            this.buttonNoSale.Click += new System.EventHandler(this.buttonNoSale_Click);
            // 
            // buttonPaymentMan
            // 
            this.buttonPaymentMan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPaymentMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPaymentMan.Location = new System.Drawing.Point(1012, 100);
            this.buttonPaymentMan.Name = "buttonPaymentMan";
            this.buttonPaymentMan.Size = new System.Drawing.Size(254, 120);
            this.buttonPaymentMan.TabIndex = 7;
            this.buttonPaymentMan.TabStop = false;
            this.buttonPaymentMan.Text = "Manage Payment Methods";
            this.buttonPaymentMan.UseVisualStyleBackColor = true;
            this.buttonPaymentMan.Click += new System.EventHandler(this.buttonPaymentMan_Click);
            // 
            // buttonCategoryMan
            // 
            this.buttonCategoryMan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCategoryMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCategoryMan.Location = new System.Drawing.Point(1012, 270);
            this.buttonCategoryMan.Name = "buttonCategoryMan";
            this.buttonCategoryMan.Size = new System.Drawing.Size(254, 120);
            this.buttonCategoryMan.TabIndex = 11;
            this.buttonCategoryMan.TabStop = false;
            this.buttonCategoryMan.Text = "Manage Categories";
            this.buttonCategoryMan.UseVisualStyleBackColor = true;
            this.buttonCategoryMan.Click += new System.EventHandler(this.buttonCategoryMan_Click);
            // 
            // buttonButtonMan
            // 
            this.buttonButtonMan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonButtonMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonButtonMan.Location = new System.Drawing.Point(708, 270);
            this.buttonButtonMan.Name = "buttonButtonMan";
            this.buttonButtonMan.Size = new System.Drawing.Size(254, 120);
            this.buttonButtonMan.TabIndex = 10;
            this.buttonButtonMan.TabStop = false;
            this.buttonButtonMan.Text = "Manage Buttons";
            this.buttonButtonMan.UseVisualStyleBackColor = true;
            // 
            // buttonProductMan
            // 
            this.buttonProductMan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonProductMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProductMan.Location = new System.Drawing.Point(404, 270);
            this.buttonProductMan.Name = "buttonProductMan";
            this.buttonProductMan.Size = new System.Drawing.Size(254, 120);
            this.buttonProductMan.TabIndex = 9;
            this.buttonProductMan.TabStop = false;
            this.buttonProductMan.Text = "Manage Products";
            this.buttonProductMan.UseVisualStyleBackColor = true;
            this.buttonProductMan.Click += new System.EventHandler(this.buttonProductMan_Click);
            // 
            // buttonUserMan
            // 
            this.buttonUserMan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUserMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUserMan.Location = new System.Drawing.Point(100, 270);
            this.buttonUserMan.Name = "buttonUserMan";
            this.buttonUserMan.Size = new System.Drawing.Size(254, 120);
            this.buttonUserMan.TabIndex = 8;
            this.buttonUserMan.TabStop = false;
            this.buttonUserMan.Text = "Manage Users";
            this.buttonUserMan.UseVisualStyleBackColor = true;
            this.buttonUserMan.Click += new System.EventHandler(this.buttonUserMan_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnd.Location = new System.Drawing.Point(1012, 440);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(254, 120);
            this.buttonEnd.TabIndex = 15;
            this.buttonEnd.TabStop = false;
            this.buttonEnd.Text = "End Of Day";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonSalesTot
            // 
            this.buttonSalesTot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSalesTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSalesTot.Location = new System.Drawing.Point(708, 440);
            this.buttonSalesTot.Name = "buttonSalesTot";
            this.buttonSalesTot.Size = new System.Drawing.Size(254, 120);
            this.buttonSalesTot.TabIndex = 14;
            this.buttonSalesTot.TabStop = false;
            this.buttonSalesTot.Text = "Sales Totals";
            this.buttonSalesTot.UseVisualStyleBackColor = true;
            this.buttonSalesTot.Click += new System.EventHandler(this.buttonSalesTot_Click);
            // 
            // buttonStaffTot
            // 
            this.buttonStaffTot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStaffTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStaffTot.Location = new System.Drawing.Point(404, 440);
            this.buttonStaffTot.Name = "buttonStaffTot";
            this.buttonStaffTot.Size = new System.Drawing.Size(254, 120);
            this.buttonStaffTot.TabIndex = 13;
            this.buttonStaffTot.TabStop = false;
            this.buttonStaffTot.Text = "Staff Totals";
            this.buttonStaffTot.UseVisualStyleBackColor = true;
            this.buttonStaffTot.Click += new System.EventHandler(this.buttonStaffTot_Click);
            // 
            // buttonTables
            // 
            this.buttonTables.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTables.Location = new System.Drawing.Point(100, 440);
            this.buttonTables.Name = "buttonTables";
            this.buttonTables.Size = new System.Drawing.Size(254, 120);
            this.buttonTables.TabIndex = 12;
            this.buttonTables.TabStop = false;
            this.buttonTables.Text = "Table Status";
            this.buttonTables.UseVisualStyleBackColor = true;
            this.buttonTables.Click += new System.EventHandler(this.button12_Click);
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
            this.labelName.TabIndex = 16;
            this.labelName.Text = "Pub Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 55);
            this.label1.TabIndex = 17;
            this.label1.Text = "Manager Menu";
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(29F, 55F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1354, 733);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonSalesTot);
            this.Controls.Add(this.buttonStaffTot);
            this.Controls.Add(this.buttonTables);
            this.Controls.Add(this.buttonCategoryMan);
            this.Controls.Add(this.buttonButtonMan);
            this.Controls.Add(this.buttonProductMan);
            this.Controls.Add(this.buttonUserMan);
            this.Controls.Add(this.buttonPaymentMan);
            this.Controls.Add(this.buttonNoSale);
            this.Controls.Add(this.buttonShowTrans);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelDate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(15, 13, 15, 13);
            this.Name = "Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Manager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonShowTrans;
        private System.Windows.Forms.Button buttonNoSale;
        private System.Windows.Forms.Button buttonPaymentMan;
        private System.Windows.Forms.Button buttonCategoryMan;
        private System.Windows.Forms.Button buttonButtonMan;
        private System.Windows.Forms.Button buttonProductMan;
        private System.Windows.Forms.Button buttonUserMan;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonSalesTot;
        private System.Windows.Forms.Button buttonStaffTot;
        private System.Windows.Forms.Button buttonTables;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label1;
    }
}