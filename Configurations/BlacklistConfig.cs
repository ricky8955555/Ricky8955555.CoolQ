using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configurations
{
    class BlacklistConfig : Configuration
    {
        public override string Name { get; } = "BlacklistConfig";
        protected override JToken InitInfo { get; } = new JArray();
    }
}
