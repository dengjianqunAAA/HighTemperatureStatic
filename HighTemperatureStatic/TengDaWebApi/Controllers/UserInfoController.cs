using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Filters;
using TDModel;
using TDModel.UserManage;
using TengDa.DB;
using TengDaWebApi.Models;
using TengDaWebApi.Tools;

namespace TengDaWebApi.Controllers
{
    public class UserInfoController : ApiController
    {

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        [HttpGet]
        public string Login(string data)
        {

            try
            {
                UsersModel model = JsonConvert.DeserializeObject<UsersModel>(data);
                List<UsersModel> res1 = TDDB.UsersModelDB.usersModelDB.Login(model.UserName, model.UserPwd);
                string res = JsonConvert.SerializeObject(res1);
                TDCommom.Logs.Info(string.Format("登陆接收数据：{0} \r\n 登陆返回数据：{1}\r\n", data, res));
                return res;
             }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("登陆异常：接收数据:{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }

        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="data">序列化UsersModel数据</param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateUserInfo(string data)
        {
            try
            {
                UsersModel model = JsonConvert.DeserializeObject<UsersModel>(data);
                bool flag = TDDB.UsersModelDB.usersModelDB.Update(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag ? 1 : 0
                });
                TDCommom.Logs.Info(string.Format(" 更新用户信息接收数据：{0}  \r\n 更新用户信息返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("更新用户信息异常：接收数据:{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 用户分页
        /// </summary>
        /// <param name="data">序列化ApiModel数据</param>
        /// <returns></returns>
        [HttpGet]
        public string GetPageList(string data)
        {
            try
            {
                ApiModel model = JsonConvert.DeserializeObject<ApiModel>(data);
                long AllRowsCount = model.AllRowsCount;
                List<UsersModel> res1 = TDDB.UsersModelDB.usersModelDB.PageList(model.PageIndex, model.PageSize, out AllRowsCount);
                string apiInfo = JsonConvert.SerializeObject(new ApiModel()
                {
                    AllRowsCount = AllRowsCount,
                    ResultData = res1
                });
                TDCommom.Logs.Info(string.Format(" 查询所有分页数据接收数据：{0}  \r\n 查询所有分页返回数据：{1}\r\n", data, apiInfo));
                return apiInfo;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询所有分页数据接收数据：{0}  \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="data">序列化UsersModel数据</param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteUserInfo(string data)
        {
            try
            {
                UsersModel model = JsonConvert.DeserializeObject<UsersModel>(data);
                int number = TDDB.UsersModelDB.usersModelDB.Delete(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = number
                });
                TDCommom.Logs.Info(string.Format(" 删除用户信息接收数据：{0}  \r\n 删除用户信息返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("删除用户信息异常：接收数据:{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="data">序列化UsersModel数据</param>
        /// <returns></returns>
        [HttpPost]
        public string InsertUserInfo(string data)
        {
            try
            {
                UsersModel model = JsonConvert.DeserializeObject<UsersModel>(data);
                int number = TDDB.UsersModelDB.usersModelDB.InsertUserInfo(model);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = number
                });
                TDCommom.Logs.Info(string.Format(" 添加用户信息接收数据：{0}  \r\n 插入用户信息返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加用户信息异常：接收数据:{0} \r\n 异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }
    }
}
