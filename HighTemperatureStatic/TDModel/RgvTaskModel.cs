using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
    public class RgvTaskModel
    {
        /// <summary>
        /// RgvTaskId
        /// Maticsoft
        /// </summary>        
        private int _rgvtaskid;
        public int RgvTaskId
        {
            get { return _rgvtaskid; }
            set { _rgvtaskid = value; }
        }

        public string RgvType { get; set; }
        /// <summary>
        /// CaiId
        /// Maticsoft
        /// </summary>        
        private int _caiid;
        public int CAId
        {
            get { return _caiid; }
            set { _caiid = value; }
        }
        /// <summary>
        /// TaskName
        /// Maticsoft
        /// </summary>        
        private string _taskname;
        public string TaskName
        {
            get { return _taskname; }
            set { _taskname = value; }
        }
        /// <summary>
        /// TaskGrade
        /// Maticsoft
        /// </summary>        
        private int _taskgrade;
        public int TaskGrade
        {
            get { return _taskgrade; }
            set { _taskgrade = value; }
        }
        /// <summary>
        /// TaskType
        /// Maticsoft
        /// </summary>        
        private int _tasktype;
        public int TaskType
        {
            get { return _tasktype; }
            set { _tasktype = value; }
        }
        /// <summary>
        /// TaskState
        /// Maticsoft
        /// </summary>        
        private int _taskstate;
        public int TaskState
        {
            get { return _taskstate; }
            set { _taskstate = value; }
        }
        /// <summary>
        /// TakeCode
        /// Maticsoft
        /// </summary>        
        private int _takecode;
        public int TakeCode
        {
            get { return _takecode; }
            set { _takecode = value; }
        }
        /// <summary>
        /// ReleaseCode
        /// Maticsoft
        /// </summary>        
        private int _releasecode;
        public int ReleaseCode
        {
            get { return _releasecode; }
            set { _releasecode = value; }
        }
        /// <summary>
        /// StoveIp
        /// Maticsoft
        /// </summary>        
        private string _stoveip;
        public string StoveIp
        {
            get { return _stoveip; }
            set { _stoveip = value; }
        }
        /// <summary>
        /// DoorAddress
        /// Maticsoft
        /// </summary>        
        private int _startdooraddress;
        public int StartDoorAddress
        {
            get { return _startdooraddress; }
            set { _startdooraddress = value; }
        }

        public int EndDoorAddress { get; set; }
        /// <summary>
        /// Remark
        /// Maticsoft
        /// </summary>        
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 任务步骤
        /// </summary>
        public int TaskStep { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class RgvTaskORMMapper : ClassMapper<RgvTaskModel>
    {
        public RgvTaskORMMapper()
        {
            base.Table("RgvTask");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
