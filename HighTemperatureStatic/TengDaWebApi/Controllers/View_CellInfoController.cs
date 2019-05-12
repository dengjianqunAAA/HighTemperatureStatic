using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.Product.Views;

namespace TengDaWebApi.Controllers
{
    
    public class View_CellInfoController : ApiController
    {
        View_CellInfoDB db = new View_CellInfoDB();

        /// <summary>
        /// 根据条码查询电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewsCellInfo(string data)
        {
            try
            {
                string res = string.Empty;
                List<View_GetCellInfoModel> list = new List<View_GetCellInfoModel>();
                long AllRowsCount;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                list = db.GetViewsCellInfo(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount,apimodel.Data, apimodel.StartTime, apimodel.EndTime);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultData = list,
                        AllRowsCount = AllRowsCount
                    }
                );
                }
                TDCommom.Logs.Info(string.Format("根据条码查询电芯信息：接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Info(string.Format("根据条码查询电芯信息异常：接收数据：{0} \r\n 异常信息：{1} \r\n", data, ex.Message));
                return null;
            }
        }

    }
}
