using ParserAPI.Core.Infrastructure;
using ParserAPI.Models.Candidates;
using System.Collections.Generic;

namespace ParserAPI.Core
{
    public class ExperienceCalculator : IExperienceCalculator
    {
        public double CalculateOverallExperience(Candidate candidate)
        {
            var result = 0.0;

            if(candidate.Education != null && candidate.Education.Count > 0)
                candidate.Education.ForEach(x =>
                {
                    result += x.Experience;
                });

            if(candidate.Jobs != null && candidate.Jobs.Count > 0)
                candidate.Jobs.ForEach(x =>
                {
                    result += x.Experience;
                });

            if(candidate.Patents != null && candidate.Patents.Count > 0)
                candidate.Patents.ForEach(x =>
                {
                    result += x.Experience;
                });

            if(candidate.Publications != null && candidate.Publications.Count > 0)
                candidate.Publications.ForEach(x =>
                {
                    result += x.Experience;
                });

            return result;
        }
        public double CalculateJobExperience(int months)
        {
            return months * 1.25;
        }
        public List<Skill> CalculateSkillExperience(Candidate candidate)
        {
            return new List<Skill>();
        }
    }
}