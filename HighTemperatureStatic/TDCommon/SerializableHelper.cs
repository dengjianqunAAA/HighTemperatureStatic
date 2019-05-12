using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class SerializableHelper<T>
    {
        public static T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>( json); 
        }
        
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


        public static T DeserializeEx(object obj)
        {
            return JsonConvert.DeserializeObject<T>(Serialize(obj));
        }

        public static string SerializeEx(string json)
        {
            return JsonConvert.SerializeObject(Deserialize(json));
        }
    }
}
