using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class ProductApi
    {
        HttpClient http = new HttpClient();

        /// <summary>
        /// 查询产能报表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetCuveInfo(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "Product/GetCuveInfo?data="+data+"");
        }
    }
}
