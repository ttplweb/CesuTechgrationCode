using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration
{
    public partial class MeterProfileProcess : Form
    {
        public MeterProfileProcess()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i <= TechgrationConfiguration.totalpersentage; i = TechgrationConfiguration.completepersentag)
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
        public int persentage()
        {
            int per = 0;
            int a = TechgrationConfiguration.completepersentag;
            int b = TechgrationConfiguration.totalpersentage;
            per = (((a * 100) / b));
            return per;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblPersentageStatus.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorker1_RunWorkerCompletedbackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblPersentageStatus.Text = "Operation cancelled";
            }
            else if (e.Error != null)
            {
                lblPersentageStatus.Text = e.Error.ToString();
            }
            else
            {
                lblPersentageStatus.Text = e.Result.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            feed();
            //savewebstatus();
            DateTime date = DateTime.Now;
            lblTime.Text = date.ToString("hh:mm:ss tt");
        }
        public string feed()
        {
            try
            {
                //lblTotalfeeder.Text = "Total Feeder : " + TechgrationConfiguration.TotalFeeederCount;
                //lblTotalfeeder.Update();
                //lblTotalfeeder.Refresh();
                //lblselectedfeeder.Text = "Selected Feeder : " + TechgrationConfiguration.SelectedFeederCount;
                //lblselectedfeeder.Update();
                //lblselectedfeeder.Refresh();
                ////labFeederID.Text = Form1.count2;
                //if (TechgrationConfiguration.CompleteFeederCount == "" || (string.IsNullOrEmpty(TechgrationConfiguration.CompleteFeederCount)))
                //{
                //    lblcompletefeeder.Text = "Completed Feeder : 0";
                //}
                //else
                //{
                //    lblcompletefeeder.Text = "Completed Feeder : " + TechgrationConfiguration.CompleteFeederCount;
                //}
                //lblcompletefeeder.Update();
                //lblcompletefeeder.Refresh();
                lblStatusP.Text = TechgrationConfiguration.lblStatusP;

                //labdivision1.Text = "Division        :   " + TechgrationConfiguration.Division1;

                //labSubstation1.Text = "Substation    :   " + TechgrationConfiguration.Substation1;

                //labFeederID1.Text = "FeederID      :   " + TechgrationConfiguration.FeederID1;

                //labFeederName1.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName1;

                //lblStatus1.Text = TechgrationConfiguration.Status2;

                //labdivision1.Text = "Division        :   " + TechgrationConfiguration.Division2;

                //labSubstation1.Text = "Substation    :   " + TechgrationConfiguration.Substation2;

                //labFeederID1.Text = "FeederID      :   " + TechgrationConfiguration.FeederID2;

                //labFeederName1.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName2;
                persentage();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Missing");
            }
            return ("1");
        }

        private void MeterProfileProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }

        private void MeterProfileProcess_Load(object sender, EventArgs e)
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
    }
}
