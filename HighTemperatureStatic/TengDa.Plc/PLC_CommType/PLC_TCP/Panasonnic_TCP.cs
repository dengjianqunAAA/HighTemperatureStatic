using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TengDa.Plc.IPLCComm;

namespace TengDa.Plc.PLC_CommType.PLC_TCP
{
    public class Panasonnic_TCP : ITCPHandle
    {
        DirectiveManager.Directive_TCP.PanasonnicDirective pd = new DirectiveManager.Directive_TCP.PanasonnicDirective();
        public bool CloseConnect(Socket socket)
        {
            try
            {
                socket.Close();
                socket.Dispose();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Socket ConnectPLC(string ip, int port, int outTime = 0)
        {
            if (TengDa.Plc.Common.PingHelps.PingIp(ip))
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
                Socket clienSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    clienSocket.ReceiveTimeout = outTime;
                    clienSocket.Connect((EndPoint)ipEndPoint);
                    TengDa.Plc.Common.Logs.Info(string.Format("连接松下PLC：Ip地址:{0}，连接成功 ", ip));
                }
                catch (Exception ex)
                {
                    TengDa.Plc.Common.Logs.Info(string.Format("连接松下PLC：Ip地址:{0}，连接失败，异常信息:{1} ", ip, ex.Message));
                }
                return clienSocket;
            }
            else
            {
                TengDa.Plc.Common.Logs.Info(string.Format("连接松下PLC：Ip地址:{0}，连接失败,异常信息:Ping不通 ", ip));
                return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            }
        }
        /// <summary>
        /// 读多个寄存器
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<string> ReadMultiD(Socket socket, string address, int number)
        {
            string sendDataStr = pd.ReadMultiD(int.Parse(address), number);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (!rev.Contains("!"))
                {
                    return pd.DTDatalearning(rev);
                }
                return new List<string>();
            }
            else
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// 读单个寄存器
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int ReadSingleD(Socket socket, string address)
        {
            string sendDataStr = pd.ReadSingleD(int.Parse(address));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (!rev.Contains("!"))
                {
                    return pd.DTDatalearningOne(rev);
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool ReadSingleBit(Socket socket, string address)
        {
            string sendDataStr = pd.ReadSingleBit(address);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (!rev.Contains("!"))
                {
                    return pd.RDatalearning(rev) == 1 ? true : false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private static readonly object Locker = new object();
        public string SendData(Socket socket, string sendInfo, ref string errMsg)
        {
            string rev = string.Empty;
            lock (Locker)
            {
                try
                {
                    if (!socket.Connected || !TengDa.Plc.Common.PingHelps.PingSocket(socket))
                    {
                        errMsg = "exception:端口连接失败";
                        TengDa.Plc.Common.Logs.Fatal(string.Format("请求松下PLC报文:{0}\r\n 返回异常信息:{1} \r\n", sendInfo, errMsg));
                        return null;
                    }
                    byte[] bytes = Encoding.ASCII.GetBytes(sendInfo + "\r");
                    byte[] numArray = new byte[1024];
                    socket.Send(bytes);
                    socket.Receive(numArray);
                    rev = Encoding.ASCII.GetString(numArray).Replace("\0", "");
                    TengDa.Plc.Common.Logs.Info(string.Format("请求松下PLC报文:{0}\r\n  返回报文:{1}\r\n", sendInfo, rev));
                }
                catch (Exception ex)
                {
                    errMsg = "exception:数据读取失败";
                    TengDa.Plc.Common.Logs.Fatal(string.Format("请求松下PLC报文:{0}\r\n  返回异常信息:{1}\r\n", sendInfo, ex.Message));
                }
            }
            return rev;
        }
        /// <summary>
        /// 写单个D
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteSingleD(Socket socket, string address, int values)
        {
            string sendDataStr = pd.WriteSingleD(int.Parse(address), values);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (!rev.Contains("!")) return true;
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteSingleBit(Socket socket, string address, int values)
        {
            string sendDataStr = pd.WriteSingleBit(address, values);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (!rev.Contains("!")) return true;
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
