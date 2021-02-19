using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core
{
    public class TwoForwardDirectionalLookup : IDirectionalLookupStrategy
    {
        private IDateExtractor _dateExtractor;
        public TwoForwardDirectionalLookup(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }
        public KeyValuePair<string, int> Execute(List<string> employmentSection, string line)
        {
            var twoForwardFutureLine = employmentSection.IndexOf(line) + 2 < employmentSection.Count() - 1 ? employmentSection.ElementAt(employmentSection.IndexOf(line) + 2).Replace(",", "") : string.Empty;
            return _dateExtractor.GetEmploymentDate(twoForwardFutureLine);
        }
    }
}
