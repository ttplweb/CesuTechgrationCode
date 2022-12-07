using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class Switch
    {
        public void getswitch(string bb, string sw, OleDbConnection connection)
        {
            string effectedname = string.Empty;
            string effectedname1 = string.Empty;
            string devicesection = "select T.SectionID , N.NodeId , N.GISID from TGDEVICE_SWITCH T ,  TempNode N where T.SectionID = N.GISID";

            try
            {
                OleDbCommand comm1 = new OleDbCommand(devicesection, connection);
                OleDbDataReader dr4 = comm1.ExecuteReader();
                DataTable dt4 = new DataTable();
                dt4.Load(dr4);

                int countrow = 0;
                int countrow1 = 0;
                effectedname1 = effectedname;

                string value = string.Empty;
                string value1 = string.Empty;
                string _value = string.Empty;
                string _value1 = string.Empty;
                string mainsectionid = string.Empty;
                string mainsectionid1 = string.Empty;
                DataTable dt5 = new DataTable();
                string change = "select * from TempSection_NotBreaked where ToNodeId='" + bb + "'";
                //string aa = "update TempNode set NodeId='" + main + "', Y='" + val + "' where Y='" + valuebb + "' and GISID='" + GISID + "' and NodeId  = '" + str + "'";
                using (OleDbCommand command = new OleDbCommand(change, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        // dt5.Load(reader);
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

                string change1 = "select * from TempSection_NotBreaked where FromNodeId='" + bb + "'";
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

                        string FF = "update TempSection_NotBreaked set FromNodeId ='" + main + "' where SectionId='" + dt6.Rows[1][0] + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        //string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                        using (OleDbCommand command = new OleDbCommand(EE, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    else
                    {
                        double val = 0.01;
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
                        string FF = "update TempSection_NotBreaked set FromNodeId ='" + main + "' where SectionId='" + dt6.Rows[1][0] + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        //  string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
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

                        string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + dt5.Rows[1][0] + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        //string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                        using (OleDbCommand command = new OleDbCommand(EE, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    else
                    {
                        double val = 0.01;
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
                        string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + dt5.Rows[1][0] + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
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
                        string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                        //string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "' , '" + str + "','0','" + main + "','0','ABC','0')";
                        using (OleDbCommand command = new OleDbCommand(EE, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        double val = 0.01;
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
                        string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                        using (OleDbCommand command = new OleDbCommand(FF, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // string switch1 = DT2.Rows[i][3].ToString();
                        string EE = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values('" + sw + "','" + str + "','0','" + main + "','0','ABC','0')";
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


                            string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                            using (OleDbCommand command = new OleDbCommand(FF, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                            using (OleDbCommand command = new OleDbCommand(aa, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string ins = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + main + "','0','" + _value + "','0','ABC','0')";
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

                            string FF = "update TempSection_NotBreaked set ToNodeId ='" + main + "' where SectionId='" + mainsectionid + "'";
                            using (OleDbCommand command = new OleDbCommand(FF, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                            using (OleDbCommand command = new OleDbCommand(aa, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string ins = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + main + "','0','" + _value + "','0','ABC','0')";
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

                            string FF = "update TempSection_NotBreaked set FromNodeId ='" + main + "' where SectionId='" + mainsectionid1 + "'";
                            using (OleDbCommand command = new OleDbCommand(FF, connection))
                            {
                                command.ExecuteNonQuery();
                            }


                            string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val1 + "' , '" + val + "')";
                            using (OleDbCommand command = new OleDbCommand(aa, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string ins = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + _value1 + "','0','" + main + "','0','ABC','0')";
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

                            string FF = "update TempSection_NotBreaked set FromNodeId ='" + main + "' where SectionId='" + mainsectionid1 + "'";
                            using (OleDbCommand command = new OleDbCommand(FF, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string aa = "insert into TempNode (NodeId ,X , Y ) values ('" + main + "' , '" + val + "' , '" + val1 + "')";
                            using (OleDbCommand command = new OleDbCommand(aa, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            string ins = "insert into TempSection_NotBreaked (SectionId ,FromNodeId,FromNodeIndex,ToNodeId,ToNodeIndex,Phase,EnvironmentID) values ('" + sw + "','" + _value1 + "','0','" + main + "','0','ABC','0')";
                            using (OleDbCommand command = new OleDbCommand(ins, connection))
                            {
                                command.ExecuteNonQuery();
                            }



                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                
            }

        }
    }
}
