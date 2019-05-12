using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Plc.IPLCComm
{
    interface ISerialPortHandle
    {
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="sendDataType">发送数据类型</param>
        /// <param name="outTime">超时时间</param>
        /// <returns>连接对象</returns>
        SerialPort ConnectPLC(string portName, int baudRate, StopBits stopBits, int outTime = 0);


        /// <summary>
        /// 读取单个寄存器
        /// </summary>
        /// <param name="serialPort">通讯对象</param>
        /// <param name="address">读取地址</param>
        /// <returns>读取返回值</returns>
        int ReadDM(SerialPort serialPort, string address);

        /// <summary>
        /// 写入单个寄存器
        /// </summary>
        /// <param name="serialPort">通讯对象</param>
        /// <param name="address">写入寄存器地址</param>
        /// <param name="values">写入寄存器值</param>
        /// <returns>是否成功</returns>
        bool WriteDM(SerialPort serialPort, string address, int values);

        /// <summary>
        /// 读取连续多个PLC寄存器值
        /// </summary>
        /// <param name="serialPort">通讯对象</param>
        /// <param name="address">读取寄存器开始地址</param>
        /// <param name="number">读取寄存器数量</param>
        /// <returns>读取多个数组</returns>
        List<string> ReadDMs(SerialPort serialPort, string address, int number);


        /// <summary>
        /// 关闭通讯连接
        /// </summary>
        /// <param name="serialPort">通讯对象</param>
        /// <returns></returns>
        bool CloseConnect(SerialPort serialPort);


        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="serialPort">通讯对象</param>
        /// <param name="sendInfo">发送信息</param>
        /// <param name="errMsg">返回错误信息</param>
        /// <returns>通讯返回值</returns>
        string SendData(SerialPort serialPort, string sendInfo, ref string errMsg);
    }
}
