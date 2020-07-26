using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    
    internal class AboutApp : App
    {
        internal override string Name { get; } = "About";

        internal override string DisplayName { get; } = "关于本插件";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new AboutCommand()
        };
    }
}