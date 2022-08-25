using Microsoft.AspNetCore.Mvc;
using MSF.Service;
using System;
using System.Threading.Tasks;

namespace MSF.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly ICountryService CountryService;

		public CountriesController(ICountryService countryService)
		{
			this.CountryService = countryService;
		}

		// GET: api/countries
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await CountryService.GetCountiresAsync());
			}
			catch (Exception ex)
			{ 
				return StatusCode(500, ex.Message);
			}
		}

		// GET: api/countries/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var product = await CountryService.GetCountryByIdAsync(id);
				if (product != null)
					return Ok(product);
				else
					throw new Exception("Not Found");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}