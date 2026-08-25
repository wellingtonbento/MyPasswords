using Microsoft.EntityFrameworkCore;
using MyPasswords.Data;
using MyPasswords.Repositories;
using MyPasswords.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, mySqlOptions =>
        mySqlOptions.EnableRetryOnFailure(maxRetryCount: 3)));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
