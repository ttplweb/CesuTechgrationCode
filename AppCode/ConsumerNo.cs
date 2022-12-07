using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class ConsumerNo
    {
        SqlDataReader dr, dr1;
        SqlCommand cmd, cmd1;

        public string sql(ConfigFileData cf, string st)
        {
            string DataPath = st + ConfigurationManager.AppSettings["connection"];
            SqlConnection con = new SqlConnection(@"Server=" + cf.Gisservername + "; uid=" + cf.Gisusername + "; Password=" + cf.Gispassword + "; database=" + cf.GisDatabase);
            string mainPath = st + "\\GISPGDB\\load.log";
            if (File.Exists(mainPath))
            {
                File.Delete(mainPath);
            }
            StreamWriter read = File.AppendText(mainPath);
            string connectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataPath;

            try
            {
                DataTable DT = new DataTable();
                string nn = "select CustomerNumber , CustomerType from TG_CUSTOMERLOADS";
                using (OleDbConnection connection3 = new OleDbConnection(connectionstring))
                {
                    OleDbCommand command3 = new OleDbCommand(nn, connection3);
                    try
                    {
                        connection3.Open();
                        OleDbDataReader dr1 = command3.ExecuteReader();
                        DT.Load(dr1);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                string consumerList = string.Empty;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    consumerList += DT.Rows[i][0].ToString() + "', '";
                }

                consumerList = consumerList.Remove(consumerList.Length - 3);
                DataTable dt = new DataTable();
                string query = "select SanctionedLoad , ConsumerNumber , MeterReading from [" + cf.GisSchema_Name + "].[CONSUMERMETER] where ConsumerNumber IN ('" + consumerList + ");";
                cmd = new SqlCommand(query, con);
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);

                con.Close();

                DataTable DT2 = new DataTable();
                DataView view = new DataView(DT);
                view.Sort = "CustomerType";
                view.RowFilter = "CustomerType = 'INDUSTRIAL'";
                DataTable DT1 = new DataTable();
                DT1 = view.ToTable();

                string industry = string.Empty;
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    industry += DT1.Rows[i][0].ToString() + "', '";
                }
                if (industry != "")
                {
                    industry = industry.Remove(industry.Length - 3);
                    string query1 = "select SanctionedLoad , ConsumerNumber from [" + cf.GisSchema_Name + "].[CONSUMERMETER] where ConsumerNumber IN ('" + industry + ");";
                    cmd1 = new SqlCommand(query1, con);
                    con.Open();
                    dr = cmd1.ExecuteReader();
                    DT2.Load(dr);
                    con.Close();
                }

                using (OleDbConnection connection3 = new OleDbConnection(connectionstring))
                {
                    connection3.Open();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string str = dt.Rows[i][0].ToString();
                        string value = (float.Parse(str.ToString()) / float.Parse(cf.RES.ToString())).ToString();

                        string str1 = dt.Rows[i][1].ToString();
                        string str2 = dt.Rows[i][2].ToString();
                        string mm = "update TG_CUSTOMERLOADS SET ConnectedKVA='" + value + "', KWH='" + str2 + "' WHERE CustomerNumber='" + str1 + "'";
                        OleDbCommand command3 = new OleDbCommand(mm, connection3);
                        try
                        {
                            int k = command3.ExecuteNonQuery();
                            read.WriteLine("mm=" + mm + "*k=" + k);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    for (int i = 0; i < DT2.Rows.Count; i++)
                    {
                        string str = DT2.Rows[i][0].ToString();
                        string value = (float.Parse(str.ToString()) / float.Parse(cf.IND.ToString())).ToString();

                        string str1 = DT2.Rows[i][1].ToString();
                        string mm = "update TG_CUSTOMERLOADS SET ConnectedKVA='" + value + "' WHERE CustomerNumber='" + str1 + "'";

                        OleDbCommand command3 = new OleDbCommand(mm, connection3);
                        try
                        {
                            int k = command3.ExecuteNonQuery();
                            read.WriteLine("mmLast=" + mm + "*Last k=" + k);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }

                if (read != null)
                {
                    read.Close();
                }
                return ("1");
            }
            catch (Exception ex)
            {
                return ("connection Problem");
            }

        }


    }
}
