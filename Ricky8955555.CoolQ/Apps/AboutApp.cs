using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{

    public class AboutApp : App
    {
        public override string Name { get; } = "About";

        public override string DisplayName { get; } = "关于本插件";

        public override Feature[] Features { get; } = new Feature[]
        {
            new AboutCommand()
        };
    }
}