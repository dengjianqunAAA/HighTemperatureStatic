using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.UserManage;

namespace TengDaWebApi.Controllers
{
    public class RolesController : ApiController
    {

        /// <summary>
        /// 查询全部权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetRolesList()
        {
            try
            {
                
                List<RolesModel> list = TDDB.RolesDB.rolesDB.GetRolesList();
                string res = JsonConvert.SerializeObject(list);
                TDCommom.Logs.Info(string.Format("权限查询返回数据：{0}\r\n", res));

                return res;
                
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("权限查询异常：ErrorMsg:{0}\r\n", ex.Message));
                return null;
            }     
            
        }

        /// <summary>
        /// 删除用户权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteRolesById(string data)
        {
            try
            {
                RolesModel model = JsonConvert.DeserializeObject<RolesModel>(data);
                int i = TDDB.RolesDB.rolesDB.Delete(model.RoleId);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info( string.Format("删除用户权限： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("删除用户权限异常：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }


        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateRolesById(string data)
        {
            try
            {
                RolesModel model = JsonConvert.DeserializeObject<RolesModel>(data);
                bool flag = TDDB.RolesDB.rolesDB.Update(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag ? 1 : 0
                });
                TDCommom.Logs.Info(string.Format("修改用户权限： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改用户权限异常：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
            
        }


        /// <summary>
        /// 增加一个用户权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsertRoles(string data)
        {
            try
            {
                RolesModel model = JsonConvert.DeserializeObject<RolesModel>(data);
                int i = TDDB.RolesDB.rolesDB.Insert(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("增加用户权限： 接收数据：{0} \r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("增加用户权限：接收数据：{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
    }
}
