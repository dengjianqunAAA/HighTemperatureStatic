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
    public class HighTemperatureMainDB : RepositoryBase<HighTemperatureMainModel>
    {
        /// <summary>
        /// 根据高温炉创建类型获取高温炉信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<HighTemperatureMainModel> GetHtmAllDataByType(int type)
        {
            string sql = string.Format(@"SELECT HTMId,HTMState,HTMNumber,HTMName,HTMCountLayer,HTMCreateType,HTMBerthPosition,HTMRowSort
 FROM dbo.HighTemperatureMain WHERE HTMCreateType={0}", type+1);
            return this.Get(sql).ToList();

        }

        /// <summary>
        /// 根据高温炉ID修改温度
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="htmId"></param>
        /// <returns></returns>
        public int SetTemp(float temp,int htmId) {
            string sql = string.Format(@"update [HighTemperatureMain]  Set [SetTemperature] = {0}  where  htmId = {1}", temp, htmId);
            return this.Execute(sql);
        }

    }
}
