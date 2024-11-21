using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;

namespace RIESGOS_Y_RESPUESTAS.Data
{
    public class DA_Usuario
    {
        private readonly DbgestorContext _context;

        // Constructor que recibe el DbContext para poder interactuar con la base de datos
        public DA_Usuario(DbgestorContext context)
        {
            _context = context;
        }

        // Método para validar el usuario usando la base de datos
        public async Task<Usuario> ValidarUsuario(string correo, string clave)
        {
            // Buscar el usuario en la base de datos utilizando LINQ y Entity Framework
            var usuario = await _context.Usuarios
                .Where(u => u.Correo == correo && u.Contraseña == clave)
                .FirstOrDefaultAsync();

            return usuario;
        }
    }
}
