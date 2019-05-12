using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using DapperExtensions.Lambda;
using TengDa.DataAccess;
using TengDa.DataAccess.Utils;
using Dapper;
using DapperExtensions;

namespace TengDa.DB.Base
{
    /// <summary>
    /// Repository基类
    /// </summary>
    public class RepositoryBase<T> : RepositoryServiceBase<T>, IDataRepository<T>
                where T : class
    {

        public RepositoryBase(string connKey = "DefaultConnection") : base(connKey)
        {
        }



        #region 传入Sql 语句执行
        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IEnumerable<T> Get(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query<T>(sql, param as object, ConnObj.DbTransaction, true, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query<T>(sql, param as object, ConnObj.DbTransaction, true, commandTimeout, commandType);
                }
            }
        }

        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IEnumerable<TReturn> Get<TReturn>(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where TReturn : class
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query<TReturn>(sql, param as object, ConnObj.DbTransaction, true, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query<TReturn>(sql, param as object, ConnObj.DbTransaction, true, commandTimeout, commandType);
                }
            }
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="allRowsCountSql"></param>
        /// <param name="allRowsCountParam"></param>
        /// <returns></returns>
        protected IEnumerable<T> GetPage(int pageIndex, int pageSize, out long allRowsCount, string sql, dynamic param = null, string allRowsCountSql = null, dynamic allRowsCountParam = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj();
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.GetPage<T>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, ConnObj.DbTransaction, commandTimeout, ConnObj.dbType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.GetPage<T>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, ConnObj.DbTransaction, commandTimeout, dbType);
                }
            }
        }
    

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="allRowsCountSql"></param>
        /// <param name="allRowsCountParam"></param>
        /// <returns></returns>
        protected IEnumerable<TReturn> GetPage<TReturn>(int pageIndex, int pageSize, out long allRowsCount, string sql, dynamic param = null, string allRowsCountSql = null, dynamic allRowsCountParam = null, int? commandTimeout = null, CommandType? commandType = null) where TReturn : class
        {
            DbConnObj ConnObj = GetConnObj();
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.GetPage<TReturn>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, ConnObj.DbTransaction, commandTimeout, ConnObj.dbType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.GetPage<TReturn>(pageIndex, pageSize, out allRowsCount, sql, param as object, allRowsCountSql, ConnObj.DbTransaction, commandTimeout, dbType);
                }
            }

        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected IEnumerable<T> Get<TFirst, TSecond>(string sql, Func<TFirst, TSecond, T> map,
            dynamic param = null, IDbTransaction transaction = null, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
                }
            }
        }


        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null) where TReturn : class
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
                }
            }
        }


        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected IEnumerable<T> Get<TFirst, TSecond, TThird>(string sql, Func<TFirst, TSecond, TThird, T> map,
            dynamic param = null, IDbTransaction transaction = null,  string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
                }
            }
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null,  string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null) where TReturn : class
        {
            DbConnObj ConnObj = GetConnObj(transaction);
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Query(sql, map, param as object, ConnObj.DbTransaction, true, splitOn, commandTimeout, commandType);
                }
            }
        }



        /// <summary>
        /// 获取多实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected SqlMapper.GridReader GetMultiple(string sql, dynamic param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj(transaction);           
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.QueryMultiple(sql, param as object, ConnObj.DbTransaction, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.QueryMultiple(sql, param as object, ConnObj.DbTransaction, commandTimeout, commandType);
                }
            }
        }



        public  List<T> QueryPageSource( int pageIndex, int pageSize, out long AllRowsCount, string sql, string key, string sort = "")
        {
            int PageEnd = pageIndex * pageSize;
            pageIndex = (pageIndex - 1) * pageSize;
            string sqldb = string.Format(@"select COUNT(*) as AllRowsCount from ({0} ) as TT where 1=1;
                           select * from (select ROW_NUMBER() over(order by {4}) as rowid,* from 
                           ({0}) as aa )  t where t.rowid > '{1}' and t.rowid <= '{2}' ; ", sql, pageIndex, pageSize, PageEnd, key, sort);
            var result = this.GetMultiple(sqldb);
            AllRowsCount = 0;
            //遍历结果集  
            while (!result.IsConsumed)
            {
                if (AllRowsCount == 0)
                {
                    var data = result.Read();
                    if (data != null && data.Count() > 0)
                    {
                        AllRowsCount = data.ToList()[0].pageCount;
                    }
                }
                else
                {
                    return result.Read<T>().ToList();
                }
            }
            return new List<T>();
        }

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected int Execute(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            DbConnObj ConnObj = GetConnObj(transaction);         
            if (ConnObj.DbConnection != null)
            {
                return ConnObj.DbConnection.Execute(sql, param as object, ConnObj.DbTransaction, commandTimeout, commandType);
            }
            else
            {
                DataBaseType dbType;
                using (IDbConnection conn = DBUtils.CreateDBConnection(ConnKey, out dbType))
                {
                    return conn.Execute(sql, param as object, ConnObj.DbTransaction, commandTimeout, commandType);
                }
            }
        }

        #endregion






    }
}
