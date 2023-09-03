using BakeryShop;
using AutoMapper;

using Infrastructure.Generic;
using Infrastructure.Repository;
using Infrastructure.Service;
using Infrastructure.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAutoMapper(typeof(Mapping));

//account
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();

builder.Services.AddScoped<IAccountsService, AccountsService>();
//Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
//Product

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddScoped<IProductsService, ProductsService>();
//employee

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeesService>();
//promotion

builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

builder.Services.AddScoped<IPromotionService, PromotionService>();

//order

builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();

// customer
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddDbContext<EXDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EXDbContextConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
   
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
