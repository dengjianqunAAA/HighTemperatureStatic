using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDModel;

namespace TDCommon.PLCSocket
{
    public class ThreadTest
    {
        public void StartThread()
        {
           Thread thread= new Thread(new ThreadStart(() =>
            {

                while (true)
                {
                    try
                    {
                       
                    }
                    catch (Exception ex)
                    {
                       
                    }
                }
            }));
            thread.IsBackground = true;
            thread.Start();



            //Thread thlogin = new Thread(new ThreadStart(LoginTs));
            //thlogin.IsBackground = true;
            //thlogin.Start();

            //Thread thGetList = new Thread(new ThreadStart(GetRoleListTs));
            //thGetList.IsBackground = true;
            //thGetList.Start();

            //Thread thUpdateCount = new Thread(new ThreadStart(UpdateCountTs));
            //thUpdateCount.IsBackground = true;
            //thUpdateCount.Start();

        }

        //TDWebApi.UserInfoApi userapi = new TDWebApi.UserInfoApi();
        //TDWebApi.RolesApi roleapi = new TDWebApi.RolesApi();
        //TDWebApi.ProduceDataApi proapi = new TDWebApi.ProduceDataApi();
    //    void LoginTs()
    //    {
    //        while (true)
    //        {
    //            try
    //            {
    //                string data = SerializableHelper<UsersModel>.Serialize(new UsersModel { UserName = "admin ", UserPwd = "admin" });//序列化参数
    //                string res = userapi.Login(data);
    //                TDCommom.Logs.Trace(string.Format("登陆:{0}\r\n", res));
    //                Thread.Sleep(1000);
    //            }
    //            catch (Exception ex)
    //            {
    //                TDCommom.Logs.Error(string.Format("测试登陆异常\r\n"));
    //                Thread.Sleep(1000);
    //            }
    //        }
            
    //    }

    //    void GetRoleListTs()
    //    {
    //        while (true)
    //        {
    //            try
    //            {
    //                string res = roleapi.GetRolesList();//发送Get请求
    //                TDCommom.Logs.Trace(string.Format("查询权限：{0}\r\n", res));
    //                Thread.Sleep(1000);
    //            }
    //            catch (Exception)
    //            {

    //                TDCommom.Logs.Error(string.Format("测试查询权限异常\r\n"));
    //                Thread.Sleep(1000);
    //            }
    //        }
    //    }


    //    void UpdateCountTs()
    //    {
    //        while (true)
    //        {
    //            try
    //            {
    //                string res = proapi.UpdateChargingCount();//发送Post请求
    //                TDCommom.Logs.Trace(string.Format("修改数量：{0}\r\n", res));
    //                Thread.Sleep(1000);
    //            }
    //            catch (Exception)
    //            {
    //                TDCommom.Logs.Error(string.Format("测试修改数量异常\r\n"));
    //                Thread.Sleep(1000);
    //            }
    //        }
    //    }
    }
}
