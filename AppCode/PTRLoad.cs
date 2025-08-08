using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace TechGration.AppCode
{
    class PTRLoad
    {

        public void setSpotLoad(OleDbConnection connectionstring, ConfigFileData cf, string NEWGETFILE,string Feedreidnet)
        {
            //cable

            try
            {
                string servername = cf.Gisservername;
                string Database = cf.GisDatabase;
                string userid = cf.Gisusername;
                string password = cf.Gispassword;


                string connectionString = @"Data Source=" + servername +
                         ";Initial Catalog=" + Database +
                         ";User ID=" + userid +
                         ";Password=" + password;


                string query = string.Empty;
                string query1 = string.Empty;

                DataTable SQLPTRDT = new DataTable();
                DataTable PTRDT = new DataTable();
                DataTable PTRDT2 = new DataTable();
                DataTable PTRDT21 = new DataTable();



                string filePath = cf.DTCSV_PATH;
             
                DataTable dataTableLoad = new DataTable();

                // Read all lines from the CSV
                var lines = File.ReadAllLines(filePath);

                if (lines.Length == 0) return;

                // Assume first line is header
                var headers = lines[0].Split(',');
                foreach (var header in headers)
                {
                    dataTableLoad.Columns.Add(header.Trim());
                }

                // Add data rows
                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    if (values.Length == headers.Length) // Ensure row has correct number of columns
                    {
                        dataTableLoad.Rows.Add(values);
                    }
                }



                String ServicePointIDCON = string.Empty;
                String ConsumerNumber = string.Empty;
                String CustomerType = string.Empty;
                String OBJECTID = string.Empty;
                String FeederId1 = string.Empty;
                String ServicePointIDLS = string.Empty;
                String Phasedesignation = "7";
                string SanctionedLoadKW = string.Empty;
                String UPATESanctionedLoadKW = string.Empty;
                string XnodeL = string.Empty;
                String YnodeL = string.Empty;

                string FromNodeID = string.Empty;
                string ToNodeID = string.Empty;

                string FromNodeX = string.Empty;
                String FromNodeY = string.Empty;
                string ToNodeX = string.Empty;
                String ToNodeY = string.Empty;

                String XY = string.Empty;
                String FROMNODE = string.Empty;
                String TONODE = string.Empty;
                String MaHieu = string.Empty;
                String TransformerID = string.Empty;
                String PF = string.Empty;
                String KVA = string.Empty;
                String KW = string.Empty;
                String FeederID = string.Empty;
                String OBJECTIDEX = string.Empty;





                string key = cf.tempkey;

                double PFW = Convert.ToDouble(cf.IND);
                int count = 0;

                for (int K = 0; K < dataTableLoad.Rows.Count; K++)
                {
                  
                    OBJECTIDEX = dataTableLoad.Rows[K]["OBJECTID"].ToString();
                    TransformerID = dataTableLoad.Rows[K]["TransformerID"].ToString();
                    FeederID = dataTableLoad.Rows[K]["FeederID"].ToString();
                    KW = dataTableLoad.Rows[K]["KW"].ToString();
                    KVA = dataTableLoad.Rows[K]["KVA"].ToString();
                    PF = dataTableLoad.Rows[K]["PF"].ToString();

                    if (TransformerID == "CD530111")
                    {
                        KW = Convert.ToString(95);
                        KVA = Convert.ToString(100);
                        PF =  Convert.ToString(95);

                    }
                    if (TransformerID == "CD530218")
                    {
                        KW = Convert.ToString(237.5);
                        KVA = Convert.ToString(250);
                        PF = Convert.ToString(95);
                    }

                    if (KW != "0" && KVA != "0" && PF != "0")
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            //query = "SELECT D.XuatTuyen, D.SHAPE, D.OBJECTID, D.IDTramBienAp, T.ID, T.IdPMIS FROM "+ Database + ".[dbo].[F04_PC03_MAYBIENAP_TT] AS D INNER JOIN [Vietnam_ASIS].[dbo].[F05_PC03_TRAMBIENAP_TT] AS T ON D.IDTramBienAp = T.ID WHERE T.IdPMIS = '" + TransformerID + "' AND D.XuatTuyen = '" + FeederID + "';";

                            string query11 = @"SELECT D.XuatTuyen as FeederId, D.OBJECTID as OBJECTID, D.IDTramBienAp, 
                            D.SoSerial, D.X, D.Y, 
                            T.ID, T.IdPMIS 
                     FROM " + Database + @".[dbo].[F04_PC03_MAYBIENAP_TT] AS D
                     INNER JOIN [Vietnam_ASIS].[dbo].[F05_PC03_TRAMBIENAP_TT] AS T 
                     ON D.IDTramBienAp = T.ID 
                     WHERE T.IdPMIS = @TransformerID 
                     AND D.XuatTuyen = @FeederID";


                            SqlCommand com = new SqlCommand(query11, conn);
                            com.Parameters.AddWithValue("@TransformerID", TransformerID);
                            com.Parameters.AddWithValue("@FeederID", FeederID);

                            SqlDataAdapter da = new SqlDataAdapter(com);
                            //DataTable PTRDT = new DataTable();
                            da.Fill(SQLPTRDT);

                            if (SQLPTRDT.Rows.Count > 0)
                            {
                                for (int i = 0; i < SQLPTRDT.Rows.Count; i++)
                                {
                                    OBJECTID = SQLPTRDT.Rows[i]["OBJECTID"].ToString();
                                    FeederId1 = SQLPTRDT.Rows[i]["FeederId"].ToString();

                                    if (OBJECTID == "1034")
                                    {

                                    }

                                }

                                query = "SELECT * from GIS_F04_PC03_MAYBIENAP_TT where OBJECTID='DT_" + OBJECTID + "' and XuatTuyen = '" + FeederId1 + "' ORDER BY OBJECTID";
                                OleDbDataReader odr;
                                //  OleDbConnection oleDbConnection = new OleDbConnection(connectionstring.ToString());

                                OleDbCommand oleDbCommand = new OleDbCommand(query, connectionstring);

                                OleDbDataAdapter ad = new OleDbDataAdapter(oleDbCommand);
                                ad.Fill(PTRDT);


                                if (PTRDT.Rows.Count > 0)
                                {
                                    for (int i = 0; i < PTRDT.Rows.Count; i++)
                                    {
                                        count = count + 1;

                                        OBJECTID = PTRDT.Rows[i]["OBJECTID"].ToString();
                                        ConsumerNumber = PTRDT.Rows[i]["SoSerial"].ToString();

                                        if (ConsumerNumber == "T00009481")
                                        {

                                        }

                                        XnodeL = PTRDT.Rows[i]["X"].ToString();
                                        YnodeL = PTRDT.Rows[i]["Y"].ToString();

                                        if (CustomerType == "")
                                        {
                                            CustomerType = "R";
                                        }

                                        XY = XnodeL + '_' + YnodeL;

                                        //cable....

                                        try
                                        {
                                            string mmqur = "SELECT OBJECTID,FromNodeX,FromNodeY,ToNodeX,ToNodeY from GIS_F09_PC03_DUONGDAY_TT Where FromNodeX='" + XnodeL + "' or  FromNodeY='" + YnodeL + "' or ToNodeX='" + XnodeL + "' or ToNodeY='" + YnodeL + "'ORDER BY OBJECTID";
                                            //nik....  //string mmqur = "SELECT OBJECTID,FromNodeX,FromNodeY,ToNodeX,ToNodeY from GIS_CABLE Where FromNodeX='" + XnodeL + "' or  FromNodeY='" + YnodeL + "' or ToNodeX='" + XnodeL + "' or ToNodeY='" + YnodeL + "'";
                                            OleDbCommand oleDbCommand2 = new OleDbCommand(mmqur, connectionstring);
                                            OleDbDataAdapter ad2 = new OleDbDataAdapter(oleDbCommand2);
                                            ad2.Fill(PTRDT2);

                                            if (PTRDT2.Rows.Count > 0)
                                            {
                                                for (int k = 0; k < PTRDT2.Rows.Count; k++)
                                                {
                                                    //OBJECTID = PTRDT2.Rows[k]["OBJECTID"].ToString();
                                                    FromNodeX = PTRDT2.Rows[k]["FromNodeX"].ToString();
                                                    FromNodeY = PTRDT2.Rows[k]["FromNodeY"].ToString();
                                                    ToNodeX = PTRDT2.Rows[k]["ToNodeX"].ToString();
                                                    ToNodeY = PTRDT2.Rows[k]["ToNodeY"].ToString();
                                                    string Location = "0";

                                                    //nik....
                                                    FROMNODE = FromNodeX + '_' + FromNodeY;
                                                    TONODE = ToNodeX + '_' + ToNodeY;
                                                    int Value2 = 0;

                                                    if (XY == FROMNODE || XY == TONODE)
                                                    {

                                                        string strsql = "insert into TGLOAD_LOADS([SECTIONID] ,[DeviceNumber] ,[LoadType] ,[Connection_] , [Location]) values('" + OBJECTID + "' , '" + TransformerID + "' , '" + "SPOT" + "' , '" + 0 + "' ,'" + Location + "')";
                                                        string strsql1 = "insert into TG_CUSTOMERLOADS ([SECTIONID] ,[DeviceNumber] ,[LoadType],[CustomerNumber],[CustomerType], [ConnectionStatus],[LockDuringLoadAllocation] ,[ValueType] ,[KWH] ,[NumberOfCustomer],LoadPhase,ConnectedKVA,Value1,Value2) values('" + OBJECTID + "' , '" + TransformerID + "' , '" + "SPOT" + "' , '" + ConsumerNumber + "', '" + CustomerType + "' , '" + 0 + "'  ,'" + 0 + "' , '" + 2 + "' , '" + 0 + "' , '" + 1 + "', '" + Phasedesignation + "' , '" + KVA + "','" + KW + "', '" + PF + "')";


                                                        OleDbCommand command1 = new OleDbCommand(strsql, connectionstring);
                                                        command1.ExecuteNonQuery();

                                                        OleDbCommand command21 = new OleDbCommand(strsql1, connectionstring);
                                                        command21.ExecuteNonQuery();

                                                    }

                                                }

                                            }


                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        PTRDT2.Clear();

                                    }


                                }

                                PTRDT.Clear();
                                SQLPTRDT.Clear();


                            }


                        }

                    }
                     
                }

                 
                string Acount = count.ToString();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }

        public void CreateSpotLoad(OleDbConnection connectionstring, ConfigFileData cf, string NEWGETFILE, string Feedreidnet)
        {
            try
            {
                string query1 = string.Empty;
                string query = string.Empty;
                DataTable dt = new DataTable();
                DataTable dtTab = new DataTable();
              
                query = "SELECT OBJECTID,CongSuat from GIS_F04_PC03_MAYBIENAP_TT";

                //  OleDbConnection oleDbConnection = new OleDbConnection(connectionstring.ToString());

                
                OleDbCommand oleDbCommand = new OleDbCommand(query, connectionstring);

              
                OleDbDataAdapter ad = new OleDbDataAdapter(oleDbCommand);
               
               
                ad.Fill(dtTab);

                for (int i = 0; i < dtTab.Rows.Count; i++)
                {
                    string OBJECTID = dtTab.Rows[i]["OBJECTID"].ToString();
                    string Device = "OH_" + OBJECTID;
                    string CongSuat = dtTab.Rows[i]["CongSuat"].ToString();

                    query1 = "SELECT DeviceNumber from TG_CUSTOMERLOADS where SECTIONID='" + OBJECTID+"'";
                    OleDbCommand oleDbCommand1 = new OleDbCommand(query1, connectionstring);
                    OleDbDataAdapter ad1 = new OleDbDataAdapter(oleDbCommand1);
                    ad1.Fill(dt);

                    if (dt.Rows.Count==0)
                    {
                        double KVAee = Convert.ToDouble(CongSuat);
                        string PFF = "95";
                        double KWW = KVAee * .95;




                        string strsql = "insert into TGLOAD_LOADS([SECTIONID] ,[DeviceNumber] ,[LoadType] ,[Connection_] , [Location]) values('" + OBJECTID + "' , '" + OBJECTID +  "' , '" + "SPOT" + "' , '" + 0 + "' ,'0')";
                        string strsql1 = "insert into TG_CUSTOMERLOADS ([SECTIONID] ,[DeviceNumber] ,[LoadType],[CustomerNumber],[CustomerType], [ConnectionStatus],[LockDuringLoadAllocation] ,[ValueType] ,[KWH] ,[NumberOfCustomer],LoadPhase,ConnectedKVA,Value1,Value2) values('" + OBJECTID + "' , '" + OBJECTID + "' , '" + "SPOT" + "' , '" + OBJECTID + "', 'R' , '" + 0 + "'  ,'" + 0 + "' , '" + 2 + "' , '" + 0 + "' , '" + 1 + "', 'ABC' , '" + KVAee + "','" + KWW + "', '" + PFF + "')";




                        OleDbCommand command1 = new OleDbCommand(strsql, connectionstring);
                        command1.ExecuteNonQuery();

                        OleDbCommand command21 = new OleDbCommand(strsql1, connectionstring);
                        command21.ExecuteNonQuery();
                    }
                    dt.Clear();
                }
               

            }
            catch (Exception ex)
            {

               
            }
             
        }
        public void PTRSecondaryNodeLoad(string getfile)
        {
            try
            {
                string DataPath = getfile + ConfigurationManager.AppSettings["connection"];
                string connectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataPath;
                string query = string.Empty;
                DataTable PTRDT = new DataTable();
                double angle;
                string NewNode;
               
                String DeviceStage = "";
                int Flags = 0;
                int InitFromEquipFlags = 0;
                String LineCableID = "3P";
                string Length=string.Empty;
                int ConnectionStatus = 0;
                string CoordX = null;
                string CoordY = null;
                int HarmonicModel = 2;
                int FlowConstraintActive = 0;
                int FlowConstraintUnit = 0;
                int MaximumFlow = 100;
                int SeriesCompensationActive = 0;
                int MaxReactanceMultiplier = 50;
                int SeriesCompensationCost = 0;
                String Voltage = "";
                String ConductorSize = "";
                String ConductorType = "";
                String ConductorMaterial = "";
                string rm_character = string.Empty;

                query = "select [SectionID],[DeviceNumber],[SecondaryBaseVoltage] from [TGDEVICE_DISTRIBUTIONTRANSFORMER]";

                //query = "select [NodeId],[GISID] from [TempNode] ";

                #region
                using (OleDbConnection conn = new OleDbConnection(connectionstring))
                {
                    using (OleDbCommand oleDbCommand = new OleDbCommand(query, conn))
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        OleDbDataReader odr = oleDbCommand.ExecuteReader();
                        System.Threading.Thread.Sleep(200);
                        PTRDT.Load(odr);
                        System.Threading.Thread.Sleep(200);
                    }
                }
                #endregion
                if (PTRDT.Rows.Count > 0)
                {

                    for (int im=0; im < PTRDT.Rows.Count; im++)
                    {
                        string sectionId = string.Empty;
                        string deviceNumber = string.Empty;
                        string TodeId = string.Empty;
                        string Oldid = string.Empty;
                        string SecondaryBaseVoltage = string.Empty;
                        //string NodeId = string.Empty;
                        //string GISID = string.Empty;
                        sectionId = PTRDT.Rows[im][0].ToString().Trim();

                        if (!sectionId.Contains("OH"))
                        {
                            if (sectionId.Contains("CA"))
                            {
                                sectionId = sectionId.Replace("CA", "OH");
                            }
                            if (sectionId.Contains("BB"))
                            {
                                sectionId = sectionId.Replace("BB", "OH");
                            }
                        }
                         
                        SecondaryBaseVoltage = PTRDT.Rows[im][2].ToString().Trim();

                        if (1 == 1)
                        {
                            deviceNumber = PTRDT.Rows[im][1].ToString().Trim();

                            string qquery = "select ToNodeY,ToNodeX,FromNodeX,FromNodeY from [GIS_F09_PC03_DUONGDAY_TT] where [OBJECTID]='" + sectionId + "'";

                            DataTable dtr = new DataTable();
                            #region
                            try
                            {
                                using (OleDbConnection oleDbConnection = new OleDbConnection(connectionstring))
                                {
                                    using (OleDbCommand oleDbCommand = new OleDbCommand(qquery, oleDbConnection))
                                    {
                                        if (oleDbConnection.State != ConnectionState.Open)
                                        {
                                            oleDbConnection.Open();
                                        }
                                        OleDbDataReader odr = oleDbCommand.ExecuteReader();
                                        System.Threading.Thread.Sleep(100);
                                        dtr.Load(odr);
                                        System.Threading.Thread.Sleep(200);

                                        if (dtr.Rows.Count > 0)
                                        {
                                            string ToNodeY = dtr.Rows[0]["ToNodeY"].ToString();
                                            string ToNodeX = dtr.Rows[0]["ToNodeX"].ToString();
                                            string FromNodeY = dtr.Rows[0]["FromNodeY"].ToString();
                                            string FromNodeX = dtr.Rows[0]["FromNodeX"].ToString();


                                            angle = Angulo(ToNodeX, ToNodeY, FromNodeX, FromNodeY);
                                            string vang = angle.ToString();
                                            TodeId = getcoordinate(ToNodeX, ToNodeY, vang);


                                            Oldid = ToNodeX + "_" + ToNodeY;

                                        }
                                         
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                               // MessageBox.Show("error1:" + ex);
                            }

                            #endregion

                            if (!string.IsNullOrWhiteSpace(TodeId))
                            {
                                //double val = 0.000013;
                                //double val1 = -0.000014;
                                string[] str1 = TodeId.Split('_');
                                string valueaa = str1[0].ToString();
                                string valuebb = str1[1].ToString();
                                //val += double.Parse(valuebb);
                                //val1 += double.Parse(valueaa);
                                string main = string.Empty;
                                //main = val1 + "_" + val;

                                #region


                                string queryy = "insert into TempNode (NodeId ,X , Y ) values ('" + TodeId + "' , '" + valueaa + "' , '" + valuebb + "')";
                                try
                                {
                                    using (OleDbConnection connection = new OleDbConnection(connectionstring))
                                    {
                                        OleDbCommand command = new OleDbCommand(queryy, connection);
                                        if (connection.State != ConnectionState.Open)
                                        {
                                            connection.Open();
                                        }
                                        command.ExecuteNonQuery();
                                        System.Threading.Thread.Sleep(200);
                                        connection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show("eeerror:" + ex);
                                }


                                //int k = 0;
                                String query11 = "insert into [TEMPSECTION-NOTBREAKED] (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + deviceNumber + "' , '" + Oldid + "','0','" + TodeId + "','0','ABC','0')";
                                try
                                {
                                    using (OleDbConnection connection1 = new OleDbConnection(connectionstring))
                                    {
                                        OleDbCommand command = new OleDbCommand(query11, connection1);
                                        if (connection1.State != ConnectionState.Open)
                                        {
                                            connection1.Open();
                                        }
                                        command.ExecuteNonQuery();
                                        System.Threading.Thread.Sleep(200);
                                        connection1.Close();
                                    }
                                    TodeId = string.Empty;
                                }
                                catch (Exception ex)
                                {
                                   // MessageBox.Show("error:" + ex);
                                }

                                #endregion
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:" + ex);
            }

        }

        public double Angulo(string ToNodeX, string ToNodeY, string FromNodeX, string FromNodeY)
        {
            double degrees;
            // double annnggg;
            double x1 = double.Parse(ToNodeX);
            double x2 = double.Parse(FromNodeX);
            double y1 = double.Parse(ToNodeY);
            double y2 = double.Parse(FromNodeY);



            // Avoid divide by zero run values.
            if (x2 - x1 == 0)
            {
                if (y2 > y1)
                    degrees = 90;
                else
                    degrees = 270;
            }
            else
            {
                // Calculate angle from offset.
                double riseoverrun = (double)(y2 - y1) / (double)(x2 - x1);

                double radians = Math.Atan(riseoverrun);
                degrees = radians * Convert.ToDouble(180 / Math.PI);
                degrees = degrees * (-1);
                //annnggg=degrees.ToString().Remove('');

                //Handle quadrant specific transformations.       
                if ((x2 - x1) < 0 || (y2 - y1) < 0)
                    degrees += 180;
                if ((x2 - x1) > 0 && (y2 - y1) < 0)
                    degrees -= 180;
                if (degrees < 0)
                    degrees += 360;
            }
           // degrees = Math.Round(degrees, 4);
            return degrees;
        }
        public string getcoordinate(string x1, string y1, string angledir)
        {

            double x = Convert.ToDouble(x1);
            double y = Convert.ToDouble(y1);

            double angle = 180 - Convert.ToDouble(angledir);

            double distance = 0.80;

            double angleInRadians = angle * Math.PI / 180;

            double deltaX = distance * Math.Cos(angleInRadians);
            double deltaY = distance * Math.Sin(angleInRadians);

            double newX = x + deltaX;
            double newY = y + deltaY;
            string noooo = newX.ToString() + '_' + newY.ToString();
            //string noooo = Math.Round(newX, 4).ToString() + '_' + Math.Round(newY, 4).ToString();
            return noooo;

        }

        private void loadVoltage(OleDbConnection connn2) //Load...... 20
        {
            //Load...... 20
            try
            {
                string quarry = "select Distinct CL.DeviceNumber as OBJECTID ,SP.Voltage from GIS_SERVICEPOINT as SP INNER JOIN TG_CUSTOMERLOADS as CL ON  SP.BuildingID = CL.DeviceNumber";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                string OBJECTID = string.Empty;
                string DataId = "VOLTAGE";
                //string DataType = string.Empty;
                string DataValue = string.Empty;
                string Voltage = string.Empty;
                int DeviceType = 20;
                int DataType = 8;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataValue = dt.Rows[i]["Voltage"].ToString();
                    OBJECTID = dt.Rows[i]["OBJECTID"].ToString();

                    GetAllDeviceData vv = new GetAllDeviceData();
                    DataValue = vv.updatevoltagecodevalue(DataValue);
                    //DataValue = updatevoltagecodevalue(DataValue);
                    //DataValue = (Voltage.ToUpper()).Replace(" KV", "");

                    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", OBJECTID);
                    comr.Parameters.AddWithValue("@P2", DeviceType);
                    comr.Parameters.AddWithValue("@P3", DataId);
                    comr.Parameters.AddWithValue("@P4", DataType);
                    comr.Parameters.AddWithValue("@P5", DataValue);


                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "LoadVoltage_Error");

            }
        }

    }
}
