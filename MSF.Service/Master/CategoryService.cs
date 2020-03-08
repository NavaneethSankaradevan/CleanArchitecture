using MSF.Domain;
using MSF.Application;
using Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSF.Service
{
    public interface ICategoryService
    {
        Task< List<Category>>  GetAllCategories();

        Task<Category> GetCategoryById(int Id);

        Task<int> SaveCategory(Category category,string user);

        Task<bool> DeleteCategory(int Id,string user);
    }

    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository, 
            IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        async Task<bool> ICategoryService.DeleteCategory(int Id,string user)
        {
            await categoryRepository.SoftDelete(Id,user);
            int result = await unitOfWork.CommitAsync();
            return result > 0;
        }

        async Task<List<Category>> ICategoryService.GetAllCategories()
        {
            return await categoryRepository.GetAllAsync();
        }

        async Task<Category> ICategoryService.GetCategoryById(int Id)
        {
            return await categoryRepository.GetAsync(Id);
        }

        async Task<int> ICategoryService.SaveCategory(Category category,string user)
        {
            await categoryRepository.SaveAsync(category,user);
            int result = await unitOfWork.CommitAsync();

            if (result > 0) return category.ID;

            return 0;
        }
    }
}
