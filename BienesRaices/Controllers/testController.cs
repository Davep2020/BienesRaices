using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BienesRaices.Models;

namespace BienesRaices.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                db_a3cb5b_webbienesraicesEntities db = new db_a3cb5b_webbienesraicesEntities();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalPath);
                Imagen_I student = new Imagen_I();
                student.Ruta_I = ImageName;
                student.Id_Propiedad_I = 4;
                db.Imagen_I.Add(student);
                db.SaveChanges();

            }
            return RedirectToAction("/Test/DisplayImage");
        }
        public ActionResult DisplayImage()
        {
            return View();
        }
    }

}