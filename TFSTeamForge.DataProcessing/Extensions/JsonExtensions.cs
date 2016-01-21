using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing
{
    public static class JsonExtensions
    {
        public static string RetrieveFieldValue(this IDictionary<string, object> fields, string key)
        {
            var result = string.Empty;

            if (fields == null)
            {
                return result;
            }
            if (fields.ContainsKey(key))
            {
                result = fields[key].ToString();
            }

            return result;
        }

        public static TType RetrieveFieldValue<TType>(this IDictionary<string, object> fields, string key)
        {
            var result = default(TType);

            if (fields == null)
            {
                return result;
            }
            if (fields.ContainsKey(key))
            {
                result = (TType)fields[key];
            }

            return result;
        }

        public static string RetrieveFieldValue(this JObject fields, string key)
        {
            var result = string.Empty;

            if (fields == null)
            {
                return result;
            }
            var token = (from p in fields.Properties()
                         where string.Compare(key, p.Name, true) == 0
                         select fields[p.Name]).FirstOrDefault();

            if (token != null)
            {
                result = token.Value<string>();
            }

            return result;
        }

        public static TType RetrieveFieldValue<TType>(this JObject fields, string key)
        {
            var result = default(TType);

            if (fields == null)
            {
                return result;
            }
            var token = (from p in fields.Properties()
                         where string.Compare(key, p.Name, true) == 0
                         select fields[p.Name]).FirstOrDefault();

            if (token != null)
            {
                result = token.Value<TType>();
            }

            return result;
        }
    }
}