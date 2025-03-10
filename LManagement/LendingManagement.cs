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

namespace LManagement
{
    public partial class LendingManagement : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        public LendingManagement()
        {
            InitializeComponent();
            LoadUsers();
            SetupDataGridView();
        }
        private void LoadUsers()
        {
            try
            {
                conn.Open();
                string query = "SELECT id, username FROM USERS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbUsers.DataSource = dt;
                cmbUsers.DisplayMember = "username";
                cmbUsers.ValueMember = "id";
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
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue != null && int.TryParse(cmbUsers.SelectedValue.ToString(), out int userId))
            {
                LoadBorrowedBooks(userId);
            }
        }
        private void LoadBorrowedBooks(int userId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"
            SELECT br.id AS BorrowID, b.id AS BookID, b.book_title, br.borrow_date, br.due_date 
            FROM Borrow_Return br
            JOIN BOOKS b ON br.book_id = b.id
            WHERE br.user_id = @userId AND br.status = 'Lending'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvBorrowedBooks.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book list: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void SetupDataGridView()
        {
            dgvBorrowedBooks.AutoGenerateColumns = false;
            dgvBorrowedBooks.Columns.Clear();

            dgvBorrowedBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "BorrowID", // Phải khớp với SQL
                Name = "BorrowID",
                Visible = false
            });

            dgvBorrowedBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Book title",
                DataPropertyName = "book_title",
                Name = "BookTitle"
            });

            dgvBorrowedBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Borrow date",
                DataPropertyName = "borrow_date",
                Name = "BorrowDate"
            });

            dgvBorrowedBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Due date",
                DataPropertyName = "due_date",
                Name = "DueDate"
            });

            DataGridViewButtonColumn returnButton = new DataGridViewButtonColumn
            {
                HeaderText = "Act",
                Text = "Return",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dgvBorrowedBooks.Columns.Add(returnButton);
            dgvBorrowedBooks.CellClick += dgvBorrowedBooks_CellClick;
        }


        private void dgvBorrowedBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 1 && dgvBorrowedBooks.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (!dgvBorrowedBooks.Columns.Contains("BorrowID"))
                {
                    MessageBox.Show("Error: BorrowID column not found in DataGridView.");
                    return;
                }

                object value = dgvBorrowedBooks.Rows[e.RowIndex].Cells["BorrowID"].Value;

                if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
                {
                    MessageBox.Show("Giá trị BorrowID không hợp lệ!");
                    return;
                }

                int borrowId = Convert.ToInt32(value);

                if (MessageBox.Show("Are you sure you want to return the book?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ReturnBook(borrowId);
                    LoadBorrowedBooks(Convert.ToInt32(cmbUsers.SelectedValue)); // Load lại danh sách
                }
            }
        }
        private void ReturnBook(int borrowId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Kiểm tra trạng thái mượn sách
                string checkQuery = "SELECT book_id, status FROM Borrow_Return WHERE id = @borrowId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@borrowId", borrowId);
                SqlDataReader reader = checkCmd.ExecuteReader();

                int bookId = -1;
                string status = "";

                if (reader.Read())
                {
                    bookId = reader.GetInt32(0);
                    status = reader.GetString(1);
                }
                reader.Close();

                if (status == "Returned")
                {
                    MessageBox.Show("This book has already been returned.");
                    return;
                }

                // Cập nhật trạng thái mượn
                string updateQuery = "UPDATE Borrow_Return SET return_date = GETDATE(), status = 'Returned' WHERE id = @id";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@id", borrowId);
                cmd.ExecuteNonQuery();

                // Cập nhật lại số lượng sách trong bảng BOOKS
                string updateBookQuery = "UPDATE BOOKS SET Quantity = Quantity + 1 WHERE id = @book_id";
                SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, conn);
                updateBookCmd.Parameters.AddWithValue("@book_id", bookId);
                updateBookCmd.ExecuteNonQuery();

                MessageBox.Show("Returned book successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error returning book: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
}