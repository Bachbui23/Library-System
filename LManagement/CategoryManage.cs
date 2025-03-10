using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LManagement
{
    public partial class CategoryManage : UserControl
    {
        public CategoryManage()
        {
            InitializeComponent();
            displayBooks();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        private String imagePath;
        public void clearFields()
        {
            category_title.Text = "";
            category_picture.Image = null;
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayBooks();
        }
        public void displayBooks()
        {
            DataCategory dab = new DataCategory();
            List<DataCategory> listData = dab.CateMane();

            dataGridView1.DataSource = listData;

        }
        private int CateID = 0;
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    CateID = Convert.ToInt32(row.Cells[0].Value);
                    category_title.Text = row.Cells[1].Value.ToString();

                    string imagePath = row.Cells[2].Value?.ToString();
                    Console.WriteLine("Đường dẫn ảnh: " + imagePath);
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        category_picture.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        category_picture.Image = null;
                        Console.WriteLine("Không tìm thấy ảnh!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(category_title.Text) || category_picture.Image == null)
            {
                MessageBox.Show("Please fill all blank fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                string path = Path.Combine(@"C:\Users\bachb\OneDrive\Desktop\LManagement\LManagement\Cate_picture", category_title.Text + ".jpg");

                string directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    File.Copy(imagePath, path, true);
                }

                string insertData = "INSERT INTO Category (category_title, image) VALUES(@catename, @image_cate)";
                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                {
                    cmd.Parameters.AddWithValue("@catename", category_title.Text.Trim());
                    cmd.Parameters.AddWithValue("@image_cate", path);
                    cmd.ExecuteNonQuery();
                }

                displayBooks();
                MessageBox.Show("Added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (CateID == 0 || string.IsNullOrEmpty(category_title.Text))
            {
                MessageBox.Show("Please select an item first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                string updateData = "UPDATE Category SET category_title = @catename WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(updateData, conn))
                {
                    cmd.Parameters.AddWithValue("@catename", category_title.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", CateID);
                    cmd.ExecuteNonQuery();
                }

                displayBooks();
                MessageBox.Show("Updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (CateID == 0)
            {
                MessageBox.Show("Please select an item first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show("Are you sure you want to DELETE Category ID: " + CateID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string deleteData = "DELETE FROM Category WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", CateID);
                        cmd.ExecuteNonQuery();
                    }

                    displayBooks();
                    MessageBox.Show("Deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void addCate_pic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    category_picture.Image = Image.FromFile(imagePath);
                }
            }
        }
    }
}
