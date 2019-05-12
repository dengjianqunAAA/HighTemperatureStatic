using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.Prodect;
using TDModel.RunCalendar;

namespace TengDaWebApi.Controllers
{
    public class ProduceDataController : ApiController
    {
        ProduceDataDB pdb = new ProduceDataDB();
        RunCalendarDB rdb = new RunCalendarDB();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetListInfo()
        {
            try
            {
                List<ProduceDataModel> list = pdb.GetListInfo();
                string data = JsonConvert.SerializeObject(list);
                TDCommom.Logs.Info(string.Format("产量查询：返回数据：{0}", data));
                return data;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("产量查询异常：错误信息：{0}", ex.Message));
                return null;
            }
            
        }

        /// <summary>
        /// 修改上料数量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateChargingCount()
        {
            try
            {
                int flag = pdb.UpdateChargingCount();
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag
                });
                TDCommom.Logs.Info(string.Format("修改上料数量：返回数据：{0}", res));
                return res;
            }
            catch (Exception ex)
            {

                TDCommom.Logs.Error(string.Format("修改上料数量异常：异常信息：{0}", ex.Message));
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = 0
                });
                return res;
            }
            
        }


        /// <summary>
        /// 修改下料数量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateBlankingCount()
        {
            try
            {
                int flag = pdb.UpdateBlankingCount();
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag
                });
                TDCommom.Logs.Info(string.Format("修改下料数量：返回数据：{0}", res));
                return res;
               
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改下料数量异常：异常信息：{0}", ex.Message));
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = 0
                });
                return res;
            }
            
        }

        /// <summary>
        /// 添加当天记录
        /// </summary>
        /// <returns></returns>
        public string InsertProduceData()
        {
            try
            {
                int flag = pdb.InsertProduceData();
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = flag
                });
                TDCommom.Logs.Info(string.Format("添加当天记录：返回数据：{0}", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加当天记录异常：异常信息：{0}", ex.Message));
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = 0
                });
                return res;
            }
        }

        /// <summary>
        /// 查询是否有当天记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetInfoByTime()
        {
            string res = "";
            try
            {
                
                List<ProduceDataModel> list = pdb.GetInfoByTime();
                if (list != null && list.Count > 0)
                {
                    res = JsonConvert.SerializeObject(new ApiModel()
                    {
                        ResultCode = 1
                    });
                    
                }
                else
                {
                    res = JsonConvert.SerializeObject(new ApiModel()
                    {
                        ResultCode = 0
                    });

                }
                TDCommom.Logs.Info(string.Format("查询是否有当天记录：返回数据：{0}", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询是否有当天记录异常：异常信息：{0}", ex.Message));
                res = JsonConvert.SerializeObject(new ApiModel()
                {
                    ResultCode = 0
                });
                return res;
            }
        }

        /// <summary>
        /// 根据日期查询产量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetProduceDataByDay(string data)
        {
            try
            {
                string res = string.Empty;
                List<ProduceDataModel> list = pdb.GetProduceDataByDay(data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("根据日期查询产量：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Info(string.Format("根据日期查询产量异常：接收数据：{0} \r\n 异常信息：{1} \r\n ", data, ex.Message));
                return null;
            }
            
        }
        
        /// <summary>
        /// 查询年日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetYearCalendar(string data)
        {
            try
            {
                string res = string.Empty;
                List<RunCalendarModel> list = rdb.GetYearCalendar(data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询年日志：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Info(string.Format("查询年日志异常：接收数据：{0} \r\n 异常信息：{1} \r\n ", data, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 查询月日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetMouthCalendar(string data)
        {
            try
            {
                string res = string.Empty;
                List<RunCalendarModel> list = rdb.GetMouthCalendar(data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询月日志：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Info(string.Format("查询月日志异常：接收数据：{0} \r\n 异常信息：{1} \r\n ", data, ex.Message));
                return null;
            }
        }


        /// <summary>
        /// 查询每日日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetDayCalendar(string data)
        {
            try
            {
                string res = string.Empty;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<RunCalendarModel> list = rdb.GetDayCalendar(apimodel.YearData,apimodel.MonthData);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询每日日志：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Info(string.Format("查询每日日志异常：接收数据：{0} \r\n 异常信息：{1} \r\n ", data, ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 查询每日工作时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetProduceRunByDay(string data)
        {
            try
            {
                string res = string.Empty;
                RunCalendarModel list = rdb.GetProduceRunByDay(data);
                if (list != null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询每日工作时间：接收数据：{0} \r\n 返回数据：{1} \r\n ", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Info(string.Format("查询每日工作时间异常：接收数据：{0} \r\n 异常信息：{1} \r\n ", data, ex.Message));
                return null;
            }
        }

        
    }
}
