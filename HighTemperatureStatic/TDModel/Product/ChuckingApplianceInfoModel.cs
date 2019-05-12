using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Product
{
    public class ChuckingApplianceInfoModel
    {

        private int _caid;
        /// <summary>
        /// 
        /// <summary>
        public int CAId
        {
            get { return _caid; }
            set { _caid = value; }
        }

        private int _productmodelid;
        /// <summary>
        /// 
        /// <summary>
        public int ProductModelId
        {
            get { return _productmodelid; }
            set { _productmodelid = value; }
        }

        private string _cabarcode;
        /// <summary>
        /// 
        /// <summary>
        public string CABarCode
        {
            get { return _cabarcode; }
            set { _cabarcode = value; }
        }

        private int _castate;
        /// <summary>
        /// 
        /// <summary>
        public int CAState
        {
            get { return _castate; }
            set { _castate = value; }
        }

        private int _htdid;
        /// <summary>
        /// 
        /// <summary>
        public int HTDId
        {
            get { return _htdid; }
            set { _htdid = value; }
        }


        private string _fixposition;
        /// <summary>
        /// 
        /// <summary>
        public string FixPosition
        {
            get { return _fixposition; }
            set { _fixposition = value; }
        }

        private DateTime _updatetime;
        /// <summary>
        /// 
        /// <summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }


        private DateTime _fixscantime;
        /// <summary>
        /// 
        /// <summary>
        public DateTime FixScanTime
        {
            get { return _fixscantime; }
            set { _fixscantime = value; }
        }

        private DateTime _instovetime;
        /// <summary>
        /// 
        /// <summary>
        public DateTime InStoveTime
        {
            get { return _instovetime; }
            set { _instovetime = value; }
        }

        private DateTime _outstovetime;
        /// <summary>
        /// 
        /// <summary>
        public DateTime OutStoveTime
        {
            get { return _outstovetime; }
            set { _outstovetime = value; }
        }

        private int _cellnumber;
        /// <summary>
        /// 
        /// <summary>
        public int CellNumber
        {
            get { return _cellnumber; }
            set { _cellnumber = value; }
        }

        private string _remark1;
        /// <summary>
        /// 
        /// <summary>
        public string Remark1
        {
            get { return _remark1; }
            set { _remark1 = value; }
        }

        private string _remark2;
        /// <summary>
        /// 
        /// <summary>
        public string Remark2
        {
            get { return _remark2; }
            set { _remark2 = value; }
        }

        private string _createuser;
        /// <summary>
        /// 
        /// <summary>
        public string CreateUser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }

        //private string _position;
        ///// <summary>
        ///// 
        ///// <summary>
        //public string Position
        //{
        //    get { return _position; }
        //    set { _position = value; }
        //}

        public int StoveIP { get; set; }

        public int Remake { get; set; }

        public int HTDLayer { get; set; }
        public class ChuckingApplianceInfoORMMapper : ClassMapper<ChuckingApplianceInfoModel>
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
