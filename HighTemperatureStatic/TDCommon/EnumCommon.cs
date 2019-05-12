using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class EnumCommon
    {
        public enum ShearType
        {
            None,
            Shear,
        }
        /// <summary>
        /// 性别
        /// </summary>
        public enum Gender
        {
            [Description("男")]
            Male,
            [Description("女")]
            Female,
        }
    }
}
