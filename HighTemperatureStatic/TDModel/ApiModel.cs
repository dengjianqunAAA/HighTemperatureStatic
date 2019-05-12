using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
    public class ApiModel
    {
        private int pageIndex;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private long allRowsCount;

        public long AllRowsCount
        {
            get { return allRowsCount; }
            set { allRowsCount = value; }
        }

        private int _htmid;
        /// <summary>
        /// 
        /// <summary>
        public int HTMId
        {
            get { return _htmid; }
            set { _htmid = value; }
        }

        private int resultCode;
        /// <summary>
        /// 调用请求结果
        /// </summary>
        public int ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }
        private object resultData;

        public object ResultData
        {
            get { return resultData; }
            set { resultData = value; }
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }


        private string fixPosition;
        /// <summary>
        /// 
        /// </summary>
        public string FixPosition
        {
            get { return fixPosition; }
            set { fixPosition = value; }
        }

        private int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public string FixState { get; set; }

        private int htdId;

        public int HTDId
        {
            get { return htdId; }
            set { htdId = value; }
        }

        public int UpDown{ get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Data { get; set; }

        public string TableName { get; set; }

        public int YearData { get; set; }

        public int MonthData { get; set; }

        public bool isFlag { get; set; }

        public int id { get; set; }
    }
}
