using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LManagement
{
    public partial class BookManage : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        private int bookID = 0;
        private string imagePath;
        public BookManage()
        {
            InitializeComponent();
            LoadCategories();
            DisplayBooks();
        }
        public void clearFields()
        {
            bookTitle.Text = "";
            author.Text = "";
            published.Value = DateTime.Today;
            category.SelectedIndex = -1;
            quantity.Text = "";
            status.Text = "";
            Books_picture.Image = null;
            bookID = 0;
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            DisplayBooks();
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        private void LoadCategories()
        {
            try
            {
                conn.Open();
                string query = "SELECT id, category_title FROM Category";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                category.DataSource = dt;
                category.DisplayMember = "category_title";
                category.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void DisplayBooks()
        {
            DataBook dab = new DataBook();
            List<DataBook> listData = dab.addBooksData();

            dataGridView.DataSource = null;
            dataGridView.AutoGenerateColumns = true; 
            dataGridView.DataSource = listData;
            dataGridView.Refresh();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                bookID = Convert.ToInt32(row.Cells[0].Value);
                bookTitle.Text = row.Cells[1].Value.ToString();
                author.Text = row.Cells[2].Value.ToString();
                published.Value = Convert.ToDateTime(row.Cells[3].Value);
                quantity.Text = row.Cells[4].Value.ToString();
                category.Text = row.Cells[7].Value.ToString();
                status.Text = row.Cells[6].Value.ToString();
                string imagePath = row.Cells[5].Value?.ToString();

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    Books_picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    Books_picture.Image = null;
                }
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bookTitle.Text) || string.IsNullOrEmpty(author.Text) ||
                category.SelectedValue == null || Books_picture.Image == null)
            {
                MessageBox.Show("Please fill all required fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                string path = Path.Combine(@"C:\Users\bachb\OneDrive\Desktop\LManagement\LManagement\Book_Pic\", bookTitle.Text + ".jpg");

                string directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    File.Copy(imagePath, path, true);
                }

                string insertData = "INSERT INTO BOOKS (book_title, author, Quantity, published_date, category_id, status, images) " +
                                    "VALUES(@title, @author, @Quantity, @published, @category, @status, @images)";
                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                {
                    cmd.Parameters.AddWithValue("@title", bookTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@author", author.Text.Trim());
                    cmd.Parameters.AddWithValue("@published", published.Value);
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(quantity.Text));
                    cmd.Parameters.AddWithValue("@category", category.SelectedValue);
                    cmd.Parameters.AddWithValue("@status", status.Text.Trim());
                    cmd.Parameters.AddWithValue("@images", path);
                    cmd.ExecuteNonQuery();
                }

                DisplayBooks();
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (bookID == 0 || string.IsNullOrEmpty(bookTitle.Text))
            {
                MessageBox.Show("Please select a book first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                string updateData = "UPDATE BOOKS SET book_title = @title, author = @author, published_date = @published, Quantity = @Quantity, category_id = @category_id, status = @status WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(updateData, conn))
                {
                    cmd.Parameters.AddWithValue("@title", bookTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@author", author.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(quantity.Text));
                    cmd.Parameters.AddWithValue("@published", published.Value);
                    cmd.Parameters.AddWithValue("@category_id", category.SelectedValue);
                    cmd.Parameters.AddWithValue("@status", status.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", bookID);
                    cmd.ExecuteNonQuery();
                }

                DisplayBooks();
                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (bookID == 0)
            {
                MessageBox.Show("Please select a book first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show("Are you sure you want to DELETE this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string deleteData = "DELETE FROM BOOKS WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", bookID);
                        cmd.ExecuteNonQuery();
                    }

                    DisplayBooks();
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void importBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    Books_picture.Image = Image.FromFile(imagePath);
                }
            }
        }
    }
}
