﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Alarm.Views
{
    //view_GetTemporariesAlarm
    public class View_GetTemporariesAlarmModel
    {

        /// <summary>Maticsoft
        /// AlarmTemporaryId
        /// </summary>        
        private int _alarmtemporaryid;
        public int AlarmTemporaryId
        {
            get { return _alarmtemporaryid; }
            set { _alarmtemporaryid = value; }
        }
        /// <summary>Maticsoft
        /// AlarmContent
        /// </summary>        
        private string _alarmcontent;
        public string AlarmContent
        {
            get { return _alarmcontent; }
            set { _alarmcontent = value; }
        }
        /// <summary>Maticsoft
        /// AlarmUnitName
        /// </summary>        
        private string _alarmunitname;
        public string AlarmUnitName
        {
            get { return _alarmunitname; }
            set { _alarmunitname = value; }
        }
        /// <summary>Maticsoft
        /// ProcessName
        /// </summary>        
        private string _processname;
        public string ProcessName
        {
            get { return _processname; }
            set { _processname = value; }
        }
        /// <summary>Maticsoft
        /// AlarmTime
        /// </summary>        
        private DateTime _alarmtime;
        public DateTime AlarmTime
        {
            get { return _alarmtime; }
            set { _alarmtime = value; }
        }
        /// <summary>Maticsoft
        /// RuleDid
        /// </summary>        
        private string _ruledid;
        public string RuleDid
        {
            get { return _ruledid; }
            set { _ruledid = value; }
        }
        /// <summary>Maticsoft
        /// AlarmTypeName
        /// </summary>        
        private string _alarmtypename;
        public string AlarmTypeName
        {
            get { return _alarmtypename; }
            set { _alarmtypename = value; }
        }
        /// <summary>Maticsoft
        /// DisposeState
        /// </summary>        
        private int _disposestate;
        public int DisposeState
        {
            get { return _disposestate; }
            set { _disposestate = value; }
        }
        /// <summary>Maticsoft
        /// SolutionImagePath
        /// </summary>        
        private string _solutionimagepath;
        public string SolutionImagePath
        {
            get { return _solutionimagepath; }
            set { _solutionimagepath = value; }
        }
        /// <summary>Maticsoft
        /// AlarmLocationImagePath
        /// </summary>        
        private string _alarmlocationimagepath;
        public string AlarmLocationImagePath
        {
            get { return _alarmlocationimagepath; }
            set { _alarmlocationimagepath = value; }
        }
        /// <summary>Maticsoft
        /// SolutionName
        /// </summary>        
        private string _solutionname;
        public string SolutionName
        {
            get { return _solutionname; }
            set { _solutionname = value; }
        }
        /// <summary>Maticsoft
        /// AlarmAddress
        /// </summary>        
        private string _alarmaddress;
        public string AlarmAddress
        {
            get { return _alarmaddress; }
            set { _alarmaddress = value; }
        }
        /// <summary>Maticsoft
        /// DisposeTime
        /// </summary>        
        private DateTime _disposetime;
        public DateTime DisposeTime
        {
            get { return _disposetime; }
            set { _disposetime = value; }
        }
        /// <summary>Maticsoft
        /// Handler
        /// </summary>        
        private string _handler;
        public string Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }
        /// <summary>Maticsoft
        /// Duration
        /// </summary>        
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        /// <summary>Maticsoft
        /// AlarmReason
        /// </summary>        
        private string _alarmreason;
        public string AlarmReason
        {
            get { return _alarmreason; }
            set { _alarmreason = value; }
        }
        /// <summary>Maticsoft
        /// CreateTime
        /// </summary>        
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>Maticsoft
        /// level
        /// </summary>        
        private int _level;
        public int level
        {
            get { return _level; }
            set { _level = value; }
        }
        /// <summary>Maticsoft
        /// AlarmRecordDID
        /// </summary>        
        private string _alarmrecorddid;
        public string AlarmRecordDID
        {
            get { return _alarmrecorddid; }
            set { _alarmrecorddid = value; }
        }

    }

    public class View_GetTemporariesAlarmORMMapper : ClassMapper<View_GetTemporariesAlarmModel>
    {
        public View_GetTemporariesAlarmORMMapper()
        {
            base.Table("View_GetTemporariesAlarm");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
