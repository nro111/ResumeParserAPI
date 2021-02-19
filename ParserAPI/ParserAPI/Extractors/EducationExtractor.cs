using ParserAPI.Core.Infrastructure;
using ParserAPI.Models.Candidates;
using System.Collections.Generic;

namespace ParserAPI.Extractors
{
    public class EducationExtractor : IEducationExtractor
    {
        IDelimiterRepository _delimiterRepository;

        public EducationExtractor(IDelimiterRepository delimiterRepository)
        {
            _delimiterRepository = delimiterRepository;
        }
        public List<Degree> GetDegrees(List<string> educationSection)
        {
            var allPossibleDegrees = _delimiterRepository.GetDegrees();
            var degrees = new List<Degree>();
            var previousLine = string.Empty;
            foreach (var degreeList in allPossibleDegrees)
            {                
                foreach(var line in educationSection)
                {
                    var result = degreeList.Degrees.Find(x => line.ToLower().Contains(x.Degree));
                    if (((line.ToLower().Contains("bachelor") || previousLine.ToLower().Contains("bachelor")) ||
                        (line.ToLower().Contains("bachelors") || previousLine.ToLower().Contains("bachelors")) ||
                        (line.ToLower().Contains("b.s") || previousLine.ToLower().Contains("b.s") ||
                        line.ToLower().Contains("bsc") || previousLine.ToLower().Contains("bsc"))) &&
                        (result != null && result.Level.ToLower() == "bachelors"))
                    {
                        degrees.Add(new Degree()
                        {
                            Name = result.Degree,
                            Level = result.Level,
                            Skills = result.Skills,
                            Experience = result.Experience,
                            Type = result.Type
                        });
                    }
                    if (((line.ToLower().Contains("master") || previousLine.ToLower().Contains("master")) ||
                        (line.ToLower().Contains("masters") || previousLine.ToLower().Contains("masters")) ||
                        (line.ToLower().Contains("m.s") || previousLine.ToLower().Contains("m.s") ||
                        line.ToLower().Contains("msc") || previousLine.ToLower().Contains("msc"))) &&
                        (result != null && result.Level.ToLower() == "masters"))
                    {
                        degrees.Add(new Degree()
                        {
                            Name = result.Degree,
                            Level = result.Level,
                            Skills = result.Skills,
                            Experience = result.Experience,
                            Type = result.Type
                        });
                    }
                    if ((line.ToLower().Contains("phd") || line.ToLower().Contains("doctorate") || line.ToLower().Contains("doctoral") || previousLine.ToLower().Contains("phd") || previousLine.ToLower().Contains("doctorate") || previousLine.ToLower().Contains("doctoral")) &&
                        (result != null && result.Level.ToLower() == "phd"))
                    {
                        degrees.Add(new Degree()
                        {
                            Name = result.Degree,
                            Level = result.Level,
                            Skills = result.Skills,
                            Experience = result.Experience,
                            Type = result.Type
                        });
                    }
                    previousLine = line;
                }
            }

            return degrees;
        }
    }
}