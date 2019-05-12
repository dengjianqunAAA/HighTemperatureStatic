using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TDCommon;
using TDModel;

namespace TDWebApi
{
    public class UserInfoApi
    {
        TDWebApi.HttpClient http = new HttpClient();
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Login(string data)
        {
            string res= http.Get(TDCommon.PublicInfo.WebAPIRoute + "UserInfo/Login?data=" + data + "");
            return res;
        }
        /// <summary>
        /// 更新用户信息请求
        /// </summary>
        /// <param name="data">序列化UsersModel数据</param>
        /// <returns></returns>
        public string UpdateUserInfo(string data)
        {
            string res = http.Post(TDCommon.PublicInfo.WebAPIRoute + "UserInfo/UpdateUserInfo?data=" + data + "", data);//发送POST请求
            return res;
        }
        /// <summary>
        /// 用户数据分页请求
        /// </summary>
        /// <param name="data">序列化ApiModel数据</param>
        /// <returns></returns>
        public string GetPageList(string data)
        {
            string res = http.Get(TDCommon.PublicInfo.WebAPIRoute + "UserInfo/GetPageList?data="  +data +"");
            return res;
        }


        /// <summary>
        /// 删除用户信息请求
        /// </summary>
        /// <param name="data">序列化UsersModel数据</param>
        /// <returns></returns>
        public string DeleteUserInfo(string data)
        {
            string res = http.Post(TDCommon.PublicInfo.WebAPIRoute + "UserInfo/DeleteUserInfo?data=" + data + "", data);//发送POST请求
            return res;
        }
        /// <summary>
        /// 插入用户信息请求
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string InsertUserInfo(string data)
        {
            string res = http.Post(TDCommon.PublicInfo.WebAPIRoute + "UserInfo/InsertUserInfo?data=" + data + "", data);//发送POST请求
            return res;
        }
    }
}
