using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core
{
    public class OneForwardDirectionalLookup : IDirectionalLookupStrategy
    {
        private IDateExtractor _dateExtractor;
        public OneForwardDirectionalLookup(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }
        public KeyValuePair<string, int> Execute(List<string> employmentSection, string line)
        {
            var oneForwardFutureLine = (employmentSection.IndexOf(line) + 1) < (employmentSection.Count() - 1) ? employmentSection.ElementAt((employmentSection.IndexOf(line) + 1)).Replace(",", "") : string.Empty;
            return _dateExtractor.GetEmploymentDate(oneForwardFutureLine);
        }
    }
}
