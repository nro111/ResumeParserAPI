using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Models
{
    public class ResumeTag
    {
        public ObjectId _id { get; set; }
        public string Type { get; set; }
        public List<string> ResumeTags { get; set; }
    }
}
