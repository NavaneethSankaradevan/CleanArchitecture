using MSF.Domain;
using MSF.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSF.UnitTest
{
	class UserServiceTest : TestBase
	{
		readonly IUserService userService;

		public UserServiceTest()
		{
			Setup();
			userService = GetService<IUserService>();
		}

		[Test]
		public async Task VerifyUserRegistration()
		{
			
			//await GetTenants();
			await RegisterUser();

			Assert.Pass();
		}

		[Test]
		public async Task VerifyGetToken()
		{
			
			await RegisterUser();

			var token = await userService.GetAccessToken(new LoginViewModel { UserEmail = "Test@Test.com", Password = "Test@123" });
			Assert.IsNotNull(token);

		}

		private async Task RegisterUser()
		{
			string userName = "Test@Test.com";

			if ( await userService.IsUniqueEmail(userName, null))
			{
				bool result = await userService.RegisterUser(new UserViewModel { UserEmail = userName, Password = "Test@123"});
				await UnitOfWork.CommitAsync();
				
				Assert.IsTrue(result);
			}

		}

	}
}
