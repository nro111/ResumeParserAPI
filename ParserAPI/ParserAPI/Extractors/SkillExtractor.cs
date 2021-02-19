using ParserAPI.Core.Infrastructure;
using ParserAPI.Models.Candidates;
using System.Collections.Generic;
using System.Linq;

namespace ParserAPI.Extractors
{
    public class SkillExtractor : ISkillExtractor
    {
        IDelimiterRepository _delimiterRepository;
        public SkillExtractor(IDelimiterRepository delimiterRepository)
        {
            _delimiterRepository = delimiterRepository;
        }
        public List<Skill> GetSkills(List<string> skillSection)
        {
            var allPossibleSkills = _delimiterRepository.GetSkills();
            var skills = new List<Skill>();
            
            foreach(var line in skillSection)
            {
                foreach(var skillList in allPossibleSkills)
                {
                    foreach(var skill in skillList.Skills)
                    {
                        var wordArray = line.ToLower().Replace(",", "").Split(" ").ToList();
                        if (wordArray.Exists(x => x == skill.Name)) 
                        {
                            skills.Add(new Skill()
                            {
                                Name = skill.Name,
                                Type = skill.Type[0]
                            });
                        }
                    }
                }
            }

            return skills;
        }
    }
}