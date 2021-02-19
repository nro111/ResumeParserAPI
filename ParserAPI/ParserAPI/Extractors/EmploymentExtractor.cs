using ParserAPI.Core;
using ParserAPI.Core.Extensions;
using ParserAPI.Core.Infrastructure;
using ParserAPI.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParserAPI.Extractors
{
    public class EmploymentExtractor : IEmploymentExtractor
    {
        private IDelimiterRepository _delimiterRepository;
        private IDateExtractor _dateExtractor;
        private IExperienceCalculator _experienceCalculator;
        public EmploymentExtractor(IDelimiterRepository delimiterRepository, IDateExtractor dateExtractor, IExperienceCalculator experienceCalculator)
        {
            _delimiterRepository = delimiterRepository;
            _dateExtractor = dateExtractor;
            _experienceCalculator = experienceCalculator;
        }

        public List<Job> GetEmployment(List<string> employmentSection)
        {
            var jobs = new List<Job>();
            var directionalLookupContext = new DirectionalLookupContext(_delimiterRepository, _dateExtractor, _experienceCalculator);
            employmentSection = employmentSection.Where(x => !string.IsNullOrWhiteSpace(x)).ToList(); 
            var result = new Job();
            for (var i = 1; i < employmentSection.Count(); i++)
            {
                var line = employmentSection.ElementAt(i);

                if (!directionalLookupContext.HasAssignedStrategy())
                {
                    result = directionalLookupContext.SetDirectionalLookupStrategy(employmentSection, line);
                }
                else
                {
                    result = directionalLookupContext.RunAssignedStrategy(employmentSection, line);
                }
                if(result != null && result.MonthsInPosition != 0 && result.Experience != 0.0 && result.Title != string.Empty)
                {
                    jobs.Add(result);
                }
            }

            return jobs;
        }

        private KeyValuePair<bool, string> IsCompanyNameInProximity(List<string> previousLine, List<string> currentLine, List<string> nextLine)
        {
            var companyNameResult = new KeyValuePair<bool, string>();
            var companyNames = _delimiterRepository.GetCompanyNames();
            previousLine.ToLower();
            currentLine.ToLower();
            nextLine.ToLower();
            if (companyNames.Exists(x => previousLine.Contains(x)))
                companyNameResult = new KeyValuePair<bool, string>(true, companyNames.Find(x => previousLine.Contains(x)));
            else if (companyNames.Exists(x => currentLine.Contains(x)))
                companyNameResult = new KeyValuePair<bool, string>(true, companyNames.Find(x => currentLine.Contains(x)));
            else if (companyNames.Exists(x => nextLine.Contains(x)))
                companyNameResult = new KeyValuePair<bool, string>(true, companyNames.Find(x => nextLine.Contains(x)));
            return companyNameResult;
        }
    }
}
