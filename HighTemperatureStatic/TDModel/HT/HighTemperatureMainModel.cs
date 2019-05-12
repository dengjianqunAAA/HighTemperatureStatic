using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.HT
{
    public class HighTemperatureMainModel
    {
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

        private int _htmstate;
        /// <summary>
        /// 
        /// <summary>
        public int HTMState
        {
            get { return _htmstate; }
            set { _htmstate = value; }
        }

        private string _htmnumber;
        /// <summary>
        /// 
        /// <summary>
        public string HTMNumber
        {
            get { return _htmnumber; }
            set { _htmnumber = value; }
        }

        private string _htmname;
        /// <summary>
        /// 
        /// <summary>
        public string HTMName
        {
            get { return _htmname; }
            set { _htmname = value; }
        }

        private int _htmcountlayer;
        /// <summary>
        /// 
        /// <summary>
        public int HTMCountLayer
        {
            get { return _htmcountlayer; }
            set { _htmcountlayer = value; }
        }

        private int _htmberthposition;
        /// <summary>
        /// 
        /// <summary>
        public int HTMBerthPosition
        {
            get { return _htmberthposition; }
            set { _htmberthposition = value; }
        }

        private int _htmrowsort;
        /// <summary>
        /// 
        /// <summary>
        public int HTMRowSort
        {
            get { return _htmrowsort; }
            set { _htmrowsort = value; }
        }

        private int _htmcreatetype;
        /// <summary>
        /// 
        /// <summary>
        public int HTMCreateType
        {
            get { return _htmcreatetype; }
            set { _htmcreatetype = value; }
        }

        private int _htmterracetype;
        /// <summary>
        /// 
        /// <summary>
        public int HTMTerraceType
        {
            get { return _htmterracetype; }
            set { _htmterracetype = value; }
        }

        /// <summary>
        /// 产品型号ID
        /// </summary>
        public int ProductModelId { get; set; }

        /// <summary>
        /// 设定温度
        /// </summary>
        public float SetTemperature { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remake { get; set; }
        public class HighTemperatureMainORMMapper : ClassMapper<HighTemperatureMainModel>
        {
            public HighTemperatureMainORMMapper()
            {
                base.Table("HighTemperatureMain");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }
    }
}
