using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;

namespace BienesRaices.Controllers
{
    public class LoginController : Controller
    {
        db_a3cb5b_webbienesraicesEntities Model = new db_a3cb5b_webbienesraicesEntities();

        [HttpPost]
        public ActionResult VerificaLogin(RetornaCuenta_Result pModelo)
        {

            RetornaCuenta_Result usuarioBuscar = this.Model.RetornaCuenta(pModelo.Usuario_CR, pModelo.Contrasena_CR).FirstOrDefault();

            if (usuarioBuscar == null)
            {
                this.ModelState.AddModelError("", "Usuario o contraseña inválidos. Por favor verifique.");
                return View("Login");
            }
            else
            {
                this.Session.Add("logueado", true);
                this.Session.Add("datosUsuario", usuarioBuscar);

                //Falta agregar a la vista que va a ingresar
                return RedirectToAction("Index", "Propiedad");
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            this.Session.Add("logueado", null);

            this.Session.Add("datosUsuario", null);

            return RedirectToAction("Login", "Login");
        }

    }
}