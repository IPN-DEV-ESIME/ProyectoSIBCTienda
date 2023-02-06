using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            CategoriaCRUD categoriaCRUD = new CategoriaCRUD();

            return View(categoriaCRUD.Listar());
        }

        // GET: Categorias/Detalles/id
        public ActionResult Detalles(int id)
        {
            CategoriaCRUD categoriaCRUD = new CategoriaCRUD();
            //devolver el objeto buscado en caso de existir
            return View(categoriaCRUD.Buscar(Convert.ToInt32(id), out bool bandera, out string msg));
        }

        // GET: Categorias/Formulario
        public ActionResult Formulario(string id)
        {
            CategoriaCRUD categoriaCRUD = new CategoriaCRUD();
            //devolver el objeto buscado en caso de existir
            return View(categoriaCRUD.Buscar(Convert.ToInt32(id), out bool bandera, out string msg));
        }

        // POST: Categorias/Formulario
        [HttpPost]
        public ActionResult Formulario(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoriaCRUD servicio = new CategoriaCRUD();
                    Categoria categoria = new Categoria();
                    categoria.Id = Convert.ToInt32(collection["id"]);
                    categoria.Descripcion = collection["descripcion"];

                    servicio.Guardar(categoria, out bool ban, out string msg);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Categorias/Delete/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                CategoriaCRUD s = new CategoriaCRUD();
                s.Eliminar(id, out bool bandera, out string msg);
                return RedirectToAction("Index");
            }
            catch
            {
                //ocurrio un error regresa a los detalles
                return RedirectToAction("Detalles/" + id);
            }
        }


    }
}
