using MongoDB.Driver;
using ParserAPI.Core.Infrastructure;
using ParserAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParserAPI.Core
{
    class DelimiterRepository : IDelimiterRepository
    {        
        private IMongoDatabase _db { get; set; }

        public DelimiterRepository()
        {
            var connectionString = "mongodb://localhost:27017/CandidateMetrics";
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase("CandidateMetrics");
        }

        public List<string> GetFirstNames()
        {
            var firstNames = new List<string>();
            var firstNameCollection = _db.GetCollection<FirstName>("FirstNames").FindAsync(_ => true).Result.ToList();
            firstNameCollection.ForEach(x =>
            {
                firstNames.AddRange(x.FirstNames);
            });
            return firstNames;
        }

        public List<Certification> GetCertifications()
        {
            return _db.GetCollection<CertificationList>("Certifications").FindAsync(_ => true).Result.ToList().ElementAt(0).Certifications;
        }
        public List<SkillList> GetSkills()
        {
            return _db.GetCollection<SkillList>("Skills").FindAsync(_ => true).Result.ToList();
        }
        public List<DegreeList> GetDegrees()
        {
            var degrees = _db.GetCollection<DegreeList>("Degrees").FindAsync(_ => true).Result.ToList();
            return degrees;
        }
        public List<string> GetJobTitles()
        {
            return _db.GetCollection<JobTitle>("JobTitles").FindAsync(_ => true).Result.ToList().ElementAt(0).JobTitles;
        }
        public List<string> GetCompanyNames()
        {
            return _db.GetCollection<CompanyName>("CompanyNames").FindAsync(_ => true).Result.ToList().ElementAt(0).CompanyNames;
        }
        public List<string> GetSummaryTags()
        {
            return _db.GetCollection<ResumeTag>("ResumeTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetCareerTags()
        {
            return _db.GetCollection<ResumeTag>("CareerTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetAddressTags()
        {
            return _db.GetCollection<ResumeTag>("AddressTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetSkillsTags()
        {
            return _db.GetCollection<ResumeTag>("SkillsTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetCertificationTags()
        {
            return _db.GetCollection<ResumeTag>("CertificationTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetEducationTags()
        {
            return _db.GetCollection<ResumeTag>("EducationTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
        public List<string> GetResumeTags()
        {
            return _db.GetCollection<ResumeTag>("ResumeTags").FindAsync(_ => true).Result.ToList().ElementAt(0).ResumeTags;
        }
    }
}
