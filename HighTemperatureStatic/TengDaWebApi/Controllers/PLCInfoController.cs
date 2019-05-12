using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDModel;

namespace TengDaWebApi.Controllers
{
    public class PLCInfoController : ApiController
    {
        TDDB.PLCInfoDB plcdb = new TDDB.PLCInfoDB();

        /// <summary>
        /// 查询所有IP地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Getplclist()
        {
            try
            {
                List<PLCInfoModel> model = plcdb.Getplclist();
                string res = JsonConvert.SerializeObject(model);
                TDCommom.Logs.Info(string.Format("查询所有IP地址：返回数据：{0}\r\n", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询所有IP地址异常：异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }



        /// <summary>
        /// 修改连接状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UpdateConnect(string data)
        {
            string res = string.Empty;
            try
            {
                List<PLCInfoModel> list = TDCommom.ObjectExtensions.FromJsonString<List<PLCInfoModel>>(data);
                bool result = plcdb.UpdateConnect(list);
                
                if (result)
                {
                     res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultCode = 1
                    });
                }
                else
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultCode = 0
                    });
                }
                TDCommom.Logs.Info(string.Format("修改连接状态 接收数据：{0}\r\n  返回数据：{1}\r\n",data,res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询所有IP地址异常：接收数据:{1} \r\n 异常信息：{0}\r\n", ex.Message,data));
                return res;
            }
        }
    }
}
