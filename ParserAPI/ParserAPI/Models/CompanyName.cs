using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Models
{
    public class CompanyName
    {
        public ObjectId _id { get; set; }
        public List<string> CompanyNames { get; set; }
    }
}
