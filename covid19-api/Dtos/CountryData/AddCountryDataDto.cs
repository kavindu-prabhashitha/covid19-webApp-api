using covid19_api.Dtos.Case;

namespace covid19_api.Dtos.CountryData
{
    public class AddCountryDataDto
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public List<AddCaseDataDto> Cases { get; set; }
    }
}
