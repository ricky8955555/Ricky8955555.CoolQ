using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ
{
    internal abstract class AppBase
    {
        internal abstract string Name { get; }

        internal abstract string DisplayName { get; }

        internal virtual bool IsInternalEnabled { get; } = true;

        internal virtual bool IsEnabledByDefault { get; } = true;

        internal virtual bool CanDisable { get; } = true;

        internal virtual AppPermission Permission { get; } = AppPermission.Everyone;

        internal virtual AppPriority Priority { get; } = AppPriority.Normal;

        internal virtual Feature[] Features { get; } = new Feature[] { };

        internal abstract void Run(MessageReceivedEventArgs e);

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

        internal bool Handled { get; set; } = false;

        internal bool IsEnabled(IChattable source) => !CanDisable || AppStatusConfig.Config[source.ToString(true)][Name].ToObject<bool>();

        internal bool IsAllowed(IUser user)
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

    internal abstract class App : AppBase
    {
        internal override void Run(MessageReceivedEventArgs e)
        {
            if (IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    internal abstract class GroupApp : AppBase
    {
        internal override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IGroup && IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    internal abstract class UserApp : AppBase
    {
        internal override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IUser && IsInternalEnabled && IsEnabled(e.Source) && IsAllowed(e.Sender))
                FeatureInvoker(e);
        }
    }

    internal enum AppPermission
    {
        Everyone,
        Administrator,
        Owner
    }

    internal enum AppPriority
    {
        Highest,
        Higher,
        Normal,
        Lower,
        Lowest
    }
}
