using MSF.Domain;
using MSF.Persistence;
using Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MSF.Service
{

    public interface IProductService
    {

        Task<List<ProductGetViewModel>> GetProducts();

        Task<List<ProductGetViewModel>> GetProductsForPage(int pageNo, int recordCount);

        Task<ProductGetViewModel> GetProductById(long Id);

        Task<ProductGetViewModel> SaveProduct(ProductViewModel product,string user);

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

        async Task<ProductGetViewModel> IProductService.GetProductById(long Id) => await productRepository.GetAsync(Id);
        
        async Task<List<ProductGetViewModel>> IProductService.GetProducts()
        {
            var products = await productRepository.GetAllAsync();
            return products.Select(p => (ProductGetViewModel)p).ToList();
        }

        async Task<List<ProductGetViewModel>> IProductService.GetProductsForPage(int pageNo, int recordCount)
        {

            // Default paging logic.
            pageNo = pageNo > 0 ? pageNo - 1 : 0;

            var products = await productRepository.GetAllPageData(p => p.ProductName, pageNo, recordCount);
            return products.Select(p => (ProductGetViewModel)p).ToList();
        }

        async Task<ProductGetViewModel> IProductService.SaveProduct(ProductViewModel product,string user)
        {
            Product updateProduct = product;
            await productRepository.SaveAsync(updateProduct, user);
            await unitOfWork.CommitAsync();

            return updateProduct;
        }
    }
}
