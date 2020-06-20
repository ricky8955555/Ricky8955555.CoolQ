using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    class PluginConfig : Configuration
    {
        public override string Name { get; } = "PluginConfig";
        protected override JToken InitInfo { get; } = new JObject()
        {
            new JProperty("Prefix", "*"),
            new JProperty("Administrator", 0)
        };
    }
}
