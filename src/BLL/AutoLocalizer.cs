namespace AocWikiTranslationHelper.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Contracts;

    public class AutoLocalizer : IAutoLocalizer
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Fields

        private readonly CultureInfo _germanCultureInfo;
        private readonly Dictionary<string, int> _monthMapping;
        private readonly Regex _dateRegex;

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Constructors

        public AutoLocalizer()
        {
            _germanCultureInfo = new CultureInfo("de-DE");
            _monthMapping = new Dictionary<string, int>
            {
                {"January", 1},
                {"February", 2},
                {"March", 3},
                {"April", 4},
                {"May", 5},
                {"June", 6},
                {"July", 7},
                {"August", 8},
                {"September", 9},
                {"October", 10},
                {"November", 11},
                {"December", 12}
            };
            _dateRegex = new Regex(@"(?<day>\d\d?) (?<month>(January|February|March|April|May|June|July|August|September|October|November|December)) (?<year>\d{4})");
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Private Methods

        private string ReplaceDates(string inputText)
        {
            var matches = _dateRegex.Matches(inputText);
            if (matches.Count == 0)
                return inputText;

            var resultText = inputText;
            foreach (Match match in matches)
            {
                var replacement = GetDateReplacement(match);
                resultText = resultText.Replace(match.Value, replacement);
            }

            return resultText;
        }

        private string GetDateReplacement(Match match)
        {
            var day = int.Parse(match.Groups["day"].Value);
            var month = _monthMapping[match.Groups["month"].Value];
            var year = int.Parse(match.Groups["year"].Value);

            // ReSharper disable once StringLiteralTypo
            return new DateTime(year, month, day).ToString("dd. MMMM yyyy", _germanCultureInfo);
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region IAutoLocalizer Members

        string IAutoLocalizer.Localize(string inputText)
        {
            return ReplaceDates(inputText);
        }

        #endregion
    }
}