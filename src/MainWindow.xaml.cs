namespace AocWikiTranslationHelper
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Documents;
    using System.Windows.Media;
    using Contracts;
    using ViewModels;

    public partial class MainWindow : IMainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.HighlightOriginText += ViewModel_HighlightOriginText;
        }

        private void ViewModel_HighlightOriginText(object sender, Models.HighlightOriginTextEventArgs e)
        {
            var completeTextRange = new TextRange(TextInputRtb.Document.ContentStart, TextInputRtb.Document.ContentEnd);
            completeTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
            var findResultIndex = completeTextRange.Text.IndexOf(e.ParsedText.Original, StringComparison.InvariantCulture);
            var findResultStartOffset = TextInputRtb.Document.ContentStart.GetPositionAtOffset(findResultIndex);
            var findResultEndOffset = TextInputRtb.Document.ContentStart.GetPositionAtOffset(findResultIndex + e.ParsedText.Original.Length);
            var searchTextRange = new TextRange(findResultStartOffset, findResultEndOffset);
            searchTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
            //var wordRanges = GetAllWordRanges(TextInputRtb.Document);
            //foreach (var wordRange in wordRanges)
            //    if (wordRange.Text == "corruption")
            //        wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }

        //private static IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        //{
        //    var pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
        //    var pointer = document.ContentStart;
        //    while (pointer != null)
        //    {
        //        if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
        //        {
        //            var textRun = pointer.GetTextInRun(LogicalDirection.Forward);
        //            var matches = Regex.Matches(textRun, pattern);
        //            foreach (Match match in matches)
        //            {
        //                var startIndex = match.Index;
        //                var length = match.Length;
        //                var start = pointer.GetPositionAtOffset(startIndex);
        //                var end = start.GetPositionAtOffset(length);
        //                yield return new TextRange(start, end);
        //            }
        //        }

        //        pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
        //    }
        //}
    }
}
