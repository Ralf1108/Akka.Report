using System.Collections.Generic;

namespace Akka.Report
{
    public class QueryInfo
    {
        public string Select { get; set; }
        public string Where { get; set; }
        public List<string> From { get; set; }
    }
}