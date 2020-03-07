using Microsoft.AspNetCore.Identity;

namespace MSF.Domain
{
	public sealed class AppUser: IdentityUser
	{

		public string FirstName { get; set; }

		public string LastName { get; set; }

    }
}
