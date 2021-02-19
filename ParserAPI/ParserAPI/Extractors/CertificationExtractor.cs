using ParserAPI.Core;
using ParserAPI.Core.Infrastructure;
using ParserAPI.Models;
using System.Collections.Generic;
using System.Linq;
using ParserAPI.Core.Extensions;

namespace ParserAPI.Extractors
{
    class CertificationExtractor : ICertificationExtractor
    {
        public List<Certification> GetCertifications(List<string> resumeTextList)
        {
            var delimiterRepository = new DelimiterRepository();
            var certificationCollection = delimiterRepository.GetCertifications();
            var certPairs = new List<Certification>(); 

            for (int i = 0; i < resumeTextList.Count; i++)
            {                
                var line = resumeTextList.ElementAt(i).RemoveSpecialCharacters().Split(" "); 

                for(int j = 0; j < line.Length; j++)
                {
                    var word = line.ElementAt(j);

                    //again, we have to evaluate line length 1 more than expected since j is the indexed number and length is the literal number
                    //in other words, the indexed number will always evaluate to one less than the length
                    var potentialCertifications_NameUpTo_SixWords = certificationCollection.FindAll(x => (line.Length >= (j + 6)) && (x.name == word
                    || x.name == word + " " + line.ElementAt(j + 1)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3) + " " + line.ElementAt(j + 4)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3) + " " + line.ElementAt(j + 4) + " " + line.ElementAt(j + 5)
                    ));

                    var potentialCertifications_NameUpTo_FiveWords = certificationCollection.FindAll(x => (line.Length >= (j + 5)) && (x.name == word
                    || x.name == word + " " + line.ElementAt(j + 1)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3) + " " + line.ElementAt(j + 4)
                    ));

                    var potentialCertifications_NameUpTo_FourWords = certificationCollection.FindAll(x => (line.Length >= (j + 4)) && (x.name == word
                    || x.name == word + " " + line.ElementAt(j + 1)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2) + " " + line.ElementAt(j + 3)
                    ));

                    var potentialCertifications_NameUpTo_ThreeWords = certificationCollection.FindAll(x => (line.Length >= (j + 3)) && (x.name == word
                    || x.name == word + " " + line.ElementAt(j + 1)
                    || x.name == word + " " + line.ElementAt(j + 1) + " " + line.ElementAt(j + 2)
                    ));

                    var potentialCertifications_NameUpTo_TwoWords = certificationCollection.FindAll(x => (line.Length >= (j + 2)) && (x.name == word
                    || x.name == word + " " + line.ElementAt(j + 1)
                    ));

                    var potentialCertifications_NameUpTo_OneWord = certificationCollection.FindAll(x => x.name == word);

                    if(potentialCertifications_NameUpTo_SixWords.Count > 0 && !potentialCertifications_NameUpTo_SixWords.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_SixWords.Except(certPairs));
                    }
                    else if (potentialCertifications_NameUpTo_FiveWords.Count > 0 && !potentialCertifications_NameUpTo_FiveWords.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_FiveWords.Except(certPairs));
                    }
                    else if (potentialCertifications_NameUpTo_FourWords.Count > 0 && !potentialCertifications_NameUpTo_FourWords.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_FourWords.Except(certPairs));
                    }
                    else if (potentialCertifications_NameUpTo_ThreeWords.Count > 0 && !potentialCertifications_NameUpTo_ThreeWords.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_ThreeWords.Except(certPairs));
                    }
                    else if (potentialCertifications_NameUpTo_TwoWords.Count > 0 && !potentialCertifications_NameUpTo_TwoWords.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_TwoWords.Except(certPairs));
                    }
                    else if (potentialCertifications_NameUpTo_OneWord.Count > 0 && !potentialCertifications_NameUpTo_OneWord.Except(certPairs).Any())
                    {
                        certPairs.AddRange(potentialCertifications_NameUpTo_OneWord.Except(certPairs));
                    }
                }
            }
            
            return certPairs.ToList(); 
        }
    }
}