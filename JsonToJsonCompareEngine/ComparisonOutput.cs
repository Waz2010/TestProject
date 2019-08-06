using System;
using System.Collections.Generic;

namespace JsonToJsonCompareEngine
{
    public class ComparisonOutput
    {
        public ComparisonOutput()
        {
            Errors = new List<string>();
        }
        public FileCompareResult FileCompareResult { get; set; }
        public IList<string> Errors { get; set; }
    }
}
