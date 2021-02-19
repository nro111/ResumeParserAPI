using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Models
{
    public class DegreeViewModel
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement("level")]
        public string Level { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("type")]
        public string Type { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("degree")]
        public string Degree { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("experience")]
        public int Experience { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("skills")]
        public List<ParserAPI.Models.Candidates.Skill> Skills { get; set; }
    }
}
