﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;
using PagedList;


namespace BienesRaices.Controllers
{

    public class IndexController : Controller
    {
        db_a3cb5b_webbienesraicesEntities Model = new db_a3cb5b_webbienesraicesEntities();
        // GET: Index


        #region Mostrar tres propiedades en el index
        /// <summary>
        /// Método que muestra los 3 últiimas propiedades para mostrar en el index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? Id_Categoria_P)
        {
            //List<MostrarTresPropiedades_Result> modeloVista = new List<MostrarTresPropiedades_Result>();
            //modeloVista = this.Model.MostrarTresPropiedades(Id_Categoria_P).ToList();

            MostrarTresCasas(1);
            MostrarTresAlquiler(2);
            MostrarTresTerrenos(3);
            CargarCategoria();
            return View();
        }

        void MostrarTresCasas(int Id_Categoria_P)
        {
            this.ViewBag.MostrarCasas = this.Model.MostrarTresPropiedades(Id_Categoria_P).ToList();
        }

        void MostrarTresAlquiler(int Id_Categoria_P)
        {
            this.ViewBag.MostrarAlquiler = this.Model.MostrarTresPropiedades(Id_Categoria_P).ToList();
        }

        void MostrarTresTerrenos(int Id_Categoria_P)
        {
            this.ViewBag.MostrarTerrenos = this.Model.MostrarTresPropiedades(Id_Categoria_P).ToList();
        }
        #endregion

        public ActionResult Propiedades(int? precio0, int? precio01, int? Precio02, int? Precio03, int? CanCuato,int? CanBano,int ? CanGara,int? Cate,int? Id_Provincia_L, int? Id_Canton_L, int? Id_Distrito_L,string tipo)
        {
            CargarCategoria();

            List<MostrarPropiedad_Result> lista = new List<MostrarPropiedad_Result>();
            if (tipo == "¢")
            {
                if (Precio02 > Precio03)
                {
                    var resultado = "Debes agregar un precio final mayor al precio inicial.";
                    Response.Write("<script language=javascript>alert('" + resultado + "')</script>");
                    List<MostrarPropiedad_Result> listaS = new List<MostrarPropiedad_Result>();
                    precio0 = 0;
                    precio01 = 0;
                    Precio02 = 0;
                    Precio03 = 0;
                    tipo = "";
                    listaS = Model.MostrarPropiedad(Precio02, Precio03, CanCuato, CanBano, CanGara, Cate, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();
                    return View(listaS);
                }
                lista = Model.MostrarPropiedad(Precio02, Precio03, CanCuato, CanBano, CanGara, Cate, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();
                return View(lista);
            }
            else
            {
                if (precio0 > precio01)
                {
                    var resultado = "Debes agregar un precio final mayor al precio inicial.";
                    Response.Write("<script language=javascript>alert('" + resultado + "')</script>");
                    List<MostrarPropiedad_Result> listaS = new List<MostrarPropiedad_Result>();
                    precio0 = 0;
                    precio01 = 0;
                    Precio02 = 0;
                    Precio03 = 0;
                    tipo = "";
                    listaS = Model.MostrarPropiedad(Precio02, Precio03, CanCuato, CanBano, CanGara, Cate, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();

                    return View(listaS);
                }
                lista = Model.MostrarPropiedad(Precio02, Precio03, CanCuato, CanBano, CanGara, Cate, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();
                return View(lista);
            }


           
        }

        public ActionResult Propiedad(int idPropiedad)
        {
            MostrarPropiedadID_Result modelovista = new MostrarPropiedadID_Result();
            modelovista = this.Model.MostrarPropiedadID(idPropiedad).FirstOrDefault();
            CargarImagenes(idPropiedad);
            return View(modelovista);
        }
        [HttpPost]
        public ActionResult Propiedad(MuestralosPedidos_Result modelovista, int idPropiedad)
        {
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.IngresaPedido(
                    modelovista.NombreCompleto_CO,
                    modelovista.Telefono_CO,
                    modelovista.Correo_CO,
                    modelovista.Id_Propiedad_CO= idPropiedad,
                    modelovista.Estado_CO="Pedido",
                    modelovista.Comentario_CO
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
                    resultado += "Solicitud realizada";
                }
                else
                {
                    resultado += "No se pudo solicitar";
                }

            }
            Response.Write("<script language=javascript>alert('" + resultado + "')</script>");

            return RedirectToAction("Propiedades","Index");

        }
        void CargarCategoria()
        {
            this.ViewBag.CargarCategoria =
                this.Model.MostrarCategoria().ToList();
        }
        void CargarCaracteristica()
        {
            this.ViewBag.CargarCaracteristica =
                this.Model.MuestraCaracteristicas().ToList();
        }

        void CargarImagenes(int idPropiedad)
        {
            this.ViewBag.CargarImagenes = this.Model.MostrarCarruoselPropiedad(idPropiedad).ToList();
        }
    }
}