namespace AocWikiTranslationHelper.Models
{
    using Newtonsoft.Json;

    public class WikiQuery
    {
        [JsonProperty("pages")]
        public WikiPage[] Pages { get; set; }
    }
}