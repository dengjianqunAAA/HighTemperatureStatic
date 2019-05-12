using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.Permissions;
using TengDaWebApi.Tools;

namespace TengDaWebApi.Controllers
{
    public class PermissionsController : ApiController
    {
        PermissionsDB db = new PermissionsDB();

        
        /// <summary>
        /// 查询全部菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetList()
        {
            try
            {
                List<PermissionsModel> res = db.GetList();
                string rec= JsonConvert.SerializeObject(res);
                TDCommom.Logs.Info(string.Format("查询所有菜单：返回数据：{0}\r\n", rec));
                return rec;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询菜单异常：异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }


        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="data">PermissionsModel实体类</param>
        /// <returns></returns>
        [HttpPost]
        public string UpdatePermissions(string data)
        {
            try
            {
                PermissionsModel model = JsonConvert.DeserializeObject<PermissionsModel>(data);
                bool flag = db.UpdatePermissions(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag ? 1 : 0
                });
                TDCommom.Logs.Info(string.Format("修改菜单成功：接收数据：{0}\r\n 返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改菜单异常：接收数据：{0}\r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }


        /// <summary>
        /// 菜单排序异常
        /// </summary>
        /// <param name="data"></param>
        [HttpPost]
        [NoPackageResult]
        public void UpdatePermissionsToUpDown(string data)
        {
            try
            {
                ApiModel apimodel = JsonConvert.DeserializeObject<ApiModel>(data);
                if (apimodel.ResultData != null)
                {
                    PermissionsModel permodel = JsonConvert.DeserializeObject<PermissionsModel>(apimodel.ResultData.ToString());
                     db.UpdatePermissionsToUpDown(permodel, apimodel.UpDown);
                }
                TDCommom.Logs.Info(string.Format("菜单排序:接收数据：{0} \r\n此方法无返回数据\r\n",data));
            }
            catch (Exception ex)
            {

                TDCommom.Logs.Error(string.Format("菜单排序异常：接收数据：{0}\r\n 异常信息：{1}\r\n", data, ex.Message));
            }
        }
    }
}
