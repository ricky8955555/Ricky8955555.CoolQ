using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;

namespace Ricky8955555.CoolQ
{
    public static class AppUtilities
    {
        public static IEnumerable<AppBase> GetApps(IChattable source)
        {
            var apps = AppBase.Apps.Where(x => x is App);

            if (source is IGroup)
                return apps.Concat(AppBase.Apps.Where(x => x is GroupApp));
            else if (source is IUser)
                return apps.Concat(AppBase.Apps.Where(x => x is UserApp));
            else
                return apps;
        }

        public static IEnumerable<AppBase> GetApps(IChattable source, IUser user)
        {
            return GetApps(source).Where(app => app.IsAllowed(user));
        }
    }
}