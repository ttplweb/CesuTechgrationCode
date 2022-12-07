using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class gettab
    {
        string countor1 = string.Empty;
        public string tab(string server, string database, string user, string pwd)
        {
            SqlConnection con = new SqlConnection(@"Server=" + server + "; uid=" + user + "; Password=" + pwd + "; database=" + database);
            SqlCommand cmd;
            SqlDataReader dr;
            cmd = new SqlCommand("select distinct Circle ,Division,Name,FeederId , Cheched from TG_FEEDERLIST where Cheched='1'", con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(dr);
            if (dt3 != null)
            {
                countor1 = dt3.Rows.Count.ToString();
            }
            return (countor1);
        }

    }
}
