namespace AocWikiTranslationHelper
{
    using System.Windows.Documents;
    using System.Windows.Media;
    using Contracts;

    public partial class MainWindow : IMainWindow
    {
        public MainWindow(IMainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.HighlightOriginText += ViewModel_HighlightOriginText;
        }

        private void ViewModel_HighlightOriginText(object sender, Models.HighlightOriginTextEventArgs e)
        {
            // Reset background from previous highlighting
            var completeTextRange = new TextRange(TextInputRtb.Document.ContentStart, TextInputRtb.Document.ContentEnd);
            completeTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);

            // Calculate positions
            var startPosition = TextInputRtb.Document.ContentStart.GetPositionAtOffset(e.ParsedText.OriginalOffset);
            var endPosition = TextInputRtb.Document.ContentStart.GetPositionAtOffset(e.ParsedText.OriginalOffset + e.ParsedText.Original.Length);
            if (startPosition == null || endPosition == null)
                return;

            // Highlight area
            var searchTextRange = new TextRange(startPosition, endPosition);
            searchTextRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }
    }
}
