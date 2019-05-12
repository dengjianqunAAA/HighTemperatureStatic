using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDModel;
using TDDB;
using TDModel.Product;

namespace TengDaWebApi.Controllers
{
    public class ProductController : ApiController
    {
        ProductCuveDB db = new ProductCuveDB();

        /// <summary>
        /// 查询产能报表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetCuveInfo(string data)
        {
            try
            {
                string res = string.Empty;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<ProductionCuveModel> list = db.GetCuveInfo(apimodel.Data, apimodel.StartTime, apimodel.EndTime);
                if (list.Count>0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("产量统计报表：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Error(string.Format("产量统计报表异常：接收数据：{0}\r\n 错误信息：{1} \r\n", data, ex.Message));
                return null;
            }
        }
    }
}
