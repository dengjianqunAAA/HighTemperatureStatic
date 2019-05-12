using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmHistoryAlarm
    public class AlarmHistoryAlarmModel
    {

        /// <summary>
        /// AlarmTemporaryId
        /// </summary>        
        private int _alarmtemporaryid;
        public int AlarmTemporaryId
        {
            get { return _alarmtemporaryid; }
            set { _alarmtemporaryid = value; }
        }
        /// <summary>
        /// RuleId
        /// </summary>        
        private int _ruleid;
        public int RuleId
        {
            get { return _ruleid; }
            set { _ruleid = value; }
        }
        /// <summary>
        /// AlarmTime
        /// </summary>        
        private DateTime _alarmtime;
        public DateTime AlarmTime
        {
            get { return _alarmtime; }
            set { _alarmtime = value; }
        }
        /// <summary>
        /// DisposeState
        /// </summary>        
        private int _disposestate;
        public int DisposeState
        {
            get { return _disposestate; }
            set { _disposestate = value; }
        }
        /// <summary>
        /// DisposeTime
        /// </summary>        
        private DateTime _disposetime;
        public DateTime DisposeTime
        {
            get { return _disposetime; }
            set { _disposetime = value; }
        }
        /// <summary>
        /// Handler
        /// </summary>        
        private string _handler;
        public string Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }
        /// <summary>
        /// Duration
        /// </summary>        
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        /// <summary>
        /// CreateUser
        /// </summary>        
        private string _createuser;
        public string CreateUser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        /// <summary>
        /// CreateTime
        /// </summary>        
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// UpdateUser
        /// </summary>        
        private string _updateuser;
        public string UpdateUser
        {
            get { return _updateuser; }
            set { _updateuser = value; }
        }
        /// <summary>
        /// UpdateTime
        /// </summary>        
        private DateTime _updatetime;
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// Remark
        /// </summary>        
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// AlarmRecordDID
        /// </summary>        
        private string _alarmrecorddid;
        public string AlarmRecordDID
        {
            get { return _alarmrecorddid; }
            set { _alarmrecorddid = value; }
        }



    }
}

