using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class SysInfo
    {
        private static string loginName;
        /// <summary>
        ///登录用户名称
        /// </summary>
        public static string LoginName
        {
            get { return SysInfo.loginName; }
            set { SysInfo.loginName = value; }
        }

        private static int loginId;
        /// <summary>
        ///登录用户ID
        /// </summary>
        public static int LoginId
        {
            get { return SysInfo.loginId; }
            set { SysInfo.loginId = value; }
        }

        private static string pwd;

        public static string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        private static List<int> craftDIDs;
        public static List<int> CraftDIDs
        {
            get
            {
                return craftDIDs ?? (craftDIDs = new List<int>());
            }
            set
            {
                craftDIDs = value;
            }
        }

        /// <summary>
        /// api提交地址
        /// </summary>
        //private static string postUrl = "http://172.17.135.210:8899";

        //public static string PostUrl
        //{
        //    get { return SysInfo.postUrl; }
        //    set { SysInfo.postUrl = value; }
        //}

        private static int craftId = 8;
        /// <summary>
        /// 工序名称
        /// </summary>
        public static int CraftId
        {
            get { return SysInfo.craftId; }
            set { SysInfo.craftId = value; }
        }

        private static int facilityId = 15;
        /// <summary>
        /// 工艺
        /// </summary>
        public static int FacilityId
        {
            get { return SysInfo.facilityId; }
            set { SysInfo.facilityId = value; }
        }
        //private static string resource;
        /// <summary>
        ///资产编号
        /// </summary>
        //public static string Resource
        //{
        //    get { return SysInfo.resource; }
        //    set { SysInfo.resource = value; }
        //}

     


        private static string strTempDB;
        /// <summary>
        /// 温度数据库连接地址
        /// </summary>
        public static string StrTempDB
        {
            get { return SysInfo.strTempDB; }
            set { SysInfo.strTempDB = value; }
        }


        private static string plcIP = "";
        /// <summary>
        /// PLC IP地址
        /// </summary>
        public static string PlcIP
        {
            get { return SysInfo.plcIP; }
            set { SysInfo.plcIP = value; }
        }

        private static int plcPort = 60011;
        /// <summary>
        /// PLC 端口
        /// </summary>
        public static int PlcPort
        {
            get { return SysInfo.plcPort; }
            set { SysInfo.plcPort = value; }
        }


        private static int plcStatus = 0;
        /// <summary>
        /// PLC 状态
        /// </summary>
        public static int PlcStatus
        {
            get { return SysInfo.plcStatus; }
            set { SysInfo.plcStatus = value; }
        }


        private static bool barCodeCheck;
        /// <summary>
        /// 条码校验
        /// </summary>
        public static bool BarCodeCheck
        {
            get { return SysInfo.barCodeCheck; }
            set { SysInfo.barCodeCheck = value; }
        }
        public static List<int> bakingTime;
        /// <summary>
        /// Baking时间
        /// </summary>
        public static List<int> BakingTime
        {
            get { return SysInfo.bakingTime; }
            set { SysInfo.bakingTime = value; }
        }


        private static string localLog = "";
        /// <summary>
        ///本地log信息
        /// </summary>
        public static string LocalLog
        {
            get { return SysInfo.localLog; }
            set { SysInfo.localLog = value; }
        }
        private static string _end_product_no;
        /// <summary>
        /// 左成品编码 
        /// </summary>
        public static string End_product_no
        {
            get { return SysInfo._end_product_no; }
            set { SysInfo._end_product_no = value; }
        }

        /// <summary>
        /// 左产品型号
        /// </summary>
        public static string ProductNO
        {
            get { return _productno; }
            set { _productno = value; }
        }

        public static int? ProductModelId { get; set; }

        public static string leftScanTime = "0";

        public static string LeftScanTime
        {
            get { return SysInfo.leftScanTime; }
            set { SysInfo.leftScanTime = value; }
        }

        public static string rightScanTime = "0";

        public static string RightScanTime
        {
            get { return SysInfo.rightScanTime; }
            set { SysInfo.rightScanTime = value; }
        }

        private static string _productno;
        

        public static int msgNum { get; set; }
    


        ///// <summary>
        ///// 早班交接班时间
        ///// </summary>
        //public static string ShiftTimeM { get; set; }

        ///// <summary>
        ///// 晚班交接班时间
        ///// </summary>
        //public static string ShiftTimeE { get; set; }


        private static string shiftTimeM = "";
        /// <summary>
        ///早班交接班时间
        /// </summary>
        public static string ShiftTimeM
        {
            get { return SysInfo.shiftTimeM; }
            set { SysInfo.shiftTimeM = value; }
        }


        /// <summary>
        /// 晚班交接班时间
        /// </summary>
        private static string shiftTimeE = "";
        /// <summary>
        ///晚班交接班时间
        /// </summary>
        public static string ShiftTimeE
        {
            get { return SysInfo.shiftTimeE; }
            set { SysInfo.shiftTimeE = value; }
        }
    }
}
