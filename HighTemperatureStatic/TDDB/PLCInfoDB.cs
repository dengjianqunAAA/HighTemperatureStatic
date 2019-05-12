using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TengDa.DB.Base;

namespace TDDB
{
    public class PLCInfoDB : RepositoryBase<PLCInfoModel>
    {
        /// <summary>
        /// 查询所有IP地址
        /// </summary>
        /// <returns></returns>
        public List<PLCInfoModel> Getplclist()
        {
            string sql = "SELECT * FROM PLCInfo";
            
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 修改连接状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateConnect(List<PLCInfoModel> list)
        {
            return this.UpdateBatch(list);
        }
    }
}
