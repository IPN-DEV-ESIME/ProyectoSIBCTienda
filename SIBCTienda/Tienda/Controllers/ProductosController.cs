using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            ProdutoCRUD produtoCRUD = new ProdutoCRUD();
            return View(produtoCRUD.Listar());
        }

        // GET: Productos/Detalles/id
        public ActionResult Detalles(int id)
        {
            ProdutoCRUD s = new ProdutoCRUD();
            return View(s.Buscar(id, out bool bandera, out string msg));
        }

        // GET: Productos/Formulario
        public ActionResult Formulario(string id)
        {
            ProdutoCRUD s = new ProdutoCRUD();
            CategoriaCRUD sc= new CategoriaCRUD();
            ProveedorCRUD sp = new ProveedorCRUD();
            return View(s.Buscar(Convert.ToInt32(id), out bool bandera, out string msg));
        }

        // POST: Productos/Formulario
        [HttpPost]
        public ActionResult Formulario(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProdutoCRUD servicio = new ProdutoCRUD();
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(collection["Id"]);
                    producto.Nombre = collection["Nombre"];
                    producto.CategoriaId = Convert.ToInt32(collection["CategoriaId"]);
                    producto.ProveedorId = Convert.ToInt32(collection["ProveedorId"]);
                    producto.Descripcion = collection["Descripcion"];
                    producto.Sku = collection["Sku"];
                    producto.Cantidad = Convert.ToInt32(collection["Cantidad"]);
                    producto.Precio = float.Parse(collection["Precio"]);

                    if (collection["Id"] == null)
                    { //guarda
                        producto.FechaRegistro = DateTime.Now;
                    }

                    servicio.Guardar(producto, out bool ban, out string msg);

                    if (collection["Id"] != null)
                    { //redirecciona a detalles
                        return RedirectToAction("Detalles/" + producto.Id);
                    }

                    return RedirectToAction("Index");

                }

                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Eliminar/id
        public ActionResult Eliminar(int id)
        {
            try
            {
                ProdutoCRUD s = new ProdutoCRUD();
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
