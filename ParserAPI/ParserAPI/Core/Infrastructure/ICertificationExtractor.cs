using ParserAPI.Models;
using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface ICertificationExtractor
    {
        List<Certification> GetCertifications(List<string> resumeTextList);
    }
}
