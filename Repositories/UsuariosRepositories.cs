using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BDUsuarios_Practica.Models;
using Microsoft.EntityFrameworkCore;

namespace BDUsuarios_Practica.Repositories
{
    public class UsuariosRepositories
    {
        usuariosContext context = new usuariosContext();
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
        public static String GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string hash = s.ToString();
            return hash;
        }

        public void Update(Usuario u)
        {
            if (string.IsNullOrEmpty(u.Nombre))
            {
                throw new ArgumentException("Por favor seleccione un registro para editarlo");
            }
            else
            {
                context.Database.ExecuteSqlRaw($"call sp_Modificacion('{u.Id}','{u.EMail}','{u.Nombre}','{u.Direccion}','{u.Telefono}','{u.Contraseña}')");
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
