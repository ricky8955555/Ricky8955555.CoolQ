using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Ricky8955555.CoolQ
{
    internal static class VersionUtilities
    {
        internal static Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;

        internal static Version GetTagVersion()
        {
            var version = GetVersion();
            return new Version(version.Major, version.Minor, Constants.TagBuild);
        }

        internal static DateTime ToDateTime(Version version) => new DateTime(2000, 1, 1) + TimeSpan.FromDays(version.Build) + TimeSpan.FromSeconds(version.Revision * 2);

        internal static bool GetLatestVersion(out Version version, out string downloadUri)
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