using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TDModel.HT;
using TengDa.DB.Base;

namespace TDDB.HT
{
    public class HighTemperatureDetailDB : RepositoryBase<HighTemperatureDetailModel>
    {
        /// <summary>
        /// 根据高温静置主表ID在明细表内的数据
        /// </summary>
        /// <param name="thmId">高温静置主表id</param>
        /// <returns></returns>
        public List<HighTemperatureDetailModel> SelectThdData(int thmId)
        {
            string sql = string.Format(@"SELECT * FROM dbo.HighTemperatureDetail WHERE HTMId={0}", thmId);
            return this.Get(sql).ToList();
        }
        /// <summary>
        /// 更新高温静置明细表
        /// </summary>
        /// <param name="data">序列化HighTemperatureDetailModel</param>
        /// <returns></returns>
        public int UpdateHtdData(HighTemperatureDetailModel htd)
        {

            //string sql = string.Format(@"UPDATE dbo.HighTemperatureDetail 
            //    SET  CAId = {0},HTDState={1}, RgvPositionCode={2},HTDNumber={3},HTDLayer={4}
            //       {5} {6} Where HTDId={7}", htd.CAId, htd.HTDState, htd.RgvPositionCode, htd.HTDNumber, htd.HTDLayer, strTime1, strTime2, htd.HTDId);
            return -1;
        }
        /// <summary>
        /// 更新高温静置明细表状态
        /// </summary>
        /// <param name="apiModel"></param>
        /// <returns></returns>
        public int UpdateHtdState(ApiModel apiModel)
        {
            string sql = string.Format(@"EXEC dbo.UpdateHtdState  {0},{1}", apiModel.HTDId, apiModel.State);
            return this.Execute(sql);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public List<HighTemperatureDetailModel> GetDetailInfoList()
        {
            string sql = "SELECT TOP 132 * FROM  dbo.HighTemperatureDetail";
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 获取入炉门号
        /// </summary>
        /// <param name="Htype">1左2右</param>
        /// <param name=""></param>
        /// <returns></returns>
        public HighTemperatureDetailModel GetInstoveSimcard(int Htype, string proNO, int remake)
        {
            string sql = string.Empty;
            if (proNO == "空夹具")
            {
                sql = string.Format(@"SELECT h2.* FROM   (
                SELECT H1.* FROM dbo.HighTemperatureMain AS H1
                LEFT OUTER JOIN dbo.PlcInfo AS P1 ON P1.PIId = H1.PIId  
                LEFT OUTER JOIN dbo.ProductModel AS pro ON h1.productModelId = pro.ProductModelID  WHERE P1.PIIsConnect = 1  AND H1.HTMCreateType = '{0}' AND 
                ProductName='{1}' AND h1.remake='{2}') AS T1  
                LEFT OUTER JOIN dbo.HighTemperatureDetail AS H2 ON H2.HTMId = T1.HTMId WHERE H2.HTDState = 20", Htype, proNO, remake);
            }
            else
            {
                sql = string.Format(@"SELECT h2.* FROM   (
                SELECT H1.* FROM dbo.HighTemperatureMain AS H1
                LEFT OUTER JOIN dbo.PlcInfo AS P1 ON P1.PIId = H1.PIId  
                LEFT OUTER JOIN dbo.ProductModel AS pro ON h1.productModelId = pro.ProductModelID  WHERE P1.PIIsConnect = 1  AND H1.HTMCreateType = '{0}' AND h1.ProductModelId= '{1}' AND h1.remake='{2}') AS T1  
                LEFT OUTER JOIN dbo.HighTemperatureDetail AS H2 ON H2.HTMId = T1.HTMId WHERE H2.HTDState = 20", Htype, int.Parse(proNO), remake);
            }

            return this.Get(sql).FirstOrDefault();
        }
    }
}
