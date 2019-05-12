using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class AlarmHistoryViewApi
    {
        HttpClient http = new HttpClient();

        /// <summary>
        /// 分页查询历史报警（报警时间）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetViewsByTime(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetViewsByTime?data=" + data + "");
        }


        /// <summary>
        /// 分页查询历史报警（报警内容）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetViewsByTimeandContent(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetViewsByTimeandContent?data=" + data + "");
        }



        /// <summary>
        /// 报警类型统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetViewHistory(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetViewHistory?data=" + data + "");
        }


        /// <summary>
        /// 报警内容统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetHistoryAlarmChartData(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetHistoryAlarmChartData?data=" + data + "");
        }


        /// <summary>
        /// 报警次数统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetLoadCraftChartData(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetLoadCraftChartData?data=" + data + "");
        }

        /// <summary>
        /// 历史报警Excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetListToExcel(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetListToExcel?data=" + data + "");
        }

        /// <summary>
        /// 报警数据统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetHistoryAlarmCountPage(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetHistoryAlarmCountPage?data=" + data + "");
        }

        /// <summary>
        /// 实时报警查询
        /// </summary>
        /// <returns></returns>
        public string GetTempAlarmList(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Alarm/GetTempAlarmList?data="+data+"");
        }
    }
}
