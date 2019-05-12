using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.UserManage
{
    public class RolesModel
    {
        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        public string PermissionIds { get; set; }
    }



    public class RolesORMMapper : ClassMapper<RolesModel>
    {
        public RolesORMMapper()
        {
            base.Table("Roles");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
