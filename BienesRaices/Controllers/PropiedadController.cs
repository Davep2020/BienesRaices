using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;

namespace BienesRaices.Controllers
{
    public class PropiedadController : Controller
    {
        
        db_a3cb5b_webbienesraicesEntities Model = new db_a3cb5b_webbienesraicesEntities();

        public ActionResult Lista()
        {
            bool sesionIniciada = false;

            if (this.Session["logueado"] != null)
            {
                sesionIniciada = Convert.ToBoolean(Session["logueado"]);
            }


           if(sesionIniciada==true)
           {
                RetornaUsuarioCorreoPwd_Result modelo = (RetornaUsuarioCorreoPwd_Result)this.Session["datosUsuario"];
                return View();
           }
           else
           {
                return RedirectToAction("Login","Login");
           }

        }

        public ActionResult Propiedades()
        {


            return View();
        }
    }
}