using HuajiTech.CoolQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricky8955555.CoolQ
{
    static class Utilities
    {
        public static IEnumerable<AppBase> GetApps(IChattable source)
        {
            bool isGroup = source is IGroup;
            bool isUser = source is IUser;
            return Commons.Apps.Where(x => x is App || (isGroup ? x is GroupApp : (isUser ? x is UserApp : false)));
        }
    }
}
