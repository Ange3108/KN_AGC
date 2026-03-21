using KN_Web.EntityFramework;
using KN_Web.Filters;
using KN_Web.Models;
using KN_Web.Services;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    public class HomeController : Controller
    {
        readonly Generales generales = new Generales();

        [SesionActiva]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Iniciar Sesión

        [HttpGet]
        // GET: es para abrir la vista, POST: es para enviar los datos del formulario
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioModel modelo)
        {
            using (var context = new KN_DBEntities())
            {

                //---------Linq-----------
                //var result = context.tUsuario.Where(p => p.CorreoElectronico == modelo.CorreoElectronico
                //      && p.Contrasena == modelo.Contrasena
                //       && p.Estado).FirstOrDefault();
                //el firstordefault solo devuelve un resultado, si no encuentra nada devuelve null



                //procedimiento almacenado
                //se enfocan en sistemas pequeños, para sistemas grandes se recomienda usar el enfoque sin procedimientos almacenados
                var result = context.IniciarSesion(modelo.CorreoElectronico, modelo.Contrasena).FirstOrDefault();

                if (result == null)
                {
                    ViewBag.Mensaje = "Su infromacion no se autentico";
                    return View();
                }
                Session["Consecutivo"] = result.Consecutivo;
                Session["Nombre"] = result.Nombre;
                Session["CorreoElectronico"] = result.CorreoElectronico;
                Session["ImagenUsuario"] = result.ImagenUsuario;
                return RedirectToAction("Index", "Home");
            }

        }

        #endregion

        #region Registrar Usuario

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(UsuarioModel model)
        {

            //delimitador de la base de datos, para abrir la conexión, realizar las operaciones y cerrar la conexión
            //context es el objeto que representa la conexión a la base de datos
            using (var context = new KN_DBEntities())
            {
                //----------Linq-----------
                //context.tUsuario.Add(new tUsuario
                //{
                //    Identificacion = model.Identificacion,
                //    Nombre = model.Nombre,
                //    Contrasena = model.Contrasena,
                //    CorreoElectronico = model.CorreoElectronico,
                //    Estado = true
                //});

                //context.tUsuario.Add(tabla); //agregar el objeto tabla a la tabla tUsuario
                //context.SaveChanges(); //guardar los cambios en la base de datos
                

                //procedimiento almacenado
                var result = context.RegistrarUsuario(model.Identificacion, model.Contrasena, model.Nombre, model.CorreoElectronico);

                if (result <= 0)
                {
                    ViewBag.Mensaje = "Su información no se registró correctamente";
                    return View();
                }

                return RedirectToAction("Login", "Home");
            }
           
        }

        #endregion

        #region Recuperar Contraseña

        [HttpGet]
        public ActionResult RecuperarContrasena()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasena(UsuarioModel modelo)
        {

            using (var context = new KN_DBEntities())
            {
                var result = context.ValidarCorreo(modelo.CorreoElectronico).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.Mensaje = "Su información no se validó correctamente";
                    return View();
                }

                //enviar correo con la nueva contraseña
                var nuevaContrasena = generales.GenerarContrasena();

                //Se actualiza la contraseña en la base de datos

                var actualizacion = context.ActualizarContrasena(nuevaContrasena, result.Consecutivo);

               if (actualizacion <= 0)
                {
                    ViewBag.Mensaje = "Su información no se actualizó correctamente";
                    return View();
                }

                //Se envía un correo electrónico al usuario con la nueva contraseña
                string rutaHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "RecuperarContrasenna.html");
                string contenidoHtml = System.IO.File.ReadAllText(rutaHtml);

                string htmlFinal = contenidoHtml
                    .Replace("{{NOMBRE_USUARIO}}", result.Nombre)
                    .Replace("{{NUEVA_CONTRASENA}}", nuevaContrasena);

                generales.EnviarCorreo(result.CorreoElectronico, "Recuperar Acceso", htmlFinal);
                return RedirectToAction("Login", "Home");

            }
           
        }

        #endregion

        #region Cerrar Sesión

        [SesionActiva]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        #endregion




      
    }
}