using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization;

namespace TechGration.AppCode
{
    class GetAllDeviceData
    {
        public void GetTGLINE_UNDERGROUNDLINEData(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            if (connn2.State == ConnectionState.Closed)
            {
                connn2.Open();
            }

            try
            {
 
                string quarry = "select *from GIS_F09_PC03_DUONGDAY_TT WHERE PhanLoai = '2'";
                OleDbDataAdapter ad = new OleDbDataAdapter(quarry, connn2);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                int nn = dt.Rows.Count;

                String SectionID = null;

                String DeviceStage = "";
                String Flags = "0";
                String InitFromEquipFlags = "0";
                String LineCableID = "3P";
                string Length;
                String NumberOfCableInParallel = "1";
                String Amps = "0";
                String Amps_1 = "0";
                String Amps_2 = "0";
                String Amps_3 = "0";
                String Amps_4 = "0";
                String ConnectionStatus = "0";
                string CoordX = null;
                string CoordY = null;
                String HarmonicModel = "2";
                String EarthResistivity = "100";
                String OperatingTemperature = "25";
                String Height = "0";
                String DistanceBetweenConductors = "10";
                String BondingType = "0";
                String CableConfiguration = "2";
                String DuctMaterial = "0";
                String Bundled = "1";
                String Neutral1Type = "29";
                String Neutral2Type = "29";
                String Neutral3Type = "29";
                String Neutral1ID = "";
                String Neutral2ID = "";
                String Neutral3ID = "";
                String AmpacityDeratingFactor = "1";
                String FlowConstraintActive = "0";
                String FlowConstraintUnit = "0";
                String MaximumFlow = "100";
                String Voltage = "";
                String NumberofCore = "3";
                String CableSize = "";
                String InsulationType = "";
                string CMMMM = string.Empty;
                string ConductorMaterial = "";
                string ConductorType = "";
                string rm_character = string.Empty;
                string SoPha = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //ConductorMaterial = dt.Rows[i]["MaHieu"].ToString();
                    ConductorMaterial = dt.Rows[i]["LoaiDayDan"].ToString();
                    ConductorType = dt.Rows[i]["LoaiDayDan"].ToString();
                    SoPha = dt.Rows[i]["SoPha"].ToString();
                    Voltage = dt.Rows[i]["DienAp"].ToString();
                   // Voltage = updatevoltagecodevalue(Voltage);
                   // rm_character = (Voltage.ToUpper()).Replace(" KV", "");
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                   // NumberofCore = dt.Rows[i]["NumberofCore"].ToString();
                    CableSize = dt.Rows[i]["TietDienDay"].ToString();
                    //InsulationType = dt.Rows[i]["LoaiDayDan"].ToString();
                    Length = dt.Rows[i]["ChieuDai"].ToString();

                    if (SectionID.Contains("OH"))
                    {
                        SectionID = SectionID.Replace("OH", "CA");
                    }
                    else
                    {
                        SectionID = SectionID;
                    }


                    if (ConductorMaterial == "Dây nhôm lõi thép trần" || ConductorMaterial == "Dây nhôm lõi thép trần có bôi mỡ bảo vệ" || ConductorMaterial == "Dây nhôm bọc" || ConductorMaterial == "Dây đồng bọc" || ConductorMaterial == "Khác")
                    {
                        ConductorMaterial = "ACSR";
                    }
                    if (ConductorMaterial == "Dây bọc bán phần")
                    {
                        ConductorMaterial = "COPPER_BUSBAR";
                    }

                    InsulationType = ConductorMaterial;

                    // LineCableID = Voltage + "_" + ConductorType + "_" + TietDienDay + "SQMM";

                    // LineCableID = Voltage + "_" + ConductorMaterial + "_" + NumberofCore + "C_" + CableSize + "SQMM";
                    LineCableID = SoPha + "P_" + Voltage + "_" + CableSize + "SQMM_"+"XLPE";

                     

                    string PP = "INSERT INTO TGLINE_UNDERGROUNDLINE (SectionID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,LineCableID,Length,NumberOfCableInParallel,Amps,Amps_1,Amps_2,Amps_3,Amps_4,ConnectionStatus,CoordX,CoordY,HarmonicModel,EarthResistivity,OperatingTemperature,Height,DistanceBetweenConductors,BondingType,CableConfiguration,DuctMaterial,Bundled,Neutral1Type,Neutral2Type,Neutral3Type,Neutral1ID,Neutral2ID,Neutral3ID,AmpacityDeratingFactor,FlowConstraintActive,FlowConstraintUnit,MaximumFlow) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35)";
                   
                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", SectionID);
                    comr.Parameters.AddWithValue("@P3", DeviceStage);
                    comr.Parameters.AddWithValue("@P4", Flags);
                    comr.Parameters.AddWithValue("@P5", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P6", LineCableID);
                    comr.Parameters.AddWithValue("@P7", Length);
                    comr.Parameters.AddWithValue("@P8", NumberOfCableInParallel);
                    comr.Parameters.AddWithValue("@P9", Amps);
                    comr.Parameters.AddWithValue("@P10", Amps_1);
                    comr.Parameters.AddWithValue("@P11", Amps_2);
                    comr.Parameters.AddWithValue("@P12", Amps_3);
                    comr.Parameters.AddWithValue("@P13", Amps_4);
                    comr.Parameters.AddWithValue("@P14", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P15", Convert.ToInt32(CoordX));
                    comr.Parameters.AddWithValue("@P16", Convert.ToInt32(CoordY));
                    comr.Parameters.AddWithValue("@P17", HarmonicModel);
                    comr.Parameters.AddWithValue("@P18", EarthResistivity);
                    comr.Parameters.AddWithValue("@P19", OperatingTemperature);
                    comr.Parameters.AddWithValue("@P20", Height);
                    comr.Parameters.AddWithValue("@P21", DistanceBetweenConductors);
                    comr.Parameters.AddWithValue("@P22", BondingType);
                    comr.Parameters.AddWithValue("@P23", CableConfiguration);
                    comr.Parameters.AddWithValue("@P24", DuctMaterial);
                    comr.Parameters.AddWithValue("@P25", Bundled);
                    comr.Parameters.AddWithValue("@P26", Neutral1Type);
                    comr.Parameters.AddWithValue("@P27", Neutral2Type);
                    comr.Parameters.AddWithValue("@P28", Neutral3Type);
                    comr.Parameters.AddWithValue("@P29", Neutral1ID);
                    comr.Parameters.AddWithValue("@P30", Neutral2ID);
                    comr.Parameters.AddWithValue("@P31", Neutral3ID);
                    comr.Parameters.AddWithValue("@P32", AmpacityDeratingFactor);
                    comr.Parameters.AddWithValue("@P33", FlowConstraintActive);
                    comr.Parameters.AddWithValue("@P34", FlowConstraintUnit);
                    comr.Parameters.AddWithValue("@P35", MaximumFlow);

                    comr.ExecuteNonQuery();

                }
                Thread.Sleep(1000);


            }
            catch (Exception ex)
            {
                 
                MessageBox.Show(ex.ToString() + Feederid);


            }
 
        }
         
        public void GetTGLINE_OVERHEADLINEData(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2) 
        {
            try
            {
                //string quarry = "select Voltage,ConductorSize,ConductorType,ConductorMaterial,ShapeLength,GISID from GIS_CONDUCTOR";
                string quarry = "select *from GIS_F09_PC03_DUONGDAY_TT WHERE PhanLoai = '1'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                String SectionID = null;
                String DeviceNumber = null;
                String DeviceStage = "";
                int Flags = 0;
                int InitFromEquipFlags = 0;
                String LineCableID = "3P";
                string Length;
                int ConnectionStatus = 0;
                string CoordX = null;
                string CoordY = null;
                int HarmonicModel = 2;
                int FlowConstraintActive = 0;
                int FlowConstraintUnit = 0;
                int MaximumFlow = 100;
                int SeriesCompensationActive = 0;
                int MaxReactanceMultiplier = 50;
                int SeriesCompensationCost = 0;
                String Voltage = "";
                String ConductorSize = "";
                String ConductorType = "";
                String ConductorMaterial = "";
                string rm_character = string.Empty;
                string TietDienDay = string.Empty;
                string SoPha = string.Empty;
                string GhiChuGIS = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Voltage = dt.Rows[i]["DienAp"].ToString();
                   // ConductorType = dt.Rows[i]["LoaiDayDan"].ToString(); 
                    ConductorSize = dt.Rows[i]["TietDienDay"].ToString();
                    SoPha = dt.Rows[i]["SoPha"].ToString();
                    GhiChuGIS = dt.Rows[i]["GhiChuGIS"].ToString();                 
                    ConductorMaterial = dt.Rows[i]["MaHieu"].ToString();
                    ConductorType = dt.Rows[i]["LoaiDayDan"].ToString();
                    TietDienDay = dt.Rows[i]["TietDienDay"].ToString(); 
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();

                    if (ConductorType == "Dây nhôm lõi thép trần" || ConductorType == "Dây nhôm lõi thép trần có bôi mỡ bảo vệ" || ConductorType == "Dây nhôm bọc" || ConductorType == "Dây đồng bọc" || ConductorType == "Khác")
                    {
                        ConductorType = "ACSR";
                    }
                    if (ConductorType == "Dây bọc bán phần")
                    {
                        ConductorType = "COPPER_BUSBAR";
                    }
                     

                    //LineCableID = Voltage + "_" + ConductorType+"_" + TietDienDay+"SQMM";
                    LineCableID = SoPha +"P_" + Voltage + "_" + GhiChuGIS + "SQMM";
                    //DeviceStage = dt.Rows[i]["DeviceStage"].ToString();
                    Length = dt.Rows[i]["ChieuDai"].ToString(); ;
                    string PP = "INSERT INTO TGLINE_OVERHEADLINE (SectionID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,LineCableID,Length,ConnectionStatus,CoordX,CoordY,HarmonicModel,FlowConstraintActive,FlowConstraintUnit,MaximumFlow,SeriesCompensationActive,MaxReactanceMultiplier,SeriesCompensationCost) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)";

                    //if (connn2.State != ConnectionState.Open)
                    //{
                    //    connn2.Open();
                    //}
                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", DeviceNumber);
                    comr.Parameters.AddWithValue("@P3", DeviceStage);
                    comr.Parameters.AddWithValue("@P4", Flags);
                    comr.Parameters.AddWithValue("@P5", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P6", LineCableID);
                    comr.Parameters.AddWithValue("@P7", Length);
                    comr.Parameters.AddWithValue("@P8", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P9", Convert.ToInt32(CoordX));
                    comr.Parameters.AddWithValue("@P10", Convert.ToInt32(CoordY));
                    comr.Parameters.AddWithValue("@P11", HarmonicModel);
                    comr.Parameters.AddWithValue("@P12", FlowConstraintActive);
                    comr.Parameters.AddWithValue("@P13", FlowConstraintUnit);
                    comr.Parameters.AddWithValue("@P14", MaximumFlow);
                    comr.Parameters.AddWithValue("@P15", SeriesCompensationActive);
                    comr.Parameters.AddWithValue("@P16", MaxReactanceMultiplier);
                    comr.Parameters.AddWithValue("@P17", SeriesCompensationCost);

                    comr.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            //BB
            try
            {
                //string quarry = "select Voltage,ConductorSize,ConductorType,ConductorMaterial,ShapeLength,GISID from GIS_CONDUCTOR";
                string quarry = "select *from GIS_F09_PC03_DUONGDAY_TT WHERE PhanLoai = '3'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                String SectionID = null;
                String DeviceNumber = null;
                String DeviceStage = "";
                int Flags = 0;
                int InitFromEquipFlags = 0;
                String LineCableID = "3P";
                string Length;
                int ConnectionStatus = 0;
                string CoordX = null;
                string CoordY = null;
                int HarmonicModel = 2;
                int FlowConstraintActive = 0;
                int FlowConstraintUnit = 0;
                int MaximumFlow = 100;
                int SeriesCompensationActive = 0;
                int MaxReactanceMultiplier = 50;
                int SeriesCompensationCost = 0;
                String Voltage = "";
                String ConductorSize = "";
                String ConductorType = "";
                String ConductorMaterial = "";
                string rm_character = string.Empty;
                string TietDienDay = string.Empty;
                string SoPha = string.Empty;
                string GhiChuGIS = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Voltage = dt.Rows[i]["DienAp"].ToString();
                    SoPha = dt.Rows[i]["SoPha"].ToString();
                    GhiChuGIS = dt.Rows[i]["GhiChuGIS"].ToString();
                    // ConductorType = dt.Rows[i]["LoaiDayDan"].ToString(); 
                    ConductorSize = dt.Rows[i]["TietDienDay"].ToString();
                    ConductorMaterial = dt.Rows[i]["MaHieu"].ToString();
                    ConductorType = dt.Rows[i]["LoaiDayDan"].ToString();
                    TietDienDay = dt.Rows[i]["TietDienDay"].ToString();
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                    if (SectionID.Contains("OH"))
                    {
                        SectionID = SectionID.Replace("OH", "BB");
                    }
                    else
                    {
                        SectionID = SectionID;
                    }

                    if (ConductorType == "Dây nhôm lõi thép trần" || ConductorType == "Dây nhôm lõi thép trần có bôi mỡ bảo vệ" || ConductorType == "Dây nhôm bọc" || ConductorType == "Dây đồng bọc" || ConductorType == "Khác")
                    {
                        ConductorType = "ACSR";
                    }
                    if (ConductorType == "Dây bọc bán phần")
                    {
                        ConductorType = "COPPER_BUSBAR";
                    }


                    //LineCableID = Voltage + "_" + ConductorType + "_" + TietDienDay + "SQMM";
                    LineCableID = SoPha + "P_" + Voltage + "_" + GhiChuGIS + "SQMM";
                    //DeviceStage = dt.Rows[i]["DeviceStage"].ToString();
                    Length = dt.Rows[i]["ChieuDai"].ToString(); ;
                    string PP = "INSERT INTO TGLINE_OVERHEADLINE (SectionID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,LineCableID,Length,ConnectionStatus,CoordX,CoordY,HarmonicModel,FlowConstraintActive,FlowConstraintUnit,MaximumFlow,SeriesCompensationActive,MaxReactanceMultiplier,SeriesCompensationCost) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)";

                    //if (connn2.State != ConnectionState.Open)
                    //{
                    //    connn2.Open();
                    //}
                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", SectionID);
                    comr.Parameters.AddWithValue("@P3", DeviceStage);
                    comr.Parameters.AddWithValue("@P4", Flags);
                    comr.Parameters.AddWithValue("@P5", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P6", LineCableID);
                    comr.Parameters.AddWithValue("@P7", Length);
                    comr.Parameters.AddWithValue("@P8", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P9", Convert.ToInt32(CoordX));
                    comr.Parameters.AddWithValue("@P10", Convert.ToInt32(CoordY));
                    comr.Parameters.AddWithValue("@P11", HarmonicModel);
                    comr.Parameters.AddWithValue("@P12", FlowConstraintActive);
                    comr.Parameters.AddWithValue("@P13", FlowConstraintUnit);
                    comr.Parameters.AddWithValue("@P14", MaximumFlow);
                    comr.Parameters.AddWithValue("@P15", SeriesCompensationActive);
                    comr.Parameters.AddWithValue("@P16", MaxReactanceMultiplier);
                    comr.Parameters.AddWithValue("@P17", SeriesCompensationCost);

                    comr.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

        }
        public void GetTGUDD_DEVICEUDD(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            //dt...... 5
            try
            {

                //string quarry = "select OBJECTID,HighVoltageSideVolts,DTCCode,DTCName from GIS_DISTRIBUTIONTRANSFORMER";
                string quarry = "select OBJECTID, CapDienApVao, DanhSoThietBi from GIS_F04_PC03_MAYBIENAP_TT";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                string OBJECTID = string.Empty;
                string DataId = "VOLTAGE";
                string DataId1 = "DTCName";

                //string DataType = string.Empty;
                string DataValue = string.Empty;
                string Voltage = string.Empty;
                string DTCName = string.Empty;
                int DeviceType = 5;
                int DataType = 8;
                string DTCCode = string.Empty;
                string DTCName1 = string.Empty;
                string Obj_DTCCode_DTCName = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataValue = dt.Rows[i]["CapDienApVao"].ToString();
                    //OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                    //DTCCode = dt.Rows[i]["DTCCode"].ToString();
                    DTCName = dt.Rows[i]["DanhSoThietBi"].ToString();

                    //DTCName1 = DTCName.Contains(",") ? DTCName.Replace(",", "_") : DTCName;

                    OBJECTID = dt.Rows[i]["OBJECTID"].ToString();

                    if (OBJECTID == "DT_1002")
                    {

                    }

                    if (DTCName.Contains(","))
                    {
                        DTCName1 = DTCName.Replace(",", "_");
                    }
                    else
                    {
                        DTCName1 = DTCName;
                    }
                    

                    //string DTCName1 = DTCName.Replace(",", "_");
                    
                    //DataValue = updatevoltagecodevalue(DataValue);
                    //DataValue = (Voltage.ToUpper()).Replace(" KV", "");

                    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", OBJECTID);
                    comr.Parameters.AddWithValue("@P2", DeviceType);
                    comr.Parameters.AddWithValue("@P3", DataId1);
                    comr.Parameters.AddWithValue("@P4", DataType);
                    comr.Parameters.AddWithValue("@P5", DTCName1);


                    comr.ExecuteNonQuery();

                }

                //for (int k = 0; k < dt.Rows.Count; k++)
                //{
                //    DataValue = dt.Rows[k]["HighVoltageSideVolts"].ToString();
                //    DTCCode = dt.Rows[k]["DTCCode"].ToString();
                //    DTCName = dt.Rows[k]["DTCName"].ToString();
                //    string DTCName1 = DTCName.Replace(",", "_");
                //    OBJECTID = dt.Rows[k]["OBJECTID"].ToString()+'_'+ DTCCode+'_'+ DTCName1;
                //    //Obj_DTCCode_DTCName = OBJECTID + "_" + DTCCode + "_" + DTCName;
                //    DataValue = updatevoltagecodevalue(DataValue);
                //    //DataValue = (Voltage.ToUpper()).Replace(" KV", "");

                //    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                //    OleDbCommand comr = new OleDbCommand(PP, connn2);
                //    comr.Parameters.AddWithValue("@P1", OBJECTID);
                //    comr.Parameters.AddWithValue("@P2", DeviceType);
                //    comr.Parameters.AddWithValue("@P3", DataId1);
                //    comr.Parameters.AddWithValue("@P4", DataType);
                //    comr.Parameters.AddWithValue("@P5", DTCName1);


                //    comr.ExecuteNonQuery();

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            //fuse....14
            try
            {
                //string quarry = "select OBJECTID,Voltage from GIS_FUSE";
                string quarry = "select OBJECTID, DienAp,DanhSoThietBi from GIS_F01_PC03_THIETBIDONGCAT_TT where LoaiTBDC = '5'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                string OBJECTID = string.Empty;
                string DataId = "VOLTAGE";
                string DataId1 = "DTCName";
                //string DataType = string.Empty;
                string DataValue = string.Empty;
                string DTCName = string.Empty;
                string DTCName1 = string.Empty;
                string Voltage = string.Empty;
                int DeviceType = 14;
                int DataType = 8;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataValue = dt.Rows[i]["DienAp"].ToString();
                    OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                    DTCName = dt.Rows[i]["DanhSoThietBi"].ToString();
                    if (OBJECTID.Contains("SG"))
                    {
                        OBJECTID = OBJECTID.Replace("SG", "FU");
                    }
                    if (DTCName.Contains(","))
                    {
                        DTCName1 = DTCName.Replace(",", "_");
                    }
                    else
                    {
                        DTCName1 = DTCName;
                    }

                    // DataValue = updatevoltagecodevalue(DataValue);
                    //DataValue = (Voltage.ToUpper()).Replace(" KV", "");

                    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", OBJECTID);
                    comr.Parameters.AddWithValue("@P2", DeviceType);
                    comr.Parameters.AddWithValue("@P3", DataId1);
                    comr.Parameters.AddWithValue("@P4", DataType);
                    comr.Parameters.AddWithValue("@P5", DTCName1);

                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            //switch.....13
            try
            {
                string quarry = "select OBJECTID, DienAp,DanhSoThietBi from GIS_F01_PC03_THIETBIDONGCAT_TT where LoaiTBDC = '2'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                string OBJECTID = string.Empty;
                string DataId = "VOLTAGE";
                string DataId1 = "DTCName";
                //string DataType = string.Empty;
                string DataValue = string.Empty;
                string DTCName = string.Empty;
                string DTCName1 = string.Empty;
                string Voltage = string.Empty;
                int DeviceType = 13;
                int DataType = 8;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataValue = dt.Rows[i]["DienAp"].ToString();
                    DTCName = dt.Rows[i]["DanhSoThietBi"].ToString();

                    OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                    if (OBJECTID.Contains("SG"))
                    {
                        OBJECTID = OBJECTID.Replace("SG", "SW");
                    }
                    if (DTCName.Contains(","))
                    {
                        DTCName1 = DTCName.Replace(",", "_");
                    }
                    else
                    {
                        DTCName1 = DTCName;
                    }

                    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", OBJECTID);
                    comr.Parameters.AddWithValue("@P2", DeviceType);
                    comr.Parameters.AddWithValue("@P3", DataId1);
                    comr.Parameters.AddWithValue("@P4", DataType);
                    comr.Parameters.AddWithValue("@P5", DTCName1);

                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            //overhead...... 2
            //try
            //{
            //    string quarry = "select OBJECTID,DienAp,DanhSoThietBi from GIS_F09_PC03_DUONGDAY_TT";
            //    OleDbCommand com = new OleDbCommand(quarry, connn2);
            //    OlebdDataAdapter ad = new OlebdDataAdapter(com);
            //    OleDbDataReader dr = com.ExecuteReader();
            //    DataTable dt = new DataTable();
            //    dt.Load(dr);


            //    string OBJECTID = string.Empty;
            //    string DataId = "VOLTAGE";
            //    string DataId1 = "DTCName";
            //    string DataType = string.Empty;
            //    string DataValue = string.Empty;
            //    string DTCName = string.Empty;
            //    string DTCName1 = string.Empty;
            //    string Voltage = string.Empty;
            //    int DeviceType = 2;
            //    int DataType = 8;


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataValue = dt.Rows[i]["DienAp"].ToString();
            //        OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
            //        DTCName = dt.Rows[i]["DanhSoThietBi"].ToString();
            //        if (DTCName.Contains(","))
            //        {
            //            DTCName1 = DTCName.Replace(",", "_");
            //        }
            //        else
            //        {
            //            DTCName1 = DTCName;
            //        }

            //        string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

            //        OleDbCommand comr = new OleDbCommand(PP, connn2);
            //        comr.Parameters.AddWithValue("@P1", OBJECTID);
            //        comr.Parameters.AddWithValue("@P2", DeviceType);
            //        comr.Parameters.AddWithValue("@P3", DataId1);
            //        comr.Parameters.AddWithValue("@P4", DataType);
            //        comr.Parameters.AddWithValue("@P5", DTCName1);


            //        comr.ExecuteNonQuery();

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString() + Feederid);

            //}



            //Breacker...... 8
            try
            {
                //string quarry = "select OBJECTID,Voltage from GIS_Switchgear";
                string quarry = "select OBJECTID, DienAp, DanhSoThietBi from GIS_F01_PC03_THIETBIDONGCAT_TT where LoaiTBDC = '1' or LoaiTBDC = '3'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                string OBJECTID = string.Empty;
                string DataId = "VOLTAGE";
                string DataId1 = "DTCName";
                //string DataType = string.Empty;
                string DataValue = string.Empty;
                string DTCName1 = string.Empty;
                string DTCName = string.Empty;
                string Voltage = string.Empty;
                int DeviceType = 8;
                int DataType = 8;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataValue = dt.Rows[i]["DienAp"].ToString();
                    OBJECTID = dt.Rows[i]["OBJECTID"].ToString(); 
                    DTCName = dt.Rows[i]["DanhSoThietBi"].ToString();
                    if (DTCName.Contains(","))
                    {
                        DTCName1 = DTCName.Replace(",", "_");
                    }
                    else
                    {
                        DTCName1 = DTCName;
                    }

                    // DataValue = updatevoltagecodevalue(DataValue);
                    //DataValue = (Voltage.ToUpper()).Replace(" KV", "");

                    string PP = "INSERT INTO TGUDD_DEVICEUDD (DeviceNumber,DeviceType,DataId,DataType,DataValue) values (@P1,@P2,@P3,@P4,@P5)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", OBJECTID);
                    comr.Parameters.AddWithValue("@P2", DeviceType);
                    comr.Parameters.AddWithValue("@P3", DataId1);
                    comr.Parameters.AddWithValue("@P4", DataType);
                    comr.Parameters.AddWithValue("@P5", DTCName1);


                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }


        }
        public void GetTGDEVICE_FUSEData(string GETFILE, string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {

            try
            {
                string connectionString = @"Data Source=" + hh.Gisservername +
                          ";Initial Catalog=" + hh.GisDatabase +
                          ";User ID=" + hh.Gisusername +
                          ";Password=" + hh.Gispassword;

                string quarry = "SELECT sw.OBJECTID, sw.ID, sw.DienAp, sw.XuatTuyen,SW.Enabled, r.IDTBDC, r.IDinhMuc,  CASE WHEN r.IDinhMuc IS NULL THEN '400' ELSE r.IDinhMuc END AS NEW_IDinhMuc " +
                              "FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw " +
                               "left JOIN PC03_FCO AS r ON sw.ID = r.IDTBDC " +
                               "WHERE sw.LoaiTBDC = 5 AND sw.XuatTuyen = @Feederid " +
                               "ORDER BY r.IDinhMuc;";
               
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand com = new SqlCommand(quarry, conn))
                    {
                        com.Parameters.AddWithValue("@Feederid", Feederid);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            String SectionID = null;
                            String Location = "S";
                            String EqID = "3P";
                            String DeviceNumber = null;
                            String DeviceStage = "";
                            int Flags = 0;
                            int InitFromEquipFlags = 0;
                            string CoordX = null;
                            string CoordY = null;
                            String ClosedPhase = string.Empty;
                            int Locked = 0;
                            int RC = 0;
                            int NStatus = 0;
                            int TCCID = 0;
                            int PhPickup = 0;
                            int GrdPickup = 0;
                            int Alternate = 0;
                            int PhAltPickup = 0;
                            int GrdAltPickup = 0;
                            String FromNodeID = "";
                            int FaultIndicator = 1;
                            int Strategic = 0;
                            int RestorationMode = 0;
                            string ConnectionStatus = "0";
                            int ByPassOnRestoration = 1;
                            int GrdAltReversiblePickup = 0;
                            int Reversible = 1;
                            string Rating = "";
                            string Voltage = "";
                            string Type = "";

                            string rm_character = string.Empty;



                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Voltage = dt.Rows[i]["DienAp"].ToString();
                                rm_character = updatevoltage(GETFILE, Voltage);
                                Rating = dt.Rows[i]["NEW_IDinhMuc"].ToString();
                                //Voltage = updatevoltagecodevalue(Voltage);
                                //if (double.TryParse(Rating, out double currentValue))
                                //{
                                //    Rating = Math.Round(currentValue).ToString();
                                //}
                                //else
                                //{
                                //    Rating = Rating;
                                //}

                                if (Rating != "")
                                {
                                    Rating = Decimal.Round(Convert.ToDecimal(Rating)).ToString();
                                }


                                SectionID = dt.Rows[i]["OBJECTID"].ToString();
                                DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                                //ConnectionStatus = dt.Rows[i]["Enabled"].ToString();
                                ClosedPhase = dt.Rows[i]["Enabled"].ToString();
                                if (ClosedPhase=="1")
                                {
                                    ClosedPhase = "7";
                                }
                                else
                                {
                                    ClosedPhase = "0";
                                }


                                //ConnectionStatus = string.Empty;
                                EqID = rm_character + '_' + Rating + "A";
                                //DeviceStage = dt.Rows[i]["DeviceStage"].ToString();
                                //Length = dt.Rows[i]["ShapeLength"].ToString();
                                string PP = "INSERT INTO TGDEVICE_FUSE (SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,TCCID,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,FaultIndicator,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Reversible) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26)";
                                //if (connn2.State != ConnectionState.Open)
                                //{
                                //    connn2.Open();
                                //}                
                                OleDbCommand comr = new OleDbCommand(PP, connn2);
                                comr.Parameters.AddWithValue("@P1", "SG_"+SectionID);
                                comr.Parameters.AddWithValue("@P2", Location);
                                comr.Parameters.AddWithValue("@P3", EqID);
                                comr.Parameters.AddWithValue("@P4", "FU_" + DeviceNumber);
                                comr.Parameters.AddWithValue("@P5", DeviceStage);
                                comr.Parameters.AddWithValue("@P6", Flags);
                                comr.Parameters.AddWithValue("@P7", InitFromEquipFlags);
                                comr.Parameters.AddWithValue("@P8", Convert.ToInt32(CoordX).ToString());
                                comr.Parameters.AddWithValue("@P9", Convert.ToInt32(CoordY).ToString());
                                comr.Parameters.AddWithValue("@P10", ClosedPhase);
                                comr.Parameters.AddWithValue("@P11", Locked);
                                comr.Parameters.AddWithValue("@P12", RC);
                                comr.Parameters.AddWithValue("@P13", NStatus);
                                comr.Parameters.AddWithValue("@P14", TCCID);
                                comr.Parameters.AddWithValue("@P15", PhPickup);
                                comr.Parameters.AddWithValue("@P16", GrdPickup);
                                comr.Parameters.AddWithValue("@P17", Alternate);
                                comr.Parameters.AddWithValue("@P18", PhAltPickup);
                                comr.Parameters.AddWithValue("@P19", GrdAltPickup);
                                comr.Parameters.AddWithValue("@P20", FromNodeID);
                                comr.Parameters.AddWithValue("@P21", FaultIndicator);
                                comr.Parameters.AddWithValue("@P22", Strategic);
                                comr.Parameters.AddWithValue("@P23", RestorationMode);
                                comr.Parameters.AddWithValue("@P24", ConnectionStatus);
                                comr.Parameters.AddWithValue("@P25", ByPassOnRestoration);
                                comr.Parameters.AddWithValue("@P26", Reversible);

                                comr.ExecuteNonQuery();

                            }

                        }
                    }
                }






                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
              
            }
        
        }
        public void GetTGDEVICE_SHUNTCAPACITORData(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {

            try
            {
                string quarry = "select * from GIS_F03_PC03_TUBU_TT";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                String SectionID = null;
                String Location = "2";
                String DeviceNumber = null;
                String DeviceStage = "";
                string ConnectionStatus = "0";
                String EqID = "3P";
                int FixedKVARA = 0;
                int FixedKVARB = 0;
                string FixedKVARC = string.Empty;
                string FixedLossesA = string.Empty; ;
                String FixedLossesB = "";
                String Connection = "";
          
                int FixedLossesC = 0;
                int SwitchedKVARA = 0;
                int SwitchedKVARB = 0;
                //int TCCID = 0;
                int SwitchedKVARC = 0;
                int SwitchedLossesA = 0;
                int SwitchedLossesB = 0;
                int SwitchedLossesC = 0;
                int ByPhase = 0;
                String VoltageOverride = "";
                int VoltageOverrideOn = 1;
                int VoltageOverrideOff = 0;
                int VoltageOverrideDeadband = 0;
                string KV = string.Empty;
                int Control = 0;
                
                int OnValueA = 0;
                int OnValueB = 1;
                string OnValueC = "";
                string OffValueA = "";
                string OffValueB = string.Empty;
                string Type = string.Empty;
                //,,,,
                int OffValueC = 0;
                int SwitchingMode = 1;
                string InitiallyClosedPhase = "";
                string ControllingPhase = "";
                string SensorLocation = string.Empty;
                string ControlledNodeId = string.Empty;
                string PythonDeviceScriptID = string.Empty;
                string ShuntCapacitorID = string.Empty;
                string CTConnection = string.Empty;
                string InterruptingRating = string.Empty;
                string TongCongSuat = string.Empty;
                string PhaseType = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KV = dt.Rows[i]["DienAp"].ToString().Replace("KV","");
                    KV = (Convert.ToInt32(KV)/1.732).ToString();
                    KV = Math.Round(Convert.ToDouble(KV),2).ToString(); ;
                   // NormalRatedCurrent = string.Empty;
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    TongCongSuat = Math.Round(Convert.ToDecimal(dt.Rows[i]["TongCongSuat"])).ToString("0");


                    if (TongCongSuat=="600"|| TongCongSuat == "300")
                    {
                        PhaseType = "3P";
                    }
                    else
                    {
                        PhaseType = "P";
                    }
                    Type = string.Empty;
                    DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                    //ConnectionStatus = "0";

                    //3P_12.7KV_300KVAR
                    ShuntCapacitorID =  PhaseType +'_'+ KV +"KV"+ '_' +TongCongSuat+"KVAR";
                    //Length = dt.Rows[i]["ShapeLength"].ToString();
                    string PP = "INSERT INTO TGDEVICE_SHUNTCAPACITOR (SectionID,DeviceNumber,DeviceStage,Location,[Connection],FixedKVARA,FixedKVARB,FixedKVARC,FixedLossesA,FixedLossesB,FixedLossesC,SwitchedKVARA,SwitchedKVARB,SwitchedKVARC,SwitchedLossesA,SwitchedLossesB,SwitchedLossesC,ByPhase,VoltageOverride,VoltageOverrideOn,VoltageOverrideOff,VoltageOverrideDeadband,[KV],[Control],OnValueA,OnValueB,OnValueC,OffValueA,OffValueB,OffValueC,SwitchingMode,InitiallyClosedPhase,ControllingPhase,SensorLocation,ControlledNodeId,PythonDeviceScriptID,ShuntCapacitorID,[ConnectionStatus],CTConnection,InterruptingRating,EquipmentId) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36,@P37,@P38,@P39,@P40,@P41)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    dt.Load(dr);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", DeviceNumber);
                    comr.Parameters.AddWithValue("@P3", DeviceStage);
                    comr.Parameters.AddWithValue("@P4", Location);
                    comr.Parameters.AddWithValue("@P5", Connection);
                    comr.Parameters.AddWithValue("@P6", FixedKVARA);
                    comr.Parameters.AddWithValue("@P7", FixedKVARB);
                    comr.Parameters.AddWithValue("@P8", FixedKVARC);
                    comr.Parameters.AddWithValue("@P9", FixedLossesA);
                    comr.Parameters.AddWithValue("@P10", FixedLossesB);
                    comr.Parameters.AddWithValue("@P11", FixedLossesC);
                    comr.Parameters.AddWithValue("@P12", SwitchedKVARA);
                    comr.Parameters.AddWithValue("@P13", SwitchedKVARB);
                    comr.Parameters.AddWithValue("@P14", SwitchedKVARC);
                    comr.Parameters.AddWithValue("@P15", SwitchedLossesA);
                    comr.Parameters.AddWithValue("@P16", SwitchedLossesB);
                    comr.Parameters.AddWithValue("@P17", SwitchedLossesC);
                    comr.Parameters.AddWithValue("@P18", ByPhase);
                    comr.Parameters.AddWithValue("@P29", VoltageOverride);
                    comr.Parameters.AddWithValue("@P20", VoltageOverrideOn);
                    comr.Parameters.AddWithValue("@P21", VoltageOverrideOff);
                    comr.Parameters.AddWithValue("@P22", VoltageOverrideDeadband);
                    comr.Parameters.AddWithValue("@P23", KV);
                    comr.Parameters.AddWithValue("@P24", Control);
                    comr.Parameters.AddWithValue("@P25", OnValueA);
                    comr.Parameters.AddWithValue("@P26", OnValueB);
                    comr.Parameters.AddWithValue("@P27", OnValueC);
                    comr.Parameters.AddWithValue("@P28", OffValueA);
                    comr.Parameters.AddWithValue("@P29", OffValueB);
                    comr.Parameters.AddWithValue("@P30", OffValueC);
                    comr.Parameters.AddWithValue("@P31", SwitchingMode);
                    comr.Parameters.AddWithValue("@P32", InitiallyClosedPhase);
                    comr.Parameters.AddWithValue("@P33", ControllingPhase);
                    comr.Parameters.AddWithValue("@P34", SensorLocation);
                    comr.Parameters.AddWithValue("@P35", ControlledNodeId);
                    comr.Parameters.AddWithValue("@P36", PythonDeviceScriptID);
                    comr.Parameters.AddWithValue("@P37", ShuntCapacitorID);
                    comr.Parameters.AddWithValue("@P38", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P39", CTConnection);
                    comr.Parameters.AddWithValue("@P40", InterruptingRating);
                    comr.Parameters.AddWithValue("@P41", EqID);

                    comr.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

        }

        public void GetTGDEVICE_SWITCHData(string GETFILE, string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {

            try
            {
                string connectionString = @"Data Source=" + hh.Gisservername +
                           ";Initial Catalog=" + hh.GisDatabase +
                           ";User ID=" + hh.Gisusername +
                           ";Password=" + hh.Gispassword;

                string quarry = "SELECT sw.OBJECTID, sw.ID,sw.LoaiTBDC, sw.DienAp, sw.XuatTuyen, sw.Enabled, r.IDTBDC, r.IDinhMuc,  r.LoaiLBS,  CASE WHEN r.IDinhMuc IS NULL THEN '400' ELSE r.IDinhMuc END AS NEW_IDinhMuc " +
                               "FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw " +
                                "left JOIN PC03_LBS AS r ON sw.ID = r.IDTBDC " +
                                "WHERE sw.LoaiTBDC in ('1','2') AND sw.XuatTuyen = @Feederid " +
                                "ORDER BY r.IDinhMuc;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand com = new SqlCommand(quarry, conn))
                    {
                        com.Parameters.AddWithValue("@Feederid", Feederid);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            String SectionID = null;
                            String Location = "L";
                            String EqID = "3P";
                            String DeviceNumber = null;
                            String DeviceStage = "";
                            int Flags = 0;
                            int InitFromEquipFlags = 0;
                            string CoordX = null;
                            string CoordY = null;
                            String ClosedPhase = string.Empty;
                            int Locked = 0;
                            int RC = 0;
                            int NStatus = 0;
                            //int TCCID = 0;
                            int PhPickup = 0;
                            int GrdPickup = 0;
                            int Alternate = 0;
                            int PhAltPickup = 0;
                            int GrdAltPickup = 0;
                            String FromNodeID = "";
                            int FaultIndicator = 1;
                            int Automated = 0;
                            int SensorMode = 0;
                            int Strategic = 0;
                            int RestorationMode = 0;
                            string ConnectionStatus = "0";
                            int ByPassOnRestoration = 0;
                            int Reversible = 1;
                            string NormalRatedCurrent = "";
                            string Voltage = "";
                            string rm_character = string.Empty;
                            string Type = string.Empty;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Voltage = dt.Rows[i]["DienAp"].ToString();
                                Voltage = updatevoltage(GETFILE, Voltage);
                                NormalRatedCurrent = dt.Rows[i]["NEW_IDinhMuc"].ToString();
                                if (double.TryParse(NormalRatedCurrent, out double currentValue))
                                {
                                    NormalRatedCurrent = Math.Round(currentValue).ToString();
                                }
                                else
                                {
                                    NormalRatedCurrent = NormalRatedCurrent;
                                }
                                SectionID = dt.Rows[i]["OBJECTID"].ToString();
                                //ConnectionStatus = dt.Rows[i]["Enabled"].ToString();
                                ClosedPhase =  dt.Rows[i]["Enabled"].ToString();
                                if (ClosedPhase == "1")
                                {
                                    ClosedPhase = "7";
                                }
                                else
                                {
                                    ClosedPhase = "0";
                                }

                                Type = dt.Rows[i]["LoaiLBS"].ToString();
                                //if (SectionID == "1725")
                                //{
                                //    NStatus = 1;
                                //}
                                DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                                //ConnectionStatus = string.Empty;
                                EqID = Voltage + '_' + NormalRatedCurrent + 'A';
                                //Length = dt.Rows[i]["ShapeLength"].ToString();
                                string PP = "INSERT INTO TGDEVICE_SWITCH (SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,FaultIndicator,Automated,SensorMode,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Reversible) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27)";

                                OleDbCommand comr = new OleDbCommand(PP, connn2);
                                dt.Load(dr);
                                comr.Parameters.AddWithValue("@P1", "SG_"+SectionID);
                                comr.Parameters.AddWithValue("@P2", Location);
                                comr.Parameters.AddWithValue("@P3", EqID);
                                comr.Parameters.AddWithValue("@P4", "SW_" +DeviceNumber);
                                comr.Parameters.AddWithValue("@P5", DeviceStage);
                                comr.Parameters.AddWithValue("@P6", Flags);
                                comr.Parameters.AddWithValue("@P7", InitFromEquipFlags);
                                comr.Parameters.AddWithValue("@P8", Convert.ToInt32(CoordX).ToString());
                                comr.Parameters.AddWithValue("@P9", Convert.ToInt32(CoordY).ToString());
                                comr.Parameters.AddWithValue("@P10", ClosedPhase);
                                comr.Parameters.AddWithValue("@P11", Locked);
                                comr.Parameters.AddWithValue("@P12", RC);
                                comr.Parameters.AddWithValue("@P13", NStatus);
                                comr.Parameters.AddWithValue("@P14", PhPickup);
                                comr.Parameters.AddWithValue("@P15", GrdPickup);
                                comr.Parameters.AddWithValue("@P16", Alternate);
                                comr.Parameters.AddWithValue("@P17", PhAltPickup);
                                comr.Parameters.AddWithValue("@P18", GrdAltPickup);
                                comr.Parameters.AddWithValue("@P29", FromNodeID);
                                comr.Parameters.AddWithValue("@P20", FaultIndicator);
                                comr.Parameters.AddWithValue("@P21", Automated);
                                comr.Parameters.AddWithValue("@P22", SensorMode);
                                comr.Parameters.AddWithValue("@P23", Strategic);
                                comr.Parameters.AddWithValue("@P24", RestorationMode);
                                comr.Parameters.AddWithValue("@P25", ConnectionStatus);
                                comr.Parameters.AddWithValue("@P26", ByPassOnRestoration);
                                comr.Parameters.AddWithValue("@P27", Reversible);
                                comr.ExecuteNonQuery();
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }




             

        }

        public void GetTGDEVICE_RECLOSER(string GETFILE ,string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {

            try
            {


                string connectionString = @"Data Source=" + hh.Gisservername +
                           ";Initial Catalog=" + hh.GisDatabase +
                           ";User ID=" + hh.Gisusername +
                           ";Password=" + hh.Gispassword;

                string quarry = "SELECT sw.OBJECTID, sw.ID, sw.DienAp, sw.XuatTuyen,sw.HangSanXuat, sw.Enabled, r.IDTBDC, r.IDinhMuc, CASE WHEN r.IDinhMuc IS NULL THEN '400' ELSE r.IDinhMuc END AS NEW_IDinhMuc FROM [F01_PC03_THIETBIDONGCAT_TT] AS sw INNER JOIN PC03_RECLOSER AS r ON sw.ID = r.IDTBDC WHERE sw.LoaiTBDC = 4 AND sw.XuatTuyen = @feederId ORDER BY r.IDinhMuc;";


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand com = new SqlCommand(quarry, conn))
                    {
                        com.Parameters.AddWithValue("@Feederid", Feederid);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {

                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            string SectionID = string.Empty;
                            string Location = string.Empty;
                            string Voltage = string.Empty;
                            string Raeting = string.Empty;
                            string EqID = string.Empty;
                            string DeviceNumber = string.Empty;
                            string DeviceStage = string.Empty;
                            string Flags = "0";
                            string InitFromEquipFlags = "0";
                            string CoordX = string.Empty;
                            string CoordY = string.Empty;
                            string ClosedPhase = string.Empty;
                            string Locked = "0";
                            string RC = "0";
                            string NStatus = "0";
                            string TCCID = string.Empty;
                            string PhPickup = "0";
                            string GrdPickup = "0";
                            string Alternate = "0";
                            string PhAltPickup = "0";
                            string GrdAltPickup = "0";
                            string FromNodeID = string.Empty;
                            string EnableReclosing = "1";
                            string FaultIndicator = "1";
                            string EnableFuseSaving = "0";
                            string MinRatedCurrentForFuseSaving = "0";
                            string Automated = "0";
                            string SensorMode = "0";
                            string Strategic = "0";
                            string RestorationMode = "0";
                            string ConnectionStatus = "0";
                            string TCCRepositoryID = string.Empty;
                            string HangSanXuat = string.Empty;
                            string TCCRepositoryAlternateID1 = string.Empty;
                            string TCCRepositoryAlternateID2 = string.Empty;
                            string TCCRepositoryAlternateID3 = string.Empty;
                            string TCCRepositoryAlternateID4 = string.Empty;
                            string TCCRepositoryAlternateID5 = string.Empty;
                            string TCCRepositoryAlternateID6 = string.Empty;
                            string TCCRepositoryAlternateID7 = string.Empty;
                            string TCCRepositoryAlternateID8 = string.Empty;
                            string TCCRepositoryAlternateID9 = string.Empty;
                            string TCCRepositoryAlternateID10 = string.Empty;
                            string IntellirupterTCCRepositoryID = string.Empty;
                            string ByPassOnRestoration = "0";
                            string Reversible = "1";
                            string TCCSettingsSelection = "0";



                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                SectionID = dt.Rows[i]["OBJECTID"].ToString();
                                DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                                DeviceNumber = DeviceNumber.Replace("SG_", "");

                                Voltage = dt.Rows[i]["DienAp"].ToString();
                                Voltage = updatevoltage(GETFILE, Voltage);
                                Raeting = dt.Rows[i]["NEW_IDinhMuc"].ToString();
                                ClosedPhase = dt.Rows[i]["Enabled"].ToString();
                                if (ClosedPhase == "1")
                                {
                                    ClosedPhase = "7";
                                }
                                else
                                {
                                    ClosedPhase = "0";
                                }
                                HangSanXuat = dt.Rows[i]["HangSanXuat"].ToString();
                                if (double.TryParse(Raeting, out double currentValue))
                                {
                                    Raeting = Math.Round(currentValue).ToString();
                                }
                                else
                                {
                                    Raeting = Raeting;
                                }

                                if (HangSanXuat=="1")
                                {
                                    EqID = "GENERALELECTRIC GEC";
                                }
                                else
                                {
                                    EqID = Voltage + "_" + Raeting + "A";
                                }

                                
                                //Length = dt.Rows[i]["ShapeLength"].ToString();
                                string PP = "INSERT INTO TGDEVICE_RECLOSER (SectionID, Location, EqID, DeviceNumber, DeviceStage, Flags, InitFromEquipFlags, CoordX, CoordY, ClosedPhase, Locked, RC, NStatus, TCCID, PhPickup, GrdPickup, Alternate, PhAltPickup, GrdAltPickup, FromNodeID, EnableReclosing, FaultIndicator, EnableFuseSaving, MinRatedCurrentForFuseSaving, Automated, SensorMode, Strategic, RestorationMode, ConnectionStatus, TCCRepositoryID, TCCRepositoryAlternateID1, TCCRepositoryAlternateID2, TCCRepositoryAlternateID3, TCCRepositoryAlternateID4, TCCRepositoryAlternateID5, TCCRepositoryAlternateID6, TCCRepositoryAlternateID7, TCCRepositoryAlternateID8, TCCRepositoryAlternateID9, TCCRepositoryAlternateID10, IntellirupterTCCRepositoryID, ByPassOnRestoration, Reversible, TCCSettingsSelection) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, @p29, @p30, @p31, @p32, @p33, @p34, @p35, @p36, @p37, @p38, @p39, @p40, @p41, @p42, @p43, @p44);";

                                OleDbCommand com1 = new OleDbCommand(PP, connn2);
                                //dt.Load(dr);
                                com1.Parameters.AddWithValue("@p1", "SG_"+ SectionID);
                                com1.Parameters.AddWithValue("@p2", Location);
                                com1.Parameters.AddWithValue("@p3", EqID);
                                com1.Parameters.AddWithValue("@p4", "RC_" + DeviceNumber);
                                com1.Parameters.AddWithValue("@p5", DeviceStage);
                                com1.Parameters.AddWithValue("@p6", Flags);
                                com1.Parameters.AddWithValue("@p7", InitFromEquipFlags);
                                com1.Parameters.AddWithValue("@p8", CoordX);
                                com1.Parameters.AddWithValue("@p9", CoordY);
                                com1.Parameters.AddWithValue("@p10", ClosedPhase);
                                com1.Parameters.AddWithValue("@p11", Locked);
                                com1.Parameters.AddWithValue("@p12", RC);
                                com1.Parameters.AddWithValue("@p13", NStatus);
                                com1.Parameters.AddWithValue("@p14", TCCID);
                                com1.Parameters.AddWithValue("@p15", PhPickup);
                                com1.Parameters.AddWithValue("@p16", GrdPickup);
                                com1.Parameters.AddWithValue("@p17", Alternate);
                                com1.Parameters.AddWithValue("@p18", PhAltPickup);
                                com1.Parameters.AddWithValue("@p19", GrdAltPickup);
                                com1.Parameters.AddWithValue("@p20", FromNodeID);
                                com1.Parameters.AddWithValue("@p21", EnableReclosing);
                                com1.Parameters.AddWithValue("@p22", FaultIndicator);
                                com1.Parameters.AddWithValue("@p23", EnableFuseSaving);
                                com1.Parameters.AddWithValue("@p24", MinRatedCurrentForFuseSaving);
                                com1.Parameters.AddWithValue("@p25", Automated);
                                com1.Parameters.AddWithValue("@p26", SensorMode);
                                com1.Parameters.AddWithValue("@p27", Strategic);
                                com1.Parameters.AddWithValue("@p28", RestorationMode);
                                com1.Parameters.AddWithValue("@p29", ConnectionStatus);
                                com1.Parameters.AddWithValue("@p30", TCCRepositoryID);
                                com1.Parameters.AddWithValue("@p31", TCCRepositoryAlternateID1);
                                com1.Parameters.AddWithValue("@p32", TCCRepositoryAlternateID2);
                                com1.Parameters.AddWithValue("@p33", TCCRepositoryAlternateID3);
                                com1.Parameters.AddWithValue("@p34", TCCRepositoryAlternateID4);
                                com1.Parameters.AddWithValue("@p35", TCCRepositoryAlternateID5);
                                com1.Parameters.AddWithValue("@p36", TCCRepositoryAlternateID6);
                                com1.Parameters.AddWithValue("@p37", TCCRepositoryAlternateID7);
                                com1.Parameters.AddWithValue("@p38", TCCRepositoryAlternateID8);
                                com1.Parameters.AddWithValue("@p39", TCCRepositoryAlternateID9);
                                com1.Parameters.AddWithValue("@p40", TCCRepositoryAlternateID10);
                                com1.Parameters.AddWithValue("@p41", IntellirupterTCCRepositoryID);
                                com1.Parameters.AddWithValue("@p42", ByPassOnRestoration);
                                com1.Parameters.AddWithValue("@p43", Reversible);
                                com1.Parameters.AddWithValue("@p44", TCCSettingsSelection);




                                com1.ExecuteNonQuery();
                            }


                        }
                    }
                }
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

        }

        public void GetTGDEVICE_DISTRIBUTIONTRANSFORMERData(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {

            try
            {
                //string mdbpath = mdbpathh + ConfigurationManager.AppSettings["Connection"];
                //string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                //OleDbConnection obj = new OleDbConnection(connectionstring12);
            
                OleDbCommand cmd = new OleDbCommand("SELECT * from GIS_F04_PC03_MAYBIENAP_TT", connn2);
                // obj.Open();
                if (connn2.State != ConnectionState.Open)
                {
                    connn2.Open();
                }
                // string quarry = "select*from GIS_FUSE"; ////
                // OleDbCommand com = new OleDbCommand();
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable DTt1 = new DataTable();
                DTt1.Load(dr);

                String Location = "L";
                string CoolingType = "ONAN";
                String SectionID =null;
                String EqID = "";
                String DeviceNumber = null;
                string DeviceStage = "";
                String Flags = "0";
                String CoordX = "";
                String CoordY = "";
                String Conn = "Dyn11";
                String PrimTap = "100";
                String SecondaryTap = "100";
                String RgPrim = "0";
                String XgPrim = "0";
                String RgSec = "0";
                String XgSec = "0";
                String ODPrimPh = "NONE";
                String PrimaryBaseVoltage = "";
                String SecondaryBaseVoltage = "";
                String FromNodeID = "";
                String SettingOption = "";
                String SetPoint = "";
                String ControlType = "";
                String LowerBandWidth = "";
                String UpperBandWidth = "";
                String TapLocation = "";
                String InitialTapPosition = "";
                String InitialTapPositionMode = "";
                String Tap = "";
                String MaxBuck = "";
                String MaxBoost = "";
                String CT = "";
                String PT = "";
                String Rset = "";
                String Xset = "";
                String FirstHouseHigh = "";
                String FirstHouseLow = "";
                String PhaseON = "";
                String AtSectionID = "";
                String MasterID = "";
                String FaultIndicator = "0";
                String PhaseShiftType = "";
                String DTCCode = "";
                String DTCName = "";
                String GammaPhaseShift = "0";
                String CTPhase = "1";
                String PrimaryCornerGroundedPhase = "NONE";
                String SecondaryCornerGroundedPhase = "NONE";
                String ConnectionStatus = "0";
                String Reversible = "1";
                String CapacityInKVA = "";
                String NoLoad = "";
                String Empidence = "";
                string EmpidenceResult = string.Empty;
                string NoLoadResult = string.Empty;


                string rm_character = string.Empty;
                for (int i = 0; i < DTt1.Rows.Count; i++)
                {
                    CoolingType = string.Empty;
                    DTCCode = string.Empty;
                    DTCName = string.Empty;
             

                    DeviceNumber = DTt1.Rows[i]["OBJECTID"].ToString();
                    SectionID = DTt1.Rows[i]["OBJECTID"].ToString();

                    if (SectionID == "DT_3408")
                    {

                    }
                    if (SectionID == "DT_4110")
                    {

                    }
                    Empidence = DTt1.Rows[i]["UNganMach"].ToString();
                    if (!string.IsNullOrWhiteSpace(Empidence))
                    {
                        float empValue = float.Parse(Empidence);
                        float roundedValue = (float)Math.Round(empValue, 1);
                        EmpidenceResult = roundedValue.ToString("0.0");

                    }

                    

                    NoLoad = DTt1.Rows[i]["TonHaoKhongTai"].ToString();
                    if (!string.IsNullOrWhiteSpace(NoLoad))
                    {
                        float NoLoadFloat = float.Parse(NoLoad);
                        float NoLoadDivided = NoLoadFloat / 1000;
                        NoLoadResult = NoLoadDivided.ToString("0.00");

                    }



                    PhaseShiftType = string.Empty;
                    
                  
                    //if(PhaseShiftType=="11")
                    //{
                    //    Conn = "1";
                    //}
                    PrimaryBaseVoltage = DTt1.Rows[i]["CapDienApVao"].ToString();
                    if (PrimaryBaseVoltage.Contains("KV"))
                    {
                        PrimaryBaseVoltage = PrimaryBaseVoltage.Replace("KV","");
                    }
                    SecondaryBaseVoltage = DTt1.Rows[i]["CapDienApRa"].ToString();
                    if (SecondaryBaseVoltage == "2" || SecondaryBaseVoltage == "1")
                    {
                        SecondaryBaseVoltage = ".4KV";
                    }
                    //if(vlolength=="4")
                    //{
                    //    SecondaryBaseVoltage = SecondaryBaseVoltage + "3";
                    //}
                    CapacityInKVA =DTt1.Rows[i]["CongSuat"].ToString();
                    if (!string.IsNullOrWhiteSpace(CapacityInKVA))
                    {
                        double parsedValue = Convert.ToDouble(CapacityInKVA);
                        CapacityInKVA = Math.Round(parsedValue).ToString();
                    }

                    //CapacityInKVA = (CapacityInKVA.ToUpper()).Replace(" KVA", "");
                    EqID = PrimaryBaseVoltage + '/' + SecondaryBaseVoltage+ "_" + CapacityInKVA+"KVA_"+ EmpidenceResult + "Z_"+ NoLoadResult + "KW";

                    string PP = "INSERT INTO TGDEVICE_DISTRIBUTIONTRANSFORMER (SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,CoordX,CoordY,Conn,PrimTap,SecondaryTap,RgPrim,XgPrim,RgSec,XgSec,ODPrimPh,PrimaryBaseVoltage,SecondaryBaseVoltage,FromNodeID,SettingOption,SetPoint,ControlType,LowerBandWidth,UpperBandWidth,TapLocation,InitialTapPosition,InitialTapPositionMode,Tap,MaxBuck,MaxBoost,CT,PT,Rset,Xset,FirstHouseHigh,FirstHouseLow,PhaseON,AtSectionID,MasterID,FaultIndicator,PhaseShiftType,GammaPhaseShift,CTPhase,PrimaryCornerGroundedPhase,SecondaryCornerGroundedPhase,ConnectionStatus,Reversible) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36,@P37,@P38,@P39,@P40,@P41,@P42,@P43,@P44,@P45,@P46,@P47)";
                    //if (obj.State != ConnectionState.Open)
                    //{
                    //    obj.Open();
                    //}

                    if(SectionID== "DT_257963")
                    {

                    }
                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", Location);
                    comr.Parameters.AddWithValue("@P3", EqID);
                    comr.Parameters.AddWithValue("@P4", DeviceNumber);
                    comr.Parameters.AddWithValue("@P5", DeviceStage);
                    comr.Parameters.AddWithValue("@P6", Flags);
                    comr.Parameters.AddWithValue("@P7", CoordX);
                    comr.Parameters.AddWithValue("@P8", CoordY);
                    comr.Parameters.AddWithValue("@P9", Conn);
                    comr.Parameters.AddWithValue("@P10", PrimTap);
                    comr.Parameters.AddWithValue("@P11", SecondaryTap);
                    comr.Parameters.AddWithValue("@P12", RgPrim);
                    comr.Parameters.AddWithValue("@P13", XgPrim);
                    comr.Parameters.AddWithValue("@P14", RgSec);
                    comr.Parameters.AddWithValue("@P15", XgSec);
                    comr.Parameters.AddWithValue("@P16", ODPrimPh);
                    comr.Parameters.AddWithValue("@P17", PrimaryBaseVoltage);
                    comr.Parameters.AddWithValue("@P18", SecondaryBaseVoltage);
                    comr.Parameters.AddWithValue("@P19", FromNodeID);
                    comr.Parameters.AddWithValue("@P20", SettingOption);
                    comr.Parameters.AddWithValue("@P21", SetPoint);
                    comr.Parameters.AddWithValue("@P22", ControlType);
                    comr.Parameters.AddWithValue("@P23", LowerBandWidth);
                    comr.Parameters.AddWithValue("@P24", UpperBandWidth);
                    comr.Parameters.AddWithValue("@P25", TapLocation);
                    comr.Parameters.AddWithValue("@P26", InitialTapPosition);
                    comr.Parameters.AddWithValue("@P27", InitialTapPositionMode);
                    comr.Parameters.AddWithValue("@P28", Tap);
                    comr.Parameters.AddWithValue("@P29", MaxBuck);
                    comr.Parameters.AddWithValue("@P30", MaxBoost);
                    comr.Parameters.AddWithValue("@P31", CT);
                    comr.Parameters.AddWithValue("@P32", PT);
                    comr.Parameters.AddWithValue("@P33", Rset);
                    comr.Parameters.AddWithValue("@P34", Xset);
                    comr.Parameters.AddWithValue("@P35", FirstHouseHigh);
                    comr.Parameters.AddWithValue("@P36", FirstHouseLow);
                    comr.Parameters.AddWithValue("@P37", PhaseON);
                    comr.Parameters.AddWithValue("@P38", AtSectionID);
                    comr.Parameters.AddWithValue("@P39", MasterID);
                    comr.Parameters.AddWithValue("@P40", FaultIndicator);
                    comr.Parameters.AddWithValue("@P41", PhaseShiftType);
                    comr.Parameters.AddWithValue("@P42", GammaPhaseShift);
                    comr.Parameters.AddWithValue("@P43", CTPhase);
                    comr.Parameters.AddWithValue("@P44", PrimaryCornerGroundedPhase);
                    comr.Parameters.AddWithValue("@P45", SecondaryCornerGroundedPhase);
                    comr.Parameters.AddWithValue("@P46", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P47", Reversible);
                    comr.ExecuteNonQuery();
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
         
             
            }

        }

        public string updatevoltagecodevalue(string voltage)
        {
            #region
            //try
            //{
            //    // Ensure the connection is open
            //    if (connn2.State != ConnectionState.Open)
            //    {
            //        connn2.Open();
            //    }

            //    // Use parameterized query to avoid SQL injection
            //    string query = "SELECT Value1 FROM GIS_DOMAIN WHERE Code = @voltage";
            //    using (OleDbCommand com = new OleDbCommand(query, connn2))
            //    {
            //        // Add voltage as a parameter
            //        com.Parameters.AddWithValue("@voltage", voltage);

            //        // Execute the command and use a DataReader to read the result
            //        using (OleDbDataReader dr = com.ExecuteReader())
            //        {
            //            // If a matching row is found
            //            if (dr.Read())
            //            {
            //                // Retrieve the "Value1" column (or any column you need)
            //                string value1 = dr["Value1"].ToString();
            //                value1 = voltage;
            //                return voltage;  // Return the column value
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log or handle the exception (for now, we just output it to console)
            //    Console.WriteLine("An error occurred: " + ex.Message);
            //    return null;
            //}
            #endregion
            #region
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
                voltage = "0.433 KV";
            }
            if (voltage == "340")
            {
                voltage = "11 KV";
            }
            if (voltage == "11")
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
            if (voltage == "430")
            {
                voltage = "0.433 KV";
            }
            if (voltage == "240")
            {
                voltage = "0.240 KV";
            }
            if (voltage == "11 kV")
            {
                voltage = "11 KV";
            }
            #endregion
            return voltage;

        }
        public string updateCapacityIncodevalue(string CapacityInKVA)
        {

            if (CapacityInKVA == "1")
            {
                CapacityInKVA = "16";
            }
            if (CapacityInKVA == "2")
            {
                CapacityInKVA = "25";
            }
            if (CapacityInKVA == "3")
            {
                CapacityInKVA = "50";
            }
            if (CapacityInKVA == "4")
            {
                CapacityInKVA = "63";
            }
            if (CapacityInKVA == "5")
            {
                CapacityInKVA = "75";
            }
            if (CapacityInKVA == "6")
            {
                CapacityInKVA = "100";
            }
            if (CapacityInKVA == "7")
            {
                CapacityInKVA = "150";
            }
            if (CapacityInKVA == "8")
            {
                CapacityInKVA = "200";
            }
            if (CapacityInKVA == "9")
            {
                CapacityInKVA = "250";
            }
            if (CapacityInKVA == "10")
            {
                CapacityInKVA = "300";
            }
            if (CapacityInKVA == "11")
            {
                CapacityInKVA = "100";
            }
            if (CapacityInKVA == "12")
            {
                CapacityInKVA = "315";
            }
            if (CapacityInKVA == "13")
            {
                CapacityInKVA = "400";
            }
            if (CapacityInKVA == "14")
            {
                CapacityInKVA = "500";
            }
            if (CapacityInKVA == "15")
            {
                CapacityInKVA = "630";
            }
            if (CapacityInKVA == "16")
            {
                CapacityInKVA = "750";
            }
            if (CapacityInKVA == "17")
            {
                CapacityInKVA = "1000";
            }
            if (CapacityInKVA == "18")
            {
                CapacityInKVA = "1200";
            }
            if (CapacityInKVA == "19")
            {
                CapacityInKVA = "1500";
            }
            if (CapacityInKVA == "20")
            {
                CapacityInKVA = "2000";
            }
            if (CapacityInKVA == "21")
            {
                CapacityInKVA = "160";
            }
            if (CapacityInKVA == "22")
            {
                CapacityInKVA = "30";
            }
            if (CapacityInKVA == "23")
            {
                CapacityInKVA = "950";
            }
            if (CapacityInKVA == "24")
            {
                CapacityInKVA = "125";
            }
            if (CapacityInKVA == "25")
            {
                CapacityInKVA = "1250";
            }
            if (CapacityInKVA == "26")
            {
                CapacityInKVA = "415";
            }
            if (CapacityInKVA == "27")
            {
                CapacityInKVA = "900";
            }
            if (CapacityInKVA == "28")
            {
                CapacityInKVA = "1600";
            }
            if (CapacityInKVA == "29")
            {
                CapacityInKVA = "10";
            }
            if (CapacityInKVA == "30")
            {
                CapacityInKVA = "220";
            }
            if (CapacityInKVA == "31")
            {
                CapacityInKVA = "990";
            }
            if (CapacityInKVA == "32")
            {
                CapacityInKVA = "995";
            }
           

            return CapacityInKVA;

        }
        public void GetTGDEVICE_CIRCUITBREAKERData(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
             
            try
            {

                string quarry = "select * from GIS_F01_PC03_THIETBIDONGCAT_TT where LoaiTBDC = '3'";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                //OlebdDataAdapter ad = new OlebdDataAdapter(com);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                String SectionID = null;
                String Location = "S";
                String EqID = "3P";
                String DeviceNumber = null;
                String DeviceStage = "";
                int Flags = 0;
                int InitFromEquipFlags = 0;
                string CoordX = null;
                string CoordY = null;
                String ClosedPhase = "";
                int Locked = 0;
                int RC = 0;
                int NStatus = 0;
                string TCCID = "DEFAULT";
                int PhPickup = 0;
                int GrdPickup = 0;
                int Alternate = 0;
                int PhAltPickup = 0;
                int GrdAltPickup = 0;
                String FromNodeID = "";
                int FaultIndicator = 1;
                int EnableFuseSaving = 1;
                String MinRatedCurrentForFuseSaving = "0";
                int Automated = 0;
                int SensorMode = 0;
                int Strategic = 0;
                int RestorationMode = 0;
                string ConnectionStatus = "0";
                int ByPassOnRestoration = 0;
                int Speed = 0;
                int SeqOpFirstPhase = 2;
                int SeqOpFirstGround = 2;
                int SeqOpLockoutPhase = 4;
                int SeqOpLockoutGround = 4;
                int SeqResetTime = 30;
                int SeqReclosingTime1 = 2;
                int SeqReclosingTime2 = 2;
                int SeqReclosingTime3 = 2;
                int Reversible = 1;
                int EnableReclosing = 1;
                string Voltage = "";
                string InsulationType = "";
                string RatedCurrent = "";
                string Rating = "";
                string SymmetricalBreakingCurrent = "";
                string rm_character = string.Empty;
                string MAKE = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Voltage = dt.Rows[i]["DienAp"].ToString();
                    if (Voltage == "2")
                    {
                        Voltage = "22KV";
                    }
                    // Voltage + 'KV' + InsulationType + '-' + Rating + 'A- + SymmetricalBreakingCurrent + 'KA'
                    InsulationType = string.Empty;
                    RatedCurrent = string.Empty;
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    string OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                     ClosedPhase = dt.Rows[i]["Enabled"].ToString();
                    if (ClosedPhase == "1")
                    {
                        ClosedPhase = "7";
                    }
                    else
                    {
                        ClosedPhase = "0";
                    }
                    DeviceNumber =  OBJECTID;
                    Rating = string.Empty;
                    MAKE = string.Empty;
                    SymmetricalBreakingCurrent = string.Empty;
                    //ConnectionStatus = "";
                    EqID = Voltage + "_1250A";
                    //EqID = InsulationType + "P_" + Voltage + "KV_" + Rating + "A_" + SymmetricalBreakingCurrent + "KA_" + MAKE;
                    //Length = dt.Rows[i]["ShapeLength"].ToString();
                    string PP = "INSERT INTO TGDEVICE_CIRCUITBREAKER (SectionID,Location,EqID,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,CoordX,CoordY,ClosedPhase,Locked,RC,NStatus,TCCID,PhPickup,GrdPickup,Alternate,PhAltPickup,GrdAltPickup,FromNodeID,EnableReclosing,FaultIndicator,EnableFuseSaving,MinRatedCurrentForFuseSaving,Automated,SensorMode,Strategic,RestorationMode,ConnectionStatus,ByPassOnRestoration,Speed,SeqOpFirstPhase,SeqOpFirstGround,SeqOpLockoutPhase,SeqOpLockoutGround,SeqResetTime,SeqReclosingTime1,SeqReclosingTime2,SeqReclosingTime3,Reversible) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36,@P37,@P38,@P39,@P40)";
                    //if (connn2.State != ConnectionState.Open)
                    //{
                    //    connn2.Open();
                    //}
                    OleDbCommand comr = new OleDbCommand(PP, connn2); dt.Load(dr);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", Location);
                    comr.Parameters.AddWithValue("@P3", EqID);
                    comr.Parameters.AddWithValue("@P4", DeviceNumber);
                    comr.Parameters.AddWithValue("@P5", DeviceStage);
                    comr.Parameters.AddWithValue("@P6", Flags);
                    comr.Parameters.AddWithValue("@P7", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P8", Convert.ToInt32(CoordX).ToString());
                    comr.Parameters.AddWithValue("@P9", Convert.ToInt32(CoordY).ToString());
                    comr.Parameters.AddWithValue("@P10", ClosedPhase);
                    comr.Parameters.AddWithValue("@P11", Locked);
                    comr.Parameters.AddWithValue("@P12", RC);
                    comr.Parameters.AddWithValue("@P13", NStatus);
                    comr.Parameters.AddWithValue("@P14", TCCID);
                    comr.Parameters.AddWithValue("@P15", PhPickup);
                    comr.Parameters.AddWithValue("@P16", GrdPickup);
                    comr.Parameters.AddWithValue("@P17", Alternate);
                    comr.Parameters.AddWithValue("@P18", PhAltPickup);
                    comr.Parameters.AddWithValue("@P19", GrdAltPickup);
                    comr.Parameters.AddWithValue("@P20", FromNodeID);
                    comr.Parameters.AddWithValue("@P21", EnableReclosing);
                    comr.Parameters.AddWithValue("@P22", FaultIndicator);
                    comr.Parameters.AddWithValue("@P23", EnableFuseSaving);
                    comr.Parameters.AddWithValue("@P24", MinRatedCurrentForFuseSaving);
                    comr.Parameters.AddWithValue("@P25", Automated);
                    comr.Parameters.AddWithValue("@P26", SensorMode);
                    comr.Parameters.AddWithValue("@P27", Strategic);
                    comr.Parameters.AddWithValue("@P28", RestorationMode);
                    comr.Parameters.AddWithValue("@P29", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P30", ByPassOnRestoration);
                    comr.Parameters.AddWithValue("@P31", Speed);
                    comr.Parameters.AddWithValue("@P32", SeqOpFirstPhase);
                    comr.Parameters.AddWithValue("@P33", SeqOpFirstGround);
                    comr.Parameters.AddWithValue("@P34", SeqOpLockoutPhase);
                    comr.Parameters.AddWithValue("@P35", SeqOpLockoutGround);
                    comr.Parameters.AddWithValue("@P36", SeqResetTime);
                    comr.Parameters.AddWithValue("@P37", SeqReclosingTime1);
                    comr.Parameters.AddWithValue("@P38", SeqReclosingTime2);
                    comr.Parameters.AddWithValue("@P39", SeqReclosingTime3);
                    comr.Parameters.AddWithValue("@P40", Reversible);
                    comr.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);

            }

            //update.....narmal status.....


        }
        public void GetTGDEVICE_SOLAR(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            if (connn2.State == ConnectionState.Closed)
            {
                connn2.Open();
            }

            try
            {

                string quarry = "select * from GIS_SOLAR";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                string SectionID = string.Empty;
                string Location = "M";
                string DeviceNumber = "0";
                string DeviceStage = string.Empty;
                string Flags = "0";
                string InitFromEquipFlags = "0";
                string EquipmentID = "PVC";
                string NumberOfGenerators = string.Empty;
                string SymbolSize = "0";
                string NS = string.Empty;
                string NP = string.Empty;
                string AmbientTemperature = string.Empty;
                string FaultContributionBasedOnRatedPower = string.Empty;
                string FaultContributionUnit = "0";
                string FaultContribution = "100";
                string ConstantInsolation = "0";
                string ForceT0 = string.Empty;
                string InsolationModelID = string.Empty;
                string FrequencySourceID = string.Empty;
                string SourceHarmonicModelType = string.Empty;
                string PulseNumber = string.Empty;
                string FiringAngle = string.Empty;
                string OverlapAngle = string.Empty;
                string ConnectionStatus = string.Empty;
                string ConnectionConfiguration = string.Empty;
                string CTConnection = "3";
                string Phase = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // ,PulseNumber ,FiringAngle ,OverlapAngle ,ConnectionStatus ,ConnectionConfiguration ,CTConnection ,Phase
                    SectionID = dt.Rows[i]["objectid"].ToString();
                    DeviceNumber = dt.Rows[i]["objectid"].ToString();
                    NumberOfGenerators = dt.Rows[i]["NumberOfGenerators"].ToString();
                    NP = dt.Rows[i]["NP"].ToString();
                    NS = dt.Rows[i]["NS"].ToString();
                    AmbientTemperature = dt.Rows[i]["RefAmbient_Temperature_"].ToString();
                    FaultContributionBasedOnRatedPower = dt.Rows[i]["FaultContributionBasedOnRatedPo"].ToString();
                    ForceT0 = dt.Rows[i]["ForceT0"].ToString();
                  
                    ConnectionConfiguration = dt.Rows[i]["ConnectionConfiguration"].ToString();
                    ConnectionStatus = "0";
                    OverlapAngle = dt.Rows[i]["OverlapAngle"].ToString();
                    FiringAngle = dt.Rows[i]["FiringAngle"].ToString();
                    PulseNumber = dt.Rows[i]["PulseNumber"].ToString();
                    SourceHarmonicModelType = dt.Rows[i]["SourceHarmonicModelType"].ToString();
                    FrequencySourceID = dt.Rows[i]["FrequencySourceID"].ToString();
                    InsolationModelID = dt.Rows[i]["InsolationModelID"].ToString();
                    Phase = dt.Rows[i]["PhaseDesignation"].ToString();


                    string PP = "INSERT INTO TGDEVICE_SOLAR (SectionID,Location ,DeviceNumber ,DeviceStage ,Flags,InitFromEquipFlags ,EquipmentID ,NumberOfGenerators ,SymbolSize ,NS ,NP ,AmbientTemperature ,FaultContributionBasedOnRatedPower ,FaultContributionUnit ,FaultContribution ,ConstantInsolation ,ForceT0 ,InsolationModelID ,FrequencySourceID ,SourceHarmonicModelType ,PulseNumber ,FiringAngle ,OverlapAngle ,ConnectionStatus ,ConnectionConfiguration ,CTConnection ,Phase ) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", Location);
                    comr.Parameters.AddWithValue("@P3", DeviceNumber);
                    comr.Parameters.AddWithValue("@P4", DeviceStage);
                    comr.Parameters.AddWithValue("@P5", Flags);
                    comr.Parameters.AddWithValue("@P6", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P7", EquipmentID);
                    comr.Parameters.AddWithValue("@P8", NumberOfGenerators);
                    comr.Parameters.AddWithValue("@P9", SymbolSize);
                    comr.Parameters.AddWithValue("@P10", NS);
                    comr.Parameters.AddWithValue("@P11", NP);
                    comr.Parameters.AddWithValue("@P12", AmbientTemperature);
                    comr.Parameters.AddWithValue("@P13", FaultContributionBasedOnRatedPower);
                    comr.Parameters.AddWithValue("@P14", FaultContributionUnit);
                    comr.Parameters.AddWithValue("@P15", FaultContribution);
                    comr.Parameters.AddWithValue("@P16", ConstantInsolation);
                    comr.Parameters.AddWithValue("@P17", ForceT0);
                    comr.Parameters.AddWithValue("@P18", InsolationModelID);
                    comr.Parameters.AddWithValue("@P19", FrequencySourceID);
                    comr.Parameters.AddWithValue("@P20", SourceHarmonicModelType);
                    comr.Parameters.AddWithValue("@P21", PulseNumber);
                    comr.Parameters.AddWithValue("@P22", FiringAngle);
                    comr.Parameters.AddWithValue("@P23", OverlapAngle);
                    comr.Parameters.AddWithValue("@P24", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P25", ConnectionConfiguration);
                    comr.Parameters.AddWithValue("@P26", CTConnection);
                    comr.Parameters.AddWithValue("@P27", Phase);

                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }

        }

        public void GetTGDEVICE_BATTERYSTORAG(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            if (connn2.State == ConnectionState.Closed)
            {
                connn2.Open();
            }

            try
            {
                string quarry = "select * from GIS_BATTERYSTORAGE";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                string SectionID = string.Empty;
                string Location = "M";
                string DeviceNumber = string.Empty;
                string DeviceStage = "0";
                string Flags = "0";
                string InitFromEquipFlags = "0";
                string EquipmentID = "BESS";
                string Phase = string.Empty;
                string ConnectionStatus = string.Empty;
                string ConnectionConfiguration = string.Empty;
                string CTConnection = "3";
                string SymbolSize = "0";
                string MaximumSOC = string.Empty;
                string MinimumSOC = string.Empty;
                string FaultContributionBasedOnRatedPower = "1";
                string FaultContributionUnit = "0";
                string FaultContribution = "100";
                string FrequencySourceID = string.Empty;
                string SourceHarmonicModelType = string.Empty;
                string PulseNumber = string.Empty;
                string FiringAngle = string.Empty;
                string OverlapAngle = string.Empty;
                string InitialSOC = string.Empty;
                string GridOutput = string.Empty;
                string RiseFallUnit = string.Empty;
                string PowerFallLimit = string.Empty;
                string PowerRiseLimit = string.Empty;
                string ChargeDelayUnit = string.Empty;
                string ChargeDelay = string.Empty;
                string DischargeDelayUnit = string.Empty;
                string DischargeDelay = string.Empty;
                string PythonDeviceScriptID = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                    Phase = dt.Rows[i]["PhaseDesignation"].ToString();
                    ConnectionStatus = dt.Rows[i]["ENABLED"].ToString();
                    ConnectionConfiguration = dt.Rows[i]["ConnectionConfiguration"].ToString();
                    MaximumSOC = dt.Rows[i]["MaximumSOC"].ToString();
                    MinimumSOC = dt.Rows[i]["MinimumSOC"].ToString();
                    FrequencySourceID = dt.Rows[i]["FrequencySourceID"].ToString();
                    SourceHarmonicModelType = dt.Rows[i]["SourceHarmonicModelType"].ToString();
                    PulseNumber = dt.Rows[i]["PulseNumber"].ToString();
                    FiringAngle = dt.Rows[i]["FiringAngle"].ToString();
                    OverlapAngle = dt.Rows[i]["OverlapAngle"].ToString();



                    string PP = "INSERT INTO TGDEVICE_BATTERYSTORAG (SectionID,Location,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,EquipmentID,Phase,ConnectionStatus,ConnectionConfiguration,CTConnection,SymbolSize,MaximumSOC,MinimumSOC,FaultContributionBasedOnRatedPower,FaultContributionUnit,FaultContribution,FrequencySourceID,SourceHarmonicModelType,PulseNumber,FiringAngle,OverlapAngle,InitialSOC,GridOutput,RiseFallUnit,PowerFallLimit,PowerRiseLimit,ChargeDelayUnit,ChargeDelay,DischargeDelayUnit,DischargeDelay,PythonDeviceScriptID) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", Location);
                    comr.Parameters.AddWithValue("@P3", DeviceNumber);
                    comr.Parameters.AddWithValue("@P4", DeviceStage);
                    comr.Parameters.AddWithValue("@P5", Flags);
                    comr.Parameters.AddWithValue("@P6", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P7", EquipmentID);
                    comr.Parameters.AddWithValue("@P8", Phase);
                    comr.Parameters.AddWithValue("@P9", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P10", ConnectionConfiguration);
                    comr.Parameters.AddWithValue("@P11", CTConnection);
                    comr.Parameters.AddWithValue("@P12", SymbolSize);
                    comr.Parameters.AddWithValue("@P13", MaximumSOC);
                    comr.Parameters.AddWithValue("@P14", MinimumSOC);
                    comr.Parameters.AddWithValue("@P15", FaultContributionBasedOnRatedPower);
                    comr.Parameters.AddWithValue("@P16", FaultContributionUnit);
                    comr.Parameters.AddWithValue("@P17", FaultContribution);
                    comr.Parameters.AddWithValue("@P18", FrequencySourceID);
                    comr.Parameters.AddWithValue("@P19", SourceHarmonicModelType);
                    comr.Parameters.AddWithValue("@P20", PulseNumber);
                    comr.Parameters.AddWithValue("@P21", FiringAngle);
                    comr.Parameters.AddWithValue("@P22", OverlapAngle);
                    comr.Parameters.AddWithValue("@P23", InitialSOC);
                    comr.Parameters.AddWithValue("@P24", GridOutput);
                    comr.Parameters.AddWithValue("@P25", RiseFallUnit);
                    comr.Parameters.AddWithValue("@P26", PowerFallLimit);
                    comr.Parameters.AddWithValue("@P27", PowerRiseLimit);
                    comr.Parameters.AddWithValue("@P28", ChargeDelayUnit);
                    comr.Parameters.AddWithValue("@P29", ChargeDelay);
                    comr.Parameters.AddWithValue("@P30", DischargeDelayUnit);
                    comr.Parameters.AddWithValue("@P31", DischargeDelay);
                    comr.Parameters.AddWithValue("@P32", PythonDeviceScriptID);

                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }

        }

        public void GetTGDEVICE_WINDMILL(string mdbpathh, ConfigFileData hh, string Feederid, OleDbConnection connn2)
        {
            if (connn2.State == ConnectionState.Closed)
            {
                connn2.Open();
            }

            try
            {
                string quarry = "select * from GIS_WINDMILL";
                OleDbCommand com = new OleDbCommand(quarry, connn2);
                OleDbDataReader dr = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                // 																						
                string SectionID = string.Empty;
                string Location = "M";
                string DeviceNumber = string.Empty;
                string DeviceStage = "0";
                string Flags = "0";
                string InitFromEquipFlags = "0";
                string EquipmentID = "WECS";
                string NumberOfGenerators = string.Empty;
                string SymbolSize = "0";
                string ConstantWindSpeed = string.Empty;
                string ForceT0 = string.Empty;
                string WindModelID = string.Empty;
                string FrequencySourceID = string.Empty;
                string SourceHarmonicModelType = "0";
                string PulseNumber = string.Empty;
                string FiringAngle = string.Empty;
                string OverlapAngle = string.Empty;
                string ConnectionStatus = string.Empty;
                string ConnectionConfiguration = string.Empty;
                string Phase = string.Empty;
                string FaultContributionBasedOnRatedPower = "1";
                string FaultContributionUnit = "0";
                string FaultContribution = "100";
                string CTConnection = "3";


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SectionID = dt.Rows[i]["OBJECTID"].ToString();
                    DeviceNumber = dt.Rows[i]["OBJECTID"].ToString();
                    NumberOfGenerators = dt.Rows[i]["NumberOfGenerators"].ToString();
                    ConstantWindSpeed = dt.Rows[i]["ConstantWindSpeed"].ToString();
                    ForceT0 = dt.Rows[i]["ForceT0"].ToString();
                    //WindModelID = dt.Rows[i]["WindModelID"].ToString();
                    FrequencySourceID = dt.Rows[i]["FrequencySourceID"].ToString();
                    PulseNumber = dt.Rows[i]["PulseNumber"].ToString();
                    FiringAngle = dt.Rows[i]["FiringAngle"].ToString();
                    OverlapAngle = dt.Rows[i]["OverlapAngle"].ToString();
                    ConnectionStatus = dt.Rows[i]["ConnectionStatus"].ToString();
                    ConnectionConfiguration = dt.Rows[i]["ConnectionConfiguration"].ToString();
                    Phase = dt.Rows[i]["PhaseDesignation"].ToString();


                    string PP = "INSERT INTO TGDEVICE_WINDMILL(SectionID,Location,DeviceNumber,DeviceStage,Flags,InitFromEquipFlags,EquipmentID,NumberOfGenerators,SymbolSize,ConstantWindSpeed,ForceT0,WindModelID,FrequencySourceID,SourceHarmonicModelType,PulseNumber,FiringAngle,OverlapAngle,ConnectionStatus,ConnectionConfiguration,Phase,FaultContributionBasedOnRatedPower,FaultContributionUnit,FaultContribution,CTConnection) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24)";

                    OleDbCommand comr = new OleDbCommand(PP, connn2);
                    comr.Parameters.AddWithValue("@P1", SectionID);
                    comr.Parameters.AddWithValue("@P2", Location);
                    comr.Parameters.AddWithValue("@P3", DeviceNumber);
                    comr.Parameters.AddWithValue("@P4", DeviceStage);
                    comr.Parameters.AddWithValue("@P5", Flags);
                    comr.Parameters.AddWithValue("@P6", InitFromEquipFlags);
                    comr.Parameters.AddWithValue("@P7", EquipmentID);
                    comr.Parameters.AddWithValue("@P8", NumberOfGenerators);
                    comr.Parameters.AddWithValue("@P9", SymbolSize);
                    comr.Parameters.AddWithValue("@P10", ConstantWindSpeed);
                    comr.Parameters.AddWithValue("@P11", ForceT0);
                    comr.Parameters.AddWithValue("@P12", WindModelID);
                    comr.Parameters.AddWithValue("@P13", FrequencySourceID);
                    comr.Parameters.AddWithValue("@P14", SourceHarmonicModelType);
                    comr.Parameters.AddWithValue("@P15", PulseNumber);
                    comr.Parameters.AddWithValue("@P16", FiringAngle);
                    comr.Parameters.AddWithValue("@P17", OverlapAngle);
                    comr.Parameters.AddWithValue("@P18", ConnectionStatus);
                    comr.Parameters.AddWithValue("@P19", ConnectionConfiguration);
                    comr.Parameters.AddWithValue("@P20", Phase);
                    comr.Parameters.AddWithValue("@P21", FaultContributionBasedOnRatedPower);
                    comr.Parameters.AddWithValue("@P22", FaultContributionUnit);
                    comr.Parameters.AddWithValue("@P23", FaultContribution);
                    comr.Parameters.AddWithValue("@P24", CTConnection);

                    comr.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + Feederid);
            }

        }
        public string updatevoltage(string getfile, string voltage)
        {
            try
            {
                string value1 = string.Empty;
                string mdbpath = getfile + ConfigurationManager.AppSettings["Connection1"];
                string connectionstring12 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbpath;
                OleDbConnection connn2 = new OleDbConnection(connectionstring12);

                if (connn2.State != ConnectionState.Open)
                {
                    connn2.Open();
                }

                string query = "SELECT Value1 FROM GIS_DOMAIN WHERE Code = @voltage";
                using (OleDbCommand com = new OleDbCommand(query, connn2))
                {
                    com.Parameters.AddWithValue("@voltage", voltage);

                    using (OleDbDataReader dr = com.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // ✅ DO NOT redeclare `string` here
                            value1 = dr["Value1"].ToString();
                        }
                    }
                }

                connn2.Close(); // optional but safe
                return value1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

    }
}
