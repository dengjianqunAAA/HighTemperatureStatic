using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class FixtrueControlApi
    {
        HttpClient http = new HttpClient();


        public string GetListInfoByCode(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "FixtureControl/GetListInfoByCode?data=" + data + "");
        }


        /// <summary>
        /// 修改夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateFinxtureById(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "FixtureControl/UpdateFinxtureById?data=" + data + "", data);
        }

        /// <summary>
        /// 添加夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string InsertFixture(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "FixtureControl/InsertFixture?data=" + data + "", data);
        }

        /// <summary>
        /// 查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetFixtureInfoByCode(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "FixtureControl/GetFixtureInfoByCode?data=" + data + "");
        }
    }
}
