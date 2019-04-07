namespace AocWikiTranslationHelper.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;
    using Contracts;

    public class ParsedText : INotifyPropertyChanged
    {
        private readonly ITextParser _textParser;

        private int _numberOfLinksInTranslation;
        private string _text;
        private string _reference;

        public int OriginalOffset { get; }

        public string Original { get; }

        public int NumberOfLinksInSource { get; }

        public int NumberOfLinksInTranslation
        {
            get => _numberOfLinksInTranslation;
            set
            {
                if (value == _numberOfLinksInTranslation) return;
                _numberOfLinksInTranslation = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
                NumberOfLinksInTranslation = _textParser.CountSimpleLinks(value);
            }
        }

        public string Reference
        {
            get => _reference;
            set
            {
                if (value == _reference) return;
                _reference = value;
                OnPropertyChanged();
            }
        }

        public ParsedText(int originalOffset,
                          string original,
                          int numberOfLinksInSource,
                          string text,
                          ITextParser textParser)
        {
            _textParser = textParser;
            OriginalOffset = originalOffset;
            Original = original;
            NumberOfLinksInSource = numberOfLinksInSource;
            Text = text;
        }

        public ParsedText(int originalOffset,
                          string original,
                          string reference)
        {
            OriginalOffset = originalOffset;
            Original = original;
            Reference = reference;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}