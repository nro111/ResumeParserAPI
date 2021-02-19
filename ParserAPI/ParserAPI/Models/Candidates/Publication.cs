using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class Publication
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public double Experience { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
