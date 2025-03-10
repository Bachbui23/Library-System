using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                LoginForm lForm = new LoginForm();
                lForm.Show();
                this.Hide();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void user_btn_Click(object sender, EventArgs e)
        {
            userManagement1.Visible = true;
            categoryManage1.Visible = false;
            bookManage1.Visible = false;
            lendingManagement1.Visible = false;
        }

        private void category_btn_Click(object sender, EventArgs e)
        {
            userManagement1.Visible = false;
            categoryManage1.Visible = true;
            bookManage1.Visible = false;
            lendingManagement1.Visible = false;
        }

        private void books_btn_Click(object sender, EventArgs e)
        {
            userManagement1.Visible = false;
            categoryManage1.Visible = false;
            bookManage1.Visible = true;
            lendingManagement1.Visible = false;
        }
        private void lendbtn_Click(object sender, EventArgs e)
        {
            userManagement1.Visible = false;
            categoryManage1.Visible = false;
            bookManage1.Visible = false;
            lendingManagement1.Visible = true;
        }
    }
}
