using MSF.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSF.UnitTest
{
	class CategoryTest : TestBase
	{

		private ICategoryService categoryService;

		[SetUp]
		public void Initiate()
		{
			Setup();
			categoryService = GetService<ICategoryService>();			
		}

		[Test]
		public async Task GetCategories()
		{
			var categories = await categoryService.GetAllCategories();
			Assert.IsNotNull(categories);
		}

		[Test]
		public async Task AddCategory()
		{
			await categoryService.SaveCategory(new Domain.Category { CategoryName= "Category 1"  });

			var categories = await categoryService.GetAllCategories();
			Assert.IsNotNull(categories);
		}
	}
}
