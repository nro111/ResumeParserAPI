using MongoDB.Bson;
using ParserAPI.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Models
{
    public class SkillList
    {
        public ObjectId _id { get; set; }
        public List<SkillSectionSkill> Skills { get; set; }
    }
}
