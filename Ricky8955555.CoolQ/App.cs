using System;
using System.Linq;
using System.Reflection;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    public abstract partial class AppBase
    {
        public readonly static AppBase[] Apps = Assembly.GetExecutingAssembly().GetTypes()
            .Where(it => it.IsSubclassOf(typeof(AppBase)))
            .Select(it => it.GetConstructors().FirstOrDefault()?.Invoke(Array.Empty<object>()))
            .Where(it => it is not null)
            .Cast<AppBase>()
            .ToArray();

        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public virtual bool IsEnabledByDefault { get; } = true;

        public virtual bool CanDisable { get; } = true;

        public virtual AppPermission Permission { get; } = AppPermission.Everyone;

        public virtual AppPriority Priority { get; } = AppPriority.Normal;

        public virtual Feature[] Features { get; } = Array.Empty<Feature>();

        public abstract void Run(MessageReceivedEventArgs e);

        protected void Invoke(MessageReceivedEventArgs e)
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

        public bool IsEnabled(IChattable source) => !CanDisable || AppStatusConfig.Config[source.ToUniversalString()][Name].ToObject<bool>();

        public bool IsAllowed(IUser user)
        {
            return Permission switch
            {
                AppPermission.Everyone => true,
                AppPermission.Administrator => user is not IMember member || member.IsAdministrator || user.Number == Owner,
                AppPermission.Owner => user.Number == Owner || Owner == -1,
                _ => false
            };
        }
    }

    public abstract class App : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (IsEnabled(e.Source) && IsAllowed(e.Subject))
                Invoke(e);
        }
    }

    public abstract class GroupApp : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IGroup && IsEnabled(e.Source) && IsAllowed(e.Subject))
                Invoke(e);
        }
    }

    public abstract class UserApp : AppBase
    {
        public override void Run(MessageReceivedEventArgs e)
        {
            if (e.Source is IUser && IsEnabled(e.Source) && IsAllowed(e.Subject))
                Invoke(e);
        }
    }

    public enum AppPermission
    {
        Everyone,
        Administrator,
        Owner
    }

    public enum AppPriority
    {
        Highest,
        Higher,
        Normal,
        Lower,
        Lowest
    }
}