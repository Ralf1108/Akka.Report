using System;
using System.Collections.Generic;

namespace Akka.Report
{
    public class KeyedQueryInfo
    {
        public KeyedQueryInfo(string select, string where, List<Guid> from)
        {
            Select = select;
            Where = where;
            From = from;
        }

        public string Select { get; }
        public string Where { get; }
        public List<Guid> From { get; }
    }
}