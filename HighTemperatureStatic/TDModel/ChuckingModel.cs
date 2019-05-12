using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
   public class ChuckingModel
    {
        #region Model
        private int _caid;
        private int _productmodelid;
        private string _cabarcode;
        private int _castate;
        private int? _htdid;
        private string _fixposition;
        private DateTime _updatetime;
        private DateTime _fixscantime;
        private DateTime? _instovetime;
        private DateTime? _outstovetime;
        private int? _cellnumber = 0;
        private string _remark1;
        private string _remark2;
        private string _createuser;
        /// <summary>
        /// 
        /// </summary>
        public int CAId
        {
            set { _caid = value; }
            get { return _caid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductModelId
        {
            set { _productmodelid = value; }
            get { return _productmodelid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CABarCode
        {
            set { _cabarcode = value; }
            get { return _cabarcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CAState
        {
            set { _castate = value; }
            get { return _castate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HTDId
        {
            set { _htdid = value; }
            get { return _htdid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FixPosition
        {
            set { _fixposition = value; }
            get { return _fixposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FixScanTime
        {
            set { _fixscantime = value; }
            get { return _fixscantime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InStoveTime
        {
            set { _instovetime = value; }
            get { return _instovetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutStoveTime
        {
            set { _outstovetime = value; }
            get { return _outstovetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CellNumber
        {
            set { _cellnumber = value; }
            get { return _cellnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark1
        {
            set { _remark1 = value; }
            get { return _remark1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark2
        {
            set { _remark2 = value; }
            get { return _remark2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        #endregion Model


        public class ChuckingApplianceInfoORMMapper : ClassMapper<ChuckingModel>
        {
            public ChuckingApplianceInfoORMMapper()
            {
                base.Table("ChuckingApplianceInfo");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }
    }
}
