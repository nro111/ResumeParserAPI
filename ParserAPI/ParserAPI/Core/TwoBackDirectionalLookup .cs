using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core
{
    public class TwoBackDirectionalLookup : IDirectionalLookupStrategy
    {
        private IDateExtractor _dateExtractor;
        public TwoBackDirectionalLookup(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }
        public KeyValuePair<string, int> Execute(List<string> employmentSection, string line)
        {
            var twoBackPreviousLine = employmentSection.IndexOf(line) - 2 > 0 ? employmentSection.ElementAt(employmentSection.IndexOf(line) - 2).Replace(",", "") : string.Empty;
            return _dateExtractor.GetEmploymentDate(twoBackPreviousLine);
        }
    }
}