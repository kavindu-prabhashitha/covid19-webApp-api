
namespace covid19_api.Models
{
    public class CountryData
    {
        [JsonProperty("country")] 
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("cases")]
        public Dictionary<DateTime, CaseData> Cases { get; set; }
    }
}
