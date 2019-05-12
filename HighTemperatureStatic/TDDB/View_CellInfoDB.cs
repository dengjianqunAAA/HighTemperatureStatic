using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Product.Views;
using TengDa.DB.Base;

namespace TDDB
{
    
    public class View_CellInfoDB : RepositoryBase<View_GetCellInfoModel>
    {
        /// <summary>
        /// 根据条码查询电芯信息
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<View_GetCellInfoModel> GetViewsCellInfo(int pageIndex, int pageSize, out long allRowsCount, string barcode, DateTime startTime, DateTime endTime)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(barcode))
            {
                sql = string.Format(@"SELECT * FROM   (
                    SELECT * FROM dbo.View_GetCellInfo WHERE CellScanTime > '{0}'  AND  CellScanTime < '{1}' UNION ALL 
                    SELECT * FROM dbo.View_GetCellInfoHistory WHERE CellScanTime > '{0}'  AND  CellScanTime < '{1}')  t ", startTime, endTime);

            }
            else
            {
                sql = string.Format(@"SELECT * FROM   (
                    SELECT * FROM dbo.View_GetCellInfo WHERE CellScanTime > '{1}'  AND  CellScanTime < '{2}' AND BarCode='{0}' UNION ALL
                    SELECT * FROM dbo.View_GetCellInfoHistory WHERE CellScanTime > '{1}'  AND  CellScanTime < '{2}' AND BarCode='{0}' )  t  ", barcode, startTime, endTime);

            }
            List<View_GetCellInfoModel> list = this.GetPage(pageIndex, pageSize, out allRowsCount, sql).ToList();
            return list;
        }
    }
}
