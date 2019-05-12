using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.RunCalendar;
using TengDa.DB.Base;

namespace TDDB
{
    public class RunCalendarDB : RepositoryBase<RunCalendarModel>
    {
        /// <summary>
        /// 查询年日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RunCalendarModel> GetYearCalendar(string data)
        {
            string sql = string.Format(@"select CalendarId,CurrentTime,Runtime,OperateTime from RunCalendar where CurrentTime like '%{0}%'", data);

            return this.Get(sql).ToList();
        }


        /// <summary>
        /// 查询月日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RunCalendarModel> GetMouthCalendar(string data)
        {
            string sql = string.Format(@"  SELECT * FROM (SELECT SUM(operateTime) AS operateTime,SUM(runtime) AS runtime ,mm from (SELECT [currentTime],[operateTime],[runtime],Datename(month,[currentTime]) AS mm  FROM [RunCalendar] WHERE [currentTime] LIKE '%{0}%') AS aa  GROUP BY mm ) AS bb ORDER BY mm ", data);

            return this.Get(sql).ToList();
        }


        /// <summary>
        /// 查询每日的工作日志
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public  List<RunCalendarModel> GetDayCalendar(int year, int month)
        {
            string sql = string.Format(@"SELECT *  FROM [RunCalendar] WHERE CONVERT(int,Datename(month,CurrentTime))={0}AND CONVERT(int,Datename(year,CurrentTime))={1}", month, year);
            return this.Get(sql).ToList();
        }



        /// <summary>
        /// 查询每天的工作时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public   RunCalendarModel GetProduceRunByDay(string date)
        {
            string sql = string.Format(@"SELECT * FROM RunCalendar WHERE currentTime='{0} 00:00:00' ", date);
            return this.Get(sql).FirstOrDefault();
            
        }
    }
}
