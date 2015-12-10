using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Logging;

namespace Utilities.Settings
{
    /// <summary>
    /// A global application settings helper
    /// </summary>
    public static class SettingsHelper
    {
        private static readonly ILogger Logger = new Logger(typeof(SettingsHelper));

        // i don't remember what the thinking was w/ this feature
        // different configs? different sections in config?
        // DB (user) vs. XML (system) ?
        public enum SettingsType
        {
            User,
            System,
        }
        
        /// <summary>
        /// SettingsType.User + type default for "default value"
        /// </summary>
        public static T Get<T>(string key)
        {
            return Get(SettingsType.User, key, default(T));
        }

        /// <summary>
        /// Default to SettingsType.User
        /// </summary>
        public static T Get<T>(string key, T defaultValue)
        {
            return Get(SettingsType.User, key, defaultValue);
        }

        /// <summary>
        /// If you wanted to change the config storage location from app/web config to, say, DB - here is where you would do that
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">A tag to note different types of settings</param>
        /// <param name="key">The setting key name like "Security:ValidUserNameCharacters"</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(SettingsType type, string key, T defaultValue)
        {
            Logger.Trace(defaultValue != null
                ? $"Retrieving setting {key} with default {defaultValue}"
                : $"Retrieving setting {key} but the default is <NULL>");

            if (ConfigurationManager.AppSettings[key] == null)
                return defaultValue;
            
            try
            {
                object value = ConfigurationManager.AppSettings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }

            catch (Exception ex)
            {                
                Logger.Log(LoggingEventType.Error, ex, 
                    string.Format("Could not convert setting for type {2} so saved as type default: {0} instead of passed default: {1}", 
                    default(T), defaultValue, typeof(T).FullName));
                return default(T);
            }
        }
    }
}
