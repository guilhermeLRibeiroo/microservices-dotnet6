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

        public async Task<IEnumerable<ProductViewModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> FindById(long id, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<ProductViewModel> Create(ProductViewModel model, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<ProductViewModel> Update(ProductViewModel model, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PutAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<bool> DeleteById(long id, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong.");

            return await response.ReadContentAs<bool>();
        }
    }
}
