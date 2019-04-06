namespace AocWikiTranslationHelper.Models
{
    using System.Collections.Generic;

    public class ParsedDocument
    {
        public List<(string Connector, ParsedText Value)> Content { get; }

        public ParsedDocument()
        {
            Content = new List<(string Connector, ParsedText Value)>();
        }
    }
}