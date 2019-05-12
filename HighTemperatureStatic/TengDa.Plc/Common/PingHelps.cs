using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Plc.Common
{
    public class PingHelps
    {
        /// <summary>
        /// ping Sockte
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static bool PingSocket(Socket socket)
        {
            if (socket == null) return false;
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply pingReply = ping.Send(((IPEndPoint)socket.RemoteEndPoint).Address.ToString(), 10);
                    return pingReply.Status == IPStatus.Success ? true : false;
                }
                catch { return false; }

            }
        }
        /// <summary>
        /// ping IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool PingIp(string ip)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply pingReply = ping.Send(ip, 10);
                    return pingReply.Status == IPStatus.Success ? true : false;
                }
                catch { return false; }
            }

        }
    }
}
