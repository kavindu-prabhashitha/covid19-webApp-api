
namespace covid19_api.Models
{
    public class CaseData
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("new")]
        public int New {  get; set; }
    }
}
