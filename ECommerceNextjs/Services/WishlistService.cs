using ECommerceNextjs.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerceNextjs.Services
{
    public class WishlistService
    {
        public readonly IMongoCollection<ProductModel> _productsCollection = null!;

        public WishlistService(IOptions<ECommerceDatabaseSettings> ecommerceDatasbaseSettings)
        {
            var mongoClient = new MongoClient(ecommerceDatasbaseSettings.Value.ConnectionString);
            var databaseName = mongoClient.GetDatabase(ecommerceDatasbaseSettings.Value.DatabaseName);
            _productsCollection = databaseName.GetCollection<ProductModel>(ecommerceDatasbaseSettings.Value.CollectionName);
        }

        public async Task addToWishlist (ProductModel product) =>
            await _productsCollection.InsertOneAsync(product);

        public async Task<List<ProductModel>> GetWishlistItems(string email) =>
            await _productsCollection.Find(x => x.email == email).ToListAsync();

        public async Task DeleteProductFromWishlist(long id) =>
            await _productsCollection.DeleteOneAsync(x => x.id == id);
    }
}
