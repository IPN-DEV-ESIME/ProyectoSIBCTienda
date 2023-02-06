using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Tienda.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acerca()
        {
            ViewBag.Message = "Nuestra info ";

            return View();
        }

        public ActionResult Salir()
        {
            //metodo para salir de la sesion
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;


            return RedirectToAction("Index", "Inicio");
        }

    }
}