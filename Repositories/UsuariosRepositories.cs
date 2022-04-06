using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDUsuarios_Practica.Models;
using Microsoft.EntityFrameworkCore;

namespace BDUsuarios_Practica.Repositories
{
    public class UsuariosRepositories
    {
        usuariosContext context = new usuariosContext();
        Usuario u = new Usuario();
        public IEnumerable<Usuario> GetAll() 
        {
            return context.Usuarios.OrderBy(x => x.Nombre);
        }

        public Usuario GetByEMail(Usuario u)
        {
            return context.Usuarios.FirstOrDefault(x => x.EMail == u.EMail);
        }

        public void Registrar(Usuario u)
        {
            context.Database.ExecuteSqlRaw($"call sp_RegistrarUsuario" + $"('{u.EMail}','{u.Nombre}','{u.Direccion}','{u.Telefono}'," +
                $"'{u.Contraseña}')");
            
        }


        public void Update(Usuario u)
        {
            Usuario clonuser = context.Usuarios.FirstOrDefault(x => x.Id == u.Id);

            if (clonuser != null)
            {
                clonuser.Id = u.Id;
                clonuser.Nombre = u.Nombre;
                clonuser.Direccion = u.Direccion;
                clonuser.Telefono = u.Telefono;
                clonuser.Contraseña = u.Contraseña;

                context.SaveChanges();
            }
        }

        public bool Validate(Usuario u)
        {
            if (string.IsNullOrEmpty(u.EMail))
            {
                throw new ArgumentException("Escribe un Correo válido.");
            }
            if (string.IsNullOrEmpty(u.Nombre))
            {
                throw new ArgumentException("Escribe un Nombre válido.");
            }
            if (string.IsNullOrEmpty(u.Direccion))
            {
                throw new ArgumentException("Escribe una Dirección válida.");
            }
            if (string.IsNullOrEmpty(u.Telefono))
            {
                throw new ArgumentException("Escribe un Telefono válido.");
            }
            if (string.IsNullOrEmpty(u.Contraseña))
            {
                throw new ArgumentException("Escribe una Contraseña válida.");
            }
            return true;
        }
    }
}
