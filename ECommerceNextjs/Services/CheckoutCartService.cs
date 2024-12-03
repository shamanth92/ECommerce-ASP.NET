using ECommerceNextjs.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net.NetworkInformation;
using static MongoDB.Driver.WriteConcern;

namespace ECommerceNextjs.Services
{
    public class CheckoutCartService
    {
        public readonly IMongoCollection<CheckoutProductModel> _checkoutCartCollection = null!;

        public CheckoutCartService(IOptions<ECommerceDatabaseSettings> ecommerceDatasbaseSettings)
        {
            var mongoClient = new MongoClient(ecommerceDatasbaseSettings.Value.ConnectionString);
            var databaseName = mongoClient.GetDatabase(ecommerceDatasbaseSettings.Value.DatabaseName);
            _checkoutCartCollection = databaseName.GetCollection<CheckoutProductModel>(ecommerceDatasbaseSettings.Value.CheckoutCartCollection);
        }

        public async Task addItemsToCart(CheckoutProductModel product) =>
            await _checkoutCartCollection.InsertOneAsync(product);

        public async Task UpdateCartItemsQuantity(string email, string quantity, long id)
        {
            var filter = Builders<CheckoutProductModel>.Filter.Eq(doc => doc.id, id);
            var update = Builders<CheckoutProductModel>.Update.Inc(quantity, 1);

            var result = await _checkoutCartCollection.UpdateOneAsync(filter, update);
        }
            

        public async Task<List<CheckoutProductModel>> GetCartItems(string email) =>
            await _checkoutCartCollection.Find(x => x.email == email).ToListAsync();

        public async Task DeleteItemFromCart(long id) =>
            await _checkoutCartCollection.DeleteOneAsync(x => x.id == id);
    }
}
