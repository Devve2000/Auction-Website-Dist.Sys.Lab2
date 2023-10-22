using AuctionWebsite.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuctionWebsite.Data;
using AuctionWebsite.Areas.Identity.Data;
using Dist.Sys.Lab2.Core.Interfaces;
using AuctionWebsite.Core;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Injection
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionSqlPersistance>();

// db, with dependency injection
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));



//Identity conf. ***
builder.Services.AddDbContext<AuctionAppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionAppIdentityDbContextConnection")));

builder.Services.AddDefaultIdentity<AuctionAppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuctionAppIdentityDbContext>();

// *****************
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();
app.Run();
