using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Product;
using TengDa.DB.Base;

namespace TDDB
{
    public class FixtureControlDB : RepositoryBase<FixtureControlModel>
    {

        /// <summary>
        /// 根据夹具条码查询夹具信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="AllRowsCount"></param>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public List<FixtureControlModel> GetListInfoByCode(int PageIndex,int PageSize,out long AllRowsCount,string BarCode)
        {
            string sql = string.Format("SELECT id,CASE FixtureState  WHEN 0 THEN '空闲' WHEN 10 THEN '使用中'  WHEN 20 THEN '维修禁用' END AS FixtureState,FixtureCode,CreateTime,Remark   FROM [FixtureControl]  where FixtureCode='{0}'", BarCode);
            return this.GetPage(PageIndex, PageSize, out AllRowsCount, sql).ToList();
        }


        /// <summary>
        /// 修改夹具信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFinxtureById(FixtureControlModel model)
        {
            string sql = string.Format(@"UPDATE FixtureControl SET FixtureCode = '{1}', FixtureState = '{2}', CreateTime = '{3}', Remark = '{4}'  WHERE id='{0}'",model.id,model.FixtureCode,model.FixtureState,DateTime.Now,model.Remark);
            return this.Execute(sql);
        }


        /// <summary>
        /// 添加夹具信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertFixture(FixtureControlModel model)
        {
            string sql = string.Format(@"INSERT INTO FixtureControl (FixtureCode, FixtureState, CreateTime, Remark) VALUES ('{0}','{1}','{2}','{3}')",model.FixtureCode,model.FixtureState,DateTime.Now,model.Remark);
            return this.Execute(sql);
        }

        /// <summary>
        /// 根据夹具条码查询信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public FixtureControlModel GetFixtureInfoByCode(string Code)
        {
            string sql = string.Format("select * from FixtureControl where FixtureCode='{0}' ", Code);
            return this.Get(sql).FirstOrDefault();
        }
    }
}
