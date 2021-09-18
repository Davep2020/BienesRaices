using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;

namespace BienesRaices.Controllers
{
    /// <summary>
    /// Controlador para el mantenimiento de las cuentas de los empleados.
    /// </summary>
    public class PerfilController : Controller
    {

        db_a3cb5b_webbienesraicesEntities Model = new db_a3cb5b_webbienesraicesEntities();

        #region Mostrar Cuentas

        //Vista que muestra todas las cuentas que hay registradas.
        public ActionResult Cuentas()
        {
            List<MuestraCuenta_Result> modeloVista = new List<MuestraCuenta_Result>();
            modeloVista = this.Model.MuestraCuenta().ToList();
            return View(modeloVista);
        }

        #endregion

        #region Ingresar Cuenta
        //Vista para ingresar las cuentas
        public ActionResult Ingresar()
        {
            return View();
        }

        /// <summary>
        /// Método que ingresa las cuentas en la BD
        /// </summary>
        /// <param name="modeloVista"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ingresar(MuestraCuenta_Result modeloVista)
        {

            int registros = 0;
            string mensaje = "";

            try
            {
                registros = this.Model.IngresaCuenta(
                        modeloVista.Usuario_CR,
                        modeloVista.Contrasena_CR
                    );
            }
            catch (Exception e)
            {
                mensaje = "Error al ingresar la cuenta, vuélvalo a intentar. " + e.Message;
            }
            finally
            {
                if (registros > 0)
                {
                    mensaje = "Cuenta ingresada con éxito.";
                }
                else
                {
                    mensaje = "Error al ingresar la cuenta, vuélvalo a intentar.";
                }
            }

            Response.Write("<script laguage=JavaScript>alert('" + mensaje + "');</script>");

            return View();
        }
        #endregion

        #region Modificar Cuenta

        //Vista para modificar una cuenta
        public ActionResult Modificar(int Id_Credenciales_CR)
        {
            RetornaCuentaID_Result modeloVista = new RetornaCuentaID_Result();
            modeloVista = this.Model.RetornaCuentaID(Id_Credenciales_CR).FirstOrDefault();
            return View(modeloVista);
        }

        //Método que modifica la cuenta en la BD
        [HttpPost]
        public ActionResult Modificar(RetornaCuentaID_Result modeloVista)
        {
            int registros = 0;
            string mensaje = "";

            try
            {
                registros = this.Model.ModicaCuenta(
                        modeloVista.Id_Credenciales_CR,
                        modeloVista.Contrasena_CR
                    );
            }
            catch (Exception e)
            {
                mensaje = "Error al modificar la cuenta, vuélvalo a intentar. " + e.Message;
            }
            finally
            {
                if (registros > 0)
                {
                    mensaje = "Cuenta modificada con éxito.";
                }
                else
                {
                    mensaje = "Error al modificar la cuenta, vuélvalo a intentar.";
                }
            }

            Response.Write("<script laguage=JavaScript>alert('" + mensaje + "');</script>");

            return RedirectToAction("Cuentas", "Perfil");
        }

        #endregion

        #region Cambiar Estado Cuenta

        [HttpPost]
        public ActionResult Estado(int Id_Credenciales_CR)
        {
            int Registros = 0;
            string resultado = "";

            try
            {
                Registros = this.Model.EstadoCuenta(Id_Credenciales_CR);
            }
            catch (Exception error)
            {

                resultado = "Ocurrió un error: " + error.Message;
            }
            finally
            {
                if (Registros > 0)
                {
                    resultado = "Estado de la cuenta ha sido modificado con éxito.";
                }
                else
                {
                    resultado = "No se pudo modificar el estado de la cuenta, intente de nuevo.";
                }
            }
            return RedirectToAction("Cuentas", "Perfil");
        }

        #endregion
    }
}