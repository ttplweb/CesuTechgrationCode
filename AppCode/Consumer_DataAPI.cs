using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class Consumer_DataAPI
    {
        public void consumernumber(string NEWGETFILE, ConfigFileData cf)   // consumer data........
        {
            string DataPath = NEWGETFILE + ConfigurationManager.AppSettings["connection"];
            string connectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataPath;

            try
            {
                DataTable DT = new DataTable();
                string nn = "select CustomerNumber from TG_CUSTOMERLOADS";

                using (OleDbConnection connection3 = new OleDbConnection(connectionstring))
                {
                    OleDbCommand command3 = new OleDbCommand(nn, connection3);

                    if (connection3.State == ConnectionState.Closed)
                    {
                        connection3.Open();
                    }

                    OleDbDataReader dr1 = command3.ExecuteReader();
                    DT.Load(dr1);
                }

                // Call GetApiData.CustomerData asynchronously
                DataTable getApiDataCustomer = CustomerData(DT);

                InsertCustomerDataTable(getApiDataCustomer, cf);


                // Optionally process getApiDataCustomer or return it
                // return DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex);
                //return null;
            }
        }
        public DataTable CustomerData(DataTable dt)   // consumernumber match in api......
        {
            DataTable retdata = new DataTable();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string consumernumber = dt.Rows[i][0].ToString();

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.176:5805/api/Home/ConsumerData");
                    var content = new StringContent("{\r\n  \"consumer_number\": \"" + consumernumber + "\"\r\n}", Encoding.UTF8, "application/json");
                    request.Content = content;

                    // Send the HTTP request synchronously
                    var response = client.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();

                    // Read the JSON response content synchronously
                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    // Convert JSON string to DataTable
                    DataTable dataTable = ConvertJsonToDataTable(jsonString);
                    retdata = dataTable.Copy();
                    //dataTable.Clear();
                }
                return retdata;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public static DataTable ConvertJsonToDataTable(string jsonString)  // convert json to dt....
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Check if the JSON string represents an array or an object
                var json = JsonConvert.DeserializeObject<JToken>(jsonString);

                if (json is JArray)
                {
                    // If it's an array, iterate through the array
                    JArray jsonArray = (JArray)json;

                    // Create columns based on first object in the JSON array
                    if (jsonArray.Count > 0)
                    {
                        JObject firstObject = (JObject)jsonArray[0];
                        foreach (JProperty property in firstObject.Properties())
                        {
                            dataTable.Columns.Add(property.Name, typeof(string)); // Adjust column type as needed
                        }

                        // Populate DataTable
                        foreach (JObject obj in jsonArray)
                        {
                            DataRow row = dataTable.NewRow();
                            foreach (JProperty property in obj.Properties())
                            {
                                row[property.Name] = property.Value.ToString(); // Adjust value conversion as needed
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }
                else if (json is JObject)
                {
                    // If it's an object, treat it as a single object
                    JObject jsonObject = (JObject)json;

                    // Create columns based on properties of the JSON object
                    foreach (JProperty property in jsonObject.Properties())
                    {
                        dataTable.Columns.Add(property.Name, typeof(string)); // Adjust column type as needed
                    }

                    // Populate DataTable with a single row
                    DataRow row = dataTable.NewRow();
                    foreach (JProperty property in jsonObject.Properties())
                    {
                        row[property.Name] = property.Value.ToString(); // Adjust value conversion as needed
                    }
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting JSON to DataTable: {ex.Message}");
                return null;
            }

            return dataTable;
        }
        public void InsertCustomerDataTable(DataTable APIDT, ConfigFileData TG)       // Insert Customer_Data Table 
        {
            #region
            try
            {
                string servername = TG.TGservername;
                string dbname = TG.TGdatabase;
                string userid = TG.TGusername;
                string password = TG.TGpassword;
                string connectionmeter = @"Data Source=" + servername +                       //Create Connection string
                      ";database=" + dbname +
                      ";User ID=" + userid +
                      ";Password=" + password;
                SqlConnection sq = new SqlConnection(connectionmeter);
                sq.Open();

                string truncateQuery = $"TRUNCATE TABLE " + dbname + ".[dbo].[Cyme_Meter_Data]";    // TRUNCATE OLD TABLE
                SqlCommand command = new SqlCommand(truncateQuery, sq);
                command.ExecuteNonQuery();

                DataTable dt = APIDT;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string z_name = dt.Rows[j]["z_name"].ToString();
                    string c_name = dt.Rows[j]["c_name"].ToString();
                    string d_name = dt.Rows[j]["d_name"].ToString();
                    string s_name = dt.Rows[j]["s_name"].ToString();
                    string bu = dt.Rows[j]["bu"].ToString();
                    string consumer_number = dt.Rows[j]["consumer_number"].ToString();
                    string consumer_type = dt.Rows[j]["consumer_type"].ToString();
                    string feeder_number = dt.Rows[j]["feeder_number"].ToString();
                    string substation_number = dt.Rows[j]["substation_number"].ToString();
                    string transformer_number = dt.Rows[j]["transformer_number"].ToString();
                    string tariff_code = dt.Rows[j]["tariff_code"].ToString();
                    string sanctioned_load_kw = dt.Rows[j]["sanctioned_load_kw"].ToString();
                    string connected_load_kw = dt.Rows[j]["connected_load_kw"].ToString();
                    string bill_year = dt.Rows[j]["bill_year"].ToString();
                    string bill_month = dt.Rows[j]["bill_month"].ToString();
                    string consumption = dt.Rows[j]["consumption"].ToString();


                    string PP = "INSERT INTO [" + dbname + "].[dbo].[Cyme_Meter_Data] (z_name,c_name,d_name,s_name,bu,consumer_number,consumer_type,feeder_number,substation_number,transformer_number,tariff_code,sanctioned_load_kw,connected_load_kw,bill_year,bill_month,consumption) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)";

                    SqlCommand cmd = new SqlCommand(PP, sq);
                    cmd.Parameters.AddWithValue("@P1", z_name);
                    cmd.Parameters.AddWithValue("@P2", c_name);
                    cmd.Parameters.AddWithValue("@P3", d_name);
                    cmd.Parameters.AddWithValue("@P4", s_name);
                    cmd.Parameters.AddWithValue("@P5", bu);
                    cmd.Parameters.AddWithValue("@P6", consumer_number);
                    cmd.Parameters.AddWithValue("@P7", consumer_type);
                    cmd.Parameters.AddWithValue("@P8", feeder_number);
                    cmd.Parameters.AddWithValue("@P9", substation_number);
                    cmd.Parameters.AddWithValue("@P10", transformer_number);
                    cmd.Parameters.AddWithValue("@P11", tariff_code);
                    cmd.Parameters.AddWithValue("@P12", sanctioned_load_kw);
                    cmd.Parameters.AddWithValue("@P13", connected_load_kw);
                    cmd.Parameters.AddWithValue("@P14", bill_year);
                    cmd.Parameters.AddWithValue("@P15", bill_month);
                    cmd.Parameters.AddWithValue("@P16", consumption);

                    cmd.ExecuteNonQuery();


                }
                if (sq.State == ConnectionState.Open)
                {
                    sq.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
            }
            #endregion
        }
        public void CreateMeterTextFile(string GETFILE, ConfigFileData cfd)   // Create Profile_Data NetworkText File ........
        {
            #region
            try
            {
                string servername = cfd.TGservername;
                string dbname = cfd.TGdatabase;
                string userid = cfd.TGusername;
                string password = cfd.TGpassword;
                string connectionmeter = @"Data Source=" + servername +                       //Create Connection string
                      ";database=" + dbname +
                      ";User ID=" + userid +
                      ";Password=" + password;
                SqlConnection sq = new SqlConnection(connectionmeter);

                string BB = "select * from Cyme_Meter_Data";
                using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                {
                    string Date1 = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                    //string Date11 = "ss";
                    StreamWriter tw1 = File.AppendText(GETFILE + "\\MeterData File\\MeterData" + Date1 + ".txt");
                    try
                    {
                        sq.Open();
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                        DataTable dttd = new DataTable();
                        dttd.Load(reader1);
                        string str = @"[METERDEMAND SETTING]
FORMAT_METERDEMANDSETTING=DeviceType,DeviceNumber,DemandType,Value1A,Value2A,Value1B,Value2B,Value1C,Value2C,Value1ABC,Value2ABC,Disconnected,ReferenceTime";
                        tw1.WriteLine(str);
                        for (int i = 0; i < dttd.Rows.Count; i++)
                        {
                            tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                            tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                            tw1.WriteLine();
                        }
                        tw1.Close();

                        reader1.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        tw1.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorsss" + ex);
            }
            #endregion
        }
    }
}
