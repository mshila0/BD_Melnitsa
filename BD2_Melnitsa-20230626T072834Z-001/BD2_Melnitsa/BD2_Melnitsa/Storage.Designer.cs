
namespace BD2_Melnitsa
{
    partial class Storage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Storage));
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(505, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Типы складов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(450, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Учет товаров на складах";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button6.Location = new System.Drawing.Point(-2, 529);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(154, 60);
            this.button6.TabIndex = 14;
            this.button6.Text = "Сохранить";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.SavebuttonY_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button7.Location = new System.Drawing.Point(-2, 466);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(154, 57);
            this.button7.TabIndex = 15;
            this.button7.Text = "Добавить";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.addbuttonY_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button9.Location = new System.Drawing.Point(-2, 278);
            this.button9.Name = "button9";
            this.button9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button9.Size = new System.Drawing.Size(154, 60);
            this.button9.TabIndex = 17;
            this.button9.Text = "Заказ товара";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.Order_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button10.Location = new System.Drawing.Point(-2, 210);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(154, 62);
            this.button10.TabIndex = 18;
            this.button10.Text = "Прием товара";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.Delivery_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(222)))), ((int)(((byte)(205)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(268, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(604, 270);
            this.dataGridView1.TabIndex = 19;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(222)))), ((int)(((byte)(205)))));
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(361, 403);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(440, 249);
            this.dataGridView4.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button2.Location = new System.Drawing.Point(-2, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 56);
            this.button2.TabIndex = 23;
            this.button2.Text = "Товары";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Product_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button4.Location = new System.Drawing.Point(-2, 32);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(154, 57);
            this.button4.TabIndex = 9;
            this.button4.Text = "Добавить";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.addButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.Location = new System.Drawing.Point(-2, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 59);
            this.button1.TabIndex = 10;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Savebutton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(222)))), ((int)(((byte)(205)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 675);
            this.panel1.TabIndex = 24;
            // 
            // Storage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(948, 677);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(964, 716);
            this.MinimumSize = new System.Drawing.Size(964, 716);
            this.Name = "Storage";
            this.Text = "Склады";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}