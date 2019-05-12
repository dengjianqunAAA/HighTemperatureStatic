using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Plc.IPLCComm
{
    interface IUDPHandle
    {
        /// <summary>
        /// PLC连接
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="outTime">超时时间</param>
        /// <returns></returns>
        Socket ConnectPLC(string ip, int port , int outTime = 0);

        /// <summary>
        /// 读取单个寄存器地址
        /// </summary>
        /// <param name="socket">通讯对象</param>
        /// <param name="address">读取寄存器地址</param>
        /// <returns>连接Socket对象</returns>
        int ReadDM(Socket socket, string address);

        /// <summary>
        /// 写入单个寄存器
        /// </summary>
        /// <param name="socket">通讯对象</param>
        /// <param name="address">写入寄存器地址</param>
        /// <param name="values">写入寄存器值</param>
        /// <returns>是否成功</returns>
        bool WriteDM(Socket socket, string address, int values);

        /// <summary>
        /// 读取连续多个PLC寄存器值
        /// </summary>
        /// <param name="socket">通讯对象</param>
        /// <param name="address">读取寄存器开始地址</param>
        /// <param name="number">读取寄存器数量</param>
        /// <returns>读取多个数组</returns>
        List<string> ReadDMs(Socket socket, string address, int number);
        /// <summary>
        /// 关闭通讯连接
        /// </summary>
        /// <param name="socket">通讯对象</param>
        /// <returns></returns>
        bool CloseConnect(Socket socket);

        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="socket">通讯对象</param>
        /// <param name="sendInfo">发送信息</param>
        /// <param name="errMsg">返回错误信息</param>
        /// <returns>通讯返回值</returns>
        string SendData(Socket socket, string sendInfo, ref string errMsg);


    }
}
