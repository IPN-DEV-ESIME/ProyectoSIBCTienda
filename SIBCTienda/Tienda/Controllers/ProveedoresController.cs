using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    [Authorize]
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public ActionResult Index()
        {
            ProveedorCRUD s = new ProveedorCRUD();
            List<Proveedor> proveedores = s.Listar();

            return View(proveedores);
        }

        // GET: Proveedores/Detalles/id
        public ActionResult Detalles(int id)
        {
            ProveedorCRUD s = new ProveedorCRUD();
            Proveedor proveedor = s.Buscar(id, out bool bandera, out string msg);

            return View(proveedor);
        }

        // GET: Proveedores/Formulario/id?
        public ActionResult Formulario(string id)
        {
            ProveedorCRUD s = new ProveedorCRUD();
            Proveedor proveedor = s.Buscar(Convert.ToInt32(id), out bool bandera, out string msg);

            return View(proveedor);
        }

        // POST: Proveedores/Formulario
        [HttpPost]
        public ActionResult Formulario(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ProveedorCRUD servicio = new ProveedorCRUD();
                    Proveedor proveedor = new Proveedor();
                    proveedor.Id = Convert.ToInt32(collection["id"]);
                    proveedor.Rfc = collection["rfc"];
                    proveedor.Nombre = collection["nombre"];
                    proveedor.PaginaWeb = collection["paginaWeb"];


                    Contacto contacto = new Contacto();
                    //contacto.id = Convert.ToInt32(collection["contacto.id"]);
                    contacto.telefono1 = collection["contacto.telefono1"];
                    contacto.telefono2 = collection["contacto.telefono2"];
                    contacto.correo = collection["contacto.correo"];

                    proveedor.contacto = contacto;

                    Direccion direccion = new Direccion();
                    //direccion.id = Convert.ToInt32(collection["direccion.id"]);
                    direccion.entidadFederativa = collection["direccion.entidadFederativa"];
                    direccion.codigoPostal = collection["direccion.codigoPostal"];
                    direccion.colonia = collection["direccion.colonia"];
                    direccion.calle = collection["direccion.calle"];
                    direccion.noInterior = collection["direccion.noInterior"];
                    direccion.noExterior = collection["direccion.noExterior"];
                    direccion.descripcion = collection["direccion.descripcion"];

                    proveedor.direccion = direccion;

                    if (proveedor.Id > 0)
                    {
                        //actualizar
                        contacto.entidad = proveedor.Id;
                        direccion.entidad = proveedor.Id;
                        servicio.Actualizar(proveedor, out bool bandera1, out string msg1);
                        return RedirectToAction("Detalles/" + proveedor.Id);
                    }

                    else
                    {
                        servicio.Guardar(proveedor, out bool bandera, out string msg);
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedores/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                ProveedorCRUD s = new ProveedorCRUD();
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
