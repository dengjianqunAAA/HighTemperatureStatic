using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class View_FixtureApi
    {
        HttpClient http = new HttpClient();

        public string GetViewsFixtureInfoByState(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "View_Fixture/GetViewsFixtureInfoByState?data="+data+"");
        }


        public string GetViewsFixtureInfoByFixName(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "View_Fixture/GetViewsFixtureInfoByFixName?data=" + data + "");
        }
    }
}
