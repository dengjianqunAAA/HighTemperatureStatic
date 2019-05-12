using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Product.Views
{
    public class View_GetFixtureInfoModel
    {

        /// <summary>
        /// CAId
        /// Maticsoft
        /// </summary>        
        private int _caid;
        public int CAId
        {
            get { return _caid; }
            set { _caid = value; }
        }
        /// <summary>
        /// CABarCode
        /// Maticsoft
        /// </summary>        
        private string _cabarcode;
        public string CABarCode
        {
            get { return _cabarcode; }
            set { _cabarcode = value; }
        }
        /// <summary>
        /// ProductName
        /// Maticsoft
        /// </summary>        
        private string _productname;
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }
        /// <summary>
        /// CAState
        /// Maticsoft
        /// </summary>        
        private int _castate;
        public int CAState
        {
            get { return _castate; }
            set { _castate = value; }
        }



        /// <summary>
        /// CAStateName
        /// Maticsoft
        /// </summary>        
        private string _castatename;
        public string CAStateName
        {
            get { return _castatename; }
            set { _castatename = value; }
        }

        /// <summary>
        /// FixPosition
        /// Maticsoft
        /// </summary>        
        private string _fixposition;
        public string FixPosition
        {
            get { return _fixposition; }
            set { _fixposition = value; }
        }
        /// <summary>
        /// FixScanTime
        /// Maticsoft
        /// </summary>        
        private DateTime _fixscantime;
        public DateTime FixScanTime
        {
            get { return _fixscantime; }
            set { _fixscantime = value; }
        }
        /// <summary>
        /// InStoveTime
        /// Maticsoft
        /// </summary>        
        private DateTime _instovetime;
        public DateTime InStoveTime
        {
            get { return _instovetime; }
            set { _instovetime = value; }
        }
        /// <summary>
        /// OutStoveTime
        /// Maticsoft
        /// </summary>        
        private DateTime _outstovetime;
        public DateTime OutStoveTime
        {
            get { return _outstovetime; }
            set { _outstovetime = value; }
        }
        /// <summary>
        /// CellNumber
        /// Maticsoft
        /// </summary>        
        private int _cellnumber;
        public int CellNumber
        {
            get { return _cellnumber; }
            set { _cellnumber = value; }
        }
        /// <summary>
        /// HTDNumber
        /// Maticsoft
        /// </summary>        
        private int _htdnumber;
        public int HTDNumber
        {
            get { return _htdnumber; }
            set { _htdnumber = value; }
        }
        /// <summary>
        /// HTMName
        /// Maticsoft
        /// </summary>        
        private string _htmname;
        public string HTMName
        {
            get { return _htmname; }
            set { _htmname = value; }
        }
        /// <summary>
        /// FurnaceNumber
        /// Maticsoft
        /// </summary>        
        private string _furnacenumber;
        public string FurnaceNumber
        {
            get { return _furnacenumber; }
            set { _furnacenumber = value; }
        }
        /// <summary>
        /// CreateUser
        /// Maticsoft
        /// </summary>        
        private string _createuser;
        public string CreateUser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        /// <summary>
        /// HTDId
        /// Maticsoft
        /// </summary>        
        private int _htdid;
        public int HTDId
        {
            get { return _htdid; }
            set { _htdid = value; }
        }

        /// <summary>
        /// BPSetBakingTime
        /// Maticsoft
        /// </summary>        
        private int _bpsetbakingtime;
        public int BPSetBakingTime
        {
            get { return _bpsetbakingtime; }
            set { _bpsetbakingtime = value; }
        }
    }
}
