using Application.Interfaces.AreaRules;
using Application.Interfaces.Areas;
using Application.Interfaces.Calculators;
using Application.Interfaces.Contexts;
using Application.Interfaces.Holidays;
using Application.Interfaces.Vehicles;
using Application.Services.AreaRules;
using Application.Services.Areas;
using Application.Services.Calculators;
using Application.Services.Holidays;
using Application.Services.Vehicles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region ConnectToDatabase

string SqlConnectionString = builder.Configuration["ConnectionStrings:Sql"];
//
builder.Services.AddDbContext<BaseContext>(Options =>
{
    Options.UseSqlServer(SqlConnectionString);
});

#endregion

#region DependencyInjections

builder.Services.AddScoped<IBaseContext,BaseContext>();
builder.Services.AddTransient<IAreaService,AreaService>();
builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<IAreaRuleService, AreaRuleService>();
builder.Services.AddTransient<IHolidayService, HolidayService>();
builder.Services.AddTransient<ICalculatorService, CalculatorService>();

#endregion

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
    pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();
