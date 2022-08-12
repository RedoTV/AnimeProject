using Microsoft.EntityFrameworkCore;
using Animes.Models;
using Animes.HelperClasses;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AnimeContext>(options => options.UseSqlite("DataSource=Database/Animes.db"));
builder.Services.AddDbContext<UserContext>(options => options.UseSqlite("DataSource=Database/Users.db"));


builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login/login";
    options.AccessDeniedPath = "/accessdenied";
});



var app = builder.Build();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
