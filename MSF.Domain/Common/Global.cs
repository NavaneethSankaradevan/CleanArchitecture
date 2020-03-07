using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSF.Domain
{
    public static class Global
    {
        public static IEnumerable<string> RoleList { get { return Enum.GetNames(typeof(Role)); } }

        public static IEnumerable<string> GenderList { get { return Enum.GetNames(typeof(Gender)); } }

        private static IEnumerable<T> GetFromJSon<T>()
            where T : class
        {
            string filePath = string.Empty;
            switch (typeof(T).Name)
            {
                case "Country":
                    filePath = "Countries.json";
                    break;
                default:
                    break;
            }

            try
            {
                var data = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            }
            catch
            {
                return new List<T>();
            }
        }

    }
}
