using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configs
{
    public class AppStatusConfig : Configuration
    {
        public override string Name { get; } = "AppStatusConfig";

        protected override JToken InitInfo { get; } = new JObject();
    }
}