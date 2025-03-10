using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LManagement
{
    internal class DataCategory
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MarryJane;Initial Catalog=nickgirl;Integrated Security=True");
        public int ID { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
        public List<DataCategory> CateMane()
        {
            List<DataCategory> listData = new List<DataCategory>();

            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();

                    string selectData = "SELECT * FROM Category";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            DataCategory dab = new DataCategory();
                            dab.ID = (int)reader["id"];
                            dab.Name = reader["category_title"].ToString();
                            dab.Images = reader["image"].ToString();

                            listData.Add(dab);
                        }

                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error conenecting Database: " + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return listData;
        }
    }
}
