using ParserAPI.Models;
using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class Candidate
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Phone { get; set; }
        public string Email { get; set; }
        public double Experience { get; set; }
        public int ExpectedSalary { get; set; }
        public string Location { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Degree> Education { get; set; }
        public List<ParserAPI.Models.Certification> Certifications { get; set; }
        public List<Patent> Patents { get; set; }
        public List<Publication> Publications { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
