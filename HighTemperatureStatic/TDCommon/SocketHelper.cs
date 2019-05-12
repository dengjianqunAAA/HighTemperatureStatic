using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TDCommon
{
    public class SocketHelper
    {

        public static bool IsCoon { get; set; }

        /// <summary>
        /// socket通讯连接
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Socket tcpConn(string IP, int port, int outTime)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(IP), Convert.ToInt32(port));
            Socket clienSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clienSocket.ReceiveTimeout = outTime;
                clienSocket.Connect((EndPoint)ipEndPoint);
                IsCoon = true;
            }
            catch (Exception ex)
            {
                //Console.Write((object)ex);
            }
            return clienSocket;
        }
        private static readonly object Locker1 = new object();
        public static string SendAddress(Socket st, string address)
        {
            string rev = "";
            lock (Locker1)
            {
                try
                {
                    if (!st.Connected)
                    {
                        IsCoon = false;
                        //st.Close();
                        //st = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ////st.Shutdown(SocketShutdown.Both);
                        ////st.Disconnect(true);  
                        //st = TDCommon.PLCSocket.SocketHelper.tcpConn("192.168.1.5", 32770, TDCommon.PLCSocket.PLCInfo.OutTime);
                    }
                    byte[] bytes = Encoding.ASCII.GetBytes(address + "\r");
                    byte[] numArray = new byte[1024];
                    
                    st.Send(bytes);
                    st.Receive(numArray);
                    rev = Encoding.ASCII.GetString(numArray).Replace("\0", "");
                    if (rev.Contains("!"))//未读到了正确数据
                    {
                        rev = "";
                    }
                }
                catch
                {
                    rev = "";
                }
            }
            return rev;
        }
    }
}
