using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Prodect
{
    public class ProduceDataModel 
    {

        /// <summary>
        /// ProduceID
        /// </summary>        
        private int _produceid;
        public int ProduceID
        {
            get { return _produceid; }
            set { _produceid = value; }
        }
        /// <summary>
        /// 当前时间
        /// </summary>        
        private DateTime _currentdate;
        public DateTime CurrentDate
        {
            get { return _currentdate; }
            set { _currentdate = value; }
        }
        /// <summary>
        /// 上料数量
        /// </summary>        
        private int _chargingcount;
        public int ChargingCount
        {
            get { return _chargingcount; }
            set { _chargingcount = value; }
        }
        /// <summary>
        /// 下料数量
        /// </summary>        
        private int _blankingcount;
        public int BlankingCount
        {
            get { return _blankingcount; }
            set { _blankingcount = value; }
        }
        /// <summary>
        /// 班次
        /// </summary>        
        private string _shift;
        public string Shift
        {
            get { return _shift; }
            set { _shift = value; }
        }
    }
    public class ProduceDataORMMapper : ClassMapper<ProduceDataModel>
    {
        public ProduceDataORMMapper()
        {
            base.Table("ProduceData");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
