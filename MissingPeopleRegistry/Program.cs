using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MissingPeopleRegistry.Models;
using MissingPeopleRegistry.Repositories;
using MissingPeopleRegistry.Services;

var builder = WebApplication.CreateBuilder(args);

var connecetionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connecetionString));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedEmail = false)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IMissingPeopleRepository, MissingPeopleRepository>();
builder.Services.AddTransient<IMissingPeopleService, MissingPeopleService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
