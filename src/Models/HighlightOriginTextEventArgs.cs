namespace AocWikiTranslationHelper.Models
{
    using System;

    public class HighlightOriginTextEventArgs : EventArgs
    {
        public ParsedText ParsedText { get; }

        public HighlightOriginTextEventArgs(ParsedText parsedText)
        {
            ParsedText = parsedText;
        }
    }
}