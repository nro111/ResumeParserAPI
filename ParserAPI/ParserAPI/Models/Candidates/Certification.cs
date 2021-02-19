using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class Certification
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CertifyingCompany { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
