using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using ParserAPI.Core.Infrastructure;
//using Novacode;
using System;

namespace ParserAPI.Core
{
    public class TypeConverterTool : ITypeConverterTool
    {
        public List<string> ConvertWordDocumentToList(string path)
        {
            List<string> results = new List<string>();
            
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, false))
            {
                //text = wordDoc.MainDocumentPart.Document.InnerText;
                foreach (var paragraph in wordDoc.MainDocumentPart.Document.Body.Descendants<Paragraph>())
                {
                    results.Add(paragraph.InnerText);
                }
            }

            return results;
        }
    }
}
