using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace TengDaWebApi.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost]
        // POST api/values
        public string Post(string value)
        {
            return value;
        }
        public HttpResponseMessage PostUser(string user)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string str = serializer.Serialize(user);
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
