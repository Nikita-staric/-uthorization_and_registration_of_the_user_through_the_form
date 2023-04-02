using Authentication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => //CookieAuthenticationOptions
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");// который будет выполнять регистрацию и логин пользователей.
    });
builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy("OnlyForLondon ", police =>
    {
        police.RequireClaim(ClaimTypes.Locality, "London", "Лондон", "Канада", "Естония");

    });
    ops.AddPolicy("OnlyForMicrosoft", policy =>
    {
        policy.RequireClaim("company", "Microsoft");
    });
});
builder.Services.AddControllersWithViews();




var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//builder.Services.AddSession(options => {
//    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
//});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();    // аутентификация
app.UseAuthorization();     // авторизация


 app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
