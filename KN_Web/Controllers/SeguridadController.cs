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

        #region CambiarAcceso

        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            return View();
        }

        [HttpPost]
        //es un formulario para cambiar la contraseña
        //valida que el modelo sea correcto, es decir, que las contraseñas coincidan y cumplan con los requisitos de seguridad
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

                //hace reemplazos en el contenido del html, en este caso, el nombre del usuario, para personalizar el correo electrónico
                string htmlFinal = contenidoHtml
                    .Replace("{{NOMBRE_USUARIO}}", Session["Nombre"].ToString());

                generales.EnviarCorreo(Session["CorreoElectronico"].ToString(), "Notificación de Acceso", htmlFinal);

                return RedirectToAction("CerrarSesion", "Home");
            }
        }

        #endregion

        #region CambiarPerfil

        //trae los datos del usuario de la session
        [HttpGet]
        public ActionResult CambiarPerfil()
        {
            using (var context = new KN_DBEntities())
            {
                var consecutivo = int.Parse(Session["Consecutivo"].ToString());
                var result = context.tUsuario.Where(p => p.Consecutivo == consecutivo).FirstOrDefault();

                var dto = new PerfilModel
                {
                    Identificacion = result.Identificacion,
                    Nombre = result.Nombre,
                    CorreoElectronico = result.CorreoElectronico
                };

                return View(dto);
            }
        }

        [HttpPost]
        //HttpPostedFileBase le pasa la imagen al controlador, el nombre debe coincidir con el input del formulario en la vista, en este caso "ImagenUsuario"
        public ActionResult CambiarPerfil(PerfilModel model, HttpPostedFileBase ImagenUsuario)
        {


            using (var context = new KN_DBEntities())
            {
                var consecutivo = int.Parse(Session["Consecutivo"].ToString());
                var result = context.tUsuario.Where(p => p.Consecutivo == consecutivo).FirstOrDefault();

                if (result != null)
                {
                    //esto es como el dto, se asignan los valores del modelo a la entidad que se va a actualizar en la base de datos
                    result.Identificacion = model.Identificacion;
                    result.Nombre = model.Nombre;
                    result.CorreoElectronico = model.CorreoElectronico;

                    if (ImagenUsuario != null && ImagenUsuario.ContentLength > 0)
                    {
                        //Aquí se guarda la imagen en una carpeta llamada "Uploads" dentro del proyecto, y se guarda la ruta relativa en la base de datos
                        //la extension de la imagen
                        string extension = Path.GetExtension(ImagenUsuario.FileName);
                        //asigna el consecutivo a la imagen y toma el consecutivo como nombre del archivo
                        string fileName = consecutivo + extension;
                        //
                        string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
                        string path = Path.Combine(folder, fileName);

                        //manda a guardar la imagen en la carpeta, si no existe la carpeta, la crea
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        //guarda la imagen en la carpeta
                        ImagenUsuario.SaveAs(path);
                        result.ImagenUsuario = "/Uploads/" + fileName;
                    }

                    context.SaveChanges();
                }
                //Actualizamos la información de la sesión(las variables de session) con los nuevos datos del perfil


                Session["Nombre"] = model.Nombre;
                Session["CorreoElectronico"] = model.CorreoElectronico;

                if (ImagenUsuario != null && ImagenUsuario.ContentLength > 0)
                    Session["ImagenUsuario"] = result.ImagenUsuario;

                return RedirectToAction("Index", "Home");
            }

        #endregion
        }
    }
}
