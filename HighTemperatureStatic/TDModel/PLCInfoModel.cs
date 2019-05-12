using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
    public class PLCInfoModel
    {
        /// <summary>
        /// PIId
        /// Maticsoft
        /// </summary>        
        private int _piid;
        public int PIId
        {
            get { return _piid; }
            set { _piid = value; }
        }
        /// <summary>
        /// PIName
        /// Maticsoft
        /// </summary>        
        private string _piname;
        public string PIName
        {
            get { return _piname; }
            set { _piname = value; }
        }
        /// <summary>
        /// PIIp
        /// Maticsoft
        /// </summary>        
        private string _piip;
        public string PIIp
        {
            get { return _piip; }
            set { _piip = value; }
        }
        /// <summary>
        /// PIType
        /// Maticsoft
        /// </summary>        
        private int _pitype;
        public int PIType
        {
            get { return _pitype; }
            set { _pitype = value; }
        }
        /// <summary>
        /// PIIsConnect
        /// Maticsoft
        /// </summary>        
        private bool _piisconnect;
        public bool PIIsConnect
        {
            get { return _piisconnect; }
            set { _piisconnect = value; }
        }
        /// <summary>
        /// ConnTime
        /// Maticsoft
        /// </summary>        
        private DateTime _conntime;
        public DateTime ConnTime
        {
            get { return _conntime; }
            set { _conntime = value; }
        }


        /// <summary>
        /// Flag
        /// Maticsoft
        /// </summary>        
        private bool _isflag;
        public bool  isFlag
        {
            get { return _isflag; }
            set { _isflag = value; }
        }
        private bool _isconn;

        public bool IsConn
        {
            get { return _isconn; }
            set { _isconn = value; }
        }
    }
    public class PLCInfoORMMapper : ClassMapper<PLCInfoModel>
    {
        public PLCInfoORMMapper()
        {
            base.Table("PLCInfo");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
