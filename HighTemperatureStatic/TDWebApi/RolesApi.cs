using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.UserManage;

namespace TDWebApi
{
    public class RolesApi
    {

        TDWebApi.HttpClient http = new HttpClient();
        /// <summary>
        /// 查询全部权限信息
        /// </summary>
        /// <returns></returns>
        public string GetRolesList()
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute+ "Roles/GetRolesList");
        }

        /// <summary>
        /// 删除用户权限信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DeleteRolesById(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "Roles/DeleteRolesById?data="+data+"",data);
        }


        /// <summary>
        /// 修改用户权限信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateRolesById(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute+ "Roles/UpdateRolesById?data=" + data + "", data);
        }


        public string InsertRoles(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "Roles/InsertRoles?data=" + data + "", data);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        public async Task<string> Reset()
        {
          return await  client.GetStringAsync(TDCommon.PublicInfo.WebAPIRoute + "Roles/GetRolesList");
        }


        //public HttpResponseMessage Reset()
        //{
        //    string url = TDCommon.PublicInfo.WebAPIRoute + "Roles/GetRolesList";
        //    HttpResponseMessage response = null;
        //    client.GetAsync(url).ContinueWith(
        //     (requestTask) =>
        //     {
        //         response = requestTask.Result;

        //     }).Wait(60000);
        //    return response;
        //    //return await  client.GetStringAsync(TDCommon.PublicInfo.WebAPIRoute + "Roles/GetRolesList");
        //}
    }
}
