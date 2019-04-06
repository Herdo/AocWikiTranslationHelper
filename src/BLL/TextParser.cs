namespace AocWikiTranslationHelper.BLL
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Contracts;
    using Extensions;
    using Models;

    public class TextParser : ITextParser
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Fields

        private readonly Regex _simpleLinkRegex;

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Constructors

        public TextParser()
        {
            _simpleLinkRegex = new Regex(@"\[\[([\w# ]+\|)?(?<linkText>[\w ]+)\]\]");
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Private Methods

        private (string TextWithoutLinks, int NumberOfLinks) ReplaceSimpleLinks(string inputText)
        {
            var matches = _simpleLinkRegex.Matches(inputText);
            if (matches.Count == 0)
                return (inputText, 0);

            var resultText = inputText;
            foreach (Match match in matches)
            {
                resultText = resultText.Replace(match.Value, match.Groups["linkText"].Value);
            }

            return (resultText, matches.Count);
        }

        private static bool IsReference(string text) =>
            text.StartsWith("web|")
            || text.StartsWith("livestream|")
            || text.StartsWith("interview|");

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region ITextParser Members

        ParsedDocument ITextParser.ParseText(string textInput)
        {
            var splits = textInput.SplitAndKeep(Environment.NewLine)
                                           .SelectMany(m => m.SplitAndKeep("{{"))
                                           .SelectMany(m => m.SplitAndKeep("}}"))
                                           .ToArray();

            var doc = new ParsedDocument();

            foreach (var split in splits)
            {
                if (string.IsNullOrWhiteSpace(split)
                    || split == string.Empty
                    || split == "{{"
                    || split == "}}")
                {
                    doc.Content.Add((split, null));
                }
                else
                {
                    var isReference = IsReference(split);
                    if (isReference)
                    {
                        doc.Content.Add((null, new ParsedText(split, split)));
                    }
                    else
                    {
                        var (textWithoutLinks, numberOfLinks) = ReplaceSimpleLinks(split);
                        doc.Content.Add((null, new ParsedText(split, numberOfLinks, textWithoutLinks, this)));
                    }
                }
            }

            return doc;
        }

        int ITextParser.CountSimpleLinks(string text) => string.IsNullOrWhiteSpace(text) ? 0 : _simpleLinkRegex.Matches(text).Count;

        #endregion
    }
}