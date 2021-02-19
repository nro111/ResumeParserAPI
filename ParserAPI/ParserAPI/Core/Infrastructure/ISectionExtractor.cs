using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Core.Infrastructure
{
    public interface ISectionExtractor
    {
        Dictionary<int, string> OrganizeResumeIntoSections(List<string> resumeTextList);
        Dictionary<string, List<string>> ExtractSectionContent(List<string> textList, Dictionary<int, string> sortedIndexDictionary);
        Dictionary<int, string> SortDictionaryByDescending(Dictionary<int, string> dictionary);
    }
}
