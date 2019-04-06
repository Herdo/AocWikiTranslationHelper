namespace AocWikiTranslationHelper.Contracts
{
    using Models;

    public interface ITextParser
    {
        ParsedDocument ParseText(string textInput);

        int CountSimpleLinks(string text);
    }
}