using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Models.Candidates
{
    public class Degree
    {
        public string ID { get; set; }
        public string School { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public double Experience { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
