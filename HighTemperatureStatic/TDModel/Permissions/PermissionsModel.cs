using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.Permissions
{
    public class PermissionsModel : ViewModelBase
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; }
        public string Remark { get; set; }

        public int Depth { get; set; }
        
    }

    public class MRoleTreeData : PermissionsModel
    {
        protected bool? isChecked = false;
        /// <summary>
        /// 是否被勾选
        /// </summary>
        public bool? IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                this.RaisePropertyChanged("IsChecked");
                SetIsCheckedByParent(value);
                if (Parent != null)
                    Parent.SetIsCheckedByChild(value);
            }
        }

        protected bool isExpanded;
        /// <summary>
        /// 是否被展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                this.RaisePropertyChanged("IsExpanded");
            }
        }


        protected MRoleTreeData parent;
        /// <summary>
        /// 父项目
        /// </summary>
        public MRoleTreeData Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        protected List<MRoleTreeData> children = new List<MRoleTreeData>();
        /// <summary>
        /// 含有的子项目
        /// </summary>
        public List<MRoleTreeData> Children
        {
            get { return children; }
            set { children = value; }
        }


        /// <summary>
        /// 包含的对象
        /// </summary>
        public object Tag { get; set; }

        #region 业务逻辑, 如果你需要改成其他逻辑, 要修改的也就是这两行

        /// <summary>
        /// 子项目的isChecked改变了, 通知 是否要跟着改变 isChecked
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByChild(bool? value)
        {
            if (this.isChecked == value)
            {
                return;
            }

            if (this.Children.All(c => c.IsChecked == true))
            {
                this.isChecked = true;
            }
            else if (this.Children.All(c => c.IsChecked == false))
            {
                this.isChecked = false;
            }
            else
            {
                this.isChecked = null;
            }

            this.RaisePropertyChanged("IsChecked");
            if (Parent != null) Parent.SetIsCheckedByChild(value);
        }

        /// <summary>
        /// 自己的isChecked改变了, 所有子项目都要跟着改变
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByParent(bool? value)
        {
            this.isChecked = value;
            this.RaisePropertyChanged("IsChecked");
            foreach (var child in Children)
            {
                child.SetIsCheckedByParent(value);
            }
        }
        #endregion
    }

    public class PermissionsORMMapper : ClassMapper<PermissionsModel>
    {
        public PermissionsORMMapper()
        {
            base.Table("Permissions");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
