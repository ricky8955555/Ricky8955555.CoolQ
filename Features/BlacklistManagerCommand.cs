using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json.Linq;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Commons;
using static Ricky8955555.CoolQ.FeatureResources.BlacklistManagerResources;

namespace Ricky8955555.CoolQ.Features
{
    class BlacklistManagerCommand : Command<PlainText, At>
    {
        public override string ResponseCommand { get; } = "blacklist";

        protected override string CommandUsage { get; } = "{0}blacklist <add/remove> <At>";

        protected override void Invoking(MessageReceivedEventArgs e, PlainText plainText, At at)
        {
            string operating = plainText.ToString().Trim().ToLower();
            var config = (JArray)BlacklistConfig.Config;
            long number = at.TargetNumber;

            if (number == Administrator || number == Bot.CurrentUser.Number)
                e.Reply(AdministratorOrRobotCannotBeListInBlacklist);
            else
            {
                if (operating == "add")
                {
                    if (config.Contains(number, true))
                        e.Reply(string.Format(Existed, number));
                    else
                    {
                        config.Add(number);
                        BlacklistConfig.Save();
                        e.Reply(string.Format(Added, number));
                    }
                }
                else if (operating == "remove")
                {
                    if (config.Contains(number, true))
                    {
                        BlacklistConfig.SetValueAndSave(config.Remove(number, true));
                        e.Reply(string.Format(Removed, number));
                    }
                    else
                        e.Reply(string.Format(DoesNotExist, number));
                }
                else
                    NotifyIncorrectUsage(e);
            }
        }
    }
}
