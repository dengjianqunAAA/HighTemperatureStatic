using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Product;
using TengDa.DB.Base;

namespace TDDB
{
    public class CellInfoDB : RepositoryBase<CellInfoModel>
    {
        /// <summary>
        /// 添加电芯记录
        /// </summary>
        /// <param name="CAid"></param>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public int InsertCellCount(CellInfoModel model)
        {
            //string sql = string.Format(@"INSERT INTO [CellInfo] ([BarCode]  ,[CellScanTime],[State] )
            //VALUES
            //('{0}' ,GETDATE() ,'10' )", BarCode);
            //return this.Execute(sql);

            return Insert(model);
        }


        /// <summary>
        /// 下料修改电芯信息
        /// </summary>
        /// <param name="CAid"></param>
        /// <param name="Marking"></param>
        /// <returns></returns>
        public int UpdatCellInfo(int CAid,string Marking)
        {
            string sql = string.Format(@"UPDATE CellInfo SET [OverTime]='{1}',[Marking]='{2}' where CAid={0} ",CAid,DateTime.Now,Marking);
            return this.Execute(sql);
        }

        public int UpdatCellInfo(string BarCode, string Marking)
        {
            string sql = string.Format(@"UPDATE CellInfo SET [OverTime]='{1}',[Marking]='{2}' where BarCode={0} ", BarCode, DateTime.Now, Marking);
            return this.Execute(sql);
        }


        /// <summary>
        /// 根据电芯条码查询电芯信息
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public CellInfoModel GetCellInfoByCode(string BarCode)
        {
            string sql = string.Format("SELECT TOP 1 * FROM CellInfo where BarCode='{0}' ", BarCode);
            return this.Get(sql).FirstOrDefault();
        }
        
        /// <summary>
        /// 修改电芯状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCellState(CellInfoModel model)
        {
            string sql = string.Empty;
            if (model.State == 20)
            {
                sql = string.Format("UPDATE [CellInfo] SET [State] = '20'  WHERE CellInfoId = '{0}'",model.CellInfoId);
            }
            else if (model.State==30)
            {
                sql = string.Format("UPDATE [CellInfo] SET [CAId] = '{1}' ,[CellPosition] = '{2}' ,[State] = '30' WHERE CellInfoId = '{0}'",model.CellInfoId,model.CAId,model.CellPosition);
            }
            return this.Execute(sql);
        }


        /// <summary>
        /// 根据电芯状态查找电芯信息
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public List<CellInfoModel> GetCellInfoBystate(string State)
        {
            string sql = string.Format("SELECT * FROM dbo.CellInfo WHERE STATE in ({0}) ORDER BY CellScanTime DESC ", State);
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 根据夹具ID查找电芯信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CellInfoModel> GetCellInfoByCaId(int id)
        {
            string sql = string.Format("SELECT * FROM [CellInfo] WHERE CAId = '{0}'", id);
            return this.Get(sql).ToList();
        }

    }
}
