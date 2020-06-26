using HuajiTech.CoolQ;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    static class Utilities
    {
        public static IEnumerable<AppBase> GetApps(IChattable source, IUser user)
        {
            var apps = Commons.Apps.Where(x => x is App && x.IsAllowed(user));
            if (source is IGroup)
                return apps.Concat(Commons.Apps.Where(x => x is GroupApp && x.IsAllowed(user)));
            else if (source is IUser)
                return apps.Concat(Commons.Apps.Where(x => x is UserApp && x.IsAllowed(user)));
            else
                return apps;
        }
    }
}
