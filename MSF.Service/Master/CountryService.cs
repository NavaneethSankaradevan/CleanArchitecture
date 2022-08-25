using MSF.Application;
using MSF.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSF.Service
{
	public interface ICountryService
	{
		Task<IEnumerable<Country>> GetCountiresAsync();

		Task<Country> GetCountryByIdAsync(int id);
	}

	internal class CountryService : ICountryService
	{
		private readonly ICountryRepository repository;

		public CountryService(ICountryRepository repository)
		{
			this.repository = repository;
		}

		async Task<IEnumerable<Country>> ICountryService.GetCountiresAsync()
		{
			return await repository.GetAllAsync();
		}

		async Task<Country> ICountryService.GetCountryByIdAsync(int id)
		{
			return await repository.GetAsync(id);
		}
	}
}
