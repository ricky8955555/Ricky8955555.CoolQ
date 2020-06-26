using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    class AppConfig : Configuration
    {
        public override string Name { get; } = "AppConfig";
        protected override JToken InitInfo { get; } = new JObject();
    }
}
