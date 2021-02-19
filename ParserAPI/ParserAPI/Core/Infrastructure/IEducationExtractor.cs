using ParserAPI.Models.Candidates;
using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface IEducationExtractor
    {
        List<Degree> GetDegrees(List<string> educationSection);
    }
}
