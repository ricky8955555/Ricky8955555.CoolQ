using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using System.Collections.Generic;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    class AutoRepeaterFeature : Feature
    {

        private static readonly List<(IChattable Source, string Message)> Messages = new List<(IChattable Source, string Message)>();

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            if (Chattables.Contains(e.Source))
                try
                {
                    e.Source.Send(e.Message.Content);
                }
                catch (ApiException ex) when (ex.ErrorCode == -11) { }
            else
            {
                Messages.Add((e.Source, e.Message));

                var messages = Messages.FindAll(x => x.Source.Equals(e.Source));

                if (messages.FindAll(x => x.Message == e.Message).Count == messages.Count)
                {
                    if (messages.Count > 2)
                        try
                        {
                            e.Source.Send(e.Message.Content);
                        }
                        catch (ApiException ex) when (ex.ErrorCode == -11) { }
                }
                else
                    Messages.RemoveAll(x => x.Source.Equals(e.Source) && x.Message != e.Message);
            }
        }
    }
}
