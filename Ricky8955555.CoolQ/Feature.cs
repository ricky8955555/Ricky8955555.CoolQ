using HuajiTech.CoolQ.Events;

namespace Ricky8955555.CoolQ
{
    internal abstract class Feature
    {
        internal virtual string Usage { get; } = null;

        internal abstract void Invoke(MessageReceivedEventArgs e);

        internal bool Handled { get; set; } = false;
    }
}