using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechGration.AppCode;

namespace TechGration
{
    public partial class TechgrationProcess : Form
    {
        
        public TechgrationProcess()
        {
            InitializeComponent();
        }
        
        private void TechgrationProcess_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.Refresh();
            feed();
           // savewebstatus();
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public int persentage()
        {
            int per=0;
            int a = TechgrationConfiguration.completepersentag;
            int b = TechgrationConfiguration.totalpersentage;
            per =(((a * 100) /b));
            return per;
        }

        public string feed()
        {
            try
            {
                lblTotalfeeder.Text = "Total Feeder : " + TechgrationConfiguration.TotalFeeederCount;
                lblTotalfeeder.Update();
                lblTotalfeeder.Refresh();
                lblselectedfeeder.Text = "Selected Feeder : " + TechgrationConfiguration.SelectedFeederCount;
                lblselectedfeeder.Update();
                lblselectedfeeder.Refresh();
                //labFeederID.Text = Form1.count2;
                if (TechgrationConfiguration.CompleteFeederCount == ""||(string.IsNullOrEmpty(TechgrationConfiguration.CompleteFeederCount)))
                {
                    lblcompletefeeder.Text = "Completed Feeder : 0";
                }
                else
                {
                    lblcompletefeeder.Text = "Completed Feeder : " + TechgrationConfiguration.CompleteFeederCount;
                }
                lblcompletefeeder.Update();
                lblcompletefeeder.Refresh();
                lblStatus1.Text = TechgrationConfiguration.Status1;
                lblStatus2.Text = TechgrationConfiguration.Status2;
                labdivision1.Text = "Division        :   " + "HPC";
                labdivision2.Text = "Division        :   " + "HPC";
                labSubstation1.Text = "Substation    :   " + TechgrationConfiguration.Substation1;
                labSubstation2.Text = "Substation    :   " + TechgrationConfiguration.Substation2;
                labFeederID1.Text = "FeederID      :   " + TechgrationConfiguration.FeederID1 ;
                labFeederID2.Text = "FeederID      :   " + TechgrationConfiguration.FeederID2 ;
                labFeederName1.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName1;
                labFeederName2.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName2;

                persentage();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Missing");
            }
            return ("1");
        }

        public void setValue()
        {
            this.Close();
            Application.Exit();
            Environment.Exit(Environment.ExitCode);
        }

        //private void savewebstatus()
        //{
        //    string configWebfilepath = TechgrationConfiguration.techgrationPathforweb + "//ConfigFile//ConfigfileForWebStatus.xml";
        //    if (File.Exists(configWebfilepath))
        //    {
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(configWebfilepath);
        //        foreach (DataTable dt in ds.Tables)
        //        {
        //            if (dt.TableName == "RunProcess1")
        //            {
        //                dt.Rows[0]["Status"] = TechgrationConfiguration.Status1;
        //                dt.Rows[0]["FeederID"] = TechgrationConfiguration.FeederID1;
        //                dt.Rows[0]["FeederName"] = TechgrationConfiguration.FeederName1;
        //                dt.Rows[0]["Division"] = TechgrationConfiguration.Division1;
        //                dt.Rows[0]["Substation"] = TechgrationConfiguration.Substation1;
        //            }
        //            if (dt.TableName == "RunProcess2")
        //            {
        //                dt.Rows[0]["Status"] = TechgrationConfiguration.Status2;
        //                dt.Rows[0]["FeederID"] = TechgrationConfiguration.FeederID2;
        //                dt.Rows[0]["FeederName"] = TechgrationConfiguration.FeederName2;
        //                dt.Rows[0]["Division"] = TechgrationConfiguration.Division2;
        //                dt.Rows[0]["Substation"] = TechgrationConfiguration.Substation2;
        //            }
        //            if (dt.TableName == "RunProcess3")
        //            {
        //                dt.Rows[0]["Status"] = TechgrationConfiguration.Status3;
        //                dt.Rows[0]["FeederID"] = TechgrationConfiguration.FeederID3;
        //                dt.Rows[0]["FeederName"] = TechgrationConfiguration.FeederName3;
        //                dt.Rows[0]["Division"] = TechgrationConfiguration.Division3;
        //                dt.Rows[0]["Substation"] = TechgrationConfiguration.Substation3;
        //            }
        //            if (dt.TableName == "RunProcess4")
        //            {
        //                dt.Rows[0]["Status"] = TechgrationConfiguration.Status4;
        //                dt.Rows[0]["FeederID"] = TechgrationConfiguration.FeederID4;
        //                dt.Rows[0]["FeederName"] = TechgrationConfiguration.FeederName4;
        //                dt.Rows[0]["Division"] = TechgrationConfiguration.Division4;
        //                dt.Rows[0]["Substation"] = TechgrationConfiguration.Substation4;
        //            }
        //            if (dt.TableName == "TechGration")
        //            {
        //                dt.Rows[0]["TotalFeeder"] = TechgrationConfiguration.TotalFeeederCount;
        //                dt.Rows[0]["SelectedFeeder"] = TechgrationConfiguration.SelectedFeederCount;
        //                dt.Rows[0]["CompleteFeeder"] = TechgrationConfiguration.CompleteFeederCount;
        //                dt.Rows[0]["Persentage"] = persentage();

        //            }

        //        }
        //        ds.WriteXml(configWebfilepath);
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i <= TechgrationConfiguration.totalpersentage; i=TechgrationConfiguration.completepersentag)
            {
                Thread.Sleep(100);
                sum = sum + 1;
                backgroundWorker1.ReportProgress(persentage());

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }
            e.Result = persentage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                label2.Text = "Operation cancelled";
            }
            else if(e.Error!=null)
            {
                label2.Text = e.Error.ToString();
            }
            else
            {
                label2.Text = e.Result.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            feed();
            //savewebstatus();
            DateTime date = DateTime.Now;
            lblTime.Text = date.ToString("hh:mm:ss tt");
        }

        private void TechgrationProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
    }
}
