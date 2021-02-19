using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface IDirectionalLookupStrategy
    {
        KeyValuePair<string, int> Execute(List<string> employmentSection, string line);
    }
}
