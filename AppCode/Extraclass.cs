using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class Extraclass
    {

        //Thread threadFeeder1 = new Thread(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString()));
        //Thread threadFeeder2 = new Thread(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
        //Thread threadFeeder3 = new Thread(() => RunProcessFeeder3("Feeder3", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
        //Thread threadFeeder4 = new Thread(() => RunProcessFeeder4("Feeder4", Dt.Rows[i + 3]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
        //threadFeeder1.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder1.Start();
        //                threadFeeder2.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder2.Start();
        //                threadFeeder3.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder3.Start();
        //                threadFeeder4.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder4.Start();
        //                while (!IsRunningProcess1 || !IsRunningProcess2 || !IsRunningProcess3 || !IsRunningProcess4)
        //                {
        //                    Thread.Sleep(1000);
        //                }

        //                if (IsRunningProcess1 && IsRunningProcess2 && IsRunningProcess3 && IsRunningProcess4)
        //                {
        //                    aa.combineErrorLog(Logpath, GETFILE, 4);
        //                }


        //Thread threadFeeder1 = new Thread(() => RunProcessFeeder1("Feeder1", Dt.Rows[i]["FeederId"].ToString(), Dt.Rows[i]["Division"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["FeederName"].ToString()));
        //Thread threadFeeder2 = new Thread(() => RunProcessFeeder2("Feeder2", Dt.Rows[i + 1]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
        //Thread threadFeeder3 = new Thread(() => RunProcessFeeder3("Feeder2", Dt.Rows[i + 2]["FeederId"].ToString(), Dt.Rows[i + 1]["Division"].ToString(), Dt.Rows[i + 1]["Name"].ToString(), Dt.Rows[i + 1]["FeederName"].ToString()));
        //threadFeeder1.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder1.Start();
        //                threadFeeder2.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder2.Start();
        //                threadFeeder3.SetApartmentState(ApartmentState.MTA);
        //                threadFeeder3.Start();
        //                while (!IsRunningProcess1 || !IsRunningProcess2 || !IsRunningProcess3)
        //                {
        //                    Thread.Sleep(1000);
        //                }

        //                if (IsRunningProcess1 && IsRunningProcess2 && IsRunningProcess3)
        //                {
        //                    aa.combineErrorLog(Logpath, GETFILE, 3);
        //                }

        //Thread trd = new Thread(new ThreadStart(this.ThreadTask));
        //trd.IsBackground = true;
        //trd.Start();



        //if(!backgroundWorker1.IsBusy)
        //{
        //    backgroundWorker1.RunWorkerAsync();
        //}


        //public void Run()
        //{

        //    SelectedFeederList SFD = new SelectedFeederList();
        //    DataTable Dt = new DataTable();
        //    Dt = SFD.GetFeederTable(cf.MDASservername, cf.MDASdatabase, cf.MDASusername, cf.MDASpassword);
        //    ErrorLog aa = new ErrorLog();
        //    string Logpath = aa.fir(cf.Errorlog, Dt.Rows.Count.ToString());
        //    if (Dt.Rows.Count>0)
        //    {
        //        for(int i=0;i<Dt.Rows.Count;i=i+2)
        //        {

        //        }
        //    }
        //}



        //        int sum = 0;
        //for(int i=0;i<=100;i++)
        //{
        //    Thread.Sleep(100);
        //    sum = sum + 1;
        //    //backgroundWorker1.ReportProgress(statuspersentage);

        //    if (backgroundWorker1.CancellationPending)
        //    {
        //        e.Cancel = true;
        //        backgroundWorker1.ReportProgress(0);
        //        return;
        //    }
        //}
        //e.Result = sum;




        //Thread th1 = new Thread(() => Count("text1",1));
        //Thread th2 = new Thread(() => Count("text2",2));
        //th1.Start();
        //th2.Start();

        //public void Count(string filename,int j)
        //{
        //    StreamWriter sw = File.AppendText(GETFILE + "\\Feeder\\"+filename+".txt");
        //    for (int i=0;i<=(100*j);i=i+j)
        //    {
        //        sw.WriteLine(i.ToString());
        //        Thread.Sleep(1000);
        //    }
        //    sw.Close();
        //}


    }
}
