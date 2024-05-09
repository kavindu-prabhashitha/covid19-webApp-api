using covid19_api.Models;

namespace covid19_api.Dtos.CountryData
{
    public class GetCountryDataDto
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("cases")]
        public Dictionary<DateTime, GetCaseDataDto> Cases { get; set; }
    }
}
