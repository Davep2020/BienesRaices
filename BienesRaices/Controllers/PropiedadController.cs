using System;
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

        #region Vista
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
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region AgregaCasa
        public ActionResult Propiedades()
        {
            CargarCategoria();
            CargarPrioridad();
            return View();
        }
        [HttpPost]
        public ActionResult Propiedades(MostrarPropiedad_Result modelos)
        {
            int registros = 0;
            int registrox = 0;
            string mensaje = "";
            CargarPrioridad();
            CargarCategoria();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (i == 0)
                {
                    HttpPostedFileBase carga = Request.Files[i];
                    string ImageName = System.IO.Path.GetFileName(carga.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + ImageName);
                    carga.SaveAs(physicalPath);

                    try
                    {
                        registros = Model.IngresaPropiedad(
                            modelos.Nombre_P,
                            modelos.Descripcion_P,
                            modelos.Precio,
                            modelos.NombreProvincia_L,
                            modelos.NombreCanton_L,
                            modelos.NombreDistrito_L,
                            modelos.Id_Provincia_L,
                            modelos.Id_Canton_L,
                            modelos.Id_Distrito_L,
                            modelos.DireccionExacta_L,
                            modelos.CanCuartos_C,
                            modelos.CanBanos_C,
                            modelos.CanGarage_C,
                            modelos.Id_Categoria_P,
                            carga.FileName,
                            modelos.MetrosCuadradosCasa_C,
                            modelos.MetrosCuadradosLote_C,
                            modelos.IdPrioridad_Pri,
                            modelos.TipoMoneda_P
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
                }
                else if (i > 0)
                {
                    HttpPostedFileBase carga = Request.Files[i];
                    string ImageName = System.IO.Path.GetFileName(carga.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + ImageName);
                    carga.SaveAs(physicalPath);

                    registrox = Model.CargarImagenes(
                            carga.FileName
                            );
                }
            }

            Response.Write("<script laguage=JavaScript>alert('" + mensaje + "');</script>");

            return View();
        }

        #endregion

        #region Pedidos
        public ActionResult Pedidos()
        {
            List<MuestralosPedidos_Result> lista = new List<MuestralosPedidos_Result>();
            lista = Model.MuestralosPedidos().ToList();

            return View(lista);
        }
        [HttpPost]
        public ActionResult PedidosVendidos(MuestralosPedidos_Result modelovista, int idContacto)
        {
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.FinalizarVenta(
                    modelovista.Id_Contacto_CO = idContacto
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

            return RedirectToAction("Pedidos", "Propiedad");


        }
      
        [HttpPost]
        public ActionResult PedidosRechazados(MuestralosPedidos_Result modelovista, int idContacto)
        {
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.RechazarVenta(
                    modelovista.Id_Contacto_CO = idContacto
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

            return RedirectToAction("Pedidos", "Propiedad");

        }

        #endregion

        #region Reportes
        public ActionResult Reportes()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReportesVentas()
        {
            List<MostrarReporte_Result> listaPersonas = Model.MostrarReporte().ToList();
            return Json(new
            {
                resultado = listaPersonas
            });
        }

        #endregion

        #region Inicio
        public ActionResult Inicio(int? precio0, int? precio01, int? precio02, int? precio03, int? CanCuato, int? CanBano, int? CanGara, int? Cate, string estado,int? Id_Provincia_L,int? Id_Canton_L, int? Id_Distrito_L, string tipo)
        {
            CargarCategoria();

 
            List<MostrarPropiedadAdmin_Result> lista = new List<MostrarPropiedadAdmin_Result>();
            if (tipo == "¢")
            {
                if (precio02 > precio03)
                {
                    var resultado = "Debes agregar un precio final mayor al precio inicial.";
                    Response.Write("<script language=javascript>alert('" + resultado + "')</script>");
                    List<MostrarPropiedadAdmin_Result> listaS = new List<MostrarPropiedadAdmin_Result>();
                    precio0 = 0;
                    precio01 = 0;
                    precio02 = 0;
                    precio03 = 0;
                    tipo = "";
                    listaS = Model.MostrarPropiedadAdmin(precio02, precio03, CanCuato, CanBano, CanGara, Cate, estado, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();

                    return View(listaS);
                }
                lista = Model.MostrarPropiedadAdmin(precio02, precio03, CanCuato, CanBano, CanGara, Cate, estado,Id_Provincia_L,Id_Canton_L,Id_Distrito_L, tipo).ToList();
            }
            else
            {
                if (precio0 > precio01)
                {
                    var resultado = "Debes agregar un precio final mayor al precio inicial.";
                    Response.Write("<script language=javascript>alert('" + resultado + "')</script>");
                    List<MostrarPropiedadAdmin_Result> listaS = new List<MostrarPropiedadAdmin_Result>();
                    precio0 = 0;
                    precio01= 0;
                    precio02 = 0;
                    precio03 = 0;
                    tipo = "";
                    listaS = Model.MostrarPropiedadAdmin(precio02, precio03, CanCuato, CanBano, CanGara, Cate, estado, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();

                    return View(listaS);
                }
                lista = Model.MostrarPropiedadAdmin(precio0, precio01, CanCuato, CanBano, CanGara, Cate, estado, Id_Provincia_L, Id_Canton_L, Id_Distrito_L, tipo).ToList();
            }
            return View(lista);
        }
        #endregion

        #region Ocultar Propiedad
        public ActionResult OcultarPropiedad(MostrarPropiedadID_Result modelovista, int idpropiedad, string Estado)
        {

            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.OcultarPropiedad(
                    modelovista.Id_Propiedad_P = idpropiedad,
                   modelovista.Estado_P = Estado

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
                    resultado += "Proceso Exitoso";
                }
                else
                {
                    resultado += "Proceso Exitoso";
                }

            }
            Response.Write("<script language=javascript>alert('" + resultado + "')</script>");

            return RedirectToAction("Inicio", "Propiedad");


        }
        #endregion

        #region ModificarPropiedad

        public ActionResult ModificarPropiedad(int idpropiedad)
        {
            CargarImagenes(idpropiedad);
            CargarCategoria();
            CargarPrioridad();
            MostrarPropiedadID_Result modelovista = new MostrarPropiedadID_Result();
            modelovista = this.Model.MostrarPropiedadID(idpropiedad).FirstOrDefault();
            return View(modelovista);
        }
        [HttpPost]
        public ActionResult ModificarPropiedad(MostrarPropiedadID_Result modelovista, int idpropiedad)
        {
            CargarImagenes(idpropiedad);
            CargarCategoria();
            CargarPrioridad();
            int cantRegistrosAfectados = 0;
            string resultado = "";
            if (modelovista.Estado_P==null)
            {
                modelovista.Estado_P = Model.Propiedad_P.Where(a => a.Id_Propiedad_P == idpropiedad).FirstOrDefault().Estado_P; ;
            }
            if (modelovista.Apartado == true)
            {
                modelovista.Estado_P = "Apartado";
            }else if (modelovista.Apartado==false)
            {
                string EstadoC = Model.Propiedad_P.Where(a => a.Id_Propiedad_P == idpropiedad).FirstOrDefault().Estado_P;
                if (EstadoC=="Apartado")
                {
                    modelovista.Estado_P = "Nuevo";
                }
           
            }
            try
            {
                cantRegistrosAfectados = this.Model.ModificarPropiedad(
                    modelovista.Id_Propiedad_P = idpropiedad,
                    modelovista.Nombre_P,
                    modelovista.Precio,
                    modelovista.Estado_P,
                    modelovista.Id_Categoria_P,
                    modelovista.Descripcion_P,
                    modelovista.NombreProvincia_L,
                    modelovista.NombreCanton_L,
                    modelovista.NombreDistrito_L,
                    modelovista.Id_Provincia_L,
                    modelovista.Id_Canton_L,
                    modelovista.Id_Distrito_L,
                    modelovista.DireccionExacta_L,
                    modelovista.CanCuartos_C,
                    modelovista.CanBanos_C,
                    modelovista.CanGarage_C,
                    modelovista.MetrosCuadradosCasa_C,
                    modelovista.MetrosCuadradosLote_C,
                    modelovista.IdPrioridad_Pri,
                    modelovista.TipoMoneda_P,
                    modelovista.PrecioA
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
                    if (modelovista.Id_Categoria_CA == 3)
                    {
                        modelovista.CanCuartos_C = null;
                        modelovista.CanBanos_C = null;
                        modelovista.CanGarage_C = null;
                        modelovista.MetrosCuadradosCasa_C = null;
                    }
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
        #endregion

        #region EliminarImagen
        [HttpPost]
        public ActionResult EliminarImagen(AgregarImagenes_Result modelovista, MostrarPropiedadID_Result modelovistas ,int Id_Imagen_I,int idpropiedad)
        {
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.EliminarImagenes(
                    modelovista.Id_Imagen_I = Id_Imagen_I
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

            return RedirectToAction("ModificarPropiedad", "Propiedad", new { idpropiedad });

        }
        #endregion

        #region AgregarImagenes
        [HttpPost]
        public ActionResult agregarImagen(MostrarPropiedad_Result modelovista , int? idpropiedad)
        {
            int registrox = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                
                    HttpPostedFileBase carga = Request.Files[i];
                    string ImageName = System.IO.Path.GetFileName(carga.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + ImageName);
                    carga.SaveAs(physicalPath);

                    registrox = Model.CargarImagenesModificar(
                            modelovista.Id_Propiedad_P= idpropiedad,
                            carga.FileName
                            );
                
            }
            return RedirectToAction("ModificarPropiedad", "Propiedad", new { idpropiedad });
        }
        #endregion

        #region venderpropiedad
        [HttpPost]
        public ActionResult venderPropiedad(MuestralosPedidos_Result modelovista, int idPropiedad)
        {
            int cantRegistrosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistrosAfectados = this.Model.FinalizarVentaEmpresa(
                    modelovista.Id_Propiedad_CO = idPropiedad,
                    modelovista.NombreCompleto_CO,
                    modelovista.Telefono_CO,
                    modelovista.Correo_CO,
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
                    resultado += "Venta realizada";
                }
                else
                {
                    resultado += "No se pudo Vender";
                }

            }
            Response.Write("<script language=javascript>alert('" + resultado + "')</script>");

            return RedirectToAction("Inicio", "Propiedad");

        }
        #endregion

        #region Metodos
        void CargarCategoria()
        {
            this.ViewBag.CargarCategoria =
                this.Model.MostrarCategoria().ToList();
        }
        void CargarPrioridad()
        {
            this.ViewBag.CargarPrioridad =
                this.Model.MuestraPrioridad().ToList();
        }

        void CargarImagenes(int idpropiedad)
        {
            this.ViewBag.CargarImagenes =
                this.Model.AgregarImagenes(idpropiedad).ToList();
        }
        #endregion
    }

}
