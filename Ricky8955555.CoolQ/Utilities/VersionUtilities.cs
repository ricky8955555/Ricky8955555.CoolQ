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
    }
}