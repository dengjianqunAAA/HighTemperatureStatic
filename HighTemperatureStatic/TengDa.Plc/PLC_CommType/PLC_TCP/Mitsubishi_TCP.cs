using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TengDa.Plc.DirectiveManager.Directive_TCP;
using TengDa.Plc.IPLCComm;

namespace TengDa.Plc.PLC_CommType.PLC_TCP
{
    /// <summary>
    /// 三菱PLC采用SLMP/TCP通信协议 本次用FX5U自带以太网测试
    /// </summary>
    public class Mitsubishi_TCP : ITCPHandle
    {
        MitsubishiDirective md = new MitsubishiDirective();
        public Mitsubishi_TCP(TengDa.Plc.Common.SysEnum.ICMP icmp)
        {

            this.icmp = icmp;
        }
        public TengDa.Plc.Common.SysEnum.ICMP icmp { get; set; }
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
        /// 连接三菱PLC
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
                    TengDa.Plc.Common.Logs.Info(string.Format("连接三菱PLC：Ip地址:{0}，连接成功 ", ip));
                }
                catch (Exception ex)
                {
                    TengDa.Plc.Common.Logs.Info(string.Format("连接三菱PLC：Ip地址:{0}，连接失败，异常信息:{1} ", ip, ex.Message));
                }
                return clienSocket;
            }
            else
            {
                TengDa.Plc.Common.Logs.Info(string.Format("连接三菱PLC：Ip地址:{0}，连接失败,异常信息:Ping不通 ", ip));
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
            string sendDataStr = md.ReadSingleD(int.Parse(address), this.icmp);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                //D00000FF03FF00000800000000
                if (rev.Length == 26)
                {
                    return Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(18, 4))) > 0 ? -1 : TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(22, 4));
                }
                return -1;
            }
            else
            {
                return -1;
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
            string sendDataStr = md.ReadMultiD(int.Parse(address), number, this.icmp);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);//D00000FF03FF00000500000
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length >= 26)
                {
                    return Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(18, 4))) > 0 ? new List<string>() : md.AnalysisMultiD(rev, this.icmp);
                }
                return new List<string>();
            }
            else
            {
                return new List<string>();
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
            string sendDataStr = md.ReadSingleBit(int.Parse(address), this.icmp);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);//D00000FF03FF00000500000
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                return md.AnalysisOneM(rev, this.icmp) == 1 ? true : false;
            }
            else
            {
                return false;
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
            string sendDataStr = md.WriteSingleD(int.Parse(address), values, this.icmp);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);//D00000FF03FF00000500000
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length == 22)
                {
                    return Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(18, 4))) > 0 ? false : true;
                }
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
            string sendDataStr = md.WriteSingleBit(int.Parse(address), values, this.icmp);
            string msgStr = string.Empty;
            string rev = SendData(socket, sendDataStr, ref msgStr);//D00000FF03FF00000500000
            if (string.IsNullOrEmpty(msgStr) && !string.IsNullOrEmpty(rev))
            {
                if (rev.Length == 22)
                {
                    return Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(18, 4))) > 0 ? false : true;
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
                        TengDa.Plc.Common.Logs.Fatal(string.Format("请求三菱PLC报文:{0}\r\n 返回异常信息:{1} \r\n", sendInfo, errMsg));
                        return null;
                    }
                    byte[] numArray = new byte[1024];
                    socket.Send(TengDa.Plc.Common.DecimalConversion.HexStrTobyte(sendInfo));
                    socket.Receive(numArray);
                    rev = TengDa.Plc.Common.DecimalConversion.byteToHexStr(numArray);
                    rev = Encoding.ASCII.GetString(numArray).Replace("\0", "");
                    TengDa.Plc.Common.Logs.Info(string.Format("请求三菱PLC报文:{0}\r\n  返回报文:{1}\r\n", sendInfo, rev));
                }
                catch (Exception ex)
                {
                    errMsg = "exception:数据读取失败";
                    TengDa.Plc.Common.Logs.Fatal(string.Format("请求三菱PLC报文:{0}\r\n  返回异常信息:{1}\r\n", sendInfo, ex.Message));
                }
            }
            return rev;
        }
    }
}
