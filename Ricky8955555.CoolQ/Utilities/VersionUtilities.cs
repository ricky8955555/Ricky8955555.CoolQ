using System;

namespace Ricky8955555.CoolQ
{
    internal static class VersionUtilities
    {
        internal static DateTime ToDateTime(Version version) => new DateTime(2000, 1, 1) + TimeSpan.FromDays(version.Build) + TimeSpan.FromSeconds(version.Revision * 2);
    }
}