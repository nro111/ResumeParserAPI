using ParserAPI.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core.Infrastructure
{
    public interface IExperienceCalculator
    {
        public double CalculateOverallExperience(Candidate candidate);
        public List<Skill> CalculateSkillExperience(Candidate candidate);
        public double CalculateJobExperience(int months);
    }
}
