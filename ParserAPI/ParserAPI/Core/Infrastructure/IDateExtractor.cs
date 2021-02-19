using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface IDateExtractor
    {
        public KeyValuePair<string, int> GetEmploymentDate(string line);
    }
}
