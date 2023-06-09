using API.Domain.Cases;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Infra.Repositories;
using API.Infra.Services;
using API.Presenters.Cases;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySQL");
IDbConnection connection = new MySqlConnection(connectionString);

// Add services to the container.
builder.Services.AddSingleton<IDbConnection>(connection);

builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ILabelRepository, LabelRepository>();
builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Services.AddTransient<ISaleProductRepository, SaleProductRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddTransient<ICartProductRepository, CartProductRepository>();

builder.Services.AddTransient<IGetProductInfoCase, GetProductInfoCase>();
builder.Services.AddTransient<IGetAllLabelsCase, GetAllLabelsCase>();
builder.Services.AddTransient<IGetAllValidCouponsCase, GetAllValidCouponsCase>();
builder.Services.AddTransient<IGetAllProductsByCategoriesCase, GetAllProductsByCategoriesCase>();
builder.Services.AddTransient<IGetAllProductsBestSellersByCategoriesCase, GetAllProductsBestSellersByCategoriesCase>();
builder.Services.AddTransient<IFavoriteProductCase, FavoriteProductCase>();
builder.Services.AddTransient<ISearchFavoritesCase, SearchFavoritesCase>();
builder.Services.AddTransient<ICountFavoritesCase, CountFavoritesCase>();
builder.Services.AddTransient<ISaveProductToCartCase, SaveProductToCartCase>();
builder.Services.AddTransient<ICountCartProductsCase, CountCartProductsCase>();
builder.Services.AddTransient<IDeleteProductFromCartCase, DeleteProductFromCartCase>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
