using System;
using System.Linq;
using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Parsing;
using static Ricky8955555.CoolQ.Configurations;

namespace Ricky8955555.CoolQ
{
    public abstract class CommandBase : Feature
    {
        public abstract string ResponseCommand { get; }

        protected abstract string CommandUsage { get; }

        protected virtual bool IsHandledAutomatically { get; } = true;

        public override string Usage => string.Format(CommandUsage, Prefix);

        protected virtual LastParameterProcessing LastParameterProcessing { get; } = LastParameterProcessing.None;

        protected string GetMessage(string message)
        {
            message = message.Trim();

            if (message.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
            {
                string msg = message.Substring(Prefix.Length);
                if (string.IsNullOrWhiteSpace(msg))
                    return null;
                else
                    return msg;
            }
            else
                return null;
        }

        protected bool GetParameter(string message, out ComplexMessage parameters)
        {
            string[] strs = message.Split(new string[] { " ", Constants.CQNewLine }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (strs.Length > 0 && strs[0].ToLower() == ResponseCommand)
            {
                if (strs.Length == 1)
                    parameters = null;
                else
                {
                    var splitter = new CommandSplitter();
                    var command = splitter.Split(strs[1]);
                    parameters = command?.Select(x => ComplexMessage.Parse(x).ToMessageElement()).ToComplexMessage();
                }

                return true;
            }
            else
            {
                parameters = null;
                return false;
            }
        }

        protected void SetHandled()
        {
            if (IsHandledAutomatically)
                Handled = true;
        }
    }

    public abstract class Command : CommandBase
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, ComplexMessage elements);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message is not null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (LastParameterProcessing == LastParameterProcessing.None && parameters is null)
                {
                    Invoking(e, null);
                }
                else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters is not null)
                {
                    Invoking(e, parameters);
                }
                else
                {
                    return;
                }

                SetHandled();
            }
        }
    }

    public abstract class Command<T> : CommandBase
        where T : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T arg, ComplexMessage elements);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message is not null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters is not null && parameters.TryDeconstruct(out T ele))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 1)
                    {
                        Invoking(e, ele, null);
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 1)
                    {
                        Invoking(e, ele, parameters.Skip(1).ToComplexMessage());
                    }
                    else
                    {
                        return;
                    }

                    SetHandled();
                }
            }
        }
    }

    public abstract class Command<T1, T2> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2, ComplexMessage elements);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message is not null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters is not null && parameters.TryDeconstruct(out T1 ele1, out T2 ele2))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 2)
                    {
                        Invoking(e, ele1, ele2, null);
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 2)
                    {
                        Invoking(e, ele1, ele2, parameters.Skip(2).ToComplexMessage());
                    }
                    else
                    {
                        return;
                    }

                    SetHandled();
                }
            }
        }
    }

    public abstract class Command<T1, T2, T3> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
        where T3 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2, T3 arg3, ComplexMessage elements);

        public override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message is not null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters is not null && parameters.TryDeconstruct(out T1 ele1, out T2 ele2, out T3 ele3))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 3)
                    {
                        Invoking(e, ele1, ele2, ele3, null);
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 3)
                    {
                        Invoking(e, ele1, ele2, ele3, parameters.Skip(2).ToComplexMessage());
                    }
                    else
                    {
                        return;
                    }

                    SetHandled();
                }
            }
        }
    }

    public enum LastParameterProcessing
    {
        None,
        ComplexMessage
    }
}