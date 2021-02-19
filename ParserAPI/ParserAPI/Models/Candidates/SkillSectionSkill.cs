using System.Collections.Generic;

namespace ParserAPI.Models.Candidates
{
    public class SkillSectionSkill
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement("name")]
        public string Name { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("type")]
        public List<string> Type { get; set; }
    }
}
