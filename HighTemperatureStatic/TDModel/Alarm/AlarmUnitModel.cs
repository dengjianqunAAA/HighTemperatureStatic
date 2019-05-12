using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmUnit
    public class AlarmUnitModel
    {

        /// <summary>
        /// AlarmUnitId
        /// </summary>        
        private int _alarmunitid;
        public int AlarmUnitId
        {
            get { return _alarmunitid; }
            set { _alarmunitid = value; }
        }
        /// <summary>
        /// AlarmUnitName
        /// </summary>        
        private string _alarmunitname;
        public string AlarmUnitName
        {
            get { return _alarmunitname; }
            set { _alarmunitname = value; }
        }
    }
}

