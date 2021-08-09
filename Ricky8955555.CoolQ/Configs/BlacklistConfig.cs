using System.Linq;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configs
{
    public class BlacklistConfig : Configuration
    {
        public override string Name { get; } = "BlacklistConfig";

        protected override JToken InitInfo { get; } = new JArray();

        public new JArray Config => (JArray)base.Config;

        public bool Contains(long number)
        {
            return Config.Any(token => token.ToObject<int>() == number);
        }

        public bool Add(long number)
        {
            if (!Contains(number))
            {
                Config.Add(number);
                return true;
            }

            return false;
        }

        public bool Remove(long number)
        {
            return Config.Remove(Config.FirstOrDefault(token => token.ToObject<int>() == number));
        }
    }
}