using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel.Permissions;
using TengDa.DB.Base;

namespace TDDB
{
    public class PermissionsDB : RepositoryBase<PermissionsModel>
    {
        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        public List<PermissionsModel> GetList()
        {
            return this.GetAll().ToList();
        }


        /// <summary>
        /// 修改菜单名称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePermissions(PermissionsModel model)
        {
            return this.Update(model);
        }

        /// <summary>
        /// 修改菜单排序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="up"></param>
        public void UpdatePermissionsToUpDown(PermissionsModel model,int up)
        {

            PermissionsModel permission = this.GetById(model.PermissionId);
            List<PermissionsModel> list = this.GetList();
            List<PermissionsModel> lst= (from m in list
                                         where m.ParentId == model.ParentId
                                         orderby m.DisplayOrder
                                         select m).ToList();
            permission = list.Where(m => m.PermissionId == model.PermissionId).First();
            int num = 0;
            for (int index = 0; index < list.Count; ++index)
            {
                PermissionsModel permissionInfo = list[index];
                permissionInfo.DisplayOrder = index + 1;
                if (ReferenceEquals(permissionInfo, permission))
                    num = index;
            }
            if (up == 1)
            {
                if (num > 0)
                {
                    ++list[num - 1].DisplayOrder;
                    --permission.DisplayOrder;
                }
            }
            else if (num + 1 < list.Count)
            {
                --list[num + 1].DisplayOrder;
                ++permission.DisplayOrder;
            }
            this.UpdatePermissions(list);
        }
        public  void UpdatePermissions(IList<PermissionsModel> permissionInfos)
        {
            foreach (PermissionsModel var in permissionInfos)
            {
                UpdatePermissionsInfo(var);
            }
        }
        public int  UpdatePermissionsInfo(PermissionsModel info)
        {
            if (info.ParentId.HasValue)
            {
                string sql = string.Format(
                @"UPDATE [Permissions]   SET [PermissionName] = '{1}',[PermissionCode] = '{2}',[Remark] = '{3}' ,[DisplayOrder] = '{4}'  ,[ParentId] = '{5}' ,[Depth] = '{6}'
                    WHERE   PermissionId = {0}"
              , info.PermissionId, info.PermissionName, info.PermissionCode, info.Remark, info.DisplayOrder, info.ParentId, info.Depth);
              return   this.Execute(sql);
            }
            else
            {
                string sql = string.Format(
                @"update Permissions  
                    set
			         PermissionName = '{1}',
			         PermissionCode = '{2}',
			         Remark = '{3}',
                     DisplayOrder = {4},
			         Depth = {5}
                    where  PermissionId = {0}"
              , info.PermissionId, info.PermissionName, info.PermissionCode, info.Remark, info.DisplayOrder, info.Depth);
                return this.Execute(sql);
            }
        }
    }
}
