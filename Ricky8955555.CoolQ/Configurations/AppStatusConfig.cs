using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    class AppStatusConfig : Configuration
    {
        internal override string Name { get; } = "AppStatusConfig";

        protected override JToken InitInfo { get; } = new JObject();
    }
}