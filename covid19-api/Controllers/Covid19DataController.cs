
using covid19_api.Dtos.CountryData;
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
            public async Task<ActionResult<ServiceResponse<CountryData>>> SaveCaseDataToDB()
          {
              string countryParam = "default";
              countryParam = HttpContext.Request.Query["country"];
              var serviceResponse = await this._covid19DataService.SaveDataToDb(countryParam);
              return Ok(serviceResponse);
          }


    }
}
