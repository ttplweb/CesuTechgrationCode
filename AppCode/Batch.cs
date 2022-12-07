using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class Batch
    {
        public string Main(string Cyme_config_txtReportDirectory, string st, string feeder)
        {
            int value = 0;
            try
            {
                //the path of the file
                string path = st + "\\CYMEimpFile\\IMPORT.ERR";
                if (File.Exists(path))
                {
                    FileStream inFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(inFile);
                    string record;
                    string input;

                    input = "Import Export";
                    // try
                    string str = @"

Status = Import Successful
";
                    string str1 = @"

Status = Import Failed [ " + feeder + " ]" +
    "";
                    while ((record = reader.ReadLine()) != null)
                    {
                        if (record.Contains("Warning"))
                        {
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);
                            read.Write(str);
                            read.Close();
                        }
                        else if (record.Contains("Error"))
                        {
                            WriteError(path, Cyme_config_txtReportDirectory);
                            StreamWriter read = File.AppendText(Cyme_config_txtReportDirectory);
                            read.Write(str1);
                            read.Close();
                            StreamWriter sw = File.AppendText(st + "\\errorlist.txt");
                            sw.WriteLine(feeder);
                            sw.Close();
                            value++;
                        }
                        break;
                        // record = reader.ReadLine();
                    }
                }
                else
                {
                    value++;
                }
            }
            catch (Exception ex)
            { }
            return (value.ToString());
        }

        public void WriteError(string filepath, string logpath)
        {
            try
            {
                bool isFound = false;
                string[] lines = File.ReadAllLines(filepath);
                foreach (string item in lines)
                {
                    if (item.Contains("Error"))
                    {
                        if (item.ToLower().Contains("same head") || item.ToLower().Contains("duplicate head node") || item.ToLower().Contains("same feeder id twice") || item.ToLower().Contains("error"))
                        {
                            if (!isFound)
                            {
                                StreamWriter readd = File.AppendText(logpath);
                                readd.WriteLine();
                                readd.Close();
                                isFound = true;
                            }
                            StreamWriter read = File.AppendText(logpath);
                            read.WriteLine(item);
                            read.Close();
                        }
                    }
                    if (isFound)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
