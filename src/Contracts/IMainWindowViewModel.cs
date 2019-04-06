namespace AocWikiTranslationHelper.Contracts
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Models;

    public interface IMainWindowViewModel
    {
        string TextInput { get; set; }
        string TextOutput { get; set; }

        ParsedText SelectedContent { get; set; }
        ObservableCollection<ParsedText> ParsedContents { get; }

        ICommand ParseInputCommand { get; }
        ICommand BuildOutputCommand { get; }
        ICommand CopyToClipboardCommand { get; }
    }
}