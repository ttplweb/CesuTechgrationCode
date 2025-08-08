using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;

namespace TechGration.AppCode
{
    class Create_GIS_Table
    {
        static int vv = 0;

        public void bb()
        {
            string hexString = "0x837F00000114263108AC2EC51B4199DD9367296B3C41FC87F4DBBDC41B41D144D8404B6B3C41";

            // Decode the hexadecimal string into byte array
            byte[] byteArray = DecodeHexString(hexString);

            // Assuming each coordinate is represented by 3 double values (x, y, z)
            int coordinateSize = sizeof(double) * 3;
            int numCoordinates = byteArray.Length / coordinateSize;

            // Parse the byte array to get the coordinates
            Vector3[] coordinates = new Vector3[numCoordinates];
            for (int i = 0; i < numCoordinates; i++)
            {
                double x = BitConverter.ToDouble(byteArray, i * coordinateSize);
                double y = BitConverter.ToDouble(byteArray, i * coordinateSize + sizeof(double));
                double z = BitConverter.ToDouble(byteArray, i * coordinateSize + sizeof(double) * 2);
                coordinates[i] = new Vector3((float)x, (float)y, (float)z);
            }

            // Calculate the length of the shape
            float length = CalculateLength(coordinates);

            Console.WriteLine("Length of the shape: " + length);
        }

        // Function to decode the hexadecimal string into a byte array
        public static byte[] DecodeHexString(string hexString)
        {
            hexString = hexString.Substring(2); // Remove "0x" prefix
            int length = hexString.Length / 2;
            byte[] byteArray = new byte[length];
            for (int i = 0; i < length; i++)
            {
                byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return byteArray;
        }

        // Function to calculate the length of the shape
        public static float CalculateLength(Vector3[] coordinates)
        {
            float length = 0;
            for (int i = 1; i < coordinates.Length; i++)
            {
                length += Vector3.Distance(coordinates[i - 1], coordinates[i]);
            }
            return length;
        }

        public void Create_GisTable(string mdb_Ptah,ConfigFileData vk,string Feederid,string getfile,string usertype,OleDbConnection connn2,String STATUS)
        {

            try
            {
               
               // GetAllData Program = new GetAllData();
                GetAllData get = new GetAllData();
                
                //get.GIS_CONSUMERMETER(getfile, vk, Feederid, connn2);
              

                get.GIS_Data(getfile, vk, Feederid, connn2, usertype,STATUS);
                get.lengthUpdate(getfile, vk, Feederid, connn2);


                get.GIS_INTERMEDIATENODESx(getfile, vk, Feederid, connn2);
              
                get.GetGIS_UpdateData(getfile, mdb_Ptah, vk, Feederid, connn2);     
              
            }
            catch (Exception ex)
            {
                TechError erro = new TechError();
                erro.ExceptionErrorHandle(getfile, ex.ToString());
            }
           
        }

        internal void CreateTable(string nEWGETFILE, object gservername, string text1, string text2, string text3)
        {
            throw new NotImplementedException();
        }

        internal void CreateTable(string nEWGETFILE, string gservername, string gusername, object gpassword, object gdatabase)
        {
            throw new NotImplementedException();
        }
    }
}
    


