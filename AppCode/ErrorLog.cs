using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class ErrorLog
    {
        public string combineErrorLog(string Cyme_config_txtReportDirectory,string GETFILE, int count)
        {
            string path = GETFILE + "\\Feeder" + count.ToString() + "\\ErrorLog\\FeederErrorlog.log";
            string errorlog = File.ReadAllText(path);
            StreamWriter sw = File.AppendText(Cyme_config_txtReportDirectory);
            sw.WriteLine(errorlog);
            sw.Close();
            return "1";
        }

        public string fir(string Cyme_config_txtReportDirectory, string id)
        {
            string log = @"              ==================================================================
              Product Information
              Tech-Gration Error Log File for CESU 
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

        public string cir(string Cyme_config_txtReportDirectory, OleDbConnection conn)
        {
            string AA = "select TG_PHASE,TG_Voltage,TG_NORMALREATEDCURRENT,TG_NORMALSTATUS,GISID  ,Phasedesignation , Voltage , NormalRatedCurrent from GIS_SWITCH";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                   
                    string query = "SELECT GISID , COUNT(*) FROM GIS_SWITCH GROUP BY GISID HAVING COUNT(*) > 1";

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

SWITCH GISID-" + dr1["GISID"] + "     This GISID is Duplicate ";

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
                        string dd = dt.Rows[i][3].ToString().Trim();
                        string EE = dt.Rows[i][4].ToString().Trim();

                        string Phasedesignation = dt.Rows[i][5].ToString().Trim();
                        string Voltage = dt.Rows[i][6].ToString().Trim();
                        string NormalRatedCurrent = dt.Rows[i][7].ToString().Trim();

                        if (aa == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();
                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();
                        }


                        if (cc == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in NORMALREATEDCURRENT";

                            read.Write(write);
                            read.Close();

                        }


                        if (dd == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in NORMALSTATUS";

                            read.Write(write);
                            read.Close();

                        }

                        if (Phasedesignation == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                            read.Write(write);
                            read.Close();

                        }

                        if (Voltage == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }

                        if (NormalRatedCurrent == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCH GISID-" + EE + "     Data Not Avilable in NORMALRATEDCURRENT";

                            read.Write(write);
                            read.Close();

                        }
                    }
                }
                catch (Exception ex)
                { }

                
            }

            return ("1");
        }

        public string LTConductor(string Cyme_config_txtReportDirectory, string st)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string con = st + ConfigurationManager.AppSettings["connection"];
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            // string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";


            OleDbConnection conn = new OleDbConnection(connString);

            //command
            int ab = 0;
            string AA = "select TG_PHASE,TG_VOLTAGE,TG_CONNECTIONSTATE,GISID , Phasedesignation , Voltage , ConductorSize , ConductorType , ConductorMaterial , Configuration from GIS_LTCONDUCTOR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT GISID , COUNT(*) FROM GIS_LTCONDUCTOR GROUP BY GISID HAVING COUNT(*) > 1";

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
                }
                catch (Exception ex)
                { }


                conn.Close();
            }

            return ("1");
        }

        public string cab(string Cyme_config_txtReportDirectory, OleDbConnection conn)
        {
            int ab = 0;
            string AA = "select TG_PHASE,TG_VOLTAGE,TG_SUBTYPE,TG_CONNECTIONSTATE,TG_CABLETYPE,GISID , Phasedesignation , Voltage , NumberofCore , CableSize , InsulationType , ConductorMaterial from GIS_LTCABLE";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {

                try
                {
                    conn.Open();
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim();
                        string bb = dt.Rows[i][1].ToString().Trim();
                        string cc = dt.Rows[i][2].ToString().Trim();
                        string dd = dt.Rows[i][3].ToString().Trim();
                        string FF = dt.Rows[i][4].ToString().Trim();
                        string EE = dt.Rows[i][5].ToString().Trim();

                        string Phasedesignation = dt.Rows[i][6].ToString().Trim();
                        string Voltage = dt.Rows[i][7].ToString().Trim();
                        string NumberofCore = dt.Rows[i][8].ToString().Trim();
                        string CableSize = dt.Rows[i][9].ToString().Trim();
                        string InsulationType = dt.Rows[i][10].ToString().Trim();
                        string ConductorMaterial = dt.Rows[i][11].ToString().Trim();


                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || dd == "" || FF == "" || Phasedesignation == "" || Voltage == "" || NumberofCore == "" || CableSize == "" || InsulationType == "" || ConductorMaterial == "")
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
LTCABLE GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();

                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in TG_VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }


                        if (cc == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in SUBTYPE";

                            read.Write(write);
                            read.Close();

                        }


                        if (dd == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in CONNECTIONSTATE";

                            read.Write(write);
                            read.Close();

                        }

                        if (FF == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in CABLETYPE";

                            read.Write(write);
                            read.Close();

                        }

                        if (Phasedesignation == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                            read.Write(write);
                            read.Close();

                        }
                        if (Voltage == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }
                        if (NumberofCore == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in NUMBEROFCORE";

                            read.Write(write);
                            read.Close();

                        }
                        if (CableSize == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in CABLESIZE";

                            read.Write(write);
                            read.Close();

                        }
                        if (InsulationType == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in INSULATIONTYPE";

                            read.Write(write);
                            read.Close();

                        }
                        if (ConductorMaterial == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
LTCABLE GISID-" + EE + "     Data Not Avilable in CONDUCTORMATERIAL";

                            read.Write(write);
                            read.Close();

                        }


                    }
                }
                catch (Exception ex)
                { }

                
            }


            return ("1");



        }

        public string HTConduct(string Cyme_config_txtReportDirectory, string st)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string con = st + ConfigurationManager.AppSettings["connection"];
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            // string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";



            OleDbConnection conn = new OleDbConnection(connString);

            //command
            int ab = 0;
            string AA = "select TG_PHASE,TG_VOLTAGE,TG_CONNECTIONSTATE,GISID , Phasedesignation , Voltage , ConductorSize , ConductorType , ConductorMaterial , Configuration from GIS_HTCONDUCTOR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    conn.Open();
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
                }
                catch (Exception ex)
                {
                    return ("2");
                }
                conn.Close();
            }

            return ("1");
        }

        public string htcab(string Cyme_config_txtReportDirectory, string st)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string con = st + ConfigurationManager.AppSettings["connection"];
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            // string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";



            OleDbConnection conn = new OleDbConnection(connString);

            //command
            int ab = 0;
            string AA = "select TG_PHASE,TG_VOLTAGE,TG_SUBTYPE,TG_CONNECTIONSTATE,TG_CABLETYPE,GISID , Phasedesignation , Voltage , NumberofCore , CableSize , InsulationType , ConductorMaterial from GIS_HTCABLE";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT GISID , COUNT(*) FROM GIS_HTCABLE GROUP BY GISID HAVING COUNT(*) > 1";

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

HTCABLE CABLEID-" + dr1["GISID"] + "     This GISID is Duplicate ID";

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
                            string dd = dt.Rows[i][3].ToString().Trim();
                            string FF = dt.Rows[i][4].ToString().Trim();
                            string EE = dt.Rows[i][5].ToString().Trim();
                            string Phasedesignation = dt.Rows[i][6].ToString().Trim();
                            string Voltage = dt.Rows[i][7].ToString().Trim();
                            string NumberofCore = dt.Rows[i][8].ToString().Trim();
                            string CableSize = dt.Rows[i][9].ToString().Trim();
                            string InsulationType = dt.Rows[i][10].ToString().Trim();
                            string ConductorMaterial = dt.Rows[i][11].ToString().Trim();

                            if (ab == 0)
                            {
                                if (aa == "" || bb == "" || cc == "" || dd == "" || FF == "" || Phasedesignation == "" || Voltage == "" || NumberofCore == "" || CableSize == "" || InsulationType == "" || ConductorMaterial == "")
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
HTCABLE GISID-" + EE + "     Data Not Avilable in PHASE";

                                read.Write(write);
                                read.Close();

                            }

                            if (bb == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in TG_VOLTAGE";

                                read.Write(write);
                                read.Close();

                            }


                            if (cc == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in SUBTYPE";

                                read.Write(write);
                                read.Close();

                            }


                            if (dd == "")
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in CONNECTIONSTATE";

                                read.Write(write);
                                read.Close();

                            }

                            if (FF == "")
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in CABLETYPE";

                                read.Write(write);
                                read.Close();

                            }

                            if (Phasedesignation == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                                read.Write(write);
                                read.Close();

                            }
                            if (Voltage == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                                read.Write(write);
                                read.Close();

                            }
                            if (NumberofCore == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in NUMBEROFCORE";

                                read.Write(write);
                                read.Close();

                            }
                            if (CableSize == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in CABLESIZE";

                                read.Write(write);
                                read.Close();

                            }
                            if (InsulationType == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in INSULATIONTYPE";

                                read.Write(write);
                                read.Close();

                            }
                            if (ConductorMaterial == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
HTCABLE GISID-" + EE + "     Data Not Avilable in CONDUCTORMATERIAL";

                                read.Write(write);
                                read.Close();

                            }
                        }
                    }
                    else
                    {
                        return ("3");
                    }
                }
                catch (Exception ex)
                {
                    return ("2");
                }

                conn.Close();
            }

            return ("1");

        }

        public string fuse(string Cyme_config_txtReportDirectory,OleDbConnection conn)
        {
            int ab = 0;
            //string AA = "select TG_PHASE,TG_Voltage,TG_TYPE,TG_NORMALSTATUS,GISID from GIS_FUSE";
            string AA = "select Phasedesignation,Voltage,Rating,Type,GISID from GIS_FUSE";

            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    
                    OleDbDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string aa = dt.Rows[i][0].ToString().Trim();
                        string bb = dt.Rows[i][1].ToString().Trim();
                        string cc = dt.Rows[i][2].ToString().Trim();
                        string dd = dt.Rows[i][3].ToString().Trim();
                        string EE = dt.Rows[i][4].ToString().Trim();
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
FUSE GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();

                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE GISID-" + EE + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();

                        }


                        if (cc == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE GISID-" + EE + "     Data Not Avilable in Rating";

                            read.Write(write);
                            read.Close();

                        }


                        if (dd == "")
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
FUSE GISID-" + EE + "     Data Not Avilable in Type";

                            read.Write(write);
                            read.Close();

                        }
                    }
                }
                catch (Exception ex)
                { }

                
            }

            return ("1");
        }

        public string dt(string Cyme_config_txtReportDirectory, OleDbConnection conn)
        {
            
            int ab = 0;
            string AA = "select TG_PHASE,TG_HighSideVoltage,TG_LowSideVoltage,CapacityInKVA,PercentageImpedance,TG_COOLINGTYPE,TG_CONNECTIONSTATE,TG_TransformerMounting,GISID from GIS_DT";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    
                    string query = "SELECT GISID , COUNT(*) FROM GIS_DT GROUP BY GISID HAVING COUNT(*) > 1";
                    //GIS_DISTRIBUTIONTRANSFORMER

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

DISTRIBUTIONTRANSFORMER GISID-" + dr1["GISID"] + "     This GISID is Duplicate ";

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
                        string dd = dt.Rows[i][3].ToString().Trim();
                        string FF = dt.Rows[i][4].ToString().Trim();
                        string GG = dt.Rows[i][5].ToString().Trim();
                        string II = dt.Rows[i][6].ToString().Trim();
                        string HH = dt.Rows[i][7].ToString().Trim();
                        string EE = dt.Rows[i][8].ToString().Trim();
                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || dd == "" || FF == "" || GG == "" || HH == "" || II == "")
                            {

                                StreamWriter read1 = File.AppendText(Cyme_config_txtReportDirectory);

                                string write1 = @"
";

                                read1.Write(write1);
                                read1.Close();

                                ab++;
                            }

                        }

                        if (string.IsNullOrWhiteSpace(aa))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();

                        }

                        if (string.IsNullOrWhiteSpace(bb))
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in HighSideVoltage";

                            read.Write(write);
                            read.Close();

                        }


                        if (string.IsNullOrWhiteSpace(cc))
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in LowSideVoltage";

                            read.Write(write);
                            read.Close();

                        }

                        if (string.IsNullOrWhiteSpace(dd))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in CapacityInKVA";

                            read.Write(write);
                            read.Close();

                        }
                        if (string.IsNullOrWhiteSpace(FF))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in PercentageImpedance";

                            read.Write(write);
                            read.Close();

                        }
                        if (string.IsNullOrWhiteSpace(GG))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in COOLINGTYPE";

                            read.Write(write);
                            read.Close();

                        }

                        if (string.IsNullOrWhiteSpace(II))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in CONNECTIONSTATE";

                            read.Write(write);
                            read.Close();

                        }


                        if (string.IsNullOrWhiteSpace(HH))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
DISTRIBUTIONTRANSFORMER GISID-" + EE + "     Data Not Avilable in TransformerMounting";

                            read.Write(write);
                            read.Close();

                        }


                    }
                }
                catch (Exception ex)
                { }

                
            }


            return ("1");

        }

        public string circuit(string Cyme_config_txtReportDirectory, OleDbConnection conn)
        {
            int ab = 0;
            string AA = "select TG_PHASE,TG_Voltage,TG_NORMALSTATUS,GISID , Phasedesignation , Voltage , RatedCurrent , InsulationType , Make from GIS_CIRCUITBREAKER";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    //conn.Open();

                    string query = "SELECT GISID , COUNT(*) FROM GIS_CIRCUITBREAKER GROUP BY GISID HAVING COUNT(*) > 1";

                    using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                    {
                        OleDbDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"

CIRCUITBREAKER GISID-" + dr1["GISID"] + "     This GISID is Duplicate ";

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
                        //  string dd = dt.Rows[i][3].ToString();
                        string EE = dt.Rows[i][3].ToString().Trim();
                        string FF = dt.Rows[i][4].ToString().Trim();
                        string GG = dt.Rows[i][5].ToString().Trim();
                        string HH = dt.Rows[i][6].ToString().Trim();
                        string II = dt.Rows[i][7].ToString().Trim();
                        string JJ = dt.Rows[i][8].ToString().Trim();


                        if (ab == 0)
                        {
                            if (aa == "" || bb == "" || cc == "" || FF == "" || GG == "" || HH == "" || II == "" || JJ == "")
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
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in PHASE";

                            read.Write(write);
                            read.Close();

                        }

                        if (bb == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in Voltage";

                            read.Write(write);
                            read.Close();

                        }


                        if (cc == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in NORMALSTATUS";

                            read.Write(write);
                            read.Close();

                        }
                        if (FF == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in PHASEDESIGNATION";

                            read.Write(write);
                            read.Close();

                        }

                        if (GG == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in VOLTAGE";

                            read.Write(write);
                            read.Close();

                        }

                        if (HH == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in RATEDCURRENT";

                            read.Write(write);
                            read.Close();

                        }

                        if (II == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in INSULATIONTYPE";

                            read.Write(write);
                            read.Close();

                        }

                        if (JJ == "")
                        {

                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                            string write = @"
SWITCHGEAR GISID-" + EE + "     Data Not Avilable in MAKE";

                            read.Write(write);
                            read.Close();

                        }

                       
                    }
                }
                catch (Exception ex)
                { }

            }

            return ("1");
        }

        public string End(string Cyme_config_txtReportDirectory)
        {
            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

            string write = @"

End DateTime                         " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss:tt");

            read.Write(write);
            read.Close();


            return ("1");
        }


        public string busbar(string st, string Cyme_config_txtReportDirectory)
        {
            //string st = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string con = st + ConfigurationManager.AppSettings["connection"];
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
            // string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Administrator\Desktop\Fme\Tech-Gration\Tech-Gration\Import\Input_Data.mdb";



            OleDbConnection conn = new OleDbConnection(connString);
            int ab = 0;
            string AA = "select Voltage , Material , GISID from GIS_BUSBAR";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                try
                {
                    conn.Open();
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


                            if (ab == 0)
                            {
                                if (aa == "" || bb == "" || cc == "")
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
BUSBAR GISID-" + cc + "     Data Not Avilable in Voltage";

                                read.Write(write);
                                read.Close();

                            }

                            if (bb == "")
                            {

                                StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);

                                string write = @"
BUSBAR GISID-" + cc + "     Data Not Avilable in Material";

                                read.Write(write);
                                read.Close();

                            }
                        }
                    }
                    else
                    {
                        return ("3");
                    }
                }
                catch (Exception ex)
                {
                    return ("2");
                }
            }

            return ("1");

        }

    }
}
