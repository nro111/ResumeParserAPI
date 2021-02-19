using System;
using System.Collections.Generic;
using System.Text;

namespace ParserAPI.Core.Extensions
{
    internal static class ListTransformationExtension
    {       
        public static List<string> ToLower(this List<string> stringList)
        {
            stringList.ForEach(x => x.ToLower());
            return stringList;
        }
    }
}
