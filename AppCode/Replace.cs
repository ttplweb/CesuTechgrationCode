using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class Replace
    {
        public string Rp(OleDbConnection connection)
        {
            #region TGSECTION_DT
            string strSQL = "select TG_DTGISID ,SectionId from TGSECTION_DT";
            using (OleDbCommand command = new OleDbCommand(strSQL, connection))
            {
                try
                {

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DataTable DT1 = new DataTable();
                        DT1.Load(reader);
                        for (int i = 0; i < DT1.Rows.Count; i++)
                        {
                            string str = DT1.Rows[i][1].ToString();
                            string str1 = DT1.Rows[i][0].ToString();
                            string mm = "update TGDEVICE_DISTRIBUTIONTRANSFORMER SET SectionID='" + str + "' WHERE DeviceNumber='" + str1 + "'";

                            OleDbCommand command3 = new OleDbCommand(mm, connection);
                            try
                            {
                                command3.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion TGSECTION_DT

            #region TGSECTION_FUSE
            string strSQL1 = "select TG_FUSEGISID ,SectionId from TGSECTION_FUSE";
            using (OleDbCommand command = new OleDbCommand(strSQL1, connection))
            {
                try
                {
               
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DataTable DT1 = new DataTable();
                        DT1.Load(reader);

                        for (int i = 0; i < DT1.Rows.Count; i++)
                        {
                            string str = DT1.Rows[i][1].ToString();
                            string str1 = DT1.Rows[i][0].ToString();

                            string mm = "update TGDEVICE_FUSE SET SectionID='" + str + "' WHERE DeviceNumber='" + str1 + "'";

                            OleDbCommand command3 = new OleDbCommand(mm, connection);
                            try
                            {

                                command3.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            #endregion TGSECTION_FUSE

            #region TGSECTION_SWITCH
            string strSQL2 = "select TG_SWITCHGISID ,SectionId from TGSECTION_SWITCH";
            using (OleDbCommand command = new OleDbCommand(strSQL2, connection))
            {
                try
                {
                  
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DataTable DT1 = new DataTable();
                        DT1.Load(reader);
                        for (int i = 0; i < DT1.Rows.Count; i++)
                        {
                            string str = DT1.Rows[i][1].ToString();
                            string str1 = DT1.Rows[i][0].ToString();

                            string mm = "update TGDEVICE_SWITCH SET SectionID='" + str + "' WHERE DeviceNumber='" + str1 + "'";

                            OleDbCommand command3 = new OleDbCommand(mm, connection);
                            try
                            {

                                command3.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion TGSECTION_SWITCH

            #region TGSECTION_BREAKER
            string strSQL3 = "select TG_BREAKERGISID ,SectionId from TGSECTION_BREAKER";
            using (OleDbCommand command = new OleDbCommand(strSQL3, connection))
            {
                try
                {
                   
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DataTable DT1 = new DataTable();
                        DT1.Load(reader);
                        for (int i = 0; i < DT1.Rows.Count; i++)
                        {
                            string str = DT1.Rows[i][1].ToString();
                            string str1 = DT1.Rows[i][0].ToString();

                            string mm = "update TGDEVICE_CIRCUITBREAKER SET SectionID='" + str + "' WHERE DeviceNumber='" + str1 + "'";

                            OleDbCommand command3 = new OleDbCommand(mm, connection);
                            try
                            {

                                command3.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion TGSECTION_BREAKER

            #region UpdateTempSection_Breaked_phase
            string strSQL4 = "update TempSection_Breaked set Phase = 'ABC' where ToNodeIndex = '0'";
            using (OleDbCommand command = new OleDbCommand(strSQL4, connection))
            {
                try
                {
                  
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            #endregion UpdateTempSection_Breaked_phase

            #region tempsection_notbreaked delete
            string strSQL5 = "select SectionId, ToNodeId from TempSection_NotBreaked T where not exists (select * from TempNode N where T.ToNodeId= N.NodeId)";
            DataTable DT5 = new DataTable();
            using (OleDbCommand command = new OleDbCommand(strSQL5, connection))
            {
                try
                {
                  
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        DT5.Load(reader);
                    }
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            try
            {
                for (int i = 0; i < DT5.Rows.Count; i++)
                {
                    string bb = DT5.Rows[i][1].ToString();

                    string aa = "delete from TempSection_NotBreaked where ToNodeId = '" + bb + "'";
                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                    {
                     
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion tempsection_notbreaked delete

            #region tempsection_notbreaked delete
            string strSQL6 = "select SectionId, FromNodeId from TempSection_NotBreaked T where not exists (select * from TempNode N where T.FromNodeId= N.NodeId)";
            DataTable DT6 = new DataTable();
            using (OleDbCommand command = new OleDbCommand(strSQL6, connection))
            {
                try
                {
                 
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DT6.Load(reader);
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            try
            {
                for (int i = 0; i < DT6.Rows.Count; i++)
                {
                    string bb = DT6.Rows[i][1].ToString();
                    string aa = "delete from TempSection_NotBreaked where FromNodeId = '" + bb + "'";
                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                    {
                       
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion tempsection_notbreaked delete

            #region tempsection_breaked delete
            string strSQL7 = "select SectionId, ToNodeId from TempSection_Breaked T where not exists (select * from TempNode N where T.ToNodeId= N.NodeId)";
            DataTable DT7 = new DataTable();
            using (OleDbCommand command = new OleDbCommand(strSQL7, connection))
            {
                try
                {
                 
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        DT7.Load(reader);
                    }
                    
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }

            try
            {
                for (int i = 0; i < DT7.Rows.Count; i++)
                {
                    string bb = DT7.Rows[i][1].ToString();

                    string aa = "delete from TempSection_Breaked where ToNodeId = '" + bb + "'";
                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                    {
                      
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            #endregion tempsection_breaked delete

            #region tempsection_breaked delete
            string strSQL8 = "select SectionId, FromNodeId from TempSection_Breaked T where not exists (select * from TempNode N where T.FromNodeId= N.NodeId)";
            DataTable DT8 = new DataTable();
            using (OleDbCommand command = new OleDbCommand(strSQL8, connection))
            {
                try
                {
                  
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DT8.Load(reader);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
            try
            {
                for (int i = 0; i < DT8.Rows.Count; i++)
                {
                    string bb = DT8.Rows[i][1].ToString();

                    string aa = "delete from TempSection_Breaked where FromNodeId = '" + bb + "'";
                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                    {
                      
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            #endregion tempsection_breaked delete

            #region TempSection_BUSBAR
            try
            {
                int tabeffect = 0;
                string breaked = "select * from TempSection_BUSBAR";
                using (OleDbCommand command = new OleDbCommand(breaked, connection))
                {
                    OleDbDataReader dr = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string value1 = dt.Rows[i][1] + "_" + dt.Rows[i][2].ToString();
                        string value2 = dt.Rows[i][3] + "_" + dt.Rows[i][4].ToString();
                        List<string> listvalue = new List<string>();
                        listvalue.Add(value1);
                        listvalue.Add(value2);

                        foreach (var item in listvalue)
                        {
                            string sectionvalue = string.Empty;
                            string notbreaked = "select * from TempSection_NotBreaked where FromNodeId ='" + item + "'";
                            string notbreaked1 = "select * from TempSection_NotBreaked where ToNodeId = '" + item + "'";
                            using (OleDbCommand command1 = new OleDbCommand(notbreaked, connection))
                            {
                                sectionvalue = Convert.ToString(command1.ExecuteScalar());
                            }
                            using (OleDbCommand command1 = new OleDbCommand(notbreaked1, connection))
                            {
                                OleDbDataReader dr1 = command1.ExecuteReader();
                                if (dr1.Read())
                                {
                                    sectionvalue = dr1["SectionId"].ToString();
                                }

                            }

                            if (!string.IsNullOrEmpty(sectionvalue))
                            {
                                string[] valuebreak = item.Split('_');
                                string valuebreak1 = valuebreak[0];
                                string valuebreak2 = valuebreak[1];
                                string insertvalue = "insert into TempNode_BUSBAR values('" + dt.Rows[i][0] + "' , '" + valuebreak1 + "' ,'" + valuebreak2 + "' , '" + sectionvalue + "')";
                                using (OleDbCommand command2 = new OleDbCommand(insertvalue, connection))
                                {
                                    command2.ExecuteNonQuery();
                                }

                            }

                            if (tabeffect == 0)
                            {
                                tabeffect++;
                                string[] valuebreak = item.Split('_');
                                string valuebreak1 = valuebreak[0];
                                string valuebreak2 = valuebreak[1];
                                string insertvalue1 = "select * from TGBUSBAR_NODECONNECTOR";

                                using (OleDbCommand command2 = new OleDbCommand(insertvalue1, connection))
                                {
                                    OleDbDataReader dr2 = command2.ExecuteReader();
                                    DataTable dt2 = new DataTable();
                                    dt2.Load(dr2);
                                    for (int j = 0; j < dt2.Rows.Count; j++)
                                    {
                                        string conector = dt2.Rows[j][1] + "_" + dt2.Rows[j][2].ToString();
                                        string conductor = "select * from TempSection_NotBreaked where FromNodeId = '" + conector + "'";
                                        string conductor1 = "select * from TempSection_NotBreaked where ToNodeId = '" + conector + "'";
                                        using (OleDbCommand command3 = new OleDbCommand(conductor, connection))
                                        {
                                            string sec = Convert.ToString(command3.ExecuteScalar());
                                            if (!string.IsNullOrEmpty(sec))
                                            {
                                                string valueinsert = "insert into TempNode_BUSBAR values('" + dt2.Rows[j][0] + "' , '" + dt2.Rows[j][1] + "' , '" + dt2.Rows[j][2] + "' , '" + sec + "')";
                                                using (OleDbCommand command4 = new OleDbCommand(valueinsert, connection))
                                                {
                                                    command4.ExecuteNonQuery();
                                                }
                                            }

                                        }

                                        using (OleDbCommand command3 = new OleDbCommand(conductor1, connection))
                                        {
                                            string sec1 = Convert.ToString(command3.ExecuteScalar());
                                            if (!string.IsNullOrEmpty(sec1))
                                            {
                                                string valueinsert = "insert into TempNode_BUSBAR values('" + dt2.Rows[j][0] + "' , '" + dt2.Rows[j][1] + "' , '" + dt2.Rows[j][2] + "' , '" + sec1 + "')";
                                                using (OleDbCommand command4 = new OleDbCommand(valueinsert, connection))
                                                {
                                                    command4.ExecuteNonQuery();
                                                }
                                            }
                                        }

                                    }

                                }


                            }


                            string busbar = "select * from TempNode_BUSBAR";

                            using (OleDbCommand cmd = new OleDbCommand(busbar, connection))
                            {
                                OleDbDataReader dr2 = cmd.ExecuteReader();
                                DataTable dt2 = new DataTable();
                                dt2.Load(dr2);
                                for (int j = 0; j < dt2.Rows.Count; j++)
                                {
                                    string values = dt2.Rows[j][1] + "_" + dt2.Rows[j][2].ToString();

                                    string query = "select FromNodeId from TempSection_NotBreaked where FromNodeId = '" + values + "'";
                                    string query1 = "select ToNodeId from TempSection_NotBreaked where ToNodeId = '" + values + "'";
                                    using (OleDbCommand cmd1 = new OleDbCommand(query, connection))
                                    {
                                        string fromnodeid = Convert.ToString(cmd1.ExecuteScalar());

                                        if (!string.IsNullOrEmpty(fromnodeid))
                                        {
                                            string update = "update TempSection_NotBreaked set FromNodeId='" + dt2.Rows[j][0] + "' where FromNodeId = '" + values + "'";
                                            using (OleDbCommand cmd2 = new OleDbCommand(update, connection))
                                            {
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    using (OleDbCommand cmd1 = new OleDbCommand(query1, connection))
                                    {
                                        string fromnodeid = Convert.ToString(cmd1.ExecuteScalar());

                                        if (!string.IsNullOrEmpty(fromnodeid))
                                        {
                                            string update = "update TempSection_NotBreaked set ToNodeId='" + dt2.Rows[j][0] + "' where ToNodeId = '" + values + "'";
                                            using (OleDbCommand cmd2 = new OleDbCommand(update, connection))
                                            {
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }


                        }
                    }


                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            #endregion TempSection_BUSBAR

            #region TGDEVICE_SWITCH
            string chnageNode = "select * from TGDEVICE_SWITCH";
            DataTable DT2 = new DataTable();
            string effectedname = string.Empty;
            string effectedname1 = string.Empty;
            using (OleDbCommand command = new OleDbCommand(chnageNode, connection))
            {
                try
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DT2.Load(reader);
                    }

                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
            try
            {
                int callswitchclass = 0;

                //string devicesection = "select * from DeviceSection where GISID='" + DT2.Rows[j][0] + "'";
                string devicesection = "select T.SectionID , N.NodeId , N.GISID from TGDEVICE_SWITCH T ,  TempNode N where T.SectionID = N.GISID AND N.GISID <> ''";

                using (OleDbCommand comm1 = new OleDbCommand(devicesection, connection))
                {
                    OleDbDataReader dr4 = comm1.ExecuteReader();
                    DataTable dt4 = new DataTable();
                    dt4.Load(dr4);

                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        effectedname = dt4.Rows[i][1].ToString();
                        if (effectedname != effectedname1)
                        {
                            int countrow = 0;
                            int countrow1 = 0;
                            effectedname1 = effectedname;
                            string bb = dt4.Rows[i][1].ToString();
                            string sw = dt4.Rows[i][2].ToString();
                            string value = string.Empty;
                            string value1 = string.Empty;
                            string _value = string.Empty;
                            string _value1 = string.Empty;
                            string mainsectionid = string.Empty;
                            string mainsectionid1 = string.Empty;
                            DataTable dt5 = new DataTable();
                            string change = "select * from TempSection_Breaked where ToNodeId='" + bb + "'";
                            //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                            using (OleDbCommand command = new OleDbCommand(change, connection))
                            {
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            countrow++;
                                            value = reader["FromNodeId"].ToString();
                                            _value = reader["ToNodeId"].ToString();
                                            mainsectionid = reader["SectionId"].ToString();

                                        }
                                    }
                                    if (countrow == 2)
                                    {
                                        reader.Close();
                                        using (OleDbDataReader reader1 = command.ExecuteReader())
                                        {
                                            dt5.Load(reader1);
                                        }
                                    }
                                }
                              
                            }

                            string change1 = "select * from TempSection_Breaked where FromNodeId='" + bb + "'";
                            DataTable dt6 = new DataTable();
                            //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                            using (OleDbCommand command = new OleDbCommand(change1, connection))
                            {
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {

                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            countrow1++;
                                            value1 = reader["ToNodeId"].ToString();
                                            _value1 = reader["FromNodeId"].ToString();
                                            mainsectionid1 = reader["SectionId"].ToString();
                                        }
                                    }
                                    if (countrow1 == 2)
                                    {
                                        reader.Close();
                                        using (OleDbDataReader reader1 = command.ExecuteReader())
                                        {
                                            dt6.Load(reader1);
                                        }
                                    }

                                }
                                
                            }

                            if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(value1))
                            {
                                Switch changeswitchvalue = new Switch();
                                changeswitchvalue.getswitch(bb, sw,connection);
                            }
                            
                            if (countrow1 == 2)
                            {
                                string[] condition = dt6.Rows[0][1].ToString().Split('_');
                                string compare = condition[0].ToString();
                                string compare1 = condition[1].ToString();

                                string[] _condition = dt6.Rows[0][3].ToString().Split('_');
                                string _compare = _condition[0].ToString();
                                string _compare1 = _condition[1].ToString();

                                double dob = Math.Abs(double.Parse(compare) - double.Parse(_compare));
                                double dob1 = Math.Abs(double.Parse(compare1) - double.Parse(_compare1));

                                if (dob > dob1)
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valuebb);
                                    val1 += double.Parse(valueaa);
                                    string main = string.Empty;
                                    main = val1 + "_" + val;
                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string FF = "update TempSection_Breaked set FromNodeId ='" + main + "' where SectionId='" + dt6.Rows[1][0] + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }

                                else
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valueaa);
                                    val1 += double.Parse(valuebb);
                                    string main = string.Empty;
                                    main = val + "_" + val1;
                                    // string GISID = DT2.Rows[i][0].ToString();
                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                    string FF = "update TempSection_Breaked set FromNodeId ='" + main + "' where SectionId='" + dt6.Rows[1][0] + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string switch1 = DT2.Rows[i][3].ToString();
                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }


                            }

                            if (countrow == 2)
                            {
                                string[] condition = dt5.Rows[0][1].ToString().Split('_');
                                string compare = condition[0].ToString();
                                string compare1 = condition[1].ToString();

                                string[] _condition = dt5.Rows[0][3].ToString().Split('_');
                                string _compare = _condition[0].ToString();
                                string _compare1 = _condition[1].ToString();

                                double dob = Math.Abs(double.Parse(compare) - double.Parse(_compare));
                                double dob1 = Math.Abs(double.Parse(compare1) - double.Parse(_compare1));

                                if (dob > dob1)
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valuebb);
                                    val1 += double.Parse(valueaa);
                                    string main = string.Empty;
                                    main = val1 + "_" + val;
                                   
                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + dt5.Rows[1][0] + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    //string switch1 = DT2.Rows[i][3].ToString();
                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }

                                else
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valueaa);
                                    val1 += double.Parse(valuebb);
                                    string main = string.Empty;
                                    main = val + "_" + val1;
                                    // string GISID = DT2.Rows[i][0].ToString();
                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                    string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + dt5.Rows[1][0] + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string switch1 = DT2.Rows[i][3].ToString();
                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                }
                            }



                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value1))
                            {
                                string[] condition = value.Split('_');
                                string compare = condition[0].ToString();
                                string compare1 = condition[1].ToString();

                                string[] _condition = _value.Split('_');
                                string _compare = _condition[0].ToString();
                                string _compare1 = _condition[1].ToString();

                                double dob = Math.Abs(double.Parse(compare) - double.Parse(_compare));
                                double dob1 = Math.Abs(double.Parse(compare1) - double.Parse(_compare1));

                                if (dob > dob1)
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valuebb);
                                    val1 += double.Parse(valueaa);
                                    string main = string.Empty;
                                    main = val1 + "_" + val;
                                    //  string GISID = DT2.Rows[i][0].ToString();


                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    //string switch1 = DT2.Rows[i][3].ToString();
                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }

                                else
                                {
                                    double val = 0.01;
                                    //double val1 = 0.009;
                                    double val1 = -0.002;
                                    string str = bb;
                                    string[] str1 = str.Split('_');
                                    string valueaa = str1[0].ToString();
                                    string valuebb = str1[1].ToString();
                                    val += double.Parse(valueaa);
                                    val1 += double.Parse(valuebb);
                                    string main = string.Empty;
                                    main = val + "_" + val1;
                                    // string GISID = DT2.Rows[i][0].ToString();
                                    string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                                    //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                                    using (OleDbCommand command = new OleDbCommand(aa, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                    string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                                    using (OleDbCommand command = new OleDbCommand(FF, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                    string switch1 = DT2.Rows[i][3].ToString();
                                    string EE = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
                                    using (OleDbCommand command = new OleDbCommand(EE, connection))
                                    {
                                        command.ExecuteNonQuery();
                                    }

                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(value) && (countrow != 2) && (countrow1 != 2))
                                {
                                    string[] condition = value.Split('_');
                                    string compare = condition[0].ToString();
                                    string compare1 = condition[1].ToString();

                                    string[] _condition = _value.Split('_');
                                    string _compare = _condition[0].ToString();
                                    string _compare1 = _condition[1].ToString();

                                    double dob = Math.Abs(double.Parse(compare) - double.Parse(_compare));
                                    double dob1 = Math.Abs(double.Parse(compare1) - double.Parse(_compare1));
                                    if (dob > dob1)
                                    {
                                        double val = 0.01;
                                        double val1 = -0.002;
                                        string[] values = _value.Split('_');
                                        string _valuee = values[0];
                                        string _valuee1 = values[1];
                                        val1 += double.Parse(_valuee);
                                        val += double.Parse(_valuee1);
                                        string main = val1 + "_" + val;


                                        string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                                        using (OleDbCommand command = new OleDbCommand(aa, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string ins = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + main + "','0','" + _value + "','0','ABC','0')";
                                        using (OleDbCommand command = new OleDbCommand(ins, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }


                                    }
                                    else
                                    {
                                        double val = 0.01;
                                        double val1 = -0.002;
                                        string[] values = _value.Split('_');
                                        string _valuee = values[0];
                                        string _valuee1 = values[1];
                                        val += double.Parse(_valuee);
                                        val1 += double.Parse(_valuee1);
                                        string main = val + "_" + val1;

                                        string FF = "update TempSection_Breaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                                        using (OleDbCommand command = new OleDbCommand(aa, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string ins = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + main + "','0','" + _value + "','0','ABC','0')";
                                        using (OleDbCommand command = new OleDbCommand(ins, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else if (!string.IsNullOrEmpty(value1) && (countrow != 2) && (countrow1 != 2))
                                {
                                    string[] condition = value1.Split('_');
                                    string compare = condition[0].ToString();
                                    string compare1 = condition[1].ToString();

                                    string[] _condition = _value1.Split('_');
                                    string _compare = _condition[0].ToString();
                                    string _compare1 = _condition[1].ToString();

                                    double dob = Math.Abs(double.Parse(compare) - double.Parse(_compare));
                                    double dob1 = Math.Abs(double.Parse(compare1) - double.Parse(_compare1));
                                    if (dob > dob1)
                                    {
                                        double val = 0.01;
                                        double val1 = -0.002;
                                        string[] values = _value1.Split('_');
                                        string _valuee = values[0];
                                        string _valuee1 = values[1];
                                        val1 += double.Parse(_valuee);
                                        val += double.Parse(_valuee1);
                                        string main = val1 + "_" + val;

                                        string FF = "update TempSection_Breaked set FromNodeId ='" + main + "' where SectionId='" + mainsectionid1 + "'";
                                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }


                                        string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                                        using (OleDbCommand command = new OleDbCommand(aa, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string ins = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + _value1 + "','0','" + main + "','0','ABC','0')";
                                        using (OleDbCommand command = new OleDbCommand(ins, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                    }
                                    else
                                    {
                                        double val = 0.01;
                                        double val1 = -0.002;
                                        string[] values = _value1.Split('_');
                                        string _valuee = values[0];
                                        string _valuee1 = values[1];
                                        val += double.Parse(_valuee);
                                        val1 += double.Parse(_valuee1);
                                        string main = val + "_" + val1;

                                        string FF = "update TempSection_Breaked set FromNodeId ='" + main + "' where SectionId='" + mainsectionid1 + "'";
                                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                                        using (OleDbCommand command = new OleDbCommand(aa, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        string ins = "insert into TempSection_Breaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + _value1 + "','0','" + main + "','0','ABC','0')";
                                        using (OleDbCommand command = new OleDbCommand(ins, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            #endregion TGDEVICE_SWITCH

         
            return ("1");
        }
    }
}
