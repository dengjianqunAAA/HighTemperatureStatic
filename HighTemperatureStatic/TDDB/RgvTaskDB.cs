using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TengDa.DB.Base;

namespace TDDB
{
    public class RgvTaskDB : RepositoryBase<RgvTaskModel>
    {

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertRgvTask(RgvTaskModel model)
        {
            return this.Insert(model);
        }



        /// <summary>
        /// 根据状态查询RGV任务
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<RgvTaskModel> GetRgvTaskInfoByState(string state,string position)
        {
            string sql = string.Format(@"SELECT  *  FROM [RgvTask] WHERE TaskState IN ({0}) AND RgvType='{1}'", state,position);
            return this.Get(sql).ToList();
        }


        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateRgvTaskInfoByid(int id,int state)
        {
            string sql = string.Format(@"UPDATE RgvTask SET TaskState='{1}' WHERE RgvTaskId={0}",id,state);
            return this.Execute(sql);
        }

        /// <summary>
        /// 修改任务步骤
        /// </summary>
        /// <param name="id"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public int UpdateTaskStepById(int id,int step)
        {
            string sql = string.Format(@"UPDATE RgvTask SET TaskStep={1} WHERE RgvTaskId={0}",id,step);
            return this.Execute(sql);
        }
    }
}
