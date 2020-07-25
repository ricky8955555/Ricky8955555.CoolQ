using HuajiTech.CoolQ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    internal static class AppUtilities
    {
        internal static IEnumerable<AppBase> GetApps(IChattable source)
        {
            var apps = AppBase.Apps.Where(x => x is App);
            if (source is IGroup)
                return apps.Concat(AppBase.Apps.Where(x => x is GroupApp));
            else if (source is IUser)
                return apps.Concat(AppBase.Apps.Where(x => x is UserApp));
            else
                return apps;
        }

        internal static IEnumerable<AppBase> GetApps(IChattable source, IUser user)
        {
            var apps = AppBase.Apps.Where(x => x is App && x.IsAllowed(user));
            if (source is IGroup)
                return apps.Concat(AppBase.Apps.Where(x => x is GroupApp && x.IsAllowed(user)));
            else if (source is IUser)
                return apps.Concat(AppBase.Apps.Where(x => x is UserApp && x.IsAllowed(user)));
            else
                return apps;
        }
    }
}