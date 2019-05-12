using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.RunCalendar
{
    public class RunCalendarModel 
    {
        /// <summary>
        /// ID  
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// 日期 每天一条
        /// </summary>
        public string CurrentTime { get; set; }

        public int mm { get; set; }
        /// <summary>
        /// 工作时间
        /// </summary>
        public int  RunTime { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public int OperateTime { get; set; }


        //public int Num { get; set; }


        public class RunCalendarORMMapper : ClassMapper<RunCalendarModel>
        {
            public RunCalendarORMMapper()
            {
                base.Table("RunCalendar");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }
    }
}
