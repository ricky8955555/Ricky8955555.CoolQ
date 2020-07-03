using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;

namespace Ricky8955555.CoolQ
{
    abstract class Feature
    {
        public virtual string Usage { get; } = null;

        public abstract void Invoke(MessageReceivedEventArgs e);

        public bool Handled { get; set; } = false;

        public void NotifyIncorrectUsage(MessageReceivedEventArgs e)
        {
            e.Reply(string.Format(Resources.FeatureIncorrectUsage, Usage));
        }
    }
}
