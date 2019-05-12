using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDModel;

namespace TengDaWebApi.Controllers
{
    public class RgvTaskController : ApiController
    {

        TDDB.RgvTaskDB db = new TDDB.RgvTaskDB();
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsertRgvTask(string data)
        {
            string res = string.Empty;
            try
            {
                RgvTaskModel model = TDCommom.ObjectExtensions.FromJsonString<RgvTaskModel>(data);
                int i = db.InsertRgvTask(model);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("添加RGV任务 ：接收数据：{0} \r\n 返回数据：{1}",data,res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加RGV任务异常 ：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateRgvTaskInfoByid(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                int i = db.UpdateRgvTaskInfoByid(model.id,model.State);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("修改任务状态 ：接收数据：{0} \r\n 返回数据：{1}", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改任务状态异常 ：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 修改任务步骤
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateTaskStepById(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                int i = db.UpdateTaskStepById(model.id, model.Type);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("修改任务步骤 ：接收数据：{0} \r\n 返回数据：{1}", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改任务步骤异常 ：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return res;
            }
        }

        

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetRgvTaskInfoByState(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<RgvTaskModel> list = db.GetRgvTaskInfoByState(model.FixState,model.FixPosition);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                
                TDCommom.Logs.Info(string.Format("查询RGV任务 ：接收数据：{0} \r\n 返回数据：{1}", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询RGV任务异常 ：接收数据：{0} \r\n 异常信息：{1}", data, ex.Message));
                return res;
            }
        }

        
    }
}
