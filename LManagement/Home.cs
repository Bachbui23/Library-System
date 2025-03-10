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
using System.Net;

namespace LManagement
{
    public partial class Home : UserControl
    {
        private string connectionString = "Server=.;Database=nickgirl;Integrated Security=True";
        public event Action<int> BookSelected; // Đổi sang int
        private FlowLayoutPanel flowLayoutPanelBook = new FlowLayoutPanel();
        public Home()
        {
            InitializeComponent();
            LoadBooks();
        }
        private void LoadBooks()
        {
            flowLayoutPanelBook.Controls.Clear();

            string query = "SELECT id, book_title, images FROM BOOKS";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = Convert.ToInt32(reader["id"]);
                            string title = reader["book_title"].ToString();
                            string imagePath = reader["images"].ToString();

                            Image coverImage = null;
                            if (File.Exists(imagePath)) // Kiểm tra nếu file ảnh tồn tại
                            {
                                coverImage = Image.FromFile(imagePath);
                            }
                            BookItem bookItem = new BookItem(bookId, title, coverImage);
                            bookItem.Clicked += (s, e) => BookSelected?.Invoke(bookId);
                            flowLayoutPanelBooks.Controls.Add(bookItem);
                        }
                    }
                }
            }
        }
    }
}
