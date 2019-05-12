using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Product
{
    public class CellInfoModel
    {

        /// <summary>
        /// CellInfoId
        /// Maticsoft
        /// </summary>        
        private int _cellinfoid;
        public int CellInfoId
        {
            get { return _cellinfoid; }
            set { _cellinfoid = value; }
        }
        /// <summary>
        /// BarCode
        /// Maticsoft
        /// </summary>        
        private string _barcode;
        public string BarCode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
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
        /// Position
        /// Maticsoft
        /// </summary>        
        private int _position;
        public int CellPosition
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <summary>
        /// CellScanTime
        /// Maticsoft
        /// </summary>        
        private DateTime? _cellscantime;
        public DateTime? CellScanTime
        {
            get { return _cellscantime; }
            set { _cellscantime = value; }
        }
        /// <summary>
        /// OverTime
        /// Maticsoft
        /// </summary>        
        private DateTime? _overtime;
        public DateTime? OverTime
        {
            get { return _overtime; }
            set { _overtime = value; }
        }
        /// <summary>
        /// Marking
        /// Maticsoft
        /// </summary>        
        private string _marking;
        public string Marking
        {
            get { return _marking; }
            set { _marking = value; }
        }
        /// <summary>
        /// Remark1
        /// Maticsoft
        /// </summary>        
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// Remark2
        /// Maticsoft
        /// </summary>        
        private string _remark2;
        public string Remark2
        {
            get { return _remark2; }
            set { _remark2 = value; }
        }

        public class CellInfoORMMapper : ClassMapper<CellInfoModel>
        {
            public CellInfoORMMapper()
            {
                base.Table("CellInfo");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }

    }
}
