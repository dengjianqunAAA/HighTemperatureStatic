using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace TDCommom
{
    public class DataTableHelper
    {
        // <summary>
        /// 获取对固定列不重复的新DataTable
        /// </summary>
        /// <param name="dt">含有重复数据的DataTable</param>
        /// <param name="colName">需要验证重复的列名</param>
        /// <returns>新的DataTable，colName列不重复，表格式保持不变</returns>
        public static DataTable GetDistinctTable(DataTable dt, string colName)
        {
            DataView dv = dt.DefaultView;
            DataTable dtCardNo = dv.ToTable(true, colName);
            DataTable Pointdt = new DataTable();
            Pointdt = dv.ToTable();
            Pointdt.Clear();
            for (int i = 0; i < dtCardNo.Rows.Count; i++)
            {
                DataRow dr = dt.Select(colName + "='" + dtCardNo.Rows[i][0].ToString() + "'")[0];
                Pointdt.Rows.Add(dr.ItemArray);
            }
            return Pointdt;
        }

        /// <summary>
        /// 筛选datatable 
        /// </summary>
        /// <param name="dt">初始数据集</param>
        /// <param name="condition">条件</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public static DataTable GetDataTableFilter(DataTable dt, string condition, string values)
        {
            DataTable dt2 = new DataTable();
            DataRow[] rows = dt.Select(condition + " = '" + values + "'");
            dt2 = dt.Clone(); //克隆A的结构
            foreach (DataRow row in rows)
            {
                dt2.ImportRow(row);//复制行数据
            }
            return dt2;
        }


        /// <summary>
        /// 筛选datatable 
        /// </summary>
        /// <param name="dt">初始数据集</param>
        /// <param name="condition">条件</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public static DataTable GetDataTableFilter(DataTable dt, string condition, string values,string orderBy)
        {
            DataTable dt2 = new DataTable();
            DataRow[] rows = dt.Select(condition + " = '" + values + "'");
            dt2 = dt.Clone(); //克隆A的结构
            foreach (DataRow row in rows)
            {
                dt2.ImportRow(row);//复制行数据
            }
            dt2.DefaultView.Sort = orderBy;
            return dt2;
        }

        /// <summary>
        /// DataTable行转列
        /// </summary>
        /// <param name="dtable">需要转换的表</param>
        /// <param name="head">转换表表头对应旧表字段（小写）</param>
        /// <returns></returns>
        public static DataTable DataTableRowtoCon(DataTable dtable, string head)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NumberID");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {//设置表头
                dt.Columns.Add(dtable.Rows[i][head].ToString());
            }
            for (int k = 0; k < dtable.Columns.Count-1; k++)
            {
                string temcol = dtable.Columns[k].ToString();
                if (dtable.Columns[k].ToString().ToLower() != head)//过滤掉设置表头的列
                {
                    DataRow new_dr = dt.NewRow();
                    new_dr[0] = dtable.Columns[k].ToString();
                    for (int j = 0; j < dtable.Rows.Count; j++)
                    {
                        string temp = dtable.Rows[j][k].ToString();
                        new_dr[j + 1] = (Object)temp;
                    }
                    dt.Rows.Add(new_dr);
                }
            }
            return dt;
        }


        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            // 定义集合 
            List<T> ts = new List<T>();
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行 
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    try
                    {
                        tempName = pi.Name;//将属性名称赋值给临时变量 
                                           //检查DataTable是否包含此列（列名==对象的属性名）  
                        if (dt.Columns.Contains(tempName))
                        {
                            //取值 
                            object value = dr[tempName];
                            //如果非空，则赋给对象的属性 
                            if (value != DBNull.Value)
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                    catch { }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }
     
        public static List<T> DataTableToList2<T>(DataTable dt) where T : class, new()
        {
            // 定义集合 
            List<T> ts = new List<T>();
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行 
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    try
                    {
                        tempName = pi.Name;//将属性名称赋值给临时变量 
                                           //检查DataTable是否包含此列（列名==对象的属性名）  
                        if (dt.Columns.Contains(tempName))
                        {
                            //取值 
                            object value = dr[tempName];
                            //如果非空，则赋给对象的属性 
                            if (value != DBNull.Value)
                            {
                                pi.SetValue(t, value.ToString(), null);
                            }
                        }
                    }
                    catch { }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 筛选datatable 
        /// </summary>
        /// <param name="dt">初始数据集</param>
        /// <param name="condition">条件</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public static DataTable GetDataTableFilter(DataTable dt, string condition, string values, string condition1, string values2,string orderby)
        {
            DataTable dt2 = new DataTable();
            DataRow[] rows = dt.Select(condition + " = '" + values + "' and "+ condition1+"='"+ values2 + "'");
            dt2 = dt.Clone(); //克隆A的结构
            foreach (DataRow row in rows)
            {
                dt2.ImportRow(row);//复制行数据
            }
            DataView dv = dt2.DefaultView;
            dv.Sort = orderby;
            return dv.ToTable();


        }

    }
}
