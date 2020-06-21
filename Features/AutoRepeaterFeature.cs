using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using System.Collections.Generic;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;
using static Ricky8955555.CoolQ.Commons;

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
                    if (!e.Message.ToString().StartsWith(Prefix))
                        e.Source.Send(e.Message.Content);
                }
                catch (ApiException) { }
            else
            {
                Messages.Add((e.Source, e.Message));

                var messages = Messages.FindAll(x => x.Source.Equals(e.Source));

                if (messages.FindAll(x => x.Message == e.Message).Count == messages.Count)
                {
                    if (messages.Count == 3)
                    {
                        e.Source.Send(e.Message.Content);
                        Messages.RemoveAll(x => x.Source.Equals(e.Source));
                    }
                }
                else
                    Messages.RemoveAll(x => x.Source.Equals(e.Source) && x.Message != e.Message);
            }
        }
    }
}
