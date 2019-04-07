namespace AocWikiTranslationHelper.DAL
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using Newtonsoft.Json;

    public class WikiSourcePageFetcher : IWikiSourcePageFetcher
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region IWikiSourcePageFetcher Members

        async Task<string> IWikiSourcePageFetcher.FetchSourcePage(Uri uri)
        {
            // Prepare
            var ub = new UriBuilder(uri.Scheme, uri.Host);
            var query = $"api.php?action=query&prop=revisions&rvprop=content&format=json&formatversion=2&titles={uri.LocalPath.Substring(1)}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = ub.Uri;
                using (var response = await client.GetAsync(query).ConfigureAwait(false))
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var converted = JsonConvert.DeserializeObject<WikiQueryResponse>(content);
                        return converted.Query.Pages[0].Revisions[0].Content;
                    }
                    else
                    {
                        return "Failed to fetch the page content!";
                    }
            }
        }

        #endregion
    }
}