using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCHelper
{
    public class KeyenceHelps
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Socket Connect(string ip, int port, int outTime)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(System.Net.IPAddress.Parse(ip), Convert.ToInt32(port));
            Socket clienSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clienSocket.ReceiveTimeout = outTime;
                clienSocket.Connect((EndPoint)ipEndPoint);
                //     clienSocket.BeginReceive(asynchronousBuffer, 0, asynchronousBuffer.Length, SocketFlags.None, new AsyncCallback(Nonsynchronous.ReceiveCallback), clienSocket);
            }
            catch(Exception EX) { }
            return clienSocket;
        }

        /// <summary>
        /// 发送
        /// </summary>
        public string Send(Socket st, string address)
        {
            try
            {
                if (st.Connected)
                {
                    string msg = string.Empty;
                    byte[] buffer = new byte[1024];
                    byte[] bytes = Encoding.ASCII.GetBytes(address + "\r");
                    st.Send(bytes);
                    Thread.Sleep(300);
                    st.Receive(buffer);
                    return Encoding.ASCII.GetString(buffer).Replace("\0", "").Trim();
                }
                else
                {
                    return "ERROR";
                }
            }
            catch(Exception ex)
            {
                return "ERROR";
            }
        }
    }
}
