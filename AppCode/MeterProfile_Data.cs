using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;
using TechGration.AppCode;
using System.Globalization;

namespace TechGration
{
    class MeterProfile_Data
    {
        //MeterProfile Data.......Nik
        private Process process;
        public void InsertProfileDataTable(string Fromdate, string Todate, string Fordate, ConfigFileData cf, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)       // Insert Profile_Data Table 
        {
            #region
            try
            {
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ProStatus = "Reading MeterData.....";


                string filePath = cf.Meter_PATH;

                // Step 1: Load CSV into DataTable
                DataTable dataTable = new DataTable();


                // Read all lines from the CSV
                var lines = File.ReadAllLines(filePath);

                if (lines.Length == 0) return;

                // Assume first line is header
                var headers = lines[0].Split(',');
                foreach (var header in headers)
                {
                    dataTable.Columns.Add(header.Trim());
                }

                // Add data rows
                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    if (values.Length == headers.Length) // Ensure row has correct number of columns
                    {
                        dataTable.Rows.Add(values);
                    }
                }

                incrementFeeder = 10;
                completepersentag += incrementFeeder;


                string servername = cf.Gisservername;
                string dbname = cf.GisDatabase;
                string userid = cf.Gisusername;
                string password = cf.Gispassword;

                string Databackup = @"Data Source=" + servername +                       //Create Connection string
                      ";database=" + dbname +
                      ";User ID=" + userid +
                      ";Password=" + password;



                string tableName = "TableMeterFeederData"; // <-- yahan apna table naam daaliye


                // Step 2: Insert into SQL Server using SqlBulkCopy
                using (SqlConnection connection = new SqlConnection(Databackup))
                {
                    connection.Open();
                    string sql = "TRUNCATE TABLE " + tableName;

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rawDate = row["DAYPROFILE_DATE"]?.ToString().Trim();
                        string format = "dd-MM-yyyy HH:mm";

                        if (DateTime.TryParseExact(rawDate, format,
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None,
                                                   out DateTime parsedDate))
                        {
                            row["DAYPROFILE_DATE"] = parsedDate;  // Correct DateTime
                        }
                        else
                        {
                            //row["DAYPROFILE_DATE"] = new DateTime(1900, 1, 1); // fallback
                        }
                    }


                    //dataTable.Columns.Remove("DAYPROFILE_DATE");
                    //dataTable.Columns["DAYPROFILE_DATE"].ColumnName = "DAYPROFILE_DATE";

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.WriteToServer(dataTable);
                    }

                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                CreateProfileTextFile(Fromdate, Todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);
                
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
            }
            catch (Exception ex)
            {
                // DE.Error_log(ex.ToString());
                //MessageBox.Show("Error : " + ex);
            }

            #endregion
        }
        public void CreateProfileTextFile(string Fromdate, string Todate, string Fordate, ConfigFileData cf, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)   // Create Profile_Data NetworkText File ........
        {
            #region
            try
            {
                ProStatus = "Create MeterProfileData Text.....";
                if (!string.IsNullOrEmpty(Fordate))
                {

                    DataTable dttd = new DataTable();
                    string connectionString = @"Data Source=" + cf.Gisservername +
                                         ";Database=" + cf.GisDatabase +
                                         ";User ID=" + cf.Gisusername +
                                         ";Password=" + cf.Gispassword;

                    // Step 2: Define your SQL query
                    string query = "SELECT * FROM " + cf.GisDatabase + ".[dbo].[TableMeterFeederData] WHERE CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' AND DATEPART(MINUTE, DAYPROFILE_DATE) = 0 ORDER BY METER_SR_NO, DAYPROFILE_DATE, INTERVAL";  // Table ka naam yahan daalein

                    // Step 3: Use SqlDataAdapter to fill DataTable
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {

                            adapter.Fill(dttd);  // Fill the DataTable with query result
                        }

                        string MeterData_path = GETFILE + "\\MeterData File\\MeterProfileData.txt";
                        if (Directory.Exists(MeterData_path))
                        {
                            System.IO.Directory.Delete(MeterData_path, true);
                        }
                        StreamWriter tw1 = File.CreateText(MeterData_path);
                        try
                        {
                            int daay = 0;
                            int k = 0;
                            int V = 0;
                            int cc = 0;
                            string PF = string.Empty;
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;
                            string METER_SR_NO111 = string.Empty;
                            string Dayy = string.Empty;

                            string Day = string.Empty;
                            string MM = string.Empty;
                            string yearString = string.Empty;


                            string str = @"[PROFILE_VALUES]
FORMAT=ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,NETWORKID,YEAR,MONTH,DAY,UNIT,PHASE,VALUES";
                            tw1.WriteLine(str);
                            int counting = 0;
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string SERIAL_NUMBER = dttd.Rows[i]["METER_SR_NO"].ToString();
                                string NETWORKID = dttd.Rows[i]["FEEDER_CODE"].ToString();
                                string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
                                string INTERVAL = dttd.Rows[i]["INTERVAL"].ToString();
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["METER_MF"].ToString());
                                string SYS_PF = dttd.Rows[i]["PF"].ToString();
                                string PROFILETYPE = "METER";
                                string INTERVALFORMAT = "8760HOURS";
                                string TIMEINTERVAL = "HOUR";
                                string GLOBALUNIT = "AMP-PF";

                                DateTime date;
                                if (DateTime.TryParse(DayProfileDate, out date))
                                {
                                    int day = date.Day;        // Extracts the day
                                    int month = date.Month;    // Extracts the month
                                    int year = date.Year;      // Extracts the year

                                    Day = day.ToString();
                                    string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                                    MM = Months[month - 1];
                                    yearString = year.ToString();

                                }

                                if (counting == i)
                                {
                                    while (k < 24)
                                    {
                                        int n = 100;
                                        int n1 = METER_MF;
                                        string PF1 = dttd.Rows[k + i]["PF"].ToString();

                                        if (PF1 == "")
                                        {
                                            PF += "0" + ",";
                                        }
                                        else
                                        {

                                            PF += Convert.ToString(PF1) + ",";
                                        }


                                        string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                        if (RPH_LCURR1 == "")
                                        {
                                            RPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                            RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";
                                        }


                                        string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                        if (YPH_LCURR1 == "")
                                        {
                                            YPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                            YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";

                                        }


                                        string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                        if (BPH_LCURR1 == "")
                                        {
                                            BPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                            BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";
                                        }



                                        k++;
                                        counting++;
                                    }
                                    PF = PF.TrimEnd(',');
                                    RPH_LCURR = RPH_LCURR.TrimEnd(',');
                                    YPH_LCURR = YPH_LCURR.TrimEnd(',');
                                    BPH_LCURR = BPH_LCURR.TrimEnd(',');

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "A");
                                    tw1.Write("," + PF);

                                    tw1.WriteLine();
                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "A");
                                    tw1.Write("," + RPH_LCURR);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "B");
                                    tw1.Write("," + PF);

                                    tw1.WriteLine();
                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "B");
                                    tw1.Write("," + YPH_LCURR);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "C");
                                    tw1.Write("," + PF);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "C");
                                    tw1.Write("," + BPH_LCURR);
                                    tw1.WriteLine();
                                    //tw1.Close();
                                    k = 0;

                                }

                                PF = string.Empty;
                                RPH_LCURR = string.Empty;
                                YPH_LCURR = string.Empty;
                                BPH_LCURR = string.Empty;

                            }
                            tw1.Close();

                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.Message);
                            tw1.Close();
                        }

                        ProStatus = "MeterProfileText File Completed.....";
                    }

                }

                else
                {

                    DataTable dttd = new DataTable();
                    string connectionString = @"Data Source=" + cf.Gisservername +
                                         ";Database=" + cf.GisDatabase +
                                         ";User ID=" + cf.Gisusername +
                                         ";Password=" + cf.Gispassword;

                    // Step 2: Define your SQL query
                    string query = "SELECT * FROM "+ cf.GisDatabase + ".[dbo].[TableMeterFeederData]  WHERE CAST(DAYPROFILE_DATE AS DATE) BETWEEN '" + Fromdate + "' AND '" + Todate + "' AND DATEPART(MINUTE, DAYPROFILE_DATE) = 0 ORDER BY METER_SR_NO, DAYPROFILE_DATE, INTERVAL";  // Table ka naam yahan daalein

                    // Step 3: Use SqlDataAdapter to fill DataTable
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {

                            adapter.Fill(dttd);  // Fill the DataTable with query result
                        }


                        string MeterData_path = GETFILE + "\\MeterData File\\MeterProfileData.txt";
                        if (Directory.Exists(MeterData_path))
                        {
                            System.IO.Directory.Delete(MeterData_path, true);
                        }
                        StreamWriter tw1 = File.CreateText(MeterData_path);
                        try
                        {
                            int daay = 0;
                            int k = 0;
                            int V = 0;
                            int cc = 0;
                            string PF = string.Empty;
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;
                            string METER_SR_NO111 = string.Empty;
                            string Dayy = string.Empty;

                            string Day = string.Empty;
                            string MM = string.Empty;
                            string yearString = string.Empty;


                            string str = @"[PROFILE_VALUES]
FORMAT=ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,NETWORKID,YEAR,MONTH,DAY,UNIT,PHASE,VALUES";
                            tw1.WriteLine(str);
                            int counting = 0;
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string SERIAL_NUMBER = dttd.Rows[i]["METER_SR_NO"].ToString();
                                string NETWORKID = dttd.Rows[i]["FEEDER_CODE"].ToString();
                                string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
                                string INTERVAL = dttd.Rows[i]["INTERVAL"].ToString();
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["METER_MF"].ToString());
                                string SYS_PF = dttd.Rows[i]["PF"].ToString();
                                string PROFILETYPE = "METER";
                                string INTERVALFORMAT = "8760HOURS";
                                string TIMEINTERVAL = "HOUR";
                                string GLOBALUNIT = "AMP-PF";

                                DateTime date;
                                if (DateTime.TryParse(DayProfileDate, out date))
                                {
                                    int day = date.Day;        // Extracts the day
                                    int month = date.Month;    // Extracts the month
                                    int year = date.Year;      // Extracts the year

                                    Day = day.ToString();
                                    string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                                    MM = Months[month - 1];
                                    yearString = year.ToString();

                                }

                                if (counting == i)
                                {
                                    while (k < 24)
                                    {
                                        int n = 100;
                                        int n1 = METER_MF;
                                        string PF1 = dttd.Rows[k + i]["PF"].ToString();

                                        if (PF1 == "")
                                        {
                                            PF += "0" + ",";
                                        }
                                        else
                                        {

                                            PF += Convert.ToString(PF1) + ",";
                                        }


                                        string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                        if (RPH_LCURR1 == "")
                                        {
                                            RPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                            RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";
                                        }


                                        string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                        if (YPH_LCURR1 == "")
                                        {
                                            YPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                            YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";

                                        }


                                        string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                        if (BPH_LCURR1 == "")
                                        {
                                            BPH_LCURR += "0" + ",";
                                        }
                                        else
                                        {
                                            double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                            BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";
                                        }



                                        k++;
                                        counting++;
                                    }
                                    PF = PF.TrimEnd(',');
                                    RPH_LCURR = RPH_LCURR.TrimEnd(',');
                                    YPH_LCURR = YPH_LCURR.TrimEnd(',');
                                    BPH_LCURR = BPH_LCURR.TrimEnd(',');

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "A");
                                    tw1.Write("," + PF);

                                    tw1.WriteLine();
                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "A");
                                    tw1.Write("," + RPH_LCURR);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "B");
                                    tw1.Write("," + PF);

                                    tw1.WriteLine();
                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "B");
                                    tw1.Write("," + YPH_LCURR);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_PF");
                                    tw1.Write("," + "C");
                                    tw1.Write("," + PF);
                                    tw1.WriteLine();

                                    tw1.Write(SERIAL_NUMBER);
                                    tw1.Write("," + PROFILETYPE);
                                    tw1.Write("," + INTERVALFORMAT);
                                    tw1.Write("," + TIMEINTERVAL);
                                    tw1.Write("," + GLOBALUNIT);
                                    tw1.Write("," + NETWORKID);
                                    tw1.Write("," + yearString);
                                    tw1.Write("," + MM);
                                    tw1.Write("," + Day);
                                    tw1.Write("," + "AMPPF_AMP");
                                    tw1.Write("," + "C");
                                    tw1.Write("," + BPH_LCURR);
                                    tw1.WriteLine();
                                    //tw1.Close();
                                    k = 0;

                                }

                                PF = string.Empty;
                                RPH_LCURR = string.Empty;
                                YPH_LCURR = string.Empty;
                                BPH_LCURR = string.Empty;

                            }
                            tw1.Close();

                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.Message);
                            tw1.Close();
                        }
                        ProStatus = "MeterProfileText File Completed.....";
                    }

                }

                meterprofileBatchFileCreate(GETFILE, cf, ref incrementFeeder, ref completepersentag, ref ProStatus);
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
            #endregion
        }
        public void meterprofileBatchFileCreate(string GETFILE, ConfigFileData cf, ref int incrementFeeder, ref int completepersentag, ref string Prostatus)     // batchFile create.....
        {
            try
            {
                Prostatus = "All Batch Delete....";
                Thread.Sleep(1000);
                string bFile = string.Empty;
                string batdel = GETFILE + "\\MeterData File\\MeterProfileEimpConfig\\IMPORT.bat";
                if (File.Exists(batdel))
                {
                    File.Delete(batdel);
                }

                string batdel1 = GETFILE + "\\MeterData File\\MeterProfileEimpConfig\\IMPORT.ini";
                if (File.Exists(batdel1))
                {
                    File.Delete(batdel1);
                }
                string batdel2 = GETFILE + "\\MeterData File\\MeterProfileEimpConfig\\IMPORT.ERR";
                if (File.Exists(batdel2))
                {
                    File.Delete(batdel2);
                }

                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                string imp = GETFILE + "\\Feeder\\MeterProfileEimpConfig";
                string[] import1 = System.IO.Directory.GetFiles(imp, "*.ini");
                string cyme = GETFILE + "\\MeterData File\\MeterProfileEimpConfig";
                foreach (string item in import1)
                {

                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.ini";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@Pdbname@", cf.txtproDatabaseName);
                    text = text.Replace("@Puser@", cf.txtproUser);
                    text = text.Replace("@Ppwd@", cf.txtpropassword);
                    text = text.Replace("@Pservice@", cf.txtproServer);
                    text = text.Replace("@profiledata@", GETFILE + "\\MeterData File\\MeterProfileData.txt");
                    File.WriteAllText(TargetFile, text);
                }
                //incrementFeeder = 10;
                //completepersentag += incrementFeeder;

                string[] import2 = System.IO.Directory.GetFiles(imp, "*.bat");

                foreach (string item in import2)
                {

                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.bat";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@setpath@", cf.CymeDirectory);
                    text = text.Replace("@upload@", cyme);
                    File.WriteAllText(TargetFile, text);
                    bFile = TargetFile;
                }
                string batchpath = bFile;
                Prostatus = "Upload MeterProfileData into Cyme......";
                Thread.Sleep(200);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                importProfileBatch(batchpath, cyme);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
            }
            catch (Exception ex)
            {
                // DE.Error_log(ex.ToString());
            }
        }
        public void importProfileBatch(string path, string workdir)    // import into cyme database....
        {
            bool importfeeder = false;
            string str_Path2 = path;
            if (str_Path2 == path)
            {
                //System.Threading.Thread.Sleep(1000);
                importfeeder = true;
            }

            if (importfeeder == true)
            {
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = workdir;
                //isimport2 = true;
                proc2.Start();
                proc2.WaitForExit();

            }

        }




        //Meter_Peak_Data.......Nik
        public void InsertPeakDataTable(string Fromdate, string Todate, string Fordate, ConfigFileData cf, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)             // Insert Profile_Data Table
        {
            #region
            try
            {
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ProStatus = "Reading MeterData.....";


                string filePath = cf.Meter_PATH;

                // Step 1: Load CSV into DataTable
                DataTable dataTable = new DataTable();


                // Read all lines from the CSV
                var lines = File.ReadAllLines(filePath);

                if (lines.Length == 0) return;

                // Assume first line is header
                var headers = lines[0].Split(',');
                foreach (var header in headers)
                {
                    dataTable.Columns.Add(header.Trim());
                }

                // Add data rows
                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    if (values.Length == headers.Length) // Ensure row has correct number of columns
                    {
                        dataTable.Rows.Add(values);
                    }
                }

                incrementFeeder = 10;
                completepersentag += incrementFeeder;


                string servername = cf.Gisservername;
                string dbname = cf.GisDatabase;
                string userid = cf.Gisusername;
                string password = cf.Gispassword;

                string Databackup = @"Data Source=" + servername +                       //Create Connection string
                      ";database=" + dbname +
                      ";User ID=" + userid +
                      ";Password=" + password;



                string tableName = "TableMeterFeederData"; // <-- yahan apna table naam daaliye


                // Step 2: Insert into SQL Server using SqlBulkCopy
                using (SqlConnection connection = new SqlConnection(Databackup))
                {
                    connection.Open();
                    string sql = "TRUNCATE TABLE " + tableName;

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rawDate = row["DAYPROFILE_DATE"]?.ToString().Trim();
                        string format = "dd-MM-yyyy HH:mm";

                        if (DateTime.TryParseExact(rawDate, format,
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None,
                                                   out DateTime parsedDate))
                        {
                            row["DAYPROFILE_DATE"] = parsedDate;  // Correct DateTime
                        }
                        else
                        {
                            //row["DAYPROFILE_DATE"] = new DateTime(1900, 1, 1); // fallback
                        }
                    }


                    //dataTable.Columns.Remove("DAYPROFILE_DATE");
                    //dataTable.Columns["DAYPROFILE_DATE"].ColumnName = "DAYPROFILE_DATE";

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.WriteToServer(dataTable);
                    }

                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                CreatePeakTextFile(Fromdate, Todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
            }
            catch (Exception ex)
            {
                // DE.Error_log(ex.ToString());
                //MessageBox.Show("Error : " + ex);
            }

            #endregion

        }
        public void CreatePeakTextFile(string profile_FstartD, string profile_TendD, string Fordate, ConfigFileData TG, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)   // Create Peek_DataMeterDemandText File ........
        {
            #region
            try
            {
                ProStatus = "Create MeterData Text.....";
                if (Fordate != null)
                {
                    string servername = TG.Gisservername;
                    string dbname = TG.GisDatabase;
                    string userid = TG.Gisusername;
                    string password = TG.Gispassword;


                    string cymeservername = TG.CNetservername;
                    string cymedbname = TG.CNetdatabase;
                    string cymeuserid = TG.CNetusername;
                    string cymepassword = TG.CNetpassword;

                    string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
                          ";database=" + cymedbname +
                          ";User ID=" + cymeuserid +
                          ";Password=" + cymepassword;
                    SqlConnection cymesq = new SqlConnection(cymeDatabackup);


                    string Databackup = @"Data Source=" + servername +                       //Create Connection string
                          ";database=" + dbname +
                          ";User ID=" + userid +
                          ";Password=" + password;
                    SqlConnection sq = new SqlConnection(Databackup);
 
                    //string BB = "SELECT * FROM " + dbname + ".dbo.[MeterProfile_EXCEL] WHERE CAST(DATE_TIME AS DATE) = '" + Fordate + "' AND DATEPART(MINUTE, DATE_TIME) = 0 ORDER BY FORMAT_ID, MONTH, DAY, DATE_TIME, INTERVAL; ";
                    string BB = "SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(PF)as PF FROM "+ dbname + ".[dbo].[TableMeterFeederData] WHERE CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' GROUP BY METER_SR_NO;";
                    //string BB = "SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(KWH_READING_WITH_MF) AS KWH_READING_WITH_MF, MAX(KVAH_READING_WITH_MF) AS KVAH_READING_WITH_MF FROM " + dbname + ".[dbo].[TableMeterFeederData] WHERE CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' GROUP BY METER_SR_NO;";
                    //string BB = "SELECT* FROM " + dbname + ".[dbo].[MeterProfile_EXCEL] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' ORDER BY SERIAL_NUMBER, FEEDER_CODE, INTERVAL";
                    using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                    {
                        ProStatus = "Delete Old MeterDemandText File.....";
                        Thread.Sleep(1000);
                        string MeterData_path = GETFILE + "\\MeterData File\\MeterDemand.txt";
                        if (Directory.Exists(MeterData_path))
                        {
                            System.IO.Directory.Delete(MeterData_path, true);
                        }
                        StreamWriter tw1 = File.CreateText(MeterData_path);
                        try
                        {

                            if (sq.State == ConnectionState.Closed)
                            {
                                sq.Open();
                            }
                            cmd1.CommandTimeout = 60; // Increase timeout
                            SqlDataReader reader1 = cmd1.ExecuteReader();

                            DataTable dttd = new DataTable();
                            dttd.Load(reader1);
                             
                            Thread.Sleep(500);
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;
                            string PF = string.Empty;



                            StringBuilder builder = new StringBuilder();
                            builder.Append("[GENERAL]\r\n");
                            builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
                            builder.Append("CYME_VERSION=9.3\r\n" + "CYMDIST_REVISION=05\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
                            builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected,ReferenceTime\r\n" + "\r\n");
                            string str = builder.ToString();
                            tw1.WriteLine(str);
                            ProStatus = "Create MeterDemandText File.....";
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string METER_SR_NO = dttd.Rows[i]["METER_SR_NO"].ToString();
                                RPH_LCURR = dttd.Rows[i]["RPH_LCURR"].ToString();
                                YPH_LCURR = dttd.Rows[i]["YPH_LCURR"].ToString();
                                BPH_LCURR = dttd.Rows[i]["BPH_LCURR"].ToString();
                                PF = dttd.Rows[i]["PF"].ToString();
                                string PF1 = string.Empty;
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["METER_MF"].ToString());
                                int n = 100;
                                int n1 = METER_MF;
                                string METER_SR_NO1 = string.Empty;

                                string cymeBB = "SELECT DeviceNumber FROM " + cymedbname + ".[dbo].[CYMBREAKER] WHERE [DeviceNumber] Like'%" + METER_SR_NO + "'";
                                SqlCommand cymecmd1 = new SqlCommand(cymeBB, cymesq);
                                if (cymesq.State == ConnectionState.Closed)
                                {
                                    cymesq.Open();
                                }
                                cymecmd1.CommandTimeout = 60;
                                // Increase timeout
                                SqlDataReader cymereader1 = cymecmd1.ExecuteReader();
                                Thread.Sleep(500);
                                DataTable cymedttd = new DataTable();
                                cymedttd.Load(cymereader1);


                                if (cymedttd.Rows.Count > 0)
                                {
                                    METER_SR_NO1 = cymedttd.Rows[0]["DeviceNumber"].ToString();
                                }
                                else
                                {
                                    METER_SR_NO1 = METER_SR_NO;
                                }
                                 

                                string RPH_LCURR1 = dttd.Rows[i]["RPH_LCURR"].ToString();
                                if (RPH_LCURR1 == string.Empty)
                                {
                                    RPH_LCURR = "0";
                                }
                                else
                                {
                                    double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                    RPH_LCURR = Convert.ToString(RPH_LCURR2);
                                }

                                //Y
                                string YPH_LCURR1 = dttd.Rows[i]["YPH_LCURR"].ToString();
                                if (YPH_LCURR1 == string.Empty)
                                {
                                    YPH_LCURR = "0";
                                }
                                else
                                {
                                    double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                    YPH_LCURR = Convert.ToString(YPH_LCURR2);
                                }

                                //B
                                string BPH_LCURR1 = dttd.Rows[i]["BPH_LCURR"].ToString();
                                if (BPH_LCURR1 == string.Empty)
                                {
                                    BPH_LCURR = "0";
                                }
                                else
                                {
                                    double RPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                    BPH_LCURR = Convert.ToString(RPH_LCURR2);
                                }


                                tw1.Write("8");
                                tw1.Write("," + METER_SR_NO1);
                                tw1.Write("," + "1");
                                tw1.Write("," + RPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + YPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + BPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + "0");
                                tw1.Write("," + "0");

                                tw1.WriteLine();

                            }
                            tw1.Close();

                            reader1.Close();
                            RPH_LCURR = string.Empty;
                            YPH_LCURR = string.Empty;
                            BPH_LCURR = string.Empty;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            tw1.Close();
                        }
                        
                        ProStatus = "MeterDemandText File Completed.....";
                         
                    }
                }
                else
                {
                    string servername = TG.Gisservername;
                    string dbname = TG.GisDatabase;
                    string userid = TG.Gisusername;
                    string password = TG.Gispassword;


                    string cymeservername = TG.CNetservername;
                    string cymedbname = TG.CNetdatabase;
                    string cymeuserid = TG.CNetusername;
                    string cymepassword = TG.CNetpassword;

                    string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
                          ";database=" + cymedbname +
                          ";User ID=" + cymeuserid +
                          ";Password=" + cymepassword;
                    SqlConnection cymesq = new SqlConnection(cymeDatabackup);


                    string Databackup = @"Data Source=" + servername +                       //Create Connection string
                          ";database=" + dbname +
                          ";User ID=" + userid +
                          ";Password=" + password;
                    SqlConnection sq = new SqlConnection(Databackup);

                    
                    //string BB = "SELECT * FROM " + dbname + ".dbo.[MeterProfile_EXCEL] WHERE CAST(DATE_TIME AS DATE) = '" + Fordate + "' AND DATEPART(MINUTE, DATE_TIME) = 0 ORDER BY FORMAT_ID, MONTH, DAY, DATE_TIME, INTERVAL; ";
                    string BB = "SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(PF)as PF FROM "+ dbname + ".[dbo].[TableMeterFeederData] WHERE CAST(DAYPROFILE_DATE AS DATE) BETWEEN '" + profile_FstartD + "' AND '" + profile_TendD + "' AND DATEPART(MINUTE, DAYPROFILE_DATE) = 0 GROUP BY METER_SR_NO;";
                    using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                    {
                        ProStatus = "Delete Old MeterDemandText File.....";
                        Thread.Sleep(500);
                        string MeterData_path = GETFILE + "\\MeterData File\\MeterDemand.txt";
                        if (Directory.Exists(MeterData_path))
                        {
                            System.IO.Directory.Delete(MeterData_path, true);
                        }
                        StreamWriter tw1 = File.CreateText(MeterData_path);
                        try
                        {

                            if (sq.State == ConnectionState.Closed)
                            {
                                sq.Open();
                            }
                            cmd1.CommandTimeout = 60; // Increase timeout
                            SqlDataReader reader1 = cmd1.ExecuteReader();

                            DataTable dttd = new DataTable();
                            dttd.Load(reader1);

                            Thread.Sleep(500);
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;
                            string PF = string.Empty;



                            StringBuilder builder = new StringBuilder();
                            builder.Append("[GENERAL]\r\n");
                            builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
                            builder.Append("CYME_VERSION=9.3\r\n" + "CYMDIST_REVISION=05\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
                            builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected,ReferenceTime\r\n" + "\r\n");
                            string str = builder.ToString();
                            tw1.WriteLine(str);
                            ProStatus = "Create MeterDemandText File.....";
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string METER_SR_NO = dttd.Rows[i]["METER_SR_NO"].ToString();
                                RPH_LCURR = dttd.Rows[i]["RPH_LCURR"].ToString();
                                YPH_LCURR = dttd.Rows[i]["YPH_LCURR"].ToString();
                                BPH_LCURR = dttd.Rows[i]["BPH_LCURR"].ToString();
                                PF = dttd.Rows[i]["PF"].ToString();
                                string PF1 = string.Empty;
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["METER_MF"].ToString());
                                int n = 100;
                                int n1 = METER_MF;
                                string METER_SR_NO1 = string.Empty;

                                string cymeBB = "SELECT DeviceNumber FROM " + cymedbname + ".[dbo].[CYMBREAKER] WHERE [DeviceNumber] Like'%" + METER_SR_NO + "'";
                                SqlCommand cymecmd1 = new SqlCommand(cymeBB, cymesq);
                                if (cymesq.State == ConnectionState.Closed)
                                {
                                    cymesq.Open();
                                }
                                cymecmd1.CommandTimeout = 60;
                                // Increase timeout
                                SqlDataReader cymereader1 = cymecmd1.ExecuteReader();
                                Thread.Sleep(500);
                                DataTable cymedttd = new DataTable();
                                cymedttd.Load(cymereader1);


                                if (cymedttd.Rows.Count > 0)
                                {
                                    METER_SR_NO1 = cymedttd.Rows[0]["DeviceNumber"].ToString();
                                }
                                else
                                {
                                    METER_SR_NO1 = METER_SR_NO;
                                }


                                string RPH_LCURR1 = dttd.Rows[i]["RPH_LCURR"].ToString();
                                if (RPH_LCURR1 == string.Empty)
                                {
                                    RPH_LCURR = "0";
                                }
                                else
                                {
                                    double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                    RPH_LCURR = Convert.ToString(RPH_LCURR2);
                                }

                                //Y
                                string YPH_LCURR1 = dttd.Rows[i]["YPH_LCURR"].ToString();
                                if (YPH_LCURR1 == string.Empty)
                                {
                                    YPH_LCURR = "0";
                                }
                                else
                                {
                                    double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                    YPH_LCURR = Convert.ToString(YPH_LCURR2);
                                }

                                //B
                                string BPH_LCURR1 = dttd.Rows[i]["BPH_LCURR"].ToString();
                                if (BPH_LCURR1 == string.Empty)
                                {
                                    BPH_LCURR = "0";
                                }
                                else
                                {
                                    double RPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                    BPH_LCURR = Convert.ToString(RPH_LCURR2);
                                }


                                tw1.Write("8");
                                tw1.Write("," + METER_SR_NO1);
                                tw1.Write("," + "1");
                                tw1.Write("," + RPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + YPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + BPH_LCURR);
                                tw1.Write("," + PF);
                                tw1.Write("," + "0");
                                tw1.Write("," + "0");

                                tw1.WriteLine();

                            }
                            tw1.Close();

                            reader1.Close();
                            RPH_LCURR = string.Empty;
                            YPH_LCURR = string.Empty;
                            BPH_LCURR = string.Empty;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            tw1.Close();
                        }
                        
                        ProStatus = "MeterDemandText File Completed.....";
                     
                    }
                }
                meterPeakBatchFileCreate(GETFILE, TG, ref incrementFeeder, ref completepersentag, ref ProStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorsss" + ex);
            }

            #endregion
        }
        public void meterPeakBatchFileCreate(string GETFILE, ConfigFileData cf, ref int incrementFeeder, ref int completepersentag, ref string Prostatus)     // batchFile create.....
        {
            try
            {
                Prostatus = "All BatchFile Delete....";
                Thread.Sleep(1000);
                string bFile = string.Empty;
                string batdel = GETFILE + "\\MeterData File\\MeterPeakEimpConfig\\IMPORT.bat";
                if (File.Exists(batdel))
                {
                    File.Delete(batdel);
                }

                string batdel1 = GETFILE + "\\MeterData File\\MeterPeakEimpConfig\\IMPORT.ini";
                if (File.Exists(batdel1))
                {
                    File.Delete(batdel1);
                }
                string batdel2 = GETFILE + "\\MeterData File\\MeterPeakEimpConfig\\IMPORT.ERR";
                if (File.Exists(batdel2))
                {
                    File.Delete(batdel2);
                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                string imp = GETFILE + "\\Feeder\\MeterPeakEimpConfig";
                string[] import1 = System.IO.Directory.GetFiles(imp, "*.ini");
                string cyme = GETFILE + "\\MeterData File\\MeterPeakEimpConfig";
                foreach (string item in import1)
                {

                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.ini";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@netdbname@", cf.CNetdatabase);
                    text = text.Replace("@netuser@", cf.CNetusername);
                    text = text.Replace("@netpwd@", cf.CNetpassword);
                    text = text.Replace("@netservice@", cf.CNetservername);
                    text = text.Replace("@eqpdbname@", cf.CEqpdatabase);
                    text = text.Replace("@eqpuser@", cf.CEqpusername);
                    text = text.Replace("@eqppwd@", cf.CEqppassword);
                    text = text.Replace("@eqpservice@", cf.CEqpservername);
                    text = text.Replace("@meterdemand@", GETFILE + "\\MeterData File\\MeterDemand.txt");
                    File.WriteAllText(TargetFile, text);
                }

                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                string[] import2 = System.IO.Directory.GetFiles(imp, "*.bat");

                foreach (string item in import2)
                {

                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.bat";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@setpath@", cf.CymeDirectory);
                    text = text.Replace("@upload@", cyme);
                    File.WriteAllText(TargetFile, text);
                    bFile = TargetFile;
                }
                string batchpath = bFile;
                Prostatus = "Upload MeterDemand Data into Cyme......";
                
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                importPeakBatch(batchpath, cyme);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                 
            }
        }
        public void importPeakBatch(string path, string workdir)    // import into cyme database....
        {
            bool importfeeder = false;
            string str_Path2 = path;
            if (str_Path2 == path)
            {
                //System.Threading.Thread.Sleep(1000);
                importfeeder = true;
            }

            if (importfeeder == true)
            {
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = workdir;
                //isimport2 = true;
                proc2.Start();
                proc2.WaitForExit();

                 
            }

        }
         
    }
}
