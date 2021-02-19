using MongoDB.Driver;
using ParserAPI.Core;
using ParserAPI.Core.Infrastructure;
using ParserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserAPI.Extractors
{
    class BasicInfoExtractor : IBasicInfoExtractor
    {    
        public List<string> GetEmailAddresses(List<string> textList)
        {
            var foundEmailAddresses = new List<string>();
            var initialEmailAddressesFound = textList.FindAll(e => e.Contains('@'));

            for(int i = 0; i < initialEmailAddressesFound.Count; i++)
            {
                var potentialEmailAddress = initialEmailAddressesFound.ElementAt(i);
                var allWordsInEmailAddressLine = potentialEmailAddress.Split(" ").ToList();
                var foundEmailAddress = allWordsInEmailAddressLine.Find(x => x.Contains('@') && x.Contains(".com"));
                foundEmailAddresses.Add(foundEmailAddress); 
            }

            return foundEmailAddresses; 
        }

        public List<string> GetPhoneNumber(List<string> textList)
        {
            const string MatchPhonePattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

            Regex rx = new Regex(MatchPhonePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            List<string> phoneNumbers = new List<string>();

            for (int i = 0; i < textList.Count; i++)
            {
                var matches = rx.Matches(textList[i]);

                if (matches.Count > 1)
                {
                    for (var j = 0; j < matches.Count; j++)
                    {
                        phoneNumbers.Add(matches[j].Value.ToString());
                    }
                }
                else if (matches.Count == 1)
                {
                    phoneNumbers.Add(matches[0].Value.ToString());
                }
            }

            return phoneNumbers;
        }

        public KeyValuePair<string, string> GetName(List<string> textList)
        {
            var delimiterRepository = new DelimiterRepository();
            var firstNameCollection = delimiterRepository.GetFirstNames();

            string first = "";
            string last = "";

            for(int i = 0; i < textList.Count; i++)
            {
                var line = textList.ElementAt(i).Split(" "); 

                if(line.Length == 1 && line[0] == "")
                {
                    continue;
                }

                //in the majority of cases, the persons name is at the beginning. Further searching is not needed. 

                if(first != "" && last != "")
                {
                    break;
                }

                for(int j = 0; j < line.Length; j++)
                {
                    var word = line.ElementAt(j);
                    if (firstNameCollection.Contains(word))
                    {
                        first = word;

                        //in majority of cases, the last name will follow immediatly after the first.                         
                        if(line.ElementAt(j + 1) != "")
                        {
                            last = line.ElementAt(j + 1);
                        }
                        else if (line.ElementAt(j + 2) != "")
                        {
                            last = line.ElementAt(j + 2);
                        }                        
                    }
                }
            }

            return new KeyValuePair<string, string>(first, last);
        }
    }
}