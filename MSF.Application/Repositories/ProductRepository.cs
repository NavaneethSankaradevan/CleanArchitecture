using System.Linq;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using MSF.Domain;
using Microsoft.Extensions.Logging;

namespace MSF.Application
{
	public interface IProductRepository : IRepository<Product, long> { }

	internal class ProductRepository : BaseRepository<Product, long>, IProductRepository
	{
		public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override IQueryable<Product> GetEntitySet(bool incluedeDeleted = true)
		{
			return Entity
				.Include(e => e.Category)
				.Include(e => e.UOM)
				.Include(e => e.Tax)
				.Where(p => !incluedeDeleted || !p.InActive);
		}
        
	}
}
