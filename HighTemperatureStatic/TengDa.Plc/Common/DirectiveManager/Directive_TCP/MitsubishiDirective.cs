using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Plc.Common;

namespace TengDa.Plc.DirectiveManager.Directive_TCP
{
    /// <summary>
    ///          FX5U采用SLMP通信，Q系列则采用MC通信协议
    /// 1.本次通信测试采用FX5U-64MT/ES PLC
    /// 2.PLC内部需要挂载SLMP协议,并设置Ip跟端口号
    /// 3.PLC内部需要更改上位机发送时，PLC本机接受的数据(默认为2进制通信，本次用Ascii通信)
    /// 4.设置好2。3两点，断电重启PLC
    /// 5. 判断是否读取或写入成功，则判断判断是否大于0（等于0为写入/读取成功）
    /// </summary>
    public class MitsubishiDirective
    {
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string ReadSingleBit(int address, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {

            string datagram = "35303030303046463033464630303030313830303130";//  报头
            datagram = datagram + "30343031303030314D2A" + TengDa.Plc.Common.DecimalConversion.StrToAscii(address.ToString().PadLeft(6, '0')) + "30303031";//报尾
            return datagram;
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string WriteSingleBit(int address, int values, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            string datagram = "35303030303046463033464630303030313930303130";//  报头
            datagram = datagram + "31343031303030314D2A" + TengDa.Plc.Common.DecimalConversion.StrToAscii(address.ToString().PadLeft(6, '0')) + "30303031" + TengDa.Plc.Common.DecimalConversion.StrToAscii(values.ToString());//报尾
            return datagram;
        }
        /// <summary>
        /// 写单个D
        /// </summary>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string WriteSingleD(int address, int values, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            string datagram = "353030303030464630334646303030303143303031303134303130303030442A";//  报头
            datagram = datagram + TengDa.Plc.Common.DecimalConversion.StrToAscii(address.ToString().PadLeft(6, '0')) + "30303031" + TengDa.Plc.Common.DecimalConversion.StrToAscii((Convert.ToString(values, 16)).PadLeft(4, '0'));//报尾
            return datagram;
        }
        /// <summary>
        /// 读单个D
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string ReadSingleD(int address, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            string datagram = "353030303030464630334646303030303138303031303034303130303030442A";//  报头
            datagram = datagram + TengDa.Plc.Common.DecimalConversion.StrToAscii(address.ToString().PadLeft(6, '0')) + "30303031";
            return datagram;
        }
        /// <summary>
        /// 读多个D
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public string ReadMultiD(int address, int number, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            string datagram = "353030303030464630334646303030303138303031303034303130303030442A";//  报头
            datagram = datagram + TengDa.Plc.Common.DecimalConversion.StrToAscii(address.ToString().PadLeft(6, '0')) + TengDa.Plc.Common.DecimalConversion.StrToAscii(TengDa.Plc.Common.DecimalConversion.Ten2Hex(number.ToString()).PadLeft(4, '0'));//报尾
            return datagram;
        }
        /// <summary>
        /// 解析读单个M地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int AnalysisOneM(string str, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            if (str.Length == 23)
            {
                return Convert.ToInt32(TengDa.Plc.Common.DecimalConversion.HexToTen(str.Substring(22, 1)));
            }
            else
            {
                return -1;
            }

        }
        /// <summary>
        /// 解析多个D地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> AnalysisMultiD(string str, TengDa.Plc.Common.SysEnum.ICMP icmp)
        {
            List<string> list = new List<string>();
            string rev = str.Substring(22);
            for (int i = 0; i < rev.Length / 4; i++)
            {
                list.Add(TengDa.Plc.Common.DecimalConversion.HexToTen(rev.Substring(i * 4, 4)).ToString());
            }
            return list;
        }
    }
}
