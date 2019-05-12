using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.HT;
using TengDa.DB.Base;

namespace TDDB.HT
{
 public  class HtmAndHtdDB : RepositoryBase<HtmAndHtdModel>
    {
        /// <summary>
        /// 根据条件找到高温主表跟高温明细表跟夹具表关联数据所有数据
        /// </summary>
        /// <returns></returns>
        public List<HtmAndHtdModel> HtmRelevanceAllDataByThdId(int htmId)
        {
            string sql = string.Format(@"SELECT htm.HTMId,htm.HTMNumber,htm.HTMName,htm.HTMCountLayer,htm.HTMBerthPosition,HTM.HTMRowSort, 
htd.* FROM dbo.HighTemperatureDetail htd LEFT JOIN dbo.HighTemperatureMain htm ON
 htm.HTMId = htd.HTMId  WHERE htm.HTMId={0} ORDER BY htd.HTDNumber ", htmId);
            return this.Get(sql).ToList();
        }
    }
}
