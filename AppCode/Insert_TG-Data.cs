using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;

namespace TechGration.AppCode
{
    class Insert_TG_Data
    {

        public void Insert_TGDATA(string mdb_Ptah, ConfigFileData vk, string Feederid, string getfile, string usertype, OleDbConnection connn2)
        {
 

            try
            {
                 
                string loadeq = "create table TGSOURCE_LOADEQUIVALENT (NodeID varchar(250),LoadModelName varchar(250),Format varchar(250),Value1A varchar(250),Value1B varchar(250),Value1C varchar(250),Value2A varchar(250),Value2B varchar(250),Value2C varchar(250),ValueSinglePhaseCT11 varchar(250),ValueSinglePhaseCT12 varchar(250),ValueSinglePhaseCT21 varchar(250),ValueSinglePhaseCT22 varchar(250))";
                OleDbCommand comeq = new OleDbCommand(loadeq, connn2);
                comeq.ExecuteNonQuery();

                string source = "create table TGSOURCE_SOURCE (SourceID varchar(250),DeviceNumber varchar(250),NodeID varchar(250),NetworkID varchar(250),OperatingVoltageA varchar(250),OperatingVoltageB varchar(250),OperatingVoltageC varchar(250),SinglePhaseCenterTap varchar(250),CenterTapPhase varchar(250))";
                OleDbCommand com116 = new OleDbCommand(source, connn2);
                com116.ExecuteNonQuery();

                string Tgfeeder = "create table [TGFEEDER_FORMAT_FEEDER] (NetworkID varchar(250),HeadNodeID varchar(250),CoordSet varchar(250),Years varchar(250),Description varchar(250),Color varchar(250),LoadFactor varchar(250),LossLoadFactorK varchar(250),Group1 varchar(250),Group2 varchar(250),Group3 varchar(250),Group4 varchar(250),Group5 varchar(250),TagText varchar(250),TagProperties varchar(250),TagDeltaX varchar(250),TagDeltaY varchar(250),TagAngle varchar(250),TagAlignment varchar(250),TagBorder varchar(250),TagBackground varchar(250),TagTextColor varchar(250),TagBorderColor varchar(250),TagBackgroundColor varchar(250),TagLocation varchar(250),TagFont varchar(250),TagTextSize varchar(250),TagOffset varchar(250),Version varchar(250),EnvironmentID varchar(250),NetworkType varchar(250))";
                OleDbCommand com1161 = new OleDbCommand(Tgfeeder, connn2);
                com1161.ExecuteNonQuery();

                string Tempnode = "create table [TempNode] (NodeID varchar(250),X varchar(250),Y varchar(250))";
                OleDbCommand coms = new OleDbCommand(Tempnode, connn2);
                coms.ExecuteNonQuery();


                string TGnode = "create table [TGNODE_INTERMEDIATENODES] (SectionID varchar(250),SeqNumber varchar(250),CoordX varchar(250),CoordY varchar(250),IsBreakPoint varchar(250),BreakPointLocation varchar(250))";
                OleDbCommand coms1 = new OleDbCommand(TGnode, connn2);
                coms1.ExecuteNonQuery();


                string Tempsec = "create table [TEMPSECTION-BREAKED] (SectionID varchar(250),FromNodeID varchar(250),FromNodeIndex varchar(250),ToNodeID varchar(250),ToNodeIndex varchar(250),Phase varchar(250),ZoneID varchar(250),SubNetWorkID varchar(250),EnvironmentID varchar(250))";
                OleDbCommand comm = new OleDbCommand(Tempsec, connn2);
                comm.ExecuteNonQuery();

                string temp = "create table [TEST-BREAKED] (AssetID varchar(250),Range varchar(250),X varchar(250),Y varchar(250),Phasedesignation varchar(250))";
                OleDbCommand comm1 = new OleDbCommand(temp, connn2);
                comm1.ExecuteNonQuery();

                string updateQuery = "create table [TEMPSECTION-NOTBREAKED] (SectionID varchar(250),FromNodeID varchar(250),FromNodeIndex varchar(250),ToNodeID varchar(250),ToNodeIndex varchar(250),Phase varchar(250),ZoneID varchar(250),SubNetWorkID varchar(250),EnvironmentID varchar(250))";
                OleDbCommand com11 = new OleDbCommand(updateQuery, connn2);
                com11.ExecuteNonQuery();

                string updateQuery1 = "create table [TGSOURCE_EQUIVALENT] (NodeID varchar(250),LoadModelName varchar(250),Voltage varchar(250),OperatingAngle1 varchar(250),OperatingAngle2 varchar(250),OperatingAngle3 varchar(250),PositiveSequenceResistance varchar(250),PositiveSequenceReactance varchar(250),NegativeSequenceResistance varchar(250),NegativeSequenceReactance varchar(250),ZeroSequenceResistance varchar(250),ZeroSequenceReactance varchar(250),OperatingVoltage1 varchar(250),OperatingVoltage2 varchar(250),OperatingVoltage3 varchar(250),BaseMVA varchar(250),ImpedanceUnit varchar(250))";
                OleDbCommand com111 = new OleDbCommand(updateQuery1, connn2);
                com111.ExecuteNonQuery();

                string TGSOURCE_HEADNODES = "create table [TGSOURCE_HEADNODES] (NodeID varchar(250),NetworkID varchar(250),ConnectorIndex varchar(250),StructureID varchar(250),HarmonicEnveloppe varchar(250),EquivalentSourceConfiguration varchar(250),EquivalentSourceSinglePhaseCT varchar(250),EquivSourceCenterTapPhase varchar(250),BackgroundHarmonicVoltage varchar(250))";
                OleDbCommand com112 = new OleDbCommand(TGSOURCE_HEADNODES, connn2);
                com112.ExecuteNonQuery();


                string GIS_SUBSTATION = "create table [GIS_SUBSTATION] (Name varchar(250),NIN varchar(250))";
                OleDbCommand com112s = new OleDbCommand(GIS_SUBSTATION, connn2);
                com112s.ExecuteNonQuery();

                string GIS_SUNCAPSHUNTCAPACITOR = @"CREATE TABLE TGDEVICE_SHUNTCAPACITOR (
    SectionID VARCHAR(250),
    DeviceNumber VARCHAR(250),
    DeviceStage VARCHAR(250),
    Location VARCHAR(250),
    [Connection] VARCHAR(250),
    FixedKVARA VARCHAR(250),
    FixedKVARB VARCHAR(250),
    FixedKVARC VARCHAR(250),
    FixedLossesA VARCHAR(250),
    FixedLossesB VARCHAR(250),
    FixedLossesC VARCHAR(250),
    SwitchedKVARA VARCHAR(250),
    SwitchedKVARB VARCHAR(250),
    SwitchedKVARC VARCHAR(250),
    SwitchedLossesA VARCHAR(250),
    SwitchedLossesB VARCHAR(250),
    SwitchedLossesC VARCHAR(250),
    ByPhase VARCHAR(250),
    VoltageOverride VARCHAR(250),
    VoltageOverrideOn VARCHAR(250),
    VoltageOverrideOff VARCHAR(250),
    VoltageOverrideDeadband VARCHAR(250),
    [KV] VARCHAR(250),
    [Control] VARCHAR(250),
    OnValueA VARCHAR(250),
    OnValueB VARCHAR(250),
    OnValueC VARCHAR(250),
    OffValueA VARCHAR(250),
    OffValueB VARCHAR(250),
    OffValueC VARCHAR(250),
    SwitchingMode VARCHAR(250),
    InitiallyClosedPhase VARCHAR(250),
    ControllingPhase VARCHAR(250),
    SensorLocation VARCHAR(250),
    ControlledNodeId VARCHAR(250),
    PythonDeviceScriptID VARCHAR(250),
    ShuntCapacitorID VARCHAR(250),
    [ConnectionStatus] VARCHAR(250),
    [CTConnection] VARCHAR(250),
    InterruptingRating VARCHAR(250),
    EquipmentId VARCHAR(250)
);";
                OleDbCommand comsun = new OleDbCommand(GIS_SUNCAPSHUNTCAPACITOR, connn2);
                comsun.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + Feederid);
            }

            try
            {
                string vdggd = "select SubstationId from GIS_CIRCUITSOURCE";
                OleDbCommand coms11 = new OleDbCommand(vdggd, connn2);
                OleDbDataAdapter adb = new OleDbDataAdapter(coms11);
                DataTable st1 = new DataTable();
                adb.Fill(st1);
                string SubstationId = st1.Rows[0][0].ToString();

                string connectionString = @"Data Source=" + vk.Gisservername +                       //Create Connection string
                     ";database=" + vk.GisDatabase +
                     ";User ID=" + vk.Gisusername +
                     ";Password=" + vk.Gispassword;
                using (SqlConnection obj = new SqlConnection(connectionString))
                {
                    if (obj.State == ConnectionState.Closed)
                    {
                        obj.Open();
                    }
                    string qrr = "Select Name,NIN  from SUBSTATION Where NIN='" + SubstationId + "'";
                    SqlCommand com = new SqlCommand(qrr, obj);
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Name = dt.Rows[i]["Name"].ToString();
                        string NIN = dt.Rows[i]["NIN"].ToString();

                        string llG1 = "insert into [GIS_SUBSTATION](Name,NIN) values ('" + Name + "','" + NIN + "')";

                        OleDbCommand com33G = new OleDbCommand(llG1, connn2);
                        com33G.ExecuteNonQuery();
                    }

                }



            }
            catch (Exception ex)
            {


            }


            try
            {
                string vdggd = "select*from GIS_INTERMEDIATENODES";
                OleDbCommand coms11 = new OleDbCommand(vdggd, connn2);
                OleDbDataAdapter adb = new OleDbDataAdapter(coms11);
                DataTable st1 = new DataTable();
                adb.Fill(st1);
                for (int kk = 0; kk < st1.Rows.Count; kk++)
                {
                    string SectionID = st1.Rows[kk]["AssetID"].ToString();
                    string SeqNumber = st1.Rows[kk]["Range"].ToString();
                    string CoordX = st1.Rows[kk]["X"].ToString();
                    string CoordY = st1.Rows[kk]["Y"].ToString();
                    string llG = "insert into [TGNODE_INTERMEDIATENODES](SectionID,SeqNumber,CoordX,CoordY,IsBreakPoint,BreakPointLocation) values ('" + SectionID + "','" + SeqNumber + "','" + CoordX + "','" + CoordY + "','" + "" + "','" + "" + "' )";

                    OleDbCommand com33G = new OleDbCommand(llG, connn2);
                    com33G.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            try
            {
                string lklj2 = "Select FromNodeID,ToNodeID from [TEMPSECTION-BREAKED]";

                OleDbCommand com033j2 = new OleDbCommand(lklj2, connn2);
                OleDbDataAdapter adx = new OleDbDataAdapter(com033j2);
                DataTable dtt = new DataTable();
                adx.Fill(dtt);
                for (int ii = 0; ii < dtt.Rows.Count; ii++)
                {
                    string ToNodeID1 = dtt.Rows[ii]["ToNodeID"].ToString();
                    string FromNodeID1 = dtt.Rows[ii]["FromNodeID"].ToString();

                    if (ToNodeID1 != FromNodeID1)
                    {

                        String hfjf = ToNodeID1;
                        if (hfjf == "368847.767_2263720.0007")
                        {

                        }
                        string[] value = hfjf.Split('_');
                        string node1 = value[0];
                        string node2 = value[1];
                        string llk1 = "insert into [TempNode](NodeID,X,Y) values('" + hfjf + "','" + node1 + "','" + node2 + "')";

                        OleDbCommand com33k1 = new OleDbCommand(llk1, connn2);
                        com33k1.ExecuteNonQuery();

                        String gdhh = FromNodeID1;
                        string[] value2 = gdhh.Split('_');
                        string node11 = value2[0];
                        string node22 = value2[1];
                        string llk11 = "insert into [TempNode](NodeID,X,Y) values('" + gdhh + "','" + node11 + "','" + node22 + "')";

                        OleDbCommand com33k11 = new OleDbCommand(llk11, connn2);
                        com33k11.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString() + Feederid);
            }

            try
            {

                string mdbfile1 = getfile + ConfigurationManager.AppSettings["Connection1"];


                string connectionstring1 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile1;

                OleDbConnection conq = new OleDbConnection(connectionstring1);
                conq.Open();


               // string lv = @"SELECT * FROM FeederList WHERE NetworkID LIKE '%"+Feederid+"%' ";
                string lv = @"SELECT * FROM GIS_PC03_DIEMXUATTUYEN WHERE XuatTuyen ='" + Feederid+"';";


                OleDbCommand CMMM = new OleDbCommand(lv, connn2);
                OleDbDataAdapter ad1 = new OleDbDataAdapter(CMMM);
                DataTable dt1 = new DataTable();
                ad1.Fill(dt1);
                conq.Close();
                for (int k = 0; k < dt1.Rows.Count; k++)
                {
                    string SubstationName = string.Empty;
                    // string Cir_Div_SubD_BU_Sec = dt1.Rows[k]["Cir_Div_SubD_BU_Sec"].ToString();
                    string voltage = dt1.Rows[k]["DienAp"].ToString();
                    if (voltage=="6")
                    {
                        voltage = "22KV";
                    }
                    //string Cir_Div_SubD_BU_Sec = dt1.Rows[k]["Cir_Div_SubD_BU_Sec"].ToString().Trim();
                    //string[] myyarr = Cir_Div_SubD_BU_Sec.Split('/');
                    string Circle = voltage;
                    string Division = voltage;
                    string Subdivision = voltage;
                    string FeederName = voltage;
                    string Region_Name = string.Empty;
                    string Zone_Name = string.Empty;
                    //Froup5 assw.[Region_Name],sw.[Zone_Name]
                    
                    SubstationName = voltage;
                    string swObjectid = dt1.Rows[k]["OBJECTID"].ToString();
                    //string swObjectid = "SG_"+ OBJECTID;
                    string swFeederid = dt1.Rows[k]["XuatTuyen"].ToString();
                   

                    //string csswitchgear = dt1.Rows[k]["SwitchGearObjID"].ToString();
                    //string csFeederid = dt1.Rows[k]["FeederID"].ToString() + "_" + FeederName;
                    string NodeX = dt1.Rows[k]["X"].ToString();
                    string NodeY = dt1.Rows[k]["Y"].ToString();
                
                    voltage = updatevoltagecodevalue(voltage);
                    string CircleCode = Division;
                    string SNDCode = "HPC";
                    string NEWSUB = SubstationName.Replace(",", " ");
                    SubstationName = dt1.Rows[k]["SubstationName"].ToString();
                    string SourceID = swFeederid+'_'+voltage;
                    // string SourceID = SID.Replace(","," ");
                    string valu = NodeX + '_' + NodeY;
                    string NodeID = "";


                    NodeID = valu;


                    string llhk = "insert into [TGSOURCE_HEADNODES](NodeID,NetworkID,ConnectorIndex,StructureID,HarmonicEnveloppe,EquivalentSourceConfiguration,EquivalentSourceSinglePhaseCT,EquivSourceCenterTapPhase,BackgroundHarmonicVoltage) values('" + NodeID + "','" + swFeederid + "','" + 0 + "','" + " " + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "')";

                    OleDbCommand com33k = new OleDbCommand(llhk, connn2);
                    com33k.ExecuteNonQuery();
                    string llkk = "insert into [TGSOURCE_SOURCE](SourceID,DeviceNumber,NodeID,NetworkID,OperatingVoltageA,OperatingVoltageB,OperatingVoltageC,SinglePhaseCenterTap,CenterTapPhase) values('" + SourceID + "','" + SubstationName + "','" + NodeID + "','" + swFeederid + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "')";

                    OleDbCommand com33kk = new OleDbCommand(llkk, connn2);
                    com33kk.ExecuteNonQuery();

                    string llkkkk = "insert into [TGSOURCE_EQUIVALENT](NodeID,LoadModelName,Voltage,OperatingAngle1,OperatingAngle2,OperatingAngle3,PositiveSequenceResistance,PositiveSequenceReactance,NegativeSequenceResistance,NegativeSequenceReactance,ZeroSequenceResistance,ZeroSequenceReactance,OperatingVoltage1,OperatingVoltage2,OperatingVoltage3,BaseMVA,ImpedanceUnit) values('" + NodeID + "','" + "DEFAULT" + "','" + voltage + "','" + 0 + "','" + 120 + "','" + 120 + "','" + 0 + "','" + 1 + "','" + 0 + "','" + 1 + "','" + 1 + "','" + 2 + "','" + 7.505553 + "','" + 7.505553 + "','" + 7.505553 + "','" + 0 + "','" + 0 + "')";

                    OleDbCommand com33kkkk = new OleDbCommand(llkkkk, connn2);
                    com33kkkk.ExecuteNonQuery();
                    string llkkkk1 = "insert into [TGFEEDER_FORMAT_FEEDER](NetworkID,HeadNodeID,CoordSet,Description,Color,LoadFactor,LossLoadFactorK,Group1,Group2,Group3,Group4,Group5,TagText,TagProperties,TagDeltaX,TagDeltaY,TagAngle,TagAlignment,TagBorder,TagBackground,TagTextColor,TagBorderColor,TagBackgroundColor,TagLocation,TagFont,TagTextSize,TagOffset,Version,EnvironmentID,NetworkType,Years) values('" + swFeederid + "','" + NodeID + "','" + 1 + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + SubstationName + "','" + CircleCode + "','" + SNDCode + "','" + Zone_Name + "','" + Region_Name + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + -1 + "','" + 0 + "','" + "Feeder" + "','" + 0 + "')";

                    OleDbCommand com33kkkk1 = new OleDbCommand(llkkkk1, connn2);
                    com33kkkk1.ExecuteNonQuery();
                    string llkkk = "insert into [TGSOURCE_LOADEQUIVALENT](NodeID,LoadModelName,Format,Value1A,Value1B,Value1C,Value2A,Value2B,Value2C,ValueSinglePhaseCT11,ValueSinglePhaseCT12,ValueSinglePhaseCT21,ValueSinglePhaseCT22) values('" + NodeID + "','" + "DEFAULT" + "','" + "KW_PF" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "')";

                    OleDbCommand com33kkk = new OleDbCommand(llkkk, connn2);
                    com33kkk.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + Feederid);
            }

            try
            {
                string SWITCH = "select*from [GIS_PC03_DIEMXUATTUYEN]";

                OleDbCommand coms11 = new OleDbCommand(SWITCH, connn2);
                OleDbDataAdapter adb = new OleDbDataAdapter(coms11);
                DataTable st1 = new DataTable();
                adb.Fill(st1);
                //UTM aa = new UTM();
                for (int kk = 0; kk < st1.Rows.Count; kk++)
                {

                    string CoordX = st1.Rows[kk]["X"].ToString();
                    string CoordY = st1.Rows[kk]["Y"].ToString();
                   //string[] xValue1 = aa.ConvertToUTM(CoordX, CoordY);
                   // ToNodeX = xValue1[0];  // Easting
                   // ToNodey = xValue1[1];
                    string llk1 = "insert into [TempNode](NodeID,X,Y) values('" + CoordX + "_" + CoordY + "','" + CoordX + "','" + CoordY + "')";

                    OleDbCommand com33k1 = new OleDbCommand(llk1, connn2);
                    com33k1.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //// BusBar.....
            try
            {
                string jjj = "select*from [GIS_F09_PC03_DUONGDAY_TT] WHERE PhanLoai = '3'";   // Cable...

                OleDbCommand c0m121 = new OleDbCommand(jjj, connn2);
                OleDbDataAdapter ad111 = new OleDbDataAdapter(c0m121);
                DataTable dt2 = new DataTable();
                ad111.Fill(dt2);
                //UTM mm = new UTM();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {

                    string assistid = dt2.Rows[j]["OBJECTID"].ToString();
                    if (assistid.Contains("OH"))
                    {
                        assistid = assistid.Replace("OH", "BB");
                    }
                    else
                    {
                        assistid = assistid;
                    }

                    string FromnodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromnodeY = dt2.Rows[j]["FromNodeY"].ToString();


                    string Phase = dt2.Rows[j]["PhaDauNoi"].ToString();
                    string Zonecode = string.Empty;
                    string FeederID = dt2.Rows[j]["XuatTuyen"].ToString();
                    if (Phase == "")   /// order by rajib sir....
                    {
                        Phase = "ABC";
                    }
                    else
                    {
                        Phase = Phase;
                    }

                    string ToNodeX = dt2.Rows[j]["ToNodeX"].ToString();
                    string ToNodey = dt2.Rows[j]["ToNodeY"].ToString();

                    string FromNodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromNodeY = dt2.Rows[j]["FromNodeY"].ToString();

                    string nodex = ToNodeX + '_' + ToNodey;
                    string nodey = FromNodeX + '_' + FromNodeY;

                    string FromNodeID = FromnodeX + '_' + FromnodeY;
                    string ToNodeID = ToNodeX + '_' + ToNodey;

                    string llk1 = "insert into [TempNode](NodeID,X,Y) values('" + nodex + "','" + ToNodeX + "','" + ToNodey + "')";

                    OleDbCommand com33k1 = new OleDbCommand(llk1, connn2);
                    com33k1.ExecuteNonQuery();

                    string llk11 = "insert into [TempNode](NodeID,X,Y) values('" + nodey + "','" + FromNodeX + "','" + FromNodeY + "')";

                    OleDbCommand com33k11 = new OleDbCommand(llk11, connn2);
                    com33k11.ExecuteNonQuery();



                    string lla = "insert into [TEMPSECTION-NOTBREAKED](SectionID,FromNodeID,FromNodeIndex,ToNodeID,ToNodeIndex,Phase,ZoneID,SubNetWorkID,EnvironmentID) values('" + assistid + "','" + FromNodeID + "','" + 0 + "','" + ToNodeID + "','" + 0 + "','" + Phase + "','" + Zonecode + "','" + "" + "','" + 0 + "')";

                    OleDbCommand com33a = new OleDbCommand(lla, connn2);
                    com33a.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }


            // Cable.....
            try
            {
                string jjj = "select*from [GIS_F09_PC03_DUONGDAY_TT] WHERE PhanLoai = '2'";   // Cable...

                OleDbCommand c0m121 = new OleDbCommand(jjj, connn2);
                OleDbDataAdapter ad111 = new OleDbDataAdapter(c0m121);
                DataTable dt2 = new DataTable();
                ad111.Fill(dt2);
                //UTM mm = new UTM();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {

                    string assistid = dt2.Rows[j]["OBJECTID"].ToString();
                    if (assistid.Contains("OH"))
                    {
                        assistid = assistid.Replace("OH", "CA");
                    }
                    else
                    {
                        assistid = assistid;
                    }

                    string FromnodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromnodeY = dt2.Rows[j]["FromNodeY"].ToString();

                      
                    string Phase = dt2.Rows[j]["PhaDauNoi"].ToString();
                    string Zonecode = string.Empty;
                    string FeederID = dt2.Rows[j]["XuatTuyen"].ToString();
                    if (Phase == "")   /// order by rajib sir....
                    {
                        Phase = "ABC";
                    }
                    else
                    {
                        Phase = Phase;
                    }

                    string ToNodeX = dt2.Rows[j]["ToNodeX"].ToString();
                    string ToNodey = dt2.Rows[j]["ToNodeY"].ToString();
                   
                    string FromNodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromNodeY = dt2.Rows[j]["FromNodeY"].ToString();
                     
                    string nodex = ToNodeX + '_' + ToNodey;
                    string nodey = FromNodeX + '_' + FromNodeY;

                    string FromNodeID = FromnodeX + '_' + FromnodeY;
                    string ToNodeID = ToNodeX + '_' + ToNodey;

                    string llk1 = "insert into [TempNode](NodeID,X,Y) values('" + nodex + "','" + ToNodeX + "','" + ToNodey + "')";

                    OleDbCommand com33k1 = new OleDbCommand(llk1, connn2);
                    com33k1.ExecuteNonQuery();

                    string llk11 = "insert into [TempNode](NodeID,X,Y) values('" + nodey + "','" + FromNodeX + "','" + FromNodeY + "')";

                    OleDbCommand com33k11 = new OleDbCommand(llk11, connn2);
                    com33k11.ExecuteNonQuery();



                    string lla = "insert into [TEMPSECTION-NOTBREAKED](SectionID,FromNodeID,FromNodeIndex,ToNodeID,ToNodeIndex,Phase,ZoneID,SubNetWorkID,EnvironmentID) values('" + assistid + "','" + FromNodeID + "','" + 0 + "','" + ToNodeID + "','" + 0 + "','" + Phase + "','" + Zonecode + "','" + "" + "','" + 0 + "')";

                    OleDbCommand com33a = new OleDbCommand(lla, connn2);
                    com33a.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }

            // Line.....
            try
            {
                string jjj = "select*from [GIS_F09_PC03_DUONGDAY_TT] WHERE PhanLoai = '1'";   // Cable...

                OleDbCommand c0m121 = new OleDbCommand(jjj, connn2);
                OleDbDataAdapter ad111 = new OleDbDataAdapter(c0m121);
                DataTable dt2 = new DataTable();
                ad111.Fill(dt2);
                //UTM mm = new UTM();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {

                    string assistid = dt2.Rows[j]["OBJECTID"].ToString();
                    //if (assistid.Contains("OH"))
                    //{
                    //    assistid = assistid.Replace("OH", "CA");
                    //}
                    //else
                    //{
                    //    assistid = assistid;
                    //}

                    string FromnodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromnodeY = dt2.Rows[j]["FromNodeY"].ToString();


                    string Phase = dt2.Rows[j]["PhaDauNoi"].ToString();
                    string Zonecode = string.Empty;
                    string FeederID = dt2.Rows[j]["XuatTuyen"].ToString();
                    if (Phase == "")   /// order by rajib sir....
                    {
                        Phase = "ABC";
                    }
                    else
                    {
                        Phase = Phase;
                    }

                    string ToNodeX = dt2.Rows[j]["ToNodeX"].ToString();
                    string ToNodey = dt2.Rows[j]["ToNodeY"].ToString();

                    string FromNodeX = dt2.Rows[j]["FromNodeX"].ToString();
                    string FromNodeY = dt2.Rows[j]["FromNodeY"].ToString();

                    string nodex = ToNodeX + '_' + ToNodey;
                    string nodey = FromNodeX + '_' + FromNodeY;

                    string FromNodeID = FromnodeX + '_' + FromnodeY;
                    string ToNodeID = ToNodeX + '_' + ToNodey;

                    string llk1 = "insert into [TempNode](NodeID,X,Y) values('" + nodex + "','" + ToNodeX + "','" + ToNodey + "')";

                    OleDbCommand com33k1 = new OleDbCommand(llk1, connn2);
                    com33k1.ExecuteNonQuery();

                    string llk11 = "insert into [TempNode](NodeID,X,Y) values('" + nodey + "','" + FromNodeX + "','" + FromNodeY + "')";

                    OleDbCommand com33k11 = new OleDbCommand(llk11, connn2);
                    com33k11.ExecuteNonQuery();



                    string lla = "insert into [TEMPSECTION-NOTBREAKED](SectionID,FromNodeID,FromNodeIndex,ToNodeID,ToNodeIndex,Phase,ZoneID,SubNetWorkID,EnvironmentID) values('" + assistid + "','" + FromNodeID + "','" + 0 + "','" + ToNodeID + "','" + 0 + "','" + Phase + "','" + Zonecode + "','" + "" + "','" + 0 + "')";

                    OleDbCommand com33a = new OleDbCommand(lla, connn2);
                    com33a.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }

        }

        public string updatevoltagecodevalue(string voltage)
        {
            if (voltage == "120")
            {
                voltage = "0.11 KV";
            }
            if (voltage == "160")
            {
                voltage = "0.24 KV";
            }
            if (voltage == "210")
            {
                voltage = "0.43 KV";
            }
            if (voltage == "11")
            {
                voltage = "11KV";
            }
            if (voltage == "340")
            {
                voltage = "11 KV";
            }
            if (voltage == "380")
            {
                voltage = "33 KV";
            }
            if (voltage == "80")
            {
                voltage = "66 KV";
            }
            if (voltage == "110")
            {
                voltage = "132 KV";
            }
            if (voltage == "50")
            {
                voltage = "220 KV";
            }
            if (voltage == "260")
            {
                voltage = "400 KV";
            }
            if (voltage == "360")
            {
                voltage = "22 KV";
            }
            if (voltage == "90")
            {
                voltage = "100 KV";
            }

            return voltage;

        }
    }
}
