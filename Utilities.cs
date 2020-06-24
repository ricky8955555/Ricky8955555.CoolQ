using HuajiTech.CoolQ;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    static class Utilities
    {
        public static IEnumerable<AppBase> GetApps(IChattable source, IUser user)
        {
            bool isGroup = source is IGroup;
            bool isUser = source is IUser;
            return Commons.Apps.Where(x => x is App || (isGroup ? x is GroupApp : (isUser ? x is UserApp : false)) && x.IsAllowed(user));
        }
    }
}
