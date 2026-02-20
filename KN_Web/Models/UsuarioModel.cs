using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_Web.Models
{
    public class UsuarioModel
    {
        public int Consecutivo { get; set; }
        public string Identificacion { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public String  CorreoElectronico { get; set; }
    }
}