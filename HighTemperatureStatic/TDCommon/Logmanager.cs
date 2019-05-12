using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class LogManager
    {
        public static void WriteLocalLog(string logInfo)
        {
            TDCommon.SysInfo.LocalLog = string.Format(" {0} ==>{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), logInfo) + TDCommon.SysInfo.LocalLog;
        }
    }
}
