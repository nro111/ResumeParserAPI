using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAPI.Core.Infrastructure
{
    public interface IBasicInfoExtractor
    {
        List<string> GetEmailAddresses(List<string> textList);
        List<string> GetPhoneNumber(List<string> textList);
        KeyValuePair<string, string> GetName(List<string> textList);
    }
}
