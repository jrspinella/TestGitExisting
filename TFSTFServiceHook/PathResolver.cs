using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTFServiceHook
{
    public class PathResolver
    {
        public static string GetPath(string path)
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + path;
        }
    }
}