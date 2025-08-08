using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class SelectedFeederList
    {
        public DataTable GetFeederTable( string ptah)
        {
            DataTable Dt = new DataTable();
            string mdbpath =  ptah + ConfigurationManager.AppSettings["Connection1"].ToString();
         
            string connection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
            //string connectionString = @"Data Source=" + servername +                       //Create Connection string
            //        ";database=" + database +
            //        ";User ID=" + username +
            //        ";Password=" + password;
            OleDbConnection conn = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand("select distinct NetworkID as FeederId, Cheched , Group2 as Division ,Group1 as Name , NetworkName as FeederName from FeederList where Cheched=1", conn);

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OleDbDataReader dr = cmd.ExecuteReader();         
            Dt.Load(dr);
            conn.Close();
            return Dt;
        }
    }
}
