using ParserAPI.Models.Candidates;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ParserAPI.Models
{    
    public class DegreeList
    {
        public ObjectId _id { get; set; }
        public List<DegreeViewModel> Degrees { get; set; }
    }
}
