using Shopping.Web.Models;
using Shopping.Web.Services.IServices;
using Shopping.Web.Utils;

namespace Shopping.Web.Services
{
    public class ProductService
        : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Update(ProductModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<bool> DeleteById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }
    }
}
