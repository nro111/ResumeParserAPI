using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core
{
    public class OneBackDirectionalLookup : IDirectionalLookupStrategy
    {
        private IDateExtractor _dateExtractor;
        public OneBackDirectionalLookup(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }
        public KeyValuePair<string, int> Execute(List<string> employmentSection, string line)
        {
            var oneBackPreviousLine = employmentSection.IndexOf(line) - 1 > 0 ? employmentSection.ElementAt(employmentSection.IndexOf(line) - 1).Replace(",", "") : string.Empty;
            return _dateExtractor.GetEmploymentDate(oneBackPreviousLine);
        }
    }
}
