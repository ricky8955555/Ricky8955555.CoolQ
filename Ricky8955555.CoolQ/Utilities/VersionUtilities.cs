﻿using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ
{
    public static class VersionUtilities
    {
        public static Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;

        public static Version GetTagVersion()
        {
            var version = GetVersion();
            return new Version(version.Major, version.Minor, Constants.TagBuild);
        }

        public static DateTime ToDateTime(Version version) => new DateTime(2000, 1, 1) + TimeSpan.FromDays(version.Build) + TimeSpan.FromSeconds(version.Revision * 2);

        public static bool GetLatestVersion(out Version version, out string downloadUri)
        {
            version = null;
            downloadUri = null;

            try
            {
                if (HttpUtilities.HttpGet(Resources.VersionInfoURL, out string content))
                {
                    var json = JObject.Parse(content);
                    var versionJson = json["version"];
                    version = new Version(versionJson["major"].ToObject<int>(), versionJson["minor"].ToObject<int>(), versionJson["build"].ToObject<int>());
                    downloadUri = json["download"].ToString();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}