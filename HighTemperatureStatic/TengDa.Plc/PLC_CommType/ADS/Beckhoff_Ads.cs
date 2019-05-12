using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TengDa.Plc.PLC_CommType.ADS
{
    /// <summary>
    /// 倍福PLC采用ADS通信协议 本次用CX2030自带以太网X000测试
    /// </summary>
    public class Beckhoff_Ads
    {

        /// <summary>
        /// 关闭倍福PLC
        /// </summary>
        /// <param name="tac"></param>
        /// <returns></returns>
        public bool CloseConnect(TcAdsClient tac)
        {
            try
            {
                tac.Close();
                tac.Dispose();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 连接倍福PLC
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
        public TcAdsClient ConnectPLC(string ip, int port, int outTime = 0)
        {
            if (TengDa.Plc.Common.PingHelps.PingIp(ip))
            {
                TcAdsClient adsClient = new TcAdsClient();
                try
                {
                    adsClient.Connect(string.Format("{0}.1.1", ip), port);
                    TengDa.Plc.Common.Logs.Info(string.Format("连接倍福PLC：Ip地址:{0}，连接成功 ", ip));

                }
                catch (Exception ex)
                {
                    TengDa.Plc.Common.Logs.Info(string.Format("连接倍福PLC：Ip地址:{0}，连接失败，异常信息:{1} ", ip, ex.Message));
                }
                return adsClient;
            }
            else
            {
                TengDa.Plc.Common.Logs.Info(string.Format("连接倍福PLC：Ip地址:{0}，连接失败,异常信息:Ping不通 ", ip));
                return new TcAdsClient();
            }
        }

        /// <summary>
        /// 读单个D-INT(PLC定义的INT和DINT类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <returns></returns>
        public int ReadSingleIntD(TcAdsClient tac, string address)
        {
            try
            {
                int number = tac.CreateVariableHandle(address);
                return Convert.ToInt32(tac.ReadAny(number, typeof(int)));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 读单个D-32位无符号整数(PLC定义的USINT类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <returns></returns>
        public int ReadSingleUsintD(TcAdsClient tac, string address)
        {
            try
            {
                int number = tac.CreateVariableHandle(address);
                return Convert.ToInt32(tac.ReadAny(number, typeof(Byte)));
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 读单个M-布尔(PLC定义的Bool类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool ReadSingleBoolM(TcAdsClient tac, string address)
        {
            try
            {
                int number = tac.CreateVariableHandle(address);
                return Convert.ToBoolean(tac.ReadAny(number, typeof(Boolean)));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读单个D-双精浮点数(PLC定义的LREAL类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <returns></returns>
        public Double ReadSingleDoubleD(TcAdsClient tac, string address)
        {
            try
            {
                int number = tac.CreateVariableHandle(address);
                return Convert.ToDouble(tac.ReadAny(number, typeof(Double)));
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// 读字符串-(PLC定义的STRING类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <returns></returns>
        public String ReadString(TcAdsClient tac, string address)
        {
            try
            {
                int number = tac.CreateVariableHandle(address);
                return tac.ReadAny(number, typeof(String), new int[] { 9999 }).ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 读数组
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <returns></returns>
        public List<string> ReadArray(TcAdsClient tac, string address)
        {
            try
            {
                List<string> list = new List<string>();
                int number = tac.CreateVariableHandle(address);
                ComplexStruct cs = (ComplexStruct)tac.ReadAny(number, typeof(ComplexStruct));
                for (int i = 0; i < cs.dintArr.Length; i++)
                {
                    list.Add(cs.dintArr[i].ToString());
                }
                return list;
            }
            catch
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// 写单个D-INT(PLC定义的INT和DINT类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <param name="values">数值</param>
        /// <returns></returns>
        public bool WriteSingleIntD(TcAdsClient tac, string address, int values)
        {
            try
            {
              int NUMBER=  tac.CreateVariableHandle(address);
                tac.WriteAny(tac.CreateVariableHandle(address), values);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写单个D-32位无符号整数(PLC定义的USINT类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <param name="values">数值</param>
        /// <returns></returns>
        public bool WriteSingleUsintD(TcAdsClient tac, string address, int values)
        {
            try
            {
                tac.WriteAny(tac.CreateVariableHandle(address), Convert.ToByte(values));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 写单个M-布尔(PLC定义的Bool类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address"></param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public bool WriteSingleBoolM(TcAdsClient tac, string address, int values)
        {
            try
            {
                tac.WriteAny(tac.CreateVariableHandle(address), values == 1 ? true : false);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 写单个D-双精浮点数(PLC定义的LREAL类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public bool WriteSingleDoubleD(TcAdsClient tac, string address, double values)
        {
            try
            {
                tac.WriteAny(tac.CreateVariableHandle(address), values);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 写字符串-(PLC定义的STRING类型)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="address">地址格式(MAIN.dint1 (MAIN目录标签 ,dint1在MAIN这个目录定义的dint1这个名)) </param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public bool WriteString(TcAdsClient tac, string address, string values)
        {
            try
            {
                tac.WriteAny(tac.CreateVariableHandle(address), values, new int[] { values.Length });
                return true;
            }
            catch
            {
                return false;
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class SimpleStruct
        {
            public double lrealVal;
            public int dintVal1;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class ComplexStruct
        {
            public short intVal;
            //封送数组 
            //指定数组具有的元素数.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dintArr = new int[4];
            [MarshalAs(UnmanagedType.I1)]
            public bool boolVal;
            public byte byteVal;
            //封送字符串 
            //字符串的字符数.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string stringVal = "";
            public SimpleStruct simpleStruct1 = new SimpleStruct();
        }
    }
}
