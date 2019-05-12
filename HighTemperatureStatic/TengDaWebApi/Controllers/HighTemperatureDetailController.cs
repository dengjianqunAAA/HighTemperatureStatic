using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TDDB.HT;
using TDModel;
using TDModel.HT;

namespace TengDaWebApi.Controllers
{
    public class HighTemperatureDetailController : ApiController
    {
        HighTemperatureDetailDB htd = new HighTemperatureDetailDB();

        /// <summary>
        /// 根据高温静置主表ID在明细表内的数据
        /// </summary>
        /// <param name="data">序列化</param>
        /// <returns></returns>
        [HttpGet]
        public string SelectThdData(string data)
        {
            try
            {
                ApiModel model = JsonConvert.DeserializeObject<ApiModel>(data);
                List<HighTemperatureDetailModel> modelData = htd.SelectThdData(model.HTMId);
                string res = JsonConvert.SerializeObject(modelData);
                TDCommom.Logs.Info(string.Format("根据高温静置主表ID在明细表内的数据： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据高温静置主表ID在明细表内的数据：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 更新高温静置明细表
        /// </summary>
        /// <param name="data">序列化HighTemperatureDetailModel</param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateHtdData(string data)
        {
            try
            {
                TDModel.HT.HighTemperatureDetailModel htdModel = JsonConvert.DeserializeObject<TDModel.HT.HighTemperatureDetailModel>(data);
                bool flag = htd.Update(htdModel);

                string res = JsonConvert.SerializeObject(new TDModel.ApiModel()
                {
                    ResultCode = flag ? 1 : 0
                });
                TDCommom.Logs.Info(string.Format("更新高温静置明细表： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("更新高温静置明细表：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 更新高温静置明细表状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateHtdState(string data)
        {
            try
            {
                ApiModel model = JsonConvert.DeserializeObject<ApiModel>(data);
                int number = htd.UpdateHtdState(model);
                string res = JsonConvert.SerializeObject(new TDModel.ApiModel()
                {
                    ResultCode = number
                });
                TDCommom.Logs.Info(string.Format("更新高温静置明细表状态： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("更新高温静置明细表状态：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
        //  UpdateHtdState

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetDetailInfoList()
        {
            try
            {
                string res = string.Empty;
                List<HighTemperatureDetailModel> list = htd.GetDetailInfoList();
                if (list.Count>0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询炉子明细表全部数据 ：无接收数据：\r\n  返回数据：{0}\r\n", res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询炉子明细表全部数据异常 ：无接收数据：\r\n 异常信息 ：{0} \r\n",ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 入炉获取门号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetInstoveSimcard(string data)
        {
            try
            {
                string res = string.Empty;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);

                HighTemperatureDetailModel model = htd.GetInstoveSimcard(apimodel.id,apimodel.Data,apimodel.Type);

                if (model != null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(model);
                }
                TDCommom.Logs.Info(string.Format("入炉获取门号 接收数据：{0} \r\n 返回数据：{1} \r\n",data,res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("入炉获取门号异常 接收数据：{0} \r\n 异常信息：{1} \r\n", data, ex.Message));
                return null;
            }
        }
    }
}