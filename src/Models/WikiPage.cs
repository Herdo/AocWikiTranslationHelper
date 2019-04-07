namespace AocWikiTranslationHelper.Models
{
    using Newtonsoft.Json;

    public class WikiPage
    {
        [JsonProperty("pageid")]
        public int PageID { get; set; }

        [JsonProperty("ns")]
        public int NamespaceID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("missing")]
        public bool Missing { get; set; }

        [JsonProperty("revisions")]
        public WikiRevision[] Revisions { get; set; }
    }
}