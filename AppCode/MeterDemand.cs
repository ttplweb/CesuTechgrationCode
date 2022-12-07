using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class MeterDemand
    {
        SqlDataReader dr;
        SqlCommand cmd;

        public string meter(string server, string database, string user, string pwd, string st)
        {
            SqlConnection con = new SqlConnection(@"Server=" + server + "; uid=" + user + "; Password=" + pwd + "; database=" + database);
            StreamWriter sw = File.AppendText(st + "\\TempChanges\\text21.txt");
            StringBuilder builder = new StringBuilder();
            builder.Append("[GENERAL]\r\n");
            builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd, yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
            builder.Append("CYME_VERSION=8.00\r\n" + "CYMDIST_REVISION=01\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
            builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected\r\n" + "\r\n");
            string str = builder.ToString();
            sw.WriteLine(str);
            try
            {
                cmd = new SqlCommand("select * from ST_MDAS_NA_DAILY_PEAK", con);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    sw.Write(Convert.ToString(dr["DeviceType"]).Trim());
                    sw.Write("," + Convert.ToString(dr["DeviceNumber"]).Trim());
                    sw.Write("," + Convert.ToString(dr["DemandType"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value1A"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value2A"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value1B"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value2B"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value1C"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Value2C"]).Trim());
                    sw.Write("," + Convert.ToString(dr["Disconnected"]).Trim());
                    sw.WriteLine();

                }
                sw.Close();
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                sw.Close();
            }


            using (StreamWriter writer = File.CreateText(st + "\\CymeTextFile\\MeterDemand.txt"))
            {
                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text21.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }
            }


            return ("1");
        }
    }
}
