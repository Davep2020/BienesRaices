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
                RetornaCuenta_Result modelo = (RetornaCuenta_Result)this.Session["datosUsuario"];
                return View();
           }
           else
           {
                return RedirectToAction("Login","Login");
           }

        }

        public ActionResult Propiedades()
        {
            CargarCategoria();

            return View();
        }
        [HttpPost]
        public ActionResult Propiedades(MostrarPropiedad_Result modelos)
        {
            int registros = 0;
            string mensaje = "";
            CargarCategoria();

            try
            {
                registros = Model.IngresaPropiedad(
                    modelos.Nombre_P,
                    modelos.Precio_P,
                    modelos.Can_Cuarto_P,
                    modelos.Can_Baños_P,
                    modelos.Can_Garaje_P,
                    modelos.Id_Categoria_P

                    );
            }
            catch (Exception e)
            {
                mensaje = "Ocurrió un error: " + e.Message;
            }
            finally
            {
                if (registros > 0)
                {

                    mensaje = "Se agregó una nueva Propiedad.";

                }
                else
                {
                    mensaje = "No se pudo insertar.";
                }
            }

            Response.Write("<script laguage=JavaScript>alert('" + mensaje + "');</script>");

            return View();
        }

        public ActionResult Pedidos()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        void CargarCategoria()
        {
            this.ViewBag.CargarCategoria =
                this.Model.MostrarCategoria().ToList();
        }
    }
}