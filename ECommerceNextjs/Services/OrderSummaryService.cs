using ECommerceNextjs.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Globalization;

namespace ECommerceNextjs.Services
{
    public class OrderSummaryService
    {
        public readonly IMongoCollection<OrderSummaryModel> _orderSummaryCollection = null!;

        public OrderSummaryService(IOptions<ECommerceDatabaseSettings> ecommerceDatasbaseSettings)
        {
            var mongoClient = new MongoClient(ecommerceDatasbaseSettings.Value.ConnectionString);
            var databaseName = mongoClient.GetDatabase(ecommerceDatasbaseSettings.Value.DatabaseName);
            _orderSummaryCollection = databaseName.GetCollection<OrderSummaryModel>(ecommerceDatasbaseSettings.Value.OrderSummaryCollection);
        }

        public async Task storeOrderSummary(OrderSummaryModel orderSummary) =>
            await _orderSummaryCollection.InsertOneAsync(orderSummary);

        public async Task<List<OrderSummaryModel>> GetOrders(string email) =>
            await _orderSummaryCollection.Find(x => x.email == email).Sort(Builders<OrderSummaryModel>.Sort.Descending("dateOrdered")).ToListAsync();
    }
}
