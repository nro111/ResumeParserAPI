using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class Patent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PatentNumber { get; set; }
        public double Experience { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
