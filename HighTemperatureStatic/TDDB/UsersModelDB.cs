using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TDModel.UserManage;
using TengDa.DB.Base;

namespace TDDB
{
   public class UsersModelDB : RepositoryBase<UsersModel>
	{
		private static UsersModelDB _usersModelDB = new UsersModelDB();

		public static  UsersModelDB usersModelDB
		{
			get { return _usersModelDB ; }

        }
        public List<UsersModel> Login(string UserName ,string PassWord)
        {
            string sql =string.Format( "select top 1 * from Users where UserName='{0}' and UserPwd='{1}'",UserName,PassWord);
            List<UsersModel> list = new List<UsersModel>();
            return Get(sql).ToList();
        }
        /// <summary>
        /// 用户分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="AllRowsCount"></param>
        /// <returns></returns>
        public List<UsersModel> PageList(int PageIndex, int PageSize, out long AllRowsCount)
        {
            return this.GetPageList(PageIndex, PageSize, out AllRowsCount).ToList();
        }
        /// <summary>
        /// 添加用户数据
        /// </summary>
        /// <param name="Users">用户UsersModel</param>
        /// <returns></returns>
        public int InsertUserInfo(UsersModel Users)
        {
            string sql = string.Format(@"INSERT INTO dbo.Users( UserNumber ,UserPwd ,UserName ,RoleId ,Gender ,LastLoginTime ,CreateTime , pageRole)
VALUES('{0}', '{1}', '{2}', {3}, '{4}', GETDATE(), GETDATE(), '{5}') ", Users.UserNumber, Users.UserPwd, Users.UserName, Users.RoleId, Users.Gender, 0);
            return this.Execute(sql);
        }
    }
}
