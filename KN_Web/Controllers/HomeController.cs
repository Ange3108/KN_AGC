using KN_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_WEB.Controllers
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

            return View();
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
            return View();
        }

        #endregion

        #region Recuperar Contraseña

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioModel modelo)
        {
            return View();
        }

        #endregion

    }
}