using covid19_api.Dtos.CountryData;
using covid19_api.Models;
using System.Diagnostics.Metrics;


namespace covid19_api.Services
{
    public class Covid19DataService : ICovid19DataService
    {
        private readonly string apiEndpoint= "https://api.api-ninjas.com/v1/covid19";
        private readonly string apiKey = "z7ypjrRoVu3bWygaDQaC2g==9zF0jFvHx1lKIBvF";
        private HttpClient _httpClient;

        public Covid19DataService()
        {
            this._httpClient = new HttpClient();
            this._httpClient.DefaultRequestHeaders.Add("X-Api-Key", this.apiKey);
        }


        public async Task<ServiceResponse<List<GetCountryDataDto>>> GetAllCaseDataTest()
        {
            var serviceResponse = new ServiceResponse<List<GetCountryDataDto>>();
            List<GetCountryDataDto> countryDataList = new List<GetCountryDataDto>();
            
            try
            {
                var response = await this._httpClient.GetAsync(this.apiEndpoint+"?country=canada");
                string apiResponse = await response.Content.ReadAsStringAsync();
                countryDataList = JsonConvert.DeserializeObject<List<GetCountryDataDto>>(apiResponse);

                if (response.IsSuccessStatusCode)
                {
                    serviceResponse.Data = countryDataList;
                    serviceResponse.Message = "All Data Related to country";
                }
                else
                {
                    throw new Exception("Request Failed");
                }

            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async  Task<ServiceResponse<List<GetCountryDataDto>>> GetCaseDataByCountry(string countryName)
        {
            var serviceResponse = new ServiceResponse<List<GetCountryDataDto>>();
            List<GetCountryDataDto> countryDataList = new List<GetCountryDataDto>();

            try
            {
                var response = await this._httpClient.GetAsync(this.apiEndpoint + "?country="+countryName);
                string apiResponse = await response.Content.ReadAsStringAsync();
                countryDataList = JsonConvert.DeserializeObject<List<GetCountryDataDto>>(apiResponse);
                serviceResponse.Data = countryDataList;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
    }
}
