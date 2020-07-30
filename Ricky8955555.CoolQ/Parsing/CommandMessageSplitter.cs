using System;
using System.Collections.Generic;
using System.Text;

namespace Ricky8955555.CoolQ.Parsing
{
    internal class CommandMessageSplitter
    {
        private enum Status
        {
            TokenStart,
            Quote,
            QuoteStart,
            QuoteEnd
        }

        public List<string> Split(string command)
        {
            var memory = command.AsMemory();
            var sb = new StringBuilder();
            var strList = new List<string>();
            var status = Status.TokenStart;
            int pos = 0;

            while (pos < memory.Length)
            {
                char c = memory.Span[pos];

                if (c == '\"')
                {
                    switch (status)
                    {
                        case Status.TokenStart:
                            status = Status.Quote;
                            break;
                        case Status.Quote:
                            status = Status.TokenStart;
                            sb.Append('\"');
                            break;
                        case Status.QuoteStart:
                            status = Status.QuoteEnd;
                            break;
                        case Status.QuoteEnd:
                            status = Status.QuoteStart;
                            sb.Append('\"');
                            break;
                    }
                }
                else if (c == ' ')
                {
                    switch (status)
                    {
                        case Status.TokenStart:
                            Write();
                            break;
                        case Status.Quote:
                            status = Status.QuoteStart;
                            sb.Append(' ');
                            break;
                        case Status.QuoteStart:
                            sb.Append(' ');
                            break;
                        case Status.QuoteEnd:
                            status = Status.TokenStart;
                            Write();
                            break;
                    }
                }
                else
                {
                    if (status == Status.Quote)
                        status = Status.QuoteStart;

                    sb.Append(c);
                }

                pos++;
            }

            Write();
            strList.RemoveAll(string.IsNullOrWhiteSpace);

            void Write()
            {
                strList.Add(sb.ToString());
                sb.Clear();
            }

            return strList;
        }
    }
}
