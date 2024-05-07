using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace restaurant
{
    class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private DataTable dt;
        private SqlDataAdapter Sda;
        private string ConStr;


        public Functions()
        {
            ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\source\repos\restaurant\restaurant\RestaurantDb.mdf;Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(ConStr);
            Cmd = new SqlCommand();
            Cmd.Connection = Con;


        }

        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            Sda = new SqlDataAdapter(Query , ConStr);
            Sda.Fill(dt);
            return dt;
        }

        public int SetData(string Query)
        {
            int Cnt = 0;
            if (Con.State== ConnectionState.Closed)
            {
                Con.Open();
            }
            Cmd.CommandText = Query;
            Cnt = Cmd.ExecuteNonQuery();
            Con.Close();
            return Cnt;
        }
        public int SetData(string Query, string category, string desc)
        {
            int rowsAffected = 0;
            using (SqlCommand cmd = new SqlCommand(Query, Con))
            {
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Desc", desc);

                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                rowsAffected = cmd.ExecuteNonQuery();
                Con.Close();
            }

            return rowsAffected;
        }



        public int SetData(string Query, int categoryId)
        {
            int rowsAffected = 0;
            using (SqlCommand cmd = new SqlCommand(Query, Con))
            {
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                rowsAffected = cmd.ExecuteNonQuery();
                Con.Close();
            }

            return rowsAffected;
        }



    }
}
