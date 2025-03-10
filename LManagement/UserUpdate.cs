using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LManagement
{
    public partial class UserUpdate : UserControl
    {
        private int userId;
        private string connectionString = @"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True";
        private Panel passPanel;
        private Label username;
        private TextBox email;
        private TextBox phonenumber;
        private TextBox password1;
        private TextBox address;
        private Button update1;
        private Button update2;
        private TextBox txtOldPassword, txtNewPassword, txtConfirmPassword;
        private Button btnConfirm;
        private Button btnCancel;
        public UserUpdate()
        {
            InitializeComponent();

            username = new Label();
            email = new TextBox();
            phonenumber = new TextBox();
            password1 = new TextBox();
            address = new TextBox();
            this.userId = Session.UserId;  // Lấy userId từ session
            CreateUserPanel();
            LoadUserData();
            CreatePassPanel();
        }

        private void LoadUserData()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT username, email, PhoneNumber, password, Address FROM USERS WHERE id = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                username.Text = reader["username"]?.ToString();
                                email.Text = reader["email"]?.ToString();
                                phonenumber.Text = reader["PhoneNumber"]?.ToString();
                                password1.Text = reader["password"]?.ToString();
                                address.Text = reader["Address"]?.ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
                }
            }
        }

        private void update2_Click(object sender, EventArgs e)
        {
            if (userId == 0)
            {
                MessageBox.Show("Lỗi: Không có UserId hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE USERS SET email = @email, PhoneNumber = @PhoneNumber, Address = @Address WHERE id = @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@email", email.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phonenumber.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void update1_Click(object sender, EventArgs e)
        {
            if (passPanel == null)
            {
                CreatePassPanel();
            }
            passPanel.Visible = true;
            passPanel.BringToFront();
        }

        private void CreateUserPanel()
        {
            Panel userPanel = new Panel
            {
                Size = new Size(1024, 643),
                BackColor = Color.LightGray
            };

            Label username = new Label { Text = "User " + Session.Username, Location = new Point(50, 30), Font = new Font("Arial", 16, FontStyle.Bold), Width = 600, Height = 50};
            //username = new TextBox { Location = new Point(200, 30), Width = 200 };

            Label lblEmail = new Label { Text = "Email:", Location = new Point(50, 200) };
            email = new TextBox { Location = new Point(200, 200), Width = 200 };

            Label lblPhone = new Label { Text = "Phone Number:", Location = new Point(50, 160) };
            phonenumber = new TextBox { Location = new Point(200, 160), Width = 200 };

            Label lblPassword = new Label { Text = "Password:", Location = new Point(50, 90) };
            password1 = new TextBox { Location = new Point(200, 90), Width = 200, PasswordChar = '*' };

            Label lblAddress = new Label { Text = "Address:", Location = new Point(50, 240) };
            address = new TextBox { Location = new Point(200, 240), Width = 200 };

            update2 = new Button
            {
                Text = "Update",
                Location = new Point(330, 280),
                Width = 80,
                Height = 30,
            };
            update1 = new Button
            {
                Text = "Change Password",
                Location = new Point(400, 90),
                Width = 80,
                Height = 30,
            };
            update1.Click += update1_Click;
            update2.Click += update2_Click;

            userPanel.Controls.Add(username);
            userPanel.Controls.Add(update1);
            userPanel.Controls.Add(lblEmail);
            userPanel.Controls.Add(email);
            userPanel.Controls.Add(lblPhone);
            userPanel.Controls.Add(phonenumber);
            userPanel.Controls.Add(lblPassword);
            userPanel.Controls.Add(password1);
            userPanel.Controls.Add(lblAddress);
            userPanel.Controls.Add(address);
            userPanel.Controls.Add(update2);

            this.Controls.Add(userPanel);
        }
        private void CreatePassPanel()
        {
            passPanel = new Panel
            {
                Size = new Size(200, 200),
                BackColor = Color.White,
                Location = new Point(200, 100),
                Visible = false // Ẩn panel ban đầu
            };

            Label lblOldPassword = new Label { Text = "Mật khẩu cũ:", Top = 10, Left = 10 };
            txtOldPassword = new TextBox { Top = 30, Left = 10, Width = 200, PasswordChar = '*' };

            Label lblNewPassword = new Label { Text = "Mật khẩu mới:", Top = 60, Left = 10 };
            txtNewPassword = new TextBox { Top = 80, Left = 10, Width = 200, PasswordChar = '*' };

            Label lblConfirmPassword = new Label { Text = "Nhập lại mật khẩu:", Top = 110, Left = 10 };
            txtConfirmPassword = new TextBox { Top = 130, Left = 10, Width = 200, PasswordChar = '*' };

            btnConfirm = new Button { Text = "Confirm", Top = 160, Left = 10, Height = 30, Width = 100 };
            btnCancel = new Button { Text = "Cancel", Top = 160, Left = 120, Height = 30, Width = 100 };
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            passPanel.Controls.Add(lblOldPassword);
            passPanel.Controls.Add(txtOldPassword);
            passPanel.Controls.Add(lblNewPassword);
            passPanel.Controls.Add(txtNewPassword);
            passPanel.Controls.Add(lblConfirmPassword);
            passPanel.Controls.Add(txtConfirmPassword);
            passPanel.Controls.Add(btnConfirm);
            passPanel.Controls.Add(btnCancel);

            this.Controls.Add(passPanel);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới không khớp!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT password FROM USERS WHERE id = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                string currentPassword = cmd.ExecuteScalar()?.ToString();

                if (currentPassword != oldPassword)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!");
                    return;
                }

                query = "UPDATE USERS SET password = @newPassword WHERE id = @userId";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đổi mật khẩu thành công!");
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            passPanel.Visible = false;
        }
    }
}
