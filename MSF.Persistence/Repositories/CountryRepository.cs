using MSF.Domain;
using Microsoft.Extensions.Logging;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Persistence
{

    public interface ICountryRepository:IReadRepository<Country, int> { }

    internal class CountryRepository : BaseRepository<Country, int>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
