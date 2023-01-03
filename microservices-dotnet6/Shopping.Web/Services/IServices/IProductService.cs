using Shopping.Web.Models;

namespace Shopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindById(long id);
        Task<ProductModel> Create(ProductModel model);
        Task<ProductModel> Update(ProductModel model);
        Task<bool> DeleteById(long id);
    }
}
