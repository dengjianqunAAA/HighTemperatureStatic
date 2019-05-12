using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class CellInfoApi
    {
        HttpClient http = new HttpClient();
        /// <summary>
        /// 添加电芯记录
        /// </summary>
        /// <param name="data">电芯条码</param>
        /// <returns></returns>
        public string InsertCellCount(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "CellInfo/InsertCellCount?data="+data, data);
        }

        /// <summary>
        /// 修改电芯状态
        /// </summary>
        /// <param name="data">model</param>
        /// <returns></returns>
        public string UpdateCellState(string data)
        {
            return http.Post(TDCommon.PublicInfo.WebAPIRoute + "CellInfo/UpdateCellState?data=" + data, data);
        }


        public string GetCellInfoBystate(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "CellInfo/GetCellInfoBystate?data=" + data);
        }

        /// <summary>
        /// 根据夹具ID查找电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetCellInfoByCaId(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "CellInfo/GetCellInfoByCaId?data=" + data);
        }

        /// 根据夹具ID查找电芯信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetCellInfoByCode(string data)
        {
            return http.Get(TDCommon.PublicInfo.WebAPIRoute + "CellInfo/GetCellInfoByCode?data=" + data);
        }
    }
}
