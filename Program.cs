using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyPasswords.Data;
using MyPasswords.Handlers;
using MyPasswords.Repositories;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Services.ObtainUserLogged;
using MyPasswords.Services.User.Login;
using MyPasswords.Services.User.Register;
using MyPasswords.Services.User.Update;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(Program));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, mySqlOptions =>
        mySqlOptions.EnableRetryOnFailure(maxRetryCount: 3)));


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterServices, RegisterService>();
builder.Services.AddScoped<IUpdateServices, UpdateServices>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();

builder.Services.AddScoped<ILoggedUser, LoggedUser>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
    });

var app = builder.Build();
app.UseExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
