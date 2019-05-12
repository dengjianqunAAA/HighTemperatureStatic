using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class PLCInfoApi
    {
        HttpClient http = new HttpClient();

        public string Getplclist()
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "PLCInfo/Getplclist");
        }



        public string UpdateConnect(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "PLCInfo/UpdateConnect?data=" + data , data);
        }
    }
}
