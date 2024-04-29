using covid19_api.Models;


namespace covid19_api.Services
{
    public class Covid19DataService : ICovid19DataService
    {
        private readonly string apiEndpoint= "https://api.api-ninjas.com/v1/covid19?country=canada";
        private readonly string apiKey = "z7ypjrRoVu3bWygaDQaC2g==9zF0jFvHx1lKIBvF";
        public async Task<ServiceResponse<List<CountryData>>> GetAllCaseData()
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            List<CountryData> countryDataList = new List<CountryData>();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", this.apiKey);

            try
            {
                var response = await httpClient.GetAsync(this.apiEndpoint);
                string apiResponse = await response.Content.ReadAsStringAsync();
                countryDataList = JsonConvert.DeserializeObject<List<CountryData>>(apiResponse);

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

        public Task<ServiceResponse<List<CountryData>>> GetCaseDataByCountry(string countryName)
        {
            throw new NotImplementedException();
        }
    }
}
