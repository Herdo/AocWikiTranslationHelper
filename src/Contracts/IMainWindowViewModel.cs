namespace AocWikiTranslationHelper.Contracts
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Models;

    public interface IMainWindowViewModel
    {
        event EventHandler<HighlightOriginTextEventArgs> HighlightOriginText;

        string SourcePageUrl { get; set; }

        string TextInput { get; set; }
        string TextOutput { get; set; }

        ParsedText SelectedContent { get; set; }
        ObservableCollection<ParsedText> ParsedContents { get; }

        ICommand FetchSourcePageCommand { get; }
        ICommand ParseInputCommand { get; }
        ICommand BuildOutputCommand { get; }
        ICommand CopyToClipboardCommand { get; }
    }
}