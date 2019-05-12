using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class UserCalibration
    {
        /// <summary>
        /// 工艺
        /// </summary>
        public static string CRAFTWORK { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public static string PROCESS { get; set; }

        /// <summary>
        /// 岗位名称 
        /// </summary>
        public static string QUARTERS { get; set; }

        /// <summary>
        /// 工段
        /// </summary>
        public static string SEGMENT { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        public static string AssetNO { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public static string STAFF_ID { get; set; }

        /// <summary>
        /// 班别
        /// </summary>
        private static string shift;
        /// <summary>
        ///班别
        /// </summary>
        public static string Shift
        {
            get
            {
                return UserCalibration.shift;
            }
            set { UserCalibration.shift = value; }
        }

        private static string deviceGroupDID = "BAK#1";
        /// <summary>
        /// 设备组 ID
        /// </summary>
        public static string DeviceGroupDID
        {
            get
            {
                return deviceGroupDID;
            }

            set
            {
                deviceGroupDID = value;
            }
        }
        private static string rightDeviceGroupDID = "BAK#2";
        public static string RightDeviceGroupDID
        {
            get
            {
                return rightDeviceGroupDID;
            }

            set
            {
                rightDeviceGroupDID = value;
            }
        }

        private static string craftDID = "BAK";
        /// <summary>
        /// 工艺编号
        /// </summary>
        public static string CraftDID
        {
            get
            {
                return craftDID;
            }

            set
            {
                craftDID = value;
            }
        }

    


        

        
        /// <summary>
        /// 产线编号
        /// </summary>
        public static string ProductLineNO { get; set; }
    }
}
