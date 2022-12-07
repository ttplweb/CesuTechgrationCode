using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class SetAllLocation
    {
        public void setAllDeviceLocation(OleDbConnection conn)
        {
            try
            {
                string query = string.Empty;
                DataTable TempBreakedDT = returnBreakedData(conn);
                string[] arr = { "TGDEVICE_SWITCH", "TGDEVICE_CIRCUITBREAKER" };
                foreach (string itemTable in arr)
                {
                    if (itemTable == "TGDEVICE_SWITCH")
                    {
                        query = "select tn.GISID,tn.NodeId,ts.SectionID from [" + itemTable + "] ts,[TempNode] tn where ts.DeviceNumber=tn.GISID";
                        DataTable dt = returnData(query, conn);
                        string location = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            if (TempBreakedDT.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    location = string.Empty;
                                    foreach (DataRow itemb in TempBreakedDT.Rows)
                                    {
                                        if (item[2].ToString().Trim() == itemb[0].ToString().Trim())
                                        {

                                            if (item[1].ToString().Trim() == itemb[1].ToString().Trim())
                                            {
                                                location = "S";
                                                break;
                                            }
                                            else if (item[1].ToString().Trim() == itemb[2].ToString().Trim())
                                            {
                                                location = "L";
                                                break;
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(location))
                                    {
                                        updateDeviceLocation("update [" + itemTable + "] set [Location]='" + location + "' where [DeviceNumber]='" + item[0].ToString().Trim() + "'", conn);
                                    }
                                }
                            }
                        }
                    }
                    else if (itemTable == "TGDEVICE_CIRCUITBREAKER")
                    {
                        query = "select tn.GISID,tn.NodeId,ts.SectionID from [" + itemTable + "] ts,[TempNode] tn where ts.DeviceNumber=tn.GISID";
                        DataTable dt = returnData(query, conn);
                        string location = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            if (TempBreakedDT.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    location = string.Empty;
                                    foreach (DataRow itemb in TempBreakedDT.Rows)
                                    {
                                        if (item[2].ToString().Trim() == itemb[0].ToString().Trim())
                                        {
                                            if (item[1].ToString().Trim() == itemb[1].ToString().Trim())
                                            {
                                                location = "S";
                                                break;
                                            }
                                            else if (item[1].ToString().Trim() == itemb[2].ToString().Trim())
                                            {
                                                location = "L";
                                                break;
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(location))
                                    {
                                        updateDeviceLocation("update [" + itemTable + "] set [Location]='" + location + "' where [DeviceNumber]='" + item[0].ToString().Trim() + "'", conn);
                                    }
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

        private DataTable returnBreakedData(OleDbConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                DataTable dtt = new DataTable();
                using (OleDbCommand com = new OleDbCommand("select [SectionID],[FromNodeId],[ToNodeId] from [TempSection_Breaked]", con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    OleDbDataReader dr = com.ExecuteReader();
                    dtt.Load(dr);
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
                dt = dtt.Copy();
                dtt.Clear();
                using (OleDbCommand com = new OleDbCommand("select [SectionID],[FromNodeId],[ToNodeId] from [TempSection_NotBreaked]", con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    OleDbDataReader dr = com.ExecuteReader();
                    dtt.Load(dr);
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
                dt.Merge(dtt);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        private void updateDeviceLocation(string query, OleDbConnection con)
        {
            try
            {
                using (OleDbCommand com = new OleDbCommand(query, con))
                {
                    int i = com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable returnData(string query, OleDbConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OleDbCommand com = new OleDbCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    OleDbDataReader dr = com.ExecuteReader();
                    dt.Load(dr);
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
    }
}
