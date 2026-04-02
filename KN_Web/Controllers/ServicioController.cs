using KN_Web.EntityFramework;
using KN_Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    [SesionActiva]
    public class ServicioController : Controller
    {
        
        [HttpGet]
        public ActionResult ConsultarServicios()
        {
            using (var context = new KN_DBEntities())
            {
                var result = context.tServicio.ToList();
                return View(result);
            }
        }

        [HttpGet]
        public ActionResult CambiarEstado(int q)
        {
            using (var context = new KN_DBEntities())
            {
                var result = context.tServicio.Where(p => p.Consecutivo == q).FirstOrDefault();

                if (result != null)
                {
                    result.Estado = result.Estado == 1 ? 0 : 1;
                    context.SaveChanges();
                }

                return RedirectToAction("ConsultarServicios", "Servicio");
            }
        }

        #region agregar servicios
        [HttpGet]
        public ActionResult AgregarServicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarServicio(tServicio modelo)
        {
            using (var context = new KN_DBEntities())
            {
                modelo.Estado = 1;
                modelo.Video = "https://www.youtube.com/embed/" + modelo.Video;
                context.tServicio.Add(modelo);
                var result = context.SaveChanges();

                if (result <= 0)
                {
                    ViewBag.Mensaje = "La información no se registró correctamente.";
                    return View();
                }

                return RedirectToAction("ConsultarServicios", "Servicio");
            }
        }
        #endregion

        #region ActualizarServicio

        [HttpGet]
        public ActionResult ActualizarServicio(int q)
        {
            using (var context = new KN_DBEntities())
            {
                var result = context.tServicio.Where(p => p.Consecutivo == q).FirstOrDefault();
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult ActualizarServicio(tServicio modelo)
        {
            using (var context = new KN_DBEntities())
            {
                var datos = context.tServicio.Where(p => p.Consecutivo == modelo.Consecutivo).FirstOrDefault();
                datos.Nombre = modelo.Nombre;
                datos.Descripcion = modelo.Descripcion;
                datos.Precio = modelo.Precio;
                datos.Video = modelo.Video;

                var result = context.SaveChanges();

                if (result <= 0)
                {
                    ViewBag.Mensaje = "La información no se actualizó correctamente.";
                    return View();
                }

                return RedirectToAction("ConsultarServicios", "Servicio");
            }
        }

        #endregion


    }
}