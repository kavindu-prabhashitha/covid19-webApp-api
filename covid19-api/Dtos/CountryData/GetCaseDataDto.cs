namespace covid19_api.Dtos.CountryData
{
    public class GetCaseDataDto
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("new")]
        public int New { get; set; }
    }
}
