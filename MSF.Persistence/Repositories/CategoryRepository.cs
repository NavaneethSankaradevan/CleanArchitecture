using MSF.Domain;
using Core.Data;

namespace MSF.Persistence
{
	public interface ICategoryRepository : IRepository<Category, int> { }

	internal class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
	{
		public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
            
		}
	}
}
