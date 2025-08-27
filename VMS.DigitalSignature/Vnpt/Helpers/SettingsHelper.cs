using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api.Helpers
{
    public class SettingsHelpers
    {
        public static void AddOrUpdateAppSetting(IConfiguration configuration, string env, string sectionPathKey, string value)
        {
            try
            {
                //var filePath = @"E:\Viet Ba\VMS.HisLis.DotNetCore30Api\appsettings.json";
                var filePath = string.Format(@"{0}\appsettings{1}.json", AppContext.BaseDirectory, 
                    env.ToLower() == "development" ? "." + env : string.Empty);
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                SetValueRecursively(sectionPathKey, jsonObj, value);

                string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
                configuration[sectionPathKey] = value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing app settings | {0}", ex.Message);
            }
        }

        private static void SetValueRecursively<T>(string sectionPathKey, dynamic jsonObj, T value)
        {
            // split the string at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);

            var currentSection = remainingSections[0];
            if (remainingSections.Length > 1)
            {
                // continue with the procress, moving down the tree
                var nextSection = remainingSections[1];
                SetValueRecursively(nextSection, jsonObj[currentSection], value);
            }
            else
            {
                // we've got to the end of the tree, set the value
                jsonObj[currentSection] = value;
            }
        }
    }
}
