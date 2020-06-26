using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using static Ricky8955555.CoolQ.Commons;

namespace Ricky8955555.CoolQ
{
    abstract class CommandBase : Feature
    {
        public abstract string ResponseCommand { get; }

        protected abstract string CommandUsage { get; }

        public override string Usage => string.Format(CommandUsage, Prefix);

        protected string GetMessage(string message)
        {
            message = message.Trim();

            if (message.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
                return message.Substring(Prefix.Length);
            else
                return null;
        }

        protected string GetParameter(string message)
        {
            string[] splitMessage = message?.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (splitMessage != null && splitMessage.Length == 2 && splitMessage[0].ToLower() == ResponseCommand && !string.IsNullOrWhiteSpace(splitMessage[1]))
                return splitMessage[1];
            else
                return null;
        }
    }

    abstract class Command : CommandBase
    {
        protected virtual bool CanHaveParameter { get; } = false;

        protected abstract void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());
            string parameter = GetParameter(message);

            if (message?.Trim().ToLower() == ResponseCommand)
                Invoking(e);
            else if (parameter != null)
            {
                if (CanHaveParameter)
                    Invoking(e, ComplexMessage.Parse(parameter));
                else
                    NotifyIncorrectUsage(e);
            }
        }
    }

    abstract class Command<T> : CommandBase
        where T : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T arg);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());
            string parameter = GetParameter(message);

            if (parameter != null)
            {
                var elements = ComplexMessage.Parse(parameter);

                if (elements.Count == 1 && elements.TryDeconstruct(out T ele))
                    Invoking(e, ele);
                else
                    NotifyIncorrectUsage(e);
            }
        }
    }

    abstract class Command<T1, T2> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());
            string parameter = GetParameter(message);

            if (parameter != null)
            {
                var elements = ComplexMessage.Parse(parameter);

                if (elements.Count == 2 && elements.TryDeconstruct(out T1 ele1, out T2 ele2))
                    Invoking(e, ele1, ele2);
                else
                    NotifyIncorrectUsage(e);
            }
        }
    }
}
