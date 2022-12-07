using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class SelectedFeederList
    {
        public DataTable GetFeederTable( string servername,string database,string username,string password)
        {
            DataTable Dt = new DataTable();
            string connectionString = @"Data Source=" + servername +                       //Create Connection string
                    ";database=" + database +
                    ";User ID=" + username +
                    ";Password=" + password;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select distinct FeederId , Cheched , Division , Name , FeederName from TG_FEEDERLIST where Cheched='1'", conn);                                                // get the value of FeederID and Checked
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            
            Dt.Load(dr);
            return Dt;
        }
    }
}
