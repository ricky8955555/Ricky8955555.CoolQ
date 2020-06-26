using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
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

        public virtual Permission Permission { get; } = Permission.Everyone;

        public virtual Feature[] Features { get; } = new Feature[] { };

        public abstract void Run(MessageReceivedEventArgs e);

        protected void FeatureInvoker(MessageReceivedEventArgs e)
        {
            foreach (var feature in Features)
                feature.Invoke(e);
        }

        public bool IsEnabled(IChattable source) => !CanDisable || AppConfig.Config[source.ToString(true)][Name].ToObject<bool>();

        public bool IsAllowed(IUser user)
        {
            switch (Permission)
            {
                case Permission.Everyone:
                    return true;
                case Permission.Administrator:
                    return !(user is IMember member) || (member.IsAdministrator || user.Number == Administrator);
                case Permission.Owner:
                    return user.Number == Administrator;
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

    enum Permission
    {
        Everyone,
        Administrator,
        Owner
    }
}
