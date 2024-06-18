using AutoMapper;
using covid19_api.Dtos.CountryData;
using covid19_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;



namespace covid19_api.Services.Covid19Data
{
    public class Covid19DataService : ICovid19DataService
    {
        private readonly string apiEndpoint = "https://api.api-ninjas.com/v1/covid19";

        // API KEY from Net-Ninja API service
        private readonly string apiKey = "z7ypjrRoVu3bWygaDQaC2g==9zF0jFvHx1lKIBvF";
        private HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public Covid19DataService(IMapper mapper, DataContext context)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetCountryDataDto>>> GetAllCaseDataTest()
        {
            var serviceResponse = new ServiceResponse<List<GetCountryDataDto>>();
            List<GetCountryDataDto> countryDataList = new List<GetCountryDataDto>();

            try
            {
                var response = await _httpClient.GetAsync(apiEndpoint + "?country=canada");
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
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCountryDataDto>>> GetCaseDataByCountry(string countryName)
        {
            var serviceResponse = new ServiceResponse<List<GetCountryDataDto>>();
            List<GetCountryDataDto> countryDataList = new List<GetCountryDataDto>();

            try
            {
                var response = await _httpClient.GetAsync(apiEndpoint + "?country=" + countryName);
                string apiResponse = await response.Content.ReadAsStringAsync();
                countryDataList = JsonConvert.DeserializeObject<List<GetCountryDataDto>>(apiResponse);
                serviceResponse.Data = countryDataList;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CountryData>>> SaveDataToDb(string countryName)
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            List<GetCountryDataDto> countryDataList = new List<GetCountryDataDto>();
            List<CountryData> countryData = new List<CountryData>();
            try
            {
                var response = await _httpClient.GetAsync(apiEndpoint + "?country=" + countryName);
                string apiResponse = await response.Content.ReadAsStringAsync();
                countryDataList = JsonConvert.DeserializeObject<List<GetCountryDataDto>>(apiResponse);

                foreach (var item in countryDataList)
                {
                    var tempCountryData = new CountryData();
                    tempCountryData = _mapper.Map<CountryData>(item);
                    countryData.Add(tempCountryData);

                }

                foreach (var dbItem in countryData)
                {
                    _context.Add(dbItem);
                    await _context.SaveChangesAsync();
                }



                serviceResponse.Data = countryData;
                serviceResponse.Message = $"Imported Data into Db for {countryName}";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<CountryData>>> GetDatoFromDb()
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            var dbCountryData = await _context.CountryDatas.Include(c => c.Cases).ToListAsync();
            serviceResponse.Data = dbCountryData.Select(c => _mapper.Map<CountryData>(c)).ToList();
            serviceResponse.Message = "All Data From DB";

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<CountryData>>> GetDatoFromDbByCountryName(string countryName)
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            var dbCountryData = await _context.CountryDatas
                .Include(c => c.Cases)
                .Where(c => c.Country == countryName)
                .ToListAsync();
            serviceResponse.Data = dbCountryData.Select(c => _mapper.Map<CountryData>(c)).ToList();
            serviceResponse.Message = $"Covid 19 Data from {countryName}";
            serviceResponse.Success = true;

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<CountryData>>> AddCase(AddCountryDataDto newCountryData)
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            var countryData = _mapper.Map<CountryData>(newCountryData);
            _context.CountryDatas.Add(countryData);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.CountryDatas.Select(c => _mapper.Map<CountryData>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<CountryData>> UpdateCountryCase(UpdateCountryDataDto updateCountryData)
        {
            var serviceResponse = new ServiceResponse<CountryData>();
            try
            {
                var countryData = await _context.CountryDatas.FirstOrDefaultAsync(c => c.Id == updateCountryData.Id);
                var caseData = await _context.Cases.FirstOrDefaultAsync(c => c.Id.Equals(updateCountryData.Case.Id));
                if (countryData is null)
                {
                    throw new Exception($"Country Data with Id '{updateCountryData.Id} not found");
                }

                if (caseData is null)
                {
                    throw new Exception($"Cseata with Id '{updateCountryData.Case.Id}' not found");
                }



                countryData.Country = updateCountryData.Country;
                countryData.Region = updateCountryData.Region;

                caseData.Date = updateCountryData.Case.Date;
                caseData.New = updateCountryData.Case.New;
                caseData.Total = updateCountryData.Case.Total;

                await _context.SaveChangesAsync();

                serviceResponse.Data = countryData;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CountryData>>> getDBCountryList()
        {
            var serviceResponse = new ServiceResponse<List<CountryData>>();
            try
            {
                var countryList = await _context.CountryDatas.ToListAsync();
                serviceResponse.Data = countryList;
                serviceResponse.Success = true;
                serviceResponse.Message = "All Country Data List";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }


            return serviceResponse;
        }
    }


}
