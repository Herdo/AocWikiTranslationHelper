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
            var offset = 0;

            foreach (var split in splits)
            {
                if (split == null)
                    continue;
                if (split == "{{"
                    || split == "}}")
                {
                    offset += split.Length + 2;
                    doc.Content.Add((split, null));
                }
                else if (string.IsNullOrWhiteSpace(split)
                         || split == string.Empty)
                {
                    offset += split.Length;
                    doc.Content.Add((split, null));
                }
                else
                {
                    var isReference = IsReference(split);
                    if (isReference)
                    {
                        doc.Content.Add((null, new ParsedText(offset, split, split)));
                    }
                    else
                    {
                        var (textWithoutLinks, numberOfLinks) = ReplaceSimpleLinks(split);
                        doc.Content.Add((null, new ParsedText(offset, split, numberOfLinks, textWithoutLinks, this)));
                    }

                    offset += split.Length;
                }
            }

            return doc;
        }

        int ITextParser.CountSimpleLinks(string text) => string.IsNullOrWhiteSpace(text) ? 0 : _simpleLinkRegex.Matches(text).Count;

        #endregion
    }
}