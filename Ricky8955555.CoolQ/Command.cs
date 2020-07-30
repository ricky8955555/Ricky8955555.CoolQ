using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using System;
using static Ricky8955555.CoolQ.Configuration;

namespace Ricky8955555.CoolQ
{
    internal abstract class CommandBase : Feature
    {
        internal abstract string ResponseCommand { get; }

        protected abstract string CommandUsage { get; }

        protected virtual bool IsHandledAutomatically { get; } = true;

        internal override string Usage => string.Format(CommandUsage, Prefix);

        protected string GetMessage(string message)
        {
            message = message.Trim();

            if (message.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
                return message.Substring(Prefix.Length);
            else
                return null;
        }

        protected bool GetParameter(string message, out ComplexMessage parameters)
        {
            var command = new CommandMessage(message);
            parameters = command.Parameters;
            return command.Command == ResponseCommand;
        }
    }

    internal abstract class Command : CommandBase
    {
        // protected virtual bool CanHaveParameter { get; } = false;

        protected abstract void Invoking(MessageReceivedEventArgs e);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());

            if (GetParameter(message?.Trim(), out ComplexMessage parameters))
            {
                if (parameters == null)
                {
                    Invoking(e);

                    if (IsHandledAutomatically)
                        Handled = true;
                }
            }
        }
    }

    internal abstract class Command<T> : CommandBase
        where T : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T arg);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());

            if (GetParameter(message?.Trim(), out ComplexMessage parameters))
            {
                if (parameters == null)
                {
                    NotifyIncorrectUsage(e);
                }
                else if (parameters.Count == 1 && parameters.TryDeconstruct(out T ele))
                {
                    Invoking(e, ele);

                    if (IsHandledAutomatically)
                        Handled = true;
                }
            }
        }
    }

    internal abstract class Command<T1, T2> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());

            if (GetParameter(message?.Trim(), out ComplexMessage parameters))
            {
                if (parameters == null)
                {
                    NotifyIncorrectUsage(e);
                }
                else if (parameters.Count == 2 && parameters.TryDeconstruct(out T1 ele1, out T2 ele2))
                {
                    Invoking(e, ele1, ele2);

                    if (IsHandledAutomatically)
                        Handled = true;
                }
            }
        }
    }

    internal abstract class Command<T1, T2, T3> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
        where T3 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2, T3 arg3);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());

            if (GetParameter(message?.Trim(), out ComplexMessage parameters))
            {
                if (parameters == null)
                {
                    NotifyIncorrectUsage(e);
                }
                else if (parameters.Count == 3 && parameters.TryDeconstruct(out T1 ele1, out T2 ele2, out T3 ele3))
                {
                    Invoking(e, ele1, ele2, ele3);

                    if (IsHandledAutomatically)
                        Handled = true;
                }
            }
        }
    }
}