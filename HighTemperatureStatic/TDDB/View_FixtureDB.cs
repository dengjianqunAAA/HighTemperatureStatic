using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Product.Views;
using TengDa.DB.Base;

namespace TDDB
{
    public class View_FixtureDB : RepositoryBase<View_GetFixtureInfoModel>
    {
       /// <summary>
       /// 根据状态查找夹具信息 
       /// </summary>
       /// <param name="state"></param>
       /// <returns></returns>
        public List<View_GetFixtureInfoModel> GetViewsFixtureInfoByState(int pageIndex, int pageSize, out long allRowsCount, int state ,DateTime startTime,DateTime endTime,string TableName)
        {
            string sql = string.Format("select * from  {3} where castate={0} and  FixScanTime>='{1}' and FixScanTime<'{2}' ", state,startTime,endTime, TableName);
            
            List<View_GetFixtureInfoModel> list = this.GetPage(pageIndex,pageSize,out  allRowsCount,sql).ToList();
            return list;
        }

        /// <summary>
        /// 根据夹具条码查询夹具信息
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<View_GetFixtureInfoModel> GetViewsFixtureInfoByFixName(int pageIndex, int pageSize, out long allRowsCount, string  cabarcode, DateTime startTime, DateTime endTime) 
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(cabarcode))
            {
                sql = string.Format(@"select   * from ( SELECT * FROM dbo.View_GetFixtureInfo WHERE CABarCode = '{0}' AND FixScanTime >='{1}' AND FixScanTime<'{2}'
                  UNION ALL 
                  SELECT * FROM dbo.View_GetHistoryFixtureInfo  WHERE CABarCode = '{0}'  AND FixScanTime >='{1}' AND FixScanTime<'{2}') t ", cabarcode, startTime, endTime);
            }
            else
            {
                sql = string.Format(@"SELECT  * FROM  ( SELECT * FROM dbo.View_GetFixtureInfo WHERE  FixScanTime >='{0}' AND FixScanTime<'{1}'
                                      UNION ALL 
                                      SELECT * FROM dbo.View_GetHistoryFixtureInfo  WHERE  FixScanTime >='{0}' AND FixScanTime<'{1}') t  ",  startTime, endTime);
            }
            List<View_GetFixtureInfoModel> list = this.GetPage(pageIndex, pageSize, out allRowsCount, sql).ToList();
            return list;
        }
    }
}