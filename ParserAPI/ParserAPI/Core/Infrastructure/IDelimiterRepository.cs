using ParserAPI.Models;
using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface IDelimiterRepository
    {
        List<string> GetFirstNames();
        List<Certification> GetCertifications();
        List<SkillList> GetSkills();
        List<DegreeList> GetDegrees();
        List<string> GetJobTitles();
        List<string> GetCompanyNames();
        List<string> GetSummaryTags();
        List<string> GetCareerTags();
        List<string> GetAddressTags();
        List<string> GetSkillsTags();
        List<string> GetCertificationTags();
        List<string> GetEducationTags();
        List<string> GetResumeTags();
    }
}
