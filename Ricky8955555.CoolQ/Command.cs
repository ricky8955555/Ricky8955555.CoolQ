using HuajiTech.CoolQ.Events;
using HuajiTech.CoolQ.Messaging;
using Ricky8955555.CoolQ.Parsing;
using System;
using System.Linq;
using static Ricky8955555.CoolQ.Configuration;

namespace Ricky8955555.CoolQ
{
    internal abstract class CommandBase : Feature
    {
        internal abstract string ResponseCommand { get; }

        protected abstract string CommandUsage { get; }

        protected virtual bool IsHandledAutomatically { get; } = true;

        internal override string Usage => string.Format(CommandUsage, Prefix);

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
            string[] strs = message.Split(new char[] { ' ' }, 2);

            if (strs.Length > 0 && strs[0] == ResponseCommand)
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

    internal abstract class Command : CommandBase
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, ComplexMessage elements);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message != null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (LastParameterProcessing == LastParameterProcessing.None && parameters == null)
                {
                    Invoking(e, null);
                    SetHandled();
                }
                else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters != null)
                {
                    Invoking(e, parameters);
                    SetHandled();
                }
            }
        }
    }

    internal abstract class Command<T> : CommandBase
        where T : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T arg, ComplexMessage elements);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message != null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters != null && parameters.TryDeconstruct(out T ele))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 1)
                    {
                        Invoking(e, ele, null);
                        SetHandled();
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 1)
                    {
                        Invoking(e, ele, parameters.Skip(1).ToComplexMessage());
                        SetHandled();
                    }
                }
            }
        }
    }

    internal abstract class Command<T1, T2> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2, ComplexMessage elements);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message != null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters != null && parameters.TryDeconstruct(out T1 ele1, out T2 ele2))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 2)
                    {
                        Invoking(e, ele1, ele2, null);
                        SetHandled();
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 2)
                    {
                        Invoking(e, ele1, ele2, parameters.Skip(2).ToComplexMessage());
                        SetHandled();
                    }
                }
            }
        }
    }

    internal abstract class Command<T1, T2, T3> : CommandBase
        where T1 : MessageElement
        where T2 : MessageElement
        where T3 : MessageElement
    {
        protected abstract void Invoking(MessageReceivedEventArgs e, T1 arg1, T2 arg2, T3 arg3, ComplexMessage elements);

        internal override void Invoke(MessageReceivedEventArgs e)
        {
            string message = GetMessage(e.Message);

            if (message != null && GetParameter(message.Trim(), out ComplexMessage parameters))
            {
                if (parameters != null && parameters.TryDeconstruct(out T1 ele1, out T2 ele2, out T3 ele3))
                {
                    if (LastParameterProcessing == LastParameterProcessing.None && parameters.Count == 3)
                    {
                        Invoking(e, ele1, ele2, ele3, null);
                        SetHandled();
                    }
                    else if (LastParameterProcessing == LastParameterProcessing.ComplexMessage && parameters.Count > 3)
                    {
                        Invoking(e, ele1, ele2, ele3, parameters.Skip(2).ToComplexMessage());
                        SetHandled();
                    }
                }
            }
        }
    }

    internal enum LastParameterProcessing
    {
        None,
        ComplexMessage
    }
}