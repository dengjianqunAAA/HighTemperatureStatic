using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.HT
{
    public class HighTemperatureDetailModel
    {
        private int _htdid;
        /// <summary>
        /// 
        /// <summary>
        public int HTDId
        {
            get { return _htdid; }
            set { _htdid = value; }
        }

        private int _htdnumber;
        /// <summary>
        /// 
        /// <summary>
        public int HTDNumber
        {
            get { return _htdnumber; }
            set { _htdnumber = value; }
        }

        private int _htdlayer;
        /// <summary>
        /// 
        /// <summary>
        public int HTDLayer
        {
            get { return _htdlayer; }
            set { _htdlayer = value; }
        }

        private int _htmid;
        /// <summary>
        /// 
        /// <summary>
        public int HTMId
        {
            get { return _htmid; }
            set { _htmid = value; }
        }

        private int _piid;
        /// <summary>
        /// 
        /// <summary>
        public int PIId
        {
            get { return _piid; }
            set { _piid = value; }
        }

        private int _bpid;
        /// <summary>
        /// 
        /// <summary>
        public int BPId
        {
            get { return _bpid; }
            set { _bpid = value; }
        }

        private int _htdstate;
        /// <summary>
        /// 
        /// <summary>
        public int HTDState
        {
            get { return _htdstate; }
            set { _htdstate = value; }
        }

        private int _rgvpositioncode;
        /// <summary>
        /// 
        /// <summary>
        public int RgvPositionCode
        {
            get { return _rgvpositioncode; }
            set { _rgvpositioncode = value; }
        }



        private int _simcard;
        /// <summary>
        /// 门号
        /// <summary>
        public int SimCard
        {
            get { return _simcard; }
            set { _simcard = value; }
        }

        /// <summary>
        /// 设定时间
        /// </summary>
        public int SetBakingTime { get; set; }

        public int Remarke { get; set; }




        public class HighTemperatureDetailORMMapper : ClassMapper<HighTemperatureDetailModel>
        {
            public HighTemperatureDetailORMMapper()
            {
                base.Table("HighTemperatureDetail");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }
    }
}
