namespace LManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.logout_btn = new System.Windows.Forms.Button();
            this.books_btn = new System.Windows.Forms.Button();
            this.category_btn = new System.Windows.Forms.Button();
            this.user_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.exit = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.userManagement1 = new LManagement.UserManagement();
            this.categoryManage1 = new LManagement.CategoryManage();
            this.bookManage1 = new LManagement.BookManage();
            this.lendingManagement1 = new LManagement.LendingManagement();
            this.lendbtn = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lendbtn);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.logout_btn);
            this.panel2.Controls.Add(this.books_btn);
            this.panel2.Controls.Add(this.category_btn);
            this.panel2.Controls.Add(this.user_btn);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(293, 738);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "LIBRARY MANAGEMENT SYSTEM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(79, 703);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Log out";
            // 
            // logout_btn
            // 
            this.logout_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logout_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.logout_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.ForeColor = System.Drawing.Color.White;
            this.logout_btn.Image = ((System.Drawing.Image)(resources.GetObject("logout_btn.Image")));
            this.logout_btn.Location = new System.Drawing.Point(24, 681);
            this.logout_btn.Margin = new System.Windows.Forms.Padding(4);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(47, 43);
            this.logout_btn.TabIndex = 12;
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // books_btn
            // 
            this.books_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.books_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.books_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.books_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.books_btn.ForeColor = System.Drawing.Color.White;
            this.books_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.books_btn.Location = new System.Drawing.Point(10, 465);
            this.books_btn.Margin = new System.Windows.Forms.Padding(4);
            this.books_btn.Name = "books_btn";
            this.books_btn.Size = new System.Drawing.Size(267, 55);
            this.books_btn.TabIndex = 10;
            this.books_btn.Text = "BOOK MANAGEMENT";
            this.books_btn.UseVisualStyleBackColor = true;
            this.books_btn.Click += new System.EventHandler(this.books_btn_Click);
            // 
            // category_btn
            // 
            this.category_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.category_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.category_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.category_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.category_btn.ForeColor = System.Drawing.Color.White;
            this.category_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.category_btn.Location = new System.Drawing.Point(10, 371);
            this.category_btn.Margin = new System.Windows.Forms.Padding(4);
            this.category_btn.Name = "category_btn";
            this.category_btn.Size = new System.Drawing.Size(267, 55);
            this.category_btn.TabIndex = 9;
            this.category_btn.Text = "CATEGORY MANAGEMENT";
            this.category_btn.UseVisualStyleBackColor = true;
            this.category_btn.Click += new System.EventHandler(this.category_btn_Click);
            // 
            // user_btn
            // 
            this.user_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.user_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.user_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.user_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.user_btn.ForeColor = System.Drawing.Color.White;
            this.user_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.user_btn.Location = new System.Drawing.Point(11, 273);
            this.user_btn.Margin = new System.Windows.Forms.Padding(4);
            this.user_btn.Name = "user_btn";
            this.user_btn.Size = new System.Drawing.Size(267, 55);
            this.user_btn.TabIndex = 8;
            this.user_btn.Text = "USER MANAGEMENT";
            this.user_btn.UseVisualStyleBackColor = true;
            this.user_btn.Click += new System.EventHandler(this.user_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(72, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Welcome Admin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(85, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 108);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(128)))), ((int)(((byte)(87)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.exit);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(293, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1174, 43);
            this.panel1.TabIndex = 3;
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(1142, 8);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(19, 22);
            this.exit.TabIndex = 6;
            this.exit.Text = "X";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1443, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "X";
            // 
            // userManagement1
            // 
            this.userManagement1.Location = new System.Drawing.Point(293, 43);
            this.userManagement1.Name = "userManagement1";
            this.userManagement1.Size = new System.Drawing.Size(1173, 695);
            this.userManagement1.TabIndex = 6;
            // 
            // categoryManage1
            // 
            this.categoryManage1.Location = new System.Drawing.Point(293, 43);
            this.categoryManage1.Name = "categoryManage1";
            this.categoryManage1.Size = new System.Drawing.Size(1173, 695);
            this.categoryManage1.TabIndex = 5;
            // 
            // bookManage1
            // 
            this.bookManage1.Location = new System.Drawing.Point(293, 43);
            this.bookManage1.Name = "bookManage1";
            this.bookManage1.Size = new System.Drawing.Size(1173, 695);
            this.bookManage1.TabIndex = 4;
            // 
            // lendingManagement1
            // 
            this.lendingManagement1.Location = new System.Drawing.Point(293, 43);
            this.lendingManagement1.Name = "lendingManagement1";
            this.lendingManagement1.Size = new System.Drawing.Size(1173, 695);
            this.lendingManagement1.TabIndex = 7;
            // 
            // lendbtn
            // 
            this.lendbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lendbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.lendbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.lendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lendbtn.ForeColor = System.Drawing.Color.White;
            this.lendbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lendbtn.Location = new System.Drawing.Point(10, 553);
            this.lendbtn.Margin = new System.Windows.Forms.Padding(4);
            this.lendbtn.Name = "lendbtn";
            this.lendbtn.Size = new System.Drawing.Size(267, 55);
            this.lendbtn.TabIndex = 14;
            this.lendbtn.Text = "LENDING MANAGEMENT";
            this.lendbtn.UseVisualStyleBackColor = true;
            this.lendbtn.Click += new System.EventHandler(this.lendbtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 738);
            this.Controls.Add(this.lendingManagement1);
            this.Controls.Add(this.userManagement1);
            this.Controls.Add(this.categoryManage1);
            this.Controls.Add(this.bookManage1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Button books_btn;
        private System.Windows.Forms.Button category_btn;
        private System.Windows.Forms.Button user_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label exit;
        private BookManage bookManage1;
        private CategoryManage categoryManage1;
        private UserManagement userManagement1;
        private System.Windows.Forms.Button lendbtn;
        private LendingManagement lendingManagement1;
    }
}