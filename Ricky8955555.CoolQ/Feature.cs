using HuajiTech.CoolQ.Events;

namespace Ricky8955555.CoolQ
{
    public abstract class Feature
    {
        public virtual string Usage { get; } = null;

        public abstract void Invoke(MessageReceivedEventArgs e);

        public bool Handled { get; set; } = false;
    }
}