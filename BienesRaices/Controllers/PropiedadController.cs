﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;
using PagedList;


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
        public ActionResult Reportes()
        {
            return View();
        }
        public ActionResult Inicio(int? pageSize, int? page, int? precio02, int? CanCuato, int? CanBano, int? CanGara, int? Cate)
        {
            CargarCategoria();

            List<MostrarPropiedad_Result> lista = new List<MostrarPropiedad_Result>();
            lista = Model.MostrarPropiedad(precio02, CanCuato, CanBano, CanGara, Cate).ToList();
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;

            return View(lista.ToPagedList(page.Value, pageSize.Value));
        }
        public ActionResult ModificarPropiedad( int idpropiedad)
        {
            CargarCategoria();
            MostrarPropiedadID_Result modelovista = new MostrarPropiedadID_Result();
            modelovista = this.Model.MostrarPropiedadID(idpropiedad).FirstOrDefault();
            return View(modelovista);
        }
        [HttpPost]
        public ActionResult ModificarPropiedad(MostrarPropiedadID_Result modelovista, int idpropiedad)
        {
            CargarCategoria();
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.ModificarPropiedad(
                    modelovista.Id_Propiedad_P = idpropiedad,
                    modelovista.Nombre_P,
                    modelovista.Precio_P,
                    modelovista.Can_Cuarto_P,
                    modelovista.Can_Baños_P,
                    modelovista.Can_Garaje_P,
                    modelovista.Estado_P,
                     modelovista.Id_Categoria_P

                    );
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error: " + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    resultado += "Registro modificado";
                }
                else
                {
                    resultado += "No se pudo modificar";
                }

            }
            Response.Write("<script language=javascript>alert('" + resultado + "')</script>");

            return View(modelovista);


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