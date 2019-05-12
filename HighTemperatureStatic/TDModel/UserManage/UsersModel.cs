using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
    public class UsersModel
    {
        private int userId;
        private string userNumber;
        private string userPwd;
        private string userName;
        private int? roleId;
        private string gender;
        private DateTime lastLoginTime;
        private DateTime createTime;
        private string pageRole;
        



        /// <summary>
        /// ID
        /// </summary>
        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserNumber
        {
            get
            {
                return userNumber;
            }

            set
            {
                userNumber = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd
        {
            get
            {
                return userPwd;
            }

            set
            {
                userPwd = value;
            }
        }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int? RoleId
        {
            get
            {
                return roleId;
            }

            set
            {
                roleId = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
            }
        }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime LastLoginTime
        {
            get
            {
                return lastLoginTime;
            }

            set
            {
                lastLoginTime = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }

        /// <summary>
        /// 页面权限
        /// </summary>
        public string PageRole
        {
            get
            {
                return pageRole;
            }

            set
            {
                pageRole = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public class UsersORMMapper : ClassMapper<UsersModel>
        {
            public UsersORMMapper()
            {
                base.Table("Users");
                //Map(f => f.UserID).Ignore();//设置忽略
                //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
                AutoMap();
            }
        }
    }
}
