using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    internal class WeatherApp : App
    {
        internal override string Name { get; } = "Weather";

        internal override string DisplayName { get; } = "天气查询";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new WeatherCommand()
        };
    }
}