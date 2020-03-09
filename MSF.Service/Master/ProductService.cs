using MSF.Domain;
using MSF.Application;
using Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MSF.Service
{

    public interface IProductService
    {

        Task<List<ProductViewModel>> GetProducts();

        Task<List<ProductViewModel>> GetProductsForPage(int pageNo, int recordCount);

        Task<ProductViewModel> GetProductById(long Id);

        Task<ProductViewModel> SaveProduct(Product product,string user);



        Task<bool> DeleteProduct(long Id,string user);
    }

    internal class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork )
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        async Task<bool> IProductService.DeleteProduct(long Id, string user)
        {
            await productRepository.SoftDelete(Id,user);
            int result = await unitOfWork.CommitAsync();
            return result > 0;
        }

        async Task<ProductViewModel> IProductService.GetProductById(long Id) => await productRepository.GetAsync(Id);
        
        async Task<List<ProductViewModel>> IProductService.GetProducts()
        {
            var products = await productRepository.GetAllAsync();
            return products.Select(p => (ProductViewModel)p).ToList();
        }

        async Task<List<ProductViewModel>> IProductService.GetProductsForPage(int pageNo, int recordCount)
        {

            // Default paging logic.
            pageNo = pageNo > 0 ? pageNo - 1 : 0;

            var products = await productRepository.GetAllPageData(p => p.ProductName, pageNo, recordCount);
            return products.Select(p => (ProductViewModel)p).ToList();
        }

        async Task<ProductViewModel> IProductService.SaveProduct(Product product,string user)
        {
            await productRepository.SaveAsync(product,user);
            await unitOfWork.CommitAsync();

            return product;
        }
    }
}
