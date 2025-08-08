
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

namespace TechGration.AppCode
{

    class MeterProfile_DataAPI
    {
       // TechgrationConfiguration DE = new TechgrationConfiguration();

        private Process process;

        public Process Process { get; private set; }

        public void ProfileData(string profile_FstartD, string profile_TendD, string Fordate, ConfigFileData CFD, String GETPATH, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)
        {
            try
            {
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ProStatus = "Get Data From API....";
                Thread.Sleep(1000);
                bool data = true;
                if (profile_FstartD != null && profile_TendD != null)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.176:8077/api/Home/MeterProfile");
                    var content = new StringContent("{\r\n  \"StartDate\": \"" + profile_FstartD + "\",\r\n  \"EndDate\": \"" + profile_TendD + "\"\r\n}", null, "application/json");
                    request.Content = content;
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    // Send the request synchronously
                    var response1 = client.SendAsync(request).Result; // Use .Result to wait synchronously
                    response1.EnsureSuccessStatusCode();
                    var jsonString = response1.Content.ReadAsStringAsync().Result; // Use .Result to wait synchronously
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    JArray jsonArray = JArray.Parse(jsonString);
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());

                    InsertProfileDataTable(dataTable, CFD, GETPATH, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    //return dataTable;

                }
                else if (Fordate != null)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.176:8077/api/Home/MeterProfile_BYDate");
                    var content = new StringContent("{\r\n  \"StartDate\": \"" + Fordate + "\"\r\n}", null, "application/json");
                    request.Content = content;
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    // Send the request synchronously
                    var response = client.SendAsync(request).Result; // Use .Result to wait synchronously
                    response.EnsureSuccessStatusCode();
                    var jsonString = response.Content.ReadAsStringAsync().Result; // Use .Result to wait synchronously
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    JArray jsonArray = JArray.Parse(jsonString);
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());

                    InsertProfileDataTable(dataTable, CFD, GETPATH, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    //return dataTable;
                }
                if (data == true)
                {
                    meterprofileFileCreate(GETPATH, CFD, ref ProStatus);
                }
            }
            catch (HttpRequestException ex)
            {
                //TechError erro = new TechError();
                //erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }
        public void InsertProfileDataTable(DataTable APIDT, ConfigFileData TG, string getfilepath, ref int incrementFeeder, ref int completepersentag, ref string Prostatus)       // Insert Profile_Data Table 
        {
            #region
            try
            {
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string servername = TG.TGservername;
                string dbname = TG.TGdatabase;
                string userid = TG.TGusername;
                string password = TG.TGpassword;

                string Databackup = @"Data Source=" + servername +                       //Create Connection string
                      ";database=" + dbname +
                      ";User ID=" + userid +
                      ";Password=" + password;
                SqlConnection sq = new SqlConnection(Databackup);
                sq.Open();
                Prostatus = "Delete Old Cyme_Profile_Data........";
                Thread.Sleep(1500);
                string truncateQuery = $"TRUNCATE TABLE " + dbname + ".[dbo].[Cyme_Profile_Data]";    // TRUNCATE OLD TABLE
                SqlCommand command = new SqlCommand(truncateQuery, sq);
                command.ExecuteNonQuery();
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                DataTable dt = APIDT;
                if (dt != null)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string FORMAT_ID = dt.Rows[j]["METER_SR_NO"].ToString();
                        string FEEDER_NAME = string.Empty;
                        string DAYPROFILE_DATE = dt.Rows[j]["DAYPROFILE_DATE"].ToString();
                        string METER_MF = "1";
                        //string METER_MF = dt.Rows[j]["METER_MF"].ToString();

                        string day = DAYPROFILE_DATE.Remove(2);
                        int monthss = Convert.ToInt32(DAYPROFILE_DATE.Substring(3, 2));

                        string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                        string MM = Months[monthss - 1];

                        #region
                        // if (MM == "March")             // February Month Data only ..........               
                        // {
                        string RPH_LCURR = dt.Rows[j]["RPH_LCURR"].ToString();
                        string INTERVAL = dt.Rows[j]["INTERVAL"].ToString();
                        string YPH_LCURR = dt.Rows[j]["YPH_LCURR"].ToString();
                        string BPH_LCURR = dt.Rows[j]["BPH_LCURR"].ToString();
                        string KWH_READING = dt.Rows[j]["KWH_READING_WITH_MF"].ToString();
                        string KVAH_READING = dt.Rows[j]["KVAH_READING_WITH_MF"].ToString();
                        //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
                        float number1 = float.Parse(KWH_READING);
                        float number2 = float.Parse(KVAH_READING);
                        float number3;
                        if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
                        {
                            number3 = (float)Convert.ToDouble("0.00");
                        }
                        else if (number1 == 0 || number2 == 0)
                        {
                            number3 = (float)Convert.ToDouble("0.00");
                        }
                        else
                        {
                            number3 = number1 / number2;
                        }


                        string PROFILETYPE = "METER";
                        string INTERVALFORMAT = "8760HOURS";
                        string TIMEINTERVAL = "HOUR";
                        string GLOBALUNIT = "AMP-PF";
                        string YEAR = "2024";

                        string PP = "INSERT INTO " + dbname + ".[dbo].[Cyme_Profile_Data] (NETWORKID,FORMAT_ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,YEAR,MONTH,DAY,DATE_TIME,RPH_LCURR,YPH_LCURR,BPH_LCURR,PF,INTERVAL,METER_MF) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)";

                        SqlCommand cmd = new SqlCommand(PP, sq);
                        cmd.Parameters.AddWithValue("@P1", FEEDER_NAME);
                        cmd.Parameters.AddWithValue("@P2", FORMAT_ID);
                        cmd.Parameters.AddWithValue("@P3", PROFILETYPE);
                        cmd.Parameters.AddWithValue("@P4", INTERVALFORMAT);
                        cmd.Parameters.AddWithValue("@P5", TIMEINTERVAL);
                        cmd.Parameters.AddWithValue("@P6", GLOBALUNIT);
                        cmd.Parameters.AddWithValue("@P7", YEAR);
                        cmd.Parameters.AddWithValue("@P8", MM);
                        cmd.Parameters.AddWithValue("@P9", day);
                        cmd.Parameters.AddWithValue("@P10", DAYPROFILE_DATE);
                        cmd.Parameters.AddWithValue("@P11", RPH_LCURR);
                        cmd.Parameters.AddWithValue("@P12", YPH_LCURR);
                        cmd.Parameters.AddWithValue("@P13", BPH_LCURR);
                        cmd.Parameters.AddWithValue("@P14", number3);
                        cmd.Parameters.AddWithValue("@P15", INTERVAL);
                        cmd.Parameters.AddWithValue("@P16", METER_MF);

                        cmd.ExecuteNonQuery();
                        Prostatus = "Insert Cyme_Profile_Data........";
                        // }
                        #endregion
                    }
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;

                    Thread.Sleep(1000);
                    //CreateProfileTextFile(sq, getfilepath,ref incrementFeeder,ref completepersentag,ref Prostatus);
                }
            }
            catch (Exception ex)
            {
               // DE.Error_log(ex.ToString());
                //MessageBox.Show("Error : " + ex);
            }

            #endregion
        }


        //public void CreateProfileTextFile(SqlConnection sq, string GETFILE,ref int incrementFeeder,ref int completepersentag,ref string ProStatus)   // Create Profile_Data NetworkText File ........
        public void CreateProfileTextFile(string profile_FstartD, string profile_TendD, string Fordate, ConfigFileData TG, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)   // Create Profile_Data NetworkText File ........
        {
            #region
            try
            {
                if (Fordate != null)
                {
                    string servername = TG.TGservername;
                    string dbname = TG.TGdatabase;
                    string userid = TG.TGusername;
                    string password = TG.TGpassword;

                    string Databackup = @"Data Source=" + servername +                       //Create Connection string
                          ";database=" + dbname +
                          ";User ID=" + userid +
                          ";Password=" + password;
                    SqlConnection sq = new SqlConnection(Databackup);

                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;  //2023-10-01 00:00:00.000
                    string BB = "SELECT* FROM[CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' ORDER BY SERIAL_NUMBER, FEEDER_CODE, INTERVAL; ";
                    //string BB = "WITH HourlyData AS ( SELECT * FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0)SELECT a. * FROM HourlyData a ORDER BY SERIAL_NUMBER ,FEEDER_CODE,INTERVAL;";
                    //string BB = "SELECT *FROM Meter_Profile_API WHERE CAST(DAYPROFILE_DATE AS date) LIKE '" + Fordate + "%' ORDER BY INTERVAL; ";
                    // string BB = "SELECT *FROM Meter_Profile_API WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
                    using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                    {
                        ProStatus = "Delete Old Meter Text File.....";
                        Thread.Sleep(1000);
                        string MeterData_path = GETFILE + "\\MeterData File\\MeterData.txt";
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

                            if (sq.State == ConnectionState.Closed)
                            {
                                sq.Open();
                            }
                            SqlDataReader reader1 = cmd1.ExecuteReader();
                            DataTable dttd = new DataTable();
                            dttd.Load(reader1);
                            string str = @"[PROFILE_VALUES]
FORMAT=ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,NETWORKID,YEAR,MONTH,DAY,UNIT,PHASE,VALUES";
                            tw1.WriteLine(str);
                            int counting = 0;
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string SERIAL_NUMBER = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
                                string NETWORKID = dttd.Rows[i]["FEEDER_CODE"].ToString();
                                string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
                               // string INTERVAL = dttd.Rows[i]["INTERVAL"].ToString();
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());
                                string SYS_PF = dttd.Rows[i]["SYS_PF"].ToString();
                                string PROFILETYPE = "METER";
                                string INTERVALFORMAT = "8760HOURS";
                                string TIMEINTERVAL = "HOUR";
                                string GLOBALUNIT = "AMP-PF";
                                if (i == 5324)
                                {

                                }


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
                                if (i == 2563)
                                {

                                }
                                //string onemonth = dttd.Rows[i]["MONTH"].ToString();

                                //string Day = day.ToString();
                                if (counting == i)
                                {
                                    while (k < 24)
                                    {
                                        int n = 100;
                                        int n1 = METER_MF;

                                        string PF1 = SYS_PF;
                                        //string PF1 = dttd.Rows[k + i]["SYS_PF"].ToString();
                                      
                                        if (PF1 == string.Empty)
                                        {
                                           
                                            string KWH_READING = dttd.Rows[k + i]["KWH_READING"].ToString();
                                            string KVAH_READING = dttd.Rows[k + i]["KVAH_READING"].ToString();
                                            //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
                                            float number1 = float.Parse(KWH_READING);
                                            float number2 = float.Parse(KVAH_READING);
                                            float number3;
                                            if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
                                            {
                                                number3 = (float)Convert.ToDouble("0.00");
                                            }
                                            else if (number1 == 0 || number2 == 0)
                                            {
                                                number3 = (float)Convert.ToDouble("0.00");
                                            }
                                            else
                                            {
                                                number3 = number1 / number2;
                                            }
                                            double PF2 = (double)n * Convert.ToDouble(number3);
                                            PF += Convert.ToString(PF2) + ",";

                                        }
                                        else
                                        {
                                            double PF2 = (double)n * Convert.ToDouble(PF1);
                                            PF += Convert.ToString(PF2) + ",";
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
                                    daay = Convert.ToInt32(Day);
                                    if (daay == 364)
                                    {
                                        break;
                                    }

                                }
                                PF = string.Empty;
                                RPH_LCURR = string.Empty;
                                YPH_LCURR = string.Empty;
                                BPH_LCURR = string.Empty;

                            }
                            tw1.Close();

                            reader1.Close();
                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.Message);
                            tw1.Close();
                        }
                        incrementFeeder = 10;
                        completepersentag += incrementFeeder;
                        ProStatus = "Create New Meter Text File.....";
                        Thread.Sleep(1000);
                    }

                }
                else
                {
                        string servername = TG.TGservername;
                        string dbname = TG.TGdatabase;
                        string userid = TG.TGusername;
                        string password = TG.TGpassword;

                        string Databackup = @"Data Source=" + servername +                       //Create Connection string
                              ";database=" + dbname +
                              ";User ID=" + userid +
                              ";Password=" + password;
                        SqlConnection sq = new SqlConnection(Databackup);

                        incrementFeeder = 10;
                        completepersentag += incrementFeeder;  //2023-10-01 00:00:00.000
                    string BB = "SELECT * FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE  DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND  CAST(DAYPROFILE_DATE AS DATE) >= '" + profile_FstartD + "' AND CAST(DAYPROFILE_DATE AS DATE) <= '" + profile_TendD + "' order by SERIAL_NUMBER,DAYPROFILE_DATE;";
                    //string BB = "SELECT * FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE  DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND  CAST(DAYPROFILE_DATE AS DATE) >= '" + profile_FstartD + "' AND CAST(DAYPROFILE_DATE AS DATE) <= '" + profile_TendD + "';";
                        //string BB = "WITH HourlyData AS ( SELECT * FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0)SELECT a. * FROM HourlyData a ORDER BY SERIAL_NUMBER ,FEEDER_CODE,INTERVAL;";
                        //string BB = "SELECT *FROM Meter_Profile_API WHERE CAST(DAYPROFILE_DATE AS date) LIKE '" + Fordate + "%' ORDER BY INTERVAL; ";
                        // string BB = "SELECT *FROM Meter_Profile_API WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
                        using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                        {
                            ProStatus = "Delete Old Meter Text File.....";
                            Thread.Sleep(1000);
                            string MeterData_path = GETFILE + "\\MeterData File\\MeterData.txt";
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

                                if (sq.State == ConnectionState.Closed)
                                {
                                    sq.Open();
                                }
                                SqlDataReader reader1 = cmd1.ExecuteReader();
                                DataTable dttd = new DataTable();
                                dttd.Load(reader1);
                                string str = @"[PROFILE_VALUES]
FORMAT=ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,NETWORKID,YEAR,MONTH,DAY,UNIT,PHASE,VALUES";
                                tw1.WriteLine(str);
                                int counting = 0;
                                for (int i = 0; i < dttd.Rows.Count; i++)
                                {
                                    string SERIAL_NUMBER = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
                                    string NETWORKID = dttd.Rows[i]["FEEDER_CODE"].ToString();
                                    string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
                                    string INTERVAL = dttd.Rows[i]["INTERVAL"].ToString();
                                    int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());
                                    string SYS_PF = dttd.Rows[i]["SYS_PF"].ToString();
                                    string PROFILETYPE = "METER";
                                    string INTERVALFORMAT = "8760HOURS";
                                    string TIMEINTERVAL = "HOUR";
                                    string GLOBALUNIT = "AMP-PF";
                                    if (SERIAL_NUMBER == "X1960247")
                                    {

                                    }


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

                                    //string onemonth = dttd.Rows[i]["MONTH"].ToString();

                                    //string Day = day.ToString();
                                    if (counting == i)
                                    {
                                        while (k < 24)
                                        {
                                            int n = 100;
                                            int nnn = 90;
                                            int n1 = METER_MF;

                                            string PF1 = SYS_PF;
                                            //string PF1 = dttd.Rows[k + i]["SYS_PF"].ToString();

                                            if (PF1 == string.Empty)
                                            {

                                                string KWH_READING = dttd.Rows[k + i]["KWH_READING"].ToString();
                                                string KVAH_READING = dttd.Rows[k + i]["KVAH_READING"].ToString();
                                            //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
                                            if (KWH_READING != "" && KVAH_READING != "")
                                            {
                                                float number1 = float.Parse(KWH_READING);
                                                float number2 = float.Parse(KVAH_READING);
                                                float number3;
                                                if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
                                                {
                                                    number3 = (float)Convert.ToDouble("0.00");
                                                }
                                                else if (number1 == 0 || number2 == 0)
                                                {
                                                    number3 = (float)Convert.ToDouble("0.00");
                                                }
                                                else
                                                {
                                                    number3 = number1 / number2;
                                                }
                                                double PF2 = (double)n * Convert.ToDouble(number3);
                                                PF += Convert.ToString(PF2) + ",";
                                            }
                                            else
                                            {
                                                double PF2 = nnn;
                                                PF += Convert.ToString(PF2) + ",";
                                            }
                                            

                                            }
                                            else
                                            {
                                                double PF2 = (double)n * Convert.ToDouble(PF1);
                                                PF += Convert.ToString(PF2) + ",";
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
                                        daay = Convert.ToInt32(Day);
                                        if (daay == 364)
                                        {
                                            break;
                                        }

                                    }
                                    PF = string.Empty;
                                    RPH_LCURR = string.Empty;
                                    YPH_LCURR = string.Empty;
                                    BPH_LCURR = string.Empty;

                                }
                                tw1.Close();

                                reader1.Close();
                            }
                            catch (Exception ex)
                            {
                                // DE.Error_log(ex.ToString());
                                //MessageBox.Show(ex.ToString());
                                tw1.Close();
                            }
                            incrementFeeder = 10;
                            completepersentag += incrementFeeder;
                            ProStatus = "Create New Meter Text File.....";
                            Thread.Sleep(1000);
                        }

                    

                }

                meterprofileFileCreate(GETFILE, TG, ref ProStatus);


            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE,ex.ToString());
            }
            #endregion
        }
        public void meterprofileFileCreate(string GETFILE, ConfigFileData cf, ref string Prostatus)     // batchFile create.....
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
                    text = text.Replace("@profiledata@", GETFILE + "\\MeterData File\\MeterData.txt");
                    File.WriteAllText(TargetFile, text);
                }

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
                Prostatus = "Upload Meter Data into Cyme......";
                Thread.Sleep(200);
                import(batchpath, cyme);
            }
            catch (Exception ex)
            {
               // DE.Error_log(ex.ToString());
            }
        }
        public void import(string path, string workdir)    // import into cyme database....
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


                //ProcessStartInfo processInfo;
                //processInfo = new ProcessStartInfo(str_Path2);
                //processInfo.CreateNoWindow = true;
                //processInfo.UseShellExecute = false;
                //// *** Redirect the output ***
                //processInfo.RedirectStandardError = true;
                //processInfo.RedirectStandardOutput = true;

                //Process = Process.Start(processInfo);
                //process.WaitForExit();
                //importfeeder = false;
            }

        }







        //Meter_Peek_Data.......Nik
        public void CreatePeakTextFile(string profile_FstartD, string profile_TendD, string Fordate, ConfigFileData TG, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)   // Create Peek_DataMeterDemandText File ........
        {
            #region
            try
            {
                if (Fordate != null)
                {
                    string servername = TG.TGservername;
                    string dbname = TG.TGdatabase;
                    string userid = TG.TGusername;
                    string password = TG.TGpassword;

                    string Databackup = @"Data Source=" + servername +                       //Create Connection string
                          ";database=" + dbname +
                          ";User ID=" + userid +
                          ";Password=" + password;
                    SqlConnection sq = new SqlConnection(Databackup);

                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    string BB = "SELECT* FROM[CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' ORDER BY SERIAL_NUMBER, FEEDER_CODE, INTERVAL";
                    //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
                    //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
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
                            int daay = 0;
                            int k = 0;
                            string PF = string.Empty;
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;
                            string METER_SR_NO111 = string.Empty;
                            string METER_SR_NO1 = string.Empty;
                            if (sq.State == ConnectionState.Closed)
                            {
                                sq.Open();
                            }
                            SqlDataReader reader1 = cmd1.ExecuteReader();
                            DataTable dttd = new DataTable();
                            dttd.Load(reader1);

                            StringBuilder builder = new StringBuilder();
                            builder.Append("[GENERAL]\r\n");
                            builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
                            builder.Append("CYME_VERSION=9.3\r\n" + "CYMDIST_REVISION=05\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
                            builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected,ReferenceTime\r\n" + "\r\n");
                            //FORMAT_METERDEMANDSETTING = DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected";
                            string str = builder.ToString();
                            tw1.WriteLine(str);
                            int counting = 0;
                            ProStatus = "Create MeterDemandText File.....";
                            for (int i = 0; i < dttd.Rows.Count; i++)
                            {
                                string METER_SR_NO = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
                                string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());

                                string Day = string.Empty;
                                string MM = string.Empty;
                                string yearString = string.Empty;

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
                                    string cymeservername = TG.CNetservername;
                                    string cymedbname = TG.CNetdatabase;
                                    string cymeuserid = TG.CNetusername;
                                    string cymepassword = TG.CNetpassword;

                                    string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
                                          ";database=" + cymedbname +
                                          ";User ID=" + cymeuserid +
                                          ";Password=" + cymepassword;
                                    SqlConnection cymesq = new SqlConnection(cymeDatabackup);

                                    //incrementFeeder = 10;
                                    //completepersentag += incrementFeeder;
                                    string cymeBB = "SELECT DeviceNumber FROM " + cymedbname + ".[dbo].[CYMBREAKER] WHERE [DeviceNumber] Like'%" + METER_SR_NO + "'";
                                    //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
                                    //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
                                    SqlCommand cymecmd1 = new SqlCommand(cymeBB, cymesq);
                                    if (cymesq.State == ConnectionState.Closed)
                                    {
                                        cymesq.Open();
                                    }
                                    SqlDataReader cymereader1 = cymecmd1.ExecuteReader();
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


                                    // start....


                                    while (k < 48)
                                    {
                                        if (counting <= dttd.Rows.Count)
                                        {
                                            int sss = k + i;
                                            if (sss >= 0 && sss < dttd.Rows.Count)
                                            {
                                                string meterno = dttd.Rows[k + i]["SERIAL_NUMBER"].ToString();
                                                //string Days = dttd.Rows[k + i]["DAY"].ToString();
                                                string Days = Day;
                                                if (METER_SR_NO == meterno && Day == Days)
                                                {
                                                    int n = 100;
                                                    int n1 = METER_MF;
                                                    string PF1 = dttd.Rows[k + i]["SYS_PF"].ToString();

                                                    if (PF1 == string.Empty)
                                                    {
                                                        string KWH_READING = dttd.Rows[k + i]["KWH_READING"].ToString();
                                                        string KVAH_READING = dttd.Rows[k + i]["KVAH_READING"].ToString();
                                                        //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
                                                        float number1 = float.Parse(KWH_READING);
                                                        float number2 = float.Parse(KVAH_READING);
                                                        float number3;
                                                        if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
                                                        {
                                                            number3 = (float)Convert.ToDouble("0.00");
                                                        }
                                                        else if (number1 == 0 || number2 == 0)
                                                        {
                                                            number3 = (float)Convert.ToDouble("0.00");
                                                        }
                                                        else
                                                        {
                                                            number3 = number1 / number2;
                                                        }
                                                        double PF2 = (double)n * Convert.ToDouble(number3);
                                                        PF += Convert.ToString(PF2) + ",";

                                                    }
                                                    else
                                                    {
                                                        double PF2 = (double)n * Convert.ToDouble(PF1);
                                                        PF += Convert.ToString(PF2) + ",";
                                                    }










                                                    //double PF2 = (double)n * Convert.ToDouble(PF1);
                                                    //PF += Convert.ToString(PF2) + ",";
                                                    //PF += dttd.Rows[k + i]["PF"].ToString() + ",";


                                                    string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                                    if (RPH_LCURR1 == string.Empty)
                                                    {
                                                        RPH_LCURR += "0" + ",";
                                                    }
                                                    else
                                                    {
                                                        double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                                        RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";
                                                    }




                                                    string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                                    if (YPH_LCURR1 == string.Empty)
                                                    {
                                                        YPH_LCURR += "0" + ",";
                                                    }
                                                    else
                                                    {
                                                        double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                                        YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";
                                                    }



                                                    string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                                    if (BPH_LCURR1 == string.Empty)
                                                    {
                                                        BPH_LCURR += "0" + ",";
                                                    }
                                                    else
                                                    {
                                                        double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                                        BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";
                                                    }


                                                    counting++;
                                                }
                                                else
                                                {
                                                    int n = 100;
                                                    int n1 = METER_MF;
                                                    //METER_SR_NO111 = METER_SR_NO;
                                                    //string PF1 = dttd.Rows[k + i]["PF"].ToString();
                                                    double PF2 = 0;
                                                    PF += Convert.ToString(PF2) + ",";


                                                    // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                                    double RPH_LCURR2 = 0;
                                                    RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


                                                    // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                                    double YPH_LCURR2 = 0;
                                                    YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


                                                    //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                                    double BPH_LCURR2 = 0;
                                                    BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

                                                }
                                            }
                                            else
                                            {
                                                int n = 100;
                                                int n1 = METER_MF;
                                                //METER_SR_NO111 = METER_SR_NO;
                                                //string PF1 = dttd.Rows[k + i]["PF"].ToString();
                                                double PF2 = 0;
                                                PF += Convert.ToString(PF2) + ",";


                                                // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                                double RPH_LCURR2 = 0;
                                                RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


                                                // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                                double YPH_LCURR2 = 0;
                                                YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


                                                //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                                double BPH_LCURR2 = 0;
                                                BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

                                            }
                                        }
                                        k++;
                                    }
                                    PF = PF.TrimEnd(',');
                                    double[] maxValuemaxValuePF = ConvertStringToDoubleArray(PF.TrimEnd(','));
                                    double[] maxValueYPH_LCURR = ConvertStringToDoubleArray(YPH_LCURR.TrimEnd(','));
                                    double[] maxValueBPH_LCURR = ConvertStringToDoubleArray(BPH_LCURR.TrimEnd(','));
                                    double[] maxValueRPH_LCURR = ConvertStringToDoubleArray(RPH_LCURR.TrimEnd(','));

                                    double maxValuemaxValuePF1 = maxValuemaxValuePF.Max();
                                    double maxValueYPH_LCURR1 = maxValueYPH_LCURR.Max();
                                    double maxValueBPH_LCURR1 = maxValueBPH_LCURR.Max();
                                    double maxValueRPH_LCURR1 = maxValueRPH_LCURR.Max();



                                    tw1.Write("8");
                                    tw1.Write("," + METER_SR_NO1);
                                    //tw1.Write("," + dttd.Rows[i]["SERIAL_NUMBER"].ToString().Trim());
                                    tw1.Write("," + "1");
                                    tw1.Write("," + maxValueRPH_LCURR1);
                                    tw1.Write("," + maxValuemaxValuePF1);
                                    tw1.Write("," + maxValueYPH_LCURR1);
                                    tw1.Write("," + maxValuemaxValuePF1);
                                    tw1.Write("," + maxValueBPH_LCURR1);
                                    tw1.Write("," + maxValuemaxValuePF1);
                                    tw1.Write("," + "0");
                                    tw1.Write("," + "0");

                                    tw1.WriteLine();

                                    k = 0;
                                    daay = Convert.ToInt32(Day);
                                    if (daay == 364)
                                    {
                                        break;
                                    }

                                    PF = string.Empty;
                                    RPH_LCURR = string.Empty;
                                    YPH_LCURR = string.Empty;
                                    BPH_LCURR = string.Empty;

                                    // }
                                }
                            }
                            tw1.Close();

                            reader1.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            tw1.Close();
                        }
                        incrementFeeder = 10;
                        completepersentag += incrementFeeder;
                        ProStatus = "MeterDemandText File Completed.....";
                        Thread.Sleep(1000);
                    }

                }
                else
                {
                    string servername = TG.TGservername;
                    string dbname = TG.TGdatabase;
                    string userid = TG.TGusername;
                    string password = TG.TGpassword;

                    string Databackup = @"Data Source=" + servername +                       //Create Connection string
                          ";database=" + dbname +
                          ";User ID=" + userid +
                          ";Password=" + password;
                    SqlConnection sq = new SqlConnection(Databackup);


                    string cymeservername = TG.CNetservername;
                    string cymedbname = TG.CNetdatabase;
                    string cymeuserid = TG.CNetusername;
                    string cymepassword = TG.CNetpassword;

                    string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
                          ";database=" + cymedbname +
                          ";User ID=" + cymeuserid +
                          ";Password=" + cymepassword;
                    SqlConnection cymesq = new SqlConnection(cymeDatabackup);






                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;

                    string BB = "SELECT SERIAL_NUMBER, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(MF) AS MF, MAX(SYS_PF) AS SYS_PF, MAX(KWH_READING) AS KWH_READING, MAX(KVAH_READING) AS KVAH_READING FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) >='" + profile_FstartD + "' AND CAST(DAYPROFILE_DATE AS DATE) <= '" + profile_TendD + "' GROUP BY SERIAL_NUMBER;";
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



                            Thread.Sleep(2000);
                            string RPH_LCURR = string.Empty;
                            string YPH_LCURR = string.Empty;
                            string BPH_LCURR = string.Empty;



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
                                string METER_SR_NO = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
                                RPH_LCURR = dttd.Rows[i]["RPH_LCURR"].ToString();
                                YPH_LCURR = dttd.Rows[i]["YPH_LCURR"].ToString();
                                BPH_LCURR = dttd.Rows[i]["BPH_LCURR"].ToString();
                                string PF1 = dttd.Rows[i]["SYS_PF"].ToString();
                                int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());
                                int n = 100;
                                int n1 = METER_MF;
                                string PF = string.Empty;
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
                                Thread.Sleep(1000);
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

                


                                if (PF1 == string.Empty)
                                {
                                    string KWH_READING = dttd.Rows[i]["KWH_READING"].ToString();
                                    string KVAH_READING = dttd.Rows[i]["KVAH_READING"].ToString();
                                    float number1 = float.Parse(KWH_READING);
                                    float number2 = float.Parse(KVAH_READING);
                                    float number3;
                                    if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
                                    {
                                        number3 = (float)Convert.ToDouble("0.00");
                                    }
                                    else if (number1 == 0 || number2 == 0)
                                    {
                                        number3 = (float)Convert.ToDouble("0.00");
                                    }
                                    else
                                    {
                                        number3 = number1 / number2;
                                    }
                                    double PF2 = (double)n * Convert.ToDouble(number3);
                                    PF = Convert.ToString(PF2);

                                }
                                else
                                {
                                    double PF2 = (double)n * Convert.ToDouble(PF1);
                                    PF = Convert.ToString(PF2);
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
                        incrementFeeder = 10;
                        completepersentag += incrementFeeder;
                        ProStatus = "MeterDemandText File Completed.....";
                        Thread.Sleep(1000);


                        

                    }


                }

                meterPeakFileCreate(GETFILE, TG, ref ProStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorsss" + ex);
            }

            #endregion

        }
        public void meterPeakFileCreate(string GETFILE, ConfigFileData cf, ref string Prostatus)     // batchFile create.....
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
                Thread.Sleep(200);
                importBat(batchpath, cyme);
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }
        public void importBat(string path, string workdir)    // import into cyme database....
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


                //ProcessStartInfo processInfo;
                //processInfo = new ProcessStartInfo(str_Path2);
                //processInfo.CreateNoWindow = true;
                //processInfo.UseShellExecute = false;
                //// *** Redirect the output ***
                //processInfo.RedirectStandardError = true;
                //processInfo.RedirectStandardOutput = true;

                //Process = Process.Start(processInfo);
                //process.WaitForExit();
                //importfeeder = false;
            }

        }
        private double[] ConvertStringToDoubleArray(string rPH_LCURR)
        {
            string[] substrings = rPH_LCURR.Replace(" ", "").Split(',');

            // Convert the substrings to double and store in an array
            double[] result = new double[substrings.Length];
            for (int i = 0; i < substrings.Length; i++)
            {
                result[i] = double.Parse(substrings[i]);
            }

            return result;
        }
    }
}



//public void CreatePeakTextFile(string profile_FstartD, string profile_TendD, string Fordate, ConfigFileData TG, string GETFILE, ref int incrementFeeder, ref int completepersentag, ref string ProStatus)   // Create Peek_DataMeterDemandText File ........
//{
//    #region
//    try
//    {
//        if (Fordate != null)
//        {
//            string servername = TG.TGservername;
//            string dbname = TG.TGdatabase;
//            string userid = TG.TGusername;
//            string password = TG.TGpassword;

//            string Databackup = @"Data Source=" + servername +                       //Create Connection string
//                  ";database=" + dbname +
//                  ";User ID=" + userid +
//                  ";Password=" + password;
//            SqlConnection sq = new SqlConnection(Databackup);

//            incrementFeeder = 10;
//            completepersentag += incrementFeeder;
//            string BB = "SELECT* FROM[CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' ORDER BY SERIAL_NUMBER, FEEDER_CODE, INTERVAL";
//            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//            using (SqlCommand cmd1 = new SqlCommand(BB, sq))
//            {
//                ProStatus = "Delete Old MeterDemandText File.....";
//                Thread.Sleep(1000);
//                string MeterData_path = GETFILE + "\\MeterData File\\MeterDemand.txt";
//                if (Directory.Exists(MeterData_path))
//                {
//                    System.IO.Directory.Delete(MeterData_path, true);
//                }
//                StreamWriter tw1 = File.CreateText(MeterData_path);
//                try
//                {
//                    int daay = 0;
//                    int k = 0;
//                    string PF = string.Empty;
//                    string RPH_LCURR = string.Empty;
//                    string YPH_LCURR = string.Empty;
//                    string BPH_LCURR = string.Empty;
//                    string METER_SR_NO111 = string.Empty;
//                    string METER_SR_NO1 = string.Empty;
//                    if (sq.State == ConnectionState.Closed)
//                    {
//                        sq.Open();
//                    }
//                    SqlDataReader reader1 = cmd1.ExecuteReader();
//                    DataTable dttd = new DataTable();
//                    dttd.Load(reader1);

//                    StringBuilder builder = new StringBuilder();
//                    builder.Append("[GENERAL]\r\n");
//                    builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
//                    builder.Append("CYME_VERSION=9.3\r\n" + "CYMDIST_REVISION=05\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
//                    builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected,ReferenceTime\r\n" + "\r\n");
//                    //FORMAT_METERDEMANDSETTING = DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected";
//                    string str = builder.ToString();
//                    tw1.WriteLine(str);
//                    int counting = 0;
//                    ProStatus = "Create MeterDemandText File.....";
//                    for (int i = 0; i < dttd.Rows.Count; i++)
//                    {
//                        string METER_SR_NO = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
//                        string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
//                        int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());

//                        string Day = string.Empty;
//                        string MM = string.Empty;
//                        string yearString = string.Empty;

//                        DateTime date;
//                        if (DateTime.TryParse(DayProfileDate, out date))
//                        {
//                            int day = date.Day;        // Extracts the day
//                            int month = date.Month;    // Extracts the month
//                            int year = date.Year;      // Extracts the year

//                            Day = day.ToString();
//                            string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
//                            MM = Months[month - 1];
//                            yearString = year.ToString();

//                        }

//                        if (counting == i)
//                        {
//                            string cymeservername = TG.CNetservername;
//                            string cymedbname = TG.CNetdatabase;
//                            string cymeuserid = TG.CNetusername;
//                            string cymepassword = TG.CNetpassword;

//                            string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
//                                  ";database=" + cymedbname +
//                                  ";User ID=" + cymeuserid +
//                                  ";Password=" + cymepassword;
//                            SqlConnection cymesq = new SqlConnection(cymeDatabackup);

//                            //incrementFeeder = 10;
//                            //completepersentag += incrementFeeder;
//                            string cymeBB = "SELECT DeviceNumber FROM " + cymedbname + ".[dbo].[CYMBREAKER] WHERE [DeviceNumber] Like'%" + METER_SR_NO + "'";
//                            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//                            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//                            SqlCommand cymecmd1 = new SqlCommand(cymeBB, cymesq);
//                            if (cymesq.State == ConnectionState.Closed)
//                            {
//                                cymesq.Open();
//                            }
//                            SqlDataReader cymereader1 = cymecmd1.ExecuteReader();
//                            DataTable cymedttd = new DataTable();
//                            cymedttd.Load(cymereader1);


//                            if (cymedttd.Rows.Count > 0)
//                            {
//                                METER_SR_NO1 = cymedttd.Rows[0]["DeviceNumber"].ToString();
//                            }
//                            else
//                            {
//                                METER_SR_NO1 = METER_SR_NO;
//                            }


//                            // start....


//                            while (k < 48)
//                            {
//                                if (counting <= dttd.Rows.Count)
//                                {
//                                    int sss = k + i;
//                                    if (sss >= 0 && sss < dttd.Rows.Count)
//                                    {
//                                        string meterno = dttd.Rows[k + i]["SERIAL_NUMBER"].ToString();
//                                        //string Days = dttd.Rows[k + i]["DAY"].ToString();
//                                        string Days = Day;
//                                        if (METER_SR_NO == meterno && Day == Days)
//                                        {
//                                            int n = 100;
//                                            int n1 = METER_MF;
//                                            string PF1 = dttd.Rows[k + i]["SYS_PF"].ToString();

//                                            if (PF1 == string.Empty)
//                                            {
//                                                string KWH_READING = dttd.Rows[k + i]["KWH_READING"].ToString();
//                                                string KVAH_READING = dttd.Rows[k + i]["KVAH_READING"].ToString();
//                                                //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
//                                                float number1 = float.Parse(KWH_READING);
//                                                float number2 = float.Parse(KVAH_READING);
//                                                float number3;
//                                                if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
//                                                {
//                                                    number3 = (float)Convert.ToDouble("0.00");
//                                                }
//                                                else if (number1 == 0 || number2 == 0)
//                                                {
//                                                    number3 = (float)Convert.ToDouble("0.00");
//                                                }
//                                                else
//                                                {
//                                                    number3 = number1 / number2;
//                                                }
//                                                double PF2 = (double)n * Convert.ToDouble(number3);
//                                                PF += Convert.ToString(PF2) + ",";

//                                            }
//                                            else
//                                            {
//                                                double PF2 = (double)n * Convert.ToDouble(PF1);
//                                                PF += Convert.ToString(PF2) + ",";
//                                            }










//                                            //double PF2 = (double)n * Convert.ToDouble(PF1);
//                                            //PF += Convert.ToString(PF2) + ",";
//                                            //PF += dttd.Rows[k + i]["PF"].ToString() + ",";


//                                            string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                            if (RPH_LCURR1 == string.Empty)
//                                            {
//                                                RPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
//                                                RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";
//                                            }




//                                            string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                            if (YPH_LCURR1 == string.Empty)
//                                            {
//                                                YPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
//                                                YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";
//                                            }



//                                            string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                            if (BPH_LCURR1 == string.Empty)
//                                            {
//                                                BPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
//                                                BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";
//                                            }


//                                            counting++;
//                                        }
//                                        else
//                                        {
//                                            int n = 100;
//                                            int n1 = METER_MF;
//                                            //METER_SR_NO111 = METER_SR_NO;
//                                            //string PF1 = dttd.Rows[k + i]["PF"].ToString();
//                                            double PF2 = 0;
//                                            PF += Convert.ToString(PF2) + ",";


//                                            // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                            double RPH_LCURR2 = 0;
//                                            RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


//                                            // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                            double YPH_LCURR2 = 0;
//                                            YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


//                                            //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                            double BPH_LCURR2 = 0;
//                                            BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

//                                        }
//                                    }
//                                    else
//                                    {
//                                        int n = 100;
//                                        int n1 = METER_MF;
//                                        //METER_SR_NO111 = METER_SR_NO;
//                                        //string PF1 = dttd.Rows[k + i]["PF"].ToString();
//                                        double PF2 = 0;
//                                        PF += Convert.ToString(PF2) + ",";


//                                        // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                        double RPH_LCURR2 = 0;
//                                        RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


//                                        // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                        double YPH_LCURR2 = 0;
//                                        YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


//                                        //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                        double BPH_LCURR2 = 0;
//                                        BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

//                                    }
//                                }
//                                k++;
//                            }
//                            PF = PF.TrimEnd(',');
//                            double[] maxValuemaxValuePF = ConvertStringToDoubleArray(PF.TrimEnd(','));
//                            double[] maxValueYPH_LCURR = ConvertStringToDoubleArray(YPH_LCURR.TrimEnd(','));
//                            double[] maxValueBPH_LCURR = ConvertStringToDoubleArray(BPH_LCURR.TrimEnd(','));
//                            double[] maxValueRPH_LCURR = ConvertStringToDoubleArray(RPH_LCURR.TrimEnd(','));

//                            double maxValuemaxValuePF1 = maxValuemaxValuePF.Max();
//                            double maxValueYPH_LCURR1 = maxValueYPH_LCURR.Max();
//                            double maxValueBPH_LCURR1 = maxValueBPH_LCURR.Max();
//                            double maxValueRPH_LCURR1 = maxValueRPH_LCURR.Max();



//                            tw1.Write("8");
//                            tw1.Write("," + METER_SR_NO1);
//                            //tw1.Write("," + dttd.Rows[i]["SERIAL_NUMBER"].ToString().Trim());
//                            tw1.Write("," + "1");
//                            tw1.Write("," + maxValueRPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + maxValueYPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + maxValueBPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + "0");
//                            tw1.Write("," + "0");

//                            tw1.WriteLine();

//                            k = 0;
//                            daay = Convert.ToInt32(Day);
//                            if (daay == 364)
//                            {
//                                break;
//                            }

//                            PF = string.Empty;
//                            RPH_LCURR = string.Empty;
//                            YPH_LCURR = string.Empty;
//                            BPH_LCURR = string.Empty;

//                            // }
//                        }
//                    }
//                    tw1.Close();

//                    reader1.Close();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.ToString());
//                    tw1.Close();
//                }
//                incrementFeeder = 10;
//                completepersentag += incrementFeeder;
//                ProStatus = "MeterDemandText File Completed.....";
//                Thread.Sleep(1000);
//            }

//        }
//        else
//        {
//            string servername = TG.TGservername;
//            string dbname = TG.TGdatabase;
//            string userid = TG.TGusername;
//            string password = TG.TGpassword;

//            string Databackup = @"Data Source=" + servername +                       //Create Connection string
//                  ";database=" + dbname +
//                  ";User ID=" + userid +
//                  ";Password=" + password;
//            SqlConnection sq = new SqlConnection(Databackup);

//            incrementFeeder = 10;
//            completepersentag += incrementFeeder;
//            string BB = "SELECT * FROM [CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE  DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND  CAST(DAYPROFILE_DATE AS DATE) >= '" + profile_FstartD + "' AND CAST(DAYPROFILE_DATE AS DATE) <= '" + profile_TendD + "';";
//            //string BB = "SELECT* FROM[CymeWebMsedclAdminDB].[dbo].[Meter_Profile_API] WHERE DATEPART(MINUTE, DAYPROFILE_DATE) = 0 AND CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' ORDER BY SERIAL_NUMBER, FEEDER_CODE, INTERVAL";
//            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//            using (SqlCommand cmd1 = new SqlCommand(BB, sq))
//            {
//                ProStatus = "Delete Old MeterDemandText File.....";
//                Thread.Sleep(1000);
//                string MeterData_path = GETFILE + "\\MeterData File\\MeterDemand.txt";
//                if (Directory.Exists(MeterData_path))
//                {
//                    System.IO.Directory.Delete(MeterData_path, true);
//                }
//                StreamWriter tw1 = File.CreateText(MeterData_path);
//                try
//                {
//                    int daay = 0;
//                    int k = 0;
//                    string PF = string.Empty;
//                    string RPH_LCURR = string.Empty;
//                    string YPH_LCURR = string.Empty;
//                    string BPH_LCURR = string.Empty;
//                    string METER_SR_NO111 = string.Empty;
//                    string METER_SR_NO1 = string.Empty;
//                    if (sq.State == ConnectionState.Closed)
//                    {
//                        sq.Open();
//                    }
//                    SqlDataReader reader1 = cmd1.ExecuteReader();
//                    DataTable dttd = new DataTable();
//                    dttd.Load(reader1);

//                    StringBuilder builder = new StringBuilder();
//                    builder.Append("[GENERAL]\r\n");
//                    builder.Append("DATE=" + DateTime.Now.ToString("MMMM dd yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
//                    builder.Append("CYME_VERSION=9.3\r\n" + "CYMDIST_REVISION=05\r\n" + "\r\n" + "[SI]\r\n" + "\r\n" + "\r\n" + "[METERDEMAND SETTING]\r\n" + "\r\n");
//                    builder.Append("FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected,ReferenceTime\r\n" + "\r\n");
//                    //FORMAT_METERDEMANDSETTING = DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Disconnected";
//                    string str = builder.ToString();
//                    tw1.WriteLine(str);
//                    int counting = 0;
//                    ProStatus = "Create MeterDemandText File.....";
//                    for (int i = 0; i < dttd.Rows.Count; i++)
//                    {
//                        string METER_SR_NO = dttd.Rows[i]["SERIAL_NUMBER"].ToString();
//                        string DayProfileDate = dttd.Rows[i]["DAYPROFILE_DATE"].ToString();
//                        int METER_MF = Convert.ToInt32(dttd.Rows[i]["MF"].ToString());

//                        string Day = string.Empty;
//                        string MM = string.Empty;
//                        string yearString = string.Empty;

//                        DateTime date;
//                        if (DateTime.TryParse(DayProfileDate, out date))
//                        {
//                            int day = date.Day;        // Extracts the day
//                            int month = date.Month;    // Extracts the month
//                            int year = date.Year;      // Extracts the year

//                            Day = day.ToString();
//                            string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
//                            MM = Months[month - 1];
//                            yearString = year.ToString();

//                        }

//                        if (counting == i)
//                        {
//                            string cymeservername = TG.CNetservername;
//                            string cymedbname = TG.CNetdatabase;
//                            string cymeuserid = TG.CNetusername;
//                            string cymepassword = TG.CNetpassword;

//                            string cymeDatabackup = @"Data Source=" + cymeservername +                       //Create Connection string
//                                  ";database=" + cymedbname +
//                                  ";User ID=" + cymeuserid +
//                                  ";Password=" + cymepassword;
//                            SqlConnection cymesq = new SqlConnection(cymeDatabackup);

//                            //incrementFeeder = 10;
//                            //completepersentag += incrementFeeder;
//                            string cymeBB = "SELECT DeviceNumber FROM " + cymedbname + ".[dbo].[CYMBREAKER] WHERE [DeviceNumber] Like'%" + METER_SR_NO + "'";
//                            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//                            //string BB = "SELECT *FROM CYME_METER_PEAK_DATA WHERE DATE_TIME LIKE '%:00:00%' ORDER BY DAY, YEAR, MONTH,FORMAT_ID , INTERVAL; ";
//                            SqlCommand cymecmd1 = new SqlCommand(cymeBB, cymesq);
//                            if (cymesq.State == ConnectionState.Closed)
//                            {
//                                cymesq.Open();
//                            }
//                            SqlDataReader cymereader1 = cymecmd1.ExecuteReader();
//                            DataTable cymedttd = new DataTable();
//                            cymedttd.Load(cymereader1);


//                            if (cymedttd.Rows.Count > 0)
//                            {
//                                METER_SR_NO1 = cymedttd.Rows[0]["DeviceNumber"].ToString();
//                            }
//                            else
//                            {
//                                METER_SR_NO1 = METER_SR_NO;
//                            }


//                            // start....


//                            while (k < 48)
//                            {
//                                if (counting <= dttd.Rows.Count)
//                                {
//                                    int sss = k + i;
//                                    if (sss >= 0 && sss < dttd.Rows.Count)
//                                    {
//                                        string meterno = dttd.Rows[k + i]["SERIAL_NUMBER"].ToString();
//                                        //string Days = dttd.Rows[k + i]["DAY"].ToString();
//                                        string Days = Day;
//                                        if (METER_SR_NO == meterno && Day == Days)
//                                        {
//                                            int n = 100;
//                                            int n1 = METER_MF;
//                                            string PF1 = dttd.Rows[k + i]["SYS_PF"].ToString();

//                                            if (PF1 == string.Empty)
//                                            {
//                                                string KWH_READING = dttd.Rows[k + i]["KWH_READING"].ToString();
//                                                string KVAH_READING = dttd.Rows[k + i]["KVAH_READING"].ToString();
//                                                //Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
//                                                float number1 = float.Parse(KWH_READING);
//                                                float number2 = float.Parse(KVAH_READING);
//                                                float number3;
//                                                if (number1 == (float)Convert.ToDouble("0.0000") && number2 == (float)Convert.ToDouble("0.0000"))
//                                                {
//                                                    number3 = (float)Convert.ToDouble("0.00");
//                                                }
//                                                else if (number1 == 0 || number2 == 0)
//                                                {
//                                                    number3 = (float)Convert.ToDouble("0.00");
//                                                }
//                                                else
//                                                {
//                                                    number3 = number1 / number2;
//                                                }
//                                                double PF2 = (double)n * Convert.ToDouble(number3);
//                                                PF += Convert.ToString(PF2) + ",";

//                                            }
//                                            else
//                                            {
//                                                double PF2 = (double)n * Convert.ToDouble(PF1);
//                                                PF += Convert.ToString(PF2) + ",";
//                                            }










//                                            //double PF2 = (double)n * Convert.ToDouble(PF1);
//                                            //PF += Convert.ToString(PF2) + ",";
//                                            //PF += dttd.Rows[k + i]["PF"].ToString() + ",";


//                                            string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                            if (RPH_LCURR1 == string.Empty)
//                                            {
//                                                RPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
//                                                RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";
//                                            }




//                                            string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                            if (YPH_LCURR1 == string.Empty)
//                                            {
//                                                YPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
//                                                YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";
//                                            }



//                                            string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                            if (BPH_LCURR1 == string.Empty)
//                                            {
//                                                BPH_LCURR += "0" + ",";
//                                            }
//                                            else
//                                            {
//                                                double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
//                                                BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";
//                                            }


//                                            counting++;
//                                        }
//                                        else
//                                        {
//                                            int n = 100;
//                                            int n1 = METER_MF;
//                                            //METER_SR_NO111 = METER_SR_NO;
//                                            //string PF1 = dttd.Rows[k + i]["PF"].ToString();
//                                            double PF2 = 0;
//                                            PF += Convert.ToString(PF2) + ",";


//                                            // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                            double RPH_LCURR2 = 0;
//                                            RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


//                                            // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                            double YPH_LCURR2 = 0;
//                                            YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


//                                            //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                            double BPH_LCURR2 = 0;
//                                            BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

//                                        }
//                                    }
//                                    else
//                                    {
//                                        int n = 100;
//                                        int n1 = METER_MF;
//                                        //METER_SR_NO111 = METER_SR_NO;
//                                        //string PF1 = dttd.Rows[k + i]["PF"].ToString();
//                                        double PF2 = 0;
//                                        PF += Convert.ToString(PF2) + ",";


//                                        // string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
//                                        double RPH_LCURR2 = 0;
//                                        RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


//                                        // string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
//                                        double YPH_LCURR2 = 0;
//                                        YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


//                                        //string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
//                                        double BPH_LCURR2 = 0;
//                                        BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

//                                    }
//                                }
//                                k++;
//                            }
//                            PF = PF.TrimEnd(',');
//                            double[] maxValuemaxValuePF = ConvertStringToDoubleArray(PF.TrimEnd(','));
//                            double[] maxValueYPH_LCURR = ConvertStringToDoubleArray(YPH_LCURR.TrimEnd(','));
//                            double[] maxValueBPH_LCURR = ConvertStringToDoubleArray(BPH_LCURR.TrimEnd(','));
//                            double[] maxValueRPH_LCURR = ConvertStringToDoubleArray(RPH_LCURR.TrimEnd(','));

//                            double maxValuemaxValuePF1 = maxValuemaxValuePF.Max();
//                            double maxValueYPH_LCURR1 = maxValueYPH_LCURR.Max();
//                            double maxValueBPH_LCURR1 = maxValueBPH_LCURR.Max();
//                            double maxValueRPH_LCURR1 = maxValueRPH_LCURR.Max();



//                            tw1.Write("8");
//                            tw1.Write("," + METER_SR_NO1);
//                            //tw1.Write("," + dttd.Rows[i]["SERIAL_NUMBER"].ToString().Trim());
//                            tw1.Write("," + "1");
//                            tw1.Write("," + maxValueRPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + maxValueYPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + maxValueBPH_LCURR1);
//                            tw1.Write("," + maxValuemaxValuePF1);
//                            tw1.Write("," + "0");
//                            tw1.Write("," + "0");

//                            tw1.WriteLine();

//                            k = 0;
//                            daay = Convert.ToInt32(Day);
//                            if (daay == 364)
//                            {
//                                break;
//                            }

//                            PF = string.Empty;
//                            RPH_LCURR = string.Empty;
//                            YPH_LCURR = string.Empty;
//                            BPH_LCURR = string.Empty;

//                            // }
//                        }
//                    }
//                    tw1.Close();

//                    reader1.Close();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.ToString());
//                    tw1.Close();
//                }
//                incrementFeeder = 10;
//                completepersentag += incrementFeeder;
//                ProStatus = "MeterDemandText File Completed.....";
//                Thread.Sleep(1000);
//            }

//        }


//        meterPeakFileCreate(GETFILE, TG, ref ProStatus);
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show("Errorsss" + ex);
//    }

//    #endregion

//}

