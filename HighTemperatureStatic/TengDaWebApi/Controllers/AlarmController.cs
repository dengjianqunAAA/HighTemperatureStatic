using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDDB;
using TDModel;
using TDModel.Alarm.Views;

namespace TengDaWebApi.Controllers
{
    public class AlarmController : ApiController
    {
        AlarmHistoryViewDB historyViewDB = new AlarmHistoryViewDB();


        /// <summary>
        /// 分页查询历史报警（报警时间）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewsByTime(string data)
        {
            try
            {
                ApiModel apimodel = JsonConvert.DeserializeObject<ApiModel>(data);
                long AllRowsCount = apimodel.AllRowsCount;
                List<View_GetHistoryAlarmModel> list = historyViewDB.GetViewsByTime(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.StartTime, apimodel.EndTime);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    AllRowsCount = AllRowsCount,
                    ResultData = list
                });
                TDCommom.Logs.Info(string.Format("分页查询历史报警（报警时间）：接收数据：{0}\r\n 返回数据：{1}  \r\n",data,res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("分页查询历史报警（报警时间）异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }
            
        }


        /// <summary>
        /// 分页查询历史报警（报警内容）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewsByTimeandContent(string data)
        {
            try
            {
                ApiModel apimodel = JsonConvert.DeserializeObject<ApiModel>(data);
                long AllRowsCount = apimodel.AllRowsCount;
                View_GetHistoryAlarmModel model = new View_GetHistoryAlarmModel();
                if (apimodel.ResultData != null)
                {
                    model = JsonConvert.DeserializeObject<View_GetHistoryAlarmModel>(apimodel.ResultData.ToString());
                }
                List<View_GetHistoryAlarmModel> list = historyViewDB.GetViewsByTimeandContent(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.StartTime, apimodel.EndTime, model.AlarmContent);
                string res = JsonConvert.SerializeObject(new ApiModel()
                {
                    AllRowsCount = AllRowsCount,
                    ResultData = list
                });
                TDCommom.Logs.Info(string.Format("分页查询历史报警（报警内容）：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("分页查询历史报警（报警内容）异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }
            
        }

        /// <summary>
        /// 报警类型统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetViewHistory(string data)
        {
            try
            {
                string res = "";
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<View_GetHistoryAlarmModel> list = historyViewDB.GetViewHistory(apimodel.StartTime, apimodel.EndTime);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("报警类型统计：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;
                
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("报警类型统计异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }
            
        }

        /// <summary>
        /// 报警内容统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetHistoryAlarmChartData(string data)
        {
            try
            {
                string res = "";
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<View_GetHistoryAlarmModel> list = historyViewDB.GetHistoryAlarmChartData(apimodel.StartTime, apimodel.EndTime);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("报警内容统计：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("报警内容统计异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null; 
            }
            
        }



        /// <summary>
        /// 报警次数统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetLoadCraftChartData(string data)
        {
            try
            {
                string res = "";
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<View_GetHistoryAlarmModel> list = historyViewDB.GetLoadCraftChartData(apimodel.StartTime, apimodel.EndTime);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("报警次数统计：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("报警次数统计异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }
            
        }


        /// <summary>
        /// Excel数据导出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetListToExcel(string data)
        {
            try
            {
                string res = string.Empty;
                ApiModel apimodel= TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<View_GetHistoryAlarmModel> list = new List<View_GetHistoryAlarmModel>();
                if (apimodel.ResultData != null)
                {
                    View_GetHistoryAlarmModel model = TDCommom.ObjectExtensions.FromJsonString<View_GetHistoryAlarmModel>(apimodel.ResultData.ToString());
                    list = historyViewDB.GetListToExcel(apimodel.StartTime, apimodel.EndTime, model.AlarmContent);
                }
                else
                {
                    list = historyViewDB.GetListToExcel(apimodel.StartTime, apimodel.EndTime, "");
                }
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("Excel数据导出：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("Excel数据导出异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }

        }

        /// <summary>
        /// 报警数据统计
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetHistoryAlarmCountPage(string data)
        {
            try
            {
                string res = string.Empty;
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);//参数序列反序列化
                List<View_GetHistoryAlarmModel> list = new List<View_GetHistoryAlarmModel>();
                long AllRowsCount;
                if (apimodel.ResultData != null)
                {
                    View_GetHistoryAlarmModel model = TDCommom.ObjectExtensions.FromJsonString<View_GetHistoryAlarmModel>(apimodel.ResultData.ToString());
                    list = historyViewDB.GetHistoryAlarmCountPage(apimodel.PageIndex,apimodel.PageSize, out AllRowsCount, apimodel.StartTime, apimodel.EndTime, model.AlarmContent);
                    //list = historyViewDB.GetHistoryAlarmCountPage(apimodel.StartTime, apimodel.EndTime, model.AlarmContent);
                }
                else
                {
                    list = historyViewDB.GetHistoryAlarmCountPage(apimodel.PageIndex, apimodel.PageSize, out AllRowsCount, apimodel.StartTime, apimodel.EndTime,"");
                    //list = historyViewDB.GetHistoryAlarmCountPage(apimodel.StartTime, apimodel.EndTime, "");
                }
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel() {
                        ResultData=list,
                        AllRowsCount=AllRowsCount
                    });
                }
                TDCommom.Logs.Info(string.Format("报警数据统计 ：接收数据：{0}\r\n 返回数据 ：{1}  \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("报警数据统计异常：接收数据：{0}\r\n 异常信息 ：{1}  \r\n", data, ex.Message));
                return null;
            }
        }


        TempAlarmDB tempdb = new TempAlarmDB();

        /// <summary>
        /// 实时报警数据查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetTempAlarmList(string data)
        {
            string res = string.Empty;
            long AllRowsCount;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);

                List<View_GetTemporariesAlarmModel> list= tempdb.GetTempAlarmList(apimodel.PageIndex,apimodel.PageSize,out AllRowsCount);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    {
                        ResultData = list,
                        AllRowsCount = AllRowsCount
                    });
                }
                TDCommom.Logs.Info(string.Format("实时报警数据查询:无接收数据\r\n 返回数据：{0}\r\n",res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("实时报警数据查询异常：无接收数据\r\n 异常信息：{0}\r\n", ex.Message));
                return res;
            }
            
        }
    }
}
