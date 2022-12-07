using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class FMEFileCreate
    {
        public void Filecreate(string GETFILE,string UserType,string Feederid,ConfigFileData CF)
        {
            string Sourcepath = GETFILE + "\\Feeder";
            string DestPath = GETFILE + "\\"+UserType;
            string destfile = DestPath + "\\FME\\Mapping";
            string[] filedelete = System.IO.Directory.GetFiles(destfile, "*.fmw");
            foreach (string item in filedelete)
            {
                System.IO.File.Delete(item);
            }
            string[] filedelete0 = System.IO.Directory.GetFiles(destfile, "*.tcl");
            foreach (string item0 in filedelete0)
            {
                System.IO.File.Delete(item0);
            }
            string map = Sourcepath + "\\Fme\\MAPPING.tcl";
            File.Copy(map, destfile + "/" + Path.GetFileName(map));
            string map1 = destfile + "\\MAPPING.tcl";
            string map2 = File.ReadAllText(map1);
            string c = DestPath.Replace(@"\", @"/");
            map2 = map2.Replace("@maping@", c);        //done
            File.WriteAllText(map1, map2);
            string batch1 = destfile + "\\Mapping.bat";
            if (File.Exists(batch1))
            {
                File.Delete(batch1);
            }
            string batch = Sourcepath + "\\Fme\\Mapping.bat";
            File.Copy(batch, destfile + "/" + Path.GetFileName(batch));
            string getbat = File.ReadAllText(batch1);
            getbat = getbat.Replace("@mapping@", DestPath);
            getbat = getbat.Replace("@fmemap@", CF.FMEDirectory);
            File.WriteAllText(batch1, getbat);
            string bb = Sourcepath + "\\FME\\Mapping.fmw";                                //Find the Fme File 
            string TargetFile2 = destfile + "\\Mapping.fmw";                                      // Copy the Fme File
            File.Copy(bb, destfile + "/" + Path.GetFileName(bb));
            string file1 = "";                                                       // Delete The Old window in Feederlist_where_clause
            file1 = DestPath + "\\FME\\custinfo";
            string[] filedelete1 = System.IO.Directory.GetFiles(file1, "*.fmw");
            foreach (string item1 in filedelete1)
            {
                System.IO.File.Delete(item1);
            }
            string[] filedelete2 = System.IO.Directory.GetFiles(file1, "*.tcl");
            foreach (string item2 in filedelete2)
            {
                System.IO.File.Delete(item2);
            }
            string btch = file1 + "\\custinfo.bat";
            if (File.Exists(btch))
            {
                File.Delete(btch);
            }
            string cust = Sourcepath + "\\Fme\\custinfo.tcl";
            File.Copy(cust, file1 + "/" + Path.GetFileName(cust));
            string cuinfo = file1 + "\\custinfo.tcl";
            string cust1 = File.ReadAllText(cuinfo);
            cust1 = cust1.Replace("@custinfo@", c);
            File.WriteAllText(cuinfo, cust1);
            string btch1 = Sourcepath + "\\Fme\\custinfo.bat";
            File.Copy(btch1, file1 + "/" + Path.GetFileName(btch1));
            string getbatch1 = File.ReadAllText(btch);
            getbatch1 = getbatch1.Replace("@custinfo@", DestPath);
            getbatch1 = getbatch1.Replace("@fmecust@", CF.FMEDirectory);
            File.WriteAllText(btch, getbatch1);
            string bb1 = Sourcepath + "\\FME\\custinfo.fmw";                                //Find the Fme File 
            string TargetFile3 = file1 + "\\custinfo.fmw";                                      // Copy the Fme File
            File.Copy(bb1, file1 + "/" + Path.GetFileName(bb1));
            
            string text = File.ReadAllText(TargetFile2);
            text = text.Replace("feederid12345", Feederid);
            text = text.Replace("@path@", DestPath);                    ///done
            text = text.Replace("@password@", CF.Gispassword);
            text = text.Replace("@user@", CF.Gisusername);
            text = text.Replace("@server@", CF.Gisservername);
            //text = text.Replace("@instance@", instance);
            //text = text.Replace("SDE.DEFAULT", Cyme_gis_Versioncombo.Text);
            text = text.Replace("@databasename@", CF.GisDatabase);
            text = text.Replace("override_schema", CF.GisSchema_Name);
            text = text.Replace("@sqldatabase@", CF.TGdatabase);
            text = text.Replace("@sqlserver@", CF.TGservername);
            text = text.Replace("@sqluser@", CF.TGusername);
            text = text.Replace("@sqlpassword@", CF.TGpassword);
            File.WriteAllText(TargetFile2, text);

            string text2 = File.ReadAllText(TargetFile3);
            text2 = text2.Replace("@feederid12345@", Feederid);
            text2 = text2.Replace("@custinfoD@", DestPath);
            text2 = text2.Replace("@custinfoS@", DestPath);
            text2 = text2.Replace("@path@", DestPath);
            text2 = text2.Replace("@password@", CF.Gispassword);
            text2 = text2.Replace("@user@", CF.Gisusername);
            text2 = text2.Replace("@server@", CF.Gisservername);
            //text2 = text2.Replace("@instance@", instance);
            //text2 = text2.Replace("SDE.DEFAULT", Cyme_gis_Versioncombo.Text);
            text2 = text2.Replace("GISDBNAME", CF.GisDatabase);
            text2 = text2.Replace("override_schema", CF.GisSchema_Name);
            File.WriteAllText(TargetFile3, text2);
            Thread.Sleep(2000);
            string mdbdelete = DestPath + @"\GISPGDB\Input_Data.mdb";
            if (File.Exists(mdbdelete))
            {
                try
                {
                    //System.GC.Collect();
                    //System.GC.WaitForPendingFinalizers();
                    File.Delete(mdbdelete);
                }
                catch (Exception ex)
                { }
            }

            string str_Path = DestPath + "\\FME\\Mapping\\Mapping.bat";
            string Net = DestPath + "\\FME\\Mapping";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = str_Path;
            proc.StartInfo.WorkingDirectory = Net;
            proc.Start();
            proc.WaitForExit();

            string str_Path1 = DestPath + "\\FME\\custinfo\\custinfo.bat";
            string Net1 = DestPath + "\\FME\\custinfo";
            System.Diagnostics.Process proc1 = new System.Diagnostics.Process();
            proc1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc1.StartInfo.CreateNoWindow = true;
            proc1.StartInfo.FileName = str_Path1;
            proc1.StartInfo.WorkingDirectory = Net1;
            proc1.Start();
            proc1.WaitForExit();

        }
    }
}
