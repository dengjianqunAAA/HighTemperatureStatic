using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class View_CellInfioApi
    {
        HttpClient http = new HttpClient();

        public string GetViewsCellInfo(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "View_CellInfo/GetViewsCellInfo?data=" + data + "");
        }
    }
}
