using Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSF.Domain;
using MSF.Service;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MSF.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Policy = Constants.ReadOnlyAccess, AuthenticationSchemes = "Bearer")]
	public class CategoriesController : ControllerBase
	{

		private ICategoryService _categoryService;
		private IUnitOfWork _unitOfWork;

		public CategoriesController(ICategoryService categoryService,
			IUnitOfWork unitOfWork)
		{
			_categoryService = categoryService;
			_unitOfWork = unitOfWork;
		}

		// GET: api/Categories
		[HttpGet]
        public async Task<IActionResult> Get() => Ok(await _categoryService.GetAllCategories());

		// GET: api/Categories/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var category = await _categoryService.GetCategoryById(id);
                if (category != null)
					return Ok(category);

				throw new Exception("Not Found");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

		// POST: api/Categories
		[HttpPost]
		[Authorize(Policy = Constants.AddEditAccess, AuthenticationSchemes = "Bearer")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Post([FromBody] Category category)
		{
			
			int id = await _categoryService.SaveCategory(category, Common.GetLoggedInUser(User));

			if (id > 0)
				return Ok(id);

			return StatusCode(500, "Data Not saved to database. Please ");
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		[Authorize(Policy = Constants.AddEditDeleteAccess, AuthenticationSchemes = "Bearer")]
		public async Task<IActionResult> Delete(int id)
		{
			using (var tran = _unitOfWork.DataContext.Database.BeginTransaction())
			{

				var result = await _categoryService.DeleteCategory(id, Common.GetLoggedInUser(User)); // Set Deleted flag.

				if (result)
				{
					return Ok();
				}

				return StatusCode(500, "Unable to delete the project.");

			}
		}

	}
}
