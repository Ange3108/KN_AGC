using KN_Web.EntityFramework;
using KN_Web.Models;
using System.Linq;
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
            using (var context = new KN_DBEntities1())
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
                Session["Nombre"] = result.Nombre;
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
            using (var context = new KN_DBEntities1())
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
            return View();
        }

        #endregion


    }
}