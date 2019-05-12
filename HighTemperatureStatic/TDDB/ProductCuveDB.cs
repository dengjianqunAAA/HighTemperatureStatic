using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Product;
using TengDa.DB.Base;

namespace TDDB
{
    public class ProductCuveDB : RepositoryBase<ProductionCuveModel>
    {
        /// <summary>
        /// 产能统计报表
        /// </summary>
        /// <param name="ProductModel">产品型号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public  List<ProductionCuveModel> GetCuveInfo(string ProductModel,DateTime startTime,DateTime endTime)
        {
            string sql = string.Format("exec PROC_GetOutputNum '{0}', '{1}', '{2}'", ProductModel,startTime,endTime);
            List<ProductionCuveModel> res = this.Get(sql).ToList();
            return res;
        }


    }
}
