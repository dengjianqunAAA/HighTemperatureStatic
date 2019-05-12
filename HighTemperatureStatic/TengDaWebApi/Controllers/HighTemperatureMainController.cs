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
    public class HighTemperatureMainController : ApiController
    {
        TDDB.HT.HighTemperatureMainDB htm = new TDDB.HT.HighTemperatureMainDB();
        /// <summary>
        /// 获取主表全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetHtmAllData()
        {
            try
            {
                List<HighTemperatureMainModel> modelData = htm.GetAll().ToList();
                string res = JsonConvert.SerializeObject(modelData);
                TDCommom.Logs.Info(string.Format("查询所有的高温静置主表数据：返回数据：{0}\r\n", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询所有的高温静置主表数据：异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 通过类型来返回高温静置主表数据
        /// </summary>
        /// <param name="date">序列化ApiModel数据</param>
        /// <returns></returns>
        [HttpGet]
        public string GetHtmAllDataByType(string data)
        {
            try
            {
                ApiModel apiModel = JsonConvert.DeserializeObject<ApiModel>(data);

                List<HighTemperatureMainModel> modelData = htm.GetHtmAllDataByType(apiModel.Type);
                string res = JsonConvert.SerializeObject(modelData);
                TDCommom.Logs.Info(string.Format("通过类型来返回高温静置主表数据：返回数据：{0}\r\n", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("通过类型来返回高温静置主表数据：异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }
        //[HttpPost]
        //public string SetTemp(string temp) {
        //    try
        //    {
        //        ApiModel apiModel = JsonConvert.DeserializeObject<ApiModel>(temp);
        //        List<HighTemperatureMainModel> modelData = htm.Get
        //        string res = JsonConvert.SerializeObject()

        //    }
        //    catch (Exception ex)
        //    {
        //        TDCommom.Logs.Error(string.Format("手动设置炉具温度：异常信息：{0}\r\n", ex.Message));
        //        return null;
        //    }
        //}
    
        
    }
}