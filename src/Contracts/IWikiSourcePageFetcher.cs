namespace AocWikiTranslationHelper.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IWikiSourcePageFetcher
    {
        Task<string> FetchSourcePage(Uri uri);
    }
}