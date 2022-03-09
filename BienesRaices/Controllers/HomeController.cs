using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;
using PagedList;
using System.Net.Mail;
using Microsoft.Exchange.WebServices.Data;


namespace BienesRaices.Controllers
{

    public class HomeController : Controller
    {
        db_a3cb5b_webbienesraicesEntities Model = new db_a3cb5b_webbienesraicesEntities();
 


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

                Session["Correo"] = modelovista.Correo_CO;
                Session["Nombre"] = modelovista.NombreCompleto_CO;

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
                    EnviarCorreoUsuario(idPropiedad, modelovista.NombreCompleto_CO, modelovista.Correo_CO);
                    EnviarCorreoAdmi(idPropiedad, 
                        modelovista.NombreCompleto_CO, 
                        modelovista.Correo_CO,
                        modelovista.Telefono_CO,
                        modelovista.Comentario_CO);
                }
                else
                {
                    resultado += "No se pudo solicitar";
                }

            }
            Response.Write("<script language=javascript>alert('" + resultado + "')</script>");

            return RedirectToAction("Propiedades","Home");

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

        #region Correos
        void EnviarCorreoUsuario(int pIdPropiedad, string pNombre, string pCorreo)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            Microsoft.Exchange.WebServices.Data.ExchangeService service = new Microsoft.Exchange.WebServices.Data.ExchangeService(Microsoft.Exchange.WebServices.Data.ExchangeVersion.Exchange2010_SP2);
            service.Credentials = new System.Net.NetworkCredential("info@bienesraicessarafcys.com", "puriscal2022");
            service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
            Microsoft.Exchange.WebServices.Data.EmailMessage emailMessage = new Microsoft.Exchange.WebServices.Data.EmailMessage(service);
            emailMessage.Subject = "Información de la propiedad.";
            emailMessage.From = new 
                Microsoft.Exchange.WebServices.Data.EmailAddress("info@bienesraicessarafcys.com");

            string Propiedad = Model.MuestralosPedidos().Where(s => s.Id_Propiedad_P == pIdPropiedad).FirstOrDefault().Nombre_P;
            #region Cuerpo HTML
            string cuerpoHTML = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <title>Correo</title>
                <style type='text/css'>
                <!--
                p.MsoNormal {
                	margin: 0cm;
                	margin-bottom: .0001pt;
                	font-size: 9.0pt;
                	font-family: 'Calibri', 'sans-serif';
                }
                -->
                </style>
                </head>
                <body>
                <div style='font-family:Arial, Helvetica, sans-serif'>
                  <p> <strong>Solicitud de información: </strong></p>
                  <p>A nombre de: " + pNombre + @"</p>
                  <p>Estimado cliente, " + pNombre + @", su solicitud por la propiedad " + Propiedad + @" está siendo procesada, en breve un asesor se pondrá en contanto con usted.</p>
                  <p>Gracias por preferirnos</p>
                  <p style='font-size:9pt'>Por favor no responda este correo, este mensaje es generado por un sistema automático.</p>
                 <p class='MsoNormal' style='text-align:center; color:green;font-size:9.0pt; '>'♻️<i> Cuidemos el Medio Ambiente, por favor, no imprima este correo electrónico si no es necesario.</i> ♻️'</p>
                
                </div>
                <hr />
                </body>
                </html>";
            #endregion
            string mensaje = "";


            emailMessage.Body = new 
                Microsoft.Exchange.WebServices.Data.MessageBody(Microsoft.Exchange.WebServices.Data.BodyType.HTML, cuerpoHTML);

            var lsMails = pCorreo.Replace(",", ";").Split(';');
            foreach (var mail in lsMails)
            {
                try
                {
                    emailMessage.ToRecipients.Add(pCorreo);
                }
                catch (Exception e)
                {
                    mensaje += $"Ocurrió un error:{e.Message}";
                }
                finally
                {
                    Response.Write("<script>alert('Solicitud enviada con éxito.')</script>");
                }
            }
            emailMessage.Send();
        }

        void EnviarCorreoAdmi(int pIdPropiedad, string pNombre, string pCorreo, string pTelefono, string pComentario)
        {
            string correoAdmi = "gerencia@bienesraicessarafcys.com";
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            Microsoft.Exchange.WebServices.Data.ExchangeService service = new Microsoft.Exchange.WebServices.Data.ExchangeService(Microsoft.Exchange.WebServices.Data.ExchangeVersion.Exchange2010_SP2);
            service.Credentials = new System.Net.NetworkCredential("info@bienesraicessarafcys.com", "puriscal2022");
            service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
            Microsoft.Exchange.WebServices.Data.EmailMessage emailMessage = new Microsoft.Exchange.WebServices.Data.EmailMessage(service);
            emailMessage.Subject = "Nueva solicitud de propiedad.";
            emailMessage.From = new
                Microsoft.Exchange.WebServices.Data.EmailAddress("info@bienesraicessarafcys.com");

            string Propiedad = Model.MuestralosPedidos().Where(s => s.Id_Propiedad_P == pIdPropiedad).FirstOrDefault().Nombre_P;
            #region Cuerpo HTML
            string cuerpoHTML = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <title>Correo</title>
                <style type='text/css'>
                <!--
                p.MsoNormal {
                	margin: 0cm;
                	margin-bottom: .0001pt;
                	font-size: 9.0pt;
                	font-family: 'Calibri', 'sans-serif';
                }
                -->
                </style>
                </head>
                <body>
                <div style='font-family:Arial, Helvetica, sans-serif'>
                  <p> <strongNueva solicitud de información: </strong></p>
                  <p>A nombre de: " + pNombre + @"</p>
                  <p>Correo: " + pCorreo + @"</p>
                  <p>Solicitud por la propiedad: " + Propiedad + @".</p>
                  <p>Télefono: " + pTelefono + @".</p>
                  <p>Comentario: " + pComentario + @".</p>
                </div>
                <hr />
                </body>
                </html>";
            #endregion
            string mensaje = "";


            emailMessage.Body = new
                Microsoft.Exchange.WebServices.Data.MessageBody(Microsoft.Exchange.WebServices.Data.BodyType.HTML, cuerpoHTML);

                try
                {
                    emailMessage.ToRecipients.Add(correoAdmi);
                }
                catch (Exception e)
                {
                    mensaje += $"Ocurrió un error:{e.Message}";
                }

            emailMessage.Send();
        }
        #endregion
    }
}