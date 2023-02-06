using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index(string txtBusqueda)
        {
            UsuarioCRUD s = new UsuarioCRUD();
            List<Usuario> usuarios = s.Listar();

            if (!String.IsNullOrEmpty(txtBusqueda))
                return View(usuarios.Where(u => u.nombre.ToLower().Contains(txtBusqueda.ToLower())));

            return View(usuarios);

        }

        // GET: Usuarios/Detalles/id
        public ActionResult Detalles(int id)
        {
            UsuarioCRUD s = new UsuarioCRUD();
            Usuario usuario = s.Buscar(id, out bool bandera, out string msg);

            return View(usuario);
        }

        // GET: Usuarios/Formulario
        public ActionResult Formulario(string id)
        {
            UsuarioCRUD s = new UsuarioCRUD();
            Usuario usuario = s.Buscar(Convert.ToInt32(id), out bool bandera, out string msg);

            return View(usuario);
        }

        // POST: Usuarios/Formulario
        [HttpPost]
        public ActionResult Formulario(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioCRUD servicioUsuario = new UsuarioCRUD();
                    Usuario usuario = new Usuario();
                    usuario.id = Convert.ToInt32(collection["id"]);
                    usuario.rfc = collection["rfc"];
                    usuario.username = collection["username"];
                    usuario.password = collection["password"];
                    usuario.nombre = collection["nombre"];
                    usuario.appaterno = collection["appaterno"];
                    usuario.apmaterno = collection["apmaterno"];
                    usuario.edad = collection["edad"];
                    usuario.sexo = collection["sexo"];

                    Contacto contacto = new Contacto();
                    //contacto.id = Convert.ToInt32(collection["contacto.id"]);
                    contacto.telefono1 = collection["contacto.telefono1"];
                    contacto.telefono2 = collection["contacto.telefono2"];
                    contacto.correo = collection["contacto.correo"];
                  
                    usuario.contacto = contacto;

                    Direccion direccion = new Direccion();
                    //direccion.id = Convert.ToInt32(collection["direccion.id"]);
                    direccion.entidadFederativa = collection["direccion.entidadFederativa"];
                    direccion.codigoPostal = collection["direccion.codigoPostal"];
                    direccion.colonia = collection["direccion.colonia"];
                    direccion.calle = collection["direccion.calle"];
                    direccion.noInterior = collection["direccion.noInterior"];
                    direccion.noExterior = collection["direccion.noExterior"];
                    direccion.descripcion = collection["direccion.descripcion"];
                    
                    usuario.direccion= direccion;

                    if (usuario.id > 0)
                    {
                        //actualizar
                        contacto.entidad = usuario.id;
                        direccion.entidad = usuario.id;
                        servicioUsuario.Actualizar(usuario, out bool bandera1, out string msg1);
                        return RedirectToAction("Detalles/" + usuario.id);
                    }

                    else
                    {
                        usuario.fechaRegistro = DateTime.Now;
                        servicioUsuario.Guardar(usuario, out bool bandera, out string msg);
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

        // GET: Usuarios/Eliminar/id
        public ActionResult Eliminar(int id)
        {
            try
            {
                UsuarioCRUD s = new UsuarioCRUD();
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
