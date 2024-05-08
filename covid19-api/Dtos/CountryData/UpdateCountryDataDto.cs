using covid19_api.Dtos.Case;

namespace covid19_api.Dtos.CountryData
{
    public class UpdateCountryDataDto
    {
        public int Id { get; set; }

        public string? Country { get; set; }

        public string? Region { get; set; }

        public UpdateCaseDto Case { get; set; }
    }
}
