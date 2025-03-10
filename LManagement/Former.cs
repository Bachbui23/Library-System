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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using static LManagement.Former;

namespace LManagement
{
    public partial class Former : Form
    {
        private string connectionString = "Server=.;Database=nickgirl;Integrated Security=True";
        private List<DataBook> Books = new List<DataBook>();
        private Panel panelBookDetail;
        public FlowLayoutPanel flowlayoutborrow = new FlowLayoutPanel();
        private Panel panelBorrowConfirm;
        private NumericUpDown numQuantity;
        private int selectedBookId;
        private int userID;
        private void SetupBorrowPanel()
        {
            panelBorrowConfirm = new Panel
            {
                Size = new Size(300, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            Label lblMessage = new Label
            {
                Text = "Nhập số lượng muốn mượn:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            numQuantity = new NumericUpDown
            {
                Location = new Point(20, 50),
                Minimum = 1,
                Maximum = 10,  // Giả sử số lượng tối đa có thể mượn
                Width = 100
            };

            Button btnConfirm = new Button
            {
                Text = "Xác nhận",
                Location = new Point(20, 100),
                Size = new Size(100, 30)
            };
            btnConfirm.Click += BtnConfirmBorrow_Click;

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(130, 100),
                Size = new Size(100, 30)
            };
            btnCancel.Click += (s, e) => panelBorrowConfirm.Visible = false;

            panelBorrowConfirm.Controls.Add(lblMessage);
            panelBorrowConfirm.Controls.Add(numQuantity);
            panelBorrowConfirm.Controls.Add(btnConfirm);
            panelBorrowConfirm.Controls.Add(btnCancel);

            this.Controls.Add(panelBorrowConfirm);
        }
        public Former()
        {
            InitializeComponent();
            LoadBooks();
            SetupUI();
            btnSearch.TextChanged += (s, e) => SearchBooks(search.Text);
            flowlayoutborrow.Visible = false;
            flowlayoutborrow.Size = new Size(1024, 643);
            flowlayoutborrow.Location = new Point(0, 105);
            flowlayoutborrow.AutoScroll = true;
            SetupBorrowPanel();
            LoadBorrowedBooks(Session.UserId);
            this.Controls.Add(flowlayoutborrow);
            this.userID = Session.UserId;
            //AddBookToUI();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanelBooks.Visible = true;
            flowlayoutborrow.Visible = false;
            userUpdate1.Visible = false;
        }
        private void LoadBooks()
        {
            Books = new DataBook().addBooksData(); // Lấy danh sách sách từ Database
            flowLayoutPanelBooks.Controls.Clear();

            foreach (var book in Books)
            {
                AddBookToUI(book);
            }
        }
        private void SetupUI()
        {
            panelBookDetail = new Panel
            {
                Location = new Point(0, 87),
                Size = new Size(1024, 660),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false  
            };
            panelBookDetail.BringToFront();
            this.Controls.Add(panelBookDetail);
        }

        private void AddBookToUI(DataBook book)
        {
            // Tạo Panel
            Panel panel = new Panel();
            panel.Name = $"PnlBook{book.ID}";
            panel.BackColor = Color.White;
            panel.Size = new Size(125, 205);
            panel.Margin = new Padding(10);
            panel.Tag = book.ID;

            // Tạo PictureBox hiển thị ảnh bìa sách
            PictureBox picBox = new PictureBox();
            picBox.Name = $"PbBookImage{book.ID}";
            picBox.Size = new Size(100, 148);
            picBox.Location = new Point(12, 10);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;

            if (!string.IsNullOrEmpty(book.images) && File.Exists(book.images))
                picBox.Image = Image.FromFile(book.images);
            picBox.Tag = book.ID;

            // Tạo Label hiển thị tên sách
            Label labelTitle = new Label();
            labelTitle.Name = $"LblBookTitle{book.ID}";
            labelTitle.Text = book.BookTitle;
            labelTitle.Location = new Point(12, 165);
            labelTitle.ForeColor = Color.Black;
            labelTitle.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelTitle.AutoSize = true;
            labelTitle.Tag = book.ID;

            // Tạo Label hiển thị tên tác giả
            Label labelAuthor = new Label();
            labelAuthor.Name = $"LblBookAuthor{book.ID}";
            labelAuthor.Text = book.Author;
            labelAuthor.Location = new Point(12, 185);
            labelAuthor.ForeColor = Color.Gray;
            labelAuthor.Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular);
            labelAuthor.AutoSize = true;
            labelAuthor.Tag = book.ID;

            // Thêm các điều khiển vào Panel
            panel.Controls.Add(picBox);
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelAuthor);

            panel.Click += new EventHandler(BookItem_Click);
            foreach (Control c in panel.Controls)
            {
                c.Click += new EventHandler(BookItem_Click);
            }


            // Thêm Panel vào FlowLayoutPanel
            flowLayoutPanelBooks.Controls.Add(panel);
        }

        private void ShowBookDetails(DataBook book)
        {
            panelBookDetail.Controls.Clear(); // Xóa nội dung cũ

            Label lblTitle = new Label
            {
                Location = new Point(290, 50),
                Size = new Size(500, 60),
                Font = new Font("Arial", 26, FontStyle.Bold),
                Text = "Book Title: " + book.BookTitle
            };

            Label lblAuthor = new Label
            {
                Location = new Point(300, 120),
                Size = new Size(330, 25),
                Font = new Font("Arial", 10),
                Text = "Author: " + book.Author
            };

            Label lblPublishedDate = new Label
            {
                Location = new Point(300, 145),
                Size = new Size(330, 25),
                Font = new Font("Arial", 10),
                Text = "Publish: " + book.Pulished
            };

            Label lblQuantity = new Label
            {
                Location = new Point(300, 170),
                Size = new Size(330, 25),
                Font = new Font("Arial", 10),
                Text = "Quantity: " + book.Quantity.ToString()
            };
            Label lblSuma = new Label
            {
                Location = new Point(300, 195),
                Size = new Size(330, 25),
                Font = new Font("Arial", 10),
                Text = "Sumary: "
            };

            PictureBox pictureBox1 = new PictureBox
            {
                Location = new Point(40, 40),
                Size = new Size(210, 270),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            if (!string.IsNullOrEmpty(book.images) && File.Exists(book.images))
            {
                pictureBox1.Image = Image.FromFile(book.images);
            }
            Button btnClose = new Button
            {
                Text = "Close",
                Location = new Point(490, 410),
                Size = new Size(100, 30)
            };
            Button btnBrr = new Button
            {
                Text = "Borrow",
                Location = new Point(590, 410),
                Size = new Size(100, 30)
            };
            btnClose.Click += (s, e) => panelBookDetail.Visible = false;
            btnBrr.Click += (s, e) =>
            {
                selectedBookId = book.ID;
                panelBorrowConfirm.Location = new Point(350, 250);
                panelBorrowConfirm.Visible = true;
                panelBorrowConfirm.BringToFront();
            };
            // Thêm controls vào panel
            panelBookDetail.Controls.Add(lblTitle);
            panelBookDetail.Controls.Add(lblAuthor);
            panelBookDetail.Controls.Add(lblPublishedDate);
            panelBookDetail.Controls.Add(lblQuantity);
            panelBookDetail.Controls.Add(pictureBox1);
            panelBookDetail.Controls.Add(btnClose);
            panelBookDetail.Controls.Add(btnBrr);
            panelBookDetail.Controls.Add(lblSuma);

            // Hiển thị panel
            panelBookDetail.Visible = true;
            panelBookDetail.BringToFront();
        }
        private void BookItem_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;

            int bookId;
            if (int.TryParse(c.Tag?.ToString(), out bookId))
            {
                DataBook selectedBook = Books.Find(x => x.ID == bookId);
                if (selectedBook != null)
                {
                    ShowBookDetails(selectedBook);
                }
                else
                {
                    MessageBox.Show("Can't find this book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SearchBooks(string keyword)
        {
            flowLayoutPanelBooks.Controls.Clear(); // Xóa danh sách cũ

            var filteredBooks = Books
                .Where(b => b.BookTitle.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            b.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (filteredBooks.Count == 0)
            {
                Label lblNotFound = new Label
                {
                    Text = "Can't find books!",
                    ForeColor = Color.Red,
                    AutoSize = true
                };
                flowLayoutPanelBooks.Controls.Add(lblNotFound);
            }
            else
            {
                foreach (var book in filteredBooks)
                {
                    AddBookToUI(book);
                }
            }
        }
        private void search_TextChanged(object sender, EventArgs e)
        {
            string keyword = ((TextBox)sender).Text;
            SearchBooks(keyword);
        }
        private void BtnConfirmBorrow_Click(object sender, EventArgs e)
        {   

            int userId = Session.UserId;  // User ID của người mượn (có thể thay đổi theo hệ thống đăng nhập)
            int quantity = (int)numQuantity.Value;  // Số lượng sách cần mượn

            try
            {
                using (SqlConnection conn = new SqlConnection("Server=.;Database=nickgirl;Integrated Security=True"))
                {
                    conn.Open();

                    // 1️⃣ Kiểm tra số lượng sách còn lại
                    string checkQuery = "SELECT Quantity FROM BOOKS WHERE id = @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", selectedBookId);

                        object result = checkCmd.ExecuteScalar();
                        int availableQuantity = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

                        Console.WriteLine("Available Quantity: " + availableQuantity);

                        if (quantity > availableQuantity)
                        {
                            MessageBox.Show("Not enough books available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 2️⃣ Bắt đầu giao dịch để đảm bảo dữ liệu đồng bộ
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 3️⃣ Chèn dữ liệu vào bảng Borrow_Return
                            string borrowQuery = @"
                    INSERT INTO Borrow_Return (user_id, book_id, borrow_date, due_date, status)
                    VALUES (@userId, @bookId, GETDATE(), DATEADD(DAY, 7, GETDATE()), 'Lending')";

                            using (SqlCommand cmd = new SqlCommand(borrowQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.Parameters.AddWithValue("@bookId", selectedBookId);
                                //cmd.Parameters.AddWithValue("@quantity", quantity);
                                cmd.ExecuteNonQuery();
                            }

                            // 4️⃣ Giảm số lượng sách trong bảng BOOKS
                            string updateBookQuery = "UPDATE BOOKS SET quantity = Quantity - @Quantity WHERE id = @bookId";
                            using (SqlCommand updateCmd = new SqlCommand(updateBookQuery, conn, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@quantity", quantity);
                                updateCmd.Parameters.AddWithValue("@bookId", selectedBookId);
                                updateCmd.ExecuteNonQuery();
                            }

                            // 5️⃣ Xác nhận giao dịch
                            transaction.Commit();
                            MessageBox.Show("Borrowed book successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 6️⃣ Cập nhật giao diện để hiển thị số lượng sách mới
                            LoadBooks();  // Gọi lại hàm load sách để cập nhật UI
                            panelBorrowConfirm.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Hoàn tác nếu có lỗi
                            MessageBox.Show("Error when borrowing book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when connecting to database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadBorrowedBooks(int userId)
        {
            flowlayoutborrow.Controls.Clear(); // Xóa danh sách cũ
            List<DataBook> borrowedBooks = new List<DataBook>();

            using (SqlConnection conn = new SqlConnection("Server=.;Database=nickgirl;Integrated Security=True"))
            {
                conn.Open();
                string query = @"SELECT B.id, B.book_title, B.author, B.images
                         FROM Borrow_Return BR 
                         JOIN BOOKS B ON BR.book_id = B.id 
                         WHERE BR.user_id = @userId AND BR.status = 'Lending'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataBook book = new DataBook
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                BookTitle = reader["book_title"].ToString(),
                                Author = reader["author"].ToString(),
                                images = reader["images"] != DBNull.Value ? reader["images"].ToString() : null
                            };
                            borrowedBooks.Add(book);
                        }
                    }
                }
            }

            foreach (var book in borrowedBooks)
            {
                Panel panel = new Panel { Width = 700, Height = 100, BorderStyle = BorderStyle.FixedSingle };
                PictureBox pic = new PictureBox { Size = new Size(60, 80), SizeMode = PictureBoxSizeMode.Zoom, Left = 5 };

                if (!string.IsNullOrEmpty(book.images) && File.Exists(book.images))
                    pic.Image = Image.FromFile(book.images);

                Label lblTitle = new Label { Text = "Book Title: " + book.BookTitle, AutoSize = true, Left = 75, Top = 10 };
                Label lblAuthor = new Label { Text = "Author: " + book.Author, AutoSize = true, Left = 75, Top = 30 };
                Button btnRemove = new Button { Text = "Delete", Left = 600, Top = 30 };
                btnRemove.Click += (s, e) => RemoveBorrowedBook(book.ID);

                panel.Controls.Add(pic);
                panel.Controls.Add(lblTitle);
                panel.Controls.Add(lblAuthor);
                panel.Controls.Add(btnRemove);

                flowlayoutborrow.Controls.Add(panel);
            }
        }

        private void RemoveBorrowedBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection("Server=.;Database=nickgirl;Integrated Security=True"))
            {
                conn.Open();

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Are you sure you want to delete this borrowed book?", "Confirm Delete",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes) // Nếu người dùng chọn "Yes"
                {
                    using (SqlTransaction transaction = conn.BeginTransaction()) // Bắt đầu transaction
                    {
                        try
                        {
                            // Xóa sách khỏi bảng Borrow_Return
                            string deleteQuery = "DELETE FROM Borrow_Return WHERE book_id = @bookId AND user_id = @userId";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@bookId", bookId);
                                deleteCmd.Parameters.AddWithValue("@userId", Session.UserId);
                                deleteCmd.ExecuteNonQuery();
                            }

                            // Tăng lại số lượng sách trong bảng BOOKS
                            string updateQuery = "UPDATE BOOKS SET quantity = quantity + 1 WHERE id = @bookId";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@bookId", bookId);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Xác nhận transaction
                            transaction.Commit();

                            MessageBox.Show("Deleted book from borrowed list and restored quantity!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại danh sách sách đã mượn
                            LoadBorrowedBooks(Session.UserId);

                            // Cập nhật lại danh sách sách trong hệ thống để hiển thị số lượng mới
                            LoadBooks();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Hoàn tác nếu có lỗi
                            MessageBox.Show("Error removing book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    // Nếu chọn "No", không làm gì cả
                    MessageBox.Show("Cancelled book removal.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowlayoutborrow.Visible = true;
            LoadBorrowedBooks(Session.UserId);
            flowlayoutborrow.BringToFront();
            flowLayoutPanelBooks.Visible = false;
        }

        private void userbtn_Click(object sender, EventArgs e)
        {
            flowlayoutborrow.Visible = false;
            flowLayoutPanelBooks.Visible = false;
            userUpdate1.Visible = true;
        }
    }
}