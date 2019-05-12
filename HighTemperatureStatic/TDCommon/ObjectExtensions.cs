using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommom
{
    public static class ObjectExtensions
    {
        #region 公共方法
        /// <summary>
        /// 使用 Newtonsoft.Json.JsonConvert 序列化 对象 为 json 字符串数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeFormat">时间格式</param>
        /// <param name="clearEnter">清除回车符</param>
        /// <returns></returns>
        public static string ToJsonString(this object value, string timeFormat = "yyyy-MM-dd HH:mm:ss", bool clearEnter = true)
        {
            if (string.IsNullOrEmpty(timeFormat))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
            Formatting format = Formatting.None;
            if (!clearEnter) //控制是否需要制表符
            {
                format = Formatting.Indented;
            }
            var returnStr = Newtonsoft.Json.JsonConvert.SerializeObject(value, format, GetTimeConverter(timeFormat));
            return returnStr;
        }
        /// <summary>
        /// 根据 timeFormat 创建 IsoDateTimeConverter 对象
        /// </summary>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        private static Newtonsoft.Json.Converters.IsoDateTimeConverter GetTimeConverter(string timeFormat)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            string timeformat = string.Empty;
            foreach (char c in timeFormat)
            {
                if ("YMDHSFymdhsf".IndexOf(c) < 0) //非日期时间的占位符,需要用 ''包含起来
                {
                    timeformat += string.Format("'{0}'", c);
                }
                else
                {
                    timeformat += c;
                }
            }
            timeConverter.DateTimeFormat = timeformat;
            return timeConverter;
        }
        /// <summary>
        /// 反序列
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value, string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (string.IsNullOrEmpty(timeFormat))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, GetTimeConverter(timeFormat));
            }
            catch (Exception ex)
            { }
            return default(T);
        }
        /// <summary>
        /// 序列号成指定类型,以Object方式返回
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static object FormJsonString(this string value, Type type, string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (string.IsNullOrEmpty(timeFormat))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type);
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type, GetTimeConverter(timeFormat));
            }
            catch (Exception ex)
            { }
            return null;
        }
        /// <summary>
        /// 根据指定Json的属性名称获取对应属性的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objectKey">指定的Json中Key</param>
        /// <returns></returns>
        public static string JsonStringParse(this string value, string objectKey)
        {
            if (string.IsNullOrEmpty(objectKey))
            {
                return string.Empty;
            }
            try
            {
                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(value);
                if (null != jObject)
                {
                    return jObject[objectKey].ToString();
                }
            }
            catch (Exception)
            { }
            return string.Empty;
        }
        #endregion


        public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class
        {
            ObjType s = JsonConvert.DeserializeObject<ObjType>(JsonString);
            return s;
        }
    }
}
