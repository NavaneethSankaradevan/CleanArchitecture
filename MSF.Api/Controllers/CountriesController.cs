using Microsoft.AspNetCore.Mvc;

namespace MSF.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		//private readonly ICountryService CountryService;

		//public CountriesController(ICountryService countryService)
		//{
		//    this.CountryService = countryService;
		//}

		//// GET: api/countries
		//[HttpGet]
		//public async Task<IActionResult> Get()
		//{
		//    return Ok(await CountryService.GetAllAsync());
		//}

		//// GET: api/countries/5
		//[HttpGet("{id:int}")]
		//public async Task<IActionResult> Get(int id)
		//{
		//    try
		//    {
		//        var product = await CountryService.GetAsync(id);
		//        if (product != null)
		//            return Ok(product);
		//        else
		//            throw new Exception("Not Found");
		//    }
		//    catch (Exception ex)
		//    {
		//        return StatusCode(500, ex.Message);
		//    }
		//}
	}
}