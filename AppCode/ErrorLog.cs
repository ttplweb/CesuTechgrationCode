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
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class ErrorLog
    {
        public string combineErrorLog(string Cyme_config_txtReportDirectory, string GETFILE, int count, string feedrid)
        {
            string[] Fid1111 = feedrid.Split('_');
            string Fid1 = Fid1111[0].Replace("/", "-");
            string path = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\ErrorLog\\FeederErrorlog.log";
            string errorlog = File.ReadAllText(path);
            StreamWriter sw = File.AppendText(Cyme_config_txtReportDirectory);
            sw.WriteLine(errorlog);
            sw.Close();
            return "1";
        }
        internal void combineErrorLog(string logpath, string gETFILE, string feederID1)
        {
            //throw new NotImplementedException();
            //string.Replace("/", "\\")
            string Fid1 = feederID1.Replace("/", "-");
            string path = gETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\ErrorLog\\FeederErrorlog.log";
            string errorlog = File.ReadAllText(path);
            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine(errorlog);
            sw.Close();
            //return "1";
        }
        public string fir(string Cyme_config_txtReportDirectory, string id, ref DateTime Startime)
        {

            Startime = DateTime.Now;
            string log = @"              ==================================================================
              Product Information
              Tech-Gration Error Log File for VIETNAM 
              Version 8.1
              ==================================================================


              Extraction process

              Beginning Date " + DateTime.Now.ToString("dd/MM/yyyy") +
              @"
              Beginning time " + DateTime.Now.ToString("hh:mm:ss:tt") +
@"

Total Selected Network - " + id + "";


            string mainPath = Cyme_config_txtReportDirectory + "\\Tech_Gration_LogFile_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".log";

            StreamWriter read = File.AppendText(mainPath);

            read.Write(log);
            read.Close();

            return (mainPath);
        }

        public string nav(string Cyme_config_txtReportDirectory, string feeder)
        {
            string log1 =

@"
------------------------------------------------------------------

FeederID                             " + feeder +
@"         
                       
Start DateTime                       " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss:tt") +
@"                                                                              
";





            StreamWriter log = File.AppendText(Cyme_config_txtReportDirectory);

            log.Write(log1);
            log.Close();
            return ("1");
        }

        public string cir(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {

            //string servername = TG.TGservername;
            //string dbname = TG.TGdatabase;
            //string userid = TG.TGusername;
            //string password = TG.TGpassword;

            //string Databackup = @"Data Source=" + servername +                       //Create Connection string
            //      ";database=" + dbname +
            //      ";User ID=" + userid +
            //      ";Password=" + password;
            //SqlConnection sq = new SqlConnection(Databackup);
            //sq.Open();

            string AA = "select OBJECTID,DienAp,Phasedesignation,RatingAmps,Type,FeederId from GIS_F01_PC03_THIETBIDONGCAT_TT";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    string query = "SELECT FeederId,OBJECTID, COUNT(*) FROM GIS_SWITCH GROUP BY OBJECTID,FeederId  HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt1 = new DataTable();
                        //DataTable dt1 = new DataTable();
                        //dt1.Load(dr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

SWITCH OBJECTID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ";

                                read.Write(write);
                                read.Close();


                            }
                        }
                    }
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim();//Voltage
                        string bb = dt.Rows[i][1].ToString().Trim();//OBJECTID
                        string cc = dt.Rows[i][2].ToString().Trim();//Phasedesignation
                        string dd = dt.Rows[i][3].ToString().Trim();//Phasedesignation
                        string ee = dt.Rows[i][4].ToString().Trim();//Phasedesignation
                        string ff = dt.Rows[i][5].ToString().Trim();//Phasedesignation

                        if (cc == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH OBJECTID-" + bb + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();


                        }
                        if (dd == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH OBJECTID-" + bb + "     Data Not Avilable in RatingAmps";

                            read.Write(write);
                            read.Close();

                        }

                        if (aa == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH OBJECTID-" + bb + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();

                        }

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }


            }

            return ("1");
        }

        public string LTConductor(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData cf)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string con = st + ConfigurationManager.AppSettings["connection"];
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            //// string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";


            //OleDbConnection conn = new OleDbConnection(connString);

            //command
            int ab = 0;
            string AA = "select Phasedesignation,Voltage ,ConnectionStatus,GISID , Phasedesignation , Voltage , ConductorSize , ConductorType , ConductorMaterial , Configuration from GIS_LTCONDUCTOR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {


                    string query = "SELECT OBJECTID , COUNT(*) FROM GIS_CONDUCTOR GROUP BY OBJECTID HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt = new DataTable();
                        //dt.Load(dr);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

LTCONDUCTOR GISID-" + dr1["GISID"] + "     This GISID is Duplicate ";

                                read.Write(write);
                                read.Close();
                            }
                        }
                    }

                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim();
                        string bb = dt.Rows[i][1].ToString().Trim();
                        string cc = dt.Rows[i][2].ToString().Trim();
                        //string dd = dt.Rows[i][3].ToString();
                        string EE = dt.Rows[i][3].ToString().Trim();

                        string Phasedesignation = dt.Rows[i][4].ToString().Trim();
                        string Voltage = dt.Rows[i][5].ToString().Trim();
                        string ConductorSize = dt.Rows[i][6].ToString().Trim();
                        string ConductorType = dt.Rows[i][7].ToString().Trim();
                        string ConductorMaterial = dt.Rows[i][8].ToString().Trim();
                        string Configuration = dt.Rows[i][9].ToString().Trim();





                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || Phasedesignation == "" || Voltage == "" || ConductorSize == "" || ConductorType == "" || ConductorMaterial == "" || Configuration == "")
                            {
                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();
                                ab++;
                            }
                        }

                        if (aa == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();

                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in TG_VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }


                        if (cc == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONNECTIONSTATE";

                            read.Write(write);
                            read.Close();

                        }


                        if (Phasedesignation == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                            read.Write(write);
                            read.Close();

                        }
                        if (Voltage == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }
                        if (ConductorSize == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORSIZE";

                            read.Write(write);
                            read.Close();

                        }
                        if (ConductorType == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORTYPE";

                            read.Write(write);
                            read.Close();

                        }
                        if (ConductorMaterial == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORMATERIAL";

                            read.Write(write);
                            read.Close();

                        }
                        if (Configuration == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONFIGURATION";

                            read.Write(write);
                            read.Close();

                        }





                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                }



            }

            return ("1");
        }

        public string OvereHead(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {
            int ab = 0;
            //OBJECTID,Voltage,NumberofCore,CableSize,InsulationType,ConductorMaterial
            //string servername = TG.TGservername;
            //string dbname = TG.TGdatabase;
            //string userid = TG.TGusername;
            //string password = TG.TGpassword;

            //string Databackup = @"Data Source=" + servername +                       //Create Connection string
            //      ";database=" + dbname +
            //      ";User ID=" + userid +
            //      ";Password=" + password;
            //SqlConnection sq = new SqlConnection(Databackup);
            //sq.Open();


            //Voltage = dt.Rows[i]["DienAp"].ToString();
            //ConductorType = dt.Rows[i]["LoaiDayDan"].ToString();
            //ConductorSize = dt.Rows[i]["TietDienDay"].ToString();
            //ConductorMaterial = dt.Rows[i]["MaHieu"].ToString();
            //ConductorType = dt.Rows[i]["LoaiDayDan"].ToString();
            //TietDienDay = dt.Rows[i]["TietDienDay"].ToString();
            //SectionID = dt.Rows[i]["OBJECTID"].ToString();
            //DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();


            string AA = "select DienAp as Voltage,LoaiDayDan as ConductorType,TietDienDay as ConductorSize,MaHieu as ConductorMaterial,OBJECTID,XuatTuyen  from GIS_F09_PC03_DUONGDAY_TT";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {

                try
                {
                    string query = "SELECT XuatTuyen,OBJECTID  , COUNT(*) FROM GIS_F09_PC03_DUONGDAY_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt2 = new DataTable();
                        //dt2.Load(dr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

F09_PC03_DUONGDAY_TT CABLEID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ID";

                                read.Write(write);
                                read.Close();

                                //string dup = "OBJECTID is Duplicate";
                                //string obj = dr1["OBJECTID"].ToString();
                                //string FeederId = dr1["FeederId"].ToString();
                                //string myerr = "Insert into [TechErrorLog] (TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                //SqlCommand com = new SqlCommand(myerr, sq);
                                //com.ExecuteNonQuery();
                            }
                        }
                    }

                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//OBJECTID
                        string Voltage = dt.Rows[i]["Voltage"].ToString().Trim();//DienAp
                        string ConductorType = dt.Rows[i]["ConductorType"].ToString().Trim();//LoaiDayDan
                        string ConductorSize = dt.Rows[i]["ConductorSize"].ToString().Trim();//TietDienDay
                        string ConductorMaterial = dt.Rows[i]["ConductorMaterial"].ToString().Trim();//MaHieu
                        string XuatTuyen = dt.Rows[i]["XuatTuyen"].ToString().Trim();//XuatTuye                  

                        if (ab == 0)
                        {
                            if (OBJECTID == "" || Voltage == "" || Voltage == "" || ConductorType == "" || ConductorSize == "" || XuatTuyen == "")
                            {
                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();
                                ab++;

                            }
                        }

                        if (Voltage == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F09_PC03_DUONGDAY_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in Voltage/DienAp";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in Phasedesignation";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }

                        if (ConductorType == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F09_PC03_DUONGDAY_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorType/LoaiDayDan";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in Voltage";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }


                        if (ConductorSize == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F09_PC03_DUONGDAY_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorSize/TietDienDay";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in NumberofCore";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                        if (ConductorMaterial == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F09_PC03_DUONGDAY_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorMaterial/MaHieu";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in CableSize";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }


                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                }


            }
            //sq.Close();

            return ("1");



        }

        public string cab(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {
            int ab = 0;
            //OBJECTID,Voltage,NumberofCore,CableSize,InsulationType,ConductorMaterial
            //string servername = TG.TGservername;
            //string dbname = TG.TGdatabase;
            //string userid = TG.TGusername;
            //string password = TG.TGpassword;

            //string Databackup = @"Data Source=" + servername +                       //Create Connection string
            //      ";database=" + dbname +
            //      ";User ID=" + userid +
            //      ";Password=" + password;
            //SqlConnection sq = new SqlConnection(Databackup);
            //sq.Open();
            string AA = "select OBJECTID , Phasedesignation , Voltage , NumberofCore , CableSize , InsulationType , ConductorMaterial,FeederId from GIS_CABLE";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {

                try
                {
                    string query = "SELECT FeederId,OBJECTID  , COUNT(*) FROM GIS_CABLE GROUP BY OBJECTID,FeederId HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt2 = new DataTable();
                        //dt2.Load(dr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

CABLE CABLEID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ID";

                                read.Write(write);
                                read.Close();

                                //string dup = "OBJECTID is Duplicate";
                                //string obj = dr1["OBJECTID"].ToString();
                                //string FeederId = dr1["FeederId"].ToString();
                                //string myerr = "Insert into [TechErrorLog] (TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                //SqlCommand com = new SqlCommand(myerr, sq);
                                //com.ExecuteNonQuery();
                            }
                        }
                    }

                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string OBJECTID = dt.Rows[i][0].ToString().Trim();//OBJECTID
                        string Phasedesignation = dt.Rows[i][1].ToString().Trim();//Phasedesignation
                        string Voltage = dt.Rows[i][2].ToString().Trim();//Voltage
                        string NumberofCore = dt.Rows[i][3].ToString().Trim();//NumberofCore
                        string CableSize = dt.Rows[i][4].ToString().Trim();//CableSize
                        string InsulationType = dt.Rows[i][5].ToString().Trim();//InsulationType
                        string ConductorMaterial = dt.Rows[i][6].ToString().Trim();//ConductorMaterial
                        string FeederId = dt.Rows[i][7].ToString().Trim();//ConductorMaterial

                        if (OBJECTID == "CA_2358897")
                        {

                        }

                        if (ab == 0)
                        {
                            if (OBJECTID == "" || Phasedesignation == "" || Voltage == "" || NumberofCore == "" || CableSize == "" || InsulationType == "" || ConductorMaterial == "")
                            {
                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();
                                ab++;

                            }
                        }

                        if (Phasedesignation == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in Phasedesignation";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in Phasedesignation";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }

                        if (Voltage == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in Voltage";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }


                        if (NumberofCore == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in NumberofCore";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in NumberofCore";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                        if (CableSize == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in CableSize";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in CableSize";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }

                        if (InsulationType == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in InsulationType";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in InsulationType";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }
                        if (ConductorMaterial == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
CABLE OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorMaterial";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in ConductorMaterial";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CABLE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                }


            }
            //sq.Close();

            return ("1");



        }

        public string HTConduct(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData cf)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string con = st + ConfigurationManager.AppSettings["connection"];
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            //// string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";



            //OleDbConnection conn = new OleDbConnection(connString);

            //command
            int ab = 0;
            string AA = "select Phasedesignation as TG_PHASE,Voltage as TG_VOLTAGE,ConnectionStatus,GISID , Phasedesignation , Voltage , ConductorSize , ConductorType , ConductorMaterial , Configuration from GIS_HTCONDUCTOR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {

                    string query = "SELECT GISID , COUNT(*) FROM GIS_HTCONDUCTOR GROUP BY GISID HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt = new DataTable();
                        //dt.Load(dr);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

HTCONDUCTOR GISID-" + dr1["GISID"] + "     This GISID is Duplicate ";

                                read.Write(write);
                                read.Close();
                            }
                        }
                    }
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string aa = dt.Rows[i][0].ToString().Trim();
                            string bb = dt.Rows[i][1].ToString().Trim();
                            string cc = dt.Rows[i][2].ToString().Trim();
                            //string dd = dt.Rows[i][3].ToString();
                            string EE = dt.Rows[i][3].ToString().Trim();

                            string Phasedesignation = dt.Rows[i][4].ToString().Trim();
                            string Voltage = dt.Rows[i][5].ToString().Trim();
                            string ConductorSize = dt.Rows[i][6].ToString().Trim();
                            string ConductorType = dt.Rows[i][7].ToString().Trim();
                            string ConductorMaterial = dt.Rows[i][8].ToString().Trim();
                            string Configuration = dt.Rows[i][9].ToString().Trim();



                            if (ab == 0)
                            {
                                if (aa == "" || bb == "" || cc == "" || Phasedesignation == "" || Voltage == "" || ConductorSize == "" || ConductorType == "" || ConductorMaterial == "" || Configuration == "")
                                {
                                    StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                    string write1 = @"
";

                                    read1.Write(write1);
                                    read1.Close();
                                    ab++;
                                }
                            }



                            if (aa == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in PHASE";

                                read.Write(write);
                                read.Close();

                            }

                            if (bb == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in TG_VOLTAGE";

                                read.Write(write);
                                read.Close();

                            }


                            if (cc == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONNECTIONSTATE";

                                read.Write(write);
                                read.Close();

                            }

                            if (Phasedesignation == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                                read.Write(write);
                                read.Close();

                            }
                            if (Voltage == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                                read.Write(write);
                                read.Close();

                            }
                            if (ConductorSize == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORSIZE";

                                read.Write(write);
                                read.Close();

                            }
                            if (ConductorType == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORTYPE";

                                read.Write(write);
                                read.Close();

                            }
                            if (ConductorMaterial == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONDUCTORMATERIAL";

                                read.Write(write);
                                read.Close();

                            }
                            if (Configuration == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCONDUCTOR GISID-" + EE + "     Data Not Avilable in CONFIGURATION";

                                read.Write(write);
                                read.Close();

                            }



                        }
                    }
                    else
                    {

                        return ("3");

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                    return ("2");
                }

            }

            return ("1");
        }

        public string htcab(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string con = st + ConfigurationManager.AppSettings["connection"];
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            //// string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";
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


            //OleDbConnection conn = new OleDbConnection(connString);

            ////command
            int ab = 0;
            string AA = "select Voltage,ConductorSize,ConductorType,ConductorMaterial,Length,OBJECTID,PhaseDesignation,FeederId  from GIS_LINE";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {

                    string query = "SELECT FeederId,OBJECTID  , COUNT(*) FROM GIS_LINE GROUP BY OBJECTID,FeederId HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt2 = new DataTable();
                        //dt2.Load(dr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

LINE GLINEID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ID";

                                read.Write(write);
                                read.Close();
                                string dup = "OBJECTID is Duplicate";
                                string obj = dr1["OBJECTID"].ToString();
                                string FeederId = dr1["FeederId"].ToString();
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                        }
                    }
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Voltage = dt.Rows[i][0].ToString().Trim();
                            string ConductorSize = dt.Rows[i][1].ToString().Trim();
                            string ConductorType = dt.Rows[i][2].ToString().Trim();
                            string ConductorMaterial = dt.Rows[i][3].ToString().Trim();
                            string Length = dt.Rows[i][4].ToString().Trim();
                            string OBJECTID = dt.Rows[i][5].ToString().Trim();
                            string PhaseDesignation = dt.Rows[i][6].ToString().Trim();
                            string FeederId = dt.Rows[i][7].ToString().Trim();
                            if (OBJECTID == "OH_790640")
                            {

                            }

                            if (ab == 0)
                            {
                                if (Voltage == "" || ConductorSize == "" || ConductorType == "" || ConductorMaterial == "" || Length == "" || OBJECTID == "" || PhaseDesignation == "")
                                {
                                    StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                    string write1 = @"
";

                                    read1.Write(write1);
                                    read1.Close();
                                    ab++;
                                }
                            }

                            if (Voltage == "")
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in Voltage";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in PHASE";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }
                            if (ConductorSize == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorSize";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in ConductorSize";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                            if (ConductorType == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorType";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in ConductorType";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                            if (ConductorMaterial == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in ConductorMaterial";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in ConductorMaterial";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                            if (Length == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in Length";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in Length";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                            if (PhaseDesignation == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
LINE OBJECTID-" + OBJECTID + "     Data Not Avilable in PhaseDesignation";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in PhaseDesignation";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','LINE','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();

                            }
                        }
                    }
                    else
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        return ("3");
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                    return ("2");
                }


            }
            sq.Close();
            return ("1");


        }

        public string fuse(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
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
            sq.Open();

            int ab = 0;
            // Voltage,FuseType,Rating,OBJECTID
            string AA = "select Phasedesignation,Voltage,Rating,FuseType,OBJECTID,FeederId from GIS_FUSE";

            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    string query = "SELECT FeederId,OBJECTID  , COUNT(*) FROM GIS_FUSE GROUP BY OBJECTID,FeederId HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt2 = new DataTable();
                        //dt2.Load(dr1);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

FUSE FUSEID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ID";

                                read.Write(write);
                                read.Close();
                                string dup = "OBJECTID is Duplicate";
                                string obj = dr1["OBJECTID"].ToString();
                                string FeederId = dr1["OBJECTID"].ToString();
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','FUSE','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }
                        }
                    }
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim();//Phasedesignation
                        string bb = dt.Rows[i][1].ToString().Trim();//Voltage
                        string cc = dt.Rows[i][2].ToString().Trim();//Rating
                        string dd = dt.Rows[i][3].ToString().Trim();//FuseType
                        string EE = dt.Rows[i][4].ToString().Trim();//OBJECTID
                        string FeederId = dt.Rows[i][5].ToString().Trim();//OBJECTID
                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || dd == "")
                            {
                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();
                                ab++;
                            }
                        }



                        if (aa == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE OBJECTID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in PHASE";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','FUSE','" + EE + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE OBJECTID-" + EE + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();

                            string Err = "Data Not Avilable in Voltage";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','FUSE','" + EE + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();

                        }


                        if (cc == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE OBJECTID-" + EE + "     Data Not Avilable in Rating";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in Rating";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','FUSE','" + EE + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();

                        }


                        if (dd == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE OBJECTID-" + EE + "     Data Not Avilable in Type";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in Type";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','FUSE','" + EE + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                }


            }
            sq.Close();
            return ("1");
        }

        public string dt(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {

            int ab = 0;
            string AA = "SELECT OBJECTID,UNganMach,TonHaoKhongTai,CapDienApVao,CapDienApRa,CongSuat,XuatTuyen from GIS_F04_PC03_MAYBIENAP_TT";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {


                    string query = "SELECT XuatTuyen,OBJECTID , COUNT(*) FROM GIS_F04_PC03_MAYBIENAP_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";
                    //GIS_DISTRIBUTIONTRANSFORMERXuatTuyen

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        //DataTable dt = new DataTable();
                        //dt.Load(dr);
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

F04_PC03_MAYBIENAP_TT OBJECTID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ";

                                read.Write(write);
                                read.Close();
                                //string dup = "OBJECTID is Duplicate";
                                //string obj = dr1["OBJECTID"].ToString();
                                //string FeederId = dr1["OBJECTID"].ToString();
                                //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                //SqlCommand com = new SqlCommand(myerr, sq);
                                //com.ExecuteNonQuery();
                            }
                        }
                    }

                    OleDbDataReader dr = cmd.ExecuteReader();



                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string UNganMach = dt.Rows[i]["UNganMach"].ToString().Trim();//HighVoltageSideVolts
                        string TonHaoKhongTai = dt.Rows[i]["TonHaoKhongTai"].ToString().Trim();//LowVoltageSideVolts
                        string CapDienApVao = dt.Rows[i]["CapDienApVao"].ToString().Trim();//Capacity
                        string CapDienApRa = dt.Rows[i]["CapDienApRa"].ToString().Trim();//CoolingType
                        string CongSuat = dt.Rows[i]["CongSuat"].ToString().Trim();//OBJECTID
                        string XuatTuyen = dt.Rows[i]["XuatTuyen"].ToString().Trim();//FeederId
                        string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//PhaseDesignation

                        if (ab == 0)
                        {
                            if (UNganMach == "" || TonHaoKhongTai == "" || CapDienApVao == "" || CapDienApRa == "" || CongSuat == "" || XuatTuyen == "")
                            {

                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();

                                ab++;
                            }

                        }

                        if (string.IsNullOrWhiteSpace(UNganMach))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F04_PC03_MAYBIENAP_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in UNganMach";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in HighVoltageSideVolts";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                        if (string.IsNullOrWhiteSpace(TonHaoKhongTai))
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F04_PC03_MAYBIENAP_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in TonHaoKhongTai";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in LowVoltageSideVolts";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }


                        if (string.IsNullOrWhiteSpace(CapDienApVao))
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F04_PC03_MAYBIENAP_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in CapDienApVao";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in Capacity";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }


                        if (string.IsNullOrWhiteSpace(CapDienApRa))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F04_PC03_MAYBIENAP_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in CapDienApRa";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in CoolingType";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                        if (string.IsNullOrWhiteSpace(CongSuat))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F04_PC03_MAYBIENAP_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in CongSuat";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in PhaseDesignation";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    MessageBox.Show(ex.ToString());
                }


            }


            return ("1");

        }

        public string ShuntCapacitor(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {

            int ab = 0;
            string AA = "SELECT OBJECTID,DienAp,TongCongSuat,XuatTuyen from GIS_F03_PC03_TUBU_TT";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {


//                    string query = "SELECT XuatTuyen,OBJECTID , COUNT(*) FROM GIS_F03_PC03_TUBU_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";
//                    //GIS_DISTRIBUTIONTRANSFORMERXuatTuyen

//                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
//                    {
//                        OleDbDataReader dr1 = cmd1.ExecuteReader();
//                        //DataTable dt = new DataTable();
//                        //dt.Load(dr);
//                        if (dr1.HasRows)
//                        {
//                            while (dr1.Read())
//                            {
//                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

//                                string write = @"

//F03_PC03_TUBU_TT OBJECTID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ";

//                                read.Write(write);
//                                read.Close();
                                 
//                            }
//                        }
//                    }

                    OleDbDataReader dr = cmd.ExecuteReader();



                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string TongCongSuat = dt.Rows[i]["TongCongSuat"].ToString().Trim();//HighVoltageSideVolts
                        string DienAp = dt.Rows[i]["DienAp"].ToString().Trim();//LowVoltageSideVolts
                        string XuatTuyen = dt.Rows[i]["XuatTuyen"].ToString().Trim();//Capacity
                        string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//CoolingType


                        if (ab == 0)
                        {
                            if (TongCongSuat == "" || DienAp == "" || XuatTuyen == "" || OBJECTID == "")
                            {

                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();

                                ab++;
                            }

                        }

                        if (string.IsNullOrWhiteSpace(DienAp))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F03_PC03_TUBU_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in DienAp";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in HighVoltageSideVolts";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();

                        }

                        if (string.IsNullOrWhiteSpace(TongCongSuat))
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
F03_PC03_TUBU_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in TongCongSuat";

                            read.Write(write);
                            read.Close();
                            //string Err = "Data Not Avilable in LowVoltageSideVolts";
                            //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            //SqlCommand com = new SqlCommand(myerr, sq);
                            //com.ExecuteNonQuery();
                        }

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    MessageBox.Show(ex.ToString());
                }


            }


            return ("1");

        }

        public DataTable Connection(ConfigFileData hh, string quary)
        {
            DataTable Table = new DataTable();
            string connectionString = @"Data Source=" + hh.Gisservername +
                           ";Initial Catalog=" + hh.GisDatabase +
                           ";User ID=" + hh.Gisusername +
                           ";Password=" + hh.Gispassword;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand(quary, conn))
                {

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        Table.Load(dr);
                    }
                }
            }

            return Table;
        }

        public SqlDataReader Connection1(ConfigFileData hh, string quary)
        {
            SqlDataReader dr;
            string connectionString = @"Data Source=" + hh.Gisservername +
                           ";Initial Catalog=" + hh.GisDatabase +
                           ";User ID=" + hh.Gisusername +
                           ";Password=" + hh.Gispassword;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand com = new SqlCommand(quary, conn))
                {

                    dr = com.ExecuteReader();
                }
            }

            return dr;
        }

        public string Switched(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG, string Feederid)
        {



            try
            {


                int ab = 0;
                string AA = "SELECT sw.OBJECTID, sw.ID, sw.DienAp, sw.XuatTuyen, sw.Enabled, r.IDTBDC, r.IDinhMuc,  r.LoaiLBS " +
                               "FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw " +
                                "left JOIN PC03_LBS AS r ON sw.ID = r.IDTBDC " +
                                    "WHERE sw.LoaiTBDC = 4 AND sw.XuatTuyen = '" + Feederid + "' " +
                                    "ORDER BY r.IDinhMuc;";
                DataTable dt = Connection(TG, AA);
                string query = "SELECT sw.OBJECTID,sw.XuatTuyen FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw left JOIN PC03_LBS AS r ON sw.ID = r.IDTBDC WHERE sw.LoaiTBDC = 4 AND sw.XuatTuyen = '" + Feederid + "' GROUP BY sw.OBJECTID,sw.XuatTuyen HAVING COUNT(*) > 1";
                //string query = "SELECT XuatTuyen,OBJECTID , COUNT(*) FROM GIS_F04_PC03_MAYBIENAP_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";
                //DataTable dt1 = Connection(TG, query);

                DataTable dt1 = Connection(TG, query);
                foreach (DataRow row in dt1.Rows)
                {
                    string objectId = row["OBJECTID"].ToString();
                    StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                    string write = @"

F04_PC03_MAYBIENAP_TT OBJECTID-" + objectId + "     This OBJECTID is Duplicate ";

                    read.Write(write);
                    read.Close();
                }






                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//HighVoltageSideVolts
                    string STATUS = dt.Rows[i]["Enabled"].ToString().Trim();//LowVoltageSideVolts
                    string LoaiLBS = dt.Rows[i]["LoaiLBS"].ToString().Trim();//LowVoltageSideVolts
                    string IDinhMuc = dt.Rows[i]["IDinhMuc"].ToString().Trim();//LowVoltageSideVolts
                    string DienAp = dt.Rows[i]["DienAp"].ToString().Trim();//LowVoltageSideVolts
                    string ID = dt.Rows[i]["ID"].ToString().Trim();//LowVoltageSideVolts


                    if (ab == 0)
                    {
                        if (IDinhMuc == "" || DienAp == "" || OBJECTID == "" || STATUS == "" || LoaiLBS == "")
                        {

                            StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                            string write1 = @"
";

                            read1.Write(write1);
                            read1.Close();

                            ab++;
                        }

                    }

                    if (string.IsNullOrWhiteSpace(IDinhMuc))
                    {
                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
PC03_LBS IDTBDC-" + ID + "     Data Not Avilable in IDinhMuc";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in HighVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();

                    }

                    if (string.IsNullOrWhiteSpace(DienAp))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
F01_PC03_THIETBIDONGCAT_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in DienAp";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in LowVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();
                    }


                    if (string.IsNullOrWhiteSpace(STATUS))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
F01_PC03_THIETBIDONGCAT_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in STATUS";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in LowVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();
                    }

                    if (string.IsNullOrWhiteSpace(LoaiLBS))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
PC03_LBS IDTBDC-" + ID + "     Data Not Avilable in LoaiLBS";

                        read.Write(write);
                        read.Close();

                    }


                }



            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.ToString());
            }





            return ("1");

        }

        public string FUSED(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG, string Feederid)
        {



            try
            {


                int ab = 0;
                string AA = "SELECT sw.OBJECTID, sw.ID, sw.DienAp, sw.XuatTuyen,SW.Enabled, r.IDTBDC, r.IDinhMuc " +
                               "FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw " +
                                "left JOIN PC03_FCO AS r ON sw.ID = r.IDTBDC " +
                                    "WHERE sw.LoaiTBDC = 5  AND sw.XuatTuyen = '" + Feederid + "' " +
                                    "ORDER BY r.IDinhMuc;";
                DataTable dt = Connection(TG, AA);
                string query = "SELECT sw.OBJECTID,sw.XuatTuyen FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw left JOIN PC03_FCO AS r ON sw.ID = r.IDTBDC WHERE sw.LoaiTBDC = 5  AND sw.XuatTuyen = '" + Feederid + "' GROUP BY sw.OBJECTID,sw.XuatTuyen HAVING COUNT(*) > 1";
                //string query = "SELECT XuatTuyen,OBJECTID , COUNT(*) FROM GIS_F04_PC03_MAYBIENAP_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";
                //DataTable dt1 = Connection(TG, query);

                //                DataTable dt1 = Connection(TG, query);
                //                foreach (DataRow row in dt1.Rows)
                //                {
                //                    string objectId = row["OBJECTID"].ToString();
                //                    StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                //                    string write = @"

                //F04_PC03_MAYBIENAP_TT OBJECTID-" + objectId + "     This OBJECTID is Duplicate ";

                //                    read.Write(write);
                //                    read.Close();
                //                }






                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//HighVoltageSideVolts
                    string STATUS = dt.Rows[i]["Enabled"].ToString().Trim();//LowVoltageSideVolts
                    //string LoaiLBS = dt.Rows[i]["LoaiLBS"].ToString().Trim();//LowVoltageSideVolts
                    string IDinhMuc = dt.Rows[i]["IDinhMuc"].ToString().Trim();//LowVoltageSideVolts
                    string IDTBDC = dt.Rows[i]["IDTBDC"].ToString().Trim();//LowVoltageSideVolts
                    string DienAp = dt.Rows[i]["DienAp"].ToString().Trim();//LowVoltageSideVolts
                    string ID = dt.Rows[i]["ID"].ToString().Trim();//LowVoltageSideVolts


                    if (ab == 0)
                    {
                        if (IDinhMuc == "" || DienAp == "" || OBJECTID == "" || STATUS == "" || IDTBDC == "")
                        {

                            StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                            string write1 = @"
";

                            read1.Write(write1);
                            read1.Close();

                            ab++;
                        }

                    }

                    if (string.IsNullOrWhiteSpace(IDinhMuc))
                    {
                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
PC03_FCO IDTBDC-" + ID + "     Data Not Avilable in IDinhMuc";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in HighVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();

                    }

                    if (string.IsNullOrWhiteSpace(DienAp))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
F01_PC03_THIETBIDONGCAT_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in DienAp";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in LowVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();
                    }


                    if (string.IsNullOrWhiteSpace(STATUS))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
F01_PC03_THIETBIDONGCAT_TT OBJECTID-" + OBJECTID + "     Data Not Avilable in STATUS";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in LowVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();
                    }


                }



            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.ToString());
            }





            return ("1");

        }

        public string Recloser(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG, string Feederid)
        { 

            try
            {


                int ab = 0;
                string AA = "SELECT sw.OBJECTID, sw.ID, sw.DienAp, sw.XuatTuyen, sw.Enabled, r.IDTBDC, r.IDinhMuc " +
                                   "FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw " +
                                    "left JOIN PC03_RECLOSER AS r ON sw.ID = r.IDTBDC " +
                                    "WHERE sw.LoaiTBDC = 4 AND sw.XuatTuyen = '" + Feederid + "' " +
                                    "ORDER BY r.IDinhMuc;";
                DataTable dt = Connection(TG, AA);
                string query = "SELECT sw.OBJECTID,sw.XuatTuyen FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw left JOIN PC03_RECLOSER AS r ON sw.ID = r.IDTBDC WHERE sw.LoaiTBDC = 4 AND sw.XuatTuyen = '" + Feederid + "' GROUP BY sw.OBJECTID,sw.XuatTuyen HAVING COUNT(*) > 1";
                //string query = "SELECT XuatTuyen,OBJECTID , COUNT(*) FROM GIS_F04_PC03_MAYBIENAP_TT GROUP BY OBJECTID,XuatTuyen HAVING COUNT(*) > 1";
                //DataTable dt1 = Connection(TG, query);

                DataTable dt1 = Connection(TG, query);
                foreach (DataRow row in dt1.Rows)
                {
                    string objectId = row["OBJECTID"].ToString();
                    StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                    string write = @"

F04_PC03_MAYBIENAP_TT OBJECTID-" + objectId + "     This OBJECTID is Duplicate ";

                    read.Write(write);
                    read.Close();
                }






                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string IDinhMuc = dt.Rows[i]["IDinhMuc"].ToString().Trim();//HighVoltageSideVolts
                    string DienAp = dt.Rows[i]["DienAp"].ToString().Trim();//LowVoltageSideVolts
                    string OBJECTID = dt.Rows[i]["OBJECTID"].ToString().Trim();//LowVoltageSideVolts


                    if (ab == 0)
                    {
                        if (IDinhMuc == "" || DienAp == "" || OBJECTID == "")
                        {

                            StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                            string write1 = @"
";

                            read1.Write(write1);
                            read1.Close();

                            ab++;
                        }

                    }

                    if (string.IsNullOrWhiteSpace(IDinhMuc))
                    {
                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
PC03_RECLOSER IDTBDC-" + OBJECTID + "     Data Not Avilable in IDinhMuc";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in HighVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();

                    }

                    if (string.IsNullOrWhiteSpace(DienAp))
                    {

                        StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                        string write = @"
F01_PC03_THIETBIDONGCAT_TT ID-" + OBJECTID + "     Data Not Avilable in DienAp";

                        read.Write(write);
                        read.Close();
                        //string Err = "Data Not Avilable in LowVoltageSideVolts";
                        //string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','DISTRIBUTIONTRANSFORMER','" + OBJECTID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                        //SqlCommand com = new SqlCommand(myerr, sq);
                        //com.ExecuteNonQuery();
                    }



                }



            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.ToString());
            }





            return ("1");

        }

        public string circuit(string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
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
            sq.Open();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //Voltage,InsulationType,AssetID,Rating,SymmetricalBreakingCurrent,OBJECTID,MAKE
            int ab = 0;
            string AA = "select Phasedesignation,Voltage,OBJECTID , InsulationType , Make,SymmetricalBreakingCurrent,Rating,FeederId from GIS_Switchgear";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    //conn.Open();


                    string query = "SELECT FeederId,OBJECTID  , COUNT(*) FROM GIS_Switchgear GROUP BY OBJECTID,FeederId HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

CIRCUITBREAKER OBJECTID-" + dr1["OBJECTID"] + "     This OBJECTID is Duplicate ";

                                read.Write(write);
                                read.Close();
                                string dup = "OBJECTID is Duplicate";
                                string obj = dr1["OBJECTID"].ToString();
                                string FeederId = dr1["FeederId"].ToString();
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','CIRCUITBREAKER','" + obj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dup + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }
                        }
                    }
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim(); //phase
                        string bb = dt.Rows[i][1].ToString().Trim(); //voltage
                        string cc = dt.Rows[i][2].ToString().Trim(); //OBJECTID
                        string dd = dt.Rows[i][3].ToString().Trim();//InsulationType
                        string ee = dt.Rows[i][4].ToString().Trim(); //Make                                          // string cc = dt.Rows[i][2].ToString().Trim();
                        string ff = dt.Rows[i][5].ToString(); //SymmetricalBreakingCurrent
                        string gg = dt.Rows[i][6].ToString(); //Rating
                        string FeederId = dt.Rows[i][7].ToString(); //Rating
                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || dd == "" || ee == "" || ff == "" || gg == "")
                            {
                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();
                                ab++;
                            }
                        }

                        if (aa == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in PHASE";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in Voltage";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }

                        if (dd == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in INSULATIONTYPE";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in INSULATIONTYPE";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }
                        if (ee == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in MAKE";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in MAKE";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }

                        if (ff == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in SymmetricalBreakingCurrent";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in SymmetricalBreakingCurrent";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }
                        if (gg == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR OBJECTID-" + cc + "     Data Not Avilable in Rating";

                            read.Write(write);
                            read.Close();
                            string Err = "Data Not Avilable in Rating";
                            string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','SWITCHGEAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                            SqlCommand com = new SqlCommand(myerr, sq);
                            com.ExecuteNonQuery();
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                }

            }

            return ("1");
        }

        public string End(string Cyme_config_txtReportDirectory, ref DateTime EndTime)
        {
            EndTime = DateTime.Now;
            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

            string write = @"

End DateTime                         " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss:tt");

            read.Write(write);
            read.Close();


            return ("1");
        }

        public string busbar(string st, string Cyme_config_txtReportDirectory, OleDbConnection conn, ConfigFileData TG)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string con = st + ConfigurationManager.AppSettings["connection"];
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            //// string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";
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


            //OleDbConnection conn = new OleDbConnection(connString);
            int ab = 0;
            string AA = "select Voltage , Material , OBJECTID,FeederId,PhaseDesignation from GIS_BUSBAR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {

                    OleDbDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string aa = dt.Rows[i][0].ToString();
                            string bb = dt.Rows[i][1].ToString();
                            string cc = dt.Rows[i][2].ToString();
                            string FeederId = dt.Rows[i][3].ToString();
                            string PhaseDesignation = dt.Rows[i][4].ToString();


                            if (ab == 0)
                            {
                                if (aa == "" || bb == "" || cc == "" || PhaseDesignation == "")
                                {
                                    StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                    string write1 = @"
";

                                    read1.Write(write1);
                                    read1.Close();
                                    ab++;
                                }
                            }

                            if (aa == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
BUSBAR OBJECTID-" + cc + "     Data Not Avilable in Voltage";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in Voltage";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','BUSBAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }
                            if (PhaseDesignation == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
BUSBAR OBJECTID-" + cc + "     Data Not Avilable in PhaseDesignation";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in PhaseDesignation";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','BUSBAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }

                            if (bb == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
BUSBAR OBJECTID-" + cc + "     Data Not Avilable in Material";

                                read.Write(write);
                                read.Close();
                                string Err = "Data Not Avilable in Material";
                                string myerr = "Insert into [TechErrorLog] (FeederId,TABLE_NAME, OBJECTID, Datetime, Error) values ('" + FeederId + "','BUSBAR','" + cc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Err + "')";
                                SqlCommand com = new SqlCommand(myerr, sq);
                                com.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        return ("3");
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show(ex.ToString());
                    return ("2");
                }
            }

            return ("1");

        }
        public string Total_ImportFeeder(string Cyme_config_txtReportDirectory, int count, DateTime StartTime, DateTime EndTime)
        {
            string mainPath = Cyme_config_txtReportDirectory;

            long lineCount = 0;
            TimeSpan ts = EndTime - StartTime;
            var time = ts.TotalMinutes;
            // int time=(Convert.ToInt32(EndTime)-Convert.ToInt32(StartTime));
            double velocity = time / Convert.ToDouble(count);
            string[] lines = File.ReadAllLines(mainPath);

            foreach (string line in lines)
            {
                if (line.Contains("Import Successful"))
                {
                    lineCount++;
                }
            }

            int Extract = (int)lineCount;
            int Failed = count - Extract;

            string log1 =

@"

=================================================================================================================================================

Total Selected Network :   " + count + "           Total ImportFeeder :   " + Extract + "             Total FailedFeeder :  " + Failed + "     " +

@"

Average Time/Feeder : " + Math.Round(velocity, 4) + "min " +

@"

================================================================================================================================================== ";

            StreamWriter sw = File.AppendText(Cyme_config_txtReportDirectory);
            sw.Write(log1);
            sw.Close();

            return ("1");
        }

    }
}
