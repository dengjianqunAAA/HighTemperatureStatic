using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.UserManage;
using TengDa.DB.Base;

namespace TDDB
{
    public class RolesDB : RepositoryBase<RolesModel>
    {
        private static RolesDB _rolesDB = new RolesDB();
        public static RolesDB rolesDB
        {
            get { return _rolesDB; }
        }

       
        public List<RolesModel> GetRolesList()
        {
            return this.GetAll().ToList();
        }


        public bool UpdateRolesById(RolesModel model)
        {
            return this.Update(model);
        }
    }
}
