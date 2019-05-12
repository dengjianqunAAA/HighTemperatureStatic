using System;
using System.Collections.Generic;
using System.Text;
using TengDa.Plc.Common;

namespace TengDa.Plc.DirectiveManager.Directive_TCP
{
    /// <summary>
    /// MEWTOCOL通信协议
    /// </summary>
    public class PanasonnicDirective
    {
        /// <summary>
        /// 读取多寄存器地址格式
        /// </summary>
        /// <param name="beginAddress">开始地址</param>
        /// <param name="endAddress">结束地址</param>
        /// <returns></returns>
        public string ReadMultiD(int beginAddress, int endAddress)
        {
            string address = "%01#RDD";// "%01#RDD0030100309**";
            address = address + beginAddress.ToString().PadLeft(5, '0') + (beginAddress + endAddress - 1).ToString().PadLeft(5, '0') + "**";
            return address;
        }
        /// <summary>
        /// 读取单个寄存器地址格式
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <returns></returns>
        public string ReadSingleD(int beginAddress)
        {
            string address = "%01#RDD";// "%01#RDD0030100309**";
            address = address + beginAddress.ToString().PadLeft(5, '0') + beginAddress.ToString().PadLeft(5, '0') + "**";
            return address;
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string ReadSingleBit(string beginAddress)
        {
            return "%01#RCSR" + beginAddress.ToString().PadLeft(4, '0') + "**";
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string RAddressReadConvert(string beginAddress, string Length)
        {
            return "%01#RCPR" + beginAddress.ToString().PadLeft(4, '0') + "**";
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string WriteSingleBit(string address, int value)
        {
            return "%01#WCSR" + address.ToString().PadLeft(4, '0') + value.ToString() + "**";
        }
        /// <summary>
        /// 写入多个寄存器
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="endAddress"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string WriteMultiDAddress(int beginAddress, List<string> values)
        {
            string address = "%01#WDD";// "%01#RDD0030100309**";
            //string writeValue = DecimalConversion.Ten2Hex(values.ToString()).PadLeft(4, '0');
            address = address + beginAddress.ToString().PadLeft(5, '0') + (beginAddress + values.Count).ToString().PadLeft(5, '0');
            for (int i = 0; i < values.Count; i++)
            {
                string value = DecimalConversion.Ten2Hex(values[i]).PadLeft(4, '0');
                address += value.Substring(2, 2) + value.Substring(0, 2);
            }
            return address + "**";
        }
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="beginAddress">地址</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public string WriteSingleD(int beginAddress, int values)
        {
            string address = "%01#WDD";
            string writeValue = DecimalConversion.Ten2Hex(values.ToString()).PadLeft(4, '0');
            address = address + beginAddress.ToString().PadLeft(5, '0') + beginAddress.ToString().PadLeft(5, '0');
            address += writeValue.Substring(2, 2) + writeValue.Substring(0, 2);
            return address + "**";
        }
        /// <summary>
        ///  读单个位
        /// </summary>
        /// <param name="beginaddress"></param>
        /// <param name="endAddress"></param>
        /// <returns></returns>
        public string ReadSingleBit(int beginaddress, int endAddress)
        {
            return "%01#RCCR" + beginaddress.ToString().PadLeft(4, '0') + endAddress.ToString().PadLeft(4, '0') + "**";
        }
        /// <summary>
        /// 截取寄存器返回值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string DTValues(string str)
        {
            return str.Substring(6, str.Length - 8);
        }
        /// <summary>
        /// 解析寄存器读取的值
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public List<string> ReadDTValue(string rev)
        {
            List<string> list = new List<string>();
            //地址 %01#RDD0030100309**
            rev = DTValues(rev);
            for (int i = 0; i < rev.Length / 2; i++)
            {
                string str = rev.Substring(i * 2, 2);
                list.Add(DecimalConversion.HexStringToASCII(str));
            }
            return list;
        }
        /// <summary>
        /// 触点数据解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public int RDatalearning(string rev)
        {
            if (!string.IsNullOrEmpty(rev))
                return Convert.ToInt32(rev.Substring(6, 1));
            else
                return -1;
        }

        /// <summary>
        /// 解析一个寄存器数据
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public int DTDatalearningOne(string rev)
        {
            if (string.IsNullOrEmpty(rev))
            {
                return -1;
            }
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            return DecimalConversion.HexToTen(ss.Substring((0) + 2, 2) + ss.Substring((0), 2));
        }
        /// <summary>
        /// 截取寄存器地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string HexStr(string str)
        {
            return str.Substring(6, str.Length - 8);
        }
        /// <summary>
        /// 多个寄存器数据解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public List<string> DTDatalearning(string rev)
        {
            List<string> Data = new List<string>();

            if (string.IsNullOrEmpty(rev))
            {
                return Data;
            }
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            string highOrder = "";
            string lowOrder = "";
            for (int i = 0; i < ss.Length / 4; i++)
            {
                highOrder = ss.Substring((i * 4), 2);//高位数据  09100910
                lowOrder = ss.Substring((i * 4) + 2, 2);//低位数据
                Data.Add(DecimalConversion.HexToTen(lowOrder + highOrder).ToString());
            }
            return Data;
        }

    }
}
