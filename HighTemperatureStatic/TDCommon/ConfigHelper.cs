using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class ConfigHelper
    {
        /// <summary>
        /// 返回＊.exe.config文件中appSettings配置节的value项
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            try
            {
                return ConfigurationManager.AppSettings[strKey];
            }
            catch { return ""; }
        }
        /// <summary>
        /// 在＊.exe.config文件中appSettings配置节增加一对键、值对
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="newValue"></param>
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            // Open App.Config of executable
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // You need to remove the old settings object before you can replace it
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // Add an Application Setting.
            config.AppSettings.Settings.Add(newKey, newValue);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
