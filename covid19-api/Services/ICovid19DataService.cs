using covid19_api.Models;

namespace covid19_api.Services
{
    public interface ICovid19DataService
    {
        Task<ServiceResponse<List<CountryData>>> GetAllCaseData();
        Task<ServiceResponse<List<CountryData>>> GetCaseDataByCountry(string countryName);

    }
}
