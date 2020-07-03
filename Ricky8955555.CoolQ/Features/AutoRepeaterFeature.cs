using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System.Collections.Generic;
using System.Linq;
using static HuajiTech.CoolQ.CurrentPluginContext;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    class AutoRepeaterFeature : Feature
    {

        static readonly List<(IChattable Source, string Message)> Messages = new List<(IChattable Source, string Message)>();

        public override void Invoke(MessageReceivedEventArgs e)
        {
            if (Chattables.Contains(e.Source))
                try
                {
                    e.Source.Send(e.Message.Content);
                }
                catch (ApiException) { }
            else
            {
                Messages.Add((e.Source, e.Message));

                var messages = Messages.FindAll(x => x.Source.Equals(e.Source));

                if (messages.FindAll(x => x.Message == e.Message).Count == messages.Count)
                {
                    if (messages.Count > 2)
                    {
                        var complexMessage = e.Message.Parse();
                        int imageCount = complexMessage.OfType<Image>().Count();
                        int recordCount = complexMessage.OfType<Record>().Count();

                        if ((imageCount == 0 || imageCount > 0 && Bot.CanSendImage) &&
                            (recordCount == 0 || recordCount > 0 && Bot.CanSendRecord))
                            e.Source.Send(e.Message.Content);
                    }
                }
                else
                    Messages.RemoveAll(x => x.Source.Equals(e.Source) && x.Message != e.Message);
            }
        }
    }
}
