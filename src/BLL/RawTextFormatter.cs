namespace AocWikiTranslationHelper.BLL
{
    using System.Windows.Documents;
    using Xceed.Wpf.Toolkit;

    public class RawTextFormatter : ITextFormatter
    {
        string ITextFormatter.GetText(FlowDocument document)
        {
            var tr = new TextRange(document.ContentStart, document.ContentEnd);
            return tr.Text;
        }

        void ITextFormatter.SetText(FlowDocument document,
                                    string text)
        {
            document.Blocks.Clear();
            document.Blocks.Add(new Paragraph(new Run(text)));
        }
    }
}