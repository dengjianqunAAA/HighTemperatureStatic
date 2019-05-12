using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.HT
{
  public  class HtmAndHtdModel
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

        private string _remark;
        /// <summary>
        /// 
        /// <summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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

    }
}
