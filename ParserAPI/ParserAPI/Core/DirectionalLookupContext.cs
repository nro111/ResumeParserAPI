using ParserAPI.Core.Infrastructure;
using ParserAPI.Models.Candidates;
using System.Collections.Generic;
using System.Linq;

namespace ParserAPI.Core
{
    public class DirectionalLookupContext
    {
        private IDirectionalLookupStrategy _directionalLookupStrategy;
        private List<string> _jobTitles = new List<string>();
        private IDateExtractor _dateExtractor;
        private IExperienceCalculator _experienceCalculator;

        public DirectionalLookupContext(IDelimiterRepository delimiterRepository, IDateExtractor dateExtractor, IExperienceCalculator experienceCalculator)
        {
            _jobTitles = delimiterRepository.GetJobTitles();
            _dateExtractor = dateExtractor;
            _experienceCalculator = experienceCalculator;
        }

        public bool HasAssignedStrategy()
        {
            return _directionalLookupStrategy != null;
        }

        public Job SetDirectionalLookupStrategy(List<string> employmentSection, string line)
        {
            if(!_jobTitles.Any(x => line.Contains(x)))
            {
                return new Job();
            }

            var oneBackPreviousLine = employmentSection.IndexOf(line) - 1 > 0 ? employmentSection.ElementAt(employmentSection.IndexOf(line) - 1).Replace(",", "") : string.Empty;
            var twoBackPreviousLine = employmentSection.IndexOf(line) - 2 > 0 ? employmentSection.ElementAt(employmentSection.IndexOf(line) - 2).Replace(",", "") : string.Empty;
            var oneForwardFutureLine = employmentSection.IndexOf(line) + 1 < (employmentSection.Count() - 1) ? employmentSection.ElementAt(employmentSection.IndexOf(line) + 1).Replace(",", "") : string.Empty;
            var twoForwardFutureLine = employmentSection.IndexOf(line) + 2 < (employmentSection.Count() - 2) ? employmentSection.ElementAt(employmentSection.IndexOf(line) + 2).Replace(",", "") : string.Empty;
            var sameLine = line.Replace(",", "");
            var extractedMonthsEmployed = _dateExtractor.GetEmploymentDate(sameLine);
            var oneBackPreviousExtractedMonthsEmployed = _dateExtractor.GetEmploymentDate(oneBackPreviousLine);
            var twoBackPreviousExtractedMonthsEmployed = _dateExtractor.GetEmploymentDate(twoBackPreviousLine);
            var oneForwardFutureExtractedMonthsEmployed = _dateExtractor.GetEmploymentDate(oneForwardFutureLine);
            var twoForwardFutureExtractedMonthsEmployed = _dateExtractor.GetEmploymentDate(twoForwardFutureLine);

            if (extractedMonthsEmployed.Value != 0)
                _directionalLookupStrategy = new SameLineDirectionalLookup(_dateExtractor);
            else if (oneBackPreviousExtractedMonthsEmployed.Value != 0)
                _directionalLookupStrategy = new OneBackDirectionalLookup(_dateExtractor);
            else if (twoBackPreviousExtractedMonthsEmployed.Value != 0)
                _directionalLookupStrategy = new TwoBackDirectionalLookup(_dateExtractor);
            else if (oneForwardFutureExtractedMonthsEmployed.Value != 0)
                _directionalLookupStrategy = new OneForwardDirectionalLookup(_dateExtractor);
            else if (twoForwardFutureExtractedMonthsEmployed.Value != 0)
                _directionalLookupStrategy = new TwoForwardDirectionalLookup(_dateExtractor);

            if (HasAssignedStrategy())
            {
                return RunAssignedStrategy(employmentSection, line);
            }
            else
            {
                return new Job();
            }
        }

        //this is what is called when its time to execute the specific strategy
        public Job RunAssignedStrategy(List<string> employmentSection, string line)
        {
            var lineSplit = line.Split(" ").ToList();
            var resultKVP = _directionalLookupStrategy.Execute(employmentSection, line);
            var foundDateRange = resultKVP.Key;
            var title = string.Empty;
            if(resultKVP.Key != string.Empty && resultKVP.Value != 0)
            {
                for (var i = 0; i < lineSplit.Count(); i++)
                {
                    var word = lineSplit.ElementAt(i);

                    if (lineSplit.Count() == 1 && _jobTitles.Any(x => x == word))
                    {
                        title = word;
                    }
                    else if (lineSplit.Count() == 1 && !_jobTitles.Any(x => x == word))
                    {
                        continue;
                    }
                    if (lineSplit.Count() >= i + 7 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4) + " " + lineSplit.ElementAt(i + 5) + " " + lineSplit.ElementAt(i + 6))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4) + " " + lineSplit.ElementAt(i + 5) + " " + lineSplit.ElementAt(i + 6);
                    }
                    else if (lineSplit.Count() >= i + 6 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4) + " " + lineSplit.ElementAt(i + 5))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4) + " " + lineSplit.ElementAt(i + 5);
                    }
                    else if (lineSplit.Count() >= i + 5 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3) + " " + lineSplit.ElementAt(i + 4);
                    }
                    else if (lineSplit.Count() >= i + 4 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2) + " " + lineSplit.ElementAt(i + 3);
                    }
                    else if (lineSplit.Count() >= i + 3 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1) + " " + lineSplit.ElementAt(i + 2);
                    }
                    else if (lineSplit.Count() >= i + 2 &&
                            _jobTitles.Any(x => x == (word + " " + lineSplit.ElementAt(i + 1))))
                    {
                        title = word + " " + lineSplit.ElementAt(i + 1);
                    }
                    else if (_jobTitles.Any(x => x == word))
                    {
                        title = word;
                    }
                }

                return new Job()
                {
                    Title = title,
                    MonthsInPosition = resultKVP.Value,
                    Experience = _experienceCalculator.CalculateJobExperience(resultKVP.Value)
                };
            }
            else
            {
                return null;
            }            
        }
    }
}