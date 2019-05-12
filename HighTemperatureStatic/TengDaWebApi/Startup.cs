using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TengDaWebApi.Startup))]

namespace TengDaWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //TDCommon.SysInfo.ShiftTimeE = "19:30";
            //TDCommon.SysInfo.ShiftTimeM = "7:30";
        }
    }
}
