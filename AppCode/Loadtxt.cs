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
    class Loadtxt
    {
        public string load(string st, OleDbConnection conn)
        {
            string aa = "select * from TGLOAD_LOADS";
            using (OleDbCommand cmd = new OleDbCommand(aa, conn))
            {

                StreamWriter tw = File.AppendText(st + "\\TempChanges\\load1.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    //write into text file

                    OleDbDataReader reader = cmd.ExecuteReader();
                    string str = @"
[LOADS]
FORMAT_LOADS=SectionID,DeviceNumber,DeviceStage,Flags,LoadType,Connection,Location
";
                    tw.WriteLine(str);
                    while (reader.Read())
                    {
                        string section = reader["SECTIONID"].ToString().Trim();
                        string deviceno = reader["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw.Write(reader["SECTIONID"].ToString().Trim());
                            tw.Write("," + reader["DeviceNumber"].ToString().Trim());
                            tw.Write("," + reader["DeviceStage"].ToString().Trim());
                            tw.Write("," + reader["Flags"].ToString().Trim());
                            tw.Write("," + reader["LoadType"].ToString().Trim());
                            tw.Write("," + reader["Connection"].ToString().Trim());
                            tw.Write("," + reader["Location"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw.WriteLine();
                        }
                    }

                    tw.Close();

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tw.Close();
                }

            }


            string bb = "select * from TG_CUSTOMERLOADS";

            using (OleDbCommand cmd = new OleDbCommand(bb, conn))
            {

                StreamWriter tw1 = File.AppendText(st + "\\TempChanges\\load2.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    OleDbDataReader reader1 = cmd.ExecuteReader();
                    string str = @"
[CUSTOMER LOADS] 
FORMAT_CUSTOMERLOADS=SectionID,DeviceNumber,LoadType,CustomerNumber,CustomerType,ConnectionStatus,LockDuringLoadAllocation,Year,LoadModelID,NormalPriority,EmergencyPriority,ValueType,LoadPhase,Value1,Value2,ConnectedKVA,KWH,NumberOfCustomer,CenterTapPercent,CenterTapPercent2,LoadValue1N1,LoadValue1N2,LoadValue2N1,LoadValue2N2
";
                    tw1.WriteLine(str);
                    while (reader1.Read())
                    {
                        string section = reader1["SECTIONID"].ToString().Trim();
                        string deviceno = reader1["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw1.Write(reader1["SECTIONID"].ToString().Trim());
                            tw1.Write("," + reader1["DeviceNumber"].ToString().Trim());
                            tw1.Write("," + reader1["LoadType"].ToString().Trim());
                            tw1.Write("," + reader1["CustomerNumber"].ToString().Trim());
                            tw1.Write("," + reader1["CustomerType"].ToString().Trim());
                            tw1.Write("," + reader1["ConnectionStatus"].ToString().Trim());
                            tw1.Write("," + reader1["LockDuringLoadAllocation"].ToString().Trim());
                            tw1.Write("," + reader1["Year"].ToString().Trim());
                            tw1.Write("," + reader1["LoadModelID"].ToString().Trim());
                            tw1.Write("," + reader1["NormalPriority"].ToString().Trim());
                            tw1.Write("," + reader1["EmergencyPriority"].ToString().Trim());
                            tw1.Write("," + reader1["ValueType"].ToString().Trim());
                            tw1.Write("," + reader1["LoadPhase"].ToString().Trim());
                            tw1.Write("," + reader1["Value1"].ToString().Trim());
                            tw1.Write("," + reader1["Value2"].ToString().Trim());
                            tw1.Write("," + reader1["ConnectedKVA"].ToString().Trim());
                            tw1.Write("," + reader1["KWH"].ToString().Trim());
                            tw1.Write("," + reader1["NumberOfCustomer"].ToString().Trim());
                            tw1.Write("," + reader1["CenterTapPercent"].ToString().Trim());
                            tw1.Write("," + reader1["CenterTapPercent2"].ToString().Trim());
                            tw1.Write("," + reader1["LoadValue1N1"].ToString().Trim());
                            tw1.Write("," + reader1["LoadValue1N2"].ToString().Trim());
                            tw1.Write("," + reader1["LoadValue2N1"].ToString().Trim());
                            tw1.Write("," + reader1["LoadValue2N2"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw1.WriteLine();
                        }
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw1.Close();

                    reader1.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tw1.Close();
                }
            }


            using (StreamWriter writer = File.CreateText(st + "\\CymeTextFile\\Load.txt"))
            {
                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\load1.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\load2.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

            }

            return ("1");
        }
    }
}
