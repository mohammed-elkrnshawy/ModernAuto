namespace Modern_Auto
{
    partial class Form_Customer_Details
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_Render = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Payment = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Total = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_OldMoney = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_AfterDiscount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Discount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_MaterialTotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(189)))), ((int)(((byte)(212)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1248, 356);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txt_Render);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txt_Payment);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txt_Total);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txt_OldMoney);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txt_AfterDiscount);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txt_Discount);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txt_MaterialTotal);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1248, 171);
            this.panel1.TabIndex = 1;
            // 
            // txt_Render
            // 
            this.txt_Render.Enabled = false;
            this.txt_Render.Location = new System.Drawing.Point(162, 116);
            this.txt_Render.Name = "txt_Render";
            this.txt_Render.ReadOnly = true;
            this.txt_Render.Size = new System.Drawing.Size(125, 20);
            this.txt_Render.TabIndex = 44;
            this.txt_Render.Text = "0";
            this.txt_Render.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(293, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(182, 28);
            this.label16.TabIndex = 43;
            this.label16.Text = "المبلغ الباقى";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Payment
            // 
            this.txt_Payment.Location = new System.Drawing.Point(162, 75);
            this.txt_Payment.Name = "txt_Payment";
            this.txt_Payment.ReadOnly = true;
            this.txt_Payment.Size = new System.Drawing.Size(125, 20);
            this.txt_Payment.TabIndex = 42;
            this.txt_Payment.Text = "0";
            this.txt_Payment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(293, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(182, 28);
            this.label13.TabIndex = 41;
            this.label13.Text = "اجمالى المدفوع";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Total
            // 
            this.txt_Total.Enabled = false;
            this.txt_Total.Location = new System.Drawing.Point(522, 75);
            this.txt_Total.Name = "txt_Total";
            this.txt_Total.ReadOnly = true;
            this.txt_Total.Size = new System.Drawing.Size(125, 20);
            this.txt_Total.TabIndex = 40;
            this.txt_Total.Text = "0";
            this.txt_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(653, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(141, 28);
            this.label14.TabIndex = 39;
            this.label14.Text = "اجمالى الحساب";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_OldMoney
            // 
            this.txt_OldMoney.Enabled = false;
            this.txt_OldMoney.Location = new System.Drawing.Point(842, 75);
            this.txt_OldMoney.Name = "txt_OldMoney";
            this.txt_OldMoney.ReadOnly = true;
            this.txt_OldMoney.Size = new System.Drawing.Size(125, 20);
            this.txt_OldMoney.TabIndex = 38;
            this.txt_OldMoney.Text = "0";
            this.txt_OldMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(973, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(141, 28);
            this.label15.TabIndex = 37;
            this.label15.Text = "حساب قديم";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_AfterDiscount
            // 
            this.txt_AfterDiscount.Enabled = false;
            this.txt_AfterDiscount.Location = new System.Drawing.Point(162, 36);
            this.txt_AfterDiscount.Name = "txt_AfterDiscount";
            this.txt_AfterDiscount.ReadOnly = true;
            this.txt_AfterDiscount.Size = new System.Drawing.Size(125, 20);
            this.txt_AfterDiscount.TabIndex = 36;
            this.txt_AfterDiscount.Text = "0";
            this.txt_AfterDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(293, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(182, 28);
            this.label12.TabIndex = 35;
            this.label12.Text = "اجمالى بعد الخصم";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Discount
            // 
            this.txt_Discount.Location = new System.Drawing.Point(522, 36);
            this.txt_Discount.Name = "txt_Discount";
            this.txt_Discount.ReadOnly = true;
            this.txt_Discount.Size = new System.Drawing.Size(125, 20);
            this.txt_Discount.TabIndex = 34;
            this.txt_Discount.Text = "0";
            this.txt_Discount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(653, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 28);
            this.label11.TabIndex = 33;
            this.label11.Text = "اجمالى الخصم";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_MaterialTotal
            // 
            this.txt_MaterialTotal.Enabled = false;
            this.txt_MaterialTotal.Location = new System.Drawing.Point(842, 36);
            this.txt_MaterialTotal.Name = "txt_MaterialTotal";
            this.txt_MaterialTotal.ReadOnly = true;
            this.txt_MaterialTotal.Size = new System.Drawing.Size(125, 20);
            this.txt_MaterialTotal.TabIndex = 32;
            this.txt_MaterialTotal.Text = "0";
            this.txt_MaterialTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(973, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 28);
            this.label10.TabIndex = 31;
            this.label10.Text = "اجمالى الفاتورة";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(2, 179);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1248, 356);
            this.panel2.TabIndex = 2;
            // 
            // Form_Customer_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 536);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form_Customer_Details";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تفاصيل الفاتورة";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Customer_Details_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_Render;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Payment;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Total;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_OldMoney;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_AfterDiscount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Discount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_MaterialTotal;
        private System.Windows.Forms.Label label10;
    }
}