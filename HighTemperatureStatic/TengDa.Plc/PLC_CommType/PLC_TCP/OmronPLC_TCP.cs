using TengDa.Plc.IPLCComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TengDa.Plc.PLC_CommType.PLC_TCP
{
    /// <summary>
    /// 欧姆龙PLC采用FINS/TCP通信协议 本次用CP1H自带以太网测试
    /// </summary>
    public class OmronPLC_TCP : ITCPHandle
    {
        TengDa.Plc.DirectiveManager.Directive_TCP.OmronDirective od = new DirectiveManager.Directive_TCP.OmronDirective();
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
        /// <summary>
        /// 连接欧姆龙PLC
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
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
                    string msgStr = string.Empty;
                    string rev = SendData(clienSocket, od.handshake, ref msgStr);//连接后进行握手
                    TengDa.Plc.Common.Logs.Info(string.Format("连接欧姆龙PLC：Ip地址:{0}，连接成功 ", ip));

                }
                catch (Exception ex)
                {
                    TengDa.Plc.Common.Logs.Info(string.Format("连接欧姆龙PLC：Ip地址:{0}，连接失败，异常信息:{1} ", ip, ex.Message));
                }
                return clienSocket;
            }
            else
            {
                TengDa.Plc.Common.Logs.Info(string.Format("连接欧姆龙PLC：Ip地址:{0}，连接失败,异常信息:Ping不通 ", ip));
                return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            }
        }
        /// <summary>
        /// 读单个D
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int ReadSingleD(Socket socket, string address)
        {
            string sendDataStr = od.ReadSingleD(int.Parse(address), int.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Split('.').LastOrDefault()));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length == 64)
                {
                    int num = Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(56, 4)));
                    return num == 0 || num == 64 ? TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(60, 4)) : -1;
                }
                return -1;
            }
            else
            {
                return -1;
            }
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
            string sendDataStr = od.WriteSingleD(int.Parse(address), values, int.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Split('.').LastOrDefault()));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length == 60)
                {
                    int num = Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(56, 4)));
                    return num == 0 || num == 64 ? true : false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 读多个D
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<string> ReadMultiD(Socket socket, string address, int number)
        {
            string sendDataStr = od.ReadMultiD(int.Parse(address), number, int.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Split('.').LastOrDefault()));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);//D00000FF03FF00000500000
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length >= 26)
                {
                    int num = Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(18, 4)));
                    return num == 0 || num == 64 ? od.AnalyzeMultiD(rev) : new List<string>();
                }
                return new List<string>();
            }
            else
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// 读单个M
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool ReadSingleBit(Socket socket, string address)
        {
            string sendDataStr = od.ReadSingleBit(address, int.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Split('.').LastOrDefault()));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {

                return od.WRatalearning(rev) == 1 ? true : false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 写单个M
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteSingleBit(Socket socket, string address, int values)
        {
            string sendDataStr = od.WriteSingleBit(address, values, int.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Split('.').LastOrDefault()));
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length == 60)
                {
                    int num = Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(56, 4)));
                    return num == 0 || num == 64 ? true : false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private static readonly object Locker = new object();
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="sendInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
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
                        TengDa.Plc.Common.Logs.Fatal(string.Format("请求欧姆龙PLC报文:{0}\r\n 返回异常信息:{1} \r\n", sendInfo, errMsg));
                        return null;
                    }
                    byte[] numArray = new byte[1024];
                    socket.Send(TengDa.Plc.Common.DecimalConversion.HexStrTobyte(sendInfo));
                    int length = socket.Receive(numArray);
                    rev = BitConverter.ToString(numArray, 0, length).Replace("-", "");
                    TengDa.Plc.Common.Logs.Info(string.Format("请求欧姆龙PLC报文:{0}\r\n  返回报文:{1}\r\n", sendInfo, rev));
                }
                catch (Exception ex)
                {
                    errMsg = "exception:数据读取失败";
                    TengDa.Plc.Common.Logs.Fatal(string.Format("请求欧姆龙PLC报文:{0}\r\n  返回异常信息:{1}\r\n", sendInfo, ex.Message));
                }
            }
            return rev;
        }
    }
}
