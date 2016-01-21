using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class SettingsLoader
    {
        private static DateTime? DateModified;
        private static ServiceHookSettings _cachedSettings;

        static SettingsLoader()
        {
            DateModified = null;
        }

        public static async Task<ServiceHookSettings> LoadFromConfigFileAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var fileToOpen = string.Empty;
            if (Path.IsPathRooted(path))
            {

                fileToOpen = path;
            }
            else
            {
                var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                fileToOpen = Path.Combine(executionPath, path);
            }
            if (!File.Exists(fileToOpen))
            {
                return null;
            }
            try
            {
                var lastWriteTime = new FileInfo(fileToOpen).LastWriteTimeUtc;
                var requiresLoad = false;
                if ((_cachedSettings == null || DateModified == null) || (DateModified != null && new FileInfo(fileToOpen).LastWriteTimeUtc > DateModified))
                {
                    DateModified = lastWriteTime;
                    requiresLoad = true;
                }
                if (requiresLoad)
                {
                    using (var reader = File.OpenText(fileToOpen))
                    {
                        var fileContents = await reader.ReadToEndAsync().ConfigureAwait(false);
                        var settings = JsonConvert.DeserializeObject<ServiceHookSettings>(fileContents);
                        _cachedSettings = settings;
                        return settings;
                    }
                }
                else
                {
                    return _cachedSettings;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static ServiceHookSettings LoadFromConfigFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var fileToOpen = string.Empty;
            if (Path.IsPathRooted(path))
            {

                fileToOpen = path;
            }
            else
            {
                var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                fileToOpen = Path.Combine(executionPath, path);
            }
            if (!File.Exists(fileToOpen))
            {
                return null;
            }
            try
            {
                var lastWriteTime = new FileInfo(fileToOpen).LastWriteTimeUtc;
                var requiresLoad = false;
                if ((_cachedSettings == null || DateModified == null) || (DateModified != null && new FileInfo(fileToOpen).LastWriteTimeUtc > DateModified))
                {
                    DateModified = lastWriteTime;
                    requiresLoad = true;
                }
                if (requiresLoad)
                {
                    using (var reader = File.OpenText(fileToOpen))
                    {
                        var fileContents = reader.ReadToEnd();
                        var settings = JsonConvert.DeserializeObject<ServiceHookSettings>(fileContents);
                        _cachedSettings = settings;
                        return settings;
                    }
                }
                else
                {
                    return _cachedSettings;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
