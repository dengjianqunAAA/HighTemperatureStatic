using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TDModel;
using TDModel.HT;

namespace TengDaWebApi.Controllers
{
    public class HtmAndHtdController : ApiController
    {
        TDDB.HT.HtmAndHtdDB hah = new TDDB.HT.HtmAndHtdDB();
        /// <summary>
        /// 根据条件找到高温主表跟高温明细表关联数据所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string HtmRelevanceAllDataByThdID(string data)
        {
            try
            {
                ApiModel apiModel = JsonConvert.DeserializeObject<ApiModel>(data);
                List<HtmAndHtdModel> hahacModel = hah.HtmRelevanceAllDataByThdId(apiModel.HTMId);
                string res = JsonConvert.SerializeObject(hahacModel);
                TDCommom.Logs.Info(string.Format("根据查询所有的高温主表跟高温明细表关联数据：{0} \r\n返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据查询所有的高温主表跟高温明细表关联数据：{0} \r\n异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }
    }
}