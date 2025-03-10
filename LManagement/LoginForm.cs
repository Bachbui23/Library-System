using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LManagement
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void login_checkpass_CheckedChanged(object sender, EventArgs e)
        {
            login_pass.PasswordChar = login_checkpass.Checked ? '\0' : '*';
        }
        private void Login_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_pass.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();

                        // Kiểm tra tài khoản admin trước
                        string adminQuery = "SELECT * FROM ADMIN WHERE adname = @username AND adpassword = @password";
                        using (SqlCommand cmdAdmin = new SqlCommand(adminQuery, conn))
                        {
                            cmdAdmin.Parameters.AddWithValue("@username", login_username.Text.Trim());
                            cmdAdmin.Parameters.AddWithValue("@password", login_pass.Text.Trim());

                            SqlDataAdapter adminAdapter = new SqlDataAdapter(cmdAdmin);
                            DataTable adminTable = new DataTable();
                            adminAdapter.Fill(adminTable);

                            if (adminTable.Rows.Count >= 1)
                            {
                                MessageBox.Show("Admin Login Successfully!", "Information Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainForm adminForm = new MainForm();
                                adminForm.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // Nếu không phải admin, kiểm tra user bình thường
                        string userQuery = "SELECT * FROM USERS WHERE username = @username AND password = @password";
                        using (SqlCommand cmdUser = new SqlCommand(userQuery, conn))
                        {
                            cmdUser.Parameters.AddWithValue("@username", login_username.Text.Trim());
                            cmdUser.Parameters.AddWithValue("@password", login_pass.Text.Trim());

                            using (SqlDataReader reader = cmdUser.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Session.UserId = Convert.ToInt32(reader["id"]);  // Lưu UserId
                                    Session.Username = reader["username"].ToString();
                                    MessageBox.Show("User Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Former mainForm = new Former();
                                    mainForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex.Message, "Error Message"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void signin_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
