using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TechGration.AppCode;

namespace TechGration
{
    public partial class GIS_RECORD : Form
    {
        string GETFILE = AppDomain.CurrentDomain.BaseDirectory;
       // string GETFILE = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public GIS_RECORD()
        {
            InitializeComponent();
        }
        string projectpath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private void GIS_RECORD_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadConnection();

        }
        private void LoadData()
        {

         
            string xml_path = GETFILE + ConfigurationManager.AppSettings["XML_PATH"];
            if (File.Exists(xml_path))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(xml_path);
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "GIS_Database")
                    {
                        txtGisservername.Text = dt.Rows[0]["Server"].ToString();
                        txtGisUserid.Text = dt.Rows[0]["Username"].ToString();
                        txtGisPassword.Text = dt.Rows[0]["Password"].ToString();
                        txtGisDatabasename.Text = dt.Rows[0]["DataBase_Name"].ToString();
                        txt_GISschemaName.Text = dt.Rows[0]["Schema_Name"].ToString();
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {


            //List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(row.Cells["Chk"].Value) == true)
            //    {
            //        rows_with_checked_column.Add(row);
            //    }
            //}






            string xml_path = projectpath + ConfigurationManager.AppSettings["XML_PATH"];
            if (File.Exists(xml_path))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(xml_path);
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "GIS_Database")
                    {
                        dt.Rows[0]["Server"] = txtGisservername.Text.ToString();
                        dt.Rows[0]["Username"] = txtGisUserid.Text.ToString();
                        dt.Rows[0]["Password"] = txtGisPassword.Text.ToString();
                        dt.Rows[0]["DataBase_Name"] = txtGisDatabasename.Text.ToString();
                        dt.Rows[0]["Schema_Name"] = txt_GISschemaName.Text.ToString();
                    }
                    ds.WriteXml(xml_path);
                }

            }
            MessageBox.Show("Save Successfully");
        }
        private void LoadConnection()
        {
            ConfigFileData hh = new ConfigFileData();
            string connectionString = @"Data Source=" + txtGisservername.Text +                       //Create Connection string
                    ";database=" + txtGisDatabasename.Text +
                    ";User ID=" + txtGisUserid.Text +
                    ";Password=" + txtGisPassword.Text;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                string tablelist = "select table_name from INFORMATION_SCHEMA.TABLES  ORDER BY table_name";
                //SqlCommand com = new SqlCommand(tablelist, obj);
                obj.Open();
                SqlDataAdapter ad = new SqlDataAdapter(tablelist, obj);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                bool k = true;
                //SqlDataReader row = com.ExecuteReader();
                List<string> item = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string aa1 = "--Select--";
                    string aa = dt.Rows[i]["table_name"].ToString();
                    if (k == true)
                    {
                        item.Add(aa1);
                        k = false;
                    }

                    item.Add(aa);
                }
                combgislist.SelectedValue = "--Select--";
                combgislist.DisplayMember = item.ToString();
                combgislist.DataSource = item;
                dataGridView1.Visible = false;
                label4.Visible = false;
                obj.Close();

            }

        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            // Application.Exit();
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
                    string texx = GETFILE + "\\ConfigFile\\GISTableConfigFile.txt";
                    List<string> check = new List<string>();
                    var logFile = File.ReadAllLines(texx);
                    foreach (var s in logFile) check.Add(s);

               
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
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void combgislist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vv = combgislist.Text;
            dataGridView1.Visible = true;
            label4.Visible = true;
            if (combgislist.Text != "--Select--")
            {
                string connectionString = @"Data Source=" + txtGisservername.Text +                                // create the connection
                                       ";database=" + txtGisDatabasename.Text +
                                       ";User ID=" + txtGisUserid.Text +
                                       ";Password=" + txtGisPassword.Text;
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection obj = new SqlConnection(connectionString))
                {
                    string mtstr = "SELECT column_name FROM information_schema.columns WHERE table_name = '" + vv + "'";
                    obj.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(mtstr, obj);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    List<string> item = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string aa = dt.Rows[i]["column_name"].ToString();
                        item.Add(aa);
                    }
                    AddHeaderCheckBox();
                    HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AllowUserToAddRows = false;



                    label4.Text = "Field Count : " + dt.Rows.Count;

                }
            }


        }


        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;
        private string xmlFileCombined;
        private object xml;

        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();
            HeaderCheckBox.Size = new Size(15, 15);
            //add check ti DGV
            this.dataGridView1.Controls.Add(HeaderCheckBox);
        }


        private void btnsavetable_Click(object sender, EventArgs e)
        {

            string hh = combgislist.Text;

            string xml_path = GETFILE + "\\"+@"\ConfigFile\GIS_Table.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xml_path);
            XmlNode tab = doc.CreateElement(hh);

            if (File.Exists(xml_path))
            {
                List<string> mylist = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["chk"].Value);
                    if (isSelected)
                    {
                        string selected = row.Cells["column_name"].Value.ToString();
                        mylist.Add(selected);

                    }
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
                }
                for (int i = 0; i < mylist.Count; i++)
                {
                    string colu = mylist[i];
                    XmlNode field = doc.CreateElement(colu);
                    tab.AppendChild(field);
                    doc.DocumentElement.AppendChild(tab);
                }
                doc.Save(xml_path);
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                combgislist.Text = null;
                label4.Visible = false;
                combgislist.Text = "--Select--";
                dataGridView1.Visible = false;
            }
            MessageBox.Show("Table Save Succussfully");
        }

        bool IsHeaderCheckedboxclick = false;
        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckedboxclick = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = HCheckBox.Checked;
            }
            dataGridView1.RefreshEdit();
            IsHeaderCheckedboxclick = false;
        }
        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }
  
    }

}
