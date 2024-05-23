using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LoginSystem.Utility
{
    public static class SessionExtension
    {
        public static void setObjectAsJson(this ISession session,string key, object value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        } 

        public static T getObjectFromJson<T>(this ISession session,string key)
        {
            var result = session.GetString(key);

            return result == null ? default(T) : JsonConvert.DeserializeObject<T>(result); 
        }
    }
}
