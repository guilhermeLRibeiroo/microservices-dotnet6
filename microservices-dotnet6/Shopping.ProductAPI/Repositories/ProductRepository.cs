using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.ProductAPI.Models;
using Shopping.ProductAPI.Models.Context;
using Shopping.ProductAPI.ValueObjects;

namespace Shopping.ProductAPI.Repositories
{
    public class ProductRepository
        : IProductRepository
    {
        public readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == productId);
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO product)
        {
            var _product = _mapper.Map<Product>(product);

            _context.Products.Add(_product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(_product);
        }

        public async Task<ProductVO> Update(ProductVO product)
        {
            var _product = _mapper.Map<Product>(product);

            _context.Products.Update(_product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(_product);
        }

        public async Task<bool> Delete(long productId)
        {
            try
            {
                var product = await _context.Products
                          .FirstOrDefaultAsync(product => product.Id == productId);

                if (product == null)
                    return false;

                _context.Products.Remove(product);

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
