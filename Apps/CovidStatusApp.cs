using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class CovidStatusApp : App
    {
        public override string Name { get; } = "CovidStatus";

        public override string DisplayName { get; } = "疫情动态";

        public override Feature[] Features { get; } = new Feature[] {
            new CovidStatusCommand()
        };
    }
}
