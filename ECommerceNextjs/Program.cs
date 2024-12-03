using ECommerceNextjs.Models;
using ECommerceNextjs.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ECommerceDatabaseSettings>(builder.Configuration.GetSection("ECommerceDatabase"));
builder.Services.AddSingleton<WishlistService>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<CheckoutCartService>();
builder.Services.AddSingleton<FetchProductsService>();
builder.Services.AddSingleton<OrderSummaryService>();
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddHttpClient();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(Path.Combine("ServiceAccount", "serviceAccount.json")),
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
