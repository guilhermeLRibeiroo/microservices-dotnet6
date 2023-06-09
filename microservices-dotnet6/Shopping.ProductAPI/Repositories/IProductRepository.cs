using Shopping.ProductAPI.ValueObjects;

namespace Shopping.ProductAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(long productId);
        Task<ProductVO> Create(ProductVO product);
        Task<ProductVO> Update(ProductVO product);
        Task<bool> Delete(long productId);
    }
}
