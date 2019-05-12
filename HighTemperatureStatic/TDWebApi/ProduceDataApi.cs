using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public  class ProduceDataApi
    {
        HttpClient http = new HttpClient();


        public string GetListInfo()
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetListInfo");
        }

        /// <summary>
        /// 修改上料数量
        /// </summary>
        /// <returns></returns>
        public string UpdateChargingCount()
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute+ "ProduceData/UpdateChargingCount","");
        }

        /// <summary>
        /// 修改下料数量
        /// </summary>
        /// <returns></returns>
        public string UpdateBlankingCount()
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/UpdateBlankingCount", "");
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <returns></returns>
        public string InsertProduceData()
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/InsertProduceData", "");
        }

        /// <summary>
        /// 查询是否有当天记录
        /// </summary>
        /// <returns></returns>
        public string GetInfoByTime()
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetInfoByTime");
        }


        /// <summary>
        /// 根据日期查询产量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetProduceDataByDay(string data)
        {
            string res= http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetProduceDataByDay?data="+data+"");
            return res;
        }

        /// <summary>
        /// 查询年日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetYearCalendar(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetYearCalendar?data="+data+"");
        }

        /// <summary>
        /// 查询月日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetMouthCalendar(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetMouthCalendar?data=" + data + "");
        }


        /// <summary>
        /// 查询每日日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDayCalendar(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetDayCalendar?data=" + data + "");
        }


        /// <summary>
        /// 查询每日工作时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetProduceRunByDay(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "ProduceData/GetProduceRunByDay?data=" + data + "");
        }
    }
}
