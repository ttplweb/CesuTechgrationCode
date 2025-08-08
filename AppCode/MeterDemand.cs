using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class MeterDemand
    {
        SqlDataReader dr;
        SqlCommand cmd;

        public string meter(ConfigFileData TG, string st, String FeederID)
        {
            String Fordate = TG.Bfordate;
            String profile_FstartD = TG.Bfromdate;
            String profile_TendD = TG.Btodate;


            string servername = TG.Gisservername;
            string dbname = TG.GisDatabase;
            string userid = TG.Gisusername;
            string password = TG.Gispassword;

            string Databackup = @"Data Source=" + servername +                       //Create Connection string
                         ";database=" + dbname +
                         ";User ID=" + userid +
                         ";Password=" + password;

            SqlConnection con = new SqlConnection(Databackup);

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
                if (TG.checkpeak == "True") /// demand.....
                {
                    if (TG.TYPE1 == "ForDate")
                    {
                        cmd = new SqlCommand("SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(PF)as PF FROM " + dbname + ".[dbo].[TableMeterFeederData] WHERE FEEDER_CODE = '" + FeederID + "' and CAST(DAYPROFILE_DATE AS DATE) = '" + Fordate + "' GROUP BY METER_SR_NO", con);

                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(PF)as PF FROM " + dbname + ".[dbo].[TableMeterFeederData] WHERE FEEDER_CODE = '" + FeederID + "' and CAST(DAYPROFILE_DATE AS DATE) BETWEEN '" + profile_FstartD + "' AND '" + profile_TendD + "' AND DATEPART(MINUTE, DAYPROFILE_DATE) = 0 GROUP BY METER_SR_NO", con);

                    }
                     
                }
                else
                {
                    cmd = new SqlCommand("SELECT  METER_SR_NO, MAX(RPH_LCURR) AS RPH_LCURR, MAX(YPH_LCURR) AS YPH_LCURR, MAX(BPH_LCURR) AS BPH_LCURR, MAX(METER_MF) AS METER_MF, MAX(PF)as PF FROM " + dbname + ".[dbo].[TableMeterFeederData] WHERE FEEDER_CODE = '" + FeederID + "' GROUP BY METER_SR_NO", con);

                }


                // cmd = new SqlCommand("SELECT Distinct [METER_SR_NO],[PF] FROM " + dbname + ".[dbo].[TableMeterFeederData]  where FEEDER_CODE = '"+ FeederID + "'", con);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    sw.Write("8");
                    sw.Write("," + Convert.ToString(dr["METER_SR_NO"]).Trim());
                    sw.Write("," + "1");
                    sw.Write("," + Convert.ToString(dr["RPH_LCURR"]).Trim());
                    sw.Write("," + Convert.ToString(dr["PF"]).Trim());
                    sw.Write("," + Convert.ToString(dr["YPH_LCURR"]).Trim());
                    sw.Write("," + Convert.ToString(dr["PF"]).Trim());
                    sw.Write("," + Convert.ToString(dr["BPH_LCURR"]).Trim());
                    sw.Write("," + Convert.ToString(dr["PF"]).Trim());
                    sw.Write("," + "0");
                    sw.WriteLine();

                }
                sw.Close();
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
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
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }


            return ("1");
        }

        public string Setmeter(ConfigFileData cf, string st, string feederid, OleDbConnection conn,string GETFILE)
        {

            string Switchid = string.Empty;
            string meterid = string.Empty;

            string GISconnectionString = @"Data Source=" + cf.Gisservername +                       //Create Connection string
                  ";database=" + cf.GisDatabase +
                  ";User ID=" + cf.Gisusername +
                  ";Password=" + cf.Gispassword;

            string connectionString = @"Data Source=" + cf.Gisservername +                       //Create Connection string
                  ";database=" + cf.GisDatabase +
                  ";User ID=" + cf.Gisusername +
                  ";Password=" + cf.Gispassword;


            string schema = cf.GisSchema_Name;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    if (obj.State == ConnectionState.Closed)
                    {
                        obj.Open();
                    }
                     
                    string quar = "SELECT Distinct METER_SR_NO FROM " + cf.GisDatabase + "." + schema + ".[TableMeterFeederData]  where FEEDER_CODE ='" + feederid + "' ";
                     
                    DataTable dtt = new DataTable();
                    
                    using (SqlCommand COMM = new SqlCommand(quar, obj))
                    {
                        using (SqlDataReader reader = COMM.ExecuteReader())
                        {
                            Thread.Sleep(500);
                            dtt.Load(reader);
                            Thread.Sleep(500);
                        }
                    }
                     

                    if (dtt.Rows.Count > 0)
                    {
                        meterid = dtt.Rows[0][0].ToString();

                        //string qarrrrrr = @"SELECT sw.objectid FROM " + cf.GisDatabase + "." + schema + ". CircuitSource as cs INNER JOIN " + cf.GisDatabase + "." + schema + ".Switchgear as sw ON sw.OBJECTID = cs.SwitchGearObjID AND cs.FeederID = sw.FeederID WHERE cs.FeederID = '" + feederid + "';";
                        string qarrrrrr = @"Select OBJECTID,XuatTuyen,LoaiTBDC From " + cf.GisDatabase + "." + schema + ".F01_PC03_THIETBIDONGCAT_TT WHERE [XuatTuyen] = '" + feederid + "' and LoaiTBDC in ('1','3') order by OBJECTID;";
                        //string qarrrrrr = @"Select [OBJECTID],[XuatTuyen],[DienAp],[SubstationName] From " + cf.GisDatabase + "." + schema + ".[PC03_DIEMXUATTUYEN] WHERE [XuatTuyen] = '" + feederid + "'";

                        //string ggj11 = GETFILE + "\\load2.txt";
                        //StreamWriter sw11 = File.AppendText(ggj11);
                        //sw11.WriteLine(qarrrrrr);
                        //sw11.Close();
                        using (SqlConnection GISobj = new SqlConnection(GISconnectionString))
                        {
                            if (GISobj.State == ConnectionState.Closed)
                            {
                                GISobj.Open();
                            }

                            SqlCommand Gcom = new SqlCommand(qarrrrrr, GISobj);
                            SqlDataAdapter ad1 = new SqlDataAdapter(Gcom);
                            DataTable dtt1 = new DataTable();
                            ad1.Fill(dtt1);
                            Switchid = dtt1.Rows[0][0].ToString();
                        }
                    }

                   //// MessageBox.Show("error:"+ feederid);
                   // string updatequarry = "UPDATE TGDEVICE_CIRCUITBREAKER SET DeviceNumber ='SG_" + Switchid + "_" + meterid + "' WHERE DeviceNumber='SG_" + Switchid + "';";

                   // ;
                   // //string ggj111 = GETFILE + "\\load3.txt";
                   // //StreamWriter sw111 = File.AppendText(ggj111);
                   // //sw111.WriteLine(updatequarry);
                   // //sw111.Close();
                   // OleDbCommand newww = new OleDbCommand(updatequarry, conn);
                   // newww.ExecuteNonQuery();
                   // if (conn.State == ConnectionState.Closed)
                   // {
                   //     conn.Open();
                   // }

                }



                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + feederid);
                }

                //string updatequarry = "UPDATE TGDEVICE_CIRCUITBREAKER SET DeviceNumber ='SG_" + Switchid + "_" + meterid + "' WHERE DeviceNumber='SG_" + Switchid + "';";

                //;
                //OleDbCommand newww = new OleDbCommand(updatequarry, conn);
                //newww.ExecuteNonQuery();
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}


            }
            return "0";
        }
         
    }
}
