using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi.HT
{
    public class HighTemperatureMainApi
    {
        TDWebApi.HttpClient hc = new HttpClient();

        /// <summary>
        /// 高温静置主表
        /// </summary>
        /// <returns></returns>
        public string GetHtmAllData()
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureMain/GetHtmAllData");
            return res;
        }
        /// <summary>
        /// 通过类型来返回高温静置主表数据
        /// </summary>
        /// <returns></returns>
        public string GetHtmAllDataByType(string data)
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureMain/GetHtmAllDataByType?data= " + data + " ");
            return res;
        }

        //public string SetTemp(float temp) {
        //    string res = hc.Post(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureMain/" + temp + "");
        //    return res;
        //}
    }
}
