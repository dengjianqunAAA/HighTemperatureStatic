using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi.HT
{
  public  class HtmAndHtdAndCaiApi
    {
        TDWebApi.HttpClient hc = new HttpClient();

        /// <summary>
        /// 高温静置主表跟高温静置明细表跟夹具表关联数据
        /// </summary>
        /// <returns></returns>
        public string HtmRelevanceAllData()
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HtmAndHtdAndCai/GetRelevanceAllData?data=");
            return res;
        }

        /// <summary>
        /// 根据条件找到高温主表跟高温明细表跟夹具表关联数据所有数据
        /// </summary>
        /// <returns></returns>
        public string HtmRelevanceAllDataByThdID(string data)
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HtmAndHtdAndCai/HtmRelevanceAllDataByThdID?data= " + data+ " ");
            return res;
        }
    }
}
