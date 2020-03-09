using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSF.Domain;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MSF.Service
{
	public interface IUserService
	{
		Task<AccessTokenResponse> GetAccessToken(LoginViewModel viewModel);

		Task<bool> RegisterUser(UserViewModel user);

		Task<bool> IsUniqueEmail(string EMail, string UserId);

	}

	internal class UserService : IUserService
	{
		private readonly SignInManager<AppUser> _signinManager;
		private readonly IConfiguration _config;

		public UserService(
			SignInManager<AppUser> signinManager,
			IConfiguration config
			)
		{
			this._signinManager = signinManager;
			this._config = config;
		}

		async Task<AccessTokenResponse> IUserService.GetAccessToken(LoginViewModel viewModel)
		{
			AppUser user = null;

			if (!string.IsNullOrEmpty(viewModel.UserEmail))
				user = await _signinManager.UserManager.FindByEmailAsync(viewModel.UserEmail);

			if (user != null)
			{
				var result = await _signinManager.CheckPasswordSignInAsync(user, viewModel.Password, false);

				if (result.Succeeded)
				{
					
					var userRoles = await _signinManager.UserManager.GetRolesAsync(user);

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
					var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

					string currentUserRole = userRoles.FirstOrDefault() ?? Role.ReadOnly.ToString();

					var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Email, user.Email),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
						new Claim(ClaimTypes.Role, currentUserRole)
					};

					var token = new JwtSecurityToken(
						_config["Jwt:Issuer"],
						_config["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(120),
						signingCredentials: creds);

					return new AccessTokenResponse
					{
						AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
						Expireation = token.ValidTo,
						User = string.IsNullOrEmpty(user.FirstName) ? user.UserName ?? user.Email : $"{user.FirstName} {user.LastName}"
					};
				}
				
				throw new Exception("Password is wrong!");
			}

			throw new Exception("User not available");
		}

		async Task<bool> IUserService.RegisterUser(UserViewModel userModel)
		{
			if (string.IsNullOrEmpty(userModel.UserEmail) || string.IsNullOrEmpty(userModel.Password))
				throw new Exception("User Email/Password is required");

			var newUser = new AppUser()
			{
				FirstName = userModel.FirstName,
				LastName = userModel.LastName,
				UserName = userModel.UserEmail,
				Email = userModel.UserEmail,
				NormalizedEmail = userModel.UserEmail,
				PasswordHash = userModel.Password
			};

			// Create the user.
			var result = await _signinManager.UserManager.CreateAsync(newUser, userModel.Password);

			if (result.Succeeded)
			{

				await _signinManager.UserManager.AddToRolesAsync(newUser,
					userModel.Roles.Select(r => r.ToString()).ToArray());
				return true;
			}
			
			throw new Exception(result.Errors.First().Description);
			

		}

		async Task<bool> IUserService.IsUniqueEmail(string eMail, string userId)
		{
			var user = await _signinManager.UserManager.FindByEmailAsync(eMail);
			return (user == null) || (user.Id != userId);
		}



	}
}