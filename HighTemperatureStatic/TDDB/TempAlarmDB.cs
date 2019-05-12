using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Alarm.Views;
using TengDa.DB.Base;

namespace TDDB
{
    public class TempAlarmDB : RepositoryBase<View_GetTemporariesAlarmModel>
    {
        public List<View_GetTemporariesAlarmModel> GetTempAlarmList(int PageIndex ,int PageSize, out long AllRowsCount)
        {
            List<View_GetTemporariesAlarmModel> list = this.GetPageList(PageIndex,PageSize, out AllRowsCount).ToList();
            return list;
        }
    }
}
