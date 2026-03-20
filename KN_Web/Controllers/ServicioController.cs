using KN_Web.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_Web.Controllers
{
    public class ServicioController
    {
        [HttpGet]
        public ActionResult ConsultarServicios()
        {
            using (var context = new KN_DBEntities())
            {
                var result = context.tServicio.Where(p => p.Estado == 1).ToList();
                return View (result);
            }
        }
    }
}