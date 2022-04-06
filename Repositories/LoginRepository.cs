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
    public class LoginRepository
    {
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

        usuariosContext context = new usuariosContext();

        public void login(Usuario u)
        {
            context.Database.ExecuteSqlRaw("$call sp_Login" + $"('{u.EMail}','{u.Contraseña}'");
        }

        public bool ValidateLogin(Usuario u)
        {
            if (context.Usuarios.Any(x => x.EMail == u.EMail && x.Contraseña == GetMD5Hash(u.Contraseña)))
            {
                throw new ArgumentException("Usuario o contraseña incorrectos");
            }
            return true;
        }
    }
}
