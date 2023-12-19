using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeriesBoxd.Data;
using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Http.Metadata;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SerieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SerieContext") ?? throw new InvalidOperationException("Connection string 'SerieContext' not found.")));

builder.Services.AddScoped<ISerieService, SerieService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IActorService, ActorService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//Auth
builder.Services.AddDbContext<SerieContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<SerieContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
