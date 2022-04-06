using System;
using System.Collections.Generic;

#nullable disable

namespace BDUsuarios_Practica.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Bitácoras = new HashSet<Bitácora>();
        }

        public int Id { get; set; }
        public string EMail { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<Bitácora> Bitácoras { get; set; }
    }
}
