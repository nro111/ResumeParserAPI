using ParserAPI.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core.Infrastructure
{
    public interface IExtractorFacade
    {
        Dictionary<int, string> GenerateResumeSections(List<string> resumeTextList);
        Candidate GenerateCandidateObject(string path);
    }
}
