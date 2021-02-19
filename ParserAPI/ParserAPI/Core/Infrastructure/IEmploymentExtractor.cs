using ParserAPI.Models.Candidates;
using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface IEmploymentExtractor
    {
        List<Job> GetEmployment(List<string> resumeTextList); 
    }
}
