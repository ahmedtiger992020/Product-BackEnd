using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System;

namespace Sample.SharedKernal.Localization
{
    public class BaseFileReader
    {
        #region Vars
        private Dictionary<string, string> ResourceData { get; set; }
        #endregion

        public BaseFileReader()
        {

        }

        #region Load Messeges From jsonFile
        private bool LoadData(string _culture)
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = "";
            switch (_culture)
            {
                case"ar":
                    fileName = "Messages_ar";
                    break;
                case"en":
                    fileName = "Messages_en";
                    break;
            }
            try
            {
                ResourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine(rootDir, @"ResourceFiles\Messages", $"{fileName}.json")));

            }
            catch (Exception e)
            {

                throw;
            }
            return ResourceData?.Any() ?? default;
        }
        #endregion




        #region Get Data  
        protected string GetKeyValue(string key, string _culture)
        {
            return LoadData(_culture) ? ResourceData[key] : key;
        }

        #endregion
    }
}
