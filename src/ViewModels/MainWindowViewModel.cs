namespace AocWikiTranslationHelper.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using Contracts;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using Models;

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Fields

        private readonly IWikiSourcePageFetcher _sourcePageFetcher;
        private readonly IAutoLocalizer _autoLocalizer;
        private readonly ITextParser _textParser;

        private string _textInput;
        private string _textOutput;
        private ParsedDocument _currentDocument;
        private ParsedText _selectedContent;
        private string _sourcePageUrl;

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Properties

        public event EventHandler<HighlightOriginTextEventArgs> HighlightOriginText;

        public string SourcePageUrl
        {
            get => _sourcePageUrl;
            set
            {
                if (value == _sourcePageUrl) return;
                _sourcePageUrl = value;
                RaisePropertyChanged();
            }
        }

        public string TextInput
        {
            get => _textInput;
            set
            {
                if (value == _textInput) return;
                _textInput = value;
                RaisePropertyChanged();
            }
        }

        public string TextOutput
        {
            get => _textOutput;
            set
            {
                if (value == _textOutput) return;
                _textOutput = value;
                RaisePropertyChanged();
            }
        }

        public ParsedText SelectedContent
        {
            get => _selectedContent;
            set
            {
                if (value == _selectedContent) return;
                _selectedContent = value;
                RaisePropertyChanged();
                HighlightOriginText?.Invoke(this, new HighlightOriginTextEventArgs(value));
            }
        }

        public ObservableCollection<ParsedText> ParsedContents { get; }

        public ICommand FetchSourcePageCommand { get; }
        public ICommand ParseInputCommand { get; }
        public ICommand BuildOutputCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Constructors

        public MainWindowViewModel(IWikiSourcePageFetcher sourcePageFetcher,
                                   IAutoLocalizer autoLocalizer,
                                   ITextParser textParser)
        {
            _sourcePageFetcher = sourcePageFetcher;
            _autoLocalizer = autoLocalizer;
            _textParser = textParser;
            ParsedContents = new ObservableCollection<ParsedText>();
            FetchSourcePageCommand = new RelayCommand(FetchSourcePage_Executed, FetchSourcePage_CanExecute);
            ParseInputCommand = new RelayCommand(ParseInput_Executed, ParseInput_CanExecute);
            BuildOutputCommand = new RelayCommand(BuildOutput_Executed, BuildOutput_CanExecute);
            CopyToClipboardCommand = new RelayCommand(CopyToClipboard_Executed);
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool FetchSourcePage_CanExecute() => !string.IsNullOrWhiteSpace(SourcePageUrl) && Uri.TryCreate(SourcePageUrl, UriKind.Absolute, out _);

        private async void FetchSourcePage_Executed()
        {
            var uri = new Uri(SourcePageUrl, UriKind.Absolute);
            ParsedContents.Clear();
            TextInput = await _sourcePageFetcher.FetchSourcePage(uri);
            // Parse automatically, if the content was fetched from the website
            if (ParseInputCommand.CanExecute(null)) ParseInputCommand.Execute(null);
        }

        private bool ParseInput_CanExecute() => !string.IsNullOrWhiteSpace(TextInput);

        private void ParseInput_Executed()
        {
            ParsedContents.Clear();
            TextOutput = string.Empty;
            var doc = _currentDocument = _textParser.ParseText(TextInput);
            foreach (var s in doc.Content.Where(m => m.Value != null))
            {
                if (s.Value.Text != null) s.Value.Text = _autoLocalizer.Localize(s.Value.Text);
                if (s.Value.Reference != null) s.Value.Reference = _autoLocalizer.Localize(s.Value.Reference);
                ParsedContents.Add(s.Value);
            }
        }

        private bool BuildOutput_CanExecute() => !string.IsNullOrWhiteSpace(TextInput) && ParsedContents.Count > 0 && ParsedContents.All(m => m.NumberOfLinksInSource == m.NumberOfLinksInTranslation);

        private void BuildOutput_Executed()
        {
            var sb = new StringBuilder();
            foreach (var (connector, value) in _currentDocument.Content)
            {
                sb.Append(connector ?? value.Text ?? value.Reference);
            }

            TextOutput = sb.ToString();
        }

        private void CopyToClipboard_Executed()
        {
            if (SelectedContent == null)
                return;

            if (!string.IsNullOrWhiteSpace(SelectedContent.Text))
            {
                Clipboard.SetDataObject(SelectedContent.Text);
                return;
            }

            if (!string.IsNullOrWhiteSpace(SelectedContent.Reference))
            {
                Clipboard.SetDataObject(SelectedContent.Reference);
            }
        }

        #endregion
    }
}