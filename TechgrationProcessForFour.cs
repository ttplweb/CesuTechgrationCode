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
    public partial class TechgrationProcessForFour : Form
    {
        public TechgrationProcessForFour()
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblPersentageStatus.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            DateTime date = DateTime.Now;
            lblTime.Text = date.ToString("hh:mm:ss tt");
        }

        private void TechgrationProcessForFour_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.Refresh();
            feed();
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public int persentage()
        {
            int per = 0;
            int a = TechgrationConfiguration.completepersentag;
            int b = TechgrationConfiguration.totalpersentage;
            per = (((a * 100) / b));
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
                if (TechgrationConfiguration.CompleteFeederCount == "" || (string.IsNullOrEmpty(TechgrationConfiguration.CompleteFeederCount)))
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
                lblStatus3.Text = TechgrationConfiguration.Status3;
                lblStatus4.Text = TechgrationConfiguration.Status4;
                labdivision1.Text = "Division        :   " + TechgrationConfiguration.Division1;
                labdivision2.Text = "Division        :   " + TechgrationConfiguration.Division2;
                labdivision3.Text = "Division        :   " + TechgrationConfiguration.Division3;
                labdivision4.Text = "Division        :   " + TechgrationConfiguration.Division4;
                labSubstation1.Text = "Substation    :   " + TechgrationConfiguration.Substation1;
                labSubstation2.Text = "Substation    :   " + TechgrationConfiguration.Substation2;
                labSubstation3.Text = "Substation    :   " + TechgrationConfiguration.Substation3;
                labSubstation4.Text = "Substation    :   " + TechgrationConfiguration.Substation4;
                labFeederID1.Text = "FeederID      :   " + TechgrationConfiguration.FeederID1;
                labFeederID2.Text = "FeederID      :   " + TechgrationConfiguration.FeederID2;
                labFeederID3.Text = "FeederID      :   " + TechgrationConfiguration.FeederID3;
                labFeederID4.Text = "FeederID      :   " + TechgrationConfiguration.FeederID4;
                labFeederName1.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName1;
                labFeederName2.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName2;
                labFeederName3.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName3;
                labFeederName4.Text = "Feeder Name :  " + TechgrationConfiguration.FeederName4;

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

        private void TechgrationProcessForFour_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
    }
}
