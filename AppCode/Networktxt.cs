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
    class Networktxt
    {
        public string main(string st, OleDbConnection conn)
        {

            string AA = "select distinct NodeId, X , Y from TempNode";
            using (OleDbCommand cmd = new OleDbCommand(AA, conn))
            {
                StreamWriter tw = File.AppendText(st + "\\TempChanges\\text1.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader = cmd.ExecuteReader();
                    string str = @"
[NODE]
FORMAT_NODE=NodeID,CoordX,CoordY
";
                    tw.WriteLine(str);
                    while (reader.Read())
                    {
                        tw.Write(reader["NodeId"].ToString().Trim());
                        tw.Write("," + reader["X"].ToString().Trim());
                        tw.Write("," + reader["Y"].ToString().Trim());
                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw.WriteLine();
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw.Close();

                    reader.Close();
                    
                }
                catch (Exception ex)
                {
                    tw.Close();
                }
            }


            string BB = "select * from TGSOURCE_HEADNODES";
            using (OleDbCommand cmd1 = new OleDbCommand(BB, conn))
            {
                StreamWriter tw1 = File.AppendText(st + "\\TempChanges\\text2.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader1 = cmd1.ExecuteReader();
                    string str = @"
[HEADNODES] 
FORMAT_HEADNODES=NodeID,NetworkID,ConnectorIndex,StructureID,HarmonicEnveloppe,EquivalentSourceConfiguration,EquivalentSourceSinglePhaseCT,EquivSourceCenterTapPhase,BackgroundHarmonicVoltage
";
                    tw1.WriteLine(str);
                    while (reader1.Read())
                    {
                        tw1.Write(reader1["NodeID"].ToString().Trim());
                        tw1.Write("," + reader1["NetworkID"].ToString().Trim());
                        tw1.Write("," + reader1["ConnectorIndex"].ToString().Trim());
                        tw1.Write("," + reader1["StructureID"].ToString().Trim());
                        tw1.Write("," + reader1["HarmonicEnveloppe"].ToString().Trim());
                        tw1.Write("," + reader1["EquivalentSourceConfiguration"].ToString().Trim());
                        tw1.Write("," + reader1["EquivalentSourceSinglePhaseCT"].ToString().Trim());
                        tw1.Write("," + reader1["EquivSourceCenterTapPhase"].ToString().Trim());
                        tw1.Write("," + reader1["BackgroundHarmonicVoltage"].ToString().Trim());
                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw1.WriteLine();
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw1.Close();

                    reader1.Close();
                    
                }
                catch (Exception ex)
                {
                    tw1.Close();
                }
            }


            string CC = "select * from TGSOURCE_SOURCE";

            using (OleDbCommand cmd2 = new OleDbCommand(CC, conn))
            {
                StreamWriter tw2 = File.AppendText(st + "\\TempChanges\\text3.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader2 = cmd2.ExecuteReader();
                    string str1 = @"
[SOURCE]
FORMAT_SOURCE=SourceID,DeviceNumber,NodeID,NetworkID,OperatingVoltageA,OperatingVoltageB,OperatingVoltageC,SinglePhaseCenterTap,CenterTapPhase
";
                    tw2.WriteLine(str1);
                    while (reader2.Read())
                    {
                        string deviceno = reader2["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {
                            tw2.Write(reader2["SourceID"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceNumber"].ToString().Trim());
                            tw2.Write("," + reader2["NodeID"].ToString().Trim());
                            tw2.Write("," + reader2["NetworkID"].ToString().Trim());
                            tw2.Write("," + reader2["OperatingVoltageA"].ToString().Trim());
                            tw2.Write("," + reader2["OperatingVoltageB"].ToString().Trim());
                            tw2.Write("," + reader2["OperatingVoltageC"].ToString().Trim());
                            tw2.Write("," + reader2["SinglePhaseCenterTap"].ToString().Trim());
                            tw2.Write("," + reader2["CenterTapPhase"].ToString().Trim());
                            tw2.WriteLine();
                        }
                    }
                    tw2.Close();

                    reader2.Close();
                }
                catch (Exception ex)
                {
                    tw2.Close();
                }
            }





            string DD = "select * from TGSOURCE_EQUIVALENT";

            using (OleDbCommand cmd3 = new OleDbCommand(DD, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text4.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[SOURCE EQUIVALENT]
FORMAT_SOURCEEQUIVALENT=NodeID,LoadModelName,Voltage,OperatingAngle1,OperatingAngle2,OperatingAngle3,PositiveSequenceResistance,PositiveSequenceReactance,NegativeSequenceResistance,NegativeSequenceReactance,ZeroSequenceResistance,ZeroSequenceReactance,OperatingVoltage1,OperatingVoltage2,OperatingVoltage3,BaseMVA,ImpedanceUnit
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        tw3.Write(reader3["NodeID"].ToString().Trim());
                        tw3.Write("," + reader3["LoadModelName"].ToString().Trim());
                        tw3.Write("," + reader3["Voltage"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingAngle1"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingAngle2"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingAngle3"].ToString().Trim());
                        tw3.Write("," + reader3["PositiveSequenceResistance"].ToString().Trim());
                        tw3.Write("," + reader3["PositiveSequenceReactance"].ToString().Trim());
                        tw3.Write("," + reader3["NegativeSequenceResistance"].ToString().Trim());
                        tw3.Write("," + reader3["NegativeSequenceReactance"].ToString().Trim());
                        tw3.Write("," + reader3["ZeroSequenceResistance"].ToString().Trim());
                        tw3.Write("," + reader3["ZeroSequenceReactance"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingVoltage1"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingVoltage2"].ToString().Trim());
                        tw3.Write("," + reader3["OperatingVoltage3"].ToString().Trim());
                        tw3.Write("," + reader3["BaseMVA"].ToString().Trim());
                        tw3.Write("," + reader3["ImpedanceUnit"].ToString().Trim());
                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw3.WriteLine();
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }




            string EE = "select * from TGSOURCE_LOADEQUIVALENT";

            using (OleDbCommand cmd3 = new OleDbCommand(EE, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text5.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[LOAD EQUIVALENT]
FORMAT_LOADEQUIVALENT=NodeID,LoadModelName,Format,Value1A,Value1B,Value1C,Value2A,Value2B,Value2C,ValueSinglePhaseCT11,ValueSinglePhaseCT12,ValueSinglePhaseCT21,ValueSinglePhaseCT22
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        tw3.Write(reader3["NodeID"].ToString().Trim());
                        tw3.Write("," + reader3["LoadModelName"].ToString().Trim());
                        tw3.Write("," + reader3["Format"].ToString().Trim());
                        tw3.Write("," + reader3["Value1A"].ToString().Trim());
                        tw3.Write("," + reader3["Value1B"].ToString().Trim());
                        tw3.Write("," + reader3["Value1C"].ToString().Trim());
                        tw3.Write("," + reader3["Value2A"].ToString().Trim());
                        tw3.Write("," + reader3["Value2B"].ToString().Trim());
                        tw3.Write("," + reader3["Value2C"].ToString().Trim());
                        tw3.Write("," + reader3["ValueSinglePhaseCT11"].ToString().Trim());
                        tw3.Write("," + reader3["ValueSinglePhaseCT12"].ToString().Trim());
                        tw3.Write("," + reader3["ValueSinglePhaseCT21"].ToString().Trim());
                        tw3.Write("," + reader3["ValueSinglePhaseCT22"].ToString().Trim());

                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw3.WriteLine();
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }





            string FF = "select * from TGLINE_OVERHEADLINE";

            using (OleDbCommand cmd3 = new OleDbCommand(FF, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text6.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[OVERHEADLINE SETTING]
FORMAT_OVERHEADLINESETTING=SectionID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,LineCableID,Length,ConnectionStatus,CoordX,CoordY,HarmonicModel,FlowConstraintActive,FlowConstraintUnit,MaximumFlow,SeriesCompensationActive,MaxReactanceMultiplier,SeriesCompensationCost
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            tw3.Write("," + reader3["InitFromEquipFlags"].ToString().Trim());
                            tw3.Write("," + reader3["LineCableID"].ToString().Trim());
                            tw3.Write("," + reader3["Length"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["HarmonicModel"].ToString().Trim());
                            tw3.Write("," + reader3["FlowConstraintActive"].ToString().Trim());
                            tw3.Write("," + reader3["FlowConstraintUnit"].ToString().Trim());
                            tw3.Write("," + reader3["MaximumFlow"].ToString().Trim());
                            tw3.Write("," + reader3["SeriesCompensationActive"].ToString().Trim());
                            tw3.Write("," + reader3["MaxReactanceMultiplier"].ToString().Trim());
                            tw3.Write("," + reader3["SeriesCompensationCost"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }





            string GG = "select * from TGLINE_UNDERGROUNDLINE";

            using (OleDbCommand cmd3 = new OleDbCommand(GG, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text7.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[UNDERGROUNDLINE SETTING]
FORMAT_UNDERGROUNDLINESETTING=SectionID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,LineCableID,Length,NumberOfCableInParallel,Amps,Amps_1,Amps_2,Amps_3,Amps_4,ConnectionStatus,CoordX,CoordY,HarmonicModel,EarthResistivity,OperatingTemperature,Height,DistanceBetweenConductors,BondingType,CableConfiguration,DuctMaterial,Bundled,Neutral1Type,Neutral2Type,Neutral3Type,Neutral1ID,Neutral2ID,Neutral3ID,AmpacityDeratingFactor,FlowConstraintActive,FlowConstraintUnit,MaximumFlow
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            tw3.Write("," + reader3["InitFromEquipFlags"].ToString().Trim());
                            tw3.Write("," + reader3["LineCableID"].ToString().Trim());
                            tw3.Write("," + reader3["Length"].ToString().Trim());
                            tw3.Write("," + reader3["NumberOfCableInParallel"].ToString().Trim());
                            tw3.Write("," + reader3["Amps"].ToString().Trim());
                            tw3.Write("," + reader3["Amps_1"].ToString().Trim());
                            tw3.Write("," + reader3["Amps_2"].ToString().Trim());
                            tw3.Write("," + reader3["Amps_3"].ToString().Trim());
                            tw3.Write("," + reader3["Amps_4"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["HarmonicModel"].ToString().Trim());
                            tw3.Write("," + reader3["EarthResistivity"].ToString().Trim());
                            tw3.Write("," + reader3["OperatingTemperature"].ToString().Trim());
                            tw3.Write("," + reader3["Height"].ToString().Trim());
                            tw3.Write("," + reader3["DistanceBetweenConductors"].ToString().Trim());
                            tw3.Write("," + reader3["BondingType"].ToString().Trim());
                            tw3.Write("," + reader3["CableConfiguration"].ToString().Trim());
                            tw3.Write("," + reader3["DuctMaterial"].ToString().Trim());
                            tw3.Write("," + reader3["Bundled"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral1Type"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral2Type"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral3Type"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral1ID"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral2ID"].ToString().Trim());
                            tw3.Write("," + reader3["Neutral3ID"].ToString().Trim());
                            tw3.Write("," + reader3["AmpacityDeratingFactor"].ToString().Trim());
                            tw3.Write("," + reader3["FlowConstraintActive"].ToString().Trim());
                            tw3.Write("," + reader3["FlowConstraintUnit"].ToString().Trim());
                            tw3.Write("," + reader3["MaximumFlow"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }




            string MM = "select * from TGFEEDER_FORMAT_FEEDER";

            using (OleDbCommand cmd3 = new OleDbCommand(MM, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text8.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[SECTION]
FORMAT_SECTION=SectionID,FromNodeID,FromNodeIndex,ToNodeID,ToNodeIndex,Phase,ZoneID,SubNetworkId,EnvironmentID
FORMAT_FEEDER=NetworkID,HeadNodeID,CoordSet,Year,Description,Color,LoadFactor,LossLoadFactorK,Group1,Group2,Group3,TagText,TagProperties,TagDeltaX,TagDeltaY,TagAngle,TagAlignment,TagBorder,TagBackground,TagTextColor,TagBorderColor,TagBackgroundColor,TagLocation,TagFont,TagTextSize,TagOffset,Version,EnvironmentID
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        tw3.Write("FEEDER=" + reader3["NetworkID"].ToString().Trim());
                        tw3.Write("," + reader3["HeadNodeID"].ToString().Trim());
                        tw3.Write("," + reader3["CoordSet"].ToString().Trim());
                        tw3.Write("," + reader3["Year"].ToString().Trim());
                        tw3.Write("," + reader3["Description"].ToString().Trim());
                        tw3.Write("," + reader3["Color"].ToString().Trim());
                        tw3.Write("," + reader3["LoadFactor"].ToString().Trim());
                        tw3.Write("," + reader3["LossLoadFactorK"].ToString().Trim());
                        tw3.Write("," + reader3["Group1"].ToString().Trim());
                        tw3.Write("," + reader3["Group2"].ToString().Trim());
                        tw3.Write("," + reader3["Group3"].ToString().Trim());
                        tw3.Write("," + reader3["TagText"].ToString().Trim());
                        tw3.Write("," + reader3["TagProperties"].ToString().Trim());
                        tw3.Write("," + reader3["TagDeltaX"].ToString().Trim());
                        tw3.Write("," + reader3["TagDeltaY"].ToString().Trim());
                        tw3.Write("," + reader3["TagAngle"].ToString().Trim());
                        tw3.Write("," + reader3["TagAlignment"].ToString().Trim());
                        tw3.Write("," + reader3["TagBorder"].ToString().Trim());
                        tw3.Write("," + reader3["TagBackground"].ToString().Trim());
                        tw3.Write("," + reader3["TagTextColor"].ToString().Trim());
                        tw3.Write("," + reader3["TagBorderColor"].ToString().Trim());
                        tw3.Write("," + reader3["TagBackgroundColor"].ToString().Trim());
                        tw3.Write("," + reader3["TagLocation"].ToString().Trim());
                        tw3.Write("," + reader3["TagFont"].ToString().Trim());
                        tw3.Write("," + reader3["TagTextSize"].ToString().Trim());
                        tw3.Write("," + reader3["TagOffset"].ToString().Trim());
                        tw3.Write("," + reader3["Version"].ToString().Trim());
                        tw3.Write("," + reader3["EnvironmentID"].ToString().Trim());

                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw3.WriteLine();
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }





            string YY = "select * from TempSection_Breaked";

            using (OleDbCommand cmd3 = new OleDbCommand(YY, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text9.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionId"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section))
                        {
                            if (reader3["FromNodeId"].ToString() != reader3["ToNodeId"].ToString())
                            {
                                tw3.Write(reader3["SectionId"].ToString().Trim());
                                tw3.Write("," + reader3["FromNodeId"].ToString().Trim());
                                tw3.Write("," + reader3["FromNodeIndex"].ToString().Trim());
                                tw3.Write("," + reader3["ToNodeId"].ToString().Trim());
                                tw3.Write("," + reader3["ToNodeIndex"].ToString().Trim());
                                tw3.Write("," + reader3["Phase"].ToString().Trim());
                                tw3.Write("," + reader3["ZoneID"].ToString().Trim());
                                tw3.Write("," + reader3["SubNetworkId"].ToString().Trim());
                                tw3.Write("," + reader3["EnvironmentID"].ToString().Trim());

                                // tw.WriteLine(", " + reader["datetime"].ToString());
                                tw3.WriteLine();
                            }
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }





            string ZZ = "select * from TempSection_NotBreaked";

            using (OleDbCommand cmd3 = new OleDbCommand(ZZ, conn))
            {
                //cmd.Connection = conn;
                //cmd.CommandText = "tblOutbox";
                //cmd.CommandType = CommandType.TableDirect;
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text10.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionId"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section))
                        {
                            tw3.Write(reader3["SectionId"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeId"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeIndex"].ToString().Trim());
                            tw3.Write("," + reader3["ToNodeId"].ToString().Trim());
                            tw3.Write("," + reader3["ToNodeIndex"].ToString().Trim());
                            tw3.Write("," + reader3["Phase"].ToString().Trim());
                            tw3.Write("," + reader3["ZoneID"].ToString().Trim());
                            tw3.Write("," + reader3["SubNetworkId"].ToString().Trim());
                            tw3.Write("," + reader3["EnvironmentID"].ToString().Trim());

                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }

            string LL = "select * from TGDEVICE_DISTRIBUTIONTRANSFORMER";

            using (OleDbCommand cmd3 = new OleDbCommand(LL, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text11.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[TRANSFORMER SETTING]
FORMAT_TRANSFORMERSETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,CoordX,CoordY,Conn,PrimTap,SecondaryTap,RgPrim,XgPrim,RgSec,XgSec,ODPrimPh,PrimaryBaseVoltage,SecondaryBaseVoltage,FromNodeID,SettingOption,SetPoint,ControlType,LowerBandWidth,UpperBandWidth,TapLocation,InitialTapPosition,InitialTapPositionMode,Tap,MaxBuck,MaxBoost,CT,PT,Rset,Xset,FirstHouseHigh,FirstHouseLow,PhaseON,AtSectionID,MasterID,FaultIndicator,PhaseShiftType,GammaPhaseShift,CTPhase,PrimaryCornerGroundedPhase,SecondaryCornerGroundedPhase,ConnectionStatus,Reversible
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["Location"].ToString().Trim());
                            tw3.Write("," + reader3["EqID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            //tw3.Write("," + reader3["InitFromEquipFlags"].ToString());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["Conn"].ToString().Trim());
                            tw3.Write("," + reader3["PrimTap"].ToString().Trim());
                            tw3.Write("," + reader3["SecondaryTap"].ToString().Trim());
                            tw3.Write("," + reader3["RgPrim"].ToString().Trim());
                            tw3.Write("," + reader3["XgPrim"].ToString().Trim());
                            tw3.Write("," + reader3["RgSec"].ToString().Trim());
                            tw3.Write("," + reader3["XgSec"].ToString().Trim());
                            tw3.Write("," + reader3["ODPrimPh"].ToString().Trim());
                            tw3.Write("," + reader3["PrimaryBaseVoltage"].ToString().Trim());
                            tw3.Write("," + reader3["SecondaryBaseVoltage"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeID"].ToString().Trim());
                            tw3.Write("," + reader3["SettingOption"].ToString().Trim());
                            tw3.Write("," + reader3["SetPoint"].ToString().Trim());
                            tw3.Write("," + reader3["ControlType"].ToString().Trim());
                            tw3.Write("," + reader3["LowerBandWidth"].ToString().Trim());
                            tw3.Write("," + reader3["UpperBandWidth"].ToString().Trim());
                            tw3.Write("," + reader3["TapLocation"].ToString().Trim());
                            tw3.Write("," + reader3["InitialTapPosition"].ToString().Trim());
                            tw3.Write("," + reader3["InitialTapPositionMode"].ToString().Trim());
                            tw3.Write("," + reader3["Tap"].ToString().Trim());
                            tw3.Write("," + reader3["MaxBuck"].ToString().Trim());
                            tw3.Write("," + reader3["MaxBoost"].ToString().Trim());
                            tw3.Write("," + reader3["CT"].ToString().Trim());
                            tw3.Write("," + reader3["PT"].ToString().Trim());
                            tw3.Write("," + reader3["Rset"].ToString().Trim());
                            tw3.Write("," + reader3["Xset"].ToString().Trim());
                            tw3.Write("," + reader3["FirstHouseHigh"].ToString().Trim());
                            tw3.Write("," + reader3["FirstHouseLow"].ToString().Trim());
                            tw3.Write("," + reader3["PhaseON"].ToString().Trim());
                            tw3.Write("," + reader3["AtSectionID"].ToString().Trim());
                            tw3.Write("," + reader3["MasterID"].ToString().Trim());
                            tw3.Write("," + reader3["FaultIndicator"].ToString().Trim());
                            tw3.Write("," + reader3["PhaseShiftType"].ToString().Trim());
                            tw3.Write("," + reader3["GammaPhaseShift"].ToString().Trim());
                            tw3.Write("," + reader3["CTPhase"].ToString().Trim());
                            tw3.Write("," + reader3["PrimaryCornerGroundedPhase"].ToString().Trim());
                            tw3.Write("," + reader3["SecondaryCornerGroundedPhase"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["Reversible"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }



            string JJ = "select * from TGDEVICE_SWITCH";

            using (OleDbCommand cmd3 = new OleDbCommand(JJ, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text12.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[SWITCH SETTING]
FORMAT_SWITCHSETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,FaultIndicator,Automated,SensorMode,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Reversible
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["Location"].ToString().Trim());
                            tw3.Write("," + reader3["EqID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            tw3.Write("," + reader3["InitFromEquipFlags"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["ClosedPhase"].ToString().Trim());
                            tw3.Write("," + reader3["Locked"].ToString().Trim());
                            tw3.Write("," + reader3["RC"].ToString().Trim());
                            tw3.Write("," + reader3["NStatus"].ToString().Trim());
                            tw3.Write("," + reader3["PhPickup"].ToString().Trim());
                            tw3.Write("," + reader3["GrdPickup"].ToString().Trim());
                            tw3.Write("," + reader3["Alternate"].ToString().Trim());
                            tw3.Write("," + reader3["PhAltPickup"].ToString().Trim());
                            tw3.Write("," + reader3["GrdAltPickup"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeID"].ToString().Trim());
                            tw3.Write("," + reader3["FaultIndicator"].ToString().Trim());
                            tw3.Write("," + reader3["Automated"].ToString().Trim());
                            tw3.Write("," + reader3["SensorMode"].ToString().Trim());
                            tw3.Write("," + reader3["Strategic"].ToString().Trim());
                            tw3.Write("," + reader3["RestorationMode"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["ByPassOnRestoration"].ToString().Trim());
                            tw3.Write("," + reader3["Reversible"].ToString().Trim());

                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }

            string NN = "select * from TGDEVICE_CIRCUITBREAKER";

            using (OleDbCommand cmd3 = new OleDbCommand(NN, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text13.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[BREAKER SETTING]
FORMAT_BREAKERSETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,TCCID,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,EnableReclosing,FaultIndicator,EnableFuseSaving,MinRatedCurrentForFuseSaving,Automated,SensorMode,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Speed,SeqOpFirstPhase,SeqOpFirstGround,SeqOpLockoutPhase,SeqOpLockoutGround,SeqResetTime,SeqReclosingTime1,SeqReclosingTime2,SeqReclosingTime3,Reversible
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        string connection = reader3["ConnectionStatus"].ToString();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["Location"].ToString().Trim());
                            tw3.Write("," + reader3["EqID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            tw3.Write("," + reader3["InitFromEquipFlags"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["ClosedPhase"].ToString().Trim());
                            tw3.Write("," + reader3["Locked"].ToString().Trim());
                            tw3.Write("," + reader3["RC"].ToString().Trim());
                            tw3.Write("," + reader3["NStatus"].ToString().Trim());
                            tw3.Write("," + reader3["TCCID"].ToString().Trim());
                            tw3.Write("," + reader3["PhPickup"].ToString().Trim());
                            tw3.Write("," + reader3["GrdPickup"].ToString().Trim());
                            tw3.Write("," + reader3["Alternate"].ToString().Trim());
                            tw3.Write("," + reader3["PhAltPickup"].ToString().Trim());
                            tw3.Write("," + reader3["GrdAltPickup"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeID"].ToString().Trim());
                            tw3.Write("," + reader3["EnableReclosing"].ToString().Trim());
                            tw3.Write("," + reader3["FaultIndicator"].ToString().Trim());
                            tw3.Write("," + reader3["EnableFuseSaving"].ToString().Trim());
                            tw3.Write("," + reader3["MinRatedCurrentForFuseSaving"].ToString().Trim());
                            tw3.Write("," + reader3["Automated"].ToString().Trim());
                            tw3.Write("," + reader3["SensorMode"].ToString().Trim());
                            tw3.Write("," + reader3["Strategic"].ToString().Trim());
                            tw3.Write("," + reader3["RestorationMode"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["ByPassOnRestoration"].ToString().Trim());
                            // tw3.Write(", " + reader3["MaxBoost"].ToString());
                            tw3.Write("," + reader3["Speed"].ToString().Trim());
                            tw3.Write("," + reader3["SeqOpFirstPhase"].ToString().Trim());
                            tw3.Write("," + reader3["SeqOpFirstGround"].ToString().Trim());
                            tw3.Write("," + reader3["SeqOpLockoutPhase"].ToString().Trim());
                            tw3.Write("," + reader3["SeqOpLockoutGround"].ToString().Trim());
                            tw3.Write("," + reader3["SeqResetTime"].ToString().Trim());
                            tw3.Write("," + reader3["SeqReclosingTime1"].ToString().Trim());
                            tw3.Write("," + reader3["SeqReclosingTime2"].ToString().Trim());
                            tw3.Write("," + reader3["SeqReclosingTime3"].ToString().Trim());
                            tw3.Write("," + reader3["Reversible"].ToString().Trim());
                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }


            string KK = "select * from TGDEVICE_FUSE";

            using (OleDbCommand cmd3 = new OleDbCommand(KK, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text14.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[FUSE SETTING]
FORMAT_FUSESETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,TCCID,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,FaultIndicator,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Reversible
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section) && !string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["Location"].ToString().Trim());
                            tw3.Write("," + reader3["EqID"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceStage"].ToString().Trim());
                            tw3.Write("," + reader3["Flags"].ToString().Trim());
                            tw3.Write("," + reader3["InitFromEquipFlags"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["ClosedPhase"].ToString().Trim());
                            tw3.Write("," + reader3["Locked"].ToString().Trim());
                            tw3.Write("," + reader3["RC"].ToString().Trim());
                            tw3.Write("," + reader3["NStatus"].ToString().Trim());
                            tw3.Write("," + reader3["TCCID"].ToString().Trim());
                            tw3.Write("," + reader3["PhPickup"].ToString().Trim());
                            tw3.Write("," + reader3["GrdPickup"].ToString().Trim());
                            tw3.Write("," + reader3["Alternate"].ToString().Trim());
                            tw3.Write("," + reader3["PhAltPickup"].ToString().Trim());

                            tw3.Write("," + reader3["GrdAltPickup"].ToString().Trim());
                            tw3.Write("," + reader3["FromNodeID"].ToString().Trim());
                            tw3.Write("," + reader3["FaultIndicator"].ToString().Trim());
                            tw3.Write("," + reader3["Strategic"].ToString().Trim());
                            tw3.Write("," + reader3["RestorationMode"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["ByPassOnRestoration"].ToString().Trim());
                            tw3.Write("," + reader3["Reversible"].ToString().Trim());

                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }




            string XX = "select * from TGNODE_INTERMEDIATENODES";

            using (OleDbCommand cmd3 = new OleDbCommand(XX, conn))
            {
              
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text15.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[INTERMEDIATE NODES]
FORMAT_INTERMEDIATENODE=SectionID,SeqNumber,CoordX,CoordY,IsBreakPoint,BreakPointLocation
               ";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section))
                        {
                            tw3.Write(reader3["SectionID"].ToString().Trim());
                            tw3.Write("," + reader3["SeqNumber"].ToString().Trim());
                            tw3.Write("," + reader3["CoordX"].ToString().Trim());
                            tw3.Write("," + reader3["CoordY"].ToString().Trim());
                            tw3.Write("," + reader3["IsBreakPoint"].ToString().Trim());
                            tw3.Write("," + reader3["BreakPointLocation"].ToString().Trim());

                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }


            string II = "select * from TGDTAG_DEVICETAG";

            using (OleDbCommand cmd3 = new OleDbCommand(II, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text16.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[DEVICETAG]
FORMAT_DEVICETAG=DeviceNumber,DeviceType,TagText,TagProperties,TagDeltaX,TagDeltaY,TagAngle,TagAlignment,TagBorder,TagBackground,TagTextColor,TagBorderColor,TagBackgroundColor,TagLocation,TagFont,TagTextSize,TagOffset
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {
                            tw3.Write(reader3["DeviceNumber"].ToString().Trim());
                            tw3.Write("," + reader3["DeviceType"].ToString().Trim());
                            tw3.Write("," + reader3["TagText"].ToString().Trim());
                            tw3.Write("," + reader3["TagProperties"].ToString().Trim());
                            tw3.Write("," + reader3["TagDeltaX"].ToString().Trim());
                            tw3.Write("," + reader3["TagDeltaY"].ToString().Trim());
                            tw3.Write("," + reader3["TagAngle"].ToString().Trim());
                            tw3.Write("," + reader3["TagAlignment"].ToString().Trim());
                            tw3.Write("," + reader3["TagBorder"].ToString().Trim());
                            tw3.Write("," + reader3["TagBackground"].ToString().Trim());
                            tw3.Write("," + reader3["TagTextColor"].ToString().Trim());
                            tw3.Write("," + reader3["TagBorderColor"].ToString().Trim());
                            tw3.Write("," + reader3["TagBackgroundColor"].ToString().Trim());
                            tw3.Write("," + reader3["TagLocation"].ToString().Trim());
                            tw3.Write("," + reader3["TagFont"].ToString().Trim());
                            tw3.Write("," + reader3["TagTextSize"].ToString().Trim());
                            tw3.Write("," + reader3["TagOffset"].ToString().Trim());

                            // tw.WriteLine(", " + reader["datetime"].ToString());
                            tw3.WriteLine();
                        }
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw3.Close();

                    reader3.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }


            string jj = "select * from TGUDD_NETWORKUDD";
            using (OleDbCommand cmd3 = new OleDbCommand(jj, conn))
            {
                StreamWriter sw = File.AppendText(st + "\\TempChanges\\text17.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();

                    string str3 = @"
[NETWORKUDD]
FORMAT_NETWORKUDD=NetworkId,DataId,DataType,DataValue
";
                    sw.WriteLine(str3);
                    while (reader3.Read())
                    {
                        sw.Write(reader3["NetworkId"].ToString().Trim());
                        sw.Write("," + reader3["DataId"].ToString().Trim());
                        sw.Write("," + reader3["DataType"].ToString().Trim());
                        sw.Write("," + reader3["DataValue"].ToString().Trim());

                        sw.WriteLine();
                    }

                    sw.Close();
                    reader3.Close();
                }
                catch (Exception ex)
                {
                    sw.Close();
                }


            }


            string kk = "select * from TGUDD_NODEUDD";
            using (OleDbCommand cmd3 = new OleDbCommand(kk, conn))
            {
                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text18.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();

                    string str3 = @"
[NODEUDD]
FORMAT_NODEUDD=NodeId,DataId,DataType,DataValue
";

                    sw.WriteLine(str3);

                    while (reader3.Read())
                    {
                        sw.Write(reader3["NodeId"].ToString().Trim());
                        sw.Write("," + reader3["DataId"].ToString().Trim());
                        sw.Write("," + reader3["DataType"].ToString().Trim());
                        sw.Write("," + reader3["DataValue"].ToString().Trim());
                        sw.WriteLine();
                    }

                    sw.Close();
                    reader3.Close();
                }
                catch (Exception ex)
                {
                    sw.Close();
                }

            }


            string ll = "select * from TGUDD_SECTIONUDD";
            using (OleDbCommand cmd3 = new OleDbCommand(ll, conn))
            {
                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text19.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();

                    string str3 = @"
[SECTIONUDD]
FORMAT_SECTIONUDD=SectionId,DataId,DataType,DataValue
";

                    sw.WriteLine(str3);

                    while (reader3.Read())
                    {
                        string section = reader3["SectionId"].ToString().Trim();
                        if (!string.IsNullOrEmpty(section))
                        {
                            sw.Write(reader3["SectionId"].ToString().Trim());
                            sw.Write("," + reader3["DataId"].ToString().Trim());
                            sw.Write("," + reader3["DataType"].ToString().Trim());
                            sw.Write("," + reader3["DataValue"].ToString().Trim());
                            sw.WriteLine();
                        }
                    }

                    sw.Close();
                    reader3.Close();
                }
                catch (Exception ex)
                {
                    sw.Close();
                }
            }


            string mm = "select * from TGUDD_DEVICEUDD";
            using (OleDbCommand cmd3 = new OleDbCommand(mm, conn))
            {
                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text20.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader reader3 = cmd3.ExecuteReader();

                    string str3 = @"
[DEVICEUDD]
FORMAT_DEVICEUDD=DeviceNumber,DeviceType,DataId,DataType,DataValue
";

                    sw.WriteLine(str3);

                    while (reader3.Read())
                    {
                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {
                            sw.Write(reader3["DeviceNumber"].ToString().Trim());
                            sw.Write("," + reader3["DeviceType"].ToString().Trim());
                            sw.Write("," + reader3["DataId"].ToString().Trim());
                            sw.Write("," + reader3["DataType"].ToString().Trim());
                            sw.Write("," + reader3["DataValue"].ToString().Trim());
                            sw.WriteLine();
                        }
                    }
                    sw.Close();
                    reader3.Close();
                }
                catch (Exception ex)
                {
                    sw.Close();
                }



            }




            using (StreamWriter writer = File.CreateText(st + "\\CymeTextFile\\Network.txt"))
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
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text1.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {

                }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text2.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text3.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text4.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text5.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text6.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text7.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text8.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text9.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text10.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text11.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text12.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text13.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text14.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text15.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text16.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text17.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text18.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text19.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text20.txt"))
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
