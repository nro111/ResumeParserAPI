namespace ParserAPI.Models.Candidates
{
    public class Skill
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement("id")]
        public int ID { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("name")]
        public string Name { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("type")]
        public string Type { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("experience")]
        public double Experience { get; set; }
    }
}
