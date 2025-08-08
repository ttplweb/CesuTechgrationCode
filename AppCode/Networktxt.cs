using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    MessageBox.Show(ex.ToString());
                    tw.Close();
                }
            }


            string BB = "select * from TGSOURCE_HEADNODES";
            using (OleDbCommand cmd1 = new OleDbCommand(BB, conn))
            {
                StreamWriter tw1 = File.AppendText(st + "\\TempChanges\\text2.txt");
                try
                {
                   
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
                    MessageBox.Show(ex.ToString());
                    tw1.Close();
                }
            }


            string CC = "select * from TGSOURCE_SOURCE";

            using (OleDbCommand cmd2 = new OleDbCommand(CC, conn))
            {
                StreamWriter tw2 = File.AppendText(st + "\\TempChanges\\text3.txt");
                try
                {
                   
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
                    MessageBox.Show(ex.ToString());
                    tw2.Close();
                }
            }


            string AA21 = "select DeviceNumber,DeviceType,DataId,DataType,DataValue from TGUDD_DEVICEUDD";
            using (OleDbCommand cmd = new OleDbCommand(AA21, conn))
            {
                StreamWriter tw = File.AppendText(st + "\\TempChanges\\text121.txt");
                try
                {

                    OleDbDataReader reader = cmd.ExecuteReader();
                    string str = @"
[DEVICEUDD]
FORMAT_DEVICEUDD=DeviceNumber,DeviceType,DataId,DataType,DataValue";
                    tw.WriteLine(str);
                    while (reader.Read())
                    {
                        tw.Write(reader["DeviceNumber"].ToString().Trim());
                        tw.Write("," + reader["DeviceType"].ToString().Trim());
                        tw.Write("," + reader["DataId"].ToString().Trim());
                        tw.Write("," + reader["DataType"].ToString().Trim());
                        tw.Write("," + reader["DataValue"].ToString().Trim());
                        // tw.WriteLine(", " + reader["datetime"].ToString());
                        tw.WriteLine();
                    }
                    // tw.WriteLine(DateTime.Now);

                    tw.Close();

                    reader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tw.Close();
                }
            }




            string DD = "select * from TGSOURCE_EQUIVALENT";

            using (OleDbCommand cmd3 = new OleDbCommand(DD, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text4.txt");
                try
                {
                  
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                }
            }




            string EE = "select * from TGSOURCE_LOADEQUIVALENT";

            using (OleDbCommand cmd3 = new OleDbCommand(EE, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text5.txt");
                try
                {
                   
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                }
            }





            string FF = "select * from TGLINE_OVERHEADLINE";

            using (OleDbCommand cmd3 = new OleDbCommand(FF, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text6.txt");
                try
                {
                   
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                }
            }





            string GG = "select * from TGLINE_UNDERGROUNDLINE";

            using (OleDbCommand cmd3 = new OleDbCommand(GG, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text7.txt");
                try
                {
                 
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                }
            }




            string MM = "select * from TGFEEDER_FORMAT_FEEDER";

            using (OleDbCommand cmd3 = new OleDbCommand(MM, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text8.txt");
                try
                {
                   
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[SECTION]
FORMAT_SECTION=SectionID,FromNodeID,FromNodeIndex,ToNodeID,ToNodeIndex,Phase,ZoneID,SubNetworkId,EnvironmentID 
FORMAT_FEEDER=NetworkID,HeadNodeID,CoordSet,Year,Description,Color,LoadFactor,LossLoadFactorK,Group1,Group2,Group3,Group4,Group5,TagText,TagProperties,TagDeltaX,TagDeltaY,TagAngle,TagAlignment,TagBorder,TagBackground,TagTextColor,TagBorderColor,TagBackgroundColor,TagLocation,TagFont,TagTextSize,TagOffset,Version,EnvironmentID
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        tw3.Write("FEEDER=" + reader3["NetworkID"].ToString().Trim());
                        //tw3.Write("," + reader3["HeadNodeID"].ToString().Trim());
                        //tw3.Write("," + reader3["CoordSet"].ToString().Trim());
                        //tw3.Write("," + reader3["Years"].ToString().Trim());
                        //tw3.Write("," + reader3["Description"].ToString().Trim());
                        //tw3.Write("," + reader3["Color"].ToString().Trim());
                        //tw3.Write("," + reader3["LoadFactor"].ToString().Trim());
                        //tw3.Write("," + reader3["LossLoadFactorK"].ToString().Trim());
                        //tw3.Write("," + reader3["Group1"].ToString().Trim());
                        //tw3.Write("," + reader3["Group2"].ToString().Trim());
                        //tw3.Write("," + reader3["Group3"].ToString().Trim());
                        //tw3.Write("," + reader3["TagText"].ToString().Trim());
                        //tw3.Write("," + reader3["TagProperties"].ToString().Trim());
                        //tw3.Write("," + reader3["TagDeltaX"].ToString().Trim());
                        //tw3.Write("," + reader3["TagDeltaY"].ToString().Trim());
                        //tw3.Write("," + reader3["TagAngle"].ToString().Trim());
                        //tw3.Write("," + reader3["TagAlignment"].ToString().Trim());
                        //tw3.Write("," + reader3["TagBorder"].ToString().Trim());
                        //tw3.Write("," + reader3["TagBackground"].ToString().Trim());
                        //tw3.Write("," + reader3["TagTextColor"].ToString().Trim());
                        //tw3.Write("," + reader3["TagBorderColor"].ToString().Trim());
                        //tw3.Write("," + reader3["TagBackgroundColor"].ToString().Trim());
                        //tw3.Write("," + reader3["TagLocation"].ToString().Trim());
                        //tw3.Write("," + reader3["TagFont"].ToString().Trim());
                        //tw3.Write("," + reader3["TagTextSize"].ToString().Trim());
                        //tw3.Write("," + reader3["TagOffset"].ToString().Trim());
                        //tw3.Write("," + reader3["Version"].ToString().Trim());
                        //tw3.Write("," + reader3["EnvironmentID"].ToString().Trim());

                        tw3.Write("," + reader3["HeadNodeID"].ToString().Trim());
                        tw3.Write("," + reader3["CoordSet"].ToString().Trim());
                        tw3.Write("," + reader3["Years"].ToString().Trim());
                        tw3.Write("," + reader3["Description"].ToString().Trim());
                        tw3.Write("," + reader3["Color"].ToString().Trim());
                        tw3.Write("," + reader3["LoadFactor"].ToString().Trim());
                        tw3.Write("," + reader3["LossLoadFactorK"].ToString().Trim());
                        tw3.Write("," + reader3["Group1"].ToString().Trim());
                        tw3.Write("," + reader3["Group2"].ToString().Trim());
                        tw3.Write("," + reader3["Group3"].ToString().Trim());
                        tw3.Write("," + reader3["Group4"].ToString().Trim());
                        tw3.Write("," + reader3["Group5"].ToString().Trim());
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                }
            }


            string ZZ = "select * from [TEMPSECTION-NOTBREAKED]";

            using (OleDbCommand cmd3 = new OleDbCommand(ZZ, conn))
            {
                //cmd.Connection = conn;
                //cmd.CommandText = "tblOutbox";
                //cmd.CommandType = CommandType.TableDirect;
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text10.txt");
                try
                {
                   
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
                    MessageBox.Show(ex.ToString());
                }
            }


            string YY = "select * from [TEMPSECTION-BREAKED]";

            using (OleDbCommand cmd3 = new OleDbCommand(YY, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text9.txt");
                try
                {
                 
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
                    MessageBox.Show(ex.ToString());
                }
            }


            string LL = "select * from TGDEVICE_DISTRIBUTIONTRANSFORMER";

            using (OleDbCommand cmd3 = new OleDbCommand(LL, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text11.txt");
                try
                {
                  
                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[TRANSFORMER SETTING]
FORMAT_TRANSFORMERSETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,CoordX,CoordY,Conn,PrimTap,SecondaryTap,RgPrim,XgPrim,RgSec,XgSec,ODPrimPh,PrimaryBaseVoltage,SecondaryBaseVoltage,FromNodeID,SettingOption,SetPoint,ControlType,LowerBandWidth,UpperBandWidth,TapLocation,InitialTapPosition,InitialTapPositionMode,Tap,MaxBuck,MaxBoost,CT,PT,Rset,Xset,FirstHouseHigh,FirstHouseLow,PhaseON,AtSectionID,MasterID,FaultIndicator,PhaseShiftType,GammaPhaseShift,CTPhase,PrimaryCornerGroundedPhase,SecondaryCornerGroundedPhase,ConnectionStatus,Reversible
";
                    tw3.WriteLine(str3);
                    while (reader3.Read())
                    {
                        string section = reader3["SectionID"].ToString().Trim();
                        if(section== "CA_4873217")
                        {

                        }

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
                    MessageBox.Show(ex.ToString());
                }
            }



            string JJ = "select * from TGDEVICE_SWITCH";

            using (OleDbCommand cmd3 = new OleDbCommand(JJ, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text12.txt");
                try
                {
                  
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
                    MessageBox.Show(ex.ToString());
                }
            }

            string NN = "select * from TGDEVICE_CIRCUITBREAKER";

            using (OleDbCommand cmd3 = new OleDbCommand(NN, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text13.txt");
                try
                {
                  
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
                    MessageBox.Show(ex.ToString());
                }
            }


            string KK = "select * from TGDEVICE_FUSE";

            using (OleDbCommand cmd3 = new OleDbCommand(KK, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text14.txt");
                try
                {
                 
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
                    MessageBox.Show(ex.ToString());
                }
            }

            string KK1 = "select * from TGDEVICE_RECLOSER";

            using (OleDbCommand cmd3 = new OleDbCommand(KK1, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text125.txt");
                try
                {

                    OleDbDataReader reader3 = cmd3.ExecuteReader();
                    string str3 = @"
[RECLOSER SETTING]
FORMAT_RECLOSERSETTING=SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,TCCID,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,EnableReclosing,FaultIndicator,EnableFuseSaving,MinRatedCurrentForFuseSaving,Automated,SensorMode,Strategic,RestorationMode,ConnectionStatus,TCCRepositoryID,TCCRepositoryAlternateID1,TCCRepositoryAlternateID2,TCCRepositoryAlternateID3,TCCRepositoryAlternateID4,TCCRepositoryAlternateID5,TCCRepositoryAlternateID6,TCCRepositoryAlternateID7,TCCRepositoryAlternateID8,TCCRepositoryAlternateID9,TCCRepositoryAlternateID10,IntellirupterTCCRepositoryID,ByPassOnRestoration,Reversible,TCCSettingsSelection
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
                            tw3.Write("," + reader3["EnableReclosing"].ToString().Trim());
                            tw3.Write("," + reader3["FaultIndicator"].ToString().Trim());
                            tw3.Write("," + reader3["EnableFuseSaving"].ToString().Trim());
                            tw3.Write("," + reader3["MinRatedCurrentForFuseSaving"].ToString().Trim());
                            tw3.Write("," + reader3["Automated"].ToString().Trim());
                            tw3.Write("," + reader3["SensorMode"].ToString().Trim());
                            tw3.Write("," + reader3["Strategic"].ToString().Trim());
                            tw3.Write("," + reader3["RestorationMode"].ToString().Trim());
                            tw3.Write("," + reader3["ConnectionStatus"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryID"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID1"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID2"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID3"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID4"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID5"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID6"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID7"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID8"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID9"].ToString().Trim());
                            tw3.Write("," + reader3["TCCRepositoryAlternateID10"].ToString().Trim());
                            tw3.Write("," + reader3["IntellirupterTCCRepositoryID"].ToString().Trim());
                            tw3.Write("," + reader3["ByPassOnRestoration"].ToString().Trim());
                            tw3.Write("," + reader3["Reversible"].ToString().Trim());
                            tw3.Write("," + reader3["TCCSettingsSelection"].ToString().Trim());


                            tw3.WriteLine();
                        }
                    }
                    tw3.Close();

                    reader3.Close();

                }
                catch (Exception ex)
                {
                    tw3.Close();
                    MessageBox.Show(ex.ToString());
                }
            }


            string cap = "select * from [TGDEVICE_SHUNTCAPACITOR]";
            using (OleDbCommand capcom = new OleDbCommand(cap, conn))
            {
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\textshunt25.txt");
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OleDbDataReader capRead = capcom.ExecuteReader();
                    string capstr = @"
[SHUNT CAPACITOR SETTING]
FORMAT_SHUNTCAPACITORSETTING=SectionID,DeviceNumber,DeviceStage,Location,Connection,FixedKVARA,FixedKVARB,FixedKVARC,FixedLossesA,FixedLossesB,FixedLossesC,SwitchedKVARA,SwitchedKVARB,SwitchedKVARC,SwitchedLossesA,SwitchedLossesB,SwitchedLossesC,ByPhase,VoltageOverride,VoltageOverrideOn,VoltageOverrideOff,VoltageOverrideDeadband,KV,Control,OnValueA,OnValueB,OnValueC,OffValueA,OffValueB,OffValueC,SwitchingMode,InitiallyClosedPhase,ControllingPhase,SensorLocation,ControlledNodeId,PythonDeviceScriptID,ShuntCapacitorID,ConnectionStatus,CTConnection,InterruptingRating
";
                    tw3.WriteLine(capstr);
                    while (capRead.Read())
                    {
                        string sectionId = Convert.ToString(capRead["SectionID"]).Trim();
                        string deviceNumber = Convert.ToString(capRead["DeviceNumber"]).Trim();
                        if (!string.IsNullOrWhiteSpace(sectionId) && !string.IsNullOrWhiteSpace(deviceNumber))
                        {
                            tw3.Write(System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SectionID"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["DeviceNumber"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["DeviceStage"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["Location"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["Connection"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedKVARA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedKVARB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedKVARC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedLossesA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedLossesB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["FixedLossesC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedKVARA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedKVARB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedKVARC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedLossesA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedLossesB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchedLossesC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["ByPhase"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["VoltageOverride"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["VoltageOverrideOn"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["VoltageOverrideOff"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["VoltageOverrideDeadband"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["KV"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["Control"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OnValueA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OnValueB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OnValueC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OffValueA"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OffValueB"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["OffValueC"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SwitchingMode"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["InitiallyClosedPhase"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["ControllingPhase"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["SensorLocation"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["ControlledNodeId"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["PythonDeviceScriptID"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["ShuntCapacitorID"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["ConnectionStatus"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["CTConnection"]), @"\t|\n|\r", ""));
                            tw3.Write("," + System.Text.RegularExpressions.Regex.Replace(Convert.ToString(capRead["InterruptingRating"]), @"\t|\n|\r", ""));

                            tw3.WriteLine();
                        }
                    }
                    capRead.Close();
                    tw3.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tw3.Close();
                }
            }


            string XX = "select * from TGNODE_INTERMEDIATENODES";

            using (OleDbCommand cmd3 = new OleDbCommand(XX, conn))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text15.txt");
                try
                {
                 
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
                    MessageBox.Show(ex.ToString());
                    tw3.Close();
                 
                }
            }



            //Solar
            string SOLAR = "select * from TGDEVICE_SOLAR";

            using (OleDbCommand cmd2 = new OleDbCommand(SOLAR, conn))
            {
                StreamWriter tw2 = File.AppendText(st + "\\TempChanges\\textS2.txt");
                try
                {

                    OleDbDataReader reader2 = cmd2.ExecuteReader();
                    string str1 = @"
[PHOTOVOLTAIC SETTINGS]
FORMAT_PHOTOVOLTAICSETTING=SectionID,Location,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,EquipmentID,NumberOfGenerators,SymbolSize,NS,NP,AmbientTemperature,FaultContributionBasedOnRatedPower,FaultContributionUnit,FaultContribution,ConstantInsolation,ForceT0,InsolationModelID,FrequencySourceID,SourceHarmonicModelType,PulseNumber,FiringAngle,OverlapAngle,ConnectionStatus,ConnectionConfiguration,CTConnection,Phase";
                    tw2.WriteLine(str1);
                    while (reader2.Read())
                    {
                        string deviceno = reader2["SECTIONID"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {

                            tw2.Write(reader2["SECTIONID"].ToString().Trim());
                            tw2.Write("," + reader2["Location"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceNumber"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceStage"].ToString().Trim());
                            tw2.Write("," + reader2["Flags"].ToString().Trim());
                            tw2.Write("," + reader2["InitFromEquipFlags"].ToString().Trim());
                            tw2.Write("," + reader2["EquipmentID"].ToString().Trim());
                            tw2.Write("," + reader2["NumberOfGenerators"].ToString().Trim());
                            tw2.Write("," + reader2["SymbolSize"].ToString().Trim());
                            tw2.Write("," + reader2["NS"].ToString().Trim());
                            tw2.Write("," + reader2["NP"].ToString().Trim());
                            tw2.Write("," + reader2["AmbientTemperature"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionBasedOnRatedPower"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionUnit"].ToString().Trim());
                            // PulseNumber,FiringAngle,OverlapAngle,ConnectionStatus,ConnectionConfiguration,CTConnection,Phase
                            tw2.Write("," + reader2["FaultContribution"].ToString().Trim());
                            tw2.Write("," + reader2["ConstantInsolation"].ToString().Trim());
                            tw2.Write("," + reader2["ForceT0"].ToString().Trim());
                            tw2.Write("," + reader2["InsolationModelID"].ToString().Trim());
                            tw2.Write("," + reader2["FrequencySourceID"].ToString().Trim());
                            tw2.Write("," + reader2["SourceHarmonicModelType"].ToString().Trim());
                            tw2.Write("," + reader2["PulseNumber"].ToString().Trim());
                            tw2.Write("," + reader2["FiringAngle"].ToString().Trim());
                            tw2.Write("," + reader2["OverlapAngle"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionStatus"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionConfiguration"].ToString().Trim());
                            tw2.Write("," + reader2["CTConnection"].ToString().Trim());
                            tw2.Write("," + reader2["Phase"].ToString().Trim());
                            tw2.WriteLine();
                        }
                    }
                    tw2.Close();

                    reader2.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tw2.Close();
                }
            }

            //Battery

            string BATTERYSTORAG = "select * from TGDEVICE_BATTERYSTORAG";

            using (OleDbCommand cmd2 = new OleDbCommand(BATTERYSTORAG, conn))
            {
                StreamWriter tw2 = File.AppendText(st + "\\TempChanges\\textB3.txt");
                try
                {

                    OleDbDataReader reader2 = cmd2.ExecuteReader();
                    string str1 = @"
[BESS SETTINGS]
FORMAT_BESSSETTING=SectionID,Location,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,EquipmentID,Phase,ConnectionStatus,ConnectionConfiguration,CTConnection,SymbolSize,MaximumSOC,MinimumSOC,FaultContributionBasedOnRatedPower,FaultContributionUnit,FaultContribution,FrequencySourceID,SourceHarmonicModelType,PulseNumber,FiringAngle,OverlapAngle,InitialSOC,GridOutput,RiseFallUnit,PowerFallLimit,PowerRiseLimit,ChargeDelayUnit,ChargeDelay,DischargeDelayUnit,DischargeDelay,PythonDeviceScriptID ";
                    tw2.WriteLine(str1);
                    while (reader2.Read())
                    {
                        string deviceno = reader2["SECTIONID"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {

                            tw2.Write(reader2["SectionID"].ToString().Trim());
                            tw2.Write("," + reader2["Location"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceNumber"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceStage"].ToString().Trim());
                            tw2.Write("," + reader2["Flags"].ToString().Trim());
                            tw2.Write("," + reader2["InitFromEquipFlags"].ToString().Trim());
                            tw2.Write("," + reader2["EquipmentID"].ToString().Trim());
                            tw2.Write("," + reader2["Phase"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionStatus"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionConfiguration"].ToString().Trim());
                            tw2.Write("," + reader2["CTConnection"].ToString().Trim());
                            tw2.Write("," + reader2["SymbolSize"].ToString().Trim());
                            tw2.Write("," + reader2["MaximumSOC"].ToString().Trim());
                            tw2.Write("," + reader2["MinimumSOC"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionBasedOnRatedPower"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionUnit"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContribution"].ToString().Trim());
                            tw2.Write("," + reader2["FrequencySourceID"].ToString().Trim());
                            tw2.Write("," + reader2["SourceHarmonicModelType"].ToString().Trim());
                            tw2.Write("," + reader2["PulseNumber"].ToString().Trim());
                            tw2.Write("," + reader2["FiringAngle"].ToString().Trim());
                            tw2.Write("," + reader2["OverlapAngle"].ToString().Trim());
                            tw2.Write("," + reader2["InitialSOC"].ToString().Trim());
                            tw2.Write("," + reader2["GridOutput"].ToString().Trim());
                            tw2.Write("," + reader2["RiseFallUnit"].ToString().Trim());
                            tw2.Write("," + reader2["PowerFallLimit"].ToString().Trim());
                            tw2.Write("," + reader2["PowerRiseLimit"].ToString().Trim());
                            tw2.Write("," + reader2["ChargeDelayUnit"].ToString().Trim());
                            tw2.Write("," + reader2["ChargeDelay"].ToString().Trim());
                            tw2.Write("," + reader2["DischargeDelayUnit"].ToString().Trim());
                            tw2.Write("," + reader2["DischargeDelay"].ToString().Trim());
                            tw2.Write("," + reader2["PythonDeviceScriptID"].ToString().Trim());
                            tw2.WriteLine();
                        }
                    }
                    tw2.Close();

                    reader2.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tw2.Close();
                }
            }

            string WINDMILL = "select * from TGDEVICE_WINDMILL";

            using (OleDbCommand cmd2 = new OleDbCommand(WINDMILL, conn))
            {
                StreamWriter tw2 = File.AppendText(st + "\\TempChanges\\text102.txt");
                try
                {

                    OleDbDataReader reader2 = cmd2.ExecuteReader();
                    string str1 = @"
[WECS SETTINGS]
FORMAT_WECSSETTING=SectionID,Location,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,EquipmentID,NumberOfGenerators,SymbolSize,ConstantWindSpeed,ForceT0,WindModelID,FrequencySourceID,SourceHarmonicModelType,PulseNumber,FiringAngle,OverlapAngle,ConnectionStatus,ConnectionConfiguration,Phase,FaultContributionBasedOnRatedPower,FaultContributionUnit,FaultContribution,CTConnection";
                    tw2.WriteLine(str1);
                    while (reader2.Read())
                    {
                        string deviceno = reader2["SECTIONID"].ToString().Trim();
                        if (!string.IsNullOrEmpty(deviceno))
                        {
                            tw2.Write(reader2["SECTIONID"].ToString().Trim());
                            tw2.Write("," + reader2["Location"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceNumber"].ToString().Trim());
                            tw2.Write("," + reader2["DeviceStage"].ToString().Trim());
                            tw2.Write("," + reader2["Flags"].ToString().Trim());
                            tw2.Write("," + reader2["InitFromEquipFlags"].ToString().Trim());
                            tw2.Write("," + reader2["EquipmentID"].ToString().Trim());
                            tw2.Write("," + reader2["NumberOfGenerators"].ToString().Trim());
                            tw2.Write("," + reader2["SymbolSize"].ToString().Trim());
                            tw2.Write("," + reader2["ConstantWindSpeed"].ToString().Trim());
                            tw2.Write("," + reader2["ForceT0"].ToString().Trim());
                            tw2.Write("," + reader2["WindModelID"].ToString().Trim());
                            tw2.Write("," + reader2["FrequencySourceID"].ToString().Trim());
                            tw2.Write("," + reader2["SourceHarmonicModelType"].ToString().Trim());
                            tw2.Write("," + reader2["PulseNumber"].ToString().Trim());
                            tw2.Write("," + reader2["FiringAngle"].ToString().Trim());
                            tw2.Write("," + reader2["OverlapAngle"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionStatus"].ToString().Trim());
                            tw2.Write("," + reader2["ConnectionConfiguration"].ToString().Trim());
                            tw2.Write("," + reader2["Phase"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionBasedOnRatedPower"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContributionUnit"].ToString().Trim());
                            tw2.Write("," + reader2["FaultContribution"].ToString().Trim());
                            tw2.Write("," + reader2["CTConnection"].ToString().Trim());
                            tw2.WriteLine();
                        }
                    }
                    tw2.Close();

                    reader2.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tw2.Close();
                }
            }



            //            string II = "select * from TGDTAG_DEVICETAG";

            //            using (OleDbCommand cmd3 = new OleDbCommand(II, conn))
            //            {
            //                StreamWriter tw3 = File.AppendText(st + "\\TempChanges\\text16.txt");
            //                try
            //                {

            //                    OleDbDataReader reader3 = cmd3.ExecuteReader();
            //                    string str3 = @"
            //[DEVICETAG]
            //FORMAT_DEVICETAG=DeviceNumber,DeviceType,TagText,TagProperties,TagDeltaX,TagDeltaY,TagAngle,TagAlignment,TagBorder,TagBackground,TagTextColor,TagBorderColor,TagBackgroundColor,TagLocation,TagFont,TagTextSize,TagOffset
            //";
            //                    tw3.WriteLine(str3);
            //                    while (reader3.Read())
            //                    {
            //                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
            //                        if (!string.IsNullOrEmpty(deviceno))
            //                        {
            //                            tw3.Write(reader3["DeviceNumber"].ToString().Trim());
            //                            tw3.Write("," + reader3["DeviceType"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagText"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagProperties"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagDeltaX"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagDeltaY"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagAngle"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagAlignment"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagBorder"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagBackground"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagTextColor"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagBorderColor"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagBackgroundColor"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagLocation"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagFont"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagTextSize"].ToString().Trim());
            //                            tw3.Write("," + reader3["TagOffset"].ToString().Trim());

            //                            // tw.WriteLine(", " + reader["datetime"].ToString());
            //                            tw3.WriteLine();
            //                        }
            //                    }
            //                    // tw.WriteLine(DateTime.Now);

            //                    tw3.Close();

            //                    reader3.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    tw3.Close();
            //                    MessageBox.Show(ex.ToString());
            //                }
            //            }


            //            string jj = "select * from TGUDD_NETWORKUDD";
            //            using (OleDbCommand cmd3 = new OleDbCommand(jj, conn))
            //            {
            //                StreamWriter sw = File.AppendText(st + "\\TempChanges\\text17.txt");
            //                try
            //                {

            //                    OleDbDataReader reader3 = cmd3.ExecuteReader();

            //                    string str3 = @"
            //[NETWORKUDD]
            //FORMAT_NETWORKUDD=NetworkId,DataId,DataType,DataValue
            //";
            //                    sw.WriteLine(str3);
            //                    while (reader3.Read())
            //                    {
            //                        sw.Write(reader3["NetworkId"].ToString().Trim());
            //                        sw.Write("," + reader3["DataId"].ToString().Trim());
            //                        sw.Write("," + reader3["DataType"].ToString().Trim());
            //                        sw.Write("," + reader3["DataValue"].ToString().Trim());

            //                        sw.WriteLine();
            //                    }

            //                    sw.Close();
            //                    reader3.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    sw.Close();
            //                    MessageBox.Show(ex.ToString());
            //                }


            //            }


            //            string kk = "select * from TGUDD_NODEUDD";
            //            using (OleDbCommand cmd3 = new OleDbCommand(kk, conn))
            //            {
            //                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text18.txt");
            //                try
            //                {

            //                    OleDbDataReader reader3 = cmd3.ExecuteReader();

            //                    string str3 = @"
            //[NODEUDD]
            //FORMAT_NODEUDD=NodeId,DataId,DataType,DataValue
            //";

            //                    sw.WriteLine(str3);

            //                    while (reader3.Read())
            //                    {
            //                        sw.Write(reader3["NodeId"].ToString().Trim());
            //                        sw.Write("," + reader3["DataId"].ToString().Trim());
            //                        sw.Write("," + reader3["DataType"].ToString().Trim());
            //                        sw.Write("," + reader3["DataValue"].ToString().Trim());
            //                        sw.WriteLine();
            //                    }

            //                    sw.Close();
            //                    reader3.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    sw.Close();
            //                    MessageBox.Show(ex.ToString());
            //                }

            //            }


            //            string ll = "select * from TGUDD_SECTIONUDD";
            //            using (OleDbCommand cmd3 = new OleDbCommand(ll, conn))
            //            {
            //                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text19.txt");
            //                try
            //                {

            //                    OleDbDataReader reader3 = cmd3.ExecuteReader();

            //                    string str3 = @"
            //[SECTIONUDD]
            //FORMAT_SECTIONUDD=SectionId,DataId,DataType,DataValue
            //";

            //                    sw.WriteLine(str3);

            //                    while (reader3.Read())
            //                    {
            //                        string section = reader3["SectionId"].ToString().Trim();
            //                        if (!string.IsNullOrEmpty(section))
            //                        {
            //                            sw.Write(reader3["SectionId"].ToString().Trim());
            //                            sw.Write("," + reader3["DataId"].ToString().Trim());
            //                            sw.Write("," + reader3["DataType"].ToString().Trim());
            //                            sw.Write("," + reader3["DataValue"].ToString().Trim());
            //                            sw.WriteLine();
            //                        }
            //                    }

            //                    sw.Close();
            //                    reader3.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    sw.Close();
            //                    MessageBox.Show(ex.ToString());
            //                }
            //            }


            //            string mm = "select * from TGUDD_DEVICEUDD";
            //            using (OleDbCommand cmd3 = new OleDbCommand(mm, conn))
            //            {
            //                StreamWriter sw = new StreamWriter(st + "\\TempChanges\\text20.txt");
            //                try
            //                {

            //                    OleDbDataReader reader3 = cmd3.ExecuteReader();

            //                    string str3 = @"
            //[DEVICEUDD]
            //FORMAT_DEVICEUDD=DeviceNumber,DeviceType,DataId,DataType,DataValue
            //";

            //                    sw.WriteLine(str3);

            //                    while (reader3.Read())
            //                    {
            //                        string deviceno = reader3["DeviceNumber"].ToString().Trim();
            //                        if (!string.IsNullOrEmpty(deviceno))
            //                        {
            //                            sw.Write(reader3["DeviceNumber"].ToString().Trim());
            //                            sw.Write("," + reader3["DeviceType"].ToString().Trim());
            //                            sw.Write("," + reader3["DataId"].ToString().Trim());
            //                            sw.Write("," + reader3["DataType"].ToString().Trim());
            //                            sw.Write("," + reader3["DataValue"].ToString().Trim());
            //                            sw.WriteLine();
            //                        }
            //                    }
            //                    sw.Close();
            //                    reader3.Close();

            //                }
            //                catch (Exception ex)
            //                {
            //                    sw.Close();
            //                    MessageBox.Show(ex.ToString());
            //                }



            //            }




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
                { MessageBox.Show(ex.ToString()); }

                
                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text1.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text2.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text3.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text4.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text5.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text6.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

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
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text9.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text10.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text11.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text12.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text13.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text14.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text15.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text121.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text125.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }


                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\textshunt25.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\textS2.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\textB3.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text102.txt"))
                    {
                        writer.Write(reader5.ReadToEnd());
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }


                //try
                //{
                //    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text16.txt"))
                //    {
                //        writer.Write(reader5.ReadToEnd());
                //    }
                //}
                //catch (Exception ex)
                //{ MessageBox.Show(ex.ToString()); }

                //try
                //{
                //    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text17.txt"))
                //    {
                //        writer.Write(reader5.ReadToEnd());
                //    }
                //}
                //catch (Exception ex)
                //{ MessageBox.Show(ex.ToString()); }

                //try
                //{
                //    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text18.txt"))
                //    {
                //        writer.Write(reader5.ReadToEnd());
                //    }
                //}
                //catch (Exception ex)
                //{ MessageBox.Show(ex.ToString()); }

                //try
                //{
                //    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text19.txt"))
                //    {
                //        writer.Write(reader5.ReadToEnd());
                //    }
                //}
                //catch (Exception ex)
                //{ MessageBox.Show(ex.ToString()); }

                //try
                //{
                //    using (StreamReader reader5 = File.OpenText(st + "\\TempChanges\\text20.txt"))
                //    {
                //        writer.Write(reader5.ReadToEnd());
                //    }
                //}
                //catch (Exception ex)
                //{ MessageBox.Show(ex.ToString()); }

            }

            return ("1");

          
        }
    }
}
