using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ricky8955555.CoolQ.Features
{
    class RichTextAutoParserFeature : AppFeature
    {
        public override App App { get; } = Main.Apps.Where(x => x.Name == "RichTextAutoParser").Single();

        protected override void Invokes(MessageReceivedEventArgs e)
        {
            if (e.Message.Parse()[0] is RichText richText && 
            richText["content"] != null) 
                App.Run(e, richText);
        }
    }
}