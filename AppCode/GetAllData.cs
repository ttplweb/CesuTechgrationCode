using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Numerics;

namespace TechGration.AppCode
{
    class GetAllData
    {
        public void GIS_Data(string getfilee, ConfigFileData hh, string Feederid, OleDbConnection conm,String USERTYPE,String Status)
        {
              
            string key = hh.tempkey;
          
            string connectionString = @"Data Source=" + hh.Gisservername +                       //Create Connection string
                      ";database=" + hh.GisDatabase +
                      ";User ID=" + hh.Gisusername +
                      ";Password=" + hh.Gispassword;
            UTM UTTT = new UTM();
            using (SqlConnection obj = new SqlConnection(connectionString)) 
             {
                try
                {
                    string Schema = hh.GisSchema_Name;
                    int A = 0;
                    while(obj.State==ConnectionState.Closed)
                    {
                        try
                        {
                            if (obj.State == ConnectionState.Closed)
                            {
                                A++;
                                obj.Open();
                            }
                        
                        }
                        catch(Exception EX)
                        {

                        }
                        if(A==1000)
                        {
                            Status = "Sql Connection Not Found";
                        }
                     
                    }

                    string Configpath = getfilee + @"\ConfigFile\GIS_Table.xml";       

                    string Name = string.Empty;
                    string newquarry = string.Empty;
                    string newquarry2 = string.Empty;
                    string data = string.Empty;
                    string insertvalue = string.Empty;
                    string insertColumn = string.Empty;
                    if (File.Exists(Configpath))
                    {
                        string quarry = string.Empty;
                        string quarry2 = string.Empty;
                        DataSet ds = new DataSet();
                        ds.ReadXml(Configpath);
                        List<string> mylist = new List<string>();
                        List<string> fieldlist = new List<string>();
                        int v = 0;
                        int hhh = 0;
                        int cc = 0;
                        int bb = 0;
                        bool gg = true;
                        foreach (DataTable dt in ds.Tables)
                        {
                            mylist.Add(dt.TableName);
                            int fieldcount = dt.Columns.Count;
                            for (int i = 0; i < fieldcount; i++)
                            {
                                string vv = dt.Columns[i].ToString();
                                fieldlist.Add(vv);

                            }
                            for (int j = bb; j < mylist.Count; j++)
                            {
                                if (gg == false)
                                {
                                    j++;
                                    bb = j;
                                }
                                string vv1 = mylist[j];
                                Name = vv1;
                                if (Name == "F03_PC03_TUBU_TT")
                                {

                                }
                                if (Name != "CONSUMERMETER" && Name!= "CONSUMERINFO")
                                {
                                    if (gg == false && v != 0)
                                    {
                                        v++;
                                        hhh++;
                                        cc++;
                                        gg = true;
                                    }
                                    int k;
                                    for (k = v; k < fieldlist.Count; k++)
                                    {
                                        int ll = 0;
                                        string test = "[SHAPE].STX AS [longITUDE],[SHAPE].STY AS [lATITUDE]";
                                        //string test = "Round([SHAPE].STX,3) AS [longITUDE],Round([SHAPE].STY,3) AS [lATITUDE]";
                                        string TEST2 = "[SHAPE].STStartPoint().STX  AS [FromNodeX] ,[SHAPE].STStartPoint().STY  AS [FromNodeY] ,[SHAPE].STEndPoint().STX  AS[ToNodeX],[SHAPE].STEndPoint().STY  AS[ToNodeY]";
                                       // string TEST2 = "ROUND([SHAPE].STStartPoint().STX,3)  AS [FromNodeX] ,Round([SHAPE].STStartPoint().STY,3)  AS [FromNodeY] ,Round([SHAPE].STEndPoint().STX,3)  AS[ToNodeX],Round([SHAPE].STEndPoint().STY,3)  AS[ToNodeY]";
                                        string nn = fieldlist[k];

                                        if (nn == "Shape")
                                        {
                                            nn = "SHAPE";
                                        }
                                        if (( Name == "CONDUCTOR" || Name == "BUSBAR"  || Name == "CABLE" || Name=="LINE" || Name== "F09_PC03_DUONGDAY_TT" || Name == "PC03_RECLOSER") && nn == "SHAPE")
                                        {
                                            string BB = nn.Replace(nn, TEST2);
                                            quarry += BB + ',';
                                            ll = 1;
                                            v = k;
                                        }

                                        if (nn == "SHAPE" && ll == 0)
                                        {
                                            string BB = nn.Replace(nn, test);
                                            quarry += BB + ',';
                                            v = k;
                                        }
                                        if (nn != "SHAPE")
                                        {
                                            if (nn == "Xuất_tuyến")
                                            {
                                               
                                            }
                                            if (nn== "SwitchGearObjId")
                                            {
                                                quarry += "floor" + "(["+fieldlist[k]+ "]) as SwitchGearObjId" + ',';
                                                v = k;
                                            }
                                            else
                                            {
                                                quarry += "["+fieldlist[k] + "]"+',';
                                                v = k;
                                            }
                                          
                                        }


                                    }

                                    for (k = hhh; k < fieldlist.Count; k++)
                                    {

                                        int cccc = 0;
                                        if (k == 48)
                                        {

                                        }

                                       

                                        string nn = fieldlist[k];
                                        if (nn == "Shape")
                                        {
                                            nn = "SHAPE";
                                        }
                                        if (nn == "SHAPE" && ( Name == "CONDUCTOR" || Name == "BUSBAR" || Name == "CABLE" || Name=="LINE" ||Name== "F09_PC03_DUONGDAY_TT" || Name == "PC03_RECLOSER"))
                                        {
                                            string JJ = "FromNodeX";
                                            quarry2 += JJ + " Varchar(250)" + ',';
                                            string JJ1 = "FromNodeY";
                                            quarry2 += JJ1 + " Varchar(250)" + ',';
                                            string JJ2 = "ToNodeX";
                                            quarry2 += JJ2 + " Varchar(250)" + ',';
                                            string JJ12 = "ToNodeY";
                                            quarry2 += JJ12 + " Varchar(250)" + ',';
                                            cccc = 1;
                                            hhh = k;
                                        }



                                        if (nn == "SHAPE" && cccc == 0)
                                        {
                                            string JJ = "X";
                                            quarry2 += JJ + " Varchar(250)" + ',';
                                            string JJ1 = "Y";
                                            quarry2 += JJ1 + " Varchar(250)" + ',';
                                            hhh = k;

                                        }
                                        if (nn != "SHAPE")
                                        {
                                           
                                            quarry2 += "["+fieldlist[k] +"]"+ " Varchar(250)" + ',';
                                            hhh = k;
                                        }

                                    }
                                    if (gg == true || v != 0)
                                    {
                                      
                                        newquarry2 = quarry2.Remove(quarry2.Length - 1);
                                        string myquarry2 = "Create Table  GIS_" + Name + " (" + newquarry2 + ")";
                                        OleDbCommand cmm = new OleDbCommand(myquarry2, conm);

                                        cmm.ExecuteNonQuery();

                                    }


                                    if (gg == true || v != 0)
                                    {
                                        //string ggj1 = getfilee + "\\load1.txt";
                                        //StreamWriter sw1 = File.AppendText(ggj1);
                                        //sw1.WriteLine("HDHDHHD");
                                        //sw1.Close();
                                        newquarry = quarry.Remove(quarry.Length - 1);
                                        string myquarry = string.Empty;

                                        if (Name == "BUSBAR")
                                        {
                                            myquarry = "Select  DISTINCT " + newquarry + " from " + Schema + "." + Name + " Where [XuatTuyen]='" + Feederid + "'";
                                        }
                                        else if (Name == "PC03_RECLOSER")
                                        {
                                            myquarry = "Select  DISTINCT " + newquarry + " from " + Schema + "." + Name + " ";
                                        }
                                        else
                                        {
                                            myquarry = "Select  DISTINCT " + newquarry + " from "+ Schema + "."+ Name + " Where [XuatTuyen] ='" + Feederid + "'";
                                        }

                                        SqlCommand cmd = new SqlCommand(myquarry, obj);
                                        // obj.Open();
                                        if (obj.State != ConnectionState.Open)
                                        {
                                            obj.Open();
                                        }

                                        SqlDataAdapter AD1 = new SqlDataAdapter(cmd);
                                        DataTable DTt1 = new DataTable();
                                        AD1.Fill(DTt1);
                                        gg = false;
                                        int ss = DTt1.Rows.Count;
                                        int d = 0;
                                        if (ss == 0)
                                        {
                                            d = fieldlist.Count;
                                        }
                                        if (gg == true || v != 0)
                                        {
                                            for (int i = 0; i < DTt1.Rows.Count; i++)
                                            {
                                             
                                                for (d = cc; d < fieldlist.Count; d++)
                                                {
                                                   

                                                    string xx = fieldlist[d];

                                                   
                                                    if (xx == "Shape")
                                                    {
                                                        xx = "SHAPE";
                                                    }

                                                    if (xx == "SHAPE" && Name != "CONDUCTOR" && Name != "BUSBAR" && Name != "CABLE" && Name!="LINE" && Name != "F09_PC03_DUONGDAY_TT" && Name != "PC03_RECLOSER")
                                                    {
                                                        string MMM = DTt1.Rows[i]["longITUDE"].ToString();
                                                        string MMM1 = DTt1.Rows[i]["lATITUDE"].ToString();
                                                        if (MMM!=""&&MMM1!="")
                                                        {
                                                            string[] value = UTTT.ConvertToUTM(MMM, MMM1);
                                                            MMM = value[0];
                                                            MMM1 = value[1];
                                                        }
                                                     
                                                        string JJ = "X";
                                                        insertColumn += JJ + ',';
                                                        data += MMM;
                                                        string JJ1 = "Y";
                                                        insertvalue += "'" + data + "'" + ',';
                                                        data = "";
                                                        insertColumn += JJ1 + ',';
                                                        data += MMM1;
                                                        insertvalue += "'" + data + "'" + ',';
                                                        //string ggj1 = getfilee + "\\load1.txt";
                                                        //StreamWriter sw1 = File.AppendText(ggj1);
                                                        //sw1.WriteLine(i);
                                                        //sw1.Close();
                                                    }
                                                    if (xx != "SHAPE")
                                                    {
                                                        
                                                        string tt = DTt1.Rows[i][xx].ToString();
                                                        string NN = tt.Replace("'", "  ");
                                                        //tt = NN;

                                                      

                                                        if (Name == "BUSBAR")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "BB_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if(Name == "CABLE")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "CA_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "F09_PC03_DUONGDAY_TT")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "OH_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "F04_PC03_MAYBIENAP_TT")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "DT_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }

                                                        }
                                                        else if (Name == "POWERTRANSFORMER")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "PW_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }

                                                        }
                                                        else if (Name == "FUSE")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "FS_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "SWITCH")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "SW_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "F01_PC03_THIETBIDONGCAT_TT")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "SG_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "F03_PC03_TUBU_TT")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "SC_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else if (Name == "PC03_RECLOSER")
                                                        {
                                                            if (xx == "OBJECTID")
                                                            {
                                                                tt = "RC_" + NN;
                                                            }
                                                            else
                                                            {
                                                                tt = NN;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            tt = NN;
                                                        }
                                                        



                                                        data += tt;
                                                        insertColumn += "["+xx + "]"+',';
                                                        insertvalue += "'" + data + "'" + ',';
                                                    }

                                                    if (xx == "SHAPE" && (Name == "CONDUCTOR" || Name == "BUSBAR" || Name == "CABLE" || Name=="LINE" || Name == "F09_PC03_DUONGDAY_TT" || Name == "PC03_RECLOSER"))
                                                    {
                                                       
                                                        string fff = DTt1.Rows[i]["FromNodeX"].ToString();
                                                        string fff1 = DTt1.Rows[i]["FromNodeY"].ToString();

                                                        if (fff!=""&&fff1!="")
                                                        {
                                                            string[] value = UTTT.ConvertToUTM(fff, fff1);
                                                            fff = value[0];
                                                            fff1 = value[1];
                                                        }

                                                        
                                                        string fff2 = DTt1.Rows[i]["ToNodeX"].ToString();
                                                        string fff3 = DTt1.Rows[i]["ToNodeY"].ToString();

                                                        if (fff2 != "" && fff3 != "")
                                                        {
                                                            string[] value1 = UTTT.ConvertToUTM(fff2, fff3);
                                                            fff2 = value1[0];
                                                            fff3 = value1[1];
                                                        }
                                                            
                                                        string JJ = "FromNodeX";
                                                        insertColumn += JJ + ',';
                                                        data += fff;
                                                        string JJ1 = "FromNodeY";
                                                        insertvalue += "'" + data + "'" + ',';
                                                        data = "";
                                                        insertColumn += JJ1 + ',';
                                                        data += fff1;
                                                        insertvalue += "'" + data + "'" + ',';
                                                        data = "";
                                                        string JJ2 = "ToNodeX";
                                                        string JJ3 = "ToNodeY";
                                                        insertColumn += JJ2 + ',';
                                                        data += fff2;
                                                        insertvalue += "'" + data + "'" + ',';
                                                        data = "";
                                                        insertColumn += JJ3 + ',';
                                                        data += fff3;
                                                        insertvalue += "'" + data + "'" + ',';
                                                        data = "";
                                                    }

                                                    //string ggj2 = getfilee + "\\load2dfr.txt";
                                                    //StreamWriter sw2 = File.AppendText(ggj2);
                                                    //sw2.WriteLine(i);
                                                    //sw2.Close();


                                                    data = string.Empty;
                                                    xx = string.Empty;

                                                }
                                                //string get2 = getfilee + "\\CYMEUPLOAD" + "\\bdbdcym.txt";
                                                //StreamWriter sw2 = File.AppendText(get2);
                                                //sw2.WriteLine(i+' '+Name);
                                                //sw2.Close();
                                                //if (i == 41)
                                                //{

                                                //}

                                                 
                                                string jjj = insertvalue.Remove(insertvalue.Length - 1);
                                                string ddd = insertColumn.Remove(insertColumn.Length - 1);
                                              
                                               
                                                string insertqrry = "insert into  GIS_" + Name + " (" + ddd + ") values (" + jjj + ")";

                                                OleDbCommand cox = new OleDbCommand(insertqrry, conm);
                                                //if (Name == "LINE")
                                                //{
                                                //    string ggj2 = getfilee + "\\load2.txt";
                                                //    StreamWriter sw2 = File.AppendText(ggj2);
                                                //    sw2.WriteLine(insertqrry);
                                                //    sw2.Close();
                                                //}
                                                if (conm.State != ConnectionState.Open)
                                                {
                                                    conm.Open();
                                                }
                                                cox.ExecuteNonQuery();
                                                insertvalue = string.Empty;
                                                insertColumn = string.Empty;
                                            
                                            }
                                            cc = d - 1;

                                        }

                                      


                                        // string insertTable = "Insert into()"




                                        quarry = string.Empty;
                                        quarry2 = string.Empty;
                                    }
                                }
                                
                            }

                        }

                    }
                    //if (conm.State == ConnectionState.Open)
                    //{
                    //    conm.Close();
                      
                    //}
                }
                catch (Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(getfilee, ex.ToString());
                }
            }

               
        } //use
         
        public void lengthUpdate(string getfile, ConfigFileData hh, string Feederid, OleDbConnection coon)
        {
            string connectionString = @"Data Source=" + hh.Gisservername +                       //Create Connection string
                      ";database=" + hh.GisDatabase +
                      ";User ID=" + hh.Gisusername +
                      ";Password=" + hh.Gispassword;
            String SCHEMA = hh.GisSchema_Name;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {
                try
                {
                    string Configpath = getfile + "\\" + @"\ConfigFile\GIS_Table.xml";

                    string quarry = string.Empty;
                    string quarry2 = string.Empty;
                    // string[] myArray = { "BUSBAR", "LTCABLE", "HTCABLE", "HTCONDUCTOR", "LTCONDUCTOR" };
                    string[] myArray = { "BUSBAR", "CABLE", "LINE" };

                    DataSet ds = new DataSet();
                    ds.ReadXml(Configpath);
                    List<string> mylist = new List<string>();
                    foreach (DataTable dt in ds.Tables)
                    {
                        mylist.Add(dt.TableName);

                    }
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        string vv = mylist[i].ToString();
                        for (int K = 0; K < myArray.Length; K++)
                        {
                            string TT = myArray[K];
                            if (TT == vv)
                            {

                                String DATABASE = hh.GisDatabase;
                                SqlCommand cmd1 = new SqlCommand("SELECT OBJECTID, Shape.STLength() as Length  FROM [" + DATABASE + "].[" + SCHEMA + "].[" + vv + "]  where [FeederId] = '" + Feederid + "'", obj); //new_query....nik

                                SqlDataAdapter AD1 = new SqlDataAdapter(cmd1);
                                cmd1.CommandTimeout = 50000; /// TimeOut_ProcessTime.....nik_find
                                DataTable DT1 = new DataTable();
                                AD1.Fill(DT1);
                                //OBJECTID,FeederID,FeederID2,FEEDERINFO,SubstationName,SubstationID,SwitchGearObjID,FeederName,FeederSourceInfo,FeederCode,WorkOrderID,ToSubstationID                      
                                string OBJECTID = null;
                                string Length = null;

                                string AddColumn = "ALTER TABLE GIS_"+vv+ " ADD Length  Double Precision";
                                OleDbCommand COMTTT = new OleDbCommand(AddColumn, coon);
                                COMTTT.ExecuteNonQuery();

                                if (DT1.Rows.Count != 0)
                                {
                                    for (int G = 0; G < DT1.Rows.Count; G++)
                                    {
                                        OBJECTID = DT1.Rows[G]["OBJECTID"].ToString();
                                        Length = DT1.Rows[G]["Length"].ToString();

                                        string DD = null;

                                        if (vv == "BUSBAR")
                                        {
                                            OBJECTID = "BB_" + OBJECTID;
                                            DD = "update GIS_" + vv + "  set Length ='" + Length + "' where OBJECTID = '" + OBJECTID + "'  ";

                                        }
                                        if (vv == "CABLE")
                                        {
                                            OBJECTID = "CA_" + OBJECTID;
                                            DD = "update GIS_" + vv + " set Length ='" + Length + "' where OBJECTID = '" + OBJECTID + "'  ";

                                        }
                                        if (vv == "LINE")
                                        {
                                            OBJECTID = "OH_" + OBJECTID;
                                            DD = "update GIS_" + vv + " set Length ='" + Length + "' where OBJECTID = '" + OBJECTID + "'  ";

                                        }
                                         
                                        OleDbCommand COMT = new OleDbCommand(DD, coon);
                                        COMT.ExecuteNonQuery();
                                    }
                                }

                                 
                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                }


            }
        }

        public void GIS_INTERMEDIATENODESx(string getfile, ConfigFileData hh, string Feederid, OleDbConnection coon)
        {
            
            string connectionString = @"Data Source=" + hh.Gisservername +                       //Create Connection string
                      ";database=" + hh.GisDatabase +
                      ";User ID=" + hh.Gisusername +
                      ";Password=" + hh.Gispassword;
            String SCHEMA = hh.GisSchema_Name;
            using (SqlConnection obj = new SqlConnection(connectionString))
            {

                try
                {
                    //if (coon.State != ConnectionState.Open)
                    //{
                    //    coon.Open();
                    //}
                    string PHF11 = "Create Table GIS_INTERMEDIATENODES (AssetID Varchar(250),Range varchar(250),X  varchar(250),Y  varchar(250))";

                    OleDbCommand cmm = new OleDbCommand(PHF11, coon);
                    cmm.ExecuteNonQuery();

                    string Configpath = getfile+"\\"+ @"\ConfigFile\GIS_Table.xml";

                    string quarry = string.Empty;
                    string quarry2 = string.Empty;
                   // string[] myArray = { "BUSBAR", "LTCABLE", "HTCABLE", "HTCONDUCTOR", "LTCONDUCTOR" };
                    string[] myArray = { "F09_PC03_DUONGDAY_TT","BUSBAR","CABLE","LINE"};
                  
                    DataSet ds = new DataSet();
                    ds.ReadXml(Configpath);
                    List<string> mylist = new List<string>();
                    foreach (DataTable dt in ds.Tables)
                    {
                        mylist.Add(dt.TableName);

                    }
                    for (int i = 0; i < mylist.Count; i++)
                     {
                        string vv = mylist[i].ToString();
                        for (int K = 0; K < myArray.Length; K++)
                        {
                            string TT = myArray[K];
                            if(TT==vv)
                            {
                                
                                String DATABASE = hh.GisDatabase;
                              //   SqlCommand cmd1 = new SqlCommand("select ct.OBJECTID,ct.FeederId,ROUND(SplitXY.X,3) As X,ROUND(SplitXY.Y,3) As Y,ROW_NUMBER() OVER(ORDER BY SplitXY.X, SplitXY.Y) AS Range from (SELECT OBJECTID,FeederId,[LongitudeX],[LatitudeY],[LongitudeX1],[LatitudeY1],SUBSTRING(Coordinates, 2, LEN(Coordinates) - CHARINDEX(',', REVERSE(Coordinates))) AS Coordinates FROM(SELECT OBJECTID,FeederId,[SHAPE].STStartPoint().STX  AS[LongitudeX],[SHAPE].STStartPoint().STY  AS[LatitudeY],[SHAPE].STEndPoint().STX  AS[LongitudeX1],[SHAPE].STEndPoint().STY  AS[LatitudeY1],SUBSTRING(SHAPE.STAsText(), CHARINDEX(',', SHAPE.STAsText()), CHARINDEX(')', SHAPE.STAsText()) - CHARINDEX('(', SHAPE.STAsText()) - 1) AS Coordinates FROM [" + DATABASE+"].["+ SCHEMA+ "].[" + vv + "] )AS CoordinatesTable)AS ct CROSS APPLY STRING_SPLIT(Coordinates, ',') As MultiSplitCoordinates CROSS APPLY(SELECT SUBSTRING(TRIM(MultiSplitCoordinates.value), 1, CHARINDEX(' ', TRIM(MultiSplitCoordinates.value))) AS X, SUBSTRING(TRIM(MultiSplitCoordinates.value), CHARINDEX(' ', TRIM (MultiSplitCoordinates.value)) + 1,LEN(TRIM(MultiSplitCoordinates.value))) AS Y) AS SplitXY where MultiSplitCoordinates.value like '% %' and ct.feederId = '" + Feederid + "' ", obj);
                               // SqlCommand cmd1 = new SqlCommand("SELECT ct.OBJECTID, ct.FeederId, ROUND(SplitXY.X, 3) AS X, ROUND(SplitXY.Y, 3) AS Y, ROW_NUMBER() OVER (PARTITION BY ct.OBJECTID ORDER BY Sequence) AS Range FROM ( SELECT OBJECTID, FeederId,[LongitudeX], [LatitudeY], [LongitudeX1], [LatitudeY1],  SUBSTRING(Coordinates, 2, LEN(Coordinates) - CHARINDEX(',', REVERSE(Coordinates))) AS Coordinates, ROW_NUMBER() OVER (PARTITION BY OBJECTID ORDER BY (SELECT NULL)) AS Sequence FROM  ( SELECT  OBJECTID, FeederId, [SHAPE].STStartPoint().STX AS [LongitudeX], [SHAPE].STStartPoint().STY AS [LatitudeY], [SHAPE].STEndPoint().STX AS [LongitudeX1], [SHAPE].STEndPoint().STY AS [LatitudeY1], SUBSTRING(SHAPE.STAsText(), CHARINDEX(',', SHAPE.STAsText()), CHARINDEX(')', SHAPE.STAsText()) - CHARINDEX('(', SHAPE.STAsText()) - 1) AS Coordinates FROM [" + DATABASE + "].[" + SCHEMA + "].[" + vv + "] ) AS CoordinatesTable ) AS ct CROSS APPLY STRING_SPLIT(Coordinates, ',') AS MultiSplitCoordinates CROSS APPLY ( SELECT SUBSTRING(TRIM(MultiSplitCoordinates.value), 1, CHARINDEX(' ', TRIM(MultiSplitCoordinates.value))) AS X, SUBSTRING(TRIM(MultiSplitCoordinates.value), CHARINDEX(' ', TRIM(MultiSplitCoordinates.value)) + 1, LEN(TRIM(MultiSplitCoordinates.value))) AS Y ) AS SplitXY  WHERE MultiSplitCoordinates.value LIKE '% %' AND ct.feederId = '" + Feederid + "'", obj); //new_query....nik
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();

                                    //Create the SqlCommand and assign the connection
                                    //SqlCommand cmd1 = new SqlCommand(
                                    //    "WITH CoordinatesTable AS (" +
                                    //    "SELECT OBJECTID, FeederId, [SHAPE].STStartPoint().STX AS [LongitudeX], [SHAPE].STStartPoint().STY AS [LatitudeY], " +
                                    //    "[SHAPE].STEndPoint().STX AS [LongitudeX1], [SHAPE].STEndPoint().STY AS [LatitudeY1], " +
                                    //    "SUBSTRING(SHAPE.STAsText(), CHARINDEX('(', SHAPE.STAsText()) + 1, " +
                                    //    "CHARINDEX(')', SHAPE.STAsText()) - CHARINDEX('(', SHAPE.STAsText()) - 1) AS Coordinates " +
                                    //    "FROM [" + DATABASE + "].[" + SCHEMA + "].[" + vv + "] " +
                                    //    "WHERE FeederId = '" + Feederid + "'), " +
                                    //    "SplitCoordinates AS (SELECT ct.OBJECTID, ct.FeederId, TRIM(value) AS Coordinate " +
                                    //    "FROM CoordinatesTable ct CROSS APPLY STRING_SPLIT(ct.Coordinates, ',')), " +
                                    //    "ParsedCoordinates AS (SELECT OBJECTID, FeederId, " +
                                    //    "CAST(LEFT(Coordinate, CHARINDEX(' ', Coordinate) - 1) AS FLOAT) AS X, " +
                                    //    "CAST(SUBSTRING(Coordinate, CHARINDEX(' ', Coordinate) + 1, LEN(Coordinate)) AS FLOAT) AS Y, " +
                                    //    "ROW_NUMBER() OVER (PARTITION BY OBJECTID ORDER BY (SELECT NULL)) AS Sequence " +
                                    //    "FROM SplitCoordinates WHERE Coordinate LIKE '% %') " +
                                    //    "SELECT OBJECTID, FeederId, ROUND(X, 3) AS X, ROUND(Y, 3) AS Y, " +
                                    //    "ROW_NUMBER() OVER (PARTITION BY OBJECTID ORDER BY Sequence) AS Range " +
                                    //    "FROM ParsedCoordinates;", connection);
                                    //     SqlCommand cmd1 = new SqlCommand("select ct.OBJECTID,ct.FeederId,ROUND(SplitXY.X,3) As X,ROUND(SplitXY.Y,3) As Y,ROW_NUMBER() OVER(ORDER BY SplitXY.X, SplitXY.Y) AS Range from (SELECT OBJECTID,FeederId,[LongitudeX],[LatitudeY],[LongitudeX1],[LatitudeY1],SUBSTRING(Coordinates, 2, LEN(Coordinates) - CHARINDEX(',', REVERSE(Coordinates))) AS Coordinates FROM(SELECT OBJECTID,FeederId,[SHAPE].STStartPoint().STX  AS[LongitudeX],[SHAPE].STStartPoint().STY  AS[LatitudeY],[SHAPE].STEndPoint().STX  AS[LongitudeX1],[SHAPE].STEndPoint().STY  AS[LatitudeY1],SUBSTRING(SHAPE.STAsText(), CHARINDEX(',', SHAPE.STAsText()), CHARINDEX(')', SHAPE.STAsText()) - CHARINDEX('(', SHAPE.STAsText()) - 1) AS Coordinates FROM [" + DATABASE + "].[" + SCHEMA + "].[" + vv + "] )AS CoordinatesTable)AS ct CROSS APPLY STRING_SPLIT(Coordinates, ',') As MultiSplitCoordinates CROSS APPLY(SELECT SUBSTRING(TRIM(MultiSplitCoordinates.value), 1, CHARINDEX(' ', TRIM(MultiSplitCoordinates.value))) AS X, SUBSTRING(TRIM(MultiSplitCoordinates.value), CHARINDEX(' ', TRIM (MultiSplitCoordinates.value)) + 1,LEN(TRIM(MultiSplitCoordinates.value))) AS Y) AS SplitXY where MultiSplitCoordinates.value like '% %' and ct.feederId = '" + Feederid + "'", connection);

                                    SqlCommand cmd1 = new SqlCommand(@"WITH CoordinatesTable AS (SELECT  OBJECTID,PhanLoai,
    XuatTuyen,
    [SHAPE].STStartPoint().STX AS [LongitudeX],
    [SHAPE].STStartPoint().STY AS [LatitudeY],
    [SHAPE].STEndPoint().STX AS [LongitudeX1],
    [SHAPE].STEndPoint().STY AS [LatitudeY1],
    CASE 
        WHEN CHARINDEX('(', SHAPE.STAsText()) > 0 
             AND CHARINDEX(')', SHAPE.STAsText()) > CHARINDEX('(', SHAPE.STAsText())
        THEN SUBSTRING(
                 SHAPE.STAsText(), 
                 CHARINDEX('(', SHAPE.STAsText()) + 1, 
                 CHARINDEX(')', SHAPE.STAsText()) - CHARINDEX('(', SHAPE.STAsText()) - 1
             )
        ELSE NULL -- Return NULL if the format is invalid
    END AS Coordinates FROM [" + DATABASE + "].[" + SCHEMA + "].[" + vv + "] WHERE XuatTuyen = '" + Feederid + "' and SHAPE IS NOT NULL), SplitCoordinates AS (SELECT ct.OBJECTID,ct.PhanLoai, ct.XuatTuyen, TRIM(value) AS Coordinate FROM CoordinatesTable ct CROSS APPLY STRING_SPLIT(ct.Coordinates, ',')), ParsedCoordinates AS (SELECT OBJECTID,PhanLoai, XuatTuyen, CAST(LEFT(Coordinate, CHARINDEX(' ', Coordinate) - 1) AS FLOAT) AS X, CAST(SUBSTRING(Coordinate, CHARINDEX(' ', Coordinate) + 1, LEN(Coordinate)) AS FLOAT) AS Y, ROW_NUMBER() OVER (PARTITION BY OBJECTID ORDER BY (SELECT NULL)) - 1 AS Sequence FROM SplitCoordinates WHERE Coordinate LIKE '% %'), NodeRanges AS (SELECT OBJECTID, PhanLoai,XuatTuyen, X, Y, ROW_NUMBER() OVER (PARTITION BY OBJECTID ORDER BY Sequence) - 1 AS Range, MIN(Sequence) OVER (PARTITION BY OBJECTID) AS MinRange, MAX(Sequence) OVER (PARTITION BY OBJECTID) AS MaxRange FROM ParsedCoordinates) SELECT OBJECTID,PhanLoai, XuatTuyen,X,Y, Range FROM NodeRanges WHERE Range > MinRange AND Range < MaxRange ORDER BY OBJECTID, Range;", connection);


                                    // Set the command timeout
                                    cmd1.CommandTimeout = 50000;

                                    SqlDataReader reader = cmd1.ExecuteReader();

                                    DataTable DT1 = new DataTable();
                                    // AD1.Fill(DT1);
                                    DT1.Load(reader);

                                    // Process your DataTable as needed

                                    //OBJECTID,FeederID,FeederID2,FEEDERINFO,SubstationName,SubstationID,SwitchGearObjID,FeederName,FeederSourceInfo,FeederCode,WorkOrderID,ToSubstationID                      
                                    string AssetID = null;
                                    string Range = null;
                                    string X = null;
                                    string Y = null;
                                    string PhanLoai = null;
                                    UTM UTTT = new UTM();
                                    if (DT1.Rows.Count != 0)
                                    {
                                        for (int G = 0; G < DT1.Rows.Count; G++)
                                        {
                                            AssetID = DT1.Rows[G]["OBJECTID"].ToString();
                                            
                                            Range = DT1.Rows[G]["Range"].ToString();
                                            PhanLoai = DT1.Rows[G]["PhanLoai"].ToString();

                                            X = DT1.Rows[G]["X"].ToString();
                                            Y = DT1.Rows[G]["Y"].ToString();
                                            string[] value1 = UTTT.ConvertToUTM(X, Y);
                                            X = value1[0];
                                            Y = value1[1];
                                            if (TT == "F09_PC03_DUONGDAY_TT" && PhanLoai=="3")
                                            {
                                                AssetID = "BB_" + AssetID;
                                            }
                                            if (TT == "F09_PC03_DUONGDAY_TT"&&PhanLoai=="2")
                                            {
                                                AssetID = "CA_" + AssetID;
                                            }
                                            if (TT == "F09_PC03_DUONGDAY_TT"&&PhanLoai=="1")
                                            {
                                                AssetID = "OH_" + AssetID;
                                            }






                                            string DD = "INSERT INTO GIS_INTERMEDIATENODES (AssetID,Range,X,Y) VALUES (@P1,@P2,@P3,@P4)";

                                            // OleDbCommand comr = new OleDbCommand(PP, coon);
                                            OleDbCommand COMT = new OleDbCommand(DD, coon);
                                            COMT.Parameters.AddWithValue("@P1", AssetID);
                                            COMT.Parameters.AddWithValue("@P2", Range);
                                            COMT.Parameters.AddWithValue("@P3", X);
                                            COMT.Parameters.AddWithValue("@P4", Y);
                                            COMT.ExecuteNonQuery();
                                        }
                                    }

                                }
                            }
                        
                        }
                

                    }
                    //if (coon.State == ConnectionState.Open)
                    //{
                    //    Thread.Sleep(1000);
                    //    coon.Close();
                    //}
                }
                catch(Exception ex)
                {
                    TechError erro = new TechError();
                    erro.ExceptionErrorHandle(getfile, ex.ToString());

                }
              
            }

        } //use

        public void GIS_CONSUMERMETER(string getfile, ConfigFileData hh, string Feederid, OleDbConnection coon)
        {
            try
            {
                string Configpath = getfile + @"\ConfigFile\GIS_Table.xml";
                string nnmyqar = string.Empty;
                string Name = string.Empty;
                string newquarry = string.Empty;
                string newquarry2 = string.Empty;
                string data = string.Empty;
                string insertvalue = string.Empty;
                string insertColumn = string.Empty;
                if (File.Exists(Configpath))
                {
                    string quarry = string.Empty;
                    string quarry2 = string.Empty;
                    
                    DataSet ds = new DataSet();

                    ds.ReadXml(Configpath);
                    List<string> mylist = new List<string>();
                    List<string> fieldlist = new List<string>();
                    List<string> fieldlist1 = new List<string>();
                    int v = 0;
                    int hhh = 0;
                    int cc = 0;
                    int bb = 0;
                    bool gg = true;
                    string nnmyqarv = string.Empty;
                    string kk = ds.Tables["CONSUMERMETER"].ToString();
                    string CI = ds.Tables["CONSUMERINFO"].ToString();
                    // DataTable dt1 = new DataTable(ds.Tables["CONSUMERMETER"].Columns);
                    //for (int i = 0; i < ds.Tables.Count; i++)
                    //{

                    //}
                    foreach (DataTable dt in ds.Tables)
                    {
                        string mmk = dt.TableName;
                        if (kk == mmk)
                        {
                            string quary1 = string.Empty;
                            string quarry1 = string.Empty;
                            string nnmyqarv1 = string.Empty;
                            string newquarry1 = string.Empty;
                            string nnmyqar1 = string.Empty;
                            mylist.Add(kk);
                            int fieldcount = dt.Columns.Count;
                            for (int i = 0; i < fieldcount; i++)
                            {
                                string vv = dt.Columns[i].ToString();
                                fieldlist1.Add(vv);

                            }
                            for (int k = 0; k < fieldlist1.Count; k++)
                            {
                                string nn = fieldlist1[k];
                                quary1 += fieldlist1[k] + " Varchar(250)" + ',';
                                nnmyqarv1 += nn + ',';
                            }
                            newquarry1 = quary1.Remove(quary1.Length - 1);
                            nnmyqar1 = nnmyqarv1.Remove(nnmyqarv1.Length - 1);
                            string myquarry2 = "Create Table  GIS_" + mmk + " (" + newquarry1 + ")";
                            OleDbCommand cmm = new OleDbCommand(myquarry2, coon);

                            cmm.ExecuteNonQuery();


                        }

                        if (CI == mmk)
                        {
                            mylist.Add(CI);
                            int fieldcount = dt.Columns.Count;
                            for (int i = 0; i < fieldcount; i++)
                            {
                                string vv = dt.Columns[i].ToString();
                                fieldlist.Add(vv);

                            }
                            for (int k = 0; k < fieldlist.Count; k++)
                            {
                                string nn = fieldlist[k];
                                quarry += fieldlist[k] + " Varchar(250)" + ',';
                                nnmyqarv += nn + ',';
                            }
                            newquarry2 = quarry.Remove(quarry.Length - 1);
                            nnmyqar = nnmyqarv.Remove(nnmyqarv.Length - 1);
                            string myquarry2 = "Create Table  GIS_" + mmk + " (" + newquarry2 + ")";
                            OleDbCommand cmm = new OleDbCommand(myquarry2, coon);
                            cmm.ExecuteNonQuery();
                            string updateCI = "ALTER TABLE GIS_" + mmk + " ADD FeederID varchar(255) ,BuildingID varchar(255) ";
                            OleDbCommand cmm1 = new OleDbCommand(updateCI, coon);
                            cmm1.ExecuteNonQuery();

                        }
                    }

                }


                string connectionString = @"Data Source=" + hh.Gisservername +                       //Create Connection string
                      ";database=" + hh.GisDatabase +
                      ";User ID=" + hh.Gisusername +
                      ";Password=" + hh.Gispassword;
                string schema = hh.GisSchema_Name;
                using (SqlConnection obj = new SqlConnection(connectionString))
                {
                    try
                    {
                        if (obj.State == ConnectionState.Closed)
                        {
                            obj.Open();
                        }

                        string quar = "select CI.OBJECTID,CI.ConsumerNumber,CI.TypeOfConsumer,CI.TypeOfSupply,CI.ConnectedPhase,CI.SanctionedLoad,CI.SectionCode,CM.Connectedto11KVFeeder,SP.BuildingID from " + schema + ".CONSUMERINFO as CI Inner join " + schema+ ".CONSUMERMETER as CM on CI.ConsumerNumber=CM.ConsumerNumber Inner join " + schema + ".SERVICEPOINT as SP On SP.BuildingID= CM.BuildingID where SP.FeederId='" + Feederid+"'";
                        //string quar = "select OBJECTID,ConsumerNumber,TypeOfSupply,ConnectedPhase,SanctionedLoad,SectionCode from CONSUMERINFO";
                        SqlCommand COMM = new SqlCommand(quar, obj);
                        SqlDataAdapter ad = new SqlDataAdapter(COMM);
                        DataTable dtt = new DataTable();
                        ad.Fill(dtt);

                        for (int cc = 0; cc < dtt.Rows.Count; cc++)
                        {
                            string OBJECTID = dtt.Rows[cc]["OBJECTID"].ToString();
                            string ConsumerNumber = dtt.Rows[cc]["ConsumerNumber"].ToString();
                            string TypeOfConsumer = dtt.Rows[cc]["TypeOfConsumer"].ToString();
                            string TypeOfSupply = dtt.Rows[cc]["TypeOfSupply"].ToString();
                            string ConnectedPhase = dtt.Rows[cc]["ConnectedPhase"].ToString();
                            string SanctionedLoad = dtt.Rows[cc]["SanctionedLoad"].ToString();
                            string SectionCode = dtt.Rows[cc]["SectionCode"].ToString();
                            string FeederID = dtt.Rows[cc]["Connectedto11KVFeeder"].ToString();
                            string BuildingID = dtt.Rows[cc]["BuildingID"].ToString();
                             

                            string insertquarr = "Insert Into GIS_CONSUMERINFO (OBJECTID,ConsumerNumber,TypeOfSupply,ConnectedPhase,SanctionedLoad,SectionCode,FeederID,BuildingID,TypeOfConsumer ) Values ('" + OBJECTID + "','" + ConsumerNumber + "','" + TypeOfSupply + "','" + ConnectedPhase + "','" + SanctionedLoad + "','" + SectionCode + "','" + FeederID + "','" + BuildingID + "','" + TypeOfConsumer + "' )";
                            OleDbCommand cvvm = new OleDbCommand(insertquarr, coon);
                            cvvm.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        TechError erro = new TechError();
                        erro.ExceptionErrorHandle(getfile, ex.ToString());
                    }

                }




            }
            catch (Exception EX)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(getfile, EX.ToString());
            }
        }

        public void GetGIS_UpdateData(string getfile, string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            try
            {
                //if (connn2.State != ConnectionState.Open)
                //{
                //    connn2.Open();
                //}
                string mdbpath = getfile + ConfigurationManager.AppSettings["Connection1"];

                // string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                OleDbConnection connn21 = new OleDbConnection(connectionstring12);


                string myqrr2 = "Select*from GIS_DOMAIN";

                OleDbCommand cmdp = new OleDbCommand(myqrr2, connn21);
                if (connn21.State != ConnectionState.Open)
                {
                    connn21.Open();
                }

                cmdp.ExecuteNonQuery();
                OleDbDataAdapter adr = new OleDbDataAdapter(cmdp);

                DataTable DT1r = new DataTable();
                adr.Fill(DT1r);

                //string[] Myarray = { "GIS_FUSE", "GIS_Switchgear", "GIS_SWITCH", "GIS_HTSERVICEPOINT", "GIS_LTSERVICEPOINT", "GIS_LTCONDUCTOR", "GIS_HTCONDUCTOR", "GIS_BUSBAR", "GIS_HTCABLE", "GIS_LTCABLE", "GIS_DISTRIBUTIONTRANSFORMER" };
                string[] Myarray = { "GIS_F01_PC03_THIETBIDONGCAT_TT", "GIS_F04_PC03_MAYBIENAP_TT", "GIS_F09_PC03_DUONGDAY_TT", "GIS_PC03_DIEMXUATTUYEN", "GIS_F03_PC03_TUBU_TT" };
                for (int i = 0; i < Myarray.Length; i++)
                {
                    string arr = Myarray[i];

                    for (int p = 0; p < DT1r.Rows.Count; p++)
                    {
                        bool che = false;
                        string Code = DT1r.Rows[p]["Code"].ToString();
                        string Type = DT1r.Rows[p]["Type"].ToString();
                        string Value1 = DT1r.Rows[p]["Value1"].ToString();
                        int K = 0;
                        if (Type== "PC_DIENAP")
                        {

                        }
                        if (Type == "PC_DIENAP" && arr!= "GIS_F04_PC03_MAYBIENAP_TT")
                        {
                            string update2 = "UPDATE " + arr + " SET DienAp = '" + Value1 + "KV' WHERE DienAp='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                        if (Type == "PC_DIENAP" && arr == "GIS_F04_PC03_MAYBIENAP_TT")
                        {
                            
                            string update2 = "UPDATE " + arr + " SET CapDienApVao = '" + Value1 + "KV' WHERE CapDienApVao='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_DIENAP" && arr == "GIS_F04_PC03_MAYBIENAP_TT")
                        {
                            if (Value1.Contains(','))
                            {
                                Value1 = Value1.Replace(',', '.');
                            }
                            string update2 = "UPDATE " + arr + " SET CapDienApRa = '" + Value1 + "KV' WHERE CapDienApRa='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_PHADAUNOI" && arr== "GIS_F09_PC03_DUONGDAY_TT")
                        {
                            if (Value1.Contains(','))
                            {
                                Value1 = Value1.Replace(',', '.');
                            }
                            string update2 = "UPDATE " + arr + " SET PhaDauNoi = '" + Value1 + "' WHERE PhaDauNoi='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                        if (Type == "TT_LOAIDAYDAN" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {
                           
                            string update2 = "UPDATE " + arr + " SET LoaiDayDan = '" + Value1 + "' WHERE LoaiDayDan='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "TT_MAHIEUDAY" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET MaHieu = '" + Value1 + "' WHERE MaHieu='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_TIETDIENDAY" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET TietDienDay = '" + Value1 + "' WHERE TietDienDay='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "TT_DIENAP" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET DienAp = '" + Value1 + "' WHERE DienAp='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                    }
                }
                //    if (connn2.State == ConnectionState.Open)
                //    {
                //        connn2.Close();
                //    }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(getfile, ex.ToString());
            }

        } //use
        public void GetGIS_UpdateVoltage(string getfile, string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            try
            {
                //if (connn2.State != ConnectionState.Open)
                //{
                //    connn2.Open();
                //}
                string mdbpath = getfile + ConfigurationManager.AppSettings["Connection1"];

                // string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                OleDbConnection connn21 = new OleDbConnection(connectionstring12);


                string myqrr2 = "Select*from GIS_DOMAIN";

                OleDbCommand cmdp = new OleDbCommand(myqrr2, connn21);
                if (connn21.State != ConnectionState.Open)
                {
                    connn21.Open();
                }

                cmdp.ExecuteNonQuery();
                OleDbDataAdapter adr = new OleDbDataAdapter(cmdp);

                DataTable DT1r = new DataTable();
                adr.Fill(DT1r);

                //string[] Myarray = { "GIS_FUSE", "GIS_Switchgear", "GIS_SWITCH", "GIS_HTSERVICEPOINT", "GIS_LTSERVICEPOINT", "GIS_LTCONDUCTOR", "GIS_HTCONDUCTOR", "GIS_BUSBAR", "GIS_HTCABLE", "GIS_LTCABLE", "GIS_DISTRIBUTIONTRANSFORMER" };
                string[] Myarray = { "TGDEVICE_SWITCH" };
                for (int i = 0; i < Myarray.Length; i++)
                {
                    string arr = Myarray[i];

                    for (int p = 0; p < DT1r.Rows.Count; p++)
                    {
                        bool che = false;
                        string Code = DT1r.Rows[p]["Code"].ToString();
                        string Type = DT1r.Rows[p]["Type"].ToString();
                        string Value1 = DT1r.Rows[p]["Value1"].ToString();
                        int K = 0;
                        if (Type == "PC_DIENAP")
                        {

                        }
                        if (Type == "PC_DIENAP" && arr != "GIS_F04_PC03_MAYBIENAP_TT")
                        {
                            string update2 = "UPDATE " + arr + " SET DienAp = '" + Value1 + "KV' WHERE DienAp='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                        if (Type == "PC_DIENAP" && arr == "GIS_F04_PC03_MAYBIENAP_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET CapDienApVao = '" + Value1 + "KV' WHERE CapDienApVao='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_DIENAP" && arr == "GIS_F04_PC03_MAYBIENAP_TT")
                        {
                            if (Value1.Contains(','))
                            {
                                Value1 = Value1.Replace(',', '.');
                            }
                            string update2 = "UPDATE " + arr + " SET CapDienApRa = '" + Value1 + "KV' WHERE CapDienApRa='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_PHADAUNOI" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {
                            if (Value1.Contains(','))
                            {
                                Value1 = Value1.Replace(',', '.');
                            }
                            string update2 = "UPDATE " + arr + " SET PhaDauNoi = '" + Value1 + "' WHERE PhaDauNoi='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                        if (Type == "TT_LOAIDAYDAN" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET LoaiDayDan = '" + Value1 + "' WHERE LoaiDayDan='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "TT_MAHIEUDAY" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET MaHieu = '" + Value1 + "' WHERE MaHieu='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }
                        if (Type == "PC_TIETDIENDAY" && arr == "GIS_F09_PC03_DUONGDAY_TT")
                        {

                            string update2 = "UPDATE " + arr + " SET TietDienDay = '" + Value1 + "' WHERE TietDienDay='" + Code + "'";

                            OleDbCommand cmd2x1 = new OleDbCommand(update2, connn2);

                            cmd2x1.ExecuteNonQuery();
                        }

                    }
                }
                //    if (connn2.State == ConnectionState.Open)
                //    {
                //        connn2.Close();
                //    }
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(getfile, ex.ToString());
            }

        } //use

        public void GIS_CONSUMERMETERINFO(string getfile, ConfigFileData hh, string Feederid, OleDbConnection coon)
        {
            try
            {
                string connectionString = @"Data Source=" + hh.Gisservername +                       //Create Connection string
                      ";database=" + hh.GisDatabase +
                      ";User ID=" + hh.Gisusername +
                      ";Password=" + hh.Gispassword;
                using (SqlConnection obj = new SqlConnection(connectionString))
                {
                    string myquarry2 = "select * from CONSUMERINFO ";
                    SqlCommand cmm = new SqlCommand(myquarry2, obj);
                    // obj.Open();
                    if (obj.State != ConnectionState.Open)
                    {
                        obj.Open();
                    }

                    SqlDataAdapter AD1 = new SqlDataAdapter(cmm);
                    DataTable DTt1 = new DataTable();
                    AD1.Fill(DTt1);

                    for (int cc = 0; cc < DTt1.Rows.Count; cc++)
                    {
                        string SanctionedLoadKW = DTt1.Rows[cc]["SanctionedLoadKW"].ToString();
                        string ConnectedLoad = DTt1.Rows[cc]["ConnectedLoad"].ToString();
                        string ServicePointID = DTt1.Rows[cc]["ServicePointID"].ToString();
                        string insertquarr = "Insert Into GIS_CONSUMERMETER (SanctionedLoadKW,ConnectedLoad,ServicePointID) Values ('" + SanctionedLoadKW + "','" + ConnectedLoad + "','" + ServicePointID + "')";
                        OleDbCommand cvvm = new OleDbCommand(insertquarr, coon);
                        cvvm.ExecuteNonQuery();
                    }

                }
                    

            }
            catch (Exception ex)
            {

            }


            try
            {
                string Configpath = getfile + @"\ConfigFile\GIS_Table.xml";
                string nnmyqar = string.Empty;
                string Name = string.Empty;
                string newquarry = string.Empty;
                string newquarry2 = string.Empty;
                string data = string.Empty;
                string insertvalue = string.Empty;
                string insertColumn = string.Empty;
                if (File.Exists(Configpath))
                {
                    string quarry = string.Empty;
                    string quarry2 = string.Empty;
                    DataSet ds = new DataSet();

                    ds.ReadXml(Configpath);
                    List<string> mylist = new List<string>();
                    List<string> fieldlist = new List<string>();
                    int v = 0;
                    int hhh = 0;
                    int cc = 0;
                    int bb = 0;
                    bool gg = true;
                    string nnmyqarv = string.Empty;
                    string kk = ds.Tables["CONSUMERINFO"].ToString();
                    // DataTable dt1 = new DataTable(ds.Tables["CONSUMERMETER"].Columns);
                    //for (int i = 0; i < ds.Tables.Count; i++)
                    //{

                    //}
                    foreach (DataTable dt in ds.Tables)
                    {
                        string mmk = dt.TableName;
                        if (kk == mmk)
                        {
                            mylist.Add(kk);
                            int fieldcount = dt.Columns.Count;
                            for (int i = 0; i < fieldcount; i++)
                            {
                                string vv = dt.Columns[i].ToString();
                                fieldlist.Add(vv);

                            }
                            for (int k = 0; k < fieldlist.Count; k++)
                            {
                                string nn = fieldlist[k];
                                quarry += fieldlist[k] + " Varchar(250)" + ',';
                                nnmyqarv += nn + ',';
                            }
                            newquarry2 = quarry.Remove(quarry.Length - 1);
                            nnmyqar = nnmyqarv.Remove(nnmyqarv.Length - 1);
                            string myquarry2 = "Create Table  GIS_" + mmk + " (" + newquarry2 + ")";
                            OleDbCommand cmm = new OleDbCommand(myquarry2, coon);

                            cmm.ExecuteNonQuery();


                        }


                    }

                }

                string quar = "select FEEDERID,ConsumerNumber from CONSUMERMETER";
                OleDbCommand COMM = new OleDbCommand(quar, coon);
                OleDbDataAdapter ad = new OleDbDataAdapter(COMM);
                DataTable dtt = new DataTable();
                ad.Fill(dtt);

                for (int cc = 0; cc < dtt.Rows.Count; cc++)
                {
                    string SanctionedLoadKW = dtt.Rows[cc]["SanctionedLoadKW"].ToString();
                    string ConnectedLoad = dtt.Rows[cc]["ConnectedLoad"].ToString();
                    string ServicePointID = dtt.Rows[cc]["ServicePointID"].ToString();
                    string insertquarr = "Insert Into GIS_CONSUMERMETER (SanctionedLoadKW,ConnectedLoad,ServicePointID) Values ('" + SanctionedLoadKW + "','" + ConnectedLoad + "','" + ServicePointID + "')";
                    OleDbCommand cvvm = new OleDbCommand(insertquarr, coon);
                    cvvm.ExecuteNonQuery();
                }


                //}


            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(getfile, ex.ToString());
            }
        }


    }

}
