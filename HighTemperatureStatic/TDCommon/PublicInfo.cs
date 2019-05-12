using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TDCommon
{
    public class PublicInfo
    {
        /// <summary>
        /// WebAPI调用路径
        /// </summary>
        public static string WebAPIRoute { get; set; }
        public static DateTime moveTime;
        /// <summary>
        /// 运行刷新时间
        /// </summary>
        public static DateTime MoveTime
        {
            get { return moveTime; }
            set { moveTime = value; }
        }

        public static int standby { get; set; }
        /// <summary>
        /// 待机状态
        /// </summary>
        public static int Standby
        {
            get { return standby; }
            set { standby = value; }
        }

    }

}
