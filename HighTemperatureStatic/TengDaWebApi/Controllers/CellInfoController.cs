using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDModel;
using TDModel.Product;

namespace TengDaWebApi.Controllers
{
    public class CellInfoController : ApiController
    {

        TDDB.CellInfoDB cellDB = new TDDB.CellInfoDB();

        /// <summary>
        /// 添加电芯记录 
        /// </summary>
        /// <param name="data">电芯条码</param>
        /// <returns></returns>
        [HttpPost]
        public string InsertCellCount(string data)
        {
            string res = string.Empty;
            try
            {
                CellInfoModel model = TDCommom.ObjectExtensions.FromJsonString<CellInfoModel>(data);
                int i = cellDB.InsertCellCount(model);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("添加电芯记录：接收数据：{0}\r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加电芯记录异常：接收数据：{0}\r\n 异常信息：{1} \r\n", data, ex.Message));
                return res;
            }
        }


        /// <summary>
        /// 修改电芯状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateCellState(string data)
        {
            string res = string.Empty;
            try
            {
                CellInfoModel model = TDCommom.ObjectExtensions.FromJsonString<CellInfoModel>(data);
                int i = cellDB.UpdateCellState(model);
                res = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    ResultCode = i
                });
                TDCommom.Logs.Info(string.Format("添加电芯记录：接收数据：{0}\r\n 返回数据：{1} \r\n", data, res));
                return res;
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("添加电芯记录异常：接收数据：{0}\r\n 异常信息：{1} \r\n", data, ex.Message));
                return res;
            }
        }

        /// <summary>
        /// 根据状态查找电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetCellInfoBystate(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<CellInfoModel> list = cellDB.GetCellInfoBystate(model.Data);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("根据状态查找电芯信息：接收参数：{0}\r\\n 返回参数：{1} \r\n",data,res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据状态查找电芯信息异常：接收参数：{0}\r\\n 异常信息：{1} \r\n", data, ex.Message));
                return res;
            }
        }
        /// <summary>
        /// 根据夹具ID查找电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetCellInfoByCaId(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                List<CellInfoModel> list = cellDB.GetCellInfoByCaId(model.id);
                if (list.Count > 0)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(list);
                }
                TDCommom.Logs.Info(string.Format("根据电芯ID查找电芯信息：接收参数：{0}\r\\n 返回参数：{1} \r\n", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据电芯ID查找电芯信息异常：接收参数：{0}\r\\n 异常信息：{1} \r\n", data, ex.Message));
                return res;
            }
        }
        /// <summary>
        /// 根据夹具ID查找电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetCellInfoByCode(string data)
        {
            string res = string.Empty;
            try
            {
                ApiModel model = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(data);
                CellInfoModel cellinfo = cellDB.GetCellInfoByCode(model.Data);
                if (cellinfo!= null)
                {
                    res = TDCommom.ObjectExtensions.ToJsonString(cellinfo);
                }
                TDCommom.Logs.Info(string.Format("根据电芯条码查找电芯信息：接收参数：{0}\r\\n 返回参数：{1} \r\n", data, res));
                return res;

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("根据电芯条码查找电芯信息异常：接收参数：{0}\r\\n 异常信息：{1} \r\n", data, ex.Message));
                return res;
            }
        }
        

    }

}
