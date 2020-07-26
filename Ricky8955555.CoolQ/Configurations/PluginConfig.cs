using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    internal class PluginConfig : Configuration
    {
        internal override string Name { get; } = "PluginConfig";

        protected override JToken InitInfo { get; } = new JObject()
        {
            new JProperty("Prefix", "minop "),
            new JProperty("Owner", -1)
        };
    }
}