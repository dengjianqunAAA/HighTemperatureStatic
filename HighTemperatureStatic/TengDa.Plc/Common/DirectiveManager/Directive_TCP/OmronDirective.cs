using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Plc.DirectiveManager.Directive_TCP
{
    // 即发送数据时候，ICF=80 
    //RSV固定00，GCT固定02
    //DNA为目标网络号
    //DA1为目标节点号
    //DA2为目标单元号
    //SNA为源网络号
    //SA1为源节点号
    //SA2为源单元号
    //SID设置00
    //此次计算机IP：10.110.59.192，PLC IP：10.110.59.33 
    //即目标（PLC）网络号0，单元号0（CPU单元，见注），节点号33
    //源（计算机）网络号0，单元号0，节点号192
    //即FINS头代码为： 
    //800002 002100 00C000 00 
    //注：PLC侧直接对CPU操作，固定为0
    //    使用0101代码读取D0 D1数据，完整命令如下： 
    //46494E530000001A（发送字节数）0000000200000000 
    //80000200210000C00000 
    //0101（读代码）82（DM地址）000000（D0）0002（2个数据） 
    //0101指令说明如下：
    public class OmronDirective
    {
        /// <summary>
        /// 握手报文
        /// </summary>
        public string handshake = "46494E530000000C0000000000000000000000" + TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(2, '0');
        /// <summary>
        /// 读单个D
        /// </summary>
        /// <param name="address"></param>
        /// <param name="goalNode">目标PLC节点</param>
        /// <returns></returns>
        public string ReadSingleD(int address, int goalNode)
        {
            string datagram = "46494E530000001A0000000200000000800002";
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010182" + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(address.ToString()).PadLeft(4, '0')).PadRight(6, '0') + "0001";
            return datagram;
        }
        /// <summary>
        /// 读多个寄存器
        /// </summary>
        /// <param name="address">开始地址</param>
        /// <param name="number">读取数量</param>
        /// <param name="goalNode">目标PLC节点</param>
        /// <returns></returns>
        public  string ReadMultiD(int address, int number, int goalNode)
        {
            string datagram = "46494E530000001A0000000200000000800002";
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010182" + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(address.ToString()).PadLeft(4, '0')).PadRight(6, '0') + TengDa.Plc.Common.DecimalConversion.Ten2Hex(number.ToString()).PadLeft(4, '0');
            return datagram;
        }
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="values">值</param>
        /// <param name="goalNode">目标PLC节点</param>
        /// <returns></returns>
        public  string WriteSingleD(int address, int values, int goalNode)
        {
            //46494E530000001A 0000000200000000 80000200 （01 PLC节点）0000 （C0 本机节点）00000101820000000002
            string datagram = "46494E530000001C0000000200000000800002";
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010282" + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(address.ToString()).PadLeft(4, '0')).PadRight(6, '0') + "0001" + TengDa.Plc.Common.DecimalConversion.Ten2Hex(values.ToString()).PadLeft(4, '0');
            return datagram;
        }
        /// <summary>
        /// 写多个寄存器
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="list">赋值数集合</param>
        /// <param name="goalNode">目标PLC节点</param>
        /// <returns></returns>
        public  string WriteMultiD(int address, List<string> list, int goalNode)
        {
            //写多个寄存器报文中,1C为（往1C字节数的总和除以2，,等于两个字节数为一个，算出后转十六进制）
            //f. 使用0102代码写D100数据，完整命令如下： 
            //46494E530000001C（发送字节数）0000000200000000
            //80000200210000C00000
            //0102（写代码）82（DM地址）006400（D100）0001（写一个数据）1234
            //0102代码格式说明如下
            StringBuilder sb = new StringBuilder();
            string datagram = "0000000200000000800002";
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010282" + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(address.ToString()).PadLeft(4, '0')).PadRight(6, '0') + list.Count.ToString().PadLeft(4, '0');
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(TengDa.Plc.Common.DecimalConversion.Ten2Hex(list[i].ToString()).PadLeft(4, '0'));
            }
            //包头                                   //计算总数除以2
            string head = "46494E53000000" + TengDa.Plc.Common.DecimalConversion.Ten2Hex(((datagram + sb.ToString()).Length / 2).ToString());
            return head + datagram + sb.ToString();
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="address">继电器触点</param>
        /// <param name="values">0或者1</param>
        /// <param name="goalNode">目标PLC节点 </param>
        /// <returns></returns>
        public  string WriteSingleBit(string address, int values, int goalNode)
        {
            //800002 000 300 00 C0 0000010182 0064000001            //101为读指令代码 //102为写指令代码
            string datagram = "46494E530000001B0000000200000000800002";              //800002 000 300 00 C0 
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010231" + ConvertCarry(address) + "0001" + TengDa.Plc.Common.DecimalConversion.Ten2Hex(values.ToString()).PadLeft(2, '0');
            return datagram;
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="address">继电器触点</param>
        /// <param name="goalNode">目标PLC节点</param>
        /// <returns></returns>
        public  string ReadSingleBit(string address, int goalNode)
        {
            string datagram = "46494E530000001A0000000200000000800002";              //800002 000 300 00 C0 
            datagram = datagram + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(goalNode.ToString()).PadLeft(4, '0')).PadRight(6, '0') + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(TengDa.Plc.Common.DNSHelp.GetLocalIpNode()).PadLeft(4, '0')).PadRight(6, '0');
            datagram = datagram + "00010131" + ConvertCarry(address) + "0001";
            return datagram;
        }
        /// <summary>
        /// 读写继电器转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private  string ConvertCarry(string str)
        {
            string[] rev = str.Split('.');//将分开个位跟小数点后几位分开
            return ((TengDa.Plc.Common.DecimalConversion.Ten2Hex(rev[0]).PadLeft(2, '0')) + (TengDa.Plc.Common.DecimalConversion.Ten2Hex(rev[1]).PadLeft(2, '0'))).PadLeft(6, '0');
        }
        /// <summary>
        ///  解析多个寄存器数据
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public List<string> AnalyzeMultiD(string rev)
        {
            List<string> list = new List<string>();
            try
            {
                string highOrder = "";
                string str = rev.Substring(60);
                for (int i = 0; i < str.Length / 4; i++)
                {
                    highOrder = str.Substring(i * 4, 4);
                    list.Add(TengDa.Plc.Common.DecimalConversion.HexToTen(highOrder).ToString());
                }
            }
            catch { return null; }

            return list;
        }
        /// <summary>
        /// 截取寄存器地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  string HexStr(string str)
        {
            return str.Substring(str.Length - 4, 4);
        }
        /// <summary>
        /// 触点数据解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public  int WRatalearning(string rev)
        {
            if (!string.IsNullOrEmpty(rev))
                return Convert.ToInt32(rev.Substring(rev.Length - 2, 2));
            else
                return -1;
        }
    }
}
