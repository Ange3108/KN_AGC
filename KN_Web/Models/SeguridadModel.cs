using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace KN_Web.Models
{
    public class SeguridadModel
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string ContrasenaNueva { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [Compare("ContrasenaNueva", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasena { get; set; }
    }
}