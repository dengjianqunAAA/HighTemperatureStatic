using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmType
    public class AlarmTypeModel
    {

        /// <summary>
        /// AlarmTypeId
        /// </summary>        
        private int _alarmtypeid;
        public int AlarmTypeId
        {
            get { return _alarmtypeid; }
            set { _alarmtypeid = value; }
        }
        /// <summary>
        /// AlarmTypeName
        /// </summary>        
        private string _alarmtypename;
        public string AlarmTypeName
        {
            get { return _alarmtypename; }
            set { _alarmtypename = value; }
        }



    }
}

