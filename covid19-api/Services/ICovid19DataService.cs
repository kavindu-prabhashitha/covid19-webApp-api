using covid19_api.Dtos.CountryData;
using covid19_api.Models;

namespace covid19_api.Services
{
    public interface ICovid19DataService
    {
        
        Task<ServiceResponse<List<GetCountryDataDto>>> GetAllCaseDataTest();
        Task<ServiceResponse<List<GetCountryDataDto>>> GetCaseDataByCountry(string countryName);

    }
}
