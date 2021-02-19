using ParserAPI.Core.Extensions;
using ParserAPI.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ParserAPI.Extractors
{
    public class SectionExtractor : ISectionExtractor
    {
        ISectionTypeAndIndexRepositoryBuilder _sectionTypeAndIndexRepositoryBuilder;
        public SectionExtractor(ISectionTypeAndIndexRepositoryBuilder sectionTypeAndIndexRepositoryBuilder)
        {
            _sectionTypeAndIndexRepositoryBuilder = sectionTypeAndIndexRepositoryBuilder;
        }

        public Dictionary<int, string> OrganizeResumeIntoSections(List<string> resumeTextList)
        {
            var sections = new Dictionary<int, string>();
            var addressSection = _sectionTypeAndIndexRepositoryBuilder.FindAddressSection(resumeTextList);
            var certificationSection = _sectionTypeAndIndexRepositoryBuilder.FindCertificationSection(resumeTextList);
            var educationSection = _sectionTypeAndIndexRepositoryBuilder.FindEducationSection(resumeTextList);
            var employmentSection = _sectionTypeAndIndexRepositoryBuilder.FindEmploymentSection(resumeTextList);
            var skillsSection = _sectionTypeAndIndexRepositoryBuilder.FindSkillsSection(resumeTextList);
            var summarySection = _sectionTypeAndIndexRepositoryBuilder.FindSummarySection(resumeTextList);
            
            if(addressSection.Key != -1)
            {
                sections.Add(addressSection.Key, addressSection.Value);
            }
            if (certificationSection.Key != -1)
            {
                sections.Add(certificationSection.Key, certificationSection.Value);
            }
            if (educationSection.Key != -1)
            {
                sections.Add(educationSection.Key, educationSection.Value);
            }
            if (employmentSection.Key != -1)
            {
                sections.Add(employmentSection.Key, employmentSection.Value);
            }
            if (skillsSection.Key != -1)
            {
                sections.Add(skillsSection.Key, skillsSection.Value);
            }
            if (summarySection.Key != -1)
            {
                sections.Add(summarySection.Key, summarySection.Value);
            }

            return sections; 
        }

        public Dictionary<string, List<string>> ExtractSectionContent(List<string> textList, Dictionary<int, string> sortedIndexDictionary)
        {
            var sectionIndexAndContentDictionary = new Dictionary<string, List<string>>();

            for(var i = 0; i < sortedIndexDictionary.Count; i++)
            {
                var sectionStartKVP = sortedIndexDictionary.ElementAt(i);
                var section = sortedIndexDictionary.Last().Equals(sectionStartKVP) ? textList.Skip(sectionStartKVP.Key).Take(textList.Count - sectionStartKVP.Key).ToList() : textList.Skip(sectionStartKVP.Key).Take(sortedIndexDictionary.ElementAt(i + 1).Key - sectionStartKVP.Key).ToList();              

                sectionIndexAndContentDictionary.Add(sectionStartKVP.Value.ToLower(), section); 
            }

            return sectionIndexAndContentDictionary;
        }

        public Dictionary<int, string> SortDictionaryByDescending(Dictionary<int, string> dictionary)
        {
            var result = from pair in dictionary
                        orderby pair.Key ascending
                        select pair;

            return result.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
