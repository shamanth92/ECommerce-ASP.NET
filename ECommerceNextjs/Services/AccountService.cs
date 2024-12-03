using ECommerceNextjs.Models;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceNextjs.Services
{
    public class AccountService
    {
        public readonly IMongoCollection<SavedAddressModel> _addressCollection = null!;
        public readonly IMongoCollection<PaymentMethodModel> _paymentMethodCollection = null!;
        public readonly IMongoCollection<AccountModel> _accountCollection = null!;

        public AccountService(IOptions<ECommerceDatabaseSettings> ecommerceDatasbaseSettings)
        {
            var mongoClient = new MongoClient(ecommerceDatasbaseSettings.Value.ConnectionString);
            var databaseName = mongoClient.GetDatabase(ecommerceDatasbaseSettings.Value.DatabaseName);
            _addressCollection = databaseName.GetCollection<SavedAddressModel>(ecommerceDatasbaseSettings.Value.AddressCollectionName);
            _paymentMethodCollection = databaseName.GetCollection<PaymentMethodModel>(ecommerceDatasbaseSettings.Value.PaymentMethodCollectionName);
            _accountCollection = databaseName.GetCollection<AccountModel>(ecommerceDatasbaseSettings.Value.AccountCollectionName);
        }

        public async Task addAddress(SavedAddressModel address) =>
            await _addressCollection.InsertOneAsync(address);

        public async Task<List<SavedAddressModel>> GetAddresses(string email) =>
            await _addressCollection.Find(x => x.email == email).ToListAsync();

        public async Task DeleteAddress(string id) =>
           await _addressCollection.DeleteOneAsync(x => x._id == id);

        public async Task UpdateAddress(string id, SavedAddressModel updatedModel)
        {
            var filter = Builders<SavedAddressModel>.Filter.Eq("_id", id);
            await _addressCollection.ReplaceOneAsync(filter, updatedModel);
        }

        public async Task SetAsDefault(string id, string fieldName, bool value)
        {
            var filter = Builders<SavedAddressModel>.Filter.Eq("_id", id);
            var update = Builders<SavedAddressModel>.Update.Set(fieldName, value);
            await _addressCollection.UpdateOneAsync(filter, update);
        }

        public async Task addPaymentMethod(PaymentMethodModel paymentMethod) =>
            await _paymentMethodCollection.InsertOneAsync(paymentMethod);

        public async Task saveAccountDetails(AccountModel accountDetails) =>
            await _accountCollection.InsertOneAsync(accountDetails);

        public async Task<AccountModel> getAccountDetails(string email) =>
            await _accountCollection.Find(x => x.emailAddress == email).FirstOrDefaultAsync();

    }
}
