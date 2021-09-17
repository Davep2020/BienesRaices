using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BienesRaices.Controllers
{
    /// <summary>
    /// Controlador para el mantenimiento de las cuentas de los empleados.
    /// </summary>
    public class PerfilController : Controller
    {
      
        public ActionResult Cuentas()
        {
            return View();
        }
    }
}