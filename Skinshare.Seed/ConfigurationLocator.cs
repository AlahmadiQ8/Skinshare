using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Skinshare.Seed
{
    public class ConfigurationLocator
    {
        public static IConfiguration GetDevelopmentConfiguration()
        {
            var projectDir = GetConfigurationDirectory();
            var appSettingsPath = Path.Combine(projectDir, "appsettings.Development.json");
            var config = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath)
                .Build();
            return config;
        }

        private static string GetConfigurationDirectory()
        {
            var workingDir = Environment.CurrentDirectory;
            var res = Directory.EnumerateDirectories(workingDir)
                .Single(s => s.Contains("Skinshare.Web"));
            
            return res;
        }
    }
}