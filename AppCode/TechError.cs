using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class TechError
    {

        public void ExceptionErrorHandle(string GETFILE,string Err)
        {
            string Path = GETFILE + "\\ErrorLog\\ExcepctionError.txt";
            StreamWriter sw11 = File.AppendText(Path);
            sw11.WriteLine($"{DateTime.Now}: {Err}");
            sw11.WriteLine();
            sw11.Close();
        }

        public void ImportExceptionErrorHandle(string GETFILE, string Err)
        {
            string Path = GETFILE + "\\ErrorLog\\ImportExcepctionError.txt";
            StreamWriter sw11 = File.AppendText(Path);
            sw11.WriteLine($"{Err}");
            sw11.WriteLine();
            sw11.Close();
        }

    }
}
