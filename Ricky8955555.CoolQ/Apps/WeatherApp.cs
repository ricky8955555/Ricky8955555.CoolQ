using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class WeatherApp : App
    {
        public override string Name { get; } = "Weather";

        public override string DisplayName { get; } = "天气查询";

        public override Feature[] Features { get; } = new Feature[]
        {
            new WeatherCommand()
        };
    }
}