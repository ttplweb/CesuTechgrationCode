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
        //string GETFILE = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        string GETFILE = AppDomain.CurrentDomain.BaseDirectory;

        public static string Status1 = string.Empty;
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
        public static int incrementFeeder = 0;
        public static int totalpersentage = 0;
        public static int completepersentag =0;
        int countfeeder = 0;

        TreeNode node = new TreeNode();
        TreeNode node1 = new TreeNode();
        TreeNode node2 = new TreeNode();
        TreeNode node3 = new TreeNode();
        TreeNode Node = new TreeNode();
        TreeNode Node1 = new TreeNode();
        TreeNode Node2 = new TreeNode();
        TreeNode Node3 = new TreeNode();
        AutoCompleteStringCollection MyCollection;
        
        public TechgrationConfiguration()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void TechgrationConfiguration_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                LoadFeederlistOntreeview();
                txtsearch();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void LoadFeederlistOntreeview()
       {
            string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                              ";database=" + txtTGdatabasename.Text +
                              ";User ID=" + txtTGusername.Text +
                              ";Password=" + txtTGpassword.Text;

            SqlConnection conn = new SqlConnection(connectionString);

            string counter6 = "";
            string counter8 = "";
            int counter9 = 0;

            string name1 = string.Empty;
            string name2 = string.Empty;
            string name3 = string.Empty;
            string name4 = string.Empty;

            SqlCommand cmd = new SqlCommand("select distinct Circle ,Division,Name,FeederId , Cheched from TG_FEEDERLIST", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(dr);
            conn.Close();
            TotalFeedercount.Text = "Total Feeder : " + DT.Rows.Count.ToString();
            TotalFeeederCount = DT.Rows.Count.ToString();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string counter7 = DT.Rows[i][4].ToString().Trim();
                if (counter7 == "1")
                {
                    counter8 = counter7;
                    //counter8 = "1";

                }
            }

            if (counter8 == "1")
            {

                int j = 0;
                for (; j < DT.Rows.Count; j++)
                {

                    string counter = DT.Rows[j][3].ToString().Trim();
                    string counter5 = DT.Rows[j][4].ToString().Trim();

                    if (counter5 == "1")
                    {
                        counter6 = counter;
                    }
                    string Circle = DT.Rows[j]["Circle"].ToString();
                    string Division = DT.Rows[j]["Division"].ToString();
                    string Name = DT.Rows[j]["Name"].ToString();
                     string FeederId = DT.Rows[j]["FeederId"].ToString();


                    if (Circle != name1)
                    {
                        name1 = Circle;
                        node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                        treeView1.Nodes.Add(node);
                    }


                    if (Division != name2)
                    {
                        name2 = Division;
                        node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                    }

                    if (Name != name3)
                    {
                        name3 = Name;
                        node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                    }

                    if (FeederId != name4)
                    {
                        name4 = FeederId;
                        node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());

                        if (counter6 == FeederId)
                        {
                            node3.Checked = true;
                            counter9++;
                            Selectfeedercount.Text = "Selected Feeder : " + counter9.ToString();
                        }
                    }
                    treeView1.ExpandAll();
                    
                }
            }
        }

        private void LoadData()
        {
            string Configpath = GETFILE + "//ConfigFile//Configfile.xml";
            if(File.Exists(Configpath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Configpath);
                foreach(DataTable dt in ds.Tables)
                {
                    if(dt.TableName== "GIS_Database")
                    {
                        txtGisservername.Text = dt.Rows[0]["Server"].ToString();
                        txtGisUserid.Text = dt.Rows[0]["Username"].ToString();
                        txtGisPassword.Text = dt.Rows[0]["Password"].ToString();
                        txtGisDatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
                        txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();  
                        GISDBtype.Text = dt.Rows[0]["Database_Type"].ToString();
                        txtSDEpath.Text = dt.Rows[0]["SDEPath"].ToString();
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
                    else if (dt.TableName == "TechGration")
                    {
                          txtTGservername.Text = dt.Rows[0]["TG_Server"].ToString();
                          txtTGdatabasename.Text = dt.Rows[0]["TG_Database"].ToString();
                          txtTGusername.Text = dt.Rows[0]["TG_Username"].ToString();
                          txtTGpassword.Text = dt.Rows[0]["TG_Password"].ToString();
                    }
                    else if (dt.TableName == "Mdas")
                    {
                        txtMDservername.Text = dt.Rows[0]["Mdas_Server"].ToString();
                        txtMDdatabasename.Text = dt.Rows[0]["Mdas_Database"].ToString();
                        txtMDusername.Text = dt.Rows[0]["Mdas_Username"].ToString();
                        txtMDpassword.Text = dt.Rows[0]["Mdas_Password"].ToString();
                    }
                    else if (dt.TableName == "Text_Record")
                    {
                        txtConfigfile.Text = dt.Rows[0]["XML_PATH"].ToString();
                        txtFMEdirectory.Text = dt.Rows[0]["FMECYMDIST"].ToString();
                        txtCYMEdirectory.Text= dt.Rows[0]["CYMDIST"].ToString();
                        txtErrorlog.Text= dt.Rows[0]["Error_Log"].ToString();
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

                    }
                }
            }
        }

        private void txtsearch()
        {
            this.txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

            string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                              ";database=" + txtTGdatabasename.Text +
                              ";User ID=" + txtTGusername.Text +
                              ";Password=" + txtTGpassword.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand cmd = new SqlCommand("select DISTINCT FeederId from TG_FEEDERLIST", conn))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader dr1 = cmd.ExecuteReader();
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
            catch(IOException)
            {

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
            catch (IOException)
            {

            }
            return FilePath;
        }

        private void BtnFMEdirectory_Click(object sender, EventArgs e)
        {
            string path= BrowseFolder();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtFMEdirectory.Text = path;
            }
            
        }

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
            if(CYMDISTDBType.SelectedItem.ToString()=="MSAccess")
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
                        dt.Rows[0]["Server"]=txtGisservername.Text.ToString();
                        dt.Rows[0]["Username"]=txtGisUserid.Text.ToString();
                        dt.Rows[0]["Password"]=txtGisPassword.Text.ToString();
                        dt.Rows[0]["DataBase_Name"]=txtGisDatabasename.Text.ToString();
                        dt.Rows[0]["Schema_Name"] =txt_GISschemaName.Text.ToString();
                        dt.Rows[0]["Database_Type"]=GISDBtype.Text.ToString();
                        dt.Rows[0]["SDEPath"]=txtSDEpath.Text.ToString();
                    }
                    else if (dt.TableName == "Equipment_Database")
                    {

                        dt.Rows[0]["Server"]=txtEqpservername.Text.ToString();
                        dt.Rows[0]["Database"]=txtEqpDbname.Text.ToString();
                        dt.Rows[0]["Username"]=txtEqpUsername.Text.ToString();
                        dt.Rows[0]["Password"]=txtEqppassword.Text.ToString();

                    }
                    else if (dt.TableName == "Network_Database")
                    {

                        dt.Rows[0]["Net_Server"]=txtNetservername.Text.ToString();
                        dt.Rows[0]["Net_Database"]=txtNetDBname.Text.ToString();
                        dt.Rows[0]["Net_Username"]=txtNetUserid.Text.ToString();
                        dt.Rows[0]["Net_Password"]=txtNetPassword.Text.ToString();

                    }
                    else if (dt.TableName == "Mdas")
                    {
                        dt.Rows[0]["Mdas_Server"]=txtMDservername.Text.ToString();
                        dt.Rows[0]["Mdas_Database"]=txtMDdatabasename.Text.ToString();
                        dt.Rows[0]["Mdas_Username"]=txtMDusername.Text.ToString();
                        dt.Rows[0]["Mdas_Password"]=txtMDpassword.Text.ToString();
                    }
                    else if (dt.TableName == "TechGration")
                    {
                        dt.Rows[0]["TG_Server"]=txtTGservername.Text.ToString();
                        dt.Rows[0]["TG_Database"]=txtTGdatabasename.Text.ToString();
                        dt.Rows[0]["TG_Username"]=txtTGusername.Text.ToString();
                        dt.Rows[0]["TG_Password"]=txtTGpassword.Text.ToString();
                    }
                    else if (dt.TableName == "Text_Record")
                    {
                        dt.Rows[0]["XML_PATH"]=txtConfigfile.Text.ToString();
                        dt.Rows[0]["FMECYMDIST"]=txtFMEdirectory.Text.ToString();
                        dt.Rows[0]["CYMDIST"]=txtCYMEdirectory.Text.ToString();
                        dt.Rows[0]["Error_Log"]=txtErrorlog.Text.ToString();
                        dt.Rows[0]["Residential"]=txtRes.Text.ToString();
                        dt.Rows[0]["Industrial"]=txtInd.Text.ToString();
                        dt.Rows[0]["FeederProcessCount"] =runprocessfeedercount.Text.ToString();

                    }
                    else if (dt.TableName == "CymDatabaseAccess")
                    {
                        dt.Rows[0]["Network"]=txtCYMDISTnetAcces.Text.ToString();
                        dt.Rows[0]["Equipment"]=txtCYMDISTeqpAcces.Text.ToString();

                    }
                    else if (dt.TableName == "CymDatabaseType")
                    {
                        dt.Rows[0]["Type"]=CYMDISTDBType.SelectedItem.ToString();
                    }
                    else if (dt.TableName == "ExtractionType")
                    {
                        if (RbtnScheduler.Checked == true)
                        {
                            dt.Rows[0]["Type"] = "Scheduler";
                        }
                        else if (RbtnManual.Checked == true)
                        {
                            dt.Rows[0]["Type"]= "Manual";
                        }

                    }
                    ds.WriteXml(Configpath);
                }
            }
        }


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

        private void BtnSDEpath_Click(object sender, EventArgs e)
        {
            string path = BrowseFile();
            if (!string.IsNullOrWhiteSpace(path))
            {
                txtSDEpath.Text = path;
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
            if(File.Exists(txtCYMDISTnetAcces.Text.ToString()))
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
                List<String> CheckedNodes1 = CheckedNodes.Distinct().ToList();

                ManageTreeChecked1(e.Node);
                //  try
                {

                    string bb = "";
                    string dd = "";

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
                    string vv;
                    string zz;
                    string AA = "Selected Feeder : ";
                    string[] arr = bb.Split('\\');
                    string[] arr1 = dd.Split('\\');
                    string count1 = "";
                    if (bb != "")
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {

                            if (i < arr.Length - 4)
                            {
                                string tt = arr[i];
                            }
                            else
                            {
                                if (i == 3)
                                {
                                    vv = arr[i];
                                    nav1.Add(vv);

                                    List<String> Checkedlist = nav1.Distinct().ToList();
                                    string count2 = Checkedlist.Count.ToString();
                                    //N++;
                                    count1 = Checkedlist.Count.ToString();
                                    Selectfeedercount.Text = AA + count2.ToString();
                                    //label6.Text = "Uncheck Checkbox : " + (int.Parse(count) - (int.Parse(count1))).ToString();
                                }
                            }
                        }
                    }
                    else
                    {

                        for (int j = 0; j < arr1.Length; j++)
                        {

                            if (j < arr1.Length - 4)
                            {
                                string ww = arr1[j];
                            }
                            else
                            {
                                if (j == 3)
                                {
                                    zz = arr1[j];
                                    while (nav1.Contains(zz))
                                    {
                                        nav1.Remove(zz);
                                    }

                                    List<String> Checkedlist = nav1.Distinct().ToList();
                                    // Checkedlist.Remove(zz);

                                    count1 = Checkedlist.Count.ToString();
                                    Selectfeedercount.Text = AA + count1;
                                    //label6.Text = "Uncheck Checkbox : " + (int.Parse(count) - (int.Parse(count1))).ToString();

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
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
                    string filepath = GETFILE + "\\Feeder\\SchedulerFeederList.txt";
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

                }
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
                int j = Convert.ToInt32(runprocessfeedercount.Text.ToString());
                btnTreeView1Ok();
                LoadDataConfig();
                SelectedFeederList SFD = new SelectedFeederList();
                DataTable Dt = new DataTable();
                Dt = SFD.GetFeederTable(cf.TGservername, cf.TGdatabase, cf.TGusername, cf.TGpassword);
                int TotalCount = Dt.Rows.Count;
                SelectedFeederCount = Convert.ToString(TotalCount);
                totalpersentage = TotalCount * 100;
                ErrorLog aa = new ErrorLog();
                string Logpath = aa.fir(cf.Errorlog, Dt.Rows.Count.ToString());
                
                this.Close();                                                           // Close Current window

                if (j == 1)
                {
                    th = new Thread(opennewform1);
                    th.SetApartmentState(ApartmentState.MTA);
                    th.Start();
                    for (int i = 0; i < Dt.Rows.Count; i = i + j)
                    {
                        RunProcessFeeder1("Feeder2", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString());
                        aa.combineErrorLog(Logpath, GETFILE, 1);
                    }
                }
                
                if (j == 2)
                {
                    
                    int k = j;
                    int first = 1;
                    th = new Thread(opennewform2);
                    th.SetApartmentState(ApartmentState.MTA);
                    th.Start();
                    for (int i = 0; i < Dt.Rows.Count; i = i + k)
                    { 
                        if(first==1)
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

                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2))
                            {
                                Thread.Sleep(1000);
                            }

                            if (i+1 == (Dt.Rows.Count-1))
                            {
                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                {
                                    Thread.Sleep(1000);
                                }
                            }

                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                            first = 2;
                        }
                        else
                        {
                            k = 0;
                            int a = 0;
                            int b = 0;
                            if (IsRunningProcess1 == false)
                            {
                                Thread trd = new Thread(new ThreadStart(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString())));
                                trd.IsBackground = true;
                                trd.Start();
                                a = 1;
                            }
                            if (IsRunningProcess2 == false)
                            {

                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                trd1.IsBackground = true;
                                trd1.Start();
                                b = 1;
                            }
                            k = a + b;

                            if (i+k == (Dt.Rows.Count))
                            {
                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2))
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                            while (!((Status1 == "Feeder Completed")&& IsRunningProcess1) && !((Status2 == "Feeder Completed")&&IsRunningProcess2))
                            {
                                Thread.Sleep(1000);
                            }
                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                        }
                        //k = 0;
                        //Thread threadFeeder1 = new Thread(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString()));
                        //Thread threadFeeder2 = new Thread(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
                        //threadFeeder1.SetApartmentState(ApartmentState.MTA);
                        //threadFeeder1.Start();
                        //threadFeeder2.SetApartmentState(ApartmentState.MTA);
                        //threadFeeder2.Start();

                        
                       
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

                            if ((Status1 == "Feeder Completed") && IsRunningProcess1 )
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                            if ((Status3 == "Feeder Completed") && IsRunningProcess3 )
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 3);
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
                            
                            if (IsRunningProcess2 == false && ((i+a) < Dt.Rows.Count))
                            {

                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                trd2.IsBackground = true;
                                trd2.Start();
                                b = 1;
                            }
                            
                            if (IsRunningProcess3 == false && ((i+a+b) < Dt.Rows.Count))
                            {

                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                trd1.IsBackground = true;
                                trd1.Start();
                                c = 1;
                            }

                            k = a + b+c;

                            if (i + k >= (Dt.Rows.Count -1))
                            {
                                while (!((Status1 == "Feeder Completed")) || !((Status2 == "Feeder Completed") ) || !((Status3 == "Feeder Completed")))
                                {
                                    Status4 = "if condition K" + k.ToString() + "a,b,c=" + a.ToString() + "," + b.ToString() + "," + c.ToString() + " run=" + IsRunningProcess1.ToString() + IsRunningProcess2.ToString() + IsRunningProcess3.ToString();
                                    Thread.Sleep(1000);
                                }
                            }
                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3))
                            {
                                Status4 = "else condition K"+k.ToString()+"a,b,c="+a.ToString()+","+b.ToString()+","+c.ToString()+" run="+IsRunningProcess1.ToString()+IsRunningProcess2.ToString()+IsRunningProcess3.ToString();
                                Thread.Sleep(1000);
                            }

                            
                            if ((Status1 == "Feeder Completed") && IsRunningProcess1 )
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2 )
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                            if ((Status3 == "Feeder Completed") && IsRunningProcess3 )
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 3);
                                IsRunningProcess3 = false;

                            }
                        }
                        
                    }
                }
                if (j == 4)
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
                                Status2 = "Reading GIS";
                                Thread trd4 = new Thread(new ThreadStart(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + 3]["FeederId"].ToString(), Dt.Rows[i + 3]["Division"].ToString(), Dt.Rows[i + 3]["Name"].ToString(), Dt.Rows[i + 3]["FeederName"].ToString())));
                                trd4.IsBackground = true;
                                trd4.Start();
                            }

                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                            {
                                Thread.Sleep(1000);
                            }

                            if (i + 1 == (Dt.Rows.Count - 1))
                            {
                                while (!((Status1 == "Feeder Completed") && IsRunningProcess1) || !((Status2 == "Feeder Completed") && IsRunningProcess2) || !((Status3 == "Feeder Completed") && IsRunningProcess3) || !((Status4 == "Feeder Completed") && IsRunningProcess4))
                                {
                                    Thread.Sleep(1000);
                                }
                            }

                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 3);
                                IsRunningProcess3 = false;

                            }
                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 4);
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
                            if (IsRunningProcess2 == false && (i+a) < Dt.Rows.Count)
                            {

                                Thread trd2 = new Thread(new ThreadStart(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + a]["FeederId"].ToString(), Dt.Rows[i + a]["Division"].ToString(), Dt.Rows[i + a]["Name"].ToString(), Dt.Rows[i + a]["FeederName"].ToString())));
                                trd2.IsBackground = true;
                                trd2.Start();
                                b = 1;
                            }
                            if (IsRunningProcess3 == false && (i + a+b) < Dt.Rows.Count)
                            {

                                Thread trd1 = new Thread(new ThreadStart(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + a + b]["FeederId"].ToString(), Dt.Rows[i + a + b]["Division"].ToString(), Dt.Rows[i + a + b]["Name"].ToString(), Dt.Rows[i + a + b]["FeederName"].ToString())));
                                trd1.IsBackground = true;
                                trd1.Start();
                                c = 1;
                            }
                            if (IsRunningProcess4 == false && (i + a+b+c) < Dt.Rows.Count)
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
                                    Thread.Sleep(1000);
                                }
                            }
                            while (!((Status1 == "Feeder Completed") && IsRunningProcess1) && !((Status2 == "Feeder Completed") && IsRunningProcess2) && !((Status3 == "Feeder Completed") && IsRunningProcess3) && !((Status4 == "Feeder Completed") && IsRunningProcess4))
                            {
                                Thread.Sleep(1000);
                            }
                            if ((Status1 == "Feeder Completed") && IsRunningProcess1)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 1);
                                IsRunningProcess1 = false;

                            }
                            if ((Status2 == "Feeder Completed") && IsRunningProcess2)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 2);
                                IsRunningProcess2 = false;

                            }
                            if ((Status3 == "Feeder Completed") && IsRunningProcess3)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 3);
                                IsRunningProcess3 = false;

                            }
                            if ((Status4 == "Feeder Completed") && IsRunningProcess4)
                            {
                                aa.combineErrorLog(Logpath, GETFILE, 4);
                                IsRunningProcess4 = false;

                            }
                        }
                        
                    }
                }
                
                TechgrationProcess tp = new TechgrationProcess();
                tp.setValue();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

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

        bool IsRunningProcess1=false;
        bool IsRunningProcess2=false;
        bool IsRunningProcess3=false;
        bool IsRunningProcess4=false;

        private void RunProcessFeeder1(string usertype, string FeederId, string Division, string Name, string FeederName)
        {
            try 
            {
                IsRunningProcess1 = true;
                Status1 = "Reading GIS";
                FeederID1 = FeederId;
                Division1 = Division;
                Substation1 = Name;
                FeederName1 = FeederName;
                
                string NEWGETFILE = GETFILE + "\\" + usertype;

                ErrorLog Error = new ErrorLog();
                string LogPath = NEWGETFILE + "\\ErrorLog\\FeederErrorlog.log";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }
                DeleteOldText(NEWGETFILE);
                Error.nav(LogPath, FeederId);
                FMEFileCreate FMEfileccreate = new FMEFileCreate();
                FMEfileccreate.Filecreate(GETFILE, usertype, FeederId, cf);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];
                while (!File.Exists(con))
                {
                    Status1 = "File not created";
                    Thread.Sleep(100);
                }
                Cursor.Current = Cursors.WaitCursor;
                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                string get = Error.circuit(LogPath, conn);

                if (get == "1")
                {
                    Status1 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR
";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Error.cir(LogPath, conn);

                Error.cab(LogPath, conn);
                Error.fuse(LogPath, conn);
                Error.dt(LogPath, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);
                
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ConsumerNo cut = new ConsumerNo();                                              //this class is use for CESU AND MDB file connection then data retrieve .  
                string vaule = cut.sql(cf, NEWGETFILE);        //done nishant
                Replace nn0 = new Replace();                                            //this class is used for MDB File
                nn0.Rp(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);

                updateFromToNode up = new updateFromToNode();
                //up.setFromToNodeId(FeederId, conn);
                
                updatedata(conn);
                
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                SetAllLocation setL = new SetAllLocation();
                setL.setAllDeviceLocation(conn);
                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                MeterDemand met = new MeterDemand();
                met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                if (get == "1")
                {
                    Status1 = "Upload Into Cyme Database";
                }
                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string Net2 = NEWGETFILE + "\\CYMEimpFile";
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = Net2;
                proc2.Start();
                proc2.WaitForExit();
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Batch ma = new Batch();
                int errorfeeder = Convert.ToInt32(ma.Main(LogPath, GETFILE, FeederId));
                //int Errorcount = (Convert.ToInt32(count1) - errorfeeder).ToString();
                Error.End(LogPath);
                Status1 = "Feeder Completed";
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                
                
                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();
                IsRunningProcess1 = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void RunProcessFeeder2(string usertype, string FeederId, string Division, string Name, string FeederName)
        {
            try
            {
                IsRunningProcess2 = true;
                Status2 = "Reading GIS";
                FeederID2 = FeederId;
                Division2 = Division;
                Substation2 = Name;
                FeederName2 = FeederName;
                
                string NEWGETFILE = GETFILE + "\\" + usertype;
                ErrorLog Error = new ErrorLog();
                string LogPath = NEWGETFILE + "\\ErrorLog\\FeederErrorlog.log";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }
                DeleteOldText(NEWGETFILE);
                Error.nav(LogPath, FeederId);
                FMEFileCreate FMEfileccreate = new FMEFileCreate();
                FMEfileccreate.Filecreate(GETFILE, usertype, FeederId, cf);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];
                while (!File.Exists(con))
                {
                    Status2 = "File not created";
                    Thread.Sleep(100);
                }
                Cursor.Current = Cursors.WaitCursor;
                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                string get = Error.circuit(LogPath, conn);

                if (get == "1")
                {
                    Status2 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR
";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Error.cir(LogPath, conn);

                Error.cab(LogPath, conn);
                Error.fuse(LogPath, conn);
                Error.dt(LogPath, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ConsumerNo cut = new ConsumerNo();                                              //this class is use for CESU AND MDB file connection then data retrieve .  
                string vaule = cut.sql(cf, NEWGETFILE);        //done nishant
                Replace nn0 = new Replace();                                            //this class is used for MDB File
                nn0.Rp(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);
                updateFromToNode up = new updateFromToNode();
                //up.setFromToNodeId(FeederId, conn);
                updatedata(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                SetAllLocation setL = new SetAllLocation();
                setL.setAllDeviceLocation(conn);
                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                MeterDemand met = new MeterDemand();
                met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                if (get == "1")
                {
                    Status2 = "Upload Into Cyme Database";
                }
                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string Net2 = NEWGETFILE + "\\CYMEimpFile";
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = Net2;
                proc2.Start();
                proc2.WaitForExit();
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Batch ma = new Batch();
                int errorfeeder = Convert.ToInt32(ma.Main(LogPath, GETFILE, FeederId));
                //int Errorcount = (Convert.ToInt32(count1) - errorfeeder).ToString();
                Error.End(LogPath);
                Status2 = "Feeder Completed";
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                
                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();
                IsRunningProcess2 = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            
        }

        private void RunProcessFeeder3(string usertype, string FeederId, string Division, string Name, string FeederName)
        {
            try
            {

                IsRunningProcess3 = true;
                Status3 = "Reading GIS";
                FeederID3 = FeederId;
                Division3 = Division;
                Substation3 = Name;
                FeederName3 = FeederName;
                string NEWGETFILE = GETFILE + "\\" + usertype;
                ErrorLog Error = new ErrorLog();
                string LogPath = NEWGETFILE + "\\ErrorLog\\FeederErrorlog.log";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }
                DeleteOldText(NEWGETFILE);
                Error.nav(LogPath, FeederId);
                FMEFileCreate FMEfileccreate = new FMEFileCreate();
                FMEfileccreate.Filecreate(GETFILE, usertype, FeederId, cf);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];
                while (!File.Exists(con))
                {
                    Status3 = "File not created";
                    Thread.Sleep(100);
                }
                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                string get = Error.circuit(LogPath, conn);

                if (get == "1")
                {
                    Status3 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR
";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Error.cir(LogPath, conn);

                Error.cab(LogPath, conn);
                Error.fuse(LogPath, conn);
                Error.dt(LogPath, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ConsumerNo cut = new ConsumerNo();                                              //this class is use for CESU AND MDB file connection then data retrieve .  
                string vaule = cut.sql(cf, NEWGETFILE);        //done nishant
                Replace nn0 = new Replace();                                            //this class is used for MDB File
                nn0.Rp(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);
                updateFromToNode up = new updateFromToNode();
                //up.setFromToNodeId(FeederId, conn);
                updatedata(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                SetAllLocation setL = new SetAllLocation();
                //setL.setAllDeviceLocation(conn);
                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                MeterDemand met = new MeterDemand();
                met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                if (get == "1")
                {
                    Status3 = "Upload Into Cyme Database";
                }
                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string Net2 = NEWGETFILE + "\\CYMEimpFile";
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = Net2;
                proc2.Start();
                proc2.WaitForExit();
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Batch ma = new Batch();
                int errorfeeder = Convert.ToInt32(ma.Main(LogPath, GETFILE, FeederId));
                //int Errorcount = (Convert.ToInt32(count1) - errorfeeder).ToString();
                Error.End(LogPath);
                Status3 = "Feeder Completed";
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                
                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();
                IsRunningProcess3 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RunProcessFeeder4(string usertype, string FeederId, string Division, string Name, string FeederName)
        {
            try
            {
                IsRunningProcess4 = true;
                Status4 = "Reading GIS";
                FeederID4 = FeederId;
                Division4 = Division;
                Substation4 = Name;
                FeederName4 = FeederName;
                
                string NEWGETFILE = GETFILE + "\\" + usertype;
                ErrorLog Error = new ErrorLog();
                string LogPath = NEWGETFILE + "\\ErrorLog\\FeederErrorlog.log";
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }
                DeleteOldText(NEWGETFILE);
                Error.nav(LogPath, FeederId);
                FMEFileCreate FMEfileccreate = new FMEFileCreate();
                FMEfileccreate.Filecreate(GETFILE, usertype, FeederId, cf);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                string con = NEWGETFILE + ConfigurationManager.AppSettings["connection"];
                while (!File.Exists(con))
                {
                    Status4 = "File not created";
                    Thread.Sleep(100);
                }
                string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + con;
                OleDbConnection conn = new OleDbConnection(connString);
                conn.Open();
                string get = Error.circuit(LogPath, conn);

                if (get == "1")
                {
                    Status4 = "Transforming GIS Data into Cyme model";
                }
                else
                {
                    StreamWriter write = File.AppendText(LogPath);
                    string strwrite = @"Data Not Available in SWITCHGEAR
";
                    write.Write(strwrite);
                    write.Close();
                }
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Error.cir(LogPath, conn);

                Error.cab(LogPath, conn);
                Error.fuse(LogPath, conn);
                Error.dt(LogPath, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                HeadNode head = new HeadNode();
                string getsourcenode = head.net(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                ConsumerNo cut = new ConsumerNo();                                              //this class is use for CESU AND MDB file connection then data retrieve .  
                string vaule = cut.sql(cf, NEWGETFILE);        //done nishant
                Replace nn0 = new Replace();                                            //this class is used for MDB File
                nn0.Rp(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Common nn2 = new Common();                                             // this class is use for string value set for textfile
                nn2.com(NEWGETFILE);
                updateFromToNode up = new updateFromToNode();
                //up.setFromToNodeId(FeederId, conn);
                updatedata(conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                SetAllLocation setL = new SetAllLocation();
                setL.setAllDeviceLocation(conn);
                Networktxt nn = new Networktxt();                                     // this class is use for Generate the NetworkFile
                nn.main(NEWGETFILE, conn);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Loadtxt nn1 = new Loadtxt();                                          // this class is use for Generate the loadFile
                nn1.load(NEWGETFILE, conn);
                MeterDemand met = new MeterDemand();
                met.meter(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword, NEWGETFILE);
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                if (get == "1")
                {
                    Status4 = "Upload Into Cyme Database";
                }
                CymeFileCreate cyme = new CymeFileCreate();
                cyme.savebuttonwork(NEWGETFILE, GETFILE, cf);
                string str_Path2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                string Net2 = NEWGETFILE + "\\CYMEimpFile";
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.StartInfo.CreateNoWindow = true;
                proc2.StartInfo.FileName = str_Path2;
                proc2.StartInfo.WorkingDirectory = Net2;
                proc2.Start();
                proc2.WaitForExit();
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                Batch ma = new Batch();
                int errorfeeder = Convert.ToInt32(ma.Main(LogPath, GETFILE, FeederId));
                //int Errorcount = (Convert.ToInt32(count1) - errorfeeder).ToString();
                Error.End(LogPath);
                Status4 = "Feeder Completed";
                incrementFeeder = 10;
                completepersentag += incrementFeeder;
                
                countfeeder++;
                CompleteFeederCount = countfeeder.ToString();
                IsRunningProcess4 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        cf.CymeDatabase = dt.Rows[0]["DataBase_Name"].ToString();
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
                        cf.Gisservername = dt.Rows[0]["XML_PATH"].ToString();
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

            }

        }

        public void btnTreeView1Ok()
        {
            string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                    ";database=" + txtTGdatabasename.Text +
                    ";User ID=" + txtTGusername.Text +
                    ";Password=" + txtTGpassword.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string aa = "";
            string bb = "";

            SqlCommand cmd1 = new SqlCommand("update TG_FEEDERLIST set Cheched='0'", conn);
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
                cmd1 = new SqlCommand("update TG_FEEDERLIST set Cheched='1' where FeederId in (" + bb + ")", conn);
                cmd1.ExecuteNonQuery();
                // MessageBox.Show("Selected Feeder Saved");

            }
            catch (Exception ex)
            {
            }
        }

        private void BtnGISTestconnSql_Click(object sender, EventArgs e)
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
                    List<string> check = new List<string>();
                    check.Add("HTCABLE");
                    check.Add("LTCABLE");
                    check.Add("HTCONDUCTOR");
                    check.Add("LTCONDUCTOR");
                    check.Add("DISTRIBUTIONTRANSFORMER");
                    check.Add("SWITCHGEAR");
                    check.Add("SWITCH");
                    check.Add("FUSE");
                    check.Add("SHUNTCAPACITOR");
                    check.Add("BUSBAR");
                    check.Add("HTSERVICEPOINT");
                    check.Add("LTSERVICEPOINT");
                    check.Add("CIRCUITSOURCE");
                    check.Add("CONSUMERINFO");
                    check.Add("CIRCUITSOURCE");
                    check.Add("SUBSTATION");
                    check.Add("SWITCHGEAR");
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
                            tab += "\r\n" + check[i] + " Not Found in GIS Schema(" + txt_GISschemaName.Text + ")";
                        }
                        obj.Close();
                    }

                    if(!string.IsNullOrWhiteSpace(tab)) //(tab != null)
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
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void BtnSDEtestcon_Click(object sender, EventArgs e)
        {
            if(File.Exists(txtSDEpath.Text))
            {
                MessageBox.Show("Test Connection Successfully");
            }
            else
            {
                MessageBox.Show("Test Connection Failed");
            }
        }

        string search = "";

        private void BtnFeederSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                        ";database=" + txtTGdatabasename.Text +
                        ";User ID=" + txtTGusername.Text +
                        ";Password=" + txtTGpassword.Text;

                SqlConnection conn = new SqlConnection(connectionString);
                treeView1.Nodes.Clear();
                //nav1.Clear();
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

                SqlCommand cmd = new SqlCommand("select distinct Circle ,Division,Name,FeederId , Cheched from TG_FEEDERLIST ", conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable DT = new DataTable();
                DT.Load(dr);

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    TotalFeeederCount = DT.Rows.Count.ToString();
                    string counter7 = DT.Rows[i][4].ToString().Trim();
                    if (counter7 == "1")
                    {
                        counter8 = counter7;
                    }
                }

                //  if (counter8 == "1")
                {

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


                        if (Circle != name1)
                        {
                            name1 = Circle;
                            Node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            treeView1.Nodes.Add(Node);
                            if (search.ToLower() == Circle.ToLower())
                            {
                                treeView1.SelectedNode = Node;
                                treeView1.Focus();
                            }
                        }


                        if (Division != name2)
                        {
                            name2 = Division;
                            Node1 = Node.Nodes.Add(DT.Rows[j]["Division"].ToString());
                            if (search.ToLower() == Division.ToLower())
                            {
                                treeView1.SelectedNode = Node1;
                                treeView1.Focus();
                            }
                        }

                        if (Name != name3)
                        {
                            name3 = Name;
                            Node2 = Node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                            if (search.ToLower() == Name.ToLower())
                            {
                                treeView1.SelectedNode = Node2;
                                treeView1.Focus();
                            }
                        }

                        if (FeederId1 != name4)
                        {
                            name4 = FeederId1;
                            Node3 = Node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());

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

                    TotalFeedercount.Text = "Total FeederID : " + TotalFeedercount;

                }
            }
            catch (Exception ex)
            { }
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
            { }
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
            { }
        }

        Thread loadingThread;
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
            catch
            {
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
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnFeederImport_Click(object sender, EventArgs e)
        {
            try
            {

                string connectionString = @"Data Source=" + txtTGservername.Text +                                // create the connection
                           ";database=" + txtTGdatabasename.Text +
                           ";User ID=" + txtTGusername.Text +
                           ";Password=" + txtTGpassword.Text;
                SqlConnection conn = new SqlConnection(connectionString);

                if (RbtnFeederImport.Checked)
                {
                    ShowLoadingGif();
                    nav1.Clear();
                    treeView1.Nodes.Clear();



                    treeView1.Visible = true;



                    // Cursor.Current = Cursors.WaitCursor;
                    //label1.Visible = false;
                    //label2.Visible = true;
                    //label3.Visible = false;

                    //label4.Visible = false;
                    //// label5.Visible = false;
                    //label6.Visible = false;




                    string Source = string.Empty;
                    string SourceFile = string.Empty;
                    string file = string.Empty;
                    string d = GETFILE.ToString() + "\\Feeder";
                    string c = d.Replace(@"\", @"/");

                    file = GETFILE + "\\Feeder\\FME\\FeederList";                 // Old file Delete 
                    
                    string[] filedelete = System.IO.Directory.GetFiles(file, "*.fmw");
                    foreach (string item in filedelete)
                    {
                        System.IO.File.Delete(item);
                    }

                    string batch1 = file + "\\FeederList.bat";
                    if (File.Exists(batch1))
                    {
                        File.Delete(batch1);
                    }

                    string batch = GETFILE + "\\Feeder\\FME\\FeederList.bat";
                    if (File.Exists(batch))
                    {
                        File.Copy(batch, file + "/" + Path.GetFileName(batch));
                    }
                    string tcl0 = file + "\\FeederList.tcl";
                    if (File.Exists(tcl0))
                    {
                        File.Delete(tcl0);
                    }

                    string tcl = GETFILE + "\\Feeder\\FME\\FeederList.tcl";
                    File.Copy(tcl, file + "/" + Path.GetFileName(tcl));


                    string tcl1 = File.ReadAllText(tcl0);
                    tcl1 = tcl1.Replace("@feeder@", c);
                    File.WriteAllText(tcl0, tcl1);

                    string getbat = File.ReadAllText(batch1);
                    getbat = getbat.Replace("@feederlist@", GETFILE+"\\Feeder");
                    getbat = getbat.Replace("@feeder@", txtFMEdirectory.Text);
                    File.WriteAllText(batch1, getbat);

                    string bb = GETFILE + "\\Feeder\\FME\\FeederList.fmw";                  // get the fme file

                    string TargetFile = file + "\\FeederList.fmw";
                    File.Copy(bb, file + "/" + Path.GetFileName(bb));                                            // copy the fme file

                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("cyme@12345", txtGisPassword.Text);
                    text = text.Replace("@user@", txtGisUserid.Text);
                    text = text.Replace("@server@", txtGisservername.Text);
                    //text = text.Replace("@instance@", txtgis.Text);
                    //text = text.Replace("SDE.DEFAULT", Cyme_gis_Versioncombo.Text);
                    text = text.Replace("GISDBNAME", txtGisDatabasename.Text);
                    text = text.Replace("override_schema", txt_GISschemaName.Text);
                    text = text.Replace("@sqldatabase@", txtTGdatabasename.Text);
                    text = text.Replace("@sqlserver@", txtTGservername.Text);
                    text = text.Replace("@sqluser@", txtTGusername.Text);
                    text = text.Replace("@sqlpassword@", txtTGpassword.Text);
                    File.WriteAllText(TargetFile, text);

                    string str_Path = GETFILE + "\\Feeder\\FME\\FeederList\\FeederList.bat";
                    string Net = GETFILE + "\\Feeder\\FME\\FeederList";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.FileName = str_Path;
                    proc.StartInfo.WorkingDirectory = Net;
                    proc.Start();
                    proc.WaitForExit();

                    //nav.Clear();

                    CheckedNodes.Clear();

                    string name1 = string.Empty;
                    string name2 = string.Empty;
                    string name3 = string.Empty;
                    string name4 = string.Empty;

                    SqlCommand cmd = new SqlCommand("select distinct Circle ,Division,Name,FeederId from TG_FEEDERLIST ", conn);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    //con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable DT = new DataTable();
                    DT.Load(dr);
                    //dataGridView1.DataSource = DT;

                    int j = 0;
                    for (; j < DT.Rows.Count; j++)
                    {

                        string counter = DT.Rows[j][3].ToString();
                        string value = DT.Rows.Count.ToString();

                        label2.Text = "Total Feeder : " + value;

                        string Circle = DT.Rows[j]["Circle"].ToString();
                        string Division = DT.Rows[j]["Division"].ToString();
                        string Name = DT.Rows[j]["Name"].ToString();
                        string FeederId = DT.Rows[j]["FeederId"].ToString();


                        if (Circle != name1)
                        {
                            name1 = Circle;
                            node = new TreeNode(DT.Rows[j]["Circle"].ToString());
                            treeView1.Nodes.Add(node);
                        }


                        if (Division != name2)
                        {

                            name2 = Division;
                            node1 = node.Nodes.Add(DT.Rows[j]["Division"].ToString());

                        }

                        if (Name != name3)
                        {
                            name3 = Name;
                            node2 = node1.Nodes.Add(DT.Rows[j]["Name"].ToString());
                        }

                        if (FeederId != name4)
                        {
                            name4 = FeederId;
                            node3 = node2.Nodes.Add(DT.Rows[j]["FeederId"].ToString());
                        }
                        Selectfeedercount.Text = "Selected Feeder : 0";
                        //label5.Text = "Selected Feeder : 0";
                    }
                    treeView1.ExpandAll();
                    treeView1.Nodes[0].EnsureVisible();
                    HideLoadingGif();
                }

                else
                {
                    MessageBox.Show("Please Select scope of import");
                }

            }
            catch (Exception ex)
            { }
        }

        private void BtnTGtestcon_Click(object sender, EventArgs e)
        {
            #region
            
            string connectionString = @"Data Source=" + txtTGservername.Text +                       //Create Connection string
                           ";database=" + txtTGdatabasename.Text +
                           ";User ID=" + txtTGusername.Text +
                           ";Password=" + txtTGpassword.Text;

            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {

                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand cmd = new SqlCommand("select ToP 1 * from TG_FEEDERLIST", obj);
                    obj.Open();
                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        MessageBox.Show("Test Connection Successful");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Table TG_FEEDERLIST Not Found in Database");
                    }
                    Cursor.Current = Cursors.Default;
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                }
            }
            #endregion
        }

        private void BtnMDtestcon_Click(object sender, EventArgs e)
        {
            string mdas = string.Empty;
            string connectionString = @"Data Source=" + txtMDservername.Text +                       //Create Connection string
                           ";database=" + txtMDdatabasename.Text +
                           ";User ID=" + txtMDusername.Text +
                           ";Password=" + txtMDpassword.Text;

            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {

                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand cmd = new SqlCommand("select ToP 1 * from ST_MDAS_NA_DAILY_PEAK", obj);
                    obj.Open();
                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        MessageBox.Show("Test Connection Successfully");
                    }
                    catch (Exception ex)
                    {
                        mdas += "\r\nST_MDAS_NA_DAILY_PEAK  Not Found in Database";
                        MessageBox.Show(mdas);
                    }
                    Cursor.Current = Cursors.Default;
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed");
                }
            }
        }
    }
}
