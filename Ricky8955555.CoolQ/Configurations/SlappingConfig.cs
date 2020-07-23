using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    internal class SlappingConfig : Configuration
    {
        internal override string Name { get; } = "SlappingConfig";

        protected override JToken InitInfo { get; } = new JObject();
    }
}