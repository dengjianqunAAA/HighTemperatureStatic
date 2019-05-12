
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon.PLCSocket
{
   public  class AnalyzeDMHelper
    {
        ///// <summary>
        ///// 读多个地址并解析
        ///// </summary>
        ///// <param name="omronPLC"></param>
        ///// <param name="Address"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static List<string> ReadMultiToErrorInfo(OmronPLC_tcp omronPLC, int Address, int value)
        //{
        //    List<string> List = new List<string> { };
        //    List<string> Binary = new List<string> { };
        //    string lowOrder = "";
        //    ushort[] us = new ushort[value];
        //    omronPLC.ReadDMs(Convert.ToUInt16(Address), ref us, Convert.ToUInt16(value));
        //    foreach (var item in us)// 将数值转2进制
        //    {
        //        Binary.Add(Convert.ToString(item, 2).PadLeft(16, '0'));//plc一个字里面包含16个位，读取不够16个位时，自动添加
        //    }
        //    for (int i = 0; i < Binary.Count; i++)
        //    {
        //        if (Binary[i].Contains("1"))//字节里有报警位
        //        {
        //            //0000 0001 0000 1000
        //            lowOrder = HeightToLow(Binary[i]);//二进制反转
        //            //0001 0000 1000 0000
        //            int ii = lowOrder.IndexOf("1");
        //            //一个字的排序方式 高位 0000 0001 0000 0110 低位
        //            while (ii >= 0 && ii < lowOrder.Length)
        //            {
        //                List.Add("D" + (Address + i).ToString() + Convert.ToString(ii).PadLeft(2, '0'));
        //                ii = lowOrder.IndexOf("1", ii + 1);
        //            }
        //        }
        //    }
        //    return List;
        //}



        public  List<string> AnalyzeToErrorInfo(long lg, int address)
        {
            List<string> List = new List<string> { };
            List<string> Binary = new List<string> { };
            string lowOrder = "";
            Binary.Add(Convert.ToString(lg, 2).PadLeft(64, '0'));
            for (int i = 0; i < Binary.Count; i++)
            {
                if (Binary[i].Contains("1"))//字节里有报警位
                {
                    lowOrder = HeightToLow(Binary[i]);
                    int ii = lowOrder.IndexOf("1");
                    //一个字的排序方式 高位 0000 0001 0000 0110 低位
                    while (ii >= 0 && ii < lowOrder.Length)
                    {
                        List.Add("Bit" + (address + i).ToString() +"."+ Convert.ToString(ii).PadLeft(2, '0'));
                        ii = lowOrder.IndexOf("1", ii + 1);
                    }
                }
            }
            return List;
        }
        /// <summary>
        /// 二进制反转
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HeightToLow(string str)
        {
            string lowOrder = "";
            char[] a = str.ToCharArray();
            Array.Reverse(a);
            foreach (char c in a)
            {
                lowOrder += c;
            }
            return lowOrder;
        }
    }
}
