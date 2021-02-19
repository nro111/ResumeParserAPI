using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class Job
    {
        public string ID { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public int MonthsInPosition { get; set; }
        public double Experience { get; set; }
        public double Salary { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
