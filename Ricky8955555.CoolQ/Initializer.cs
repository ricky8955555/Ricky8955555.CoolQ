using HuajiTech.CoolQ.Events;

namespace Ricky8955555.CoolQ
{
    internal static class Initializer
    {
        public static Main Init()
        {
            // IBotEventSource botEventSource = BotEventSource.Instance;
            ICurrentUserEventSource currentUserEventSource = CurrentUserEventSource.Instance;
            // IGroupEventSource groupEventSource = GroupEventSource.Instance;

            return new Main(currentUserEventSource);
        }
    }
}