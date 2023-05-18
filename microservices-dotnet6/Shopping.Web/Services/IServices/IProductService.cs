using Shopping.Web.Models;

namespace Shopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAll();
        Task<ProductViewModel> FindById(long id, string token);
        Task<ProductViewModel> Create(ProductViewModel model, string token);
        Task<ProductViewModel> Update(ProductViewModel model, string token);
        Task<bool> DeleteById(long id, string token);
    }
}
