using Billing.Application.Services;
using Billing.Domain.Interfaces;
using Billing.Infra.Data;
using Billing.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Transient);
//builder.Services.AddDbContext<BillingDbContext>(options =>
//options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
//            maxRetryCount: 5,
//            maxRetryDelay: TimeSpan.FromSeconds(30),
//            errorNumbersToAdd: null
//        )));

builder.Services.AddScoped<IBillingRepository, BillingRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBillingService, BillingService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

