using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Configs
{
    public class SlappingConfig : Configuration
    {
        public override string Name { get; } = "SlappingConfig";

        protected override JToken InitInfo { get; } = new JObject();

        public new JObject Config => (JObject)base.Config;

        public void SetSentence(long number, string sentence)
        {
            string key = number.ToString();

            if (Config.ContainsKey(key))
            {
                Config[key] = sentence;
            }
            else
            {
                Config.Add(new JProperty(number.ToString(), sentence));
            }
        }

        public bool Remove(long number)
        {
            return Config.Remove(number.ToString());
        }
    }
}