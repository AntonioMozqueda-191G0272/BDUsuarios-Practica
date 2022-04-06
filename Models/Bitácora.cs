using System;
using System.Collections.Generic;

#nullable disable

namespace BDUsuarios_Practica.Models
{
    public partial class Bitácora
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
