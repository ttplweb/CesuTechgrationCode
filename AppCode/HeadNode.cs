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
    class HeadNode
    {
        public string net(string st, OleDbConnection conn)
        {
          
            string aa = "select * from TGSOURCE_HEADNODES";
            using (OleDbCommand cmd = new OleDbCommand(aa, conn))
            {
                StreamWriter tw = File.AppendText(st + "\\CymeTextFile\\NetworkList.txt");
                try
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    //DataTable dt = new DataTable();
                    //dt.Load(reader);
                    
                    //conn.Close();
                    while (reader.Read())
                    {                    
                        tw.Write(reader["NetworkID"].ToString());
                        tw.WriteLine();
                    }
                   
                    tw.Close();
                    reader.Close();
                 
              
                }
                catch (Exception ex)
                {
                  
                    tw.Close();
                }
            
                return ("1");
               
            }
            
        }
    }
}
