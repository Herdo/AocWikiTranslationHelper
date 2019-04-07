namespace AocWikiTranslationHelper.Models
{
    using Newtonsoft.Json;

    public class WikiQueryResponse
    {
        [JsonProperty("batchcomplete")]
        public bool BatchComplete { get; set; }

        [JsonProperty("query")]
        public WikiQuery Query { get; set; }
    }
}