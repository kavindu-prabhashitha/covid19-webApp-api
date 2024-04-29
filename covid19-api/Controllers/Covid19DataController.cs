using covid19_api.Models;
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
        public async Task<ActionResult<ServiceResponse<List<CountryData>>>> Get()

        {
            var response = await this._covid19DataService.GetAllCaseData();
            return response;
        }


        [HttpGet("{country}")]
        public async Task<ActionResult<ServiceResponse<CountryData>>> GetCaseByCountry(string country)
        {
            var serviceResponse = await this._covid19DataService.GetCaseDataByCountry(country);
            return serviceResponse;
        }
    }


    
}
