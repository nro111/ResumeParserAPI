using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserAPI.Core.Infrastructure
{
    public interface ITypeConverterTool
    {
        List<string> ConvertWordDocumentToList(string path);
    }
}
