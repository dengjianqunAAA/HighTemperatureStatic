using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.Product;

namespace TengDaWebApi.Controllers
{
    public class FixtureControlController : ApiController
    {
        FixtureControlDB db = new FixtureControlDB();

        [HttpGet]
        public string GetListInfoByCode(string data)
        {
            long AllRowsCount;
            string res = string.Empty;
            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
            List<FixtureControlModel> list = db.GetListInfoByCode(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.Data);
            if (list.Count > 0)
            {
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    AllRowsCount = AllRowsCount,
                    ResultData = list
                });
            }

            TDCommom.Logs.Info(string.Format("夹具管控查询记录：接收数据：{0}\r\n  返回数据：{1} \r\n", data, res));
            return res;
        }

        /// <summary>
        /// 修改夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateFinxtureById(string data)
        {
            try
            {
                string res = string.Empty;
                FixtureControlModel model = TDCommom.ObjectExtensions.FromJsonString<FixtureControlModel>(data);
                int i = db.UpdateFinxtureById(model);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("夹具管控修改夹具信息：接收信息：{0} \r\n 返回信息：{1} \r\n", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("夹具管控修改夹具信息异常：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return null;
            }
            
        }

        /// <summary>
        /// 添加夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsertFixture(string data)
        {
            try
            {
                string res = string.Empty;
                FixtureControlModel model = TDCommom.ObjectExtensions.FromJsonString<FixtureControlModel>(data);
                int i = db.InsertFixture(model);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("夹具管控添加夹具信息：接收信息：{0} \r\n 返回信息：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("夹具管控添加夹具信息异常：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return null;
            }
            
        }

        /// <summary>
        /// 根据夹具条码查询记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetFixtureInfoByCode(string data)
        {
            try
            {
                string res = string.Empty;
                FixtureControlModel model = db.GetFixtureInfoByCode(data);
                if (model != null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(model);
                }
                TDCommom.Logs.Info(string.Format("根据夹具条码查询夹具记录：接收信息：{0} \r\n 返回信息：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据夹具条码查询夹具记录异常：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return null;
            }
        }

    }
}
