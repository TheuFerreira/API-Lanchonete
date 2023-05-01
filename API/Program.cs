using API.Domain.Cases;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Infra.Repositories;
using API.Infra.Services;
using API.Presenters.Cases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ILabelRepository, LabelRepository>();
builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();
builder.Services.AddTransient<IGetAllProductsCase, GetAllProductsCase>();
builder.Services.AddTransient<IGetProductInfoCase, GetProductInfoCase>();
builder.Services.AddTransient<IGetAllLabelsCase, GetAllLabelsCase>();

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