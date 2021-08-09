using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;
using static Ricky8955555.CoolQ.Apps.AutoRepeaterApp;

namespace Ricky8955555.CoolQ.Features
{
    public class AutoRepeaterFeature : Feature
    {

        private static readonly List<(IChattable Source, string Message)> Messages = new();

        public override void Invoke(MessageReceivedEventArgs e)
        {
            if (Chattables.Contains(e.Source))
                try
                {
                    e.Source.Send(e.Message.Content);
                }
                catch (ApiException ex) when (ex.ErrorCode == -11 || ex.ErrorCode == -33) { }
            else
            {
                Messages.Add((e.Source, e.Message));

                var messages = Messages.FindAll(x => x.Source.Equals(e.Source));

                if (messages.Count(x => x.Message == e.Message) == messages.Count)
                {
                    if (messages.Count > 2)
                        try
                        {
                            e.Source.Send(e.Message.Content);
                        }
                        catch (ApiException ex) when (ex.ErrorCode is -11 or -33) { }
                }
                else
                    Messages.RemoveAll(x => x.Source.Equals(e.Source) && x.Message != e.Message);
            }
        }
    }
}