using MSF.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSF.UnitTest
{
	class ProductServiceTest:TestBase
	{
		private IProductService productService;

		[SetUp]
		public void Initiate()
		{
			Setup();
			productService = GetService<IProductService>();
		}

		[Test]
		public async Task GetAllProductTest()
		{
			var products =await productService.GetProducts();
			Assert.IsNotNull(products);
		}


		[Test]
		public async Task SaveProduct()
		{
			var result = await productService.SaveProduct(new Domain.Product { ProductCode="001", ProductName ="Product 1", CategoryId = 1 });
			Assert.IsNotNull(result);
		}
	}
}
