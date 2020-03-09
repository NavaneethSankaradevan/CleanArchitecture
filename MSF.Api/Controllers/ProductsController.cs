using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSF.Service;
using System;
using System.Threading.Tasks;

namespace MSF.Api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	[Authorize(Policy = Constants.ReadOnlyAccess, AuthenticationSchemes = "Bearer")]
	public class ProductsController : ControllerBase
	{
		private IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<IActionResult> Get() => Ok(await _productService.GetProducts());
		
		// GET: api/Products/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var product = await _productService.GetProductById(id);
				if (product != null)
					return Ok(product);

				throw new Exception("Not Found");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        //api/products/page
        [HttpGet]
        [Route("perPage")]
        public async Task<IActionResult> GetProductsForPage([FromQuery] int pageNo, [FromQuery]int recordCount)
            => Ok(await _productService.GetProductsForPage( pageNo,  recordCount));

        // POST: api/Products
        [HttpPost]
		[Authorize(Policy = Constants.AddEditAccess)]
		public async Task<IActionResult> Post([FromBody] ProductViewModel product)
		{
			try
			{
				var productView = await _productService.SaveProduct(product, Common.GetLoggedInUser(User));

				if (productView.ProductId > 0)
				{
					return Ok(productView);
				}
				throw new Exception("Unable to save the Product.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id:int}")]
		[Authorize(Policy = Constants.AddEditDeleteAccess)]

		public async Task<IActionResult> Delete(int id)
		{
			if (await _productService.DeleteProduct(id, Common.GetLoggedInUser(User)))
				return Ok();

			return StatusCode(500, "Unable to deleted the product");

		}
	}
}
