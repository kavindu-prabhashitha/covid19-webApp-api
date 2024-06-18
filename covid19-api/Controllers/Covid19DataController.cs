
using covid19_api.Constants;
using covid19_api.Dtos.CountryData;
using covid19_api.Handlers;
using covid19_api.Services.Covid19Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace covid19_api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class Covid19DataController : ControllerBase
    {
        public readonly ICovid19DataService _covid19DataService;

        public Covid19DataController(ICovid19DataService covid19DataService)
        {
            this._covid19DataService = covid19DataService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCountryDataDto>>>> Get()
        {
            var response = await this._covid19DataService.GetAllCaseDataTest();
            return Ok(response);
        }


        [HttpGet("country")]
        public async Task<ActionResult<ServiceResponse<GetCountryDataDto>>> GetCaseByCountry()
        {
            string countryParam = "default";
            countryParam = HttpContext.Request.Query["country"];
            var serviceResponse = await this._covid19DataService.GetCaseDataByCountry(countryParam);
            return Ok(serviceResponse);
        }

        [HttpGet("save-data")]
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> SaveCaseDataToDB()
        {
            string countryParam = "default";
            countryParam = HttpContext.Request.Query["country"];
            var serviceResponse = await this._covid19DataService.SaveDataToDb(countryParam);
            return Ok(serviceResponse);
        }

        [HttpGet("get-db-data")]
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> GetCaseDataFromDB()
        {

            var serviceResponse = await this._covid19DataService.GetDatoFromDb();
            return Ok(serviceResponse);
        }

  
        [HttpGet("get-db-data-country")]
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> GetCaseDataFromDbByCountry()
        {
            string countryParam = "default";
            countryParam = HttpContext.Request.Query["country"];
            var serviceResponse = await this._covid19DataService.GetDatoFromDbByCountryName(countryParam);
            return Ok(serviceResponse);
        }

        [HttpPost]
        [HasPermission(Permissions.ADD_CASE)]
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> AddCountryData(AddCountryDataDto newCountryData)
        {
            return Ok(await this._covid19DataService.AddCase(newCountryData));
        }

        [HttpPut]
        [HasPermission(Permissions.UPDATE_CASE)]
        public async Task<ActionResult<ServiceResponse<CountryData>>> UpdateCountryCaseData(UpdateCountryDataDto updateCountryData)
        {
            var response = await _covid19DataService.UpdateCountryCase(updateCountryData);
            if (response is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-all-country-names")]
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> GetAllCountryNames()
        {
            var serviceResponse = await this._covid19DataService.getDBCountryList();
            return Ok(serviceResponse);
        }


    }
}
