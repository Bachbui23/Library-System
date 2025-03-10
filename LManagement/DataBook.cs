using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LManagement
{
    public class DataBook
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        public int ID { set; get; }
        public string BookTitle { set; get; }
        public string Author { set; get; }
        public string Pulished { set; get; }
        public int Quantity {  set; get; }
        public string images { set; get; }
        public string Status { set; get; }
        public int CategoryID { set; get; }

        public List<DataBook> addBooksData()
        {
            List<DataBook> listData = new List<DataBook>();

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open(); // Mở kết nối đúng cách

                string selectData = "SELECT * FROM BOOKS WHERE date_delete IS NULL";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DataBook dab = new DataBook();
                        dab.ID = reader.GetInt32(reader.GetOrdinal("id"));
                        dab.BookTitle = reader["book_title"]?.ToString() ?? "N/A";
                        dab.Author = reader["author"]?.ToString() ?? "N/A";
                        dab.Pulished = reader["published_date"] != DBNull.Value ? Convert.ToDateTime(reader["published_date"]).ToString("yyyy-MM-dd") : "N/A";
                        dab.Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0;
                        dab.images = reader["images"]?.ToString() ?? "";
                        dab.Status = reader["status"]?.ToString() ?? "N/A";
                        dab.CategoryID = reader["category_id"] != DBNull.Value ? Convert.ToInt32(reader["category_id"]) : -1; // -1 nếu NULL

                        listData.Add(dab);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting Database: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close(); // Đảm bảo đóng kết nối
            }

            return listData;
        }
    }
}
