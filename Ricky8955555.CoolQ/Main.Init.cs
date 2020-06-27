using HuajiTech.CoolQ.Events;

namespace Ricky8955555.CoolQ
{
    public static partial class Main
    {
        public static void Init()
        {
            ICurrentUserEventSource currentUserEventSource = CurrentUserEventSource.Instance;
            currentUserEventSource.AddMessageReceivedEventHandler(MessageReceived);
        }
    }
}
