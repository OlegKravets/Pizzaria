using Pizzaria.Data;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Repositories;
using Pizzaria.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddRazorPages();
builder.Services.AddSession();

// DI for repositories
RegisterRepositories(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseSession();

app.Run();

static void RegisterRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IRepository<Pizza>, PizzaRepository>();
    builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
    builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
}