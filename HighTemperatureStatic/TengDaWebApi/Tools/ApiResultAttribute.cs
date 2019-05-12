using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace TengDaWebApi.Tools
{
    public class ApiResultAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            base.OnActionExecuted(actionExecutedContext);

            // 不包裹返回值
            var noPackage = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoPackageResult>();
            if (!noPackage.Any())
            {
                //初始化返回结果
                ApiResultModel result = new ApiResultModel();
                if (actionExecutedContext.Exception != null)
                {
                    //result.StatusCode = 123;
                    result.Success = false;
                    result.Message = actionExecutedContext.Exception.Message;
                }
                else
                {
                    // 取得由 API 返回的状态代码
                    result.StatusCode = actionExecutedContext.ActionContext.Response.StatusCode;

                    var a = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>();
                    if (!a.IsFaulted)
                    {
                        // 取得由 API 返回的资料
                        result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                    }

                    //请求是否成功
                    result.Success = actionExecutedContext.ActionContext.Response.IsSuccessStatusCode;
                }


                //结果转为自定义消息格式
                HttpResponseMessage httpResponseMessage = JsonHelper.toJson(result);

                // 重新封装回传格式
                actionExecutedContext.Response = httpResponseMessage;
            }

        }
    }
        public class NoPackageResult : Attribute
        {

        }
    }
