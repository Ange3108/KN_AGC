using KN_Web.EntityFramework;
using KN_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    public class HomeController : Controller
    {
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
                var result = context.tUsuario.Where(p => p.CorreoElectronico == modelo.CorreoElectronico
                                                       && p.Contrasena == modelo.Contrasena
                                                       && p.Estado).FirstOrDefault();
                                                        //el firstordefault solo devuelve un resultado, si no encuentra nada devuelve null

                var result = context.IniciarSesion(modelo.Identificacion, modelo.Contrasena).FirstOrDefault();

                if (result == null)
                {
                    ViewBag.Mensaje = "Usuario o contraseña incorrecta.";
                    return View();
                }

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
                var tabla = new tUsuario
                {

                    Identificacion = model.Identificacion,
                    Nombre = model.Nombre,
                    Contrasena = model.Contrasena

                    //crear una instancia de la tabla tUsuario


                };
                context.tUsuario.Add(tabla); //agregar el objeto tabla a la tabla tUsuario
                context.SaveChanges(); //guardar los cambios en la base de datos

            }

                return View();
            
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
                return View();
            }

            #endregion

        
    }
}