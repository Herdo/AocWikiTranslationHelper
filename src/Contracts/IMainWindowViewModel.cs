namespace AocWikiTranslationHelper.Contracts
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Models;

    public interface IMainWindowViewModel
    {
        event EventHandler<HighlightOriginTextEventArgs> HighlightOriginText;

        string TextInput { get; set; }
        string TextOutput { get; set; }

        ParsedText SelectedContent { get; set; }
        ObservableCollection<ParsedText> ParsedContents { get; }

        ICommand ParseInputCommand { get; }
        ICommand BuildOutputCommand { get; }
        ICommand CopyToClipboardCommand { get; }
    }
}