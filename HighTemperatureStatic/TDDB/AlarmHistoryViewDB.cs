using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Alarm.Views;
using TengDa.DB.Base;

namespace TDDB
{
    public class AlarmHistoryViewDB : RepositoryBase<View_GetHistoryAlarmModel>
    {

        /// <summary>
        /// 分页查询历史报警
        /// </summary>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页几条</param>
        /// <param name="allRowsCount">总记录条数</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetViewsByTime(int PageIndex, int PageSize, out long allRowsCount, DateTime starttime, DateTime endtime)
        {
            string sql = string.Format("select * from View_GetHistoryAlarm where  alarmTime >= '{0}'  and alarmTime <='{1}' ", starttime, endtime);

            List<View_GetHistoryAlarmModel> list = this.GetPage(PageIndex, PageSize, out allRowsCount, sql).ToList();
            return list;
        }


        /// <summary>
        /// 分页查询历史报警
        /// </summary>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="PageSize">每页几条</param>
        /// <param name="allRowsCount">总记录条数</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="AlarmContent">报警内容</param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetViewsByTimeandContent(int PageIndex, int PageSize, out long allRowsCount, DateTime starttime, DateTime endtime, string AlarmContent)
        {
            string sql = string.Format("select * from View_GetHistoryAlarm where AlarmContent like '%{2}%' and  alarmTime >= '{0}'  and alarmTime <='{1}'", starttime, endtime, AlarmContent);

            List<View_GetHistoryAlarmModel> list = this.GetPage(PageIndex, PageSize, out allRowsCount, sql).ToList();
            return list;
        }

        /// <summary>
        /// 报警类型统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetViewHistory(DateTime startTime, DateTime endTime)
        {
            string sql = string.Format("SELECT alarmtypeid, ALarmtypeName, count(ALarmtypeName) as countNum  FROM View_GetHistoryAlarm where alarmTime>='{0}' and alarmTime<='{1}' group by  alarmtypeid, ALarmtypeName order by count(*) DESC", startTime, endTime); ;
            List<View_GetHistoryAlarmModel> list = this.Get(sql).ToList();
            return list;
        }


        /// <summary>
        /// 报警内容统计
        /// </summary>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetHistoryAlarmChartData(DateTime starTime, DateTime endTime)
        {
            string sql = string.Format("SELECT top 5 ruleid, alarmcontent, count(*) as countNum  FROM View_GetHistoryAlarm where alarmtime >= '{0}'  and alarmtime <= '{1}' group by ruleid, alarmcontent order by count(*) desc", starTime, endTime);
            List<View_GetHistoryAlarmModel> list = this.Get(sql).ToList();
            return list;
        }



        /// <summary>
        /// 报警次数统计
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetLoadCraftChartData(DateTime starTime, DateTime endTime)
        {
            string sql = string.Format("SELECT COUNT(Alarmunitname) AS countNum,Alarmunitname  FROM View_GetHistoryAlarm where alarmtime >= '{0}'  and alarmtime <= '{1}'  GROUP BY Alarmunitname", starTime, endTime);
            List<View_GetHistoryAlarmModel> list = this.Get(sql).ToList();
            return list;
        }


        /// <summary>
        /// 历史报警导出Excel
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="AlarmContent"></param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetListToExcel(DateTime starTime, DateTime endTime, string AlarmContent)
        {
            string sql = "";
            if (string.IsNullOrEmpty(AlarmContent))
            {
                sql = string.Format("select * from  View_GetHistoryAlarm where alarmTime >= '{0}'  and alarmTime <='{1}'  order by alarmTime desc", starTime, endTime);
            }
            else
            {
                sql = string.Format("select * from  View_GetHistoryAlarm where alarmcontent like '%{0}%'  and alarmTime >= '{1}'  and alarmTime <='{2}'  order by alarmTime desc", AlarmContent, starTime, endTime);
            }
            List<View_GetHistoryAlarmModel> list = this.Get(sql).ToList();
            return list;
        }


        /// <summary>
        /// 分页查询历史报警
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="AllRowsCount"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="AlarmContent"></param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetHistoryAlarmCountPage(int pageIndex, int PageSize, out long AllRowsCount, DateTime starTime, DateTime endTime, string AlarmContent)
        {
            string sql = "";
            if (string.IsNullOrEmpty(AlarmContent))
            {
                sql = string.Format(@"select 
                count(alarmcontent)  AS countNum,AlarmUnitName,RuleDid,AlarmTypeName,SolutionName ,alarmcontent
                FROM  dbo.View_GetHistoryAlarm where 1 = 1   and alarmtime >= '{0}' and alarmtime < '{1}'
                GROUP BY AlarmUnitName,RuleDid,AlarmTypeName,SolutionName, alarmcontent", starTime, endTime);
            }
            else
            {
                sql = string.Format(@"select
                    count(alarmcontent) as  countNum  ,AlarmUnitName,RuleDid,AlarmTypeName,SolutionName ,alarmcontent     FROM  dbo.View_GetHistoryAlarm where 1 = 1 and alarmcontent like '%{2}%'  and alarmtime >= '{0}' and alarmtime < '{1}'
                    GROUP BY AlarmUnitName,RuleDid,AlarmTypeName,SolutionName, alarmcontent", starTime, endTime, AlarmContent);
            }
            List<View_GetHistoryAlarmModel> list = this.GetPage(pageIndex, PageSize, out AllRowsCount, sql).ToList();
            return list;
        }

        /// <summary>
        /// 报警统计
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="AlarmContent"></param>
        /// <returns></returns>
        public List<View_GetHistoryAlarmModel> GetHistoryAlarmCountPage(DateTime starTime, DateTime endTime, string AlarmContent)
        {
            string sql = "";
            if (string.IsNullOrEmpty(AlarmContent))
            {
                sql = string.Format(@"select count(alarmcontent)AS countNum,AlarmUnitName,RuleDid,AlarmTypeName,SolutionName ,alarmcontent
    FROM  dbo.View_GetHistoryAlarm where 1 = 1   and alarmtime >= '{0}' and alarmtime < '{1}'
    GROUP BY AlarmUnitName,RuleDid,AlarmTypeName,SolutionName, alarmcontent", starTime, endTime);

            }
            else
            {
                sql = string.Format(@"select  count(alarmcontent)AS countNum,AlarmUnitName,RuleDid,AlarmTypeName,SolutionName ,alarmcontent
    FROM  dbo.View_GetHistoryAlarm where 1 = 1 and alarmcontent like '%{2}%'  and alarmtime >= '{0}' and alarmtime < '{1}'
    GROUP BY AlarmUnitName,RuleDid,AlarmTypeName,SolutionName, alarmcontent", starTime, endTime, AlarmContent);
            }
            List<View_GetHistoryAlarmModel> list = this.Get(sql).ToList();
            return list;
        }
    }
}
