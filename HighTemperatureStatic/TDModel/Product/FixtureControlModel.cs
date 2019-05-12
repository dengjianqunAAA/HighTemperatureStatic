using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Product
{
    public class FixtureControlModel
    {
        /// <summary>
        /// id
        /// Maticsoft
        /// </summary>        
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 夹具条码
        /// Maticsoft
        /// </summary>        
        private string _fixturecode;
        public string FixtureCode
        {
            get { return _fixturecode; }
            set { _fixturecode = value; }
        }
        /// <summary>
        /// 夹具使用状态
        /// Maticsoft
        /// </summary>        
        private string _fixturestate;
        public string FixtureState
        {
            get { return _fixturestate; }
            set { _fixturestate = value; }
        }
        //private string _fixturestatename;
        //public string FixtureStateName
        //{
        //    get { return _fixturestatename; }
        //    set { _fixturestatename = value; }
        //}

        

        /// <summary>
        /// 创建时间
        /// Maticsoft
        /// </summary>        
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 备注 
        /// Maticsoft
        /// </summary>        
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
    }


    public class FixtureControlORMMapper : ClassMapper<FixtureControlModel>
    {
        public FixtureControlORMMapper()
        {
            base.Table("FixtureControl");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
