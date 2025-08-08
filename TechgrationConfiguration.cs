using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
//using TechGration.AppCode;
using System.Diagnostics;
using System.Data.OleDb;
using System.Configuration;
using TechGration.AppCode;


namespace TechGration
{
    public partial class TechgrationConfiguration : Form
    {
         string GETFILE = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
         //string GETFILE = AppDomain.CurrentDomain.BaseDirectory;

        public static string techgrationPathforweb = string.Empty;
        public static string Status1 = string.Empty;
        public static string lblStatusP = string.Empty;
        public static string Status2 = string.Empty;
        public static string Status3 = string.Empty;
        public static string Status4 = string.Empty;
        public static string TotalFeeederCount = string.Empty;
        public static string CompleteFeederCount = string.Empty;
        public static string SelectedFeederCount = string.Empty;
        public static string Division1 = string.Empty;
        public static string Division2 = string.Empty;
        public static string Division3 = string.Empty;
        public static string Division4 = string.Empty;
        public static string Substation1 = string.Empty;
        public static string Substation2 = string.Empty;
        public static string Substation3 = string.Empty;
        public static string Substation4 = string.Empty;
        public static string FeederName1 = string.Empty;
        public static string FeederName2 = string.Empty;
        public static string FeederName3 = string.Empty;
        public static string FeederName4 = string.Empty;
        public static string FeederID1 = string.Empty;
        public static string FeederID2 = string.Empty;
        public static string FeederID3 = string.Empty;
        public static string FeederID4 = string.Empty;
        public static DateTime StartTime;
        public static DateTime EndTime;
        public static int incrementFeeder = 0;
        public static int totalpersentage = 0; 

        public static int completepersentag = 0;
        public static int countk11kv = 0;
        public static int count33kv = 0;
        //string Pfromdate = string.Empty;
        //string Ptodate = string.Empty;
        //string Pfordate = string.Empty;
        int countfeeder = 0;

        TreeNode node = new TreeNode();
        TreeNode node1 = new TreeNode();
        TreeNode node2 = new TreeNode();
        TreeNode node3 = new TreeNode();
        TreeNode Noode1 = new TreeNode();
        TreeNode Node1 = new TreeNode();
        TreeNode Node2 = new TreeNode();
        TreeNode Node3 = new TreeNode();
        TreeNode Node4 = new TreeNode();
        TreeNode Node5 = new TreeNode();
        TreeNode noude1 = new TreeNode();
        TreeNode noude2 = new TreeNode();
        TreeNode noude3 = new TreeNode();
        TreeNode noude4 = new TreeNode();
        TreeNode noude5 = new TreeNode();
        TreeNode nodeR = new TreeNode();
        TreeNode nodeZ = new TreeNode();
        TreeNode node4 = new TreeNode();
        AutoCompleteStringCollection MyCollection;
        bool showCalendar = false;
        bool btn1 = false;
        bool btn2 = false;
        bool isTaskSchedulerEvent = false;
        bool isIncrementalFeederSelected = false;
        public TechgrationConfiguration()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Panel panel = new Panel
            {
                Size = new Size(200, 150), // Smaller size than MonthCalendar
                Location = new Point(10, 10),
                AutoScroll = true
            };
            // Add MonthCalendar to the panel
            //Calendar1.Location = new Point(0, 0);
            //panel.Controls.Add(Calendar1);
            //this.Controls.Add(panel);
        }

        private void TechgrationConfiguration_Load(object sender, EventArgs e)
        {
            try
            {
                groupBox7.Visible = false;
                //tabControl1.tabPage1.Visible = false;
               // tabControl1.TabPages.Remove(tabControl1.TabPages[2]); 
                if (showCalendar)
                {
                    Calendar.Visible = true;
                    Calendar1.Visible = true;
                }
                else
                {
                    Calendar.Visible = false;
                    Calendar1.Visible = false;
                    checkpeak.Checked = true;
                    checkmeter.Checked = true;
                    rbtnfordate.Checked = true;
                    rbtnmeterbetween.Checked = true;
                }

                bool isRunningApp = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
                if (isRunningApp && !File.Exists(GETFILE + "\\ConfigFile\\isRunningApp.txt"))
                {
                    MessageBox.Show("Application already running");
                    this.Close();
                }
                else
                {
                    LoadData();
                    bool IsRunButtonProcess = false;
                    bool IsImportFeeder = false;
                    bool IsSchedulerProcess = false;

                    string[] abc = Environment.GetCommandLineArgs();
                    //string[] abc = { "TechGration", "RunProcess" };

                    if (abc != null && abc.Length > 1)
                    {
                        isTaskSchedulerEvent = true;
                    }
                    
                    try
                    {
                        #region
                        LoadFeederlistOntreeview();
                        txtsearch();
                        //deleteAutoErrorLogFiles(Cyme_config_txtReportDirectory.Text);
                        if (RbtnScheduler.Checked || RbtnincScheduler.Checked)
                        {
                            if (isTaskSchedulerEvent)
                            {
                                BtnRun_Click(BtnRun, null);
                                FormCollection fc = Application.OpenForms;
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "TechgrationConfiguration")
                                    {
                                        MessageBox.Show("TechgrationConfiguration");
                                        frm.Close();
                                    }
                                    if (frm.Name == "TechgrationProcess")
                                    {
                                        MessageBox.Show("TechgrationProcess");
                                        frm.Close();
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                         
                    }
                     
                    if (IsImportFeeder == true)
                    {

                        BtnFeederImport_Click(BtnFeederImport, null);
                    }
                    if (IsRunButtonProcess == true)
                    {
                        savefeederlist("ManualFeederList.txt");
                        BtnRun_Click(BtnRun, null);
                    }
                    if (IsSchedulerProcess == true)
                    {
                        BtnFeederImport_Click(BtnFeederImport, null);

                        savefeederlist("SchedulerFeederList.txt");

                        BtnRun_Click(BtnRun, null);
                    }


                    string FeederlistDelete = GETFILE + "\\CYMEUPLOAD";
                    if (Directory.Exists(FeederlistDelete))
                    {
                        System.IO.Directory.Delete(FeederlistDelete, true);
                    }
                    if (!Directory.Exists(FeederlistDelete))
                    {
                        Directory.CreateDirectory(FeederlistDelete);
                    }

                    
                }

                 
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }

        }

        private void CreateMDB(OleDbConnection conn)
        {
            string connectionString = @"Data Source=" + txtGisservername.Text +                       //Create Connection string
                      ";database=" + txtGisDatabasename.Text +
                      ";User ID=" + txtGisUserid.Text +
                      ";Password=" + txtGisPassword.Text;

            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                string database = txtGisDatabasename.Text;
                string schema = txt_GISschemaName.Text;
                if (obj.State != ConnectionState.Open)
                {
                    obj.Open();
                }


                try
                {
                    
                    SqlCommand cmd = new SqlCommand("SELECT codedValue.value('Code[1]', 'nvarchar(max)') AS Code, codedValue.value('Name[1]', 'nvarchar(max)') AS Value, PhysicalName  as Type FROM "+schema+".GDB_ITEMS AS items CROSS APPLY items.Definition.nodes('/GPCodedValueDomain2/CodedValues/CodedValue') AS CodedValues(codedValue) ", obj);
                    string Code = string.Empty;
                    string Value = string.Empty;
                    string Type = string.Empty;



                    SqlDataAdapter AD1 = new SqlDataAdapter(cmd);
                    DataTable DTt11 = new DataTable();
                    AD1.Fill(DTt11);
                    string myqrr1 = "create table GIS_DOMAIN(Code  varchar(250),Value1  varchar(250),Type varchar(250))";
                    OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd2.ExecuteNonQuery();
                    for (int i = 0; i < DTt11.Rows.Count; i++)
                    {

                        Code = DTt11.Rows[i]["Code"].ToString();
                        Value = DTt11.Rows[i]["Value"].ToString();
                        Type = DTt11.Rows[i]["Type"].ToString();
                        if (Type == "PHASE DESIGNATION")
                        {
                            if (Code == "4")
                            {
                                Value = "A";
                            }
                            if (Code == "2")
                            {
                                Value = "B";
                            }
                            if (Code == "1")
                            {
                                Value = "C";
                            }
                            if (Code == "6")
                            {
                                Value = "AB";
                            }
                            if (Code == "3")
                            {
                                Value = "BC";
                            }
                            if (Code == "5")
                            {
                                Value = "AC";
                            }
                            if (Code == "7")
                            {
                                Value = "ABC";
                            }
                        }

                        String kk = "insert into GIS_DOMAIN(Code,Value1,Type) Values(@p1,@p2,@p3)";

                        OleDbCommand cmdw = new OleDbCommand(kk, conn);
                        cmdw.Parameters.AddWithValue("@p1", Code);
                        cmdw.Parameters.AddWithValue("@p2", Value);
                        cmdw.Parameters.AddWithValue("@p3", Type);
                        cmdw.ExecuteNonQuery();
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());

                }


                try
                {
                   
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ["+database+"].[" + schema + "].[PC03_DIEMXUATTUYEN]", obj);
                 
                    string OBJECTID = string.Empty;
                    string XuatTuyen = string.Empty;
                    string DienAp = string.Empty;
                    string SubstationName = string.Empty;

                  
                    //SqlDataAdapter AD1 = new SqlDataAdapter(cmd);
                    DataTable DTt11 = new DataTable();
                    
                    //AD1.Fill(DTt11);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string myqrr1 = "create table PC03_DIEMXUATTUYEN (OBJECTID Varchar(250), XuatTuyen Varchar(250),DienAp Varchar(250),SubstationName Varchar(250))";
                        OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        cmd2.ExecuteNonQuery();
                        // Check the number of columns
                        while (reader.Read())
                        {
                             OBJECTID = reader["OBJECTID"]?.ToString() ?? "NULL";
                             XuatTuyen = reader["XuatTuyen"]?.ToString() ?? "NULL";
                             DienAp = reader["DienAp"]?.ToString() ?? "NULL";
                            SubstationName = reader["SubstationName"]?.ToString() ?? "NULL";
                          
                            String kk = "insert into PC03_DIEMXUATTUYEN (OBJECTID, XuatTuyen,DienAp,SubstationName) Values(@p1,@p2,@p3,@p4)";

                            OleDbCommand cmdw = new OleDbCommand(kk, conn);
                            cmdw.Parameters.AddWithValue("@p1", OBJECTID);
                            cmdw.Parameters.AddWithValue("@p2", XuatTuyen);
                            cmdw.Parameters.AddWithValue("@p3", DienAp);
                            cmdw.Parameters.AddWithValue("@p4", SubstationName);
                            cmdw.ExecuteNonQuery();
                            //Console.WriteLine($"OBJECTID: {OBJECTID}, XuatTuyen: {XuatTuyen}, DienAp: {DienAp}");
                        }


                    }
                   
                    //for (int i = 0; i < DTt11.Rows.Count; i++)
                    //{
                       
                    //    OBJECTID = DTt11.Rows[i]["OBJECTID"].ToString();
                    //    XuatTuyen = DTt11.Rows[i]["XuatTuyen"].ToString();
                    //    DienAp = DTt11.Rows[i]["DienAp"].ToString();
                    //    String kk = "insert into PC03_DIEMXUATTUYEN (OBJECTID, XuatTuyen,DienAp) Values(@p1,@p2,@p3)";

                    //    OleDbCommand cmdw = new OleDbCommand(kk, conn);
                    //    cmdw.Parameters.AddWithValue("@p1", OBJECTID);
                    //    cmdw.Parameters.AddWithValue("@p2", XuatTuyen);
                    //    cmdw.Parameters.AddWithValue("@p3", DienAp);
                    //    cmdw.ExecuteNonQuery();
                    //}
                    conn.Close();

                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());

                }



















                //                string Configpath = GETFILE + "//ConfigFile//Configfile.xml";
                //                DataSet ds = new DataSet();
                //                ds.ReadXml(Configpath);
                //                string domschema = string.Empty;

                //                foreach (DataTable dt in ds.Tables)
                //                {
                //                    if (dt.TableName == "GIS_Database")
                //                    {
                //                        domschema = dt.Rows[0]["Schema_Name1"].ToString();
                //                        break;
                //                    }

                //                }



                //                try
                //                {
                //                    //string get2 = GETFILE + "\\load.txt";
                //                    //if (File.Exists(get2))
                //                    //{
                //                    //    File.Delete(get2);
                //                    //}
                //                    SqlCommand cmd = new SqlCommand("SELECT codedValue.value('Code[1]', 'nvarchar(max)') AS Code, codedValue.value('Name[1]', 'nvarchar(max)') AS Value, PhysicalName  as Type FROM " + domschema + ".GDB_ITEMS AS items CROSS APPLY items.Definition.nodes('/GPCodedValueDomain2/CodedValues/CodedValue') AS CodedValues(codedValue) WHERE items.Name = 'ConductorMaterial'  or items.Name = 'Operating Voltage' or items.Name = 'Subdivision' or items.Name = 'Circle' or items.Name = 'PHASE DESIGNATION'  or items.Name = 'Voltage' or items.Name = 'NORMAL STATUS' or items.Name = 'CONNECTION STATUS' or items.Name = 'FuseType' or items.Name = 'NUMBER OF CORE'or items.Name = 'CoolingType' or items.Name = 'PTCapacity' or items.Name = 'DTCapacity'  or items.Name = 'T_VOLTAGE' or items.Name = 'MAKE' or items.Name = 'TypeOfConsumer'", obj);
                //                    //SqlCommand cmd = new SqlCommand(" SELECT codedValue.value('Code[1]', 'nvarchar(max)') AS Code, codedValue.value('Name[1]', 'nvarchar(max)') AS Value, PhysicalName  as Type FROM " + domschema + ".GDB_ITEMS AS items CROSS APPLY items.Definition.nodes('/GPCodedValueDomain2/CodedValues/CodedValue') AS CodedValues(codedValue) WHERE items.Name = 'ConductorMaterial'  or items.Name = 'Operating Voltage' or items.Name = 'Subdivision' or items.Name = 'Circle' or items.Name = 'PHASE DESIGNATION'  or items.Name = 'Voltage' or items.Name = 'NORMAL STATUS' or items.Name = 'CONNECTION STATUS' or items.Name = 'FuseType' or items.Name = 'NUMBER OF CORE'or items.Name = 'CoolingType' or items.Name = 'PTCapacity' or items.Name = 'DTCapacity'  or items.Name = 'T_VOLTAGE' or items.Name = 'MAKE'", obj);
                //                    // SqlCommand cmd = new SqlCommand("SELECT codedValue.value('Code[1]', 'nvarchar(max)') AS Code, codedValue.value('Name[1]', 'nvarchar(max)') AS Value ,PhysicalName  as Type FROM "+domschema+ ".GDB_ITEMS AS items  CROSS APPLY items.Definition.nodes('/GPCodedValueDomain2/CodedValues/CodedValue') AS CodedValues(codedValue) WHERE items.Name ='Division'  or items.Name = 'Voltage' or items.Name='Subdivision' or items.Name='Circle' or items.Name = 'PHASE DESIGNATION'  or items.Name = 'Voltage' or items.Name='NORMAL STATUS' or items.Name='CONNECTION STATUS' or items.Name='FUSE TYPE' or items.Name='NUMBER OF CORE'", obj);
                //                    // SELECT codedValue.value('Code[1]', 'nvarchar(max)') AS Code, codedValue.value('Name[1]', 'nvarchar(max)') AS Value, PhysicalName  as Type FROM sde.GDB_ITEMS AS items CROSS APPLY items.Definition.nodes('/GPCodedValueDomain2/CodedValues/CodedValue') AS CodedValues(codedValue) WHERE items.Name = 'ConductorMaterial'  or items.Name = 'Operating Voltage' or items.Name = 'Subdivision' or items.Name = 'Circle' or items.Name = 'PHASE DESIGNATION'  or items.Name = 'Voltage' or items.Name = 'NORMAL STATUS' or items.Name = 'CONNECTION STATUS' or items.Name = 'FuseType' or items.Name = 'NUMBER OF CORE'or items.Name = 'CoolingType' or items.Name = 'PTCapacity' or items.Name = 'DTCapacity'  or items.Name = 'T_VOLTAGE'
                //                    string Code = string.Empty;
                //                    string Value = string.Empty;
                //                    string Type = string.Empty;



                //                    SqlDataAdapter AD1 = new SqlDataAdapter(cmd);
                //                    DataTable DTt11 = new DataTable();
                //                    AD1.Fill(DTt11);
                //                    string myqrr1 = "create table GIS_DOMAIN(Code  varchar(250),Value1  varchar(250),Type varchar(250))";
                //                    OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);
                //                    if (conn.State != ConnectionState.Open)
                //                    {
                //                        conn.Open();
                //                    }

                //                    cmd2.ExecuteNonQuery();
                //                    for (int i = 0; i < DTt11.Rows.Count; i++)
                //                    {

                //                        Code = DTt11.Rows[i]["Code"].ToString();
                //                        Value = DTt11.Rows[i]["Value"].ToString();
                //                        Type = DTt11.Rows[i]["Type"].ToString();
                //                        if (Type == "PHASE DESIGNATION")
                //                        {
                //                            if (Code == "4")
                //                            {
                //                                Value = "A";
                //                            }
                //                            if (Code == "2")
                //                            {
                //                                Value = "B";
                //                            }
                //                            if (Code == "1")
                //                            {
                //                                Value = "C";
                //                            }
                //                            if (Code == "6")
                //                            {
                //                                Value = "AB";
                //                            }
                //                            if (Code == "3")
                //                            {
                //                                Value = "BC";
                //                            }
                //                            if (Code == "5")
                //                            {
                //                                Value = "AC";
                //                            }
                //                            if (Code == "7")
                //                            {
                //                                Value = "ABC";
                //                            }
                //                        }

                //                        String kk = "insert into GIS_DOMAIN(Code,Value1,Type) Values(@p1,@p2,@p3)";

                //                        OleDbCommand cmdw = new OleDbCommand(kk, conn);
                //                        cmdw.Parameters.AddWithValue("@p1", Code);
                //                        cmdw.Parameters.AddWithValue("@p2", Value);
                //                        cmdw.Parameters.AddWithValue("@p3", Type);
                //                        cmdw.ExecuteNonQuery();
                //                    }
                //                    conn.Close();

                //                }
                //                catch (Exception ex)
                //                {
                //                    TechError erro = new TechError();
                //                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());

                //                }

                //                try
                //                {
                //                    TechError err1 = new TechError();
                //                    string log = @"==================================================================
                //Table Name:                             CIRCUITSOURCE
                //==================================================================";
                //                    err1.ImportExceptionErrorHandle(GETFILE, log);
                //                    List<string> requiredColumns = new List<string> { "SwitchGearObjID", "FeederID", "SubstationId" };
                //                    //sw.[Voltage], sw.[Feeder_Name], sw.[Circle_Name], sw.[Division_Name]
                //                    foreach (string column in requiredColumns)
                //                    {
                //                        if (!ColumnExists(obj, "CIRCUITSOURCE", column))
                //                        {
                //                            // Log error or skip column
                //                            string error = $"Column {column} does not exist in CIRCUITSOURCE table.";
                //                            err1.ImportExceptionErrorHandle(GETFILE, error);
                //                        }
                //                    }

                //                    SqlCommand cmd1 = new SqlCommand("SELECT Distinct FeederID,SwitchGearObjID,SubstationId,OBJECTID FROM " + database + "." + schema + ".[CIRCUITSOURCE]", obj);

                //                    SqlDataReader dr = cmd1.ExecuteReader();
                //                    // SqlDataAdapter AD1 = new SqlDataAdapter(cmd1);
                //                    DataTable DT1 = new DataTable();
                //                    DT1.Load(dr);
                //                    //OBJECTID,FeederID,FeederID2,FEEDERINFO,SubstationName,SubstationID,SwitchGearObjID,FeederName,FeederSourceInfo,FeederCode,WorkOrderID,ToSubstationID                      
                //                    string FeederID = null;
                //                    string SubstationID = null;
                //                    string SwitchGearObjID = null;
                //                    string OBJECTID = null;

                //                    string myqrr1 = "create table GIS_CIRCUITSOURCE(FeederID varchar(250),SubstationID varchar(250),SwitchGearObjID varchar(250),OBJECTID varchar(250))";
                //                    OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);
                //                    if (conn.State != ConnectionState.Open)
                //                    {
                //                        conn.Open();
                //                    }
                //                    cmd2.ExecuteNonQuery();
                //                    conn.Close();



                //                    for (int i = 0; i < DT1.Rows.Count; i++)
                //                    {
                //                        FeederID = DT1.Rows[i]["FeederID"].ToString();
                //                        SubstationID = DT1.Rows[i]["SubstationID"].ToString();
                //                        SwitchGearObjID = DT1.Rows[i]["SwitchGearObjID"].ToString();
                //                        OBJECTID = DT1.Rows[i]["OBJECTID"].ToString();

                //                        if (string.IsNullOrEmpty(FeederID))
                //                        {
                //                            string write = $@"
                //CIRCUITSOURCE OBJECTID-{OBJECTID}     Data Not Available in FeederID";
                //                            err1.ImportExceptionErrorHandle(GETFILE, write);
                //                        }

                //                        if (string.IsNullOrEmpty(SubstationID))
                //                        {
                //                            string write = $@"
                //CIRCUITSOURCE OBJECTID-{OBJECTID}     Data Not Available in SubstationID";
                //                            err1.ImportExceptionErrorHandle(GETFILE, write);
                //                        }

                //                        if (string.IsNullOrEmpty(SwitchGearObjID))
                //                        {
                //                            string write = $@"
                //CIRCUITSOURCE OBJECTID-{OBJECTID}     Data Not Available in SwitchGearObjID";
                //                            err1.ImportExceptionErrorHandle(GETFILE, write);
                //                        }





                //                        string PHF11 = "INSERT INTO GIS_CIRCUITSOURCE (FeederID,SubstationID,SwitchGearObjID,OBJECTID) VALUES (@P1,@P2,@P3,@P4)";
                //                        if (conn.State != ConnectionState.Open)
                //                        {
                //                            conn.Open();
                //                        }
                //                        OleDbCommand COMT = new OleDbCommand(PHF11, conn);
                //                        COMT.Parameters.AddWithValue("@P1", FeederID);
                //                        COMT.Parameters.AddWithValue("@P2", SubstationID);
                //                        COMT.Parameters.AddWithValue("@P3", SwitchGearObjID);
                //                        COMT.Parameters.AddWithValue("@P4", OBJECTID);
                //                        COMT.ExecuteNonQuery();

                //                    }
                //                    conn.Close();
                //                }
                //                catch (Exception ex)
                //                {
                //                    TechError erro = new TechError();
                //                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
                //                }
                //                try
                //                {
                //                    TechError err1 = new TechError();

                //                    // Define the initial log message
                //                    string log = @"==================================================================
                //Table Name:                             SwitchGear
                //==================================================================";
                //                    err1.ImportExceptionErrorHandle(GETFILE, log);
                //                    List<string> requiredColumns = new List<string> { "FeederID", "Voltage", "Circle_Name", "Division_Name", "Feeder_Name", "OBJECTID" };
                //                    //sw.[Voltage], sw.[Feeder_Name], sw.[Circle_Name], sw.[Division_Name]
                //                    foreach (string column in requiredColumns)
                //                    {
                //                        if (!ColumnExists(obj, "SWITCHGEAR", column))
                //                        {
                //                            // Log error or skip column
                //                            // Console.WriteLine($"Column {column} does not exist in SWITCHGEAR table.");
                //                            string error = $"Column {column} does not exist in SWITCHGEAR table.";
                //                            err1.ImportExceptionErrorHandle(GETFILE, error);
                //                        }
                //                    }


                //                    //SELECT cs.[FeederId], cs.[SubstationId], cs.[FeederName], sw.[Voltage], sw.[Feeder_Name], sw.[Circle_Name], sw.[Division_Name], sub.[NIN], sub.[Name] FROM ([GIS_CircuitSource] AS cs INNER JOIN [GIS_Switchgear] AS sw ON cs.[SwitchGearObjID] = sw.[OBJECTID] AND cs.[FeederID] = sw.[FeederID]) INNER JOIN [GIS_SUBSTATION] AS sub ON cs.[SubstationId] = sub.[NIN]


                //                    SqlCommand GisSwitchGear = new SqlCommand("SELECT Distinct FeederID,Voltage,Circle_Name,Division_Name,Feeder_Name,Region_Name,Zone_Name,OBJECTID,Round([SHAPE].STX,3) AS [longITUDE],Round([SHAPE].STY,3) AS [lATITUDE] FROM " + database + "." + schema + ".SWITCHGEAR where FeederId is not null and FeederId!='' and Voltage is not null and Voltage!='' order by Voltage", obj);
                //                    if (obj.State != ConnectionState.Open)
                //                    {
                //                        obj.Open();
                //                    }
                //                    //string get = GETFILE + "\\load.txt";
                //                    //StreamWriter sw = File.AppendText(get);
                //                    //sw.WriteLine(GisSwitchGear.CommandText);
                //                    //sw.Close();


                //                    SqlDataAdapter AD12 = new SqlDataAdapter(GisSwitchGear);
                //                    DataTable DTt1 = new DataTable();
                //                    AD12.Fill(DTt1);


                //                    String FeederID = null;
                //                    String Voltage = null;
                //                    string Circle_Name = string.Empty;
                //                    string Division_Name = string.Empty;
                //                    string Feeder_Name = string.Empty;
                //                    string OBJECTID = string.Empty;
                //                    string X = string.Empty;
                //                    string Y = string.Empty;
                //                    string Region_Name = string.Empty;
                //                    string Zone_Name = string.Empty;


                //                    string myqrr1 = "create table GIS_Switchgear(FeederID   Varchar(250),Voltage   Varchar(250),Circle_Name Varchar(250),Division_Name Varchar(250),Feeder_Name varchar(250),Zone_Name varchar(250),Region_Name varchar(250),OBJECTID varchar(250),X varchar(250),Y varchar(250))";
                //                    OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);
                //                    conn.Open();
                //                    cmd2.ExecuteNonQuery();
                //                    conn.Close();



                //                    // Loop through each row in the data table (DTt1)
                //                    for (int i = 0; i < DTt1.Rows.Count; i++)
                //                    {
                //                        // Retrieve column values from the current row
                //                        FeederID = DTt1.Rows[i]["FeederID"].ToString();
                //                        Voltage = DTt1.Rows[i]["Voltage"].ToString();
                //                        Circle_Name = DTt1.Rows[i]["Circle_Name"].ToString();
                //                        Division_Name = DTt1.Rows[i]["Division_Name"].ToString();
                //                        Feeder_Name = DTt1.Rows[i]["Feeder_Name"].ToString();
                //                        Zone_Name = DTt1.Rows[i]["Zone_Name"].ToString();
                //                        Region_Name = DTt1.Rows[i]["Region_Name"].ToString();
                //                        OBJECTID = DTt1.Rows[i]["OBJECTID"].ToString();
                //                        X = DTt1.Rows[i]["longITUDE"].ToString();
                //                        Y = DTt1.Rows[i]["lATITUDE"].ToString();
                //                        if (FeederID == "134/113027/202")
                //                        {

                //                        }
                //                        if (FeederID != "")
                //                        {
                //                            string PP = "INSERT INTO GIS_Switchgear (FeederID, Voltage, Circle_Name, Division_Name, Feeder_Name,Zone_Name,Region_Name, OBJECTID,X,Y) VALUES (@P1, @P2, @P3, @P4, @P5, @P6,@P7,@P8,@P9,@P10)";

                //                            // Check for missing values and log them if necessary
                //                            if (string.IsNullOrEmpty(FeederID))
                //                            {
                //                                string write = $@"
                //SwitchGear OBJECTID-{OBJECTID}     Data Not Available in FeederID";
                //                                err1.ImportExceptionErrorHandle(GETFILE, write);
                //                            }

                //                            if (string.IsNullOrEmpty(Voltage))
                //                            {
                //                                string write = $@"
                //SwitchGear OBJECTID-{OBJECTID}     Data Not Available in Voltage";
                //                                err1.ImportExceptionErrorHandle(GETFILE, write);
                //                            }

                //                            if (string.IsNullOrEmpty(Circle_Name))
                //                            {
                //                                string write = $@"
                //SwitchGear OBJECTID-{OBJECTID}     Data Not Available in Circle_Name";
                //                                err1.ImportExceptionErrorHandle(GETFILE, write);
                //                            }

                //                            if (string.IsNullOrEmpty(Division_Name))
                //                            {
                //                                string write = $@"
                //SwitchGear OBJECTID-{OBJECTID}     Data Not Available in Division_Name";
                //                                err1.ImportExceptionErrorHandle(GETFILE, write);
                //                            }

                //                            if (string.IsNullOrEmpty(Feeder_Name))
                //                            {
                //                                string write = $@"
                //SwitchGear OBJECTID-{OBJECTID}     Data Not Available in Feeder_Name";
                //                                err1.ImportExceptionErrorHandle(GETFILE, write);
                //                            }

                //                            // If all necessary fields are available, inse+rt the data into the database
                //                            if (!string.IsNullOrEmpty(FeederID) && !string.IsNullOrEmpty(Voltage) && !string.IsNullOrEmpty(Circle_Name) &&
                //                                !string.IsNullOrEmpty(Division_Name))
                //                            {
                //                                OleDbCommand comr = new OleDbCommand(PP, conn);

                //                                // Open the connection if it is not open already
                //                                if (conn.State != ConnectionState.Open)
                //                                {
                //                                    conn.Open();
                //                                }

                //                                // Add parameters to the command
                //                                comr.Parameters.AddWithValue("@P1", FeederID);
                //                                comr.Parameters.AddWithValue("@P2", Voltage);
                //                                comr.Parameters.AddWithValue("@P3", Circle_Name);
                //                                comr.Parameters.AddWithValue("@P4", Division_Name);
                //                                comr.Parameters.AddWithValue("@P5", Feeder_Name);
                //                                comr.Parameters.AddWithValue("@P6", Zone_Name);
                //                                comr.Parameters.AddWithValue("@P7", Region_Name);
                //                                comr.Parameters.AddWithValue("@P8", OBJECTID);
                //                                comr.Parameters.AddWithValue("@P9", X);
                //                                comr.Parameters.AddWithValue("@P10", Y);

                //                                // Execute the insert query
                //                                comr.ExecuteNonQuery();
                //                            }
                //                        }
                //                        // Define the SQL insert query

                //                    }

                //                    // Final log footer
                //                    conn.Close();
                //                    if (obj.State == ConnectionState.Open)
                //                    {
                //                        obj.Close();
                //                    }
                //                }
                //                catch (Exception ex)
                //                {
                //                    TechError erro = new TechError();
                //                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
                //                }

                //                try
                //                {
                //                    SqlCommand GisSUBSTATION = new SqlCommand("SELECT Name,NIN FROM " + database + "." + schema + ".SUBSTATION", obj);
                //                    if (obj.State != ConnectionState.Open)
                //                    {
                //                        obj.Open();
                //                    }
                //                    //string get = GETFILE + "\\load.txt";
                //                    //StreamWriter sw = File.AppendText(get);
                //                    //sw.WriteLine(GisSwitchGear.CommandText);
                //                    //sw.Close();


                //                    SqlDataAdapter AD121 = new SqlDataAdapter(GisSUBSTATION);
                //                    DataTable DTt11 = new DataTable();
                //                    AD121.Fill(DTt11);

                //                    String NIN = null;
                //                    String Name = null;





                //                    string myqrr1 = "create table GIS_SUBSTATION(NIN   Varchar(250),Name Varchar(250))";
                //                    OleDbCommand cmd2 = new OleDbCommand(myqrr1, conn);

                //                    if (conn.State != ConnectionState.Open)
                //                    {
                //                        conn.Open();
                //                    }

                //                    cmd2.ExecuteNonQuery();
                //                    conn.Close();


                //                    for (int i = 0; i < DTt11.Rows.Count; i++)
                //                    {

                //                        NIN = DTt11.Rows[i]["NIN"].ToString();
                //                        Name = DTt11.Rows[i]["Name"].ToString();



                //                        string PP = "INSERT INTO GIS_SUBSTATION (NIN,Name) values (@P1,@P2)";



                //                        OleDbCommand comr = new OleDbCommand(PP, conn);
                //                        if (conn.State != ConnectionState.Open)
                //                        {
                //                            conn.Open();
                //                        }

                //                        comr.Parameters.AddWithValue("@P1", NIN);
                //                        comr.Parameters.AddWithValue("@P2", Name);
                //                        comr.ExecuteNonQuery();

                //                    }

                //                    conn.Close();
                //                    if (obj.State == ConnectionState.Open)
                //                    {
                //                        obj.Close();
                //                    }
                //                }
                //                catch (Exception ex)
                //                {
                //                    TechError erro = new TechError();
                //                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
                //                }


            }

        }

        public bool ColumnExists(SqlConnection conn, string tableName, string columnName)
        {
            string query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@ColumnName", columnName);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void savefeederlist(string FileName)
        {
            string path = GETFILE + "\\Feeder\\" + FileName;
            if (File.Exists(path))
            {
                string filedate = File.ReadAllText(path);
                if (filedate.Contains(','))
                {
                    string[] feederlist = filedate.Split(',');
                    foreach (string item in feederlist)
                    {
                       
                        nav1.Add(item);
                    }
                }

            }
        }

        private void LoadFeederlistOntreeview()
        {
            try
            {
  
                string counter6 = "";
                string counter8 = "";
                int counter9 = 0;

                string name1 = string.Empty;
                string name2 = string.Empty;
                string name3 = string.Empty;
                string name4 = string.Empty;

                string mdbfile1 = GETFILE + "\\MDB\\FeederList.mdb";



                string connectionstring25 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;
                OleDbConnection con = new OleDbConnection(connectionstring25);


                DataTable DT1 = new DataTable();
                DataTable DT2 = new DataTable();
                DataTable DT3 = new DataTable();
                DataTable DT4 = new DataTable();
                DataTable DT66 = new DataTable();
                DataTable DT132 = new DataTable();
                string selectedfeedercount_11kv = string.Empty;
                string selectedfeedercount_100kv = string.Empty;
                string selectedfeedercount_33kv = string.Empty;
                string selectedfeedercount_22kv = string.Empty;
                string selectedfeedercount_66kv = string.Empty;
                string a = Feederbind11kv(DT1, con, ref selectedfeedercount_11kv);
                //string b = Feederbind33kv(DT2, con, ref selectedfeedercount_33kv);
                 

                string c = "0";
                //string c = Feederbind22kv(DT3, con, ref selectedfeedercount_22kv);
               // string e = Feederbind66kv(DT66, con, ref selectedfeedercount_66kv);
               // string d = Feederbind100kv(DT4, con, ref selectedfeedercount_100kv);
                TotalFeedercount.Text = "Total Feeder: " + (Convert.ToInt32(DT1.Rows.Count.ToString()) + Convert.ToInt32(DT2.Rows.Count.ToString()) + Convert.ToInt32(DT3.Rows.Count.ToString()) + Convert.ToInt32(DT4.Rows.Count.ToString()) + Convert.ToInt32(DT66.Rows.Count.ToString())).ToString();
                TotalFeeederCount = (Convert.ToInt32(DT1.Rows.Count.ToString()) + Convert.ToInt32(DT2.Rows.Count.ToString()) + Convert.ToInt32(DT3.Rows.Count.ToString()) + Convert.ToInt32(DT4.Rows.Count.ToString()) + Convert.ToInt32(DT66.Rows.Count.ToString())).ToString();

                if (selectedfeedercount_11kv == "")
                {
                    selectedfeedercount_11kv = 0.ToString();
                }
                if (selectedfeedercount_33kv == "")
                {
                    selectedfeedercount_33kv = 0.ToString();
                }
                if (selectedfeedercount_22kv == "")
                {
                    selectedfeedercount_22kv = 0.ToString();
                }
                if (selectedfeedercount_100kv == "")
                {
                    selectedfeedercount_100kv = 0.ToString();
                }
                if (selectedfeedercount_66kv == "")
                {
                    selectedfeedercount_66kv = 0.ToString();
                }
                Selectfeedercount.Text = "Selected Feeder : " + (Convert.ToInt32(selectedfeedercount_11kv) + Convert.ToInt32(selectedfeedercount_33kv) + Convert.ToInt32(selectedfeedercount_22kv) + Convert.ToInt32(selectedfeedercount_100kv) + Convert.ToInt32(selectedfeedercount_66kv));

            }
            catch (Exception ex)
            {

                
            }
            

        }

        private void ExpandAndFocusSelectedNode()
        {
            TreeNode selectedNode = treeView1.SelectedNode;

            if (selectedNode != null)
            {
                // Expand the full path
                TreeNode currentNode = selectedNode;
                while (currentNode.Parent != null)
                {
                    currentNode.Parent.Expand();
                    currentNode = currentNode.Parent;
                }

                selectedNode.Expand();
                selectedNode.EnsureVisible();
                treeView1.Focus();
            }
        }
        private string Feederbind11kv(DataTable DT, OleDbConnection con, ref string selectedfeedercount_11kv)
        {

            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;
            string name5 = string.Empty;
            string name6 = string.Empty;
            string counter8 = string.Empty;
            string counter6 = string.Empty;
            int counter9 = 0;

            string vol = "22KV";
            string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 DESC,NetworkID ASC";

            OleDbCommand com = new OleDbCommand(qrry, con);
            con.Open();
            OleDbDataReader dr = com.ExecuteReader();


            DT.Load(dr);
            TreeNode noode = new TreeNode("22KV");
            treeView1.Nodes.Add(noode);

            count33kv = DT.Rows.Count;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string counter7 = DT.Rows[i]["Cheched"].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            //  if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT.Rows.Count; j++)
                {

                    string counter = DT.Rows[j]["FeederId"].ToString().Trim();
                    string counter5 = DT.Rows[j]["Cheched"].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT.Rows[j]["Circle"].ToString();
                    string Division = DT.Rows[j]["Division"].ToString();
                    string Name = DT.Rows[j]["Name"].ToString();

                    if (Name=="")
                    {
                        Name = "NULL";
                    }


                    string FeederId = DT.Rows[j]["FeederId"].ToString();
                    string Region_Name = DT.Rows[j]["Region_Name"].ToString();
                    string Zone_Name = DT.Rows[j]["Zone_Name"].ToString();
                    //string Subdivision = DT.Rows[j]["Subdivision"].ToString();
                    //string SubstationId = DT.Rows[j]["SubstationId"].ToString();
                    //if (Region_Name == "PUNE REGION")
                    //{

                    //}

                    //if (Region_Name != name5)
                    //{
                    //    name5 = Region_Name;
                    //    //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                    //    nodeR = noode.Nodes.Add(Convert.ToString(DT.Rows[j]["Region_Name"].ToString()));
                    //    // treeView1.Nodes.Add(node);
                    //}

                    //if (Zone_Name != name6)
                    //{
                    //    name6 = Zone_Name;
                    //    //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                    //    nodeZ = nodeR.Nodes.Add(Convert.ToString(DT.Rows[j]["Zone_Name"].ToString()));
                    //    // treeView1.Nodes.Add(node);
                    //}


                    //if (Circle != name1)
                    //{
                    //    name1 = Circle;
                    //    //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                    //    node = nodeZ.Nodes.Add(Convert.ToString(DT.Rows[j]["Circle"].ToString()));
                    //    // treeView1.Nodes.Add(node);
                    //}


                    //if (Division != name2)
                    //{
                    //    name2 = Division;
                    //    //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                    //    node1 = node.Nodes.Add(Convert.ToString(DT.Rows[j]["Division"].ToString()));
                    //}

                    //if (Name != name3)
                    //{
                    //    name3 = Name;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node3 = node1.Nodes.Add(Convert.ToString(DT.Rows[j]["Name"].ToString()));
                    //}

                    //if (SubstationId != name6)
                    //{
                    //    name6 = SubstationId;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node4 = node3.Nodes.Add(Convert.ToString(DT.Rows[j]["SubstationId"].ToString()));
                    //}


                    if (Name != name5)
                    {
                        name5 = Name;
                        //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                        Node4 = noode.Nodes.Add(Convert.ToString(DT.Rows[j]["Name"].ToString()));
                    }

                    if (FeederId != name4)
                    {
                        name4 = FeederId;
                        //node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        Node5 = Node4.Nodes.Add(Convert.ToString(DT.Rows[j]["FeederId"].ToString()));
                        if (counter6 == FeederId)
                        {
                            Node5.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                            selectedfeedercount_11kv = counter9.ToString();
                        }
                    }

                    //(TotalFeedercount).ToString() = Convert.ToInt32(Convert.ToInt32(count33kv).ToString() + Convert.ToInt32(countk11kv).ToString()).ToString();

                }
                //treeView1.ExpandAll();
                string DT11KV = DT.Rows.Count.ToString();
                con.Close();
                return DT11KV;
            }
        }
        private string Feederbind33kv(DataTable DT1, OleDbConnection con, ref string selectedfeedercount_33kv)
        {
            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;
            string name5 = string.Empty;
            string name6 = string.Empty;
            string counter8 = string.Empty;
            string counter6 = string.Empty;
            int counter9 = 0;
            string vol = "35 KV";
            //string vol = "340";
            string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC,NetworkID ASC";
            //SELECT NetworkID AS FeederId, 
            //            First(Group1) AS Name,
            //            First(Group2) AS Division,
            //            First(Group3) AS Circle
            //FROM FeederList
            //WHERE voltage = '33 kV'017/288057/403_33KV CPWD
            //GROUP BY NetworkID
            //ORDER BY NetworkID ASC;
            OleDbCommand com = new OleDbCommand(qrry, con);
            con.Open();
            OleDbDataReader dr = com.ExecuteReader();

            //DataTable DT = new DataTable();
            DT1.Load(dr);
            TreeNode noode1 = new TreeNode("35KV");
            treeView1.Nodes.Add(noode1);
            //  TotalFeedercount.Text = "Total Feeder : " + DT1.Rows.Count.ToString();
            count33kv = DT1.Rows.Count;
            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                string counter7 = DT1.Rows[i]["Cheched"].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            //  if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT1.Rows.Count; j++)
                {

                    string counter = DT1.Rows[j]["FeederId"].ToString().Trim();
                    string counter5 = DT1.Rows[j]["Cheched"].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT1.Rows[j]["Circle"].ToString();
                    string Division = DT1.Rows[j]["Division"].ToString();
                    string Name = DT1.Rows[j]["Name"].ToString();
                    string FeederId = DT1.Rows[j]["FeederId"].ToString();
                    string Region_Name = DT1.Rows[j]["Region_Name"].ToString();
                    string Zone_Name = DT1.Rows[j]["Zone_Name"].ToString();

                    if (FeederId == "128/146001/404_33KV NAGALA")
                    {

                    }
                    if (Region_Name != name5)
                    {
                        name5 = Region_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeR = noode1.Nodes.Add(Convert.ToString(DT1.Rows[j]["Region_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }

                    if (Zone_Name != name6)
                    {
                        name6 = Zone_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeZ = nodeR.Nodes.Add(Convert.ToString(DT1.Rows[j]["Zone_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }
                    //string Subdivision = DT1.Rows[j]["Subdivision"].ToString();
                    //string SubstationId = DT1.Rows[j]["SubstationId"].ToString();
                    if (Circle != name1)
                    {
                        name1 = Circle;
                        // node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        node = nodeZ.Nodes.Add(Convert.ToString(DT1.Rows[j]["Circle"].ToString()));
                        // treeView1.Nodes.Add(noode);
                    }


                    if (Division != name2)
                    {
                        name2 = Division;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node1 = node.Nodes.Add(Convert.ToString(DT1.Rows[j]["Division"].ToString()));
                    }

                    if (Name != name3)
                    {
                        //name3 = Division;
                        name3 = Name;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node2 = noode1.Nodes.Add(Convert.ToString(DT1.Rows[j]["Name"].ToString()));
                    }

                    //if (SubstationId != name6)
                    //{
                    //    name6 = SubstationId;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["SubstationId"].ToString()));
                    //}

                    if (FeederId != name4)
                    {
                        name4 = FeederId;
                        // node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        Node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["FeederId"].ToString()));
                        if (counter6 == FeederId)
                        {
                            Node4.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                            selectedfeedercount_33kv = counter9.ToString();
                        }
                    }


                }

            }
            treeView1.ExpandAll();
            string DT33KV = DT1.Rows.Count.ToString();
            con.Close();
            return DT33KV;
        }
        private string Feederbind22kv(DataTable DT2, OleDbConnection con, ref string selectedfeedercount_22kv)
        {
            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;
            string name5 = string.Empty;
            string name6 = string.Empty;
            string counter8 = string.Empty;
            string counter6 = string.Empty;
            int counter9 = 0;
            string vol = "22 kV";
            string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC,NetworkID ASC";

            OleDbCommand com = new OleDbCommand(qrry, con);
            con.Open();
            OleDbDataReader dr = com.ExecuteReader();

            //DataTable DT = new DataTable();
            DT2.Load(dr);
            TreeNode noode1 = new TreeNode("22KV");
            treeView1.Nodes.Add(noode1);
            //  TotalFeedercount.Text = "Total Feeder : " + DT1.Rows.Count.ToString();
            count33kv = DT2.Rows.Count;
            for (int i = 0; i < DT2.Rows.Count; i++)
            {
                string counter7 = DT2.Rows[i]["Cheched"].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            //  if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT2.Rows.Count; j++)
                {

                    string counter = DT2.Rows[j]["FeederId"].ToString().Trim();
                    string counter5 = DT2.Rows[j]["Cheched"].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT2.Rows[j]["Circle"].ToString();
                    string Division = DT2.Rows[j]["Division"].ToString();
                    string Name = DT2.Rows[j]["Name"].ToString();
                    string FeederId = DT2.Rows[j]["FeederId"].ToString();
                    string Region_Name = DT2.Rows[j]["Region_Name"].ToString();
                    string Zone_Name = DT2.Rows[j]["Zone_Name"].ToString();
                    //string Subdivision = DT1.Rows[j]["Subdivision"].ToString();
                    //string SubstationId = DT1.Rows[j]["SubstationId"].ToString();

                    if (Region_Name != name5)
                    {
                        name5 = Region_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeR = noode1.Nodes.Add(Convert.ToString(DT2.Rows[j]["Region_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }

                    if (Zone_Name != name6)
                    {
                        name6 = Zone_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeZ = nodeR.Nodes.Add(Convert.ToString(DT2.Rows[j]["Zone_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }
                    if (Circle != name1)
                    {
                        name1 = Circle;
                        // node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        node = nodeZ.Nodes.Add(Convert.ToString(DT2.Rows[j]["Circle"].ToString()));
                        // treeView1.Nodes.Add(noode);
                    }


                    if (Division != name2)
                    {
                        name2 = Division;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node1 = node.Nodes.Add(Convert.ToString(DT2.Rows[j]["Division"].ToString()));
                    }

                    if (Name != name3)
                    {
                        //name3 = Division;
                        name3 = Name;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node2 = node1.Nodes.Add(Convert.ToString(DT2.Rows[j]["Name"].ToString()));
                    }

                    //if (SubstationId != name6)
                    //{
                    //    name6 = SubstationId;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["SubstationId"].ToString()));
                    //}

                    if (FeederId != name4)
                    {
                        name4 = FeederId;
                        // node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        Node4 = node2.Nodes.Add(Convert.ToString(DT2.Rows[j]["FeederId"].ToString()));
                        if (counter6 == FeederId)
                        {
                            Node4.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                            selectedfeedercount_22kv = counter9.ToString();
                        }
                    }


                }

            }
            treeView1.ExpandAll();
            string DT22KV = DT2.Rows.Count.ToString();
            con.Close();
            return DT22KV;
        }
        private string Feederbind100kv(DataTable DT1, OleDbConnection con, ref string selectedfeedercount_100kv)
        {
            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;
            string name5 = string.Empty;
            string name6 = string.Empty;
            string counter8 = string.Empty;
            string counter6 = string.Empty;
            int counter9 = 0;
            string vol = "100 kV";
            string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC";

            OleDbCommand com = new OleDbCommand(qrry, con);
            con.Open();
            OleDbDataReader dr = com.ExecuteReader();

            //DataTable DT = new DataTable();
            DT1.Load(dr);
            TreeNode noode1 = new TreeNode("100KV");
            treeView1.Nodes.Add(noode1);
            //  TotalFeedercount.Text = "Total Feeder : " + DT1.Rows.Count.ToString();
            count33kv = DT1.Rows.Count;
            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                string counter7 = DT1.Rows[i]["Cheched"].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            //  if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT1.Rows.Count; j++)
                {

                    string counter = DT1.Rows[j]["FeederId"].ToString().Trim();
                    string counter5 = DT1.Rows[j]["Cheched"].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT1.Rows[j]["Circle"].ToString();
                    string Division = DT1.Rows[j]["Division"].ToString();
                    string Name = DT1.Rows[j]["Name"].ToString();
                    string FeederId = DT1.Rows[j]["FeederId"].ToString();
                    string Region_Name = DT1.Rows[j]["Region_Name"].ToString();
                    string Zone_Name = DT1.Rows[j]["Zone_Name"].ToString();
                    //string Subdivision = DT1.Rows[j]["Subdivision"].ToString();
                    //string SubstationId = DT1.Rows[j]["SubstationId"].ToString();
                    if (Region_Name != name5)
                    {
                        name5 = Region_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeR = noode1.Nodes.Add(Convert.ToString(DT1.Rows[j]["Region_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }

                    if (Zone_Name != name6)
                    {
                        name6 = Zone_Name;
                        //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        nodeZ = nodeR.Nodes.Add(Convert.ToString(DT1.Rows[j]["Zone_Name"].ToString()));
                        // treeView1.Nodes.Add(node);
                    }
                    if (Circle != name1)
                    {
                        name1 = Circle;
                        // node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        node = nodeZ.Nodes.Add(Convert.ToString(DT1.Rows[j]["Circle"].ToString()));
                        // treeView1.Nodes.Add(noode);
                    }


                    if (Division != name2)
                    {
                        name2 = Division;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node1 = node.Nodes.Add(Convert.ToString(DT1.Rows[j]["Division"].ToString()));
                    }

                    if (Name != name3)
                    {
                        //name3 = Division;
                        name3 = Name;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node2 = node1.Nodes.Add(Convert.ToString(DT1.Rows[j]["Name"].ToString()));
                    }

                    //if (SubstationId != name6)
                    //{
                    //    name6 = SubstationId;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["SubstationId"].ToString()));
                    //}

                    if (FeederId != name4)
                    {
                        name4 = FeederId;
                        // node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        Node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["FeederId"].ToString()));
                        if (counter6 == FeederId)
                        {
                            Node4.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                            selectedfeedercount_100kv = counter9.ToString();
                        }
                    }


                }

            }
            treeView1.ExpandAll();
            string DT100KV = DT1.Rows.Count.ToString();
            con.Close();
            return DT100KV;
        }

        private string Feederbind66kv(DataTable DT66, OleDbConnection con, ref string selectedfeedercount_66kv)
        {
            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;
            string name5 = string.Empty;
            string name6 = string.Empty;
            string counter8 = string.Empty;
            string counter6 = string.Empty;
            int counter9 = 0;
            string vol = "66 kV";
            string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group2 ASC,NetworkID ASC";

            OleDbCommand com = new OleDbCommand(qrry, con);
            con.Open();
            OleDbDataReader dr = com.ExecuteReader();

            //DataTable DT = new DataTable();
            DT66.Load(dr);
            TreeNode noode1 = new TreeNode("66KV");
            treeView1.Nodes.Add(noode1);
            //  TotalFeedercount.Text = "Total Feeder : " + DT1.Rows.Count.ToString();
            count33kv = DT66.Rows.Count;
            for (int i = 0; i < DT66.Rows.Count; i++)
            {
                string counter7 = DT66.Rows[i]["Cheched"].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            //  if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT66.Rows.Count; j++)
                {

                    string counter = DT66.Rows[j]["FeederId"].ToString().Trim();
                    string counter5 = DT66.Rows[j]["Cheched"].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT66.Rows[j]["Circle"].ToString();
                    string Division = DT66.Rows[j]["Division"].ToString();
                    string Name = DT66.Rows[j]["Name"].ToString();
                    string FeederId = DT66.Rows[j]["FeederId"].ToString();
                    //string Subdivision = DT1.Rows[j]["Subdivision"].ToString();
                    //string SubstationId = DT1.Rows[j]["SubstationId"].ToString();
                    if (Circle != name1)
                    {
                        name1 = Circle;
                        // node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        node = noode1.Nodes.Add(Convert.ToString(DT66.Rows[j]["Circle"].ToString()));
                        // treeView1.Nodes.Add(noode);
                    }


                    if (Division != name2)
                    {
                        name2 = Division;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node1 = node.Nodes.Add(Convert.ToString(DT66.Rows[j]["Division"].ToString()));
                    }

                    if (Name != name3)
                    {
                        //name3 = Division;
                        name3 = Name;
                        //node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                        node2 = node1.Nodes.Add(Convert.ToString(DT66.Rows[j]["Name"].ToString()));
                    }

                    //if (SubstationId != name6)
                    //{
                    //    name6 = SubstationId;
                    //    //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    //    node4 = node2.Nodes.Add(Convert.ToString(DT1.Rows[j]["SubstationId"].ToString()));
                    //}

                    if (FeederId != name5)
                    {
                        name5 = FeederId;
                        // node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        Node4 = node2.Nodes.Add(Convert.ToString(DT66.Rows[j]["FeederId"].ToString()));
                        if (counter6 == FeederId)
                        {
                            Node4.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                            selectedfeedercount_66kv = counter9.ToString();
                        }
                    }


                }

            }
            treeView1.ExpandAll();
            string DT22KV = DT66.Rows.Count.ToString();
            con.Close();
            return DT22KV;
        }

        private void LoadData()
        {
            string Configpath = GETFILE + "\\ConfigFile\\Configfile.xml";

            if (File.Exists(Configpath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Configpath);
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "GIS_RECORD")
                    {
                        String KK = dt.Rows[0]["KEY"].ToString();
                        if (KK == "VISHAL")
                        {
                            btngisrecord.Visible = true;
                        }
                        else
                        {
                            btngisrecord.Visible = false;
                        }
                    }
                    if (dt.TableName == "GIS_Database")
                    {
                        txtGisservername.Text = dt.Rows[0]["Server"].ToString();
                        txtGisUserid.Text = dt.Rows[0]["Username"].ToString();
                        txtGisPassword.Text = dt.Rows[0]["Password"].ToString();
                        txtGisDatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
                        txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();
                        if (dt.Rows[0]["Type"].ToString() == "True")
                        {
                            GISimpbox.Checked = true;
                            txtGisservername.Enabled = true;
                            txtGisDatabasename.Enabled = true;
                            txtGisPassword.Enabled = true;
                            txtGisUserid.Enabled = true;
                            txt_GISschemaName.Enabled = true;
                            BtnGISTestconnSql.Enabled = true;
                        }
                        else if (dt.Rows[0]["Type"].ToString() == "False")
                        {
                            GISimpbox.Checked = false;
                            txtGisservername.Enabled = false;
                            txtGisDatabasename.Enabled = false;
                            txtGisPassword.Enabled = false;
                            txtGisUserid.Enabled = false;
                            txt_GISschemaName.Enabled = false;
                            BtnGISTestconnSql.Enabled = false;
                        }

                    }
                    else if (dt.TableName == "TG_Database")
                    {
                        txtTGservername.Text = dt.Rows[0]["Server"].ToString();
                        txtTGuserid.Text = dt.Rows[0]["Username"].ToString();
                        txtTGpassword.Text = dt.Rows[0]["Password"].ToString();
                        txtTGdatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
                        // txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();
                    }
                    else if (dt.TableName == "Equipment_Database")
                    {
                        txtEqpservername.Text = dt.Rows[0]["Server"].ToString();
                        txtEqpDbname.Text = dt.Rows[0]["Database"].ToString();
                        txtEqpUsername.Text = dt.Rows[0]["Username"].ToString();
                        txtEqppassword.Text = dt.Rows[0]["Password"].ToString();
                    }
                    else if (dt.TableName == "Network_Database")
                    {
                        txtNetservername.Text = dt.Rows[0]["Net_Server"].ToString();
                        txtNetDBname.Text = dt.Rows[0]["Net_Database"].ToString();
                        txtNetUserid.Text = dt.Rows[0]["Net_Username"].ToString();
                        txtNetPassword.Text = dt.Rows[0]["Net_Password"].ToString();
                    }
                    else if (dt.TableName == "Profile_Database")
                    {
                        txtproServer.Text = dt.Rows[0]["Profile_Server"].ToString();
                        txtproDatabaseName.Text = dt.Rows[0]["Profile_Databasename"].ToString();
                        txtproUser.Text = dt.Rows[0]["Profile_Username"].ToString();
                        txtpropassword.Text = dt.Rows[0]["Profile_Password"].ToString();
                    }
                    else if (dt.TableName == "Text_Record")
                    {
                        txtConfigfile.Text = dt.Rows[0]["XML_PATH"].ToString();
                        txtdtfilepath.Text = dt.Rows[0]["DTCSV_PATH"].ToString();
                        txtmeterfilepath.Text = dt.Rows[0]["Meter_PATH"].ToString();
                        txtCYMEdirectory.Text = dt.Rows[0]["CYMDIST"].ToString();
                        txtErrorlog.Text = dt.Rows[0]["Error_Log"].ToString();
                        txtRes.Text = dt.Rows[0]["Residential"].ToString();
                        txtInd.Text = dt.Rows[0]["Industrial"].ToString();
                        runprocessfeedercount.Text = dt.Rows[0]["FeederProcessCount"].ToString();
                    }
                    else if (dt.TableName == "CymDatabaseAccess")
                    {
                        txtCYMDISTnetAcces.Text = dt.Rows[0]["Network"].ToString();
                        txtCYMDISTeqpAcces.Text = dt.Rows[0]["Equipment"].ToString();

                    }
                    else if (dt.TableName == "CymDatabaseType")
                    {
                        CYMDISTDBType.SelectedItem = dt.Rows[0]["Type"].ToString();
                    }
                    else if (dt.TableName == "ExtractionType")
                    {
                        if (dt.Rows[0]["Type"].ToString() == "Scheduler")
                        {
                            RbtnScheduler.Checked = true;
                        }
                        else if (dt.Rows[0]["Type"].ToString() == "Manual")
                        {
                            RbtnManual.Checked = true;
                        }
                        else if (dt.Rows[0]["Type"].ToString() == "incScheduler")
                        {
                            RbtnincScheduler.Checked = true;
                        }

                    }
                    else if (dt.TableName == "Date_Time")
                    {

                        /*  dt.Rows[0]["ForDate_Peak"] =txtfordate.Text.ToString();
                        dt.Rows[0]["ToDate_Peak"] = txttodatepeak.Text.ToString();
                        dt.Rows[0]["FromDate_Peak"] = txtfromdatepeak.Text.ToString();*/
                        txtfordate.Text = dt.Rows[0]["ForDate_Peak"].ToString();
                        txttodatepeak.Text = dt.Rows[0]["ToDate_Peak"].ToString();
                        txtfromdatepeak.Text = dt.Rows[0]["FromDate_Peak"].ToString();
                        txtmeterfordate.Text = dt.Rows[0]["ForDate"].ToString();
                        txtmetertodate.Text = dt.Rows[0]["ToDate"].ToString();
                        txtmeterfromdate.Text = dt.Rows[0]["FromDate"].ToString();
                        if (dt.Rows[0]["Meter_Demand"].ToString() == "True")
                        {
                            checkmeter.Checked = true;
                        }
                        else
                        {
                            checkmeter.Checked = false;
                        }
                        if (dt.Rows[0]["Peak_Demand"].ToString() == "True")
                        {
                            checkpeak.Checked = true;
                        }
                        else
                        {
                            checkpeak.Checked = false;
                        }
                        if (dt.Rows[0]["Type"].ToString() == "FromToEndDate")
                        {
                            rbtnmeterbetween.Checked = true;
                        }
                        else if (dt.Rows[0]["Type"].ToString() == "ForDate")
                        {
                            rbtnmeterfordate.Checked = true;
                        }
                        if (dt.Rows[0]["Type1"].ToString() == "FromToEndDate")
                        {
                            rbtnpeakdate.Checked = true;
                        }
                        else if (dt.Rows[0]["Type1"].ToString() == "ForDate")
                        {
                            rbtnfordate.Checked = true;
                        }


                    }
                }
            }
        }
        //private void LoadData()
        //{
        //    string Configpath = GETFILE + "\\ConfigFile\\Configfile.xml";

        //    if (File.Exists(Configpath))
        //    {
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(Configpath);
        //        foreach (DataTable dt in ds.Tables)
        //        {
        //            if (dt.TableName == "GIS_RECORD")
        //            {
        //                String KK = dt.Rows[0]["KEY"].ToString();
        //                if (KK == "VISHAL")
        //                {
        //                    btngisrecord.Visible = true;
        //                }
        //                else
        //                {
        //                    btngisrecord.Visible = false;
        //                }
        //            }
        //            if (dt.TableName == "GIS_Database")
        //            {
        //                txtGisservername.Text = dt.Rows[0]["Server"].ToString();
        //                txtGisUserid.Text = dt.Rows[0]["Username"].ToString();
        //                txtGisPassword.Text = dt.Rows[0]["Password"].ToString();
        //                txtGisDatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
        //                txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();
        //                if (dt.Rows[0]["Type"].ToString() == "True")
        //                {
        //                    GISimpbox.Checked = true;
        //                    txtGisservername.Enabled = true;
        //                    txtGisDatabasename.Enabled = true;
        //                    txtGisPassword.Enabled = true;
        //                    txtGisUserid.Enabled = true;
        //                    txt_GISschemaName.Enabled = true;
        //                    BtnGISTestconnSql.Enabled = true;
        //                }
        //                else if (dt.Rows[0]["Type"].ToString() == "False")
        //                {
        //                    GISimpbox.Checked = false;
        //                    txtGisservername.Enabled = false;
        //                    txtGisDatabasename.Enabled = false;
        //                    txtGisPassword.Enabled = false;
        //                    txtGisUserid.Enabled = false;
        //                    txt_GISschemaName.Enabled = false;
        //                    BtnGISTestconnSql.Enabled = false;
        //                }

        //            }
        //            else if (dt.TableName == "TG_Database")
        //            {
        //                txtTGservername.Text = dt.Rows[0]["Server"].ToString();
        //                txtTGuserid.Text = dt.Rows[0]["Username"].ToString();
        //                txtTGpassword.Text = dt.Rows[0]["Password"].ToString();
        //                txtTGdatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
        //                // txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();
        //            }
        //            else if (dt.TableName == "Equipment_Database")
        //            {
        //                txtEqpservername.Text = dt.Rows[0]["Server"].ToString();
        //                txtEqpDbname.Text = dt.Rows[0]["Database"].ToString();
        //                txtEqpUsername.Text = dt.Rows[0]["Username"].ToString();
        //                txtEqppassword.Text = dt.Rows[0]["Password"].ToString();
        //            }
        //            else if (dt.TableName == "Network_Database")
        //            {
        //                txtNetservername.Text = dt.Rows[0]["Net_Server"].ToString();
        //                txtNetDBname.Text = dt.Rows[0]["Net_Database"].ToString();
        //                txtNetUserid.Text = dt.Rows[0]["Net_Username"].ToString();
        //                txtNetPassword.Text = dt.Rows[0]["Net_Password"].ToString();
        //            }
        //            else if (dt.TableName == "Profile_Database")
        //            {
        //                txtproServer.Text = dt.Rows[0]["Profile_Server"].ToString();
        //                txtproDatabaseName.Text = dt.Rows[0]["Profile_Databasename"].ToString();
        //                txtproUser.Text = dt.Rows[0]["Profile_Username"].ToString();
        //                txtpropassword.Text = dt.Rows[0]["Profile_Password"].ToString();
        //            }
        //            else if (dt.TableName == "Text_Record")
        //            {
        //                txtConfigfile.Text = dt.Rows[0]["XML_PATH"].ToString();
        //                txtCYMEdirectory.Text = dt.Rows[0]["CYMDIST"].ToString();
        //                txtErrorlog.Text = dt.Rows[0]["Error_Log"].ToString();
        //                txtRes.Text = dt.Rows[0]["Residential"].ToString();
        //                txtInd.Text = dt.Rows[0]["Industrial"].ToString();
        //                runprocessfeedercount.Text = dt.Rows[0]["FeederProcessCount"].ToString();
        //            }
        //            else if (dt.TableName == "CymDatabaseAccess")
        //            {
        //                txtCYMDISTnetAcces.Text = dt.Rows[0]["Network"].ToString();
        //                txtCYMDISTeqpAcces.Text = dt.Rows[0]["Equipment"].ToString();

        //            }
        //            else if (dt.TableName == "CymDatabaseType")
        //            {
        //                CYMDISTDBType.SelectedItem = dt.Rows[0]["Type"].ToString();
        //            }
        //            else if (dt.TableName == "ExtractionType")
        //            {
        //                if (dt.Rows[0]["Type"].ToString() == "Scheduler")
        //                {
        //                    RbtnScheduler.Checked = true;
        //                }
        //                else if (dt.Rows[0]["Type"].ToString() == "Manual")
        //                {
        //                    RbtnManual.Checked = true;
        //                }

        //            }
        //            else if (dt.TableName == "Date_Time")
        //            {
        //                txtmeterfordate.Text = dt.Rows[0]["ForDate"].ToString();
        //                txtmetertodate.Text = dt.Rows[0]["ToDate"].ToString();
        //                txtmeterfromdate.Text = dt.Rows[0]["FromDate"].ToString();
        //                if (dt.Rows[0]["Meter_Demand"].ToString() == "True")
        //                {
        //                    checkmeter.Checked = true;
        //                }
        //                else
        //                {
        //                    checkmeter.Checked = false;
        //                }
        //                if (dt.Rows[0]["Peak_Demand"].ToString() == "True")
        //                {
        //                    checkpeak.Checked = true;
        //                }
        //                else
        //                {
        //                    checkpeak.Checked = false;
        //                }
        //                if (dt.Rows[0]["Type"].ToString() == "FromToEndDate")
        //                {
        //                    rbtnmeterbetween.Checked = true;
        //                }
        //                else if (dt.Rows[0]["Type"].ToString() == "ForDate")
        //                {
        //                    rbtnmeterfordate.Checked = true;
        //                }

        //            }
        //        }
        //    }
        //}

        private void txtsearch()
        {
            this.txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            string mdbpath = GETFILE + ConfigurationManager.AppSettings["Connection1"];
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;


            OleDbConnection conn = new OleDbConnection(connectionString);
            using (OleDbCommand cmd = new OleDbCommand("select DISTINCT NetworkID as FeederId from FeederList", conn))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                OleDbDataReader dr1 = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                MyCollection = new AutoCompleteStringCollection();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MyCollection.Add(dt.Rows[i][0].ToString());
                }

                txtSearch.AutoCompleteCustomSource = MyCollection;
            }
            conn.Close();
        }

        private string BrowseFolder()
        {
            string Path = string.Empty;
            try
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Path = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (IOException ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
            return Path;
        }

        private string BrowseFile()
        {
            string FilePath = string.Empty;
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FilePath = openFileDialog1.FileName;
                }
            }
            catch (IOException ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
            return FilePath;
        }

        //private void BtnFMEdirectory_Click(object sender, EventArgs e)
        //{
        //    string path= BrowseFolder();
        //    if (!string.IsNullOrWhiteSpace(path))
        //    {
        //       // txtFMEdirectory.Text = path;
        //    }

        //}

        private void BtnCYMEdirectory_Click(object sender, EventArgs e)
        {
            string path = BrowseFolder();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtCYMEdirectory.Text = path;
            }
        }

        private void BtnErrorlog_Click(object sender, EventArgs e)
        {
            string path = BrowseFolder();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtErrorlog.Text = path;
            }
        }

        private void BtnCofigfille_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtConfigfile.Text = path;
            }
        }

        private void CYMDISTDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CYMDISTDBType.SelectedItem.ToString() == "MSAccess")
            {
                groupBoxCYMDISTEqp.Visible = true;
                groupBoxCYMDISTEqpSql.Visible = false;
                groupBoxCYMDISTNet.Visible = true;
                groupBoxCYMDISTNetSql.Visible = false;
            }
            if (CYMDISTDBType.SelectedItem.ToString() == "MSSQL")
            {
                groupBoxCYMDISTEqp.Visible = false;
                groupBoxCYMDISTEqpSql.Visible = true;
                groupBoxCYMDISTNet.Visible = false;
                groupBoxCYMDISTNetSql.Visible = true;
            }
        }

        private void RbtnManual_CheckedChanged(object sender, EventArgs e)
        {
            BtnRun.Enabled = true;
            BtnScheduler.Enabled = false;
        }

        private void RbtnScheduler_CheckedChanged(object sender, EventArgs e)
        {
            BtnRun.Enabled = false;
            BtnScheduler.Enabled = true;
        }
        private void RbtnincScheduler_CheckedChanged(object sender, EventArgs e)
        {
            BtnRun.Enabled = false;
            BtnScheduler.Enabled = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveLoadData();

            MessageBox.Show("Save Successfully");
        }

        private void SaveLoadData()
        {
            string Configpath = GETFILE + "//ConfigFile//Configfile.xml";
            if (File.Exists(Configpath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Configpath);
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "GIS_Database")
                    {
                        dt.Rows[0]["Server"] = txtGisservername.Text.ToString();
                        dt.Rows[0]["Username"] = txtGisUserid.Text.ToString();
                        dt.Rows[0]["Password"] = txtGisPassword.Text.ToString();
                        dt.Rows[0]["DataBase_Name"] = txtGisDatabasename.Text.ToString();
                        dt.Rows[0]["Schema_Name"] = txt_GISschemaName.Text.ToString();
                        if (GISimpbox.Checked == true)
                        {
                            dt.Rows[0]["Type"] = "True";
                        }
                        else
                        {
                            dt.Rows[0]["Type"] = "False";
                        }
                    }
                    else if (dt.TableName == "TG_Database")
                    {
                        dt.Rows[0]["Server"] = txtTGservername.Text.ToString();
                        dt.Rows[0]["Username"] = txtTGuserid.Text.ToString();
                        dt.Rows[0]["Password"] = txtTGpassword.Text.ToString();
                        dt.Rows[0]["DataBase_Name"] = txtTGdatabasename.Text.ToString();
                        // dt.Rows[0]["Schema_Name"] = txtTGschemaName.Text.ToString();
                        //dt.Rows[0]["Database_Type"] = GISDBtype.Text.ToString();
                        //dt.Rows[0]["SDEPath"] = txtSDEpath.Text.ToString();
                    }
                    else if (dt.TableName == "Equipment_Database")
                    {

                        dt.Rows[0]["Server"] = txtEqpservername.Text.ToString();
                        dt.Rows[0]["Database"] = txtEqpDbname.Text.ToString();
                        dt.Rows[0]["Username"] = txtEqpUsername.Text.ToString();
                        dt.Rows[0]["Password"] = txtEqppassword.Text.ToString();

                    }
                    else if (dt.TableName == "Network_Database")
                    {
                        dt.Rows[0]["Net_Server"] = txtNetservername.Text.ToString();
                        dt.Rows[0]["Net_Database"] = txtNetDBname.Text.ToString();
                        dt.Rows[0]["Net_Username"] = txtNetUserid.Text.ToString();
                        dt.Rows[0]["Net_Password"] = txtNetPassword.Text.ToString();
                    }
                    else if (dt.TableName == "Profile_Database")
                    {
                        dt.Rows[0]["Profile_Server"] = txtproServer.Text.ToString();
                        dt.Rows[0]["Profile_Databasename"] = txtproDatabaseName.Text.ToString();
                        dt.Rows[0]["Profile_Username"] = txtproUser.Text.ToString();
                        dt.Rows[0]["Profile_Password"] = txtpropassword.Text.ToString();
                    }
                    else if (dt.TableName == "Text_Record")
                    {
                        dt.Rows[0]["XML_PATH"] = txtConfigfile.Text.ToString();
                        dt.Rows[0]["DTCSV_PATH"] = txtdtfilepath.Text.ToString();
                        dt.Rows[0]["Meter_PATH"] = txtmeterfilepath.Text.ToString();
                        // dt.Rows[0]["FMECYMDIST"]=txtFMEdirectory.Text.ToString();
                        dt.Rows[0]["CYMDIST"] = txtCYMEdirectory.Text.ToString();
                        dt.Rows[0]["Error_Log"] = txtErrorlog.Text.ToString();
                        dt.Rows[0]["Residential"] = txtRes.Text.ToString();
                        dt.Rows[0]["Industrial"] = txtInd.Text.ToString();
                        dt.Rows[0]["FeederProcessCount"] = runprocessfeedercount.Text.ToString();

                    }
                    else if (dt.TableName == "CymDatabaseAccess")
                    {
                        dt.Rows[0]["Network"] = txtCYMDISTnetAcces.Text.ToString();
                        dt.Rows[0]["Equipment"] = txtCYMDISTeqpAcces.Text.ToString();

                    }
                    else if (dt.TableName == "CymDatabaseType")
                    {
                        dt.Rows[0]["Type"] = CYMDISTDBType.SelectedItem.ToString();
                    }
                    else if (dt.TableName == "ExtractionType")
                    {
                        if (RbtnScheduler.Checked == true)
                        {
                            dt.Rows[0]["Type"] = "Scheduler";
                        }
                        else if (RbtnManual.Checked == true)
                        {
                            dt.Rows[0]["Type"] = "Manual";
                        }
                        else if (RbtnincScheduler.Checked == true)
                        {
                            dt.Rows[0]["Type"] = "incScheduler";
                        }


                    }
                    else if (dt.TableName == "Date_Time")
                    {

                        dt.Rows[0]["ForDate"] = txtmeterfordate.Text.ToString();
                        dt.Rows[0]["ToDate"] = txtmetertodate.Text.ToString();
                        dt.Rows[0]["FromDate"] = txtmeterfromdate.Text.ToString();
                        dt.Rows[0]["ForDate_Peak"] = txtfordate.Text.ToString();
                        dt.Rows[0]["ToDate_Peak"] = txttodatepeak.Text.ToString();
                        dt.Rows[0]["FromDate_Peak"] = txtfromdatepeak.Text.ToString();
                        if (checkmeter.Checked == true)
                        {
                            //Meter_Demand
                            dt.Rows[0]["Meter_Demand"] = true;
                        }
                        else
                        {
                            dt.Rows[0]["Meter_Demand"] = false;
                        }
                        if (checkpeak.Checked == true)
                        {
                            dt.Rows[0]["Peak_Demand"] = true;
                        }
                        else
                        {
                            dt.Rows[0]["Peak_Demand"] = false;
                        }
                        if (rbtnmeterbetween.Checked == true)
                        {
                            dt.Rows[0]["TYPE"] = "FromToEndDate";
                        }
                        if (rbtnmeterfordate.Checked == true)
                        {
                            dt.Rows[0]["TYPE"] = "ForDate";
                        }
                        if (rbtnpeakdate.Checked == true)
                        {
                            dt.Rows[0]["TYPE1"] = "FromToEndDate";
                        }
                        if (rbtnfordate.Checked == true)
                        {
                            dt.Rows[0]["TYPE1"] = "ForDate";
                        }

                    }
                    ds.WriteXml(Configpath);
                }
            }
        }

        //private void SaveLoadData()
        //{
        //    string Configpath = GETFILE + "//ConfigFile//Configfile.xml";
        //    if (File.Exists(Configpath))
        //    {
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(Configpath);
        //        foreach (DataTable dt in ds.Tables)
        //        {
        //            if (dt.TableName == "GIS_Database")
        //            {
        //                dt.Rows[0]["Server"] = txtGisservername.Text.ToString();
        //                dt.Rows[0]["Username"] = txtGisUserid.Text.ToString();
        //                dt.Rows[0]["Password"] = txtGisPassword.Text.ToString();
        //                dt.Rows[0]["DataBase_Name"] = txtGisDatabasename.Text.ToString();
        //                dt.Rows[0]["Schema_Name"] = txt_GISschemaName.Text.ToString();
        //                if (GISimpbox.Checked == true)
        //                {
        //                    dt.Rows[0]["Type"] = "True";
        //                }
        //                else
        //                {
        //                    dt.Rows[0]["Type"] = "False";
        //                }
        //            }
        //            else if (dt.TableName == "TG_Database")
        //            {
        //                dt.Rows[0]["Server"] = txtTGservername.Text.ToString();
        //                dt.Rows[0]["Username"] = txtTGuserid.Text.ToString();
        //                dt.Rows[0]["Password"] = txtTGpassword.Text.ToString();
        //                dt.Rows[0]["DataBase_Name"] = txtTGdatabasename.Text.ToString();
        //                // dt.Rows[0]["Schema_Name"] = txtTGschemaName.Text.ToString();
        //                //dt.Rows[0]["Database_Type"] = GISDBtype.Text.ToString();
        //                //dt.Rows[0]["SDEPath"] = txtSDEpath.Text.ToString();
        //            }
        //            else if (dt.TableName == "Equipment_Database")
        //            {

        //                dt.Rows[0]["Server"] = txtEqpservername.Text.ToString();
        //                dt.Rows[0]["Database"] = txtEqpDbname.Text.ToString();
        //                dt.Rows[0]["Username"] = txtEqpUsername.Text.ToString();
        //                dt.Rows[0]["Password"] = txtEqppassword.Text.ToString();

        //            }
        //            else if (dt.TableName == "Network_Database")
        //            {
        //                dt.Rows[0]["Net_Server"] = txtNetservername.Text.ToString();
        //                dt.Rows[0]["Net_Database"] = txtNetDBname.Text.ToString();
        //                dt.Rows[0]["Net_Username"] = txtNetUserid.Text.ToString();
        //                dt.Rows[0]["Net_Password"] = txtNetPassword.Text.ToString();
        //            }
        //            else if (dt.TableName == "Profile_Database")
        //            {
        //                dt.Rows[0]["Profile_Server"] = txtproServer.Text.ToString();
        //                dt.Rows[0]["Profile_Databasename"] = txtproDatabaseName.Text.ToString();
        //                dt.Rows[0]["Profile_Username"] = txtproUser.Text.ToString();
        //                dt.Rows[0]["Profile_Password"] = txtpropassword.Text.ToString();
        //            }
        //            else if (dt.TableName == "Text_Record")
        //            {
        //                dt.Rows[0]["XML_PATH"] = txtConfigfile.Text.ToString();
        //                // dt.Rows[0]["FMECYMDIST"]=txtFMEdirectory.Text.ToString();
        //                dt.Rows[0]["CYMDIST"] = txtCYMEdirectory.Text.ToString();
        //                dt.Rows[0]["Error_Log"] = txtErrorlog.Text.ToString();
        //                dt.Rows[0]["Residential"] = txtRes.Text.ToString();
        //                dt.Rows[0]["Industrial"] = txtInd.Text.ToString();
        //                dt.Rows[0]["FeederProcessCount"] = runprocessfeedercount.Text.ToString();

        //            }
        //            else if (dt.TableName == "CymDatabaseAccess")
        //            {
        //                dt.Rows[0]["Network"] = txtCYMDISTnetAcces.Text.ToString();
        //                dt.Rows[0]["Equipment"] = txtCYMDISTeqpAcces.Text.ToString();

        //            }
        //            else if (dt.TableName == "CymDatabaseType")
        //            {
        //                dt.Rows[0]["Type"] = CYMDISTDBType.SelectedItem.ToString();
        //            }
        //            else if (dt.TableName == "ExtractionType")
        //            {
        //                if (RbtnScheduler.Checked == true)
        //                {
        //                    dt.Rows[0]["Type"] = "Scheduler";
        //                }
        //                else if (RbtnManual.Checked == true)
        //                {
        //                    dt.Rows[0]["Type"] = "Manual";
        //                }


        //            }
        //            else if (dt.TableName == "Date_Time")
        //            {

        //                dt.Rows[0]["ForDate"] = txtmeterfordate.Text.ToString();
        //                dt.Rows[0]["ToDate"] = txtmetertodate.Text.ToString();
        //                dt.Rows[0]["FromDate"] = txtmeterfromdate.Text.ToString();
        //                dt.Rows[0]["ForDate_Peak"] = txtfordate.Text.ToString();
        //                dt.Rows[0]["ToDate_Peak"] = txttodatepeak.Text.ToString();
        //                dt.Rows[0]["FromDate_Peak"] = txtfromdatepeak.Text.ToString();
        //                if (checkmeter.Checked == true)
        //                {
        //                    //Meter_Demand
        //                    dt.Rows[0]["Meter_Demand"] = true;
        //                }
        //                else
        //                {
        //                    dt.Rows[0]["Meter_Demand"] = false;
        //                }
        //                if (checkpeak.Checked == true)
        //                {
        //                    dt.Rows[0]["Peak_Demand"] = true;
        //                }
        //                else
        //                {
        //                    dt.Rows[0]["Peak_Demand"] = false;
        //                }
        //                if (rbtnmeterbetween.Checked == true)
        //                {
        //                    dt.Rows[0]["TYPE"] = "FromToEndDate";
        //                }
        //                if (rbtnmeterfordate.Checked == true)
        //                {
        //                    dt.Rows[0]["TYPE"] = "ForDate";
        //                }

        //            }
        //            ds.WriteXml(Configpath);
        //        }
        //    }
        //}

        private void BtnNettestcon_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + txtNetservername.Text +                       //Create Connection string
                          ";database=" + txtNetDBname.Text +
                          ";User ID=" + txtNetUserid.Text +
                          ";Password=" + txtNetPassword.Text;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                }
            }
        }

        private void BtnEqpTestcon_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + txtEqpservername.Text +                       //Create Connection string
                          ";database=" + txtEqpDbname.Text +
                          ";User ID=" + txtEqpUsername.Text +
                          ";Password=" + txtEqppassword.Text;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                }
            }
        }
         
        private void BtnCymDistnet_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtCYMDISTnetAcces.Text = path;
            }
        }

        private void BtnCymdistnettestcon_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtCYMDISTnetAcces.Text.ToString()))
            {
                MessageBox.Show("Test Connection Successfully");
            }
            else
            {
                MessageBox.Show("Test Connection Faild");
            }
        }

        private void BtnCymDisteqp_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtCYMDISTeqpAcces.Text = path;
            }
        }

        private void BtnCymdisteqptestcon_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtCYMDISTeqpAcces.Text.ToString()))
            {
                MessageBox.Show("Test Connection Successfully");
            }
            else
            {
                MessageBox.Show("Test Connection Faild");
            }
        }

        void ManageTreeChecked1(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = node.Checked;
            }
        }

        List<String> CheckedNodes = new List<String>();
        List<string> nav1 = new List<string>();
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            try
            {
                // ShowLoadingGif();

                List<String> CheckedNodes1 = CheckedNodes.Distinct().ToList();

                ManageTreeChecked1(e.Node);
                {
                    string bb = string.Empty;
                    string dd = string.Empty;
                    if (e.Node.Checked)
                    {

                        CheckedNodes.Add(e.Node.FullPath.ToString());
                        bb = e.Node.FullPath;
                    }
                    else
                    {
                        CheckedNodes.Remove(e.Node.FullPath.ToString());
                        dd = e.Node.FullPath;
                    }
                    string vv = string.Empty;
                    string zz = string.Empty;
                    string AA = "Selected Feeder : ";
                    //string BB = "33KV Feeder : ";
                    //string CC = "66KV Feeder : ";
                    string[] arr = bb.Split('\\');
                    string[] arr1 = dd.Split('\\');

                    int ddlength = arr1.Length;
                    string count1 = string.Empty;
                    string count7 = string.Empty;
                    string count8 = string.Empty;
                    if (!string.IsNullOrWhiteSpace(bb))
                    {
                        int bblength = 0;
                        if (bb.Contains("11KV"))
                        {
                            bblength = arr.Length;
                            int kk = 1;
                            for (int i = 6; i <= arr.Length; i++)
                            {
                                if ((i == 6 && bblength == 7) || bblength == 7 && kk == 1)
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);
                                    kk = 2;
                                }
                            }
                            List<String> Checkedlist1 = nav1.Distinct().ToList();
                            string count3 = Checkedlist1.Count.ToString();
                            count1 = Checkedlist1.Count.ToString();
                            Selectfeedercount.Text = AA + count3.ToString();
                            //lbl11kvfeeder.Text = "Uncheck Checkbox : " + (int.Parse(count) - (int.Parse(count1))).ToString();
                        }
                        else if (bb.Contains("33KV"))
                        {
                            bblength = arr.Length;
                            int kk = 1;
                            for (int i = 6; i <= arr.Length; i++)
                            {
                                if ((i == 6 && bblength == 7) || (bblength == 7 && kk == 1))
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);
                                    kk = 2;
                                }
                            }
                            List<String> Checkedlist2 = nav1.Distinct().ToList();
                            string count4 = Checkedlist2.Count.ToString();
                            count7 = Checkedlist2.Count.ToString();
                            Selectfeedercount.Text = AA + count4.ToString();

                        }
                        else if (bb.Contains("22KV"))
                        {
                            bblength = arr.Length;
                            int kk = 1;
                            for (int i = 1; i <= arr.Length; i++)
                            {
                                if ((i == 2 && bblength == 3) || (bblength == 7 && kk == 1))
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);
                                    kk = 2;
                                }
                            }
                            List<String> Checkedlist2 = nav1.Distinct().ToList();
                            string count4 = Checkedlist2.Count.ToString();
                            count7 = Checkedlist2.Count.ToString();
                            Selectfeedercount.Text = AA + count4.ToString();

                        }
                        else if (bb.Contains("35KV"))
                        {
                            bblength = arr.Length;
                            int kk = 1;
                            for (int i = 1; i <= arr.Length; i++)
                            {
                                if ((i == 1 && bblength == 2) || (bblength == 7 && kk == 1))
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);
                                    kk = 2;
                                }
                            }
                            List<String> Checkedlist2 = nav1.Distinct().ToList();
                            string count4 = Checkedlist2.Count.ToString();
                            count7 = Checkedlist2.Count.ToString();
                            Selectfeedercount.Text = AA + count4.ToString();

                        }
                        else if (bb.Contains("100KV"))
                        {
                            bblength = arr.Length;
                            for (int i = 4; i <= arr.Length; i++)
                            {
                                if (i == 6)
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);
                                }
                            }
                            List<String> Checkedlist2 = nav1.Distinct().ToList();
                            string count4 = Checkedlist2.Count.ToString();
                            count7 = Checkedlist2.Count.ToString();
                            Selectfeedercount.Text = AA + count4.ToString();

                        }
                        List<String> Checkedlist = nav1.Distinct().ToList();
                        string count2 = Checkedlist.Count.ToString();
                        count1 = Checkedlist.Count.ToString();

                    }
                    else
                    {

                        for (int j = 1; j <= arr1.Length; j++)
                        {
                            if (ddlength == 3)
                            {
                                if (j == 2)
                                {
                                    zz = arr1[j];
                                    while (nav1.Contains(zz))
                                    {
                                        nav1.Remove(zz);
                                    }
                                }
                            }
                            

                            List<String> Checkedlist = nav1.Distinct().ToList();
                            count1 = Checkedlist.Count.ToString();
                            Selectfeedercount.Text = "Selected Feeder: " + count1;
                            //label6.Text = "Uncheck Checkbox : " + (int.Parse(count) - (int.Parse(count1))).ToString();
                            try
                            {
                                if (RbtnCheckedfeeder.Checked == true)
                                {
                                    RbtnCheckedfeeder.Checked = false;
                                }
                                if (Rbtnuncheckedfeeder.Checked == true)
                                {
                                    Rbtnuncheckedfeeder.Checked = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                TechError erro = new TechError();
                                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                            }
                        }
                    }
                }

                // HideLoadingGif();
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }




        }

        private void BtnlogFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtErrorlog.Text);
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }


        private void BtnScheduler_Click(object sender, EventArgs e)
        {
            if (RbtnScheduler.Checked)
            {
                try
                {
                    string filepath = GETFILE + "\\Feeder\\SchedulerFeed erList.txt";
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    string xml = GETFILE + "\\ConfigFile\\Configfile.xml";
                    DataSet ds = new DataSet();
                    ds.ReadXml(xml);
                    ds.Tables["ExtractionType"].Rows[0]["Type"] = "Scheduler";
                    ds.WriteXml(xml);
                    List<string> checklist = nav1.Distinct().ToList();
                    if (checklist.Count > 0)
                    {
                        string text = string.Empty;
                        for (int i = 0; i < checklist.Count; i++)
                        {
                            text += "'" + checklist[i] + "',";
                        }
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            text = text.TrimEnd(',');
                            StreamWriter sw = File.AppendText(filepath);
                            sw.WriteLine(text);
                            sw.Close();
                            MessageBox.Show("Scheduler Saved");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select at least one feeder.");
                    }

                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
            }

            else if  (RbtnincScheduler.Checked)
            {
            //    try
            //    {
            //        string filepath = GETFILE + "\\Feeder\\SchedulerFeederList.txt";
            //        if (File.Exists(filepath))
            //        {
            //            File.Delete(filepath);
            //        }
            //        string xml = GETFILE + "\\ConfigFile\\Configfile.xml";
            //        DataSet ds = new DataSet();
            //        ds.ReadXml(xml);
            //        ds.Tables["ExtractionType"].Rows[0]["Type"] = "Scheduler";
            //        ds.WriteXml(xml);
            //        List<string> checklist = nav1.Distinct().ToList();
            //        if (checklist.Count > 0)
            //        {
            //            string text = string.Empty;
            //            for (int i = 0; i < checklist.Count; i++)
            //            {
            //                text += "'" + checklist[i] + "',";
            //            }
            //            if (!string.IsNullOrWhiteSpace(text))
            //            {
            //                text = text.TrimEnd(',');
            //                StreamWriter sw = File.AppendText(filepath);
            //                sw.WriteLine(text);
            //                sw.Close();
            //                MessageBox.Show("Scheduler Saved");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please select at least one feeder.");
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        TechError erro = new TechError();
            //        erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            //    }
            }
        
        }
        AppCode.ConfigFileData cf = new AppCode.ConfigFileData();
        Thread th;
        private void opennewform1(object obj)
        {
            Application.Run(new TechgrationProcessForOne());
        }

        private void opennewform2(object obj)
        {
            Application.Run(new TechgrationProcess());
        }

        private void opennewform3(object obj)
        {
            Application.Run(new TechgrationProcessForThree());
        }

        private void opennewform4(object obj)
        {
            Application.Run(new TechgrationProcessForFour());
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (GISimpbox.Checked == true)         //GIS DATA...
                {
                    techgrationPathforweb = GETFILE;
                    if (RbtnincScheduler.Checked == true)
                    {
                        try
                        {
                            int j = Convert.ToInt32(runprocessfeedercount.Text.ToString());

                            btnTreeView1INCScheduler();
                            LoadDataConfig();

                            SelectedFeederList SFD = new SelectedFeederList();
                            DataTable Dt = new DataTable();
                            Dt = SFD.GetFeederTable(GETFILE);
                            if (Dt.Rows.Count<=0)
                            {
                                TechgrationProcess tp1 = new TechgrationProcess();
                                tp1.setValue();
                            }
                            int TotalCount = Dt.Rows.Count;
                            SelectedFeederCount = Convert.ToString(TotalCount);
                            totalpersentage = TotalCount * 100;
                            ErrorLog aa = new ErrorLog();
                            string Logpath = aa.fir(cf.Errorlog, Dt.Rows.Count.ToString(), ref StartTime);

                            this.Close();                                                           // Close Current window

                            if (j == 1)
                            {
                                th = new Thread(opennewform1);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + j)
                                {
                                    RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[i]["FeederId"].ToString());
                                }
                            }

                            if (j == 2)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                }
                                else if (Dt.Rows.Count >= 2)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            first = 2;
                                        }

                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }

                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }





                                }



                            }


                            if (j == 3)
                            {
                                int k = j;
                                int first = 1;
                                th = new Thread(opennewform3);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                {

                                    if (first == 1)
                                    {
                                        if (IsRunningProcess1 == false)
                                        {
                                            Status1 = "Reading GIS";
                                            Thread trd = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd.IsBackground = true;
                                            trd.Start();
                                        }
                                        if (IsRunningProcess2 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }
                                        if (IsRunningProcess3 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }

                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Thread.Sleep(1000);
                                        }

                                        if (i + 1 == (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                            {
                                                Thread.Sleep(1000);
                                            }
                                        }

                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                        first = 2;
                                    }
                                    else
                                    {
                                        k = 0;
                                        int a = 0;
                                        int b = 0;
                                        int c = 0;
                                        if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                        {
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            a = 1;
                                        }

                                        if (IsRunningProcess2 == false && ((i + a) < Dt.Rows.Count))
                                        {

                                            Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                            trd2.IsBackground = true;
                                            trd2.Start();
                                            b = 1;
                                        }

                                        if (IsRunningProcess3 == false && ((i + a + b) < Dt.Rows.Count))
                                        {

                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            c = 1;
                                        }

                                        k = a + b + c;

                                        if (i + k >= (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed")) || !((Status2 == "Feeder Completed")) || !((Status3 == "Feeder Completed")))
                                            {
                                                Status4 = "if condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                                Thread.Sleep(1000);
                                            }
                                        }
                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Status4 = "else condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                            Thread.Sleep(1000);
                                        }


                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                    }

                                }
                            }
                            if (j == 4)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());

                                }
                                else if (Dt.Rows.Count == 2)
                                {
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                }
                                else if (Dt.Rows.Count == 3)
                                {
                                    th = new Thread(opennewform3);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }
                                    if (IsRunningProcess3 == false)
                                    {
                                        Status3 = "Reading GIS";
                                        Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[2]["FeederId"].ToString(), Dt.Rows[2]["Division"].ToString(), Dt.Rows[2]["Name"].ToString(), Dt.Rows[2]["FeederName"].ToString())));
                                        trd3.IsBackground = true;
                                        trd3.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                    if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[2]["FeederId"].ToString());
                                        IsRunningProcess3 = false;

                                    }
                                }

                                else if (Dt.Rows.Count >= 4)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform4);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }
                                            if (IsRunningProcess3 == false)
                                            {
                                                Status3 = "Reading GIS";
                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                            }
                                            if (IsRunningProcess4 == false)
                                            {
                                                Status4 = "Reading GIS";
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + 3]["FeederId"].ToString(), Dt.Rows[i + 3]["Division"].ToString(), Dt.Rows[i + 3]["Name"].ToString(), Dt.Rows[i + 3]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            first = 2;
                                        }
                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }
                                            if (IsRunningProcess3 == false && (i + a + b) < Dt.Rows.Count)
                                            {

                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                                c = 1;
                                            }
                                            if (IsRunningProcess4 == false && (i + a + b + c) < Dt.Rows.Count)
                                            {
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + a + b + c]["FeederId"].ToString(), Dt.Rows[i + a + b + c]["Division"].ToString(), Dt.Rows[i + a + b + c]["Name"].ToString(), Dt.Rows[i + a + b + c]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                                d = 1;
                                            }
                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed") && (Status3 == "Feeder Completed") && (Status4 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }
                                }


                            }

                            aa.Total_ImportFeeder(Logpath, Convert.ToInt32(SelectedFeederCount), StartTime, EndTime);



                            Thread.Sleep(2000);

                            TechgrationProcess tp = new TechgrationProcess();
                            tp.setValue();
                            //TechgrationProcess tp = new TechgrationProcess();
                            //tp.setValue();
                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                        }
                    }

                    if (RbtnScheduler.Checked == true)
                    {
                        try
                        {
                            int j = Convert.ToInt32(runprocessfeedercount.Text.ToString());

                            btnTreeView1Scheduler();
                            LoadDataConfig();

                            SelectedFeederList SFD = new SelectedFeederList();
                            DataTable Dt = new DataTable();
                            Dt = SFD.GetFeederTable(GETFILE);
                            if (Dt.Rows.Count <= 0)
                            {
                                TechgrationProcess tp1 = new TechgrationProcess();
                                tp1.setValue();
                            }
                            int TotalCount = Dt.Rows.Count;
                            SelectedFeederCount = Convert.ToString(TotalCount);
                            totalpersentage = TotalCount * 100;
                            ErrorLog aa = new ErrorLog();
                            string Logpath = aa.fir(cf.Errorlog, Dt.Rows.Count.ToString(), ref StartTime);
                            this.Close();                                                           // Close Current window
                            if (j == 1)
                            {
                                th = new Thread(opennewform1);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + j)
                                {
                                    RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[i]["FeederId"].ToString());
                                }
                            }

                            if (j == 2)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                }
                                else if (Dt.Rows.Count >= 2)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            first = 2;
                                        }

                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }

                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }

                                }
                                 
                            }


                            if (j == 3)
                            {
                                int k = j;
                                int first = 1;
                                th = new Thread(opennewform3);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                {

                                    if (first == 1)
                                    {
                                        if (IsRunningProcess1 == false)
                                        {
                                            Status1 = "Reading GIS";
                                            Thread trd = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd.IsBackground = true;
                                            trd.Start();
                                        }
                                        if (IsRunningProcess2 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }
                                        if (IsRunningProcess3 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }

                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Thread.Sleep(1000);
                                        }

                                        if (i + 1 == (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                            {
                                                Thread.Sleep(1000);
                                            }
                                        }

                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                        first = 2;
                                    }
                                    else
                                    {
                                        k = 0;
                                        int a = 0;
                                        int b = 0;
                                        int c = 0;
                                        if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                        {
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            a = 1;
                                        }

                                        if (IsRunningProcess2 == false && ((i + a) < Dt.Rows.Count))
                                        {

                                            Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                            trd2.IsBackground = true;
                                            trd2.Start();
                                            b = 1;
                                        }

                                        if (IsRunningProcess3 == false && ((i + a + b) < Dt.Rows.Count))
                                        {

                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            c = 1;
                                        }

                                        k = a + b + c;

                                        if (i + k >= (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed")) || !((Status2 == "Feeder Completed")) || !((Status3 == "Feeder Completed")))
                                            {
                                                Status4 = "if condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                                Thread.Sleep(1000);
                                            }
                                        }
                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Status4 = "else condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                            Thread.Sleep(1000);
                                        }


                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                    }

                                }
                            }
                            if (j == 4)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());

                                }
                                else if (Dt.Rows.Count == 2)
                                {
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                }
                                else if (Dt.Rows.Count == 3)
                                {
                                    th = new Thread(opennewform3);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }
                                    if (IsRunningProcess3 == false)
                                    {
                                        Status3 = "Reading GIS";
                                        Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[2]["FeederId"].ToString(), Dt.Rows[2]["Division"].ToString(), Dt.Rows[2]["Name"].ToString(), Dt.Rows[2]["FeederName"].ToString())));
                                        trd3.IsBackground = true;
                                        trd3.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                    if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[2]["FeederId"].ToString());
                                        IsRunningProcess3 = false;

                                    }
                                }

                                else if (Dt.Rows.Count >= 4)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform4);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }
                                            if (IsRunningProcess3 == false)
                                            {
                                                Status3 = "Reading GIS";
                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                            }
                                            if (IsRunningProcess4 == false)
                                            {
                                                Status4 = "Reading GIS";
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + 3]["FeederId"].ToString(), Dt.Rows[i + 3]["Division"].ToString(), Dt.Rows[i + 3]["Name"].ToString(), Dt.Rows[i + 3]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            first = 2;
                                        }
                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }
                                            if (IsRunningProcess3 == false && (i + a + b) < Dt.Rows.Count)
                                            {

                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                                c = 1;
                                            }
                                            if (IsRunningProcess4 == false && (i + a + b + c) < Dt.Rows.Count)
                                            {
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + a + b + c]["FeederId"].ToString(), Dt.Rows[i + a + b + c]["Division"].ToString(), Dt.Rows[i + a + b + c]["Name"].ToString(), Dt.Rows[i + a + b + c]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                                d = 1;
                                            }
                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed") && (Status3 == "Feeder Completed") && (Status4 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }
                                }


                            }

                            aa.Total_ImportFeeder(Logpath, Convert.ToInt32(SelectedFeederCount), StartTime, EndTime);



                            Thread.Sleep(2000);

                            TechgrationProcess tp = new TechgrationProcess();
                            tp.setValue();
                            //TechgrationProcess tp = new TechgrationProcess();
                            //tp.setValue();
                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                        }
                    }

                    if (RbtnManual.Checked == true)
                    {
                        try
                        {
                            int j = Convert.ToInt32(runprocessfeedercount.Text.ToString());

                            btnTreeView1Menual();
                            LoadDataConfig();


                            SelectedFeederList SFD = new SelectedFeederList();
                            DataTable Dt = new DataTable();
                            Dt = SFD.GetFeederTable(GETFILE);
                            int TotalCount = Dt.Rows.Count;
                            SelectedFeederCount = Convert.ToString(TotalCount);
                            totalpersentage = TotalCount * 100;
                            ErrorLog aa = new ErrorLog();
                            string Logpath = aa.fir(cf.Errorlog, Dt.Rows.Count.ToString(), ref StartTime);

                            this.Close();                                                           // Close Current window

                            if (j == 1)
                            {
                                th = new Thread(opennewform1);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + j)
                                {

                                    RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[i]["FeederId"].ToString());
                                }
                            }

                            if (j == 2)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                }
                                else if (Dt.Rows.Count >= 2)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            first = 2;
                                        }

                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }

                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }

                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }





                                }



                            }


                            if (j == 3)
                            {
                                int k = j;
                                int first = 1;
                                th = new Thread(opennewform3);
                                th.SetApartmentState(ApartmentState.MTA);
                                th.Start();
                                for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                {

                                    if (first == 1)
                                    {
                                        if (IsRunningProcess1 == false)
                                        {
                                            Status1 = "Reading GIS";
                                            Thread trd = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd.IsBackground = true;
                                            trd.Start();
                                        }
                                        if (IsRunningProcess2 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }
                                        if (IsRunningProcess3 == false)
                                        {
                                            Status2 = "Reading GIS";
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                        }

                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Thread.Sleep(1000);
                                        }

                                        if (i + 1 == (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                            {
                                                Thread.Sleep(1000);
                                            }
                                        }

                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                        first = 2;
                                    }
                                    else
                                    {
                                        k = 0;
                                        int a = 0;
                                        int b = 0;
                                        int c = 0;
                                        if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                        {
                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            a = 1;
                                        }

                                        if (IsRunningProcess2 == false && ((i + a) < Dt.Rows.Count))
                                        {

                                            Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                            trd2.IsBackground = true;
                                            trd2.Start();
                                            b = 1;
                                        }

                                        if (IsRunningProcess3 == false && ((i + a + b) < Dt.Rows.Count))
                                        {

                                            Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                            trd1.IsBackground = true;
                                            trd1.Start();
                                            c = 1;
                                        }

                                        k = a + b + c;

                                        if (i + k >= (Dt.Rows.Count - 1))
                                        {
                                            while (!((Status1 == "Feeder Completed")) || !((Status2 == "Feeder Completed")) || !((Status3 == "Feeder Completed")))
                                            {
                                                Status4 = "if condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                                Thread.Sleep(1000);
                                            }
                                        }
                                        while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                        {
                                            Status4 = "else condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                            Thread.Sleep(1000);
                                        }


                                        if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess1 = false;

                                        }
                                        if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess2 = false;

                                        }
                                        if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                        {
                                            aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[0]["FeederId"].ToString());
                                            IsRunningProcess3 = false;

                                        }
                                    }

                                }
                            }
                            if (j == 4)
                            {
                                if (Dt.Rows.Count == 1)
                                {
                                    th = new Thread(opennewform1);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();

                                    RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString());
                                    aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());

                                }
                                else if (Dt.Rows.Count == 2)
                                {
                                    th = new Thread(opennewform2);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                }
                                else if (Dt.Rows.Count == 3)
                                {
                                    th = new Thread(opennewform3);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    if (IsRunningProcess1 == false)
                                    {
                                        Status1 = "Reading GIS";
                                        Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[0]["FeederId"].ToString(), Dt.Rows[0]["Division"].ToString(), Dt.Rows[0]["Name"].ToString(), Dt.Rows[0]["FeederName"].ToString())));
                                        trd1.IsBackground = true;
                                        trd1.Start();
                                    }
                                    if (IsRunningProcess2 == false)
                                    {
                                        Status2 = "Reading GIS";
                                        Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[1]["FeederId"].ToString(), Dt.Rows[1]["Division"].ToString(), Dt.Rows[1]["Name"].ToString(), Dt.Rows[1]["FeederName"].ToString())));
                                        trd2.IsBackground = true;
                                        trd2.Start();
                                    }
                                    if (IsRunningProcess3 == false)
                                    {
                                        Status3 = "Reading GIS";
                                        Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[2]["FeederId"].ToString(), Dt.Rows[2]["Division"].ToString(), Dt.Rows[2]["Name"].ToString(), Dt.Rows[2]["FeederName"].ToString())));
                                        trd3.IsBackground = true;
                                        trd3.Start();
                                    }

                                    while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3))
                                    {
                                        Thread.Sleep(1000);
                                    }


                                    if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 1, Dt.Rows[0]["FeederId"].ToString());
                                        IsRunningProcess1 = false;

                                    }
                                    if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 2, Dt.Rows[1]["FeederId"].ToString());
                                        IsRunningProcess2 = false;

                                    }
                                    if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                    {
                                        aa.combineErrorLog(Logpath, GETFILE, 3, Dt.Rows[2]["FeederId"].ToString());
                                        IsRunningProcess3 = false;

                                    }
                                }

                                else if (Dt.Rows.Count >= 4)
                                {
                                    int k = j;
                                    int first = 1;
                                    th = new Thread(opennewform4);
                                    th.SetApartmentState(ApartmentState.MTA);
                                    th.Start();
                                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                                    {
                                        if (first == 1)
                                        {
                                            if (IsRunningProcess1 == false)
                                            {
                                                Status1 = "Reading GIS";
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                            }
                                            if (IsRunningProcess2 == false)
                                            {
                                                Status2 = "Reading GIS";
                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                            }
                                            if (IsRunningProcess3 == false)
                                            {
                                                Status3 = "Reading GIS";
                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 2]["Division"].ToString(), Dt.Rows[i + 2]["Name"].ToString(), Dt.Rows[i + 2]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                            }
                                            if (IsRunningProcess4 == false)
                                            {
                                                Status4 = "Reading GIS";
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + 3]["FeederId"].ToString(), Dt.Rows[i + 3]["Division"].ToString(), Dt.Rows[i + 3]["Name"].ToString(), Dt.Rows[i + 3]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                            }

                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                Thread.Sleep(1000);
                                            }

                                            if (i + 3 == (Dt.Rows.Count - 1))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    Thread.Sleep(1000);
                                                }
                                            }

                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            first = 2;
                                        }
                                        else
                                        {
                                            k = 0;
                                            int a = 0;
                                            int b = 0;
                                            int c = 0;
                                            int d = 0;
                                            if (IsRunningProcess1 == false && i < Dt.Rows.Count)
                                            {
                                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                                trd1.IsBackground = true;
                                                trd1.Start();
                                                a = 1;
                                            }
                                            if (IsRunningProcess2 == false && (i + a) < Dt.Rows.Count)
                                            {

                                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                                trd2.IsBackground = true;
                                                trd2.Start();
                                                b = 1;
                                            }
                                            if (IsRunningProcess3 == false && (i + a + b) < Dt.Rows.Count)
                                            {

                                                Thread trd3 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                                trd3.IsBackground = true;
                                                trd3.Start();
                                                c = 1;
                                            }
                                            if (IsRunningProcess4 == false && (i + a + b + c) < Dt.Rows.Count)
                                            {
                                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + a + b + c]["FeederId"].ToString(), Dt.Rows[i + a + b + c]["Division"].ToString(), Dt.Rows[i + a + b + c]["Name"].ToString(), Dt.Rows[i + a + b + c]["FeederName"].ToString())));
                                                trd4.IsBackground = true;
                                                trd4.Start();
                                                d = 1;
                                            }
                                            k = a + b + c + d;

                                            if (i + k >= (Dt.Rows.Count))
                                            {
                                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                                {
                                                    if ((Status1 == "Feeder Completed") && (Status2 == "Feeder Completed") && (Status3 == "Feeder Completed") && (Status4 == "Feeder Completed"))
                                                    {
                                                        break;
                                                    }
                                                    Thread.Sleep(1000);
                                                }
                                            }
                                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                            {
                                                if (i + k >= (Dt.Rows.Count))
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1000);
                                            }
                                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID1);
                                                IsRunningProcess1 = false;

                                            }
                                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID2);
                                                IsRunningProcess2 = false;

                                            }
                                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID3);
                                                IsRunningProcess3 = false;

                                            }
                                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                                            {
                                                aa.combineErrorLog(Logpath, GETFILE, FeederID4);
                                                IsRunningProcess4 = false;

                                            }
                                            if (k == 0)
                                            {
                                                k = 1;
                                            }
                                        }

                                    }
                                }


                            }

                            aa.Total_ImportFeeder(Logpath, Convert.ToInt32(SelectedFeederCount), StartTime, EndTime);



                            Thread.Sleep(2000);

                            TechgrationProcess tp = new TechgrationProcess();
                            tp.setValue();
                            //TechgrationProcess tp = new TechgrationProcess();
                            //tp.setValue();
                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                        }
                    }

                }
                  
                else if (GISimpbox.Checked == false)   //PROFILE DATA.....
                {
                    this.Close();
                    if (checkmeter.Checked == true && checkpeak.Checked == true)
                    {
                        if (checkmeter.Checked == true)
                        {
                            th = new Thread(opennewform5);
                            th.SetApartmentState(ApartmentState.MTA);
                            th.Start();
                            totalpersentage = 200;
                            LoadDataConfig();

                            profiledata(GETFILE, ref lblStatusP);
                            Billingdata(GETFILE, ref lblStatusP);
                            Thread.Sleep(2000);
                            TechgrationProcess tp = new TechgrationProcess();
                            tp.setValue();
                        }
                         
                    }
                    if (checkmeter.Checked == true)
                    {
                        th = new Thread(opennewform5);
                        th.SetApartmentState(ApartmentState.MTA);
                        th.Start();
                        totalpersentage = 100;
                        LoadDataConfig();

                        profiledata(GETFILE, ref lblStatusP);
                        Thread.Sleep(2000);
                        TechgrationProcess tp = new TechgrationProcess();
                        tp.setValue();
                    }
                    if (checkpeak.Checked == true) /// demand.....
                    {
                        th = new Thread(opennewform5);
                        th.SetApartmentState(ApartmentState.MTA);
                        th.Start();
                        totalpersentage = 100;
                        LoadDataConfig();

                        Billingdata(GETFILE, ref lblStatusP);
                        Thread.Sleep(2000);
                        TechgrationProcess tp = new TechgrationProcess();
                        tp.setValue();
                    }
                }
                 
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }


        }

        private void opennewform5()
        {
            Application.Run(new MeterProfileProcess());
        }

        private void DeleteOldText(string path)
        {
            string folder = path + "\\TempChanges";
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            string folder1 = path + "\\CymeTextFile";

            string[] file2 = System.IO.Directory.GetFiles(folder1, "*.txt");                           // Old file Delete 
            foreach (string item1 in file2)
            {
                System.IO.File.Delete(item1);
            }
            System.IO.Directory.CreateDirectory(folder);
        }

        bool IsRunningProcess1 = false;
        bool IsRunningProcess2 = false;
        bool IsRunningProcess3 = false;
        bool IsRunningProcess4 = false;

        private void RunProcessFeeder1(string usertype, string NetworkID, string Division, string Name, string FeederName)
        {
            try
            {
                string[] FeederId1 = NetworkID.Split('_');
                string FeederId = FeederId1[0].ToString();

                IsRunningProcess1 = true;
                Status1 = "Reading GIS Data";
                FeederID1 = FeederId;
                Division1 = Division;
                Substation1 = Name;
                FeederName1 = FeederName;

                incrementFeeder = 2;
                completepersentag += incrementFeeder;
                string NEWGETFILE = GETFILE + "\\" + usertype;

                ErrorLog Error = new ErrorLog();
                string Fid11 = FeederID1.Replace("/", "-");
                string LogPath = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog\\FeederErrorlog.log";
                string LogPath1 = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }

                DeleteOldText(NEWGETFILE);
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath1);
                }
                Error.nav(LogPath, FeederId);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];

                Cursor.Current = Cursors.WaitCursor;

                bool mdbdelete = false;

                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DeleteOldTable(conn);
                CreateTechTable(conn);
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Create_GIS_Table table = new Create_GIS_Table();
                table.Create_GisTable(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn, Status1);
                
                incrementFeeder = 1;
                completepersentag += incrementFeeder;

                Insert_TG_Data tgData = new Insert_TG_Data();
                tgData.Insert_TGDATA(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                GetAllDeviceData vv = new GetAllDeviceData();
                vv.GetTGLINE_UNDERGROUNDLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGLINE_OVERHEADLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGUDD_DEVICEUDD(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_FUSEData(GETFILE,NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SHUNTCAPACITORData(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                vv.GetTGDEVICE_SWITCHData(GETFILE,NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_RECLOSER(GETFILE,NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_DISTRIBUTIONTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
               // vv.GetTGDEVICE_POWERTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_CIRCUITBREAKERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SOLAR(NEWGETFILE, cf, FeederId, conn);
                //vv.GetTGDEVICE_BATTERYSTORAG(NEWGETFILE, cf, FeederId, conn);
                //vv.GetTGDEVICE_WINDMILL(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 4;
                completepersentag += incrementFeeder;
                UpdatedeviceLocation vv1 = new UpdatedeviceLocation();
                vv1.UpdateLoaction(conn);
               // vv1.UpdateDeaviceConnection(conn);
                incrementFeeder = 1;
                completepersentag += incrementFeeder;            //30

                string get = "1";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (get == "1")
                {
                    Status1 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;

                Error.OvereHead(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.dt(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                //Error.ShuntCapacitor(LogPath, conn, cf);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}

                Error.FUSED(LogPath, conn, cf,FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Error.Switched(LogPath, conn, cf,FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.Recloser(LogPath, conn, cf,FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                 
                incrementFeeder = 3;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;    

                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);

                updateFromToNode up = new updateFromToNode();

                PTRLoad ptrload = new PTRLoad();
                ptrload.PTRSecondaryNodeLoad(NEWGETFILE);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;             //50

                ptrload.setSpotLoad(conn, cf, NEWGETFILE,FeederId);
                ptrload.CreateSpotLoad(conn, cf, NEWGETFILE,FeederId);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;  

                MeterDemand met = new MeterDemand();
                met.meter( cf, NEWGETFILE, FeederId);
                //met.Setmeter(cf, NEWGETFILE, FeederId, conn, GETFILE);

                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                //MeterDemand met = new MeterDemand();
                //met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string Fid1 = FeederID1.Replace("/", "-");
                string dir = GETFILE + "\\CYMEUPLOAD\\" + Fid1;
                incrementFeeder = 2;
                completepersentag += incrementFeeder;   //65
                if (!Directory.Exists(dir))
                {

                    Directory.CreateDirectory(dir);

                }

                string dir1 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                string dir2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CymeTextFile";
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                }
                if (!Directory.Exists(dir2))
                {
                    Directory.CreateDirectory(dir2);
                }

                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string str_Path3 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ini";
                string str_Path4 = NEWGETFILE + "\\CymeTextFile\\Load.txt";
                string str_Path5 = NEWGETFILE + "\\CymeTextFile\\MeterDemand.txt";
                string str_Path6 = NEWGETFILE + "\\CymeTextFile\\Network.txt";
                string str_Path8 = NEWGETFILE + "\\CymeTextFile\\NetworkList.txt";

                File.Copy(str_Path2, dir1 + "/" + Path.GetFileName(str_Path2));
                File.Copy(str_Path3, dir1 + "/" + Path.GetFileName(str_Path3));
                File.Copy(str_Path4, dir2 + "/" + Path.GetFileName(str_Path4));
                File.Copy(str_Path5, dir2 + "/" + Path.GetFileName(str_Path5));
                File.Copy(str_Path6, dir2 + "/" + Path.GetFileName(str_Path6));
                File.Copy(str_Path8, dir2 + "/" + Path.GetFileName(str_Path8));

                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path3))
                {
                    str_Path3 = GETFILE + @"\CYMEUPLOAD\" + Fid1 + "\\CYMEimpFile\\IMPORT.ini";
                    string text = File.ReadAllText(str_Path3);
                    text = text.Replace("Feeder1", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path3, text);
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path2))
                {
                    str_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";
                    string text = File.ReadAllText(str_Path2);
                    text = text.Replace("Feeder1", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path2, text);
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;               //75

                if (get == "1")
                {
                    Status1 = "Upload Into Cyme Database";
                }

                string mystr_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";

                string Net2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                CymeFileCreate feederlist = new CymeFileCreate();
                feederlist.import(mystr_Path2, Net2, GETFILE);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                 
                if (cf.Extractiontype == "incScheduler")
                {
                    UPDATETRACKEQUIPMENTUPDATES(LogPath, GETFILE, FeederId);
                }
                
                incrementFeeder = 13;
                completepersentag += incrementFeeder;
                Status1 = "Feeder Completed";

                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();

                Batch Batch = new Batch();
                Batch.Main(LogPath, GETFILE, FeederId); 
                incrementFeeder = 2;
                completepersentag += incrementFeeder;           //100
                Thread.Sleep(1000);
                Error.End(LogPath, ref EndTime);
                IsRunningProcess1 = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Errror:"+ex);
            }
        }

        private void RunProcessFeeder2(string usertype, string NetworkID, string Division, string Name, string FeederName)
        {
            try
            {
                string[] FeederId2 = NetworkID.Split('_');
                string FeederId = FeederId2[0].ToString();

                IsRunningProcess2 = true;
                Status2 = "Reading GIS Data";
                FeederID2 = FeederId;
                Division2 = Division;
                Substation2 = Name;
                FeederName2 = FeederName;

                incrementFeeder = 2;
                completepersentag += incrementFeeder;
                string NEWGETFILE = GETFILE + "\\" + usertype;

                ErrorLog Error = new ErrorLog();
                string Fid11 = FeederID2.Replace("/", "-");
                string LogPath = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog\\FeederErrorlog.log";
                string LogPath1 = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }

                DeleteOldText(NEWGETFILE);
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath1);
                }
                Error.nav(LogPath, FeederId);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];

                Cursor.Current = Cursors.WaitCursor;

                bool mdbdelete = false;

                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DeleteOldTable(conn);
                CreateTechTable(conn);
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Create_GIS_Table table = new Create_GIS_Table();
                table.Create_GisTable(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn, Status2);

                incrementFeeder = 1;
                completepersentag += incrementFeeder;

                Insert_TG_Data tgData = new Insert_TG_Data();
                tgData.Insert_TGDATA(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                GetAllDeviceData vv = new GetAllDeviceData();
                vv.GetTGLINE_UNDERGROUNDLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGLINE_OVERHEADLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGUDD_DEVICEUDD(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_FUSEData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SHUNTCAPACITORData(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                vv.GetTGDEVICE_SWITCHData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_RECLOSER(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_DISTRIBUTIONTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                //vv.GetTGDEVICE_POWERTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_CIRCUITBREAKERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SOLAR(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 4;
                completepersentag += incrementFeeder;
                UpdatedeviceLocation vv1 = new UpdatedeviceLocation();
                vv1.UpdateLoaction(conn);
                 
                incrementFeeder = 1;
                completepersentag += incrementFeeder;            //30

                string get = "1";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (get == "1")
                {
                    Status2 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;

                Error.OvereHead(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.dt(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                //Error.ShuntCapacitor(LogPath, conn, cf);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}

                Error.FUSED(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Error.Switched(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.Recloser(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                incrementFeeder = 3;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);

                updateFromToNode up = new updateFromToNode();

                PTRLoad ptrload = new PTRLoad();
                ptrload.PTRSecondaryNodeLoad(NEWGETFILE);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;             //50

                ptrload.setSpotLoad(conn, cf, NEWGETFILE, FeederId);
                ptrload.CreateSpotLoad(conn, cf, NEWGETFILE, FeederId);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                MeterDemand met = new MeterDemand();
                met.meter(cf, NEWGETFILE, FeederId);
                //met.Setmeter(cf, NEWGETFILE, FeederId, conn, GETFILE);

                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                //MeterDemand met = new MeterDemand();
                //met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string Fid1 = FeederID2.Replace("/", "-");
                string dir = GETFILE + "\\CYMEUPLOAD\\" + Fid1;
                incrementFeeder = 2;
                completepersentag += incrementFeeder;   //65
                if (!Directory.Exists(dir))
                {

                    Directory.CreateDirectory(dir);

                }

                string dir1 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                string dir2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CymeTextFile";
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                }
                if (!Directory.Exists(dir2))
                {
                    Directory.CreateDirectory(dir2);
                }

                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string str_Path3 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ini";
                string str_Path4 = NEWGETFILE + "\\CymeTextFile\\Load.txt";
                string str_Path5 = NEWGETFILE + "\\CymeTextFile\\MeterDemand.txt";
                string str_Path6 = NEWGETFILE + "\\CymeTextFile\\Network.txt";
                string str_Path8 = NEWGETFILE + "\\CymeTextFile\\NetworkList.txt";

                File.Copy(str_Path2, dir1 + "/" + Path.GetFileName(str_Path2));
                File.Copy(str_Path3, dir1 + "/" + Path.GetFileName(str_Path3));
                File.Copy(str_Path4, dir2 + "/" + Path.GetFileName(str_Path4));
                File.Copy(str_Path5, dir2 + "/" + Path.GetFileName(str_Path5));
                File.Copy(str_Path6, dir2 + "/" + Path.GetFileName(str_Path6));
                File.Copy(str_Path8, dir2 + "/" + Path.GetFileName(str_Path8));

                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path3))
                {
                    str_Path3 = GETFILE + @"\CYMEUPLOAD\" + Fid1 + "\\CYMEimpFile\\IMPORT.ini";
                    string text = File.ReadAllText(str_Path3);
                    text = text.Replace("Feeder2", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path3, text);
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path2))
                {
                    str_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";
                    string text = File.ReadAllText(str_Path2);
                    text = text.Replace("Feeder2", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path2, text);
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;               //75

                if (get == "1")
                {
                    Status2 = "Upload Into Cyme Database";
                }

                string mystr_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";

                string Net2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                CymeFileCreate feederlist = new CymeFileCreate();
                feederlist.import(mystr_Path2, Net2, GETFILE);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                if (cf.Extractiontype == "incScheduler")
                {
                    UPDATETRACKEQUIPMENTUPDATES(LogPath, GETFILE, FeederId);
                }

                incrementFeeder = 13;
                completepersentag += incrementFeeder;
                Status2 = "Feeder Completed";

                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();

                Batch Batch = new Batch();
                Batch.Main(LogPath, GETFILE, FeederId);
                incrementFeeder = 2;
                completepersentag += incrementFeeder;           //100
                Thread.Sleep(1000);
                Error.End(LogPath, ref EndTime);
                IsRunningProcess2 = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Errror:"+ex);
            }
        }

        private void RunProcessFeeder3(string usertype, string NetworkID, string Division, string Name, string FeederName)
        {
            try
            {
                string[] FeederId3 = NetworkID.Split('_');
                string FeederId = FeederId3[0].ToString();

                IsRunningProcess3 = true;
                Status3 = "Reading GIS Data";
                FeederID3 = FeederId;
                Division3 = Division;
                Substation3 = Name;
                FeederName3 = FeederName;

                incrementFeeder = 2;
                completepersentag += incrementFeeder;
                string NEWGETFILE = GETFILE + "\\" + usertype;

                ErrorLog Error = new ErrorLog();
                string Fid11 = FeederID3.Replace("/", "-");
                string LogPath = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog\\FeederErrorlog.log";
                string LogPath1 = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }

                DeleteOldText(NEWGETFILE);
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath1);
                }
                Error.nav(LogPath, FeederId);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];

                Cursor.Current = Cursors.WaitCursor;

                bool mdbdelete = false;

                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DeleteOldTable(conn);
                CreateTechTable(conn);
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Create_GIS_Table table = new Create_GIS_Table();
                table.Create_GisTable(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn, Status3);

                incrementFeeder = 1;
                completepersentag += incrementFeeder;

                Insert_TG_Data tgData = new Insert_TG_Data();
                tgData.Insert_TGDATA(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                GetAllDeviceData vv = new GetAllDeviceData();
                vv.GetTGLINE_UNDERGROUNDLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGLINE_OVERHEADLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGUDD_DEVICEUDD(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_FUSEData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SHUNTCAPACITORData(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                vv.GetTGDEVICE_SWITCHData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_RECLOSER(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_DISTRIBUTIONTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
               // vv.GetTGDEVICE_POWERTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_CIRCUITBREAKERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SOLAR(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 4;
                completepersentag += incrementFeeder;
                UpdatedeviceLocation vv1 = new UpdatedeviceLocation();
                vv1.UpdateLoaction(conn);
                 
                incrementFeeder = 1;
                completepersentag += incrementFeeder;            //30

                string get = "1";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (get == "1")
                {
                    Status3 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;

                Error.OvereHead(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.dt(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                //Error.ShuntCapacitor(LogPath, conn, cf);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}

                Error.FUSED(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Error.Switched(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.Recloser(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                incrementFeeder = 3;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);

                updateFromToNode up = new updateFromToNode();

                PTRLoad ptrload = new PTRLoad();
                ptrload.PTRSecondaryNodeLoad(NEWGETFILE);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;             //50

                ptrload.setSpotLoad(conn, cf, NEWGETFILE, FeederId);
                ptrload.CreateSpotLoad(conn, cf, NEWGETFILE, FeederId);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                MeterDemand met = new MeterDemand();
                met.meter(cf, NEWGETFILE, FeederId);
                //met.Setmeter(cf, NEWGETFILE, FeederId, conn, GETFILE);

                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                //MeterDemand met = new MeterDemand();
                //met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string Fid1 = FeederID3.Replace("/", "-");
                string dir = GETFILE + "\\CYMEUPLOAD\\" + Fid1;
                incrementFeeder = 2;
                completepersentag += incrementFeeder;   //65
                if (!Directory.Exists(dir))
                {

                    Directory.CreateDirectory(dir);

                }

                string dir1 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                string dir2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CymeTextFile";
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                }
                if (!Directory.Exists(dir2))
                {
                    Directory.CreateDirectory(dir2);
                }

                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string str_Path3 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ini";
                string str_Path4 = NEWGETFILE + "\\CymeTextFile\\Load.txt";
                string str_Path5 = NEWGETFILE + "\\CymeTextFile\\MeterDemand.txt";
                string str_Path6 = NEWGETFILE + "\\CymeTextFile\\Network.txt";
                string str_Path8 = NEWGETFILE + "\\CymeTextFile\\NetworkList.txt";

                File.Copy(str_Path2, dir1 + "/" + Path.GetFileName(str_Path2));
                File.Copy(str_Path3, dir1 + "/" + Path.GetFileName(str_Path3));
                File.Copy(str_Path4, dir2 + "/" + Path.GetFileName(str_Path4));
                File.Copy(str_Path5, dir2 + "/" + Path.GetFileName(str_Path5));
                File.Copy(str_Path6, dir2 + "/" + Path.GetFileName(str_Path6));
                File.Copy(str_Path8, dir2 + "/" + Path.GetFileName(str_Path8));

                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path3))
                {
                    str_Path3 = GETFILE + @"\CYMEUPLOAD\" + Fid1 + "\\CYMEimpFile\\IMPORT.ini";
                    string text = File.ReadAllText(str_Path3);
                    text = text.Replace("Feeder3", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path3, text);
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path2))
                {
                    str_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";
                    string text = File.ReadAllText(str_Path2);
                    text = text.Replace("Feeder3", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path2, text);
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;               //75

                if (get == "1")
                {
                    Status3 = "Upload Into Cyme Database";
                }

                string mystr_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";

                string Net2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                CymeFileCreate feederlist = new CymeFileCreate();
                feederlist.import(mystr_Path2, Net2, GETFILE);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                if (cf.Extractiontype == "incScheduler")
                {
                    UPDATETRACKEQUIPMENTUPDATES(LogPath, GETFILE, FeederId);
                }

                incrementFeeder = 13;
                completepersentag += incrementFeeder;
                Status3 = "Feeder Completed";

                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();

                Batch Batch = new Batch();
                Batch.Main(LogPath, GETFILE, FeederId);
                incrementFeeder = 2;
                completepersentag += incrementFeeder;           //100
                Thread.Sleep(1000);
                Error.End(LogPath, ref EndTime);
                IsRunningProcess3 = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Errror:"+ex);
            }
        }

        private void RunProcessFeeder4(string usertype, string NetworkID, string Division, string Name, string FeederName)
        {
            try
            {
                string[] FeederId4 = NetworkID.Split('_');
                string FeederId = FeederId4[0].ToString();

                IsRunningProcess4 = true;
                Status4 = "Reading GIS Data";
                FeederID4 = FeederId;
                Division4 = Division;
                Substation4 = Name;
                FeederName4 = FeederName;

                incrementFeeder = 2;
                completepersentag += incrementFeeder;
                string NEWGETFILE = GETFILE + "\\" + usertype;

                ErrorLog Error = new ErrorLog();
                string Fid11 = FeederID4.Replace("/", "-");
                string LogPath = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog\\FeederErrorlog.log";
                string LogPath1 = GETFILE + "\\CYMEUPLOAD\\" + Fid11 + "\\ErrorLog";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }

                DeleteOldText(NEWGETFILE);
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath1);
                }
                Error.nav(LogPath, FeederId);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];

                Cursor.Current = Cursors.WaitCursor;

                bool mdbdelete = false;

                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DeleteOldTable(conn);
                CreateTechTable(conn);
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Create_GIS_Table table = new Create_GIS_Table();
                table.Create_GisTable(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn, Status4);

                incrementFeeder = 1;
                completepersentag += incrementFeeder;

                Insert_TG_Data tgData = new Insert_TG_Data();
                tgData.Insert_TGDATA(NEWGETFILE, cf, FeederId, GETFILE, usertype, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                GetAllDeviceData vv = new GetAllDeviceData();
                vv.GetTGLINE_UNDERGROUNDLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGLINE_OVERHEADLINEData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGUDD_DEVICEUDD(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_FUSEData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SHUNTCAPACITORData(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                vv.GetTGDEVICE_SWITCHData(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_RECLOSER(GETFILE, NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_DISTRIBUTIONTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                //vv.GetTGDEVICE_POWERTRANSFORMERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_CIRCUITBREAKERData(NEWGETFILE, cf, FeederId, conn);
                vv.GetTGDEVICE_SOLAR(NEWGETFILE, cf, FeederId, conn);
                incrementFeeder = 4;
                completepersentag += incrementFeeder;
                UpdatedeviceLocation vv1 = new UpdatedeviceLocation();
                vv1.UpdateLoaction(conn);
                
                incrementFeeder = 1;
                completepersentag += incrementFeeder;            //30

                string get = "1";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (get == "1")
                {
                    Status4 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;

                Error.OvereHead(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.dt(LogPath, conn, cf);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                //Error.ShuntCapacitor(LogPath, conn, cf);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}

                Error.FUSED(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                Error.Switched(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Error.Recloser(LogPath, conn, cf, FeederId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                incrementFeeder = 3;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);

                incrementFeeder = 4;
                completepersentag += incrementFeeder;

                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);

                updateFromToNode up = new updateFromToNode();

                PTRLoad ptrload = new PTRLoad();
                ptrload.PTRSecondaryNodeLoad(NEWGETFILE);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;             //50

                ptrload.setSpotLoad(conn, cf, NEWGETFILE, FeederId);
                ptrload.CreateSpotLoad(conn, cf, NEWGETFILE, FeederId);

                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                MeterDemand met = new MeterDemand();
                met.meter(cf, NEWGETFILE, FeederId);
                //met.Setmeter(cf, NEWGETFILE, FeederId, conn, GETFILE);

                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                //MeterDemand met = new MeterDemand();
                //met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string Fid1 = FeederID4.Replace("/", "-");
                string dir = GETFILE + "\\CYMEUPLOAD\\" + Fid1;
                incrementFeeder = 2;
                completepersentag += incrementFeeder;   //65
                if (!Directory.Exists(dir))
                {

                    Directory.CreateDirectory(dir);

                }

                string dir1 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                string dir2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CymeTextFile";
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                }
                if (!Directory.Exists(dir2))
                {
                    Directory.CreateDirectory(dir2);
                }

                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string str_Path3 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ini";
                string str_Path4 = NEWGETFILE + "\\CymeTextFile\\Load.txt";
                string str_Path5 = NEWGETFILE + "\\CymeTextFile\\MeterDemand.txt";
                string str_Path6 = NEWGETFILE + "\\CymeTextFile\\Network.txt";
                string str_Path8 = NEWGETFILE + "\\CymeTextFile\\NetworkList.txt";

                File.Copy(str_Path2, dir1 + "/" + Path.GetFileName(str_Path2));
                File.Copy(str_Path3, dir1 + "/" + Path.GetFileName(str_Path3));
                File.Copy(str_Path4, dir2 + "/" + Path.GetFileName(str_Path4));
                File.Copy(str_Path5, dir2 + "/" + Path.GetFileName(str_Path5));
                File.Copy(str_Path6, dir2 + "/" + Path.GetFileName(str_Path6));
                File.Copy(str_Path8, dir2 + "/" + Path.GetFileName(str_Path8));

                incrementFeeder = 5;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path3))
                {
                    str_Path3 = GETFILE + @"\CYMEUPLOAD\" + Fid1 + "\\CYMEimpFile\\IMPORT.ini";
                    string text = File.ReadAllText(str_Path3);
                    text = text.Replace("Feeder4", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path3, text);
                }
                incrementFeeder = 3;
                completepersentag += incrementFeeder;

                if (File.Exists(str_Path2))
                {
                    str_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";
                    string text = File.ReadAllText(str_Path2);
                    text = text.Replace("Feeder4", "CYMEUPLOAD\\" + Fid1);
                    File.WriteAllText(str_Path2, text);
                }
                incrementFeeder = 2;
                completepersentag += incrementFeeder;               //75

                if (get == "1")
                {
                    Status4 = "Upload Into Cyme Database";
                }

                string mystr_Path2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.bat";

                string Net2 = GETFILE + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile";
                CymeFileCreate feederlist = new CymeFileCreate();
                feederlist.import(mystr_Path2, Net2, GETFILE);

                incrementFeeder = 10;
                completepersentag += incrementFeeder;

                if (cf.Extractiontype == "incScheduler")
                {
                    UPDATETRACKEQUIPMENTUPDATES(LogPath, GETFILE, FeederId);
                }

                incrementFeeder = 13;
                completepersentag += incrementFeeder;
                Status4 = "Feeder Completed";

                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();

                Batch Batch = new Batch();
                Batch.Main(LogPath, GETFILE, FeederId);
                incrementFeeder = 2;
                completepersentag += incrementFeeder;           //100
                Thread.Sleep(1000);
                Error.End(LogPath, ref EndTime);
                IsRunningProcess4 = true;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Errror:"+ex);
            }
        }

        private void CYMEUPDATETABLE(string ServerName, string DbName, string password, string username, string Feederid)
        {
            try
            {
                string connectionString = @"Data Source=" + ServerName +                       //Create Connection string
                     ";database=" + DbName +
                     ";User ID=" + username +
                     ";Password=" + password;
                using (SqlConnection obj = new SqlConnection(connectionString))
                {

                    string insr = "Insert into CYMBACKUPTABLE (NetworkId) values ('" + Feederid + "')";
                    SqlCommand com = new SqlCommand(insr, obj);
                    obj.Open();
                    com.ExecuteNonQuery();
                    obj.Close();
                }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }

        }

        private void DeleteOldTable(OleDbConnection conn)
        {
            try
            {
                System.Data.DataTable dt = null;

                // Get the data table containing the schema
                string strSheetTableName = null;
                string TableType = null;
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dt.Rows)
                {
                    strSheetTableName = row["TABLE_NAME"].ToString();
                    TableType = row["TABLE_TYPE"].ToString();
                    if (TableType == "TABLE")
                    {
                        string feederlistdelete = "drop table [" + strSheetTableName + "]";
                        OleDbCommand cmm = new OleDbCommand(feederlistdelete, conn);

                        cmm.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }
        private void CreateTechTable(OleDbConnection con)
        {
            try
            {


                string TG_CUSTOMERLOADS = "create table TG_CUSTOMERLOADS (SECTIONID Varchar(250),DeviceNumber Varchar(250),LoadType Varchar(250),CustomerNumber Varchar(250),CustomerType Varchar(250),ConnectionStatus Varchar(250),LockDuringLoadAllocation Varchar(250),Years Varchar(250),LoadModelID Varchar(250),NormalPriority Varchar(250),EmergencyPriority Varchar(250),ValueType Varchar(250),LoadPhase Varchar(250),Value1 Varchar(250),Value2 Varchar(250),ConnectedKVA Varchar(250),KWH Varchar(250),NumberOfCustomer Varchar(250),CenterTapPercent Varchar(250),CenterTapPercent2 Varchar(250),LoadValue1N1 Varchar(250),LoadValue1N2 Varchar(250),LoadValue2N1 Varchar(250),LoadValue2N2 Varchar(250))";
                OleDbCommand comeq = new OleDbCommand(TG_CUSTOMERLOADS, con);
                comeq.ExecuteNonQuery();

                string TGDEVICE_CIRCUITBREAKER = "create table TGDEVICE_CIRCUITBREAKER (SectionID Varchar(250),Location Varchar(250),EqID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),CoordX Varchar(250),CoordY Varchar(250),ClosedPhase Varchar(250),Locked Varchar(250),RC Varchar(250),NStatus Varchar(250),TCCID Varchar(250),PhPickup Varchar(250),GrdPickup Varchar(250),Alternate Varchar(250),PhAltPickup Varchar(250),GrdAltPickup Varchar(250),FromNodeID Varchar(250),EnableReclosing Varchar(250),FaultIndicator Varchar(250),EnableFuseSaving Varchar(250),MinRatedCurrentForFuseSaving Varchar(250),Automated Varchar(250),SensorMode Varchar(250),Strategic Varchar(250),RestorationMode Varchar(250),ConnectionStatus Varchar(250),ByPassOnRestoration Varchar(250),Speed Varchar(250),SeqOpFirstPhase Varchar(250),SeqOpFirstGround Varchar(250),SeqOpLockoutPhase Varchar(250),SeqOpLockoutGround Varchar(250),SeqResetTime Varchar(250),SeqReclosingTime1 Varchar(250),SeqReclosingTime2 Varchar(250),SeqReclosingTime3 Varchar(250),Reversible Varchar(250))";
                OleDbCommand com116 = new OleDbCommand(TGDEVICE_CIRCUITBREAKER, con);
                com116.ExecuteNonQuery();

                string TGDEVICE_DISTRIBUTIONTRANSFORMER = "create table [TGDEVICE_DISTRIBUTIONTRANSFORMER] (SectionID Varchar(250),Location Varchar(250),EqID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),CoordX Varchar(250),CoordY Varchar(250),Conn Varchar(250),PrimTap Varchar(250),SecondaryTap Varchar(250),RgPrim Varchar(250),XgPrim Varchar(250),RgSec Varchar(250),XgSec Varchar(250),ODPrimPh Varchar(250),PrimaryBaseVoltage Varchar(250),SecondaryBaseVoltage Varchar(250),FromNodeID Varchar(250),SettingOption Varchar(250),SetPoint Varchar(250),ControlType Varchar(250),LowerBandWidth Varchar(250),UpperBandWidth Varchar(250),TapLocation Varchar(250),InitialTapPosition Varchar(250),InitialTapPositionMode Varchar(250),Tap Varchar(250),MaxBuck Varchar(250),MaxBoost Varchar(250),CT Varchar(250),PT Varchar(250),Rset Varchar(250),Xset Varchar(250),FirstHouseHigh Varchar(250),FirstHouseLow Varchar(250),PhaseON Varchar(250),AtSectionID Varchar(250),MasterID Varchar(250),FaultIndicator Varchar(250),PhaseShiftType Varchar(250),GammaPhaseShift Varchar(250),CTPhase Varchar(250),PrimaryCornerGroundedPhase Varchar(250),SecondaryCornerGroundedPhase Varchar(250),ConnectionStatus Varchar(250),Reversible Varchar(250))";
                OleDbCommand com1161 = new OleDbCommand(TGDEVICE_DISTRIBUTIONTRANSFORMER, con);
                com1161.ExecuteNonQuery();

                string TGDEVICE_FUSE = "create table [TGDEVICE_FUSE] (SectionID Varchar(250),Location Varchar(250),EqID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),CoordX Varchar(250),CoordY Varchar(250),ClosedPhase Varchar(250),Locked Varchar(250),RC Varchar(250),NStatus Varchar(250),TCCID Varchar(250),PhPickup Varchar(250),GrdPickup Varchar(250),Alternate Varchar(250),PhAltPickup Varchar(250),GrdAltPickup Varchar(250),FromNodeID Varchar(250),FaultIndicator Varchar(250),Strategic Varchar(250),RestorationMode Varchar(250),ConnectionStatus Varchar(250),ByPassOnRestoration Varchar(250),Reversible Varchar(250))";
                OleDbCommand coms = new OleDbCommand(TGDEVICE_FUSE, con);
                coms.ExecuteNonQuery();

                string TGUDD_DEVICEUDD = "create table [TGUDD_DEVICEUDD] (DeviceNumber Varchar(250),DeviceType int,DataId Varchar(250),DataType int,DataValue Varchar(250))";
                OleDbCommand comsS = new OleDbCommand(TGUDD_DEVICEUDD, con);
                comsS.ExecuteNonQuery();


                string TGDEVICE_SWITCH = "create table [TGDEVICE_SWITCH] (SectionID Varchar(250),Location Varchar(250),EqID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),CoordX Varchar(250),CoordY Varchar(250),ClosedPhase Varchar(250),Locked Varchar(250),RC Varchar(250),NStatus Varchar(250),PhPickup Varchar(250),GrdPickup Varchar(250),Alternate Varchar(250),PhAltPickup Varchar(250),GrdAltPickup Varchar(250),FromNodeID Varchar(250),FaultIndicator Varchar(250),Automated Varchar(250),SensorMode Varchar(250),Strategic Varchar(250),RestorationMode Varchar(250),ConnectionStatus Varchar(250),ByPassOnRestoration Varchar(250),Reversible Varchar(250))";
                OleDbCommand coms1 = new OleDbCommand(TGDEVICE_SWITCH, con);
                coms1.ExecuteNonQuery();


                string TGDEVICE_RECLOSER = "CREATE TABLE TGDEVICE_RECLOSER (SectionID VARCHAR(255), Location VARCHAR(255), EqID VARCHAR(255), DeviceNumber VARCHAR(255), DeviceStage VARCHAR(255), Flags VARCHAR(255), InitFromEquipFlags VARCHAR(255), CoordX VARCHAR(255), CoordY VARCHAR(255), ClosedPhase VARCHAR(255), Locked VARCHAR(255), RC VARCHAR(255), NStatus VARCHAR(255), TCCID VARCHAR(255), PhPickup VARCHAR(255), GrdPickup VARCHAR(255), Alternate VARCHAR(255), PhAltPickup VARCHAR(255), GrdAltPickup VARCHAR(255), FromNodeID VARCHAR(255), EnableReclosing VARCHAR(255), FaultIndicator VARCHAR(255), EnableFuseSaving VARCHAR(255), MinRatedCurrentForFuseSaving VARCHAR(255), Automated VARCHAR(255), SensorMode VARCHAR(255), Strategic VARCHAR(255), RestorationMode VARCHAR(255), ConnectionStatus VARCHAR(255), TCCRepositoryID VARCHAR(255), TCCRepositoryAlternateID1 VARCHAR(255), TCCRepositoryAlternateID2 VARCHAR(255), TCCRepositoryAlternateID3 VARCHAR(255), TCCRepositoryAlternateID4 VARCHAR(255), TCCRepositoryAlternateID5 VARCHAR(255), TCCRepositoryAlternateID6 VARCHAR(255), TCCRepositoryAlternateID7 VARCHAR(255), TCCRepositoryAlternateID8 VARCHAR(255), TCCRepositoryAlternateID9 VARCHAR(255), TCCRepositoryAlternateID10 VARCHAR(255), IntellirupterTCCRepositoryID VARCHAR(255), ByPassOnRestoration VARCHAR(255), Reversible VARCHAR(255), TCCSettingsSelection VARCHAR(255));";
                OleDbCommand coms11 = new OleDbCommand(TGDEVICE_RECLOSER, con);
                coms11.ExecuteNonQuery();

                string TGLINE_OVERHEADLINE = "create table [TGLINE_OVERHEADLINE] (SectionID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),LineCableID Varchar(250),Length Varchar(250),ConnectionStatus Varchar(250),CoordX Varchar(250),CoordY Varchar(250),HarmonicModel Varchar(250),FlowConstraintActive Varchar(250),FlowConstraintUnit Varchar(250),MaximumFlow Varchar(250),SeriesCompensationActive Varchar(250),MaxReactanceMultiplier Varchar(250),SeriesCompensationCost Varchar(250))";
                OleDbCommand comm = new OleDbCommand(TGLINE_OVERHEADLINE, con);
                comm.ExecuteNonQuery();

                string TGLINE_UNDERGROUNDLINE = "create table [TGLINE_UNDERGROUNDLINE] (SectionID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),LineCableID Varchar(250),Length Varchar(250),NumberOfCableInParallel Varchar(250),Amps Varchar(250),Amps_1 Varchar(250),Amps_2 Varchar(250),Amps_3 Varchar(250),Amps_4 Varchar(250),ConnectionStatus Varchar(250),CoordX Varchar(250),CoordY Varchar(250),HarmonicModel Varchar(250),EarthResistivity Varchar(250),OperatingTemperature Varchar(250),Height Varchar(250),DistanceBetweenConductors Varchar(250),BondingType Varchar(250),CableConfiguration Varchar(250),DuctMaterial Varchar(250),Bundled Varchar(250),Neutral1Type Varchar(250),Neutral2Type Varchar(250),Neutral3Type Varchar(250),Neutral1ID Varchar(250),Neutral2ID Varchar(250),Neutral3ID Varchar(250),AmpacityDeratingFactor Varchar(250),FlowConstraintActive Varchar(250),FlowConstraintUnit Varchar(250),MaximumFlow Varchar(250))";
                OleDbCommand comm1 = new OleDbCommand(TGLINE_UNDERGROUNDLINE, con);
                comm1.ExecuteNonQuery();

                string TGLOAD_LOADS = "create table TGLOAD_LOADS (SECTIONID Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),LoadType Varchar(250),Connection_ Varchar(250),Location Varchar(250))";
                OleDbCommand com11 = new OleDbCommand(TGLOAD_LOADS, con);
                com11.ExecuteNonQuery();

                string TGDEVICE_SOLAR = "create table [TGDEVICE_SOLAR] (SectionID Varchar(250),Location Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),	Flags Varchar(250),	InitFromEquipFlags Varchar(250),EquipmentID Varchar(250),NumberOfGenerators Varchar(250),SymbolSize Varchar(250),NS Varchar(250),NP Varchar(250),AmbientTemperature Varchar(250),FaultContributionBasedOnRatedPower Varchar(250),FaultContributionUnit Varchar(250),FaultContribution Varchar(250),ConstantInsolation Varchar(250),ForceT0 Varchar(250),InsolationModelID Varchar(250),FrequencySourceID Varchar(250),SourceHarmonicModelType Varchar(250),PulseNumber Varchar(250),FiringAngle Varchar(250),OverlapAngle Varchar(250),ConnectionStatus Varchar(250),ConnectionConfiguration Varchar(250),CTConnection Varchar(250),Phase Varchar(250))";
                OleDbCommand BB1 = new OleDbCommand(TGDEVICE_SOLAR, con);
                BB1.ExecuteNonQuery();


                string TGDEVICE_BATTERYSTORAGE = "create table [TGDEVICE_BATTERYSTORAG] (SECTIONID Varchar(250),Location Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),EquipmentID Varchar(250),Phase Varchar(250),ConnectionStatus Varchar(250),ConnectionConfiguration Varchar(250),CTConnection Varchar(250),SymbolSize Varchar(250),MaximumSOC Varchar(250),MinimumSOC Varchar(250),FaultContributionBasedOnRatedPower Varchar(250),FaultContributionUnit Varchar(250),FaultContribution Varchar(250),FrequencySourceID Varchar(250),SourceHarmonicModelType Varchar(250),PulseNumber Varchar(250),FiringAngle Varchar(250),OverlapAngle Varchar(250),InitialSOC Varchar(250),GridOutput Varchar(250),RiseFallUnit Varchar(250),PowerFallLimit Varchar(250),PowerRiseLimit Varchar(250),ChargeDelayUnit Varchar(250),ChargeDelay Varchar(250),DischargeDelayUnit Varchar(250),DischargeDelay Varchar(250),PythonDeviceScriptID Varchar(250))";
                OleDbCommand BB2 = new OleDbCommand(TGDEVICE_BATTERYSTORAGE, con);
                BB2.ExecuteNonQuery();

                string TGDEVICE_WINDMILL = "create table [TGDEVICE_WINDMILL] (SECTIONID Varchar(250),Location Varchar(250),DeviceNumber Varchar(250),DeviceStage Varchar(250),Flags Varchar(250),InitFromEquipFlags Varchar(250),EquipmentID Varchar(250),NumberOfGenerators Varchar(250),SymbolSize Varchar(250),ConstantWindSpeed Varchar(250),ForceT0 Varchar(250),WindModelID Varchar(250),FrequencySourceID Varchar(250),SourceHarmonicModelType Varchar(250),PulseNumber Varchar(250),FiringAngle Varchar(250),OverlapAngle Varchar(250),ConnectionStatus Varchar(250),ConnectionConfiguration Varchar(250),Phase Varchar(250),FaultContributionBasedOnRatedPower Varchar(250),FaultContributionUnit Varchar(250),FaultContribution Varchar(250),CTConnection Varchar(250))";
                OleDbCommand BB = new OleDbCommand(TGDEVICE_WINDMILL, con);
                BB.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }





        }
        private void LoadDataConfig()
        {
            string Configpath = GETFILE + "//ConfigFile//Configfile.xml";

            if (File.Exists(Configpath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Configpath);
                //ConfigFileData cf = new ConfigFileData();
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "GIS_Database")
                    {
                        cf.Gisservername = dt.Rows[0]["Server"].ToString();
                        cf.Gisusername = dt.Rows[0]["Username"].ToString();
                        cf.Gispassword = dt.Rows[0]["Password"].ToString();
                        cf.GisDatabase = dt.Rows[0]["DataBase_Name"].ToString();
                        cf.GisSchema_Name = dt.Rows[0]["Schema_Name"].ToString();

                        cf.SDEpath = dt.Rows[0]["SDEPath"].ToString();
                    }
                    else if (dt.TableName == "Equipment_Database")
                    {

                        cf.CEqpservername = dt.Rows[0]["Server"].ToString();
                        cf.CEqpdatabase = dt.Rows[0]["Database"].ToString();
                        cf.CEqpusername = dt.Rows[0]["Username"].ToString();
                        cf.CEqppassword = dt.Rows[0]["Password"].ToString();

                    }
                    else if (dt.TableName == "Network_Database")
                    {

                        cf.CNetservername = dt.Rows[0]["Net_Server"].ToString();
                        cf.CNetdatabase = dt.Rows[0]["Net_Database"].ToString();
                        cf.CNetusername = dt.Rows[0]["Net_Username"].ToString();
                        cf.CNetpassword = dt.Rows[0]["Net_Password"].ToString();

                    }
                    else if (dt.TableName == "Mdas")
                    {
                        cf.MDASservername = dt.Rows[0]["Mdas_Server"].ToString();
                        cf.MDASdatabase = dt.Rows[0]["Mdas_Database"].ToString();
                        cf.MDASusername = dt.Rows[0]["Mdas_Username"].ToString();
                        cf.MDASpassword = dt.Rows[0]["Mdas_Password"].ToString();
                        //cf.ptpatdh = dt.Rows[0]["PT_Path"].ToString();
                        //cf.dtpath = dt.Rows[0]["DT_Path"].ToString();
                    }
                    else if (dt.TableName == "TechGration")
                    {
                        cf.TGservername = dt.Rows[0]["TG_Server"].ToString();
                        cf.TGdatabase = dt.Rows[0]["TG_Database"].ToString();
                        cf.TGusername = dt.Rows[0]["TG_Username"].ToString();
                        cf.TGpassword = dt.Rows[0]["TG_Password"].ToString();
                    }
                    else if (dt.TableName == "Text_Record")
                    {
                        cf.xmlPath = dt.Rows[0]["XML_PATH"].ToString();
                        cf.DTCSV_PATH = dt.Rows[0]["DTCSV_PATH"].ToString();
                        cf.Meter_PATH = dt.Rows[0]["Meter_PATH"].ToString();
                        cf.FMEDirectory = dt.Rows[0]["FMECYMDIST"].ToString();
                        cf.CymeDirectory = dt.Rows[0]["CYMDIST"].ToString();
                        cf.Errorlog = dt.Rows[0]["Error_Log"].ToString();
                        cf.RES = dt.Rows[0]["Residential"].ToString();
                        cf.IND = dt.Rows[0]["Industrial"].ToString();
                    }
                    else if (dt.TableName == "CymDatabaseAccess")
                    {
                        cf.Cnetmdbpath = dt.Rows[0]["Network"].ToString();
                        cf.Ceqpmdbpath = dt.Rows[0]["Equipment"].ToString();

                    }
                    else if (dt.TableName == "CymDatabaseType")
                    {
                        cf.Cdatabasetype = dt.Rows[0]["Type"].ToString();
                    }
                    else if (dt.TableName == "ExtractionType")
                    {
                        cf.Extractiontype = dt.Rows[0]["Type"].ToString();

                    }
                    if (dt.TableName == "GIS_RECORD")
                    {
                        cf.tempkey = dt.Rows[0]["key"].ToString();
                    }
                    else if (dt.TableName == "TG_Database")
                    {
                        cf.TGservername = dt.Rows[0]["Server"].ToString();
                        cf.TGdatabase = dt.Rows[0]["DataBase_Name"].ToString();
                        cf.TGusername = dt.Rows[0]["Username"].ToString();
                        cf.TGpassword = dt.Rows[0]["Password"].ToString();
                    }
                    else if (dt.TableName == "Profile_Database")
                    {
                        cf.txtproServer = dt.Rows[0]["Profile_Server"].ToString();
                        cf.txtproDatabaseName = dt.Rows[0]["Profile_Databasename"].ToString();
                        cf.txtproUser = dt.Rows[0]["Profile_Username"].ToString();
                        cf.txtpropassword = dt.Rows[0]["Profile_Password"].ToString();
                    }
                    else if (dt.TableName == "Date_Time")
                    {
                        cf.Pfordate = dt.Rows[0]["ForDate"].ToString();
                        cf.ptodate = dt.Rows[0]["ToDate"].ToString();
                        cf.pfromdate = dt.Rows[0]["FromDate"].ToString();
                        cf.Bfordate = dt.Rows[0]["ForDate_Peak"].ToString();
                        cf.Btodate = dt.Rows[0]["ToDate_Peak"].ToString();
                        cf.Bfromdate = dt.Rows[0]["FromDate_Peak"].ToString();
                        cf.checkmeter = dt.Rows[0]["Meter_Demand"].ToString();
                        cf.checkpeak = dt.Rows[0]["Peak_Demand"].ToString();
                        cf.TYPE1 = dt.Rows[0]["TYPE1"].ToString();
                    }
                }
            }
        }

        private void updatedata(OleDbConnection selectConnection)
        {
            try
            {
                #region

                DataTable dtFinal = new DataTable();
                string selectQuery = "select * from FinalSection order by SectionId asc";

                OleDbDataAdapter selectDataAdapter = new OleDbDataAdapter(selectQuery, selectConnection);
                selectDataAdapter.Fill(dtFinal);

                selectQuery = string.Empty;
                selectQuery = "select * from TempSection_Breaked";
                DataTable dtBreaked = new DataTable();
                OleDbDataAdapter BreakedDataAdapter = new OleDbDataAdapter(selectQuery, selectConnection);
                BreakedDataAdapter.Fill(dtBreaked);

                selectQuery = string.Empty;
                selectQuery = "select * from TempSection_NotBreaked";
                DataTable dtNotBreaked = new DataTable();
                OleDbDataAdapter NotDataAdapter = new OleDbDataAdapter(selectQuery, selectConnection);
                NotDataAdapter.Fill(dtNotBreaked);

                string fnodeId = string.Empty;
                string toodeId = string.Empty;
                string sectionId = string.Empty;
                if (dtFinal.Rows.Count > 0 && dtBreaked.Rows.Count > 0)
                {
                    foreach (DataRow bitem in dtBreaked.Rows)
                    {
                        foreach (DataRow fitem in dtFinal.Rows)
                        {
                            if (bitem["SectionId"].ToString().Trim() == fitem["SectionId"].ToString().Trim())
                            {
                                sectionId = fitem["SectionId"].ToString().Trim();
                                fnodeId = fitem["FromNodeId"].ToString().Trim();
                                toodeId = fitem["ToNodeId"].ToString().Trim();

                                string QUERY = "update TempSection_Breaked set [FromNodeId] ='" + fnodeId + "' , [ToNodeId] ='" + toodeId + "' where [SectionId]='" + sectionId + "'";

                                using (OleDbCommand cmd2 = new OleDbCommand(QUERY, selectConnection))
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                fnodeId = string.Empty;
                toodeId = string.Empty;
                sectionId = string.Empty;
                if (dtFinal.Rows.Count > 0 && dtNotBreaked.Rows.Count > 0)
                {
                    foreach (DataRow nitem in dtNotBreaked.Rows)
                    {
                        foreach (DataRow fitem in dtFinal.Rows)
                        {
                            if (nitem["SectionId"].ToString().Trim() == fitem["SectionId"].ToString().Trim())
                            {
                                sectionId = fitem["SectionId"].ToString().Trim();
                                fnodeId = fitem["FromNodeId"].ToString().Trim();
                                toodeId = fitem["ToNodeId"].ToString().Trim();

                                string QUERY = "update TempSection_NotBreaked set [FromNodeId] ='" + fnodeId + "' , [ToNodeId] ='" + toodeId + "' where [SectionId]='" + sectionId + "'";

                                using (OleDbCommand cmd2 = new OleDbCommand(QUERY, selectConnection))
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }

        }

        public void btnTreeView1Menual()
        {
            string mdbfile1 = GETFILE + "\\MDB\\FeederList.mdb";

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;

            OleDbConnection conn = new OleDbConnection(connectionString);
            //string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
            //        ";database=" + txtTGdatabasename.Text +
            //        ";User ID=" + txtTGusername.Text +
            //        ";Password=" + txtTGpassword.Text;

            //SqlConnection conn = new SqlConnection(connectionString);
            string aa = "";
            string bb = "";

            OleDbCommand cmd1 = new OleDbCommand("update FeederList set Cheched='0'", conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd1.ExecuteNonQuery();
            try
            {
                List<string> checklist = nav1.Distinct().ToList();
                for (int i = 0; i < checklist.Count; i++)
                {
                    aa += "'" + checklist[i] + "',";
                }
                bb = aa.Remove(aa.Length - 1);
                cmd1 = new OleDbCommand("update FeederList set Cheched='1' where NetworkID in (" + bb + ")", conn);
                cmd1.ExecuteNonQuery();
                // MessageBox.Show("Selected Feeder Saved");
                conn.Close();
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        public void btnTreeView1Scheduler()
        {
            string mdbfile1 = GETFILE + "\\MDB\\FeederList.mdb";

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;

            OleDbConnection conn = new OleDbConnection(connectionString);
            //string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
            //        ";database=" + txtTGdatabasename.Text +
            //        ";User ID=" + txtTGusername.Text +
            //        ";Password=" + txtTGpassword.Text;

            //SqlConnection conn = new SqlConnection(connectionString);
            string aa = "";
            string bb = "";

            OleDbCommand cmd1 = new OleDbCommand("update FeederList set Cheched='0'", conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd1.ExecuteNonQuery();
            try
            {
                string feeders = GETFILE + "\\Feeder\\SchedulerFeed erList.txt";

                List<string> checklist = File.ReadAllLines(feeders).Distinct().ToList();
                for (int i = 0; i < checklist.Count; i++)
                {
                    aa += "" + checklist[i] + ",";
                }
                bb = aa.Remove(aa.Length - 1);
                cmd1 = new OleDbCommand("update FeederList set Cheched='1' where NetworkID in (" + bb + ")", conn);
                cmd1.ExecuteNonQuery();
                // MessageBox.Show("Selected Feeder Saved");
                conn.Close();
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }
         
        public void btnTreeView1INCScheduler()
        {
            string mdbfile1 = GETFILE + "\\MDB\\FeederList.mdb";

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;

            OleDbConnection conn = new OleDbConnection(connectionString);
            //string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
            //        ";database=" + txtTGdatabasename.Text +
            //        ";User ID=" + txtTGusername.Text +
            //        ";Password=" + txtTGpassword.Text;

            //SqlConnection conn = new SqlConnection(connectionString);
            string aa = "";
            string bb = "";

            OleDbCommand cmd1 = new OleDbCommand("update FeederList set Cheched='0'", conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd1.ExecuteNonQuery();

             

            string connectionStringsc = @"Data Source=" + txtGisservername.Text.Trim() +                                // create the connection
                ";database=" + txtGisDatabasename.Text.Trim() +
                ";User ID=" + txtGisUserid.Text.Trim() +
                ";Password=" + txtGisPassword.Text.Trim();

            //string netconnectionString = @"Data Source=" + Cyme_net_Servernamecombo.Text +                       //Create Connection string
            //         ";database=" + Cyme_net_DBnametext.Text +
            //         ";User ID=" + Cyme_txtMSSQL_username.Text +
            //         ";Password=" + Cyme_txtMSSQL_pwd.Text;

            Cursor.Current = Cursors.WaitCursor;

            SqlConnection consc = new SqlConnection(connectionStringsc);

            string query = "select distinct [XuatTuyen] from [TRACKEQUIPMENTUPDATES]  where XuatTuyen IS NOT NULL and [FeederStatus]='NO' ";
            SqlCommand cmdsc = new SqlCommand(query, consc);                                                // get the value of FeederID and Checked
            if (consc.State != ConnectionState.Open)
            {
                consc.Open();
            }
            SqlDataReader drsc = cmdsc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(drsc);


            //List<string> checklist1 = new List<string>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    checklist1.Add(row["XuatTuyen"].ToString());
            //}
             

            //// Update FeederStatus to YES
            //if (checklist1.Count > 0)
            //{
            //    string bb1 = string.Join(",", checklist1.Select(x => $"'{x}'"));
            //    string updateQuery = $"update TRACKEQUIPMENTUPDATES set FeederStatus='YES' where XuatTuyen in ({bb1})";
            //    SqlCommand updateCmd = new SqlCommand(updateQuery, consc);
            //    updateCmd.ExecuteNonQuery();
            //}


            try
            {
                List<string> checklist = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var networkId = dt.Rows[i]["XuatTuyen"].ToString();
                    aa += "'" + networkId + "',";
                }
                bb = aa.Remove(aa.Length - 1);
                cmd1 = new OleDbCommand("update FeederList set Cheched='1' where NetworkID in (" + bb + ")", conn);
                cmd1.ExecuteNonQuery();
                // MessageBox.Show("Selected Feeder Saved");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incremental Data Not Available");
            }
        }

        public void UPDATETRACKEQUIPMENTUPDATES(string Cyme_config_txtReportDirectory, string st, string feeder)
        {
            try
            {
                string servername = cf.Gisservername;
                string databasename = cf.GisDatabase;
                string username = cf.Gisusername;
                string password = cf.Gispassword;

                string connectionStringsc = @"Data Source=" +servername+                                // create the connection
                ";database=" + databasename +
                ";User ID=" + username +
                ";Password=" + password;
                   
                SqlConnection consc = new SqlConnection(connectionStringsc);
                if (consc.State != ConnectionState.Open)
                {
                    consc.Open();
                }

                // string Fid1 = feeder.Replace("/", "-");
                string Fid1 = feeder;
                //the path of the file"D:\Vishal\Lastest Cesu\TechGration\TechGration\CYMEUPLOAD\14093-01\CYMEimpFile\IMPORT.ERR"
                string path = st + "\\CYMEUPLOAD\\" + Fid1 + "\\CYMEimpFile\\IMPORT.ERR";
                if (File.Exists(path))
                {
                    FileStream inFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(inFile);
                    string record;
                     
                    while ((record = reader.ReadToEnd()) != null)
                    {
                        if (record.Contains("Error"))
                        {
                            string updateQuery = $"update TRACKEQUIPMENTUPDATES set FeederStatus='NO' where XuatTuyen ='" + Fid1 + "'";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, consc);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            string updateQuery = $"update TRACKEQUIPMENTUPDATES set FeederStatus='YES' where XuatTuyen ='" + Fid1 + "'";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, consc);
                            updateCmd.ExecuteNonQuery();
                        }
                        break;
                    }
                }

                consc.Close();
                 
            }
            catch (Exception ex)
            {
            
            }
        }

        string search = "";

        private void BtnFeederSearch_Click(object sender, EventArgs e)
        {
            try
            {
                treeView1.Nodes.Clear();
                
                if (RbtnCheckedfeeder.Checked)
                {
                    RbtnCheckedfeeder.Checked = false;
                }
                if (Rbtnuncheckedfeeder.Checked)
                {
                    Rbtnuncheckedfeeder.Checked = false;
                }
                treeView1.Visible = true;



                BtnFeederSearch.Visible = true;

                search = txtSearch.Text;

                string counter6 = "";
                string counter8 = "";
                int counter9 = 0;
                string name1 = string.Empty;
                string name2 = string.Empty;
                string name3 = string.Empty;
                string name4 = string.Empty;
                string name5 = string.Empty;
                string name6 = string.Empty;
                String NAME6 = string.Empty;

                string mdbfile1 = GETFILE + "\\MDB\\FeederList.mdb";



                string connectionstring25 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;
                OleDbConnection con = new OleDbConnection(connectionstring25);
                DataTable DT = new DataTable();
                DataTable DT1 = new DataTable();
                DataTable DT3 = new DataTable();//
                DataTable DT4 = new DataTable();
                //11kv----------
                #region 11kv
                try
                {
                    string vol = "11 kV";
                    string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC,NetworkID ASC";
                    con = new OleDbConnection(connectionstring25);
                    OleDbCommand com = new OleDbCommand(qrry, con);
                    con.Open();
                    OleDbDataReader dr = com.ExecuteReader();
                    TreeNode noode = new TreeNode("11KV");
                    //treeView1.Nodes.Add(noode);
                    string name31 = string.Empty;
                    DT.Load(dr);

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //  TotalFeeederCount = DT.Rows.Count.ToString();
                        string counter7 = DT.Rows[i][4].ToString().Trim();
                        if (counter7 == "1")
                        {
                            counter8 = counter7;
                        }
                    }



                    int j = 0;
                    for (; j < DT.Rows.Count; j++)
                    {

                        string counter = DT.Rows[j][3].ToString().Trim();
                        string counter5 = DT.Rows[j][4].ToString().Trim();

                        if (counter5 == "1")
                        {
                            counter6 = counter;
                        }
                        TotalFeedercount.Text = "Total Feeder : " + TotalFeeederCount;

                        string Circle = DT.Rows[j]["Circle"].ToString();
                        string Division = DT.Rows[j]["Division"].ToString();
                        string Name = DT.Rows[j]["Name"].ToString();
                        string FeederId1 = DT.Rows[j]["FeederId"].ToString();
                        string Region_Name = DT.Rows[j]["Region_Name"].ToString();
                        string Zone_Name = DT.Rows[j]["Zone_Name"].ToString();


                        if (Region_Name != name5)
                        {
                            name5 = Region_Name;
                            //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            nodeR = noode.Nodes.Add(Convert.ToString(DT.Rows[j]["Region_Name"].ToString()));
                            // treeView1.Nodes.Add(node);
                        }

                        if (Zone_Name != name6)
                        {
                            name6 = Zone_Name;
                            //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            nodeZ = nodeR.Nodes.Add(Convert.ToString(DT.Rows[j]["Zone_Name"].ToString()));
                            // treeView1.Nodes.Add(node);
                        }


                        if (Circle != name1)
                        {
                            name1 = Circle;
                            Noode1 = nodeZ.Nodes.Add(DT.Rows[j]["Circle"].ToString());
                            //treeView1.Nodes.Add(Noode1);
                            if (search.ToLower() == Circle.ToLower())
                            {
                                treeView1.SelectedNode = Noode1;
                                treeView1.Focus();
                            }
                        }


                        if (Division != name2)
                        {
                            name2 = Division;
                            Node1 = Noode1.Nodes.Add(DT.Rows[j]["Division"].ToString());
                            if (search.ToLower() == Division.ToLower())
                            {
                                treeView1.SelectedNode = Node1;
                                treeView1.Focus();
                            }
                        }
                        if (Name != name31)
                        {
                            name31 = Name;
                            //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                            node3 = Node1.Nodes.Add(Convert.ToString(DT.Rows[j]["Name"].ToString()));
                            if (search.ToLower() == Division.ToLower())
                            {
                                treeView1.SelectedNode = node3;
                                treeView1.Focus();
                            }
                        }
                        if (FeederId1 != name4)
                        {
                            name4 = FeederId1;
                            Node3 = node3.Nodes.Add(DT.Rows[j]["FeederId"].ToString());

                            if (search.ToLower() == FeederId1.ToLower())
                            {
                                // Node3.Checked = true;
                                treeView1.SelectedNode = Node3;
                                treeView1.SelectedNode.EnsureVisible();
                                treeView1.Focus();
                                counter9++;
                            }
                        }
                    }

                    //TotalFeedercount.Text = "Total FeederID : " + TotalFeedercount;


                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                #endregion 11kv

                #region 22kv

                try
                {
                    string vol = "22KV";
                    string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 DESC,NetworkID ASC";
                    con = new OleDbConnection(connectionstring25);
                    OleDbCommand com = new OleDbCommand(qrry, con);
                    con.Open();
                    OleDbDataReader dr = com.ExecuteReader();
                    TreeNode noode = new TreeNode("22KV");
                    treeView1.Nodes.Add(noode);
                    string name31 = string.Empty;
                    DT1.Load(dr);

                    for (int i = 0; i < DT1.Rows.Count; i++)
                    {
                        //  TotalFeeederCount = DT.Rows.Count.ToString();
                        string counter7 = DT1.Rows[i][4].ToString().Trim();
                        if (counter7 == "1")
                        {
                            counter8 = counter7;
                        }
                    }

                    //  if (counter8 == "1")
                    {

                        int j = 0;
                        for (; j < DT1.Rows.Count; j++)
                        {

                            //string counter = DT1.Rows[j][3].ToString().Trim();
                            //string counter5 = DT1.Rows[j][4].ToString().Trim();

                            //if (counter5 == "1")
                            //{
                            //    counter6 = counter;
                            //}
                            //TotalFeedercount.Text = "Total Feeder : " + TotalFeeederCount;
                            //string Circle = DT1.Rows[j]["Circle"].ToString();
                            //string Division = DT1.Rows[j]["Division"].ToString();
                            string Name = DT1.Rows[j]["Name"].ToString();
                            string FeederId1 = DT1.Rows[j]["FeederId"].ToString();
                             
                            //string Region_Name = DT1.Rows[j]["Region_Name"].ToString();
                            //string Zone_Name = DT1.Rows[j]["Zone_Name"].ToString();

                            //if (Region_Name != name5)
                            //{
                            //    name5 = Region_Name;
                            //    //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            //    nodeR = noode.Nodes.Add(Convert.ToString(DT1.Rows[j]["Region_Name"].ToString()));
                            //    // treeView1.Nodes.Add(node);
                            //}

                            //if (Zone_Name != name6)
                            //{
                            //    name6 = Zone_Name;
                            //    //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            //    nodeZ = nodeR.Nodes.Add(Convert.ToString(DT1.Rows[j]["Zone_Name"].ToString()));
                            //    // treeView1.Nodes.Add(node);
                            //}
                            //if (Circle != name1)
                            //{
                            //    name1 = Circle;
                            //    Noode1 = nodeZ.Nodes.Add(DT1.Rows[j]["Circle"].ToString());
                            //    //treeView1.Nodes.Add(Noode1);
                            //    if (search.ToLower() == Circle.ToLower())
                            //    {
                            //        treeView1.SelectedNode = Noode1;
                            //        treeView1.Focus();
                            //    }
                            //}


                            //if (Division != name2)
                            //{
                            //    name2 = Division;
                            //    Node1 = Noode1.Nodes.Add(DT1.Rows[j]["Division"].ToString());
                            //    if (search.ToLower() == Division.ToLower())
                            //    {
                            //        treeView1.SelectedNode = Node1;
                            //        treeView1.Focus();
                            //    }
                            //}
                            if (Name != name3)
                            {
                                name3 = Name;
                                //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                                node3 = noode.Nodes.Add(Convert.ToString(DT1.Rows[j]["Name"].ToString()));
                                if (search.ToLower() == Name.ToLower())
                                {
                                    treeView1.SelectedNode = node3;
                                    treeView1.Focus();
                                }
                            }
                            if (FeederId1 != name4)
                            {

                                name4 = FeederId1;
                                Node3 = node3.Nodes.Add(DT1.Rows[j]["FeederId"].ToString());

                                if (search.ToLower() == FeederId1.ToLower())
                                {
                                    // Node3.Checked = true;
                                    treeView1.SelectedNode = Node3;
                                    treeView1.SelectedNode.EnsureVisible();
                                    treeView1.Focus();
                                    counter9++;
                                }
                            }
                        }

                        //TotalFeedercount.Text = "Total FeederID : " + TotalFeedercount;

                    }
                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                #endregion 22kv


                //33kv.................

                #region 33kV
                try
                {
                    string name321 = string.Empty;
                    string name11 = string.Empty;
                    string name21 = string.Empty;
                    string name31 = string.Empty;
                    string name41 = string.Empty;
                    string name411 = string.Empty;
                    string vol = "33 kV";
                    string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC,NetworkID ASC";
                    con = new OleDbConnection(connectionstring25);
                    OleDbCommand com = new OleDbCommand(qrry, con);
                    con.Open();
                    OleDbDataReader dr = com.ExecuteReader();
                   TreeNode noude = new TreeNode("33KV");
                   // treeView1.Nodes.Add(noude);

                    DT4.Load(dr);

                    for (int i = 0; i < DT4.Rows.Count; i++)
                    {
                        //  TotalFeeederCount = DT1.Rows.Count.ToString();
                        string counter7 = DT4.Rows[i][4].ToString().Trim();
                        if (counter7 == "1")
                        {
                            counter8 = counter7;
                        }
                    }

                    //  if (counter8 == "1")
                    {

                        int j = 0;
                        for (; j < DT4.Rows.Count; j++)
                        {

                            string counter = DT4.Rows[j][3].ToString().Trim();
                            string counter5 = DT4.Rows[j][4].ToString().Trim();

                            if (counter5 == "1")
                            {
                                counter6 = counter;
                            }
                            TotalFeedercount.Text = "Total Feeder : " + TotalFeeederCount;

                            string Circle = DT4.Rows[j]["Circle"].ToString();
                            string Division = DT4.Rows[j]["Division"].ToString();
                            string Name = DT4.Rows[j]["Name"].ToString();
                            string FeederId1 = DT4.Rows[j]["FeederId"].ToString();
                            string Region_Name = DT4.Rows[j]["Region_Name"].ToString();
                            string Zone_Name = DT4.Rows[j]["Zone_Name"].ToString();

                            if (Region_Name != name5)
                            {
                                name5 = Region_Name;
                                //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                                nodeR = noude.Nodes.Add(Convert.ToString(DT4.Rows[j]["Region_Name"].ToString()));
                                // treeView1.Nodes.Add(node);
                            }

                            if (Zone_Name != name6)
                            {
                                name6 = Zone_Name;
                                //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                                nodeZ = nodeR.Nodes.Add(Convert.ToString(DT4.Rows[j]["Zone_Name"].ToString()));
                                // treeView1.Nodes.Add(node);
                            }

                            if (Circle != name11)
                            {
                                name11 = Circle;
                                noude1 = nodeZ.Nodes.Add(DT4.Rows[j]["Circle"].ToString());
                                //treeView1.Nodes.Add(Noode1);
                                if (search.ToLower() == Circle.ToLower())
                                {
                                    treeView1.SelectedNode = noude1;
                                    treeView1.Focus();
                                }
                            }


                            if (Division != name21)
                            {
                                name21 = Division;
                                noude2 = noude1.Nodes.Add(DT4.Rows[j]["Division"].ToString());
                                if (search.ToLower() == Division.ToLower())
                                {
                                    treeView1.SelectedNode = noude2;
                                    treeView1.Focus();
                                }
                            }
                            if (Name != name321)
                            {
                                name321 = Name;
                                //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                                noude3 = noude2.Nodes.Add(Convert.ToString(DT4.Rows[j]["Name"].ToString()));
                                if (search.ToLower() == Division.ToLower())
                                {
                                    treeView1.SelectedNode = noude3;
                                    treeView1.Focus();
                                }
                            }
                            //if (SubstationId != name411)
                            //{
                            //    name411 = SubstationId;
                            //    noude4 = noude3.Nodes.Add(DT1.Rows[j]["SubstationId"].ToString());
                            //    if (search.ToLower() == SubstationId.ToLower())
                            //    {
                            //        treeView1.SelectedNode = noude4;
                            //        treeView1.Focus();
                            //    }
                            //}

                            if (FeederId1 != name41)
                            {
                                name41 = FeederId1;
                                noude5 = noude3.Nodes.Add(DT4.Rows[j]["FeederId"].ToString());

                                if (search.ToLower() == FeederId1.ToLower())
                                {
                                    // Node3.Checked = true;
                                    treeView1.SelectedNode = noude5;
                                    treeView1.SelectedNode.EnsureVisible();
                                    treeView1.Focus();
                                    counter9++;
                                }
                            }
                        }

                        //TotalFeedercount.Text = "Total FeederID : " + TotalFeedercount;

                    }
                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
                #endregion 33kv

                #region 100kv

                try
                {
                    string vol = "100 kV";
                    string qrry = "select Distinct NetworkID as FeederId,Group1 as Name,Group2 as Division,Group3 as Circle,Group4 as Zone_Name,Group5 as Region_Name,SubstationId,Cheched from FeederList where voltage='" + vol + "' ORDER  BY Group5,Group4,Group3,Group2,Group1 ASC,NetworkID ASC";
                    con = new OleDbConnection(connectionstring25);
                    OleDbCommand com = new OleDbCommand(qrry, con);
                    con.Open();
                    OleDbDataReader dr = com.ExecuteReader();
                    TreeNode noode = new TreeNode("100KV");
                    //treeView1.Nodes.Add(noode);
                    string name31 = string.Empty;
                    DT3.Load(dr);

                    for (int i = 0; i < DT3.Rows.Count; i++)
                    {
                        //  TotalFeeederCount = DT.Rows.Count.ToString();
                        string counter7 = DT3.Rows[i][4].ToString().Trim();
                        if (counter7 == "1")
                        {
                            counter8 = counter7;
                        }
                    }

                    //  if (counter8 == "1")
                    {

                        int j = 0;
                        for (; j < DT3.Rows.Count; j++)
                        {

                            string counter = DT3.Rows[j][3].ToString().Trim();
                            string counter5 = DT3.Rows[j][4].ToString().Trim();

                            if (counter5 == "1")
                            {
                                counter6 = counter;
                            }
                            TotalFeedercount.Text = "Total Feeder : " + TotalFeeederCount;
                            string Circle = DT3.Rows[j]["Circle"].ToString();
                            string Division = DT3.Rows[j]["Division"].ToString();
                            string Name = DT3.Rows[j]["Name"].ToString();
                            string FeederId1 = DT3.Rows[j]["FeederId"].ToString();
                            string Region_Name = DT3.Rows[j]["Region_Name"].ToString();
                            string Zone_Name = DT3.Rows[j]["Zone_Name"].ToString();



                            if (Region_Name != name5)
                            {
                                name5 = Region_Name;
                                //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                                nodeR = noode.Nodes.Add(Convert.ToString(DT.Rows[j]["Region_Name"].ToString()));
                                // treeView1.Nodes.Add(node);
                            }

                            if (Zone_Name != name6)
                            {
                                name6 = Zone_Name;
                                //node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                                nodeZ = nodeR.Nodes.Add(Convert.ToString(DT.Rows[j]["Zone_Name"].ToString()));
                                // treeView1.Nodes.Add(node);
                            }
                            if (Circle != name1)
                            {
                                name1 = Circle;
                                Noode1 = nodeZ.Nodes.Add(DT3.Rows[j]["Circle"].ToString());
                                //treeView1.Nodes.Add(Noode1);
                                if (search.ToLower() == Circle.ToLower())
                                {
                                    treeView1.SelectedNode = Noode1;
                                    treeView1.Focus();
                                }
                            }


                            if (Division != name2)
                            {
                                name2 = Division;
                                Node1 = Noode1.Nodes.Add(DT3.Rows[j]["Division"].ToString());
                                if (search.ToLower() == Division.ToLower())
                                {
                                    treeView1.SelectedNode = Node1;
                                    treeView1.Focus();
                                }
                            }
                            if (Name != name31)
                            {
                                name31 = Name;
                                //node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                                node3 = Node1.Nodes.Add(Convert.ToString(DT3.Rows[j]["Name"].ToString()));
                                if (search.ToLower() == Division.ToLower())
                                {
                                    treeView1.SelectedNode = node3;
                                    treeView1.Focus();
                                }
                            }
                            //if (SubstationId != NAME6)
                            //{
                            //    NAME6 = SubstationId;
                            //    Node2 = node3.Nodes.Add(DT.Rows[j]["SubstationId"].ToString());
                            //    if (search.ToLower() == SubstationId.ToLower())
                            //    {
                            //        treeView1.SelectedNode = Node2;
                            //        treeView1.Focus();
                            //    }
                            //}

                            if (FeederId1 != name4)
                            {
                                name4 = FeederId1;
                                Node3 = node3.Nodes.Add(DT3.Rows[j]["FeederId"].ToString());

                                if (search.ToLower() == FeederId1.ToLower())
                                {
                                    // Node3.Checked = true;
                                    treeView1.SelectedNode = Node3;
                                    treeView1.SelectedNode.EnsureVisible();
                                    treeView1.Focus();
                                    counter9++;
                                }
                            }
                        }

                        //TotalFeedercount.Text = "Total FeederID : " + TotalFeedercount;

                    }
                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                #endregion 100kv

                TotalFeedercount.Text = "Total Feeder: " + (Convert.ToInt32(DT1.Rows.Count.ToString()) + Convert.ToInt32(DT.Rows.Count.ToString()) + Convert.ToInt32(DT4.Rows.Count.ToString()) + Convert.ToInt32(DT3.Rows.Count.ToString())).ToString();


            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        private void RbtnCheckedfeeder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RbtnCheckedfeeder.Checked == true)
                {
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        node.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        private void Rbtnuncheckedfeeder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rbtnuncheckedfeeder.Checked == true)
                {
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        node.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        Thread loadingThread;
        private Process process;

         
        private void ShowLoadingGif()
        {
            try
            {
                this.Enabled = false;
                // btnClose11KV.Enabled = true;
                System.Drawing.Point formCenterPt = new System.Drawing.Point()
                {
                    X = this.Location.X + (this.Width / 2) + 35,
                    Y = this.Location.Y + (this.Height / 2) + 10
                };
                loadingThread = new Thread(() => ShowLoading(formCenterPt));
                loadingThread.IsBackground = true;
                loadingThread.Name = "Loading gif";
                loadingThread.Start();
                //btnClose11KV.Enabled = true;
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());

                // MessageBox.Show("Some Problem occered during processing !", Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void HideLoadingGif()
        {
            try
            {
                this.Enabled = true;
                if (loadingThread != null)
                {

                    if (loadingThread.IsAlive)
                        loadingThread.Abort();
                }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                // MessageBox.Show(ex.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
         
        private void ShowLoading(System.Drawing.Point parentCenter)
        {
            try
            {
                TechgrationWaiting loading = new TechgrationWaiting();
                System.Drawing.Point offsetPt = new System.Drawing.Point
                {
                    X = parentCenter.X - ((loading.Width / 2) + 50),
                    Y = parentCenter.Y - (loading.Height / 2)
                };
                loading.StartPosition = FormStartPosition.Manual;
                loading.Location = offsetPt;
                loading.TopMost = true;
                loading.ShowDialog();
                loading.Focus();
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        private void BtnFeederImport_Click(object sender, EventArgs e)
        {
            try
            {
                Selectfeedercount.Text = "Selected Feeder : 0";
                TotalFeedercount.Text = "Total Feeder : ";
                nav1.Clear();
                //string x = "0";
                //TotalFeedercount.Text = "Total Feeder :"+x;
                //Selectfeedercount.Text = "Selectfeedercount :"+x;
                string ImpError = GETFILE + "\\ErrorLog\\ImportExcepctionError.txt";
                if (File.Exists(ImpError))
                {
                    File.Delete(ImpError);
                }





                string mdbfile1 = GETFILE + ConfigurationManager.AppSettings["Connection1"];


                string connectionstring1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;

                OleDbConnection con = new OleDbConnection(connectionstring1);
                ShowLoadingGif();
                treeView1.Nodes.Clear();

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                // DataTable dt = con.GetSchema("FeederList");
                System.Data.DataTable dt = null;

                //   Get the data table containing the schema;
                string strSheetTableName = null;
                dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dt.Rows)
                {
                    strSheetTableName = row["TABLE_NAME"].ToString();
                    if (strSheetTableName == "FeederList")
                    {
                        string feederlistdelete = "drop table FeederList";
                        OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);
                        cmm.ExecuteNonQuery();

                    }
                    if (strSheetTableName == "PC03_DIEMXUATTUYEN")
                    {
                        string feederlistdelete = "drop table PC03_DIEMXUATTUYEN";
                        OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);
                        cmm.ExecuteNonQuery();

                    }
                    //if (strSheetTableName == "GIS_CIRCUITSOURCE")
                    //{
                    //    string feederlistdelete = "drop table GIS_CircuitSource";
                    //    OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);

                    //    cmm.ExecuteNonQuery();

                    //}
                    if (strSheetTableName == "GIS_DOMAIN")
                    {
                        string feederlistdelete = "drop table GIS_DOMAIN";
                        OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);

                        cmm.ExecuteNonQuery();

                    }
                    //if (strSheetTableName == "GIS_Switchgear")
                    //{
                    //    string feederlistdelete = "drop table GIS_Switchgear";
                    //    OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);

                    //    cmm.ExecuteNonQuery();

                    //}
                    //if (strSheetTableName == "GIS_SUBSTATION")
                    //{
                    //    string feederlistdelete = "drop table GIS_SUBSTATION";
                    //    OleDbCommand cmm = new OleDbCommand(feederlistdelete, con);

                    //    cmm.ExecuteNonQuery();

                    //}
                }
                con.Close();


                CreateMDB(con);





                string myqrr = "SELECT Distinct XuatTuyen as [FeederId], DienAp as [SubstationId],DienAp as [Voltage],OBJECTID,SubstationName FROM PC03_DIEMXUATTUYEN";
                //string myqrr = "SELECT Distinct cs.[FeederId], cs.[SubstationId], sw.[Voltage], sw.[Feeder_Name], sw.[Circle_Name], sw.[Division_Name], sub.[NIN], sub.[Name],sw.[Zone_Name],sw.[Region_Name],sw.X,sw.Y,sw.OBJECTID FROM ([GIS_CircuitSource] AS cs INNER JOIN [GIS_Switchgear] AS sw ON cs.[SwitchGearObjID] = sw.[OBJECTID] AND cs.[FeederID] = sw.[FeederID]) INNER JOIN [GIS_SUBSTATION] AS sub ON cs.[SubstationId] = sub.[NIN] Where len(cs.[SubstationId])>0";

                OleDbCommand cmd = new OleDbCommand(myqrr, con);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string feederiddd = string.Empty;
                cmd.ExecuteNonQuery();
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                Thread.Sleep(2000);
                DataTable DT1 = new DataTable();
                ad.Fill(DT1);
                Thread.Sleep(2000);
                con.Close();
                bool visss = true;
                for (int i = 0; i < DT1.Rows.Count; i++)
                {

                    //string Cir_Div_SubD_BU_Sec = DT1.Rows[i]["Cir_Div_SubD_BU_Sec"].ToString().Trim();
                    // string[] myyarr = Cir_Div_SubD_BU_Sec.Split('/');

                    string Circle = string.Empty;
                    string Division = string.Empty;
                    string X = string.Empty;
                    string Y = string.Empty;
                    string OBJECTID = DT1.Rows[i]["OBJECTID"].ToString().Trim();
                    string Region_Name = string.Empty;
                    string Zone_Name = string.Empty;
                    string Subdivision = "";
                    string SubstationId = string.Empty;
                    string Name = string.Empty;
                    string Feederid = DT1.Rows[i]["FeederId"].ToString().Trim();
                    string Feederid1 = DT1.Rows[i]["FeederId"].ToString().Trim();
                    string SubstationName = DT1.Rows[i]["SubstationName"].ToString().Trim();
                    if (Feederid1== "134/283171/321")
                    {

                    }


                    if (feederiddd != Feederid)
                    {
                       
                        string Voltage = DT1.Rows[i]["Voltage"].ToString().Trim();
                        if (Voltage=="6")
                        {
                            Voltage = "22KV";
                        }
                        int Cheched = 0;
                        con = new OleDbConnection(connectionstring1);
                        if (visss == true)
                        {
                            string myqrr1 = "create table FeederList (NetworkID varchar(250),voltage Varchar(250),NetworkName varchar(250),Group1 varchar(250),Group2 varchar(250),Group3 varchar(250),Group4 varchar(250),Group5 varchar(250),Group6 varchar(250),SubstationId varchar(250),Cheched int,X varchar(250),Y varchar(250),OBJECTID varchar(250),Feederid varchar(250))";
                            OleDbCommand cmd2 = new OleDbCommand(myqrr1, con);
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            visss = false;
                        }

                      

                        string myqrr11 = "insert into FeederList (NetworkID,NetworkName,voltage,Group1,Group2,Group3,Group4,Group5,Group6,SubstationId,Cheched,X,Y,OBJECTID,Feederid) values ('" + Feederid + "','" + Name + "','" + Voltage + "','" + SubstationName + "','" + Division + "','" + Circle + "','" + Zone_Name + "','" + Region_Name + "','" + "" + "','" + SubstationId + "','" + Cheched + "','" + X + "','" + Y + "','" + OBJECTID + "','"+ Feederid1 + "')";

                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        OleDbCommand cmd21 = new OleDbCommand(myqrr11, con);
                        cmd21.ExecuteNonQuery();
                        con.Close();
                    }
                    //string Division = DT1.Rows[i]["FeederName"].ToString().Trim();
                    //string Subdivision = DT1.Rows[i]["FeederName"].ToString().Trim();

                }


                try
                {
                    string myqrr2 = "Select*from GIS_DOMAIN";

                    OleDbCommand cmdp = new OleDbCommand(myqrr2, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    cmdp.ExecuteNonQuery();
                    OleDbDataAdapter adr = new OleDbDataAdapter(cmdp);

                    DataTable DT1r = new DataTable();
                    adr.Fill(DT1r);
                    con.Close();

                    for (int p = 0; p < DT1r.Rows.Count; p++)
                    {
                        string Code = DT1r.Rows[p]["Code"].ToString();
                        string Type = DT1r.Rows[p]["Type"].ToString();
                        string Value1 = DT1r.Rows[p]["Value1"].ToString();


                        if (Type == "TT_DIENAP")
                        {
                            string update213 = "UPDATE FeederList SET  voltage = '" + Value1 + "' WHERE voltage='" + Code + "'";
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            OleDbCommand cmd2x113 = new OleDbCommand(update213, con);

                            cmd2x113.ExecuteNonQuery();
                        }








                    }
                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
                }



                //try
                //{
                //    string myqrr2 = "Select Circle_Name,Division_Name,Feeder_Name from GIS_Switchgear";
                //    OleDbCommand cmdp = new OleDbCommand(myqrr2, con);
                //    if (con.State != ConnectionState.Open)
                //    {
                //        con.Open();
                //    }
                //    cmdp.ExecuteNonQuery();
                //    OleDbDataAdapter adr = new OleDbDataAdapter(cmdp);

                //    DataTable DT1r = new DataTable();
                //    adr.Fill(DT1r);
                //    con.Close();
                //    string Circle_Code = string.Empty;
                //    string Division_Code = string.Empty;
                //    string Subdivision_Code = string.Empty;
                //    string Circle_Name = string.Empty;
                //    string Division_Name = string.Empty;
                //    string SubDivsion = string.Empty;
                //    for (int i = 0; i < DT1r.Rows.Count; i++)
                //    {
                //        Circle_Code = DT1r.Rows[i]["Circle_Code"].ToString();
                //        Division_Code = DT1r.Rows[i]["Division_Code"].ToString();
                //        // Subdivision_Code = DT1r.Rows[i]["Subdivision_Code"].ToString();
                //        Circle_Name = DT1r.Rows[i]["Circle_Name"].ToString();
                //        Division_Name = DT1r.Rows[i]["Division_Name"].ToString();
                //        //SubDivsion = DT1r.Rows[i]["SubDivsion"].ToString(); 

                //        if (Circle_Name != "")
                //        {
                //            string update = "UPDATE FeederList SET Group4 = '" + Circle_Name + "' WHERE Group4='" + Circle_Code + "'";
                //            if (con.State != ConnectionState.Open)
                //            {
                //                con.Open();
                //            }
                //            OleDbCommand cmd2x = new OleDbCommand(update, con);
                //            cmd2x.ExecuteNonQuery();
                //        }
                //        if (Division_Name != "")
                //        {
                //            string update = "UPDATE FeederList SET Group3 = '" + Division_Name + "' WHERE Group3='" + Division_Code + "'";
                //            if (con.State != ConnectionState.Open)
                //            {
                //                con.Open();
                //            }
                //            OleDbCommand cmd2x = new OleDbCommand(update, con);
                //            cmd2x.ExecuteNonQuery();
                //        }
                //        if (SubDivsion != "")
                //        {
                //            string update = "UPDATE FeederList SET Group2 = '" + SubDivsion + "' WHERE Group2='" + Subdivision_Code + "'";
                //            if (con.State != ConnectionState.Open)
                //            {
                //                con.Open();
                //            }
                //            OleDbCommand cmd2x = new OleDbCommand(update, con);
                //            cmd2x.ExecuteNonQuery();
                //        }



                //    }

                //}
                //catch (Exception ex)
                //{
                //    TechError erro = new TechError();
                //    erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
                //}


                if (RbtnFeederImport.Checked)
                {
                    LoadFeederlistOntreeview();
                    
                }
                else
                {
                    MessageBox.Show("Please Select scope of import");
                }

                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

                treeView1.ExpandAll();
                treeView1.Nodes[0].EnsureVisible();
                HideLoadingGif();
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ImportExceptionErrorHandle(GETFILE, ex.ToString());
            }
        }

        private void BtnTGtestcon_Click(object sender, EventArgs e)
        {
            #region

            string connectionString = @"Data Source=" + txtGisDatabasename.Text +                       //Create Connection string
                           ";database=" + txtGisDatabasename.Text +
                           ";User ID=" + txtGisUserid.Text +
                           ";Password=" + txtGisPassword.Text;

            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
            }
            #endregion
        }
 
        private void BtnGISTestconnSql_Click_1(object sender, EventArgs e)
        {
            string tab = string.Empty;
            string connectionString = @"Data Source=" + txtGisservername.Text +                                // create the connection
                        ";database=" + txtGisDatabasename.Text +
                        ";User ID=" + txtGisUserid.Text +
                        ";Password=" + txtGisPassword.Text;
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    string texx = GETFILE + "\\ConfigFile\\GISTableConfigFile.txt";
                    List<string> check = new List<string>();
                    var logFile = File.ReadAllLines(texx);
                    foreach (var s in logFile) check.Add(s);

                    /// List<string> check = new List<string>();
                    //check.Add("HTCABLE");
                    //check.Add("LTCABLE");
                    //check.Add("HTCONDUCTOR");
                    //check.Add("LTCONDUCTOR");
                    //check.Add("DISTRIBUTIONTRANSFORMER");
                    //check.Add("SWITCHGEAR");
                    //check.Add("SWITCH");
                    //check.Add("FUSE");
                    //check.Add("SHUNTCAPACITOR");
                    //check.Add("BUSBAR");
                    //check.Add("HTSERVICEPOINT");
                    //check.Add("LTSERVICEPOINT");
                    //// check.Add("CIRCUITSOURCE");
                    //check.Add("CONSUMERINFO");
                    //check.Add("CIRCUIT_SOURCE");
                    //check.Add("SUBSTATION");
                    //check.Add("SWITCHGEAR");
                    for (int i = 0; i < check.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("select TOP 1 * from [" + txt_GISschemaName.Text + "].[" + check[i] + "]", obj);
                        if (obj.State != ConnectionState.Open)
                        {
                            obj.Open();
                        }
                        try
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            TechError erro = new TechError();
                            erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                            tab += "\r\n" + check[i] + " Not Found in GIS Schema(" + txt_GISschemaName.Text + ")";
                        }
                        obj.Close();
                    }

                    if (!string.IsNullOrWhiteSpace(tab)) //(tab != null)
                    {
                        MessageBox.Show(tab);
                    }
                    else
                    {
                        MessageBox.Show("Test Connection Successfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Test Connection Failed");
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
            }

        }

        private void btngisrecord_Click(object sender, EventArgs e)
        {
            GIS_RECORD f2 = new GIS_RECORD();
            f2.ShowDialog();
        }

        private void btnTGtest_Click(object sender, EventArgs e)
        {
            #region

            string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                           ";database=" + txtTGdatabasename.Text +
                           ";User ID=" + txtTGuserid.Text +
                           ";Password=" + txtTGpassword.Text;

            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
            }
            #endregion
        }

        private void rbtnpeakdate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnpeakdate.Checked == true)
            {
                txtfromdatepeak.Enabled = true;
                txttodatepeak.Enabled = true;
                btnfromdateCal.Enabled = true;
                btntodatepeakCal.Enabled = true;

                txtfordate.Enabled = false;
                btnfordateCal.Enabled = false;

            }
        }

        private void rbtnfordate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnfordate.Checked == true)
            {
                txtfordate.Enabled = true;
                btnfordateCal.Enabled = true;

                txtfromdatepeak.Enabled = false;
                txttodatepeak.Enabled = false;
                btnfromdateCal.Enabled = false;
                btntodatepeakCal.Enabled = false;

            }
        }

        private void btnfromdateCal_Click(object sender, EventArgs e)
        {
            btn1 = true;
            Calendar.Visible = true;

        }

        private void btntodatepeakCal_Click(object sender, EventArgs e)
        {
            btn2 = true;
            Calendar.Visible = true;

        }

        private void btnfordateCal_Click(object sender, EventArgs e)
        {
            Calendar.Visible = true;
        }

        private void checkpeak_CheckedChanged(object sender, EventArgs e)
        {

            //if (rbtnmeterfordate.Checked == true)
            //{
            //    rbtnmeterbetween.Enabled = true;
            //    rbtnmeterfordate.Enabled = true;
            //    txtmeterfordate.Enabled = true;
            //    btnmeterfordate.Enabled = true;
            //}
            //if (rbtnmeterbetween.Checked == true)
            //{
            //    rbtnmeterbetween.Enabled = true;
            //    txtmeterfromdate.Enabled = true;
            //    txtmetertodate.Enabled = true;
            //    btnmeterfromdate.Enabled = true;
            //    btnmetertodate.Enabled = true;
            //    rbtnmeterfordate.Enabled = true;
            //}
            if (checkpeak.Checked == true)
            {
                rbtnpeakdate.Enabled = true;
                rbtnfordate.Enabled = true;
            }
            else
            {
                rbtnpeakdate.Enabled = false;
                rbtnfordate.Enabled = false;
                txttodatepeak.Enabled = false;
                txtfromdatepeak.Enabled = false;
                txttodatepeak.Enabled = false;
                txtfordate.Enabled = false;
                btnfordateCal.Enabled = false;
                btntodatepeakCal.Enabled = false;
                btnfromdateCal.Enabled = false;
            }
        }

         
        private void rbtnmeterbetween_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnmeterbetween.Checked == true)
            {
                txtmeterfromdate.Enabled = true;
                txtmetertodate.Enabled = true;
                btnmeterfromdate.Enabled = true;
                btnmetertodate.Enabled = true;

                txtmeterfordate.Enabled = false;
                btnmeterfordate.Enabled = false;
            }
        }

        private void rbtnmeterfordate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnmeterfordate.Checked == true)
            {
                txtmeterfordate.Enabled = true;
                btnmeterfordate.Enabled = true;

                txtmeterfromdate.Enabled = false;
                txtmetertodate.Enabled = false;
                btnmeterfromdate.Enabled = false;
                btnmetertodate.Enabled = false;
            }
        }

        private void checkmeter_CheckedChanged(object sender, EventArgs e)
        {

            if (checkmeter.Checked == true)
            {

                if (rbtnmeterfordate.Checked == true)
                {
                    rbtnmeterbetween.Enabled = true;
                    rbtnmeterfordate.Enabled = true;
                    txtmeterfordate.Enabled = true;
                    btnmeterfordate.Enabled = true;
                }
                if (rbtnmeterbetween.Checked == true)
                {
                    rbtnmeterbetween.Enabled = true;
                    txtmeterfromdate.Enabled = true;
                    txtmetertodate.Enabled = true;
                    btnmeterfromdate.Enabled = true;
                    btnmetertodate.Enabled = true;
                    rbtnmeterfordate.Enabled = true;
                }

            }
            else
            {
                rbtnmeterbetween.Enabled = false;
                rbtnmeterfordate.Enabled = false;
                txtmetertodate.Enabled = false;
                txtmeterfromdate.Enabled = false;
                txtmeterfordate.Enabled = false;
                btnmetertodate.Enabled = false;
                btnmeterfromdate.Enabled = false;
                btnmeterfordate.Enabled = false;
            }
        }

        private void btnmeterfromdate_Click(object sender, EventArgs e)
        {
            btn1 = true;
            Calendar1.Visible = true;
        }

        private void btnmetertodate_Click(object sender, EventArgs e)
        {
            btn2 = true;
            Calendar1.Visible = true;
        }

        private void btnmeterfordate_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
        }

        private void btnproConnection_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + txtproServer.Text +                       //Create Connection string
                        ";database=" + txtproDatabaseName.Text +
                        ";User ID=" + txtproUser.Text +
                        ";Password=" + txtpropassword.Text;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                }
            }

            //InsertProfileDataTable();
            //CreateProfileNetworkTextFile();
        }

        public void InsertProfileDataTable()       // Insert One Data Table To Other Data Table
        {
            #region
            try
            {
                string connectionmeter = @"Data Source=" + txtproServer.Text +                       //Create Connection string
                      ";database=" + txtproDatabaseName.Text +
                      ";User ID=" + txtproUser.Text +
                      ";Password=" + txtpropassword.Text;
                SqlConnection sq = new SqlConnection(connectionmeter);

                string BB = "select * from CYME_METER_PROFILE_HR1";
                //string BB = "select DAYPROFILE_DATE from CYME_METER_PROFILE_HR where DAYPROFILE_DATE Like '%:00:00.%'";
                // string BB = " select * from CYME_METER_PROFILE_HR1 ORDER BY INTERVAL "; //okk
                //string BB = "  select * from CYME_METER_PROFILE_HR1 where DAYPROFILE_DATE Like '%:00:00.%' and DAYPROFILE_DATE Like '%-03-%'"; //okk
                //string BB = "select * from CYME_METER_PROFILE_HR where FEEDER_NAME ='"+ FEEDER_NAME[j1] +"'and  DAYPROFILE_DATE Like '%-02-%' and DAYPROFILE_DATE Like '%:00:00.%'  ORDER BY FEEDER_NAME ";
                using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                {
                    sq.Open();
                    DataTable dt = new DataTable();
                    SqlDataReader dr = cmd1.ExecuteReader();
                    dt.Load(dr);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string FORMAT_ID = dt.Rows[j]["METER_SR_NO"].ToString();
                        string FEEDER_NAME = dt.Rows[j]["FEEDER_NAME"].ToString();
                        string DAYPROFILE_DATE = dt.Rows[j]["DAYPROFILE_DATE"].ToString();
                        string METER_MF = dt.Rows[j]["METER_MF"].ToString();


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
                        Double PF = Convert.ToDouble(KWH_READING) / Convert.ToDouble(KVAH_READING);
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

                        string PP = "INSERT INTO [MSEDCL_ProfileConnection].[dbo].[New_Data_Profile] (NETWORKID,FORMAT_ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,YEAR,MONTH,DAY,DATE_TIME,RPH_LCURR,YPH_LCURR,BPH_LCURR,PF,INTERVAL,METER_MF) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)";

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

                        // }
                        #endregion
                    }
                    if (sq.State == ConnectionState.Open)
                    {
                        sq.Close();
                    }
                }




            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }

            #endregion
        }
        public void CreateProfileNetworkTextFile()   // Create Profile_Data NetworkText File ........
        {
            #region
            try
            {

                string connectionmeter = @"Data Source=" + txtproServer.Text +                   //Create Connection string
                        ";database=" + txtproDatabaseName.Text +
                        ";User ID=" + txtproUser.Text +
                        ";Password=" + txtpropassword.Text;
                SqlConnection sq = new SqlConnection(connectionmeter);

                // string BB = "select * from New_Data_Profile ORDER BY DAY ";
                string BB = "    select * from New_Data_Profile WHERE  DATE_TIME Like '%:00:00.%' order by NETWORKID,MONTH, DAY , INTERVAL ";
                //string BB = " select * from New_Data_Profile WHERE  DATE_TIME Like '%:00:00.%' and DATE_TIME Like '%-03-%' order by DAY , INTERVAL ";
                // string BB1 = "Select * from New_Data_Profile where DATE_TIME LIKE '%" + txtmeterfordate.Text + "%'";  /// only meter For Date Selected Query....... 
                using (SqlCommand cmd1 = new SqlCommand(BB, sq))
                {
                    string Date1 = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                    //string Date11 = "ss";
                    StreamWriter tw1 = File.AppendText(GETFILE + "\\MeterData File\\MeterData" + Date1 + ".txt");
                    try
                    {
                        int daay = 0;
                        int k = 0;
                        string PF = string.Empty;
                        string RPH_LCURR = string.Empty;
                        string YPH_LCURR = string.Empty;
                        string BPH_LCURR = string.Empty;

                        sq.Open();
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                        DataTable dttd = new DataTable();
                        dttd.Load(reader1);
                        string str = @"[PROFILE_VALUES]
FORMAT=ID,PROFILETYPE,INTERVALFORMAT,TIMEINTERVAL,GLOBALUNIT,NETWORKID,YEAR,MONTH,DAY,UNIT,PHASE,VALUES";
                        tw1.WriteLine(str);
                        for (int i = 0; i < dttd.Rows.Count; i++)
                        {
                            string onemonth = dttd.Rows[i]["MONTH"].ToString();
                            int METER_MF = Convert.ToInt32(dttd.Rows[i]["METER_MF"].ToString());
                            //if (onemonth == "February")    //  February Months Data only .......           
                            //  if (onemonth == "March")    //  March Months Data only .......              
                            //  {
                            string Day = dttd.Rows[i]["DAY"].ToString();
                            if (Day != daay.ToString())
                            {
                                while (k < 24)
                                {
                                    int n = 100;
                                    int n1 = METER_MF;

                                    string PF1 = dttd.Rows[k + i]["PF"].ToString();
                                    double PF2 = (double)n * Convert.ToDouble(PF1);
                                    PF += Convert.ToString(PF2) + ",";
                                    //PF += dttd.Rows[k + i]["PF"].ToString() + ",";


                                    string RPH_LCURR1 = dttd.Rows[k + i]["RPH_LCURR"].ToString();
                                    double RPH_LCURR2 = (double)n1 * Convert.ToDouble(RPH_LCURR1);
                                    RPH_LCURR += Convert.ToString(RPH_LCURR2) + ",";


                                    string YPH_LCURR1 = dttd.Rows[k + i]["YPH_LCURR"].ToString();
                                    double YPH_LCURR2 = (double)n1 * Convert.ToDouble(YPH_LCURR1);
                                    YPH_LCURR += Convert.ToString(YPH_LCURR2) + ",";


                                    string BPH_LCURR1 = dttd.Rows[k + i]["BPH_LCURR"].ToString();
                                    double BPH_LCURR2 = (double)n1 * Convert.ToDouble(BPH_LCURR1);
                                    BPH_LCURR += Convert.ToString(BPH_LCURR2) + ",";

                                    k++;
                                }
                                PF = PF.TrimEnd(',');
                                RPH_LCURR = RPH_LCURR.TrimEnd(',');
                                YPH_LCURR = YPH_LCURR.TrimEnd(',');
                                BPH_LCURR = BPH_LCURR.TrimEnd(',');

                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                                tw1.Write("," + "AMPPF_PF");
                                tw1.Write("," + "A");
                                tw1.Write("," + PF);

                                tw1.WriteLine();
                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                                tw1.Write("," + "AMPPF_AMP");
                                tw1.Write("," + "A");
                                tw1.Write("," + RPH_LCURR);
                                tw1.WriteLine();

                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                                tw1.Write("," + "AMPPF_PF");
                                tw1.Write("," + "B");
                                tw1.Write("," + PF);

                                tw1.WriteLine();
                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                                tw1.Write("," + "AMPPF_AMP");
                                tw1.Write("," + "B");
                                tw1.Write("," + YPH_LCURR);
                                tw1.WriteLine();

                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
                                tw1.Write("," + "AMPPF_PF");
                                tw1.Write("," + "C");
                                tw1.Write("," + PF);

                                tw1.WriteLine();
                                tw1.Write(dttd.Rows[i]["FORMAT_ID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["PROFILETYPE"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["INTERVALFORMAT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["TIMEINTERVAL"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["GLOBALUNIT"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["NETWORKID"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["YEAR"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["MONTH"].ToString().Trim());
                                tw1.Write("," + dttd.Rows[i]["DAY"].ToString().Trim());
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

                            // }
                        }
                        tw1.Close();

                        reader1.Close();
                    }
                    catch (Exception ex)
                    {
                        TechError erro = new TechError();
                        erro.ExceptionErrorHandle(GETFILE, ex.ToString());
                        tw1.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(GETFILE, ex.ToString());
            }
            #endregion
        }

        private void GISimpbox_CheckedChanged(object sender, EventArgs e)
        {
            if (GISimpbox.Checked == true)
            {
                txtGisservername.Enabled = true;
                txtGisDatabasename.Enabled = true;
                txtGisPassword.Enabled = true;
                txtGisUserid.Enabled = true;
                txt_GISschemaName.Enabled = true;
                BtnGISTestconnSql.Enabled = true;
            }
            else
            {
                txtGisservername.Enabled = false;
                txtGisDatabasename.Enabled = false;
                txtGisPassword.Enabled = false;
                txtGisUserid.Enabled = false;
                txt_GISschemaName.Enabled = false;
                BtnGISTestconnSql.Enabled = false;
            }
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtdtfilepath.Text = path;
            }
        }

        private void btnmeterfilepath_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
               txtmeterfilepath.Text = path;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnproConnection_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + txtproServer.Text +                       //Create Connection string
                         ";database=" + txtproDatabaseName.Text +
                         ";User ID=" + txtproUser.Text +
                         ";Password=" + txtpropassword.Text;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    obj.Open();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Test Connection Successfully");
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                }
            }
        }



        //peeak data
        private void checkpeak_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkpeak.Checked == true)
            {
                if (rbtnpeakdate.Checked == true)
                {
                    rbtnpeakdate.Enabled = true;
                    rbtnfordate.Enabled = true;
                    txttodatepeak.Enabled = true;
                    txtfromdatepeak.Enabled = true;
                    btntodatepeakCal.Enabled = true;
                    btnfromdateCal.Enabled = true;
                }
                if (rbtnfordate.Checked == true)
                {
                    rbtnpeakdate.Enabled = true;
                    rbtnfordate.Enabled = true;
                    txtfordate.Enabled = true;
                    btnfordateCal.Enabled = true;
                }
            }
            else
            {
                rbtnpeakdate.Enabled = false;
                rbtnfordate.Enabled = false;
                txttodatepeak.Enabled = false;
                txtfromdatepeak.Enabled = false;
                txtfordate.Enabled = false;
                btnfordateCal.Enabled = false;
                btntodatepeakCal.Enabled = false;
                btnfromdateCal.Enabled = false;
            }
        }

        private void rbtnpeakdate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkpeak.Checked == true)
            {
                if (rbtnpeakdate.Checked == true)
                {
                    txtfromdatepeak.Enabled = true;
                    txttodatepeak.Enabled = true;
                    btnfromdateCal.Enabled = true;
                    btntodatepeakCal.Enabled = true;

                    txtfordate.Enabled = false;
                    btnfordateCal.Enabled = false;

                }
            }
        }

        private void rbtnfordate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkpeak.Checked == true)
            {
                if (rbtnfordate.Checked == true)
                {
                    txtfordate.Enabled = true;
                    btnfordateCal.Enabled = true;

                    txtfromdatepeak.Enabled = false;
                    txttodatepeak.Enabled = false;
                    btnfromdateCal.Enabled = false;
                    btntodatepeakCal.Enabled = false;
                }
            }
        }

        private void btnmeterfromdate_Click_1(object sender, EventArgs e)
        {
            btn1 = true;
            Calendar1.Visible = true;
        }

        //profile data..
        private void checkmeter_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkmeter.Checked == true)
            {

                if (rbtnmeterfordate.Checked == true)
                {
                    rbtnmeterbetween.Enabled = true;
                    rbtnmeterfordate.Enabled = true;
                    txtmeterfordate.Enabled = true;
                    btnmeterfordate.Enabled = true;
                }
                if (rbtnmeterbetween.Checked == true)
                {
                    rbtnmeterbetween.Enabled = true;
                    txtmeterfromdate.Enabled = true;
                    txtmetertodate.Enabled = true;
                    btnmeterfromdate.Enabled = true;
                    btnmetertodate.Enabled = true;
                    rbtnmeterfordate.Enabled = true;
                }

            }
            else
            {
                rbtnmeterbetween.Enabled = false;
                rbtnmeterfordate.Enabled = false;
                txtmetertodate.Enabled = false;
                txtmeterfromdate.Enabled = false;
                txtmeterfordate.Enabled = false;
                btnmetertodate.Enabled = false;
                btnmeterfromdate.Enabled = false;

            }
        }

        private void rbtnmeterbetween_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkmeter.Checked == true)
            {
                if (rbtnmeterbetween.Checked == true)
                {
                    txtmeterfromdate.Enabled = true;
                    txtmetertodate.Enabled = true;
                    btnmeterfromdate.Enabled = true;
                    btnmetertodate.Enabled = true;

                    txtmeterfordate.Enabled = false;
                    btnmeterfordate.Enabled = false;
                }
            }
        }

        private void rbtnmeterfordate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkmeter.Checked == true)
            {
                if (rbtnmeterfordate.Checked == true)
                {
                    txtmeterfordate.Enabled = true;
                    btnmeterfordate.Enabled = true;

                    txtmeterfromdate.Enabled = false;
                    txtmetertodate.Enabled = false;
                    btnmeterfromdate.Enabled = false;
                    btnmetertodate.Enabled = false;
                }
            }
        }

        private void btnfromdateCal_Click_1(object sender, EventArgs e)
        {
            btn1 = true;
            Calendar.Visible = true;
        }

        private void btntodatepeakCal_Click_1(object sender, EventArgs e)
        {
            btn2 = true;
            Calendar.Visible = true;
        }

        private void btnfordateCal_Click_1(object sender, EventArgs e)
        {
            Calendar.Visible = true;
        }

        private void btnmetertodate_Click_1(object sender, EventArgs e)
        {
            btn2 = true;
            Calendar1.Visible = true;
        }

        private void btnmeterfordate_Click_1(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
        }

        private void Calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (rbtnpeakdate.Checked == true)
            {
                if (btn1 == true)
                {
                    txtfromdatepeak.Text = Calendar.SelectionStart.ToString("yyyy-MM-dd");
                    Calendar.Visible = false;
                    btn1 = false;
                }
                else
                {
                    txttodatepeak.Text = Calendar.SelectionEnd.ToString("yyyy-MM-dd");
                    Calendar.Visible = false;
                    btn2 = false;
                }

            }
            else
            {
                txtfordate.Text = Calendar.SelectionEnd.ToString("yyyy-MM-dd");
                Calendar.Visible = false;
            }

            
        }

        private void Calendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (rbtnmeterbetween.Checked == true)
            {
                if (btn1 == true)
                {

                    txtmeterfromdate.Text = Calendar1.SelectionStart.ToString("yyyy-MM-dd");

                    Calendar1.Visible = false;
                    btn1 = false;
                }
                else
                {
                    txtmetertodate.Text = Calendar1.SelectionEnd.ToString("yyyy-MM-dd");

                    Calendar1.Visible = false;
                    btn2 = false;
                }

            }
            else
            {
                txtmeterfordate.Text = Calendar1.SelectionStart.ToString("yyyy-MM-dd");

                Calendar1.Visible = false;
            }
        }
         
        public void profiledata(string GETFILE, ref string ProStatus)
        {
            if (checkmeter.Checked == true)
            {

                if (rbtnmeterbetween.Checked == true)
                {
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    ProStatus = "Reading MeterData.....";
                    string fromdate = cf.pfromdate;
                    string todate = cf.ptodate;
                    string Fordate = null;
                    MeterProfile_Data meterprofile = new MeterProfile_Data();
                    meterprofile.InsertProfileDataTable(fromdate, todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    incrementFeeder = 20;
                    completepersentag += incrementFeeder;
                }
                if (rbtnmeterfordate.Checked == true)
                {
                    incrementFeeder = 10;
                    ProStatus = "Reading MeterData.....";
                    completepersentag += incrementFeeder;
                    string fromdate = null;
                    string todate = null;
                    string Fordate = cf.Pfordate;
                    MeterProfile_Data meterprofile = new MeterProfile_Data();
                    meterprofile.InsertProfileDataTable(fromdate, todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    incrementFeeder = 20;
                    completepersentag += incrementFeeder;
                }
            }
        }

        public void Billingdata(string GETFILE, ref string ProStatus)
        {
            if (checkpeak.Checked == true)
            {
                if (rbtnpeakdate.Checked == true)
                {
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    ProStatus = "Reading Data.....";
                    Thread.Sleep(1000);
                    string fromdate = cf.Bfromdate;
                    string todate = cf.Btodate;
                    string Fordate = null;
                    MeterProfile_Data meterprofile = new MeterProfile_Data();

                    meterprofile.InsertPeakDataTable(fromdate, todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    incrementFeeder = 20;
                    completepersentag += incrementFeeder;
                }
                if (rbtnfordate.Checked == true)
                {
                    incrementFeeder = 10;
                    completepersentag += incrementFeeder;
                    string fromdate = null;
                    string todate = null;
                    string Fordate = cf.Bfordate;
                    MeterProfile_Data meterprofile = new MeterProfile_Data();
                    meterprofile.InsertPeakDataTable(fromdate, todate, Fordate, cf, GETFILE, ref incrementFeeder, ref completepersentag, ref ProStatus);
                    incrementFeeder = 20;
                    completepersentag += incrementFeeder;
                }
            }
        }

    }
}
