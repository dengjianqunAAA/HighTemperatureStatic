using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TDModel.Product;
using TengDa.DB.Base;

namespace TDDB
{
    public class ChuckingApplianceInfoDB : RepositoryBase<ChuckingApplianceInfoModel>
    {

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="CAId"></param>
        /// <param name="state"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public int UpdateFixtureInfo(int CAId,int state,int number)
        {
            string sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}' , CellNumber={2} WHERE CAId={0} ",CAId,state,number);

            return this.Execute(sql);
        }

        public int UpdateStateById(int CAId, int state)
        {
            string sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}'  WHERE CAId={0} ", CAId, state);

            return this.Execute(sql);
        }
        


        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="CAId"></param>
        /// <param name="state"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public int UpdateFixtureInfo(int CAId, int state, int number, int HTDId, DateTime InStoveTime)
        {
            string sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState={1} , CellNumber={2}, HTDId={3}, InStoveTime='{4}' WHERE CAId={0} ", CAId, state, number,HTDId,InStoveTime);

            return this.Execute(sql);
        }
        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="CAId"></param>
        /// <param name="state"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public int UpdateFixtureInfo(int CAId, int state, int number , int HTDId, DateTime InStoveTime ,DateTime OutStoveTime)
        {
            string sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState={1} , CellNumber={2},HTDId={3}, InStoveTime='{4}', OutStoveTime='{5}' WHERE CAId={0} ", CAId, state, number,HTDId,InStoveTime,OutStoveTime);

            return this.Execute(sql);
        }


        /// <summary>
        /// 将单次流程结束夹具添加到历史表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int FixtureHandle(int id)
        {
            string sql = string.Format("EXEC [Proc_FixtureHandle] '{0}'", id);
            return this.Execute(sql);
        }



        /// <summary>
        /// 通过状态40找到相关的夹具信息
        /// </summary>
        /// <param name="apiModel"></param>
        /// <returns></returns>
        public List<ChuckingApplianceInfoModel> GetCaiDataByState(ApiModel apiModel)
        {
            string sql = string.Format(@"SELECT CABarCode,CAState ,FixPosition,HTDId
 FROM dbo.ChuckingApplianceInfo WHERE CAState=40 AND FixPosition='{0}'", apiModel.FixPosition);
            return this.Get(sql).ToList();
        }

        public List<ChuckingApplianceInfoModel> GetCaiDataByState(int state)
        {
            string sql = string.Format(@"SELECT * FROM dbo.ChuckingApplianceInfo WHERE CAState='{0}'", state);
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 添加夹具记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int InsertFirstInfo(string code,string str)
        {
            string sql = string.Format(@"INSERT INTO [ChuckingApplianceInfo] ([ProductModelId] ,[CABarCode] ,[CAState]  ,[FixPosition] ,[UpdateTime] ,[FixScanTime] ,[CreateUser]) VALUES 
('{0}' , '{1}' , '{2}'  , '{3}' , '{4}' , '{5}'  , '{6}')",TDCommon.SysInfo.ProductModelId,code,0,str,DateTime.Now,DateTime.Now);
            return this.Execute(sql);
        }



        /// <summary>
        /// 根据夹具条码查询夹具信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ChuckingApplianceInfoModel> GetInfoByCode(string code)
        {
            string sql = string.Format("select * from ChuckingApplianceInfo where CABarCode='{0}'",code);
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 查找入炉中夹具
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<ChuckingApplianceInfoModel> GetInstoveInfoByState(string state,string str)
        {
            string sql = string.Format("select c1.*,h1.Remark AS StoveIP from ChuckingApplianceInfo c1 ,dbo.HighTemperatureDetail h1 WHERE c1.HTDId=h1.HTDId AND  FixPosition='{1}' and CAState in ({0}) and Remark1!='1' order BY C1.FixScanTime desc", state,str);
            return this.Get(sql).ToList();
        }


        /// <summary>
        /// 根据状态查询夹具信息
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public List<ChuckingApplianceInfoModel> GetInfoByState(int State ,string position)
        {
            string sql = string.Empty;
            if (State == 10)
            {
                sql = string.Format("SELECT TOP 2 [CAId] ,[ProductModelId] ,[CABarCode] ,[CAState] ,[HTDId] ,[FixPosition] ,[UpdateTime] ,[FixScanTime]  ,[InStoveTime] ,[OutStoveTime] ,[CellNumber] ,[Remark1] ,[Remark2] ,[CreateUser]  FROM [ChuckingApplianceInfo] WHERE CAState='{0}' AND FixPosition= '{1}'", State, position);
            }
            else
            {
                sql = string.Format("SELECT *  FROM ChuckingApplianceInfo  WHERE CAState='{0}' AND FixPosition= '{1}' ", State,position);
            }
            
            return this.Get(sql).ToList();
        }

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <returns></returns>
        public int UpdateState(ChuckingApplianceInfoModel model)
        {
            string sql = string.Empty;
            if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.CodeBinding.ToString("d")))//电芯绑定中
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}',UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.CodeBindEnd.ToString("d")))//电芯绑定完成
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{2}' , CellNumber='{1}', UpdateTime=GETDATE(), Remark1='{3}' WHERE CAId='{0}'", model.CAId, model.CellNumber, model.CAState, model.Remark1);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.TobeReclaimed.ToString("d")))//待取料
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}' , UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.Instoveing.ToString("d")))//入炉中
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{2}',HTDId='{1}' , UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.HTDId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.InstoveEnd.ToString("d")))//入炉完成
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}',InStoveTime=GETDATE() , UpdateTime=GETDATE(),Remark1='{2}' WHERE CAId='{0}'", model.CAId, model.CAState, model.Remark1);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.Outstoveing.ToString("d")))//出炉中
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}', UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.OutstoveEnd.ToString("d")))//已出炉
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}',OutStoveTime=GETDATE(), UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.UnloadedBuffer.ToString("d")))//待入下料缓存炉
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}', UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.BlankingBuff.ToString("d")))//下料缓存中
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}', UpdateTime=GETDATE(),Remark1='{2}' WHERE CAId='{0}'", model.CAId, model.CAState,model.Remark1);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.UnloadedBlankingScan.ToString("d")))//待下料扫码
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}', UpdateTime=GETDATE() WHERE CAId='{0}'", model.CAId, model.CAState);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.BlankingScan.ToString("d")))//下料扫码中
            {
                sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET CAState='{1}', UpdateTime=GETDATE(),Remark1='{2}' WHERE CAId='{0}'", model.CAId, model.CAState,model.Remark1);
            }
            else if (model.CAState == int.Parse(TDCommon.SysEnum.FixtureState.ProcessEnd.ToString("d")))//生产流程结束
            {
                sql = string.Format("EXEC Proc_FixtureHandle {0}", model.CAId);
            }
            return this.Execute(sql);
        }

        /// <summary>
        /// 获取出炉夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public ChuckingApplianceInfoModel GetOutStoveInfo(string data)
        //{
        //    string sql = string.Format(@"SELECT TOP 1 C1.*,h1.HTDLayer,h1.Remark FROM dbo.ChuckingApplianceInfo c1 
        //                                  INNER JOIN dbo.HighTemperatureDetail AS h1 ON c1.HTDId = h1.HTDId
        //                                  INNER JOIN dbo.HighTemperatureMain   AS h2 ON h1.HTMId = h2.HTMId
        //                                  INNER JOIN dbo.PlcInfo               AS p1 ON p1.PIId  = h2.PIId
        //                                  WHERE FixPosition = '{0}' AND  CAState = 40 AND p1.PIIsConnect = 1
        //                                  AND   dateadd(mi, h1.SetBakingTime, c1.inStoveTime) <= GETDATE() ORDER BY c1.InStoveTime",data);
        //    return this.Get(sql).FirstOrDefault();
        //}
        public ChuckingApplianceInfoModel GetOutStoveInfo(string data)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM dbo.ChuckingApplianceInfo c1  WHERE  CAState = 40  ORDER BY c1.InStoveTime", data);
            return this.Get(sql).FirstOrDefault();
        }

        /// <summary>
        /// 修改备注值1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remake1"></param>
        /// <returns></returns>
        public int UpdateChuckInfoRemake1(int id,string remake1)
        {
            string sql = string.Format("UPDATE dbo.ChuckingApplianceInfo SET Remark1='{1}' WHERE CAid={0}",id,remake1);
            return this.Execute(sql);
        }


        /// <summary>
        /// 根据夹具ID查询夹具信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ChuckingApplianceInfoModel GetChuckInfoByid(int id)
        {
            string sql = string.Format("select * from ChuckingApplianceInfo where CAId='{0}'", id);
            return this.Get(sql).FirstOrDefault();
        }
    }
}
