using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TDModel
{
    [XmlRoot(ElementName = "PLCAddress")]
    [Serializable]
    public  class PLCAddressModel
    {
        #region 通用
        /// <summary>
        /// PLC连接超时时间
        /// </summary>
        [XmlElement(ElementName = "PLCconnTimeOut")]
        public string PLCconnTimeOut { get; set; }

        /// <summary>
        /// PLC连接端口号
        /// </summary>
        [XmlElement(ElementName = "PLCconnPort")]
        public string PLCconnPort { get; set; }

        /// <summary>
        /// 心跳
        /// </summary>
        [XmlElement(ElementName = "HeartBeat")]
        public string HeartBeat { get; set; }

       

        #endregion



        public string  Paramater { get; set; }

        #region 上料
        /// <summary>
        /// 左边是否有空夹具扫码
        /// </summary>
        [XmlElement(ElementName = "IsEmptyScaninLeft")]
        public string IsEmptyScaninLeft { get; set; }

        /// <summary>
        /// 左边空夹具扫码是否OK
        /// </summary>
        [XmlElement(ElementName = "IsFixScanCheckLeft")]
        public string IsFixScanCheckLeft { get; set; }

        /// <summary>
        /// 右边是否有空夹具扫码
        /// </summary>
        [XmlElement(ElementName = "IsEmptyScaninRight")]
        public string IsEmptyScaninRight { get; set; }

        /// <summary>
        /// 右边空夹具扫码是否OK
        /// </summary>
        [XmlElement(ElementName = "IsFixScanCheckRight")]
        public string IsFixScanCheckRight { get; set; }

        /// <summary>
        /// 是否有电芯扫码
        /// </summary>
        [XmlElement(ElementName = "IsCellScan")]
        public string IsCellScan { get; set; }
        /// <summary>
        /// 电芯扫码是否OK
        /// </summary>
        [XmlElement(ElementName = "IsCheckCellOK")]
        public string IsCheckCellOK { get; set; }

        /// <summary>
        /// 扫码连续三个
        /// </summary>
        [XmlElement(ElementName = "ScanContinuityNG")]
        public string ScanContinuityNG { get; set; }


        /// <summary>
        /// 电芯入缓存位
        /// </summary>
        [XmlElement(ElementName = "CellToBuff")]
        public string CellToBuff { get; set; }

        /// <summary>
        /// 夹具去夹具
        /// </summary>
        [XmlElement(ElementName = "CellToFix")]
        public string CellToFix { get; set; }

        /// <summary>
        /// left夹具状态
        /// </summary>
        [XmlElement(ElementName = "LeftFixState")]
        public string LeftFixState { get; set; }

        /// <summary>
        /// right夹具状态
        /// </summary>
        [XmlElement(ElementName = "RightFixState")]
        public string RightFixState { get; set; }


        /// <summary>
        /// left夹具电芯数量
        /// </summary>
        [XmlElement(ElementName = "LeftCellCount")]
        public string LeftCellCount { get; set; }

        /// <summary>
        /// right夹具电芯数量
        /// </summary>
        [XmlElement(ElementName = "RightCellCount")]
        public string RightCellCount { get; set; }





        /// <summary>
        /// 左下料夹具电芯数量
        /// </summary>
        [XmlElement(ElementName = "LeftBlankCellCount")]
        public string LeftBlankCellCount { get; set; }



        /// <summary>
        /// 右下料夹具电芯数量
        /// </summary>
        [XmlElement(ElementName = "RightBlankCellCount")]
        public string RightBlankCellCount { get; set; }


        /// <summary>
        /// 下料左夹具状态 1有夹具 3空夹具
        /// </summary>
        [XmlElement(ElementName = "LeftBlankFixState")]
        public string LeftBlankFixState { get; set; }


        /// <summary>
        /// 下料右夹具状态 1有夹具 3空夹具
        /// </summary>
        [XmlElement(ElementName = "RightBlankFixState")]
        public string RightBlankFixState { get; set; }


        
        #endregion

        #region RGV
        /// <summary>
        /// RGV当前状态 -空闲 -执行任务中
        /// </summary>
        [XmlElement(ElementName = "RgvState")]
        public string RgvState { get; set; }


        /// <summary>
        /// 启动RGV
        /// </summary>
        [XmlElement(ElementName = "RGVStart")]
        public string RGVStart { get; set; }

        /// <summary>
        /// 停止RGV
        /// </summary>
        [XmlElement(ElementName = "RGVStop")]
        public string RGVStop { get; set; }

        /// <summary>
        /// 复位RGV
        /// </summary>
        [XmlElement(ElementName = "RGVReset")]
        public string RGVReset { get; set; }

        /// <summary>
        /// 急停
        /// </summary>
        [XmlElement(ElementName = "EmergencyStop")]
        public string EmergencyStop { get; set; }

        /// <summary>
        /// 手动/自动
        /// </summary>
        [XmlElement(ElementName = "Automation")]
        public string Automation { get; set; }

        /// <summary>
        /// 写入RGV门号
        /// </summary>
        [XmlElement(ElementName = "RgvSimcard")]
        public string RgvSimcard { get; set; }


        /// <summary>
        /// 取放信号 2取 1放
        /// </summary>
        [XmlElement(ElementName = "FetchOrRelease")]
        public string FetchOrRelease { get; set; }

        /// <summary>
        /// 是否可以取放料
        /// </summary>
        [XmlElement(ElementName = "IsFetchOrRelease")]
        public string IsFetchOrRelease { get; set; }

        /// <summary>
        /// 取放完成
        /// </summary>
        [XmlElement(ElementName = "FeRefinish")]
        public string FeRefinish { get; set; }
        




        #endregion

        #region 炉子
        /// <summary>
        /// 开门指令
        /// </summary>
        [XmlElement(ElementName = "OpenSimcard")]
        public string OpenSimcard { get; set; }

        /// <summary>
        /// 开门完成
        /// </summary>
        [XmlElement(ElementName = "Openfinish")]
        public string Openfinish { get; set; }

        /// <summary>
        /// 关门指令
        /// </summary>
        [XmlElement(ElementName = "CloseSimcard")]
        public string CloseSimcard { get; set; }

        /// <summary>
        /// 关门完成
        /// </summary>
        [XmlElement(ElementName = "Closefinish")]
        public string Closefinish { get; set; }

        #endregion




    }
}
