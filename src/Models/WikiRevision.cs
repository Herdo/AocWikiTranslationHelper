namespace AocWikiTranslationHelper.Models
{
    using Newtonsoft.Json;

    public class WikiRevision
    {
        [JsonProperty("contentformat")]
        public string ContentFormat { get; set; }

        [JsonProperty("contentmodel")]
        public string ContentModel { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}