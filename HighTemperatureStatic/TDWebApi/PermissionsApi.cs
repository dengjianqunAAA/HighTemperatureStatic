using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class PermissionsApi
    {
        TDWebApi.HttpClient http = new HttpClient();

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        public string GetMenuInfo()
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute+"Permissions/GetList");//发送Get请求
        }


        /// <summary>
        /// 修改菜单名称
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdatePermissions(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "Permissions/UpdatePermissions?data="+data+"",data);
        }



        /// <summary>
        /// 修改菜单排序
        /// </summary>
        /// <param name="data"></param>
        public void UpdatePermissionsToUpDown(string data)
        {
            http.Post(TDCommon.PublicInfo.WebAPIRoute + "Permissions/UpdatePermissionsToUpDown?data=" + data + "", data);
        }
    }
}
