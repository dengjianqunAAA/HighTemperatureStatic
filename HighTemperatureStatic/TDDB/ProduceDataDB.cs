using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Prodect;
using TengDa.DB.Base;

namespace TDDB
{
    public class ProduceDataDB : RepositoryBase<ProduceDataModel>
    {

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public List<ProduceDataModel> GetListInfo()
        {
            return this.GetAll().ToList();
        }


        protected bool getTimeSpan(string timeStr)
        {
            //判断当前时间是否在工作时间段内
            string _strWorkingDayAM = TDCommon.ConfigHelper.GetAppConfig("TimeM");//早上换班时间
            string _strWorkingDayPM = TDCommon.ConfigHelper.GetAppConfig("TimeE");
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            DateTime t1 = Convert.ToDateTime(timeStr);

            TimeSpan dspNow = t1.TimeOfDay;
            if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
            {
                return true;
            }
            return false;
        }
        TimeSpan am = DateTime.Parse(TDCommon.ConfigHelper.GetAppConfig("TimeM")).TimeOfDay;
        TimeSpan pm = DateTime.Parse(TDCommon.ConfigHelper.GetAppConfig("TimeE")).TimeOfDay;

        /// <summary>
        /// 修改上料数量
        /// </summary>
        /// <returns></returns>
        public int UpdateChargingCount()
        {

            string sql = string.Empty;
            if (getTimeSpan(DateTime.Now.ToString()) == true)
            {
                sql = string.Format("update ProduceData set ChargingCount=ChargingCount+1  where currentDate='{0}'", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeM") + ":00"));
            }
            else
            {
                TimeSpan t1 = DateTime.Now.TimeOfDay;
                if (t1 < am && t1 >= pm)
                {
                    sql = string.Format("update ProduceData set ChargingCount=ChargingCount+1  where currentDate='{0}'", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
                else
                {
                    sql = string.Format("update ProduceData set ChargingCount=ChargingCount+1  where currentDate='{0}'", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
            }
            return this.Execute(sql);
        }

        /// <summary>
        /// 修改下料数量
        /// </summary>
        /// <returns></returns>
        public int UpdateBlankingCount()
        {
            string sql = string.Empty;
            if (getTimeSpan(DateTime.Now.ToString()) == true)
            {
                sql = string.Format("update ProduceData set BlankingCount=BlankingCount+1  where currentDate='{0}'", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeM") + ":00"));
            }
            else
            {
                TimeSpan t1 = DateTime.Now.TimeOfDay;
                if (t1 < am && t1 >= pm)
                {
                    sql = string.Format("update ProduceData set BlankingCount=BlankingCount+1  where currentDate='{0}'", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
                else
                {
                    sql = string.Format("update ProduceData set BlankingCount=BlankingCount+1  where currentDate='{0}'", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
            }
            return this.Execute(sql);
        }


        /// <summary>
        /// 添加当天记录
        /// </summary>
        /// <returns></returns>
        public int InsertProduceData()
        {
            string sql = string.Empty;
            if (getTimeSpan(DateTime.Now.ToString()) == true)
            {
                sql = string.Format("INSERT INTO [ProduceData]([CurrentDate],[ChargingCount],[BlankingCount],[Shift]) VALUES ('{0}',0,0,'M') ", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeM") + ":00"));
            }
            else
            {
                TimeSpan t1 = DateTime.Now.TimeOfDay;
                if (t1 < am && t1 >= pm)
                {
                    sql = string.Format("INSERT INTO [ProduceData]([CurrentDate],[ChargingCount],[BlankingCount],[Shift]) VALUES ('{0}',0,0,'E') ", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
                else
                {
                    sql = string.Format("INSERT INTO [ProduceData]([CurrentDate],[ChargingCount],[BlankingCount],[Shift]) VALUES ('{0}',0,0,'E') ", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
            }
            return this.Execute(sql);
        }


        /// <summary>
        /// 根据时间查询当天产量信息
        /// </summary>
        /// <returns></returns>
        public List<ProduceDataModel> GetInfoByTime()
        {
            string sql = string.Empty;
            if (getTimeSpan(DateTime.Now.ToString()) == true)
            {
                sql = string.Format("select * from  ProduceData where CurrentDate='{0}'", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeM") + ":00"));
            }
            else
            {
                TimeSpan t1 = DateTime.Now.TimeOfDay;
                if (t1 < am && t1 >= pm)
                {
                    sql = string.Format("select * from  ProduceData where CurrentDate='{0}' ", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
                else
                {
                    sql = string.Format("select * from  ProduceData where CurrentDate='{0}' ", DateTime.Now.ToString("yyyy-MM-dd " + TDCommon.ConfigHelper.GetAppConfig("TimeE") + ":00"));
                }
            }
            return this.Get(sql).ToList();
        }


        /// <summary>
        /// 根据日期查询产量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ProduceDataModel> GetProduceDataByDay(string data)
        {
            string sql = string.Format(@"SELECT * FROM ProduceData WHERE CurrentDate>='{0} 00:00:00.001'AND CurrentDate <= '{0} 23:59:59' ", data);
            return this.Get(sql).ToList();
        }

        
    }
}
