using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class CovidStatusApp : App
    {
        internal override string Name { get; } = "CovidStatus";

        internal override string DisplayName { get; } = "疫情动态";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new CovidStatusCommand()
        };
    }
}
