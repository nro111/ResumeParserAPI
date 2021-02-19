using System.Collections.Generic;
using System.Linq;
using ParserAPI.Models.Candidates;
using ParserAPI.Core.Infrastructure;

namespace ParserAPI.Core
{
    public class ExtractorFacade : IExtractorFacade
    {
        private IBasicInfoExtractor _basicInfoExtractor;
        private ICertificationExtractor _certificationExtractor;
        private IEducationExtractor _educationExtractor;
        private IEmploymentExtractor _employmentExtractor;
        private ISkillExtractor _skillExtractor;
        private ISectionExtractor _sectionExtractor;
        private IExperienceCalculator _experienceCalculator;
        private Candidate _candidate;

        public ExtractorFacade(IBasicInfoExtractor basicInfoExtractor, ICertificationExtractor certificationExtractor,
                               IEducationExtractor educationExtractor, IEmploymentExtractor employmentExtractor, 
                               ISkillExtractor skillExtractor, ISectionExtractor sectionExtractor, IExperienceCalculator experienceCalculator)
        {
            _candidate = new Candidate(); 
            _basicInfoExtractor = basicInfoExtractor;
            _certificationExtractor = certificationExtractor;
            _educationExtractor = educationExtractor;
            _employmentExtractor = employmentExtractor;
            _skillExtractor = skillExtractor;
            _sectionExtractor = sectionExtractor;
            _experienceCalculator = experienceCalculator; 
        }

        public Dictionary<int, string> GenerateResumeSections(List<string> resumeTextList)
        {
            var sections = _sectionExtractor.OrganizeResumeIntoSections(resumeTextList);
            var organizedSections = _sectionExtractor.SortDictionaryByDescending(sections);
            return organizedSections;
        }

        public Candidate GenerateCandidateObject(string path)
        {
            var typeConverterTool = new TypeConverterTool();
            var resumeTextList = typeConverterTool.ConvertWordDocumentToList(path);
            var sections = _sectionExtractor.OrganizeResumeIntoSections(resumeTextList);
            var organizedSections = _sectionExtractor.SortDictionaryByDescending(sections);
            var sectionsAndContent = _sectionExtractor.ExtractSectionContent(resumeTextList, organizedSections); 

            var name = _basicInfoExtractor.GetName(resumeTextList);
            var email = _basicInfoExtractor.GetEmailAddresses(resumeTextList);
            var phone = _basicInfoExtractor.GetPhoneNumber(resumeTextList); 

            //basic info
            _candidate.FirstName = name.Key;
            _candidate.LastName = name.Value;
            _candidate.Email = email.Count > 0 ? email.ElementAt(0) : string.Empty;
            _candidate.Phone = phone;

            //skills
            if(sectionsAndContent.ContainsKey("skills"))
                _candidate.Skills = _skillExtractor.GetSkills(sectionsAndContent["skills"]);

            //certs
            if (sectionsAndContent.ContainsKey("certifications"))
                _candidate.Certifications = _certificationExtractor.GetCertifications(sectionsAndContent["certifications"]);

            //education
            if (sectionsAndContent.ContainsKey("education"))
                _candidate.Education = _educationExtractor.GetDegrees(sectionsAndContent["education"]);

            //employment
            if (sectionsAndContent.ContainsKey("employment"))
                _candidate.Jobs = _employmentExtractor.GetEmployment(sectionsAndContent["employment"]);

            //calculate experience
            _candidate.Experience = _experienceCalculator.CalculateOverallExperience(_candidate);
            //_candidate.Skills = _experienceCalculator.CalculateSkillExperience(_candidate);

            return _candidate; 
        }
    }
}
