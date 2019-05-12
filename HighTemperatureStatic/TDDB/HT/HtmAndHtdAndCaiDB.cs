using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.HT;
using TengDa.DB.Base;

namespace TDDB.HT
{
   public  class HtmAndHtdAndCaiDB : RepositoryBase<HtmAndHtdAndCaiModel>
    {
        /// <summary>
        /// 高温主表跟高温明细表跟夹具表关联数据所有数据
        /// </summary>
        /// <returns></returns>
        public List<HtmAndHtdAndCaiModel> GetRelevanceAllData()
        {
            string sql = string.Format(@"SELECT htm.HTMId,htm.HTMNumber,htm.HTMName,htm.HTMCountLayer,htm.HTMBerthPosition,HTM.HTMRowSort,
htd.* ,cai.* FROM dbo.HighTemperatureDetail htd LEFT JOIN  dbo.HighTemperatureMain htm 
ON htd.HTMId=htm.HTMId  LEFT JOIN  dbo.ChuckingApplianceInfo cai ON cai.HTDId = htd.HTDId");
            return this.Get(sql).ToList();
        }
        /// <summary>
        /// 根据条件找到高温主表跟高温明细表跟夹具表关联数据所有数据
        /// </summary>
        /// <returns></returns>
        public List<HtmAndHtdAndCaiModel> HtmRelevanceAllDataByThdId(int htmId)
        {
            string sql = string.Format(@"SELECT htm.HTMId,htm.HTMNumber,htm.HTMName,htm.HTMCountLayer,htm.HTMBerthPosition,HTM.HTMRowSort,
htd.* ,cai.* FROM dbo.HighTemperatureDetail htd LEFT JOIN  dbo.HighTemperatureMain htm 
ON htd.HTMId=htm.HTMId  where htd.HTMId={0} 
order by htd.HTDNumber", htmId);
            return this.Get(sql).ToList();
        }
    }
}
