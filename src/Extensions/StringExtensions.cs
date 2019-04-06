namespace AocWikiTranslationHelper.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAndKeep(this string s, string separator)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            int start = 0, index;

            while ((index = s.IndexOf(separator, start, StringComparison.InvariantCulture)) != -1)
            {
                if (index - start > 0)
                    yield return s.Substring(start, index - start);
                yield return s.Substring(index, separator.Length);
                start = index + separator.Length;
            }

            if (start < s.Length)
            {
                yield return s.Substring(start);
            }
        }
    }
}