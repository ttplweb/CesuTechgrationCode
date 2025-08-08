using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class UpdatedeviceLocation
    {

        public void UpdateLoaction(OleDbConnection conn)
        {
            try
            {
               
                string[] myarray = { "GIS_F04_PC03_MAYBIENAP_TT", "GIS_F01_PC03_THIETBIDONGCAT_TT", "GIS_PC03_DIEMXUATTUYEN", "GIS_F03_PC03_TUBU_TT", "GIS_F01_PC03_THIETBIDONGCAT_TT", "GIS_F01_PC03_THIETBIDONGCAT_TT", "GIS_F01_PC03_THIETBIDONGCAT_TT","GIS_BATTERYSTORAGE", "GIS_SOLAR" , "GIS_WINDMILL" };
               // string[] myarray2 = {"TGDEVICE_SWITCH", "TGDEVICE_FUSE", "TGDEVICE_DISTRIBUTIONTRANSFORMER" };
                string[] myarray2 = { "TGDEVICE_DISTRIBUTIONTRANSFORMER", "TGDEVICE_CIRCUITBREAKER", "TGDEVICE_CIRCUITBREAKER", "TGDEVICE_SHUNTCAPACITOR", "TGDEVICE_SWITCH", "TGDEVICE_FUSE",  "TGDEVICE_RECLOSER", "TGDEVICE_BATTERYSTORAG", "TGDEVICE_SOLAR", "TGDEVICE_WINDMILL" };

                for (int k = 0; k < myarray.Length; k++)
                {
                    string arr = myarray[k];
                    string arr1 = myarray2[k];
                    string str = "Select  OBJECTID as GISID,X,Y from " + arr + "";
                    OleDbCommand com = new OleDbCommand(str, conn);

                    


                    // OlebdDataAdapter ad = new OlebdDataAdapter(com);
                    OleDbDataReader dr = com.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);


                    string str1 = @"SELECT FromNodeID, ToNodeID, SectionID 
FROM[TEMPSECTION-NOTBREAKED]
UNION ALL
SELECT FromNodeID, ToNodeID, SectionID
FROM[TEMPSECTION-BREAKED]";
                   // string str1 = "Select FromNodeID,ToNodeID,SectionID FROM [TEMPSECTION-NOTBREAKED]";
                    OleDbCommand com1 = new OleDbCommand(str1, conn);
                  
                    //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                    OleDbDataReader dr1 = com1.ExecuteReader();
                    DataTable dt1 = new DataTable();
                    dt1.Load(dr1);
                    bool ss = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String X = dt.Rows[i]["X"].ToString();
                        String Y = dt.Rows[i]["Y"].ToString();
                        String NODE = X + "_" + Y;

                        String GISID = dt.Rows[i]["GISID"].ToString();

                        for (int K = 0; K < dt1.Rows.Count; K++)
                        {
                            String FromNodeID = dt1.Rows[K]["FromNodeID"].ToString();
                            String ToNodeID = dt1.Rows[K]["ToNodeID"].ToString();
                            String SectionID = dt1.Rows[K]["SectionID"].ToString();

                            if (FromNodeID == NODE || ToNodeID == NODE)
                            {
                                String Location = string.Empty;


                                if (arr1== "TGDEVICE_CIRCUITBREAKER" && ss==true)
                                {
                                    Thread.Sleep(1000);
                                    ss = false;
                                }
                                if (FromNodeID == NODE)
                                {
                                    Location = "S";
                                }
                                else
                                {
                                    Location = "L";
                                }

                                if (arr1 == "TGDEVICE_DISTRIBUTIONTRANSFORMER")
                                {

                                    string dtupdate = " ";

                                    string update221 = "UPDATE " + arr1 + " SET Location = '" + Location + "' WHERE SectionID='" + GISID + "'";
                                    string update211 = "UPDATE " + arr1 + " SET SectionID = '" + SectionID + "' WHERE SectionID='" + GISID + "'";

                                    OleDbCommand cmd2x1121 = new OleDbCommand(update221, conn);
                                    OleDbCommand cmd2x111 = new OleDbCommand(update211, conn);

                                    cmd2x1121.ExecuteNonQuery();
                                    cmd2x111.ExecuteNonQuery();



                                }

                                else
                                {
                                    string update22 = "UPDATE " + arr1 + " SET Location = '" + Location + "' WHERE SectionID='" + GISID + "'";
                                    string update21 = "UPDATE " + arr1 + " SET SectionID = '" + SectionID + "' WHERE SectionID='" + GISID + "'";

                                    OleDbCommand cmd2x112 = new OleDbCommand(update22, conn);
                                    OleDbCommand cmd2x11 = new OleDbCommand(update21, conn);

                                    cmd2x112.ExecuteNonQuery();
                                    cmd2x11.ExecuteNonQuery();
                                }
                                 
                            }


                        }



                    }
                }
               
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.ToString());
            }

        }

         
    }
}
