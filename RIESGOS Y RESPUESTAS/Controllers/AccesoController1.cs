using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RIESGOS_Y_RESPUESTAS.Models;
using System.Security.Claims;
using RIESGOS_Y_RESPUESTAS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class AccesoController1 : Controller
    {
        private readonly DbgestorContext _context;

        // Constructor con DbgestorContext
        public AccesoController1(DbgestorContext context)
        {
            _context = context;
        }

        // Acción GET para mostrar la vista de inicio de sesión
        public IActionResult Index()
        {
            return View();
        }

        // Acción POST para procesar el inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            if (string.IsNullOrEmpty(_usuario.Correo) || string.IsNullOrEmpty(_usuario.Contraseña))
            {
                // Si no se proporciona correo o contraseña, se muestra un error.
                ViewBag.Error = "Correo y contraseña son obligatorios.";
                return View();
            }

            // Buscar al usuario en la base de datos
            var usuario = await _context.Usuarios
                .Where(u => u.Correo == _usuario.Correo)
                .FirstOrDefaultAsync();

            // Si el usuario no existe o la contraseña es incorrecta
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(_usuario.Contraseña, usuario.Contraseña))
            {
                // Mostrar un mensaje de error si la validación falla
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            // Configuración de autenticación (inicio de sesión)
            #region AUTHENTICATION

            // Declarar e inicializar la lista de claims antes de agregar elementos
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Usuario1),
                new Claim("Correo", usuario.Correo),
            };

            // Agregar los roles del usuario si existen
            if (usuario.Roles != null && usuario.Roles.Any())
            {
                foreach (var rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.ToString())); // Convertir 'char' a 'string'
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Crear la cookie de autenticación
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            #endregion

            // Redirigir al usuario a la página principal (Home)
            return RedirectToAction("Index", "Home");
        }
    }
}

