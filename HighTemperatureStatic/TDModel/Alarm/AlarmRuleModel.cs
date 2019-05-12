using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmRule
    public class AlarmRuleModel
    {

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
        /// RuleDid
        /// </summary>        
        private string _ruledid;
        public string RuleDid
        {
            get { return _ruledid; }
            set { _ruledid = value; }
        }
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
        /// SolutionId
        /// </summary>        
        private int _solutionid;
        public int SolutionId
        {
            get { return _solutionid; }
            set { _solutionid = value; }
        }
        /// <summary>
        /// SolutionImageId
        /// </summary>        
        private int _solutionimageid;
        public int SolutionImageId
        {
            get { return _solutionimageid; }
            set { _solutionimageid = value; }
        }
        /// <summary>
        /// AlarmLocationImageId
        /// </summary>        
        private int _alarmlocationimageid;
        public int AlarmLocationImageId
        {
            get { return _alarmlocationimageid; }
            set { _alarmlocationimageid = value; }
        }
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
        /// AlarmContent
        /// </summary>        
        private string _alarmcontent;
        public string AlarmContent
        {
            get { return _alarmcontent; }
            set { _alarmcontent = value; }
        }
        /// <summary>
        /// AlarmReason
        /// </summary>        
        private string _alarmreason;
        public string AlarmReason
        {
            get { return _alarmreason; }
            set { _alarmreason = value; }
        }
        /// <summary>
        /// AlarmAddress
        /// </summary>        
        private string _alarmaddress;
        public string AlarmAddress
        {
            get { return _alarmaddress; }
            set { _alarmaddress = value; }
        }
        /// <summary>
        /// AlarmProcessId
        /// </summary>        
        private int _alarmprocessid;
        public int AlarmProcessId
        {
            get { return _alarmprocessid; }
            set { _alarmprocessid = value; }
        }
        /// <summary>
        /// AlarmProcessNumber
        /// </summary>        
        private int _alarmprocessnumber;
        public int AlarmProcessNumber
        {
            get { return _alarmprocessnumber; }
            set { _alarmprocessnumber = value; }
        }
        /// <summary>
        /// facilityDID
        /// </summary>        
        private int _facilitydid;
        public int facilityDID
        {
            get { return _facilitydid; }
            set { _facilitydid = value; }
        }
        /// <summary>
        /// level
        /// </summary>        
        private int _level;
        public int level
        {
            get { return _level; }
            set { _level = value; }
        }



    }
}

