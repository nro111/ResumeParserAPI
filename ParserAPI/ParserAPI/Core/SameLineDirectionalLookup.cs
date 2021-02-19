using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core
{
    public class SameLineDirectionalLookup : IDirectionalLookupStrategy
    {
        private IDateExtractor _dateExtractor;
        public SameLineDirectionalLookup(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }
        public KeyValuePair<string, int> Execute(List<string> employmentSection, string line)
        {
            line = line.Replace(",", "");
            return _dateExtractor.GetEmploymentDate(line);            
        }
    }
}
