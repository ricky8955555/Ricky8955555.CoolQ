using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    internal class AppStatusConfig : Configuration
    {
        internal override string Name { get; } = "AppStatusConfig";

        protected override JToken InitInfo { get; } = new JObject();
    }
}