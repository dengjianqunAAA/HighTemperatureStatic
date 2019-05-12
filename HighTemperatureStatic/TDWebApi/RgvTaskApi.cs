using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class RgvTaskApi
    {
        HttpClient http = new HttpClient();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string InsertRgvTask(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "RgvTask/InsertRgvTask?data=" + data + "", data);
        }
        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateRgvTaskInfoByid(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "RgvTask/UpdateRgvTaskInfoByid?data=" + data + "", data);
        }

        /// <summary>
        /// 修改任务步骤
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateTaskStepById(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "RgvTask/UpdateTaskStepById?data=" + data + "", data);
        }

        

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetRgvTaskInfoByState(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "RgvTask/GetRgvTaskInfoByState?data=" + data + "");
        }
    }
}
