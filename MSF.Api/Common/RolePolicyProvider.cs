using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MSF.Service;
using System.Threading.Tasks;

namespace MSF.API
{

	/// <summary>
	/// Role based Authorization Policy Provider
	/// </summary>
	public class RoleBasedPolicyProvider : DefaultAuthorizationPolicyProvider
	{
		public RoleBasedPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
		{
		}

		/// <summary>
		/// Override method to handle custom role based authorization.
		/// </summary>
		/// <param name="policyName"></param>
		/// <returns></returns>
		public async override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			AuthorizationPolicyBuilder builder = new AuthorizationPolicyBuilder();
			builder.RequireAuthenticatedUser();

			switch (policyName)
			{
				case Constants.AdminAccess:
					builder.RequireRole(Role.Admin.ToString());
					break;
				case Constants.AddEditDeleteAccess:
					builder.RequireRole(Role.AddEditDelete.ToString(), Role.Admin.ToString());
					break;
				case Constants.AddEditAccess:
					builder.RequireRole(Role.AddEdit.ToString(), Role.AddEditDelete.ToString(), Role.Admin.ToString());
					break;
				default:
					builder.RequireRole(Domain.Global.RoleList);
					break;
			}
			return await Task.FromResult(builder.Build());
		}
	}
}
