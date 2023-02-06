using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class InicioController : Controller
    {

        // GET: Inicio
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string user, string pass)
        {
            UsuarioCRUD servicio = new UsuarioCRUD();
            Usuario usuario = servicio.Login(user, pass);

            if (usuario.id>0)
            {
                FormsAuthentication.SetAuthCookie(user, false);

                Session["Usuario"] = usuario;

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}
