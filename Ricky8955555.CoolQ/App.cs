using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using System.Linq;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ
{
    abstract class AppBase
    {
        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public virtual bool IsInternalEnabled { get; } = true;

        public virtual bool IsEnabledByDefault { get; } = true;

        public virtual bool CanDisable { get; } = true;

        public virtual AppPermission Permission { get; } = AppPermission.Everyone;

        public virtual AppPriority Priority { get; } = AppPriority.Normal;

        public virtual Feature[] Features { get; } = new Feature[] { };

        public abstract void Run(MessageReceivedEventArgs e);

        protected void FeatureInvoker(MessageReceivedEventArgs e)
        {
            foreach (var feature in Features)
            {
                feature.Invoke(e);
                if (feature.Handled)
                {
                    Handled = true;
                    feature.Handled = false;
                    break;
                }
            }
        }

        public bool Handled { get; set; } = false;

        public bool IsEnabled(IChattable source) => !CanDisable || AppConfig.Config[source.ToString(true)][Name].ToObject<bool>();

        public bool IsAllowed(IUser user)
        {
            switch (Permission)
            {
                case AppPermission.Everyone:
                    return true;
                case AppPermission.Administrator:
                    return !(user is IMember member) || (member.IsAdministrator || user.Number == Owner);
                case AppPermission.Owner:
                    return user.Number == Owner;
                default:
                    return false;
            }
        }
    }

    abstract class App : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    abstract class GroupApp : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IGroup && IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    abstract class UserApp : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IUser && IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    enum AppPermission
    {
        Everyone,
        Administrator,
        Owner
    }

    enum AppPriority
    {
        Highest,
        Higher,
        Normal,
        Lower,
        Lowest
    }
}
