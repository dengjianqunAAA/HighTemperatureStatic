using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi.HT
{
    public class HtmAndHtdApi
    {
        TDWebApi.HttpClient hc = new HttpClient();

        /// <summary>
        ///  通过状态找到相关的夹具信息
        /// </summary>
        /// <returns></returns>
        public string HtmRelevanceAllDataByThdID(string data)
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "HtmAndHtd/HtmRelevanceAllDataByThdID?data= " + data + " ");
            return res;
        }
    }
}
