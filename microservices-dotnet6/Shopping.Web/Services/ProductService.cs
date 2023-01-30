using Shopping.Web.Models;
using Shopping.Web.Services.IServices;
using Shopping.Web.Utils;
using System.Net.Http.Headers;

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

        public async Task<IEnumerable<ProductModel>> FindAll(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Update(ProductModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<bool> DeleteById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }
    }
}
