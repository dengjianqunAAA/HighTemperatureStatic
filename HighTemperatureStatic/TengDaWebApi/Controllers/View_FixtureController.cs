using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDModel;
using TDDB;
using TDModel.Product.Views;

namespace TengDaWebApi.Controllers
{
    public class View_FixtureController : ApiController
    {
        View_FixtureDB db = new View_FixtureDB();

        /// <summary>
        /// 根据状态查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewsFixtureInfoByState(string data)
        {
            try
            {
                string res = string.Empty;
                List<View_GetFixtureInfoModel> list = new List<View_GetFixtureInfoModel>();
                long AllRowsCount;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                list = db.GetViewsFixtureInfoByState(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.UpDown, apimodel.StartTime, apimodel.EndTime, apimodel.TableName);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultData = list,
                        AllRowsCount=AllRowsCount
                    }
                );
                }
                TDCommom.Logs.Info(string.Format("根据状态查询夹具信息：接收数据：{0} \r\n 返回数据：{1} \r\n",data,res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Info(string.Format("根据状态查询夹具信息异常：接收数据：{0} \r\n 异常信息：{1} \r\n", data, ex.Message));
                return null;
            }
            
        }


        /// <summary>
        /// 根据夹具条码查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewsFixtureInfoByFixName(string data)
        {
            try
            {
                string res = string.Empty;
                List<View_GetFixtureInfoModel> list = new List<View_GetFixtureInfoModel>();
                long AllRowsCount;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                list = db.GetViewsFixtureInfoByFixName(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.Data,apimodel.StartTime,apimodel.EndTime);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultData = list,
                        AllRowsCount = AllRowsCount
                    }
                );
                }
                TDCommom.Logs.Info(string.Format("根据夹具条码查询夹具信息：接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Info(string.Format("根据夹具条码查询夹具信息异常：接收数据：{0} \r\n 异常信息：{1} \r\n", data, ex.Message));
                return null;
            }

        }
        

    }
}
