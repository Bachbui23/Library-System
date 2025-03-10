using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LManagement
{
    public partial class UserManagement : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        private int userid;
        public UserManagement()
        {
            InitializeComponent();
            LoadUsers();
            LoadGender();
            LoadStatus();
        }
        private void LoadGender()
        {
            cmbgender.Items.Clear();
            cmbgender.Items.Add("Male");
            cmbgender.Items.Add("Female");
            cmbgender.SelectedIndex = 0;
        }
        private void LoadStatus()
        {
            cmbstatus.Items.Clear();
            cmbstatus.Items.Add("Active");
            cmbstatus.Items.Add("Inactive");
            cmbstatus.Items.Add("Banned");
            cmbstatus.SelectedIndex = 0;
        }
        private void LoadUsers()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM USERS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datauser.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bookIssue_addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO USERS (email, username, password, PhoneNumber, Address, Gender, Status, CCCD) " +
                               "VALUES (@Email, @Username, @Password, @PhoneNumber, @Address, @Gender, @Status, @CCCD)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", useremail.Text);
                cmd.Parameters.AddWithValue("@Username", username.Text);
                cmd.Parameters.AddWithValue("@Password", "default_password"); // Mật khẩu mặc định hoặc có thể thêm textbox password
                cmd.Parameters.AddWithValue("@PhoneNumber", phonenumber.Text);
                cmd.Parameters.AddWithValue("@Address", address.Text);
                cmd.Parameters.AddWithValue("@Gender", cmbgender.Text);
                cmd.Parameters.AddWithValue("@Status", cmbstatus.Text);
                cmd.Parameters.AddWithValue("@CCCD", id.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User added successfully!");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bookIssue_updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "UPDATE USERS SET email=@Email, username=@Username, password=@Password, PhoneNumber=@PhoneNumber, " +
                               "Address=@Address, Gender=@Gender, Status=@Status, CCCD=@CCCD WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userid);
                cmd.Parameters.AddWithValue("@Email", useremail.Text);
                cmd.Parameters.AddWithValue("@Username", username.Text);
                cmd.Parameters.AddWithValue("@Password", "default_password"); // Có thể cho phép thay đổi password
                cmd.Parameters.AddWithValue("@PhoneNumber", phonenumber.Text);
                cmd.Parameters.AddWithValue("@Address", address.Text);
                cmd.Parameters.AddWithValue("@Gender", cmbgender.Text);
                cmd.Parameters.AddWithValue("@Status", cmbstatus.Text);
                cmd.Parameters.AddWithValue("@CCCD", id.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User updated successfully!");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bookIssue_deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM USERS WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userid);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User deleted successfully!");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bookIssue_clearBtn_Click(object sender, EventArgs e)
        {
            userid = 0;
            id.Clear();
            username.Clear();
            phonenumber.Clear();
            address.Clear();
            useremail.Clear();
            cmbgender.SelectedIndex = 0;
            cmbstatus.SelectedIndex = 0;
        }

        private void datauser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datauser.Rows[e.RowIndex];
                userid = Convert.ToInt32(row.Cells[0].Value);
                useremail.Text = row.Cells[1].Value.ToString();
                username.Text = row.Cells[2].Value.ToString();
                phonenumber.Text = row.Cells[3].Value.ToString();
                address.Text = row.Cells[4].Value.ToString();
                cmbgender.Text = row.Cells[7].Value.ToString();
                cmbstatus.Text = row.Cells[6].Value.ToString();
                id.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
