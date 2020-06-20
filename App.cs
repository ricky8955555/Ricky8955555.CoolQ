using HuajiTech.CoolQ.Events;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ
{
    abstract class App
    {
        public abstract string Name { get; }
        public abstract string DisplayName { get; }
        public virtual bool IsInternalEnabled { get; } = true;
        public virtual bool IsEnabledByDefault { get; } = true;
        public virtual bool IsForAdministrator { get; } = false;
        public virtual bool CanDisable { get; } = true;
        public virtual Feature[] Features { get; } = new Feature[] { };

        public void Run(MessageReceivedEventArgs e)
        {
            if (IsInternalEnabled &&
            ((!CanDisable) ||
            AppConfig.Config[e.Source.ToString(true)][Name].ToObject<bool>()))
            {
                if ((!IsForAdministrator) ||
                    (IsForAdministrator && Administrator == e.Sender.Number))
                    FeatureInvoker(e);
            }
        }

        private void FeatureInvoker(MessageReceivedEventArgs e)
        {
            foreach (var feature in Features)
                feature.Invoke(e);
        }
    }
}
