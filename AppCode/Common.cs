using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class Common
    {
        public string com(string Cyme_config_txtReportDirectory)
        {
            string log1 =
@"[GENERAL]
DATE=" + DateTime.Now.ToString("MMMM dd, yyyy 'at' HH:mm:ss") +
             @"
CYME_VERSION=8.00
CYMDIST_REVISION=01
              
[SI]
";



            StreamWriter log = new StreamWriter(Cyme_config_txtReportDirectory + "\\TempChanges\\text.txt");

            log.Write(log1);
            log.Close();
            return ("1");
        }
    }
}
