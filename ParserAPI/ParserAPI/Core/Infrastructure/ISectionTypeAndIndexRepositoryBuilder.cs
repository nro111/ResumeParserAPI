using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface ISectionTypeAndIndexRepositoryBuilder
    {
        KeyValuePair<int, string> FindAddressSection(List<string> textList);
        KeyValuePair<int, string> FindCertificationSection(List<string> textList);        
        KeyValuePair<int, string> FindEducationSection(List<string> textList);
        KeyValuePair<int, string> FindEmploymentSection(List<string> textList);
        KeyValuePair<int, string> FindSkillsSection(List<string> textList);
        KeyValuePair<int, string> FindSummarySection(List<string> textList);
    }
}
