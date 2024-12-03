using ECommerceNextjs.Models;
using System.Text.Json;
using System.Net.Http;

namespace ECommerceNextjs.Services
{
    public class FetchProductsService
    {
        private readonly HttpClient _httpClient;

        public FetchProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductModel>> GetProductDetails(string url)
        {
            var products = await _httpClient.GetAsync(url);
            Console.WriteLine(products);
            products.EnsureSuccessStatusCode();
            var jsonProducts = await products.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductModel>>(jsonProducts);

        }

        public async Task<ProductModel> GetProductDetailsByID(string url)
        {
            var products = await _httpClient.GetAsync(url);
            Console.WriteLine(products);
            products.EnsureSuccessStatusCode();
            var jsonProducts = await products.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductModel>(jsonProducts);

        }
    }
}
