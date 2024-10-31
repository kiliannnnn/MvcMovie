using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data.Models;
using System.Reflection;
using MvcMovie.Core.ServiceHelpers;
using MvcMovie.Core.Helpers;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddViewLocalization();

builder.Services.AddLocalization(options => {
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options => 
{
    var supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
    };
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

Assembly.Load("MvcMovie.Core");
builder.Services.AddIServices();

builder.Services
    .AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MvcMovie.Auth";
        options.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedUserPolicy", 
        policyBuilder => policyBuilder.RequireAuthenticatedUser()
    );
});

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();
/*
     .AddMvcOptions(options =>options.Filters.Add(new AuthorizeFilter("AuthenticatedUserPolicy")))
*/

builder.Services.AddMvcMovieDbContext<MvcMovieContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovie"));
        options.UseLazyLoadingProxies();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions.Value);

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

app.Run();
