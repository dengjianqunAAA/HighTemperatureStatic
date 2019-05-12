using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi.HT
{
  public  class HighTemperatureDetailApi
    {
        TDWebApi.HttpClient hc = new HttpClient();

        /// <summary>
        /// 根据高温静置主表ID在找到明细表内的数据
        /// </summary>
        /// <returns></returns>
        public string SelectThdData(string data)
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureDetail/SelectThdData?data=" + data + "");
            return res;
        }
        /// <summary>
        /// 更新高温静置明细表信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateHtdData(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureDetail/UpdateHtdData?data=" + data + "", data);
        }
        /// <summary>
        /// 更新高温静置明细表状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateHtdState(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureDetail/UpdateHtdState?data=" + data + "", data);
        }
		/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDetailInfoList()
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureDetail/GetDetailInfoList");
        }

        /// <summary>
        /// 入炉获取门号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetInstoveSimcard(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HighTemperatureDetail/GetInstoveSimcard?data=" + data + "");
        }
        
    }
}
