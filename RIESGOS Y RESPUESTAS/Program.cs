using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;
using RIESGOS_Y_RESPUESTAS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();


// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a base de datos-------------------------------------------------------------------

builder.Services.AddDbContext<DbgestorContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("conexion"))
);

//-------------------------------------------------------------------------------------------

// Configurar los servicios de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/AccesoController1/Index"; // Redirige a la página de inicio de sesión si no está autenticado
        options.LogoutPath = "/AccesoController1/Logout"; // Redirige a esta página cuando se cierre sesión
        options.AccessDeniedPath = "/Home/AccessDenied"; // Redirige si el usuario no tiene acceso
    });

// Agrega soporte para controladores y vistas
builder.Services.AddControllersWithViews();
//-------------------------------------------------------------------------------------------

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
    pattern: "{controller=AccesoController1}/{action=Index}/{id?}");

app.Run();
