using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class CymeFileCreate
    {


        public void savebuttonwork(string NEWGETFILE,string GETFILE,ConfigFileData cf)
        {
            try
            {
                string[] file2 = System.IO.Directory.GetFiles(NEWGETFILE, "*.txt");                           // Old file Delete 
                foreach (string item1 in file2)
                {
                    System.IO.File.Delete(item1);
                }

                string batdel = NEWGETFILE + "\\CYMEimpFile\\IMPORT.bat";
                if (File.Exists(batdel))
                {
                    File.Delete(batdel);
                }

                string batdel1 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ini";
                if (File.Exists(batdel1))
                {
                    File.Delete(batdel1);
                }
                string batdel2 = NEWGETFILE + "\\CYMEimpFile\\IMPORT.ERR";
                if (File.Exists(batdel2))
                {
                    File.Delete(batdel2);
                }

                string imp = GETFILE + "\\Feeder\\CYMEimpConfig";
                string[] import1 = System.IO.Directory.GetFiles(imp, "*.ini");
                foreach (string item in import1)
                {
                    string cyme = NEWGETFILE + "\\CYMEimpFile";
                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.ini";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@netdbname@", cf.CNetdatabase);
                    text = text.Replace("@netuser@", cf.CNetusername);
                    text = text.Replace("@netpwd@", cf.CNetpassword);
                    text = text.Replace("@netservice@", cf.CNetservername);
                    text = text.Replace("@eqpdbname@", cf.CEqpdatabase);
                    text = text.Replace("@eqpuser@", cf.CEqpusername);
                    text = text.Replace("@eqppwd@", cf.CEqppassword);
                    text = text.Replace("@eqpservice@", cf.CEqpservername);
                    text = text.Replace("@network@", NEWGETFILE + "\\CymeTextFile\\Network.txt");
                    text = text.Replace("@load@", NEWGETFILE + "\\CymeTextFile\\Load.txt");
                    text = text.Replace("@networklist@", NEWGETFILE + "\\CymeTextFile\\NetworkList.txt");
                    text = text.Replace("@meterdemand@", NEWGETFILE + "\\CymeTextFile\\MeterDemand.txt");
                    File.WriteAllText(TargetFile, text);
                }


                string[] import2 = System.IO.Directory.GetFiles(imp, "*.bat");
                foreach (string item in import2)
                {
                    string cyme = NEWGETFILE + "\\CYMEimpFile";
                    File.Copy(item, cyme + "/" + Path.GetFileName(item));
                    string TargetFile = cyme + "\\IMPORT.bat";
                    string text = File.ReadAllText(TargetFile);
                    text = text.Replace("@setpath@", cf.CymeDirectory);
                    text = text.Replace("@upload@", NEWGETFILE);
                    File.WriteAllText(TargetFile, text);
                }
            }
            catch (Exception ex)
            { }
        }

        private Process process;

        public void import(string path, string workdir,string path1)
        {
            System.Threading.Thread.Sleep(100); // Optional delay

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };

                process.Start();
                process.WaitForExit();
            }




            //bool importfeeder = false;
            //string str_Path2 = path;
            //string Net2 = workdir;
            //if (str_Path2 == path)
            //{
            //    System.Threading.Thread.Sleep(100);
            //    importfeeder = true;
            //}

            //if (importfeeder == true)
            //{
            //    ProcessStartInfo processInfo;
            //    processInfo = new ProcessStartInfo(str_Path2);
            //    processInfo.CreateNoWindow = true;
            //    processInfo.UseShellExecute = false;
            //    // *** Redirect the output ***
            //    processInfo.RedirectStandardError = true;
            //    processInfo.RedirectStandardOutput = true;

            //    process = Process.Start(processInfo);
            //    process.WaitForExit();
            //    importfeeder = false;
            //}

        }
    }
}
