using System;
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
        public ActionResult Index()
        {
            List<MostrarTresPropiedades_Result> modeloVista = new List<MostrarTresPropiedades_Result>();
            modeloVista = this.Model.MostrarTresPropiedades().ToList();
            return View(modeloVista);
        }
        #endregion

        public ActionResult Propiedades(int? pageSize, int? page,int? precio02, int? CanCuato,int? CanBano,int ? CanGara,int? Cate)
        {
            CargarCategoria();

            List<MostrarPropiedad_Result> lista = new List<MostrarPropiedad_Result>();
            //lista = Model.MostrarPropiedad(precio02, CanCuato, CanBano, CanGara, Cate).ToList();
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);

            ViewBag.PageSize = pageSize;

            return View(lista.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Propiedad(int idPropiedad)
        {
            MostrarPropiedadID_Result modelovista = new MostrarPropiedadID_Result();
            modelovista = this.Model.MostrarPropiedadID(idPropiedad).FirstOrDefault();
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
    }
}