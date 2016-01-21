using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTFServiceHook
{
    public class PathConstants
    {
        private static string _logConfig;
        private static string _settingsConfig;

        public static string LogConfig
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_logConfig))
                {
                    _logConfig = PathResolver.GetPath("web.config");
                }
                return _logConfig;
            }
        }

        public static string SettingsConfig
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_settingsConfig))
                {
                    _settingsConfig = PathResolver.GetPath("config.json");
                }
                return _settingsConfig;
            }
        }
    }
}