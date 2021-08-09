using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configs
{
    public class PluginConfig : Configuration
    {
        public override string Name { get; } = "PluginConfig";

        protected override JToken InitInfo { get; } = new JObject()
        {
            new JProperty("Prefix", "minop "),
            new JProperty("Owner", -1)
        };
    }
}