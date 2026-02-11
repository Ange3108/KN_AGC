using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_Web.Models
{
    public class UsuarioModel
    {
        public int Identificacion { get; set; }
        public int Contrasena { get; set; }
        public string Nombre { get; set; }
    }
}