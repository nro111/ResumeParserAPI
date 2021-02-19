using ParserAPI.Models.Candidates;
using System.Collections.Generic;

namespace ParserAPI.Core.Infrastructure
{
    public interface ISkillExtractor
    {
        public List<Skill> GetSkills(List<string> skillSection); 
    }
}
