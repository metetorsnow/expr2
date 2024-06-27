using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using expr2.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizeFolder("/users", "admin");
    options.Conventions.AuthorizeFolder("/Stocks", "admin");
    options.Conventions.AuthorizePage("/Items/Index", "UserAndAdmin");
    options.Conventions.AuthorizePage("/Items/Create", "admin");
    options.Conventions.AuthorizePage("/Items/Edit", "admin");
    options.Conventions.AuthorizePage("/Items/Delete", "admin");
    options.Conventions.AuthorizePage("/Items/Details", "admin");
	options.Conventions.AuthorizePage("/UsageRequests/Index", "admin");
	options.Conventions.AuthorizePage("/UsageRequests/Create", "UserAndAdmin");
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

        options.LoginPath = "/login";
        options.AccessDeniedPath = "/Index";
        options.SlidingExpiration = true;
    });
builder.Services.AddDbContext<ExprContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("expr2Context") ?? throw new InvalidOperationException("Connection string 'expr2Context' not found.")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
          policy.RequireRole("admin"));
    options.AddPolicy("user", policy =>
          policy.RequireRole("user"));
	options.AddPolicy("UserAndAdmin", policy =>
		  policy.RequireRole("admin", "user"));
});
/*builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "login";
    options.AccessDeniedPath = "login";
    options.SlidingExpiration = true;
});*/

builder.Services.AddHttpContextAccessor();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapDefaultControllerRoute();


app.Run();
