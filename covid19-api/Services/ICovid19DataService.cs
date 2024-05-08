using covid19_api.Dtos.CountryData;
using covid19_api.Models;

namespace covid19_api.Services
{
    public interface ICovid19DataService
    {
        
        Task<ServiceResponse<List<GetCountryDataDto>>> GetAllCaseDataTest();

        Task<ServiceResponse<List<GetCountryDataDto>>> GetCaseDataByCountry(string countryName);

        Task<ServiceResponse<List<CountryData>>> SaveDataToDb(string countryName);

        Task<ServiceResponse<List<CountryData>>> GetDatoFromDb();

        Task<ServiceResponse<List<CountryData>>> GetDatoFromDbByCountryName(string countryName);

        Task<ServiceResponse<List<CountryData>>> AddCase(AddCountryDataDto newCountryData);

        Task<ServiceResponse<CountryData>> UpdateCountryCase(UpdateCountryDataDto updateCountryData);

    }
}
