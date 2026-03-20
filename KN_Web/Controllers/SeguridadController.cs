using KN_Web.EntityFramework;
using KN_Web.Models;
using KN_Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    public class SeguridadController : Controller
    {
        readonly Generales generales = new Generales();

        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarAcceso(SeguridadModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            using (var context = new KN_DBEntities())
            {
                var consecutivoSesion = int.Parse(Session["Consecutivo"].ToString());
                var actualizacion = context.ActualizarContrasena(model.ContrasenaNueva, consecutivoSesion);

                if (actualizacion <= 0)
                {
                    ViewBag.Mensaje = "Su información no se actualizó correctamente.";
                    return View();
                }

                //Se envía un correo electrónico al usuario con la nueva contraseña
                string rutaHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "NotificarContrasenna.html");
                string contenidoHtml = System.IO.File.ReadAllText(rutaHtml);

                string htmlFinal = contenidoHtml
                    .Replace("{{NOMBRE_USUARIO}}", Session["Nombre"].ToString());

                generales.EnviarCorreo(Session["CorreoElectronico"].ToString(), "Notificación de Acceso", htmlFinal);

                return RedirectToAction("CerrarSesion", "Home");
            }
        }
    }

    }
}
