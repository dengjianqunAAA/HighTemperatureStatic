﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TDModel
{
    public class ApiResultModel
    {
        private HttpStatusCode statusCode;

        private object data;

        //private string errorMessage;

        //private bool isSuccess;

        /// <summary>
        /// 状态代码
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        ///// <summary>
        ///// 错误消息
        ///// </summary>
        //public string ErrorMessage
        //{
        //    get { return errorMessage; }
        //    set { errorMessage = value; }
        //}

        ///// <summary>
        ///// 是否成功
        ///// </summary>
        //public bool IsSuccess
        //{
        //    get { return isSuccess; }
        //    set { isSuccess = value; }
        //}


        public string Code { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

    }
}
