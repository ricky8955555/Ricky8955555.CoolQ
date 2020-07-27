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

        protected bool GetParameter(string message, out string parameter)
        {
            string[] splitMessage = message?.Split(new string[] { " ", Constants.CQNewLine }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (splitMessage != null && splitMessage.Length > 0)
            {
                parameter = splitMessage.Length == 2 ? splitMessage[1] : null;
                return splitMessage[0].ToLower() == ResponseCommand;
            }
            else
            {
                parameter = null;
                return false;
            }
        }
    }

    internal abstract class Command : CommandBase
    {
        protected virtual bool CanHaveParameter { get; } = false;

        protected abstract void Invoking(MessageReceivedEventArgs e, ComplexMessage elements = null);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message.ToString());

            if (GetParameter(message?.Trim(), out string parameter))
            {
                if (parameter == null)
                    Invoking(e);
                else
                {
                    if (CanHaveParameter)
                        Invoking(e, ComplexMessage.Parse(parameter));
                    else
                        NotifyIncorrectUsage(e);
                }
                if (IsHandledAutomatically)
                    Handled = true;
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

            if (GetParameter(message?.Trim(), out string parameter))
            {
                if (parameter == null)
                {
                    NotifyIncorrectUsage(e);
                }
                else
                {
                    var elements = ComplexMessage.Parse(parameter);

                    if (elements.Count == 1 && elements.TryDeconstruct(out T ele))
                        Invoking(e, ele);
                    else
                        NotifyIncorrectUsage(e);

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

            if (GetParameter(message?.Trim(), out string parameter))
            {
                if (parameter == null)
                {
                    NotifyIncorrectUsage(e);
                }
                else
                {
                    var elements = ComplexMessage.Parse(parameter);

                    if (elements.Count == 2 && elements.TryDeconstruct(out T1 ele1, out T2 ele2))
                        Invoking(e, ele1, ele2);
                    else
                        NotifyIncorrectUsage(e);

                    if (IsHandledAutomatically)
                        Handled = true;
                }
            }
        }
    }
}