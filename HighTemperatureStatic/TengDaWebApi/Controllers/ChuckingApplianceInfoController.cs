using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TDModel;
using TDModel.Product;

namespace TengDaWebApi.Controllers
{
    public class ChuckingApplianceInfoController : ApiController
    {
        TDDB.ChuckingApplianceInfoDB caidb = new TDDB.ChuckingApplianceInfoDB();
        /// <summary>
        /// 获取夹具全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetCaiAllData()
        {
            try
            {
                List<ChuckingApplianceInfoModel> modelData = caidb.GetAll().ToList();
                string res = JsonConvert.SerializeObject(modelData);
                TDCommom.Logs.Info(string.Format("查询所有的夹具表数据：返回数据：{0}\r\n", res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询所有的夹具表数据：异常信息：{0}\r\n", ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 通过状态找到相关的夹具信息
        /// </summary>
        /// <param name="data">序列化ApiModel数据</param>
        /// <returns></returns>
        [HttpGet]
        public string GetCaiDataByState(string data)
        {
            try
            {
                ApiModel apiModel = JsonConvert.DeserializeObject<ApiModel>(data);
                List<ChuckingApplianceInfoModel> modelData = caidb.GetCaiDataByState(apiModel.State);
                string res = JsonConvert.SerializeObject(modelData);
                TDCommom.Logs.Info(string.Format("通过状态找到相关的夹具信息： 接收数据：{0} \r\n ：返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("通过状态找到相关的夹具信息： 接收数据：{0} \r\n ：异常信息：{1}\r\n", data, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string UpdateFixtureInfo(string data)
        {
            try
            {
                ChuckingApplianceInfoModel model = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(data);
                ApiModel apimodel;
                if (model.CAState <= 30)
                {
                    int i = caidb.UpdateFixtureInfo(model.CAId, model.CAState, model.CellNumber);
                    apimodel = new ApiModel()
                    {
                        ResultCode = i
                    };
                }
                else if (model.CAState > 30 && model.CAState <= 50)
                {
                    int i = caidb.UpdateFixtureInfo(model.CAId, model.CAState, model.CellNumber, model.HTDId, model.InStoveTime);
                    apimodel = new ApiModel()
                    {
                        ResultCode = i
                    };
                }
                else if (model.CAState==int.Parse(TDCommon.SysEnum.FixtureState.ProcessEnd.ToString("d")))//夹具单次流程结束 添加到历史表
                {
                    int i = caidb.UpdateFixtureInfo(model.CAId, model.CAState, model.CellNumber, model.HTDId, model.InStoveTime, model.OutStoveTime);
                    int t = -1;
                    if (i > 0)
                    {
                        t= caidb.FixtureHandle(model.CAId);
                    }
                    apimodel = new ApiModel()
                    {
                        ResultCode = t
                    };
                }
                else
                {
                    int i = caidb.UpdateFixtureInfo(model.CAId, model.CAState, model.CellNumber, model.HTDId, model.InStoveTime, model.OutStoveTime);
                    apimodel = new ApiModel()
                    {
                        ResultCode = i
                    };
                }

                string res = TDCommom.ObjectExtensions.ToJsonString(apimodel);
                TDCommom.Logs.Info(string.Format("修改夹具状态：接收数据:{0} \r\n 返回数据:{1} \r\n ", data, res));
                return res;
            }
            catch (System.Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改夹具状态异常：接收数据:{0} \r\n 异常信息:{1} \r\n ", data, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateStateById(string data)
        {
            string res = string.Empty;

            try
            {
                ChuckingApplianceInfoModel model = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(data);
                ApiModel apimodel = new ApiModel();
                int i = caidb.UpdateStateById(model.CAId, model.CAState);
                apimodel = new ApiModel()
                {
                    ResultCode = i
                };
                TDCommom.Logs.Info(string.Format("修改夹具状态：接收数据：{0} \r\n 返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改夹具状态异常：接收数据：{0} \r\n 返回数据：{1} \r\n", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 添加实时夹具任务信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsertFirstInfo(string data)
        {
            string res = string.Empty;
            try
            {
                ChuckingApplianceInfoModel model = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(data);
                int i = caidb.InsertFirstInfo(model.CABarCode,model.FixPosition);
                ApiModel apimodel = new ApiModel()
                {
                    ResultCode = i
                };
                res = TDCommom.ObjectExtensions.ToJsonString(apimodel);
                TDCommom.Logs.Info(string.Format("添加A实时夹具任务信息：接收数据:{0} \r\n 返回数据:{1} \r\n ", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加A实时夹具任务信息异常：接收数据:{0} \r\n 异常信息:{1} \r\n ", data, ex.Message));
                return res;
            }
        }


        TDDB.ChuckingDB chuckDB = new TDDB.ChuckingDB();
        [HttpPost]
        public string InsertInfo(string data)
        {
            string res = string.Empty;
            try
            {
                ChuckingModel model = TDCommom.ObjectExtensions.FromJsonString<ChuckingModel>(data);
                int i = chuckDB.InsertInfo(model);
                ApiModel apimodel = new ApiModel()
                {
                    ResultCode = i
                };
                res = TDCommom.ObjectExtensions.ToJsonString(apimodel);
                TDCommom.Logs.Info(string.Format("添加A实时夹具任务信息：接收数据:{0} \r\n 返回数据:{1} \r\n ", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加A实时夹具任务信息异常：接收数据:{0} \r\n 异常信息:{1} \r\n ", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 查询入炉中夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetInstoveInfoByState(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<ChuckingApplianceInfoModel> list = caidb.GetInstoveInfoByState(apimodel.FixState,apimodel.Data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("查询入炉中夹具信息：接收参数：{0} \r\n  返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("查询入炉中夹具信息异常：接收参数：{0} \r\n  异常信息：{1}\r\n", data, ex.Message));
                return res;
            }
        }


        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateState(string data)
        {
            string res = string.Empty;
            try
            {
                ChuckingApplianceInfoModel model = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(data);
                int i = caidb.UpdateState(model);
                ApiModel apimodel = new ApiModel()
                {
                    ResultCode = i
                };
                res = TDCommom.ObjectExtensions.ToJsonString(apimodel);
                TDCommom.Logs.Info(string.Format("修改夹具状态：接收数据:{0} \r\n 返回数据:{1} \r\n ", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("修改夹具状态异常：接收数据:{0} \r\n 异常信息:{1} \r\n ", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 根据状态查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetInfoByState(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<ChuckingApplianceInfoModel> list = caidb.GetInfoByState(apimodel.State ,apimodel.Data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("根据状态查询夹具数据：接收参数：{0} \r\n  返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据状态查询夹具数据异常：接收参数：{0} \r\n  异常信息：{1}\r\n", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 获取出炉信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetOutStoveInfo(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                ChuckingApplianceInfoModel list = caidb.GetOutStoveInfo(apimodel.Data);
                if (list !=null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("获取出炉信息：接收参数：{0} \r\n  返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
               
                TDCommom.Logs.Error(string.Format("获取出炉信息异常：接收参数：{0} \r\n  异常信息：{1}\r\n", data, ex.Message));
                return res;
            }
        }


        /// <summary>
        /// 根据ID查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetChuckInfoByid(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                ChuckingApplianceInfoModel list = caidb.GetChuckInfoByid(apimodel.id);
                if (list != null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("根据ID查询夹具信息：接收参数：{0} \r\n  返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {

                TDCommom.Logs.Error(string.Format("根据ID查询夹具信息异常：接收参数：{0} \r\n  异常信息：{1}\r\n", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 修改备注值1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateChuckInfoRemake1(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                int i = caidb.UpdateChuckInfoRemake1(apimodel.id,apimodel.Data);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode=i
                });
                TDCommom.Logs.Info(string.Format("修改备注值1：接收参数：{0} \r\n  返回数据：{1}\r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {

                TDCommom.Logs.Error(string.Format("修改备注值1异常：接收参数：{0} \r\n  异常信息：{1}\r\n", data, ex.Message));
                return res;
            }
        }
    }
}