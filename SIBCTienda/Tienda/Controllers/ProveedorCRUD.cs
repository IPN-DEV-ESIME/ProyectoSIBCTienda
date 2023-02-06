using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Tienda.Models;

namespace Tienda.Controllers
{

    public class ProveedorCRUD
    {
        private SqlConnection con;
        ContactoCRUD contactoCRUD;
        DireccionCRUD direccionCRUD;

        public ProveedorCRUD()
        {
            //Conexion
            con = Conexion.GetConexion();
            //Contactos
            contactoCRUD = new ContactoCRUD();
            //direcciones
            direccionCRUD = new DireccionCRUD();
        }

        public List<Proveedor> Listar()
        {
            List<Proveedor> Proveedors = new List<Proveedor>();
            string sentencia = "select * from proveedores";

            if (con != null)
            {

                SqlCommand sqlCmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sentencia,
                    CommandType = CommandType.Text
                };

                try
                {
                    con.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    //recorriendo los datos
                    while (reader.Read())
                    {
                        Proveedors.Add(ObtieneProveedor(reader)); //agregando Proveedor a la lista
                    }

                }
                catch
                {

                }
                finally { con.Close(); }

            }

            return Proveedors;

        }

        public void Guardar(Proveedor x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "INSERT INTO proveedores (nombre, rfc, paginaWeb)" +
                " VALUES ('{0}','{1}','{2}'); SELECT SCOPE_IDENTITY()";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.Nombre,
               x.Rfc,
               x.PaginaWeb);

            //si hay conexion
            if (con != null)
            {
                SqlCommand sqlCmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sentencia,
                    CommandType = CommandType.Text
                };

                //intentando la consulta
                try
                {
                    con.Open();
                    //obtiene el id generado y asigna
                    int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    x.contacto.entidad = id;
                    x.direccion.entidad = id;
                    msg = "Proveedor guardado";
                    con.Close();
                    //guardando contacto
                    contactoCRUD.Guardar(x.contacto, out bandera, out msg);
                    //guardando direccion
                    direccionCRUD.Guardar(x.direccion, out bandera, out msg);

                }
                catch (SqlException ex)
                {
                    msg = "Error:" + ex.Message;

                }
                finally
                {
                    con.Close();
                }


            }
            else
                msg = "No hay conexion con la BD";

        }

        public void Actualizar(Proveedor x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "update proveedores set rfc='{0}', nombre='{1}', paginaWeb='{2}'" +
                   " where id_proveedor='{3}'";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.Nombre,
               x.Rfc,
               x.PaginaWeb,
               x.Id);


            SqlConnection con = Conexion.GetConexion();

            if (con != null)
            {
                SqlCommand sqlCmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sentencia,
                    CommandType = CommandType.Text
                };

                try
                {
                    con.Open();
                    sqlCmd.ExecuteNonQuery();
                    msg = "Proveedor actualizado";
                    con.Close();
                    //guardando contacto
                    contactoCRUD.Actualizar(x.contacto, out bandera, out msg);
                    direccionCRUD.Actualizar(x.direccion, out bandera, out msg);
                }
                catch (SqlException ex)
                {
                    msg = "Error al actualizar Proveedor: " + ex.Message;
                    bandera = false;

                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";

        }

        public void Eliminar(int id, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;

            string sentencia = "delete from proveedores where id_proveedor='{0}'";

            sentencia = string.Format(sentencia, id.ToString());

            if (con != null)
            {
                SqlCommand sqlCmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sentencia,
                    CommandType = CommandType.Text
                };

                try
                {
                    con.Open();
                    sqlCmd.ExecuteNonQuery();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    con.Close();

                    //eliminando el contacto
                    contactoCRUD.Eliminar(id, out bandera, out msg);
                    direccionCRUD.Eliminar(id, out bandera, out msg);

                }
                catch (SqlException ex)
                {
                    msg = "Error al eliminar Proveedor: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Proveedor Buscar(int id, out bool bandera, out string msg)
        {
            Proveedor u = new Proveedor();
            msg = string.Empty;
            bandera = false;
            string sentencia = "select * from proveedores where id_proveedor='{0}'";
            sentencia = string.Format(sentencia, id.ToString());

            if (con != null)
            {
                SqlCommand sqlCmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = sentencia,
                    CommandType = CommandType.Text
                };

                try
                {
                    con.Open();
                    sqlCmd.ExecuteNonQuery();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    //recorriendo los datos
                    while (reader.Read())
                    {
                        u = ObtieneProveedor(reader);
                    }
                    con.Close();

                    //agrega el contacto
                    u.contacto = contactoCRUD.Buscar(u.Id, out bandera, out msg);
                    //agrega el domicilio
                    u.direccion = direccionCRUD.Buscar(u.Id, out bandera, out msg);


                }
                catch (SqlException ex)
                {
                    msg = "Error al buscar Proveedor: " + ex.Message;

                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";

            return u;
        }

        private Proveedor ObtieneProveedor(SqlDataReader reader)
        {
            Proveedor x = new Proveedor();

            x.Id = reader.GetInt32(0);
            x.Nombre = reader.GetString(1);
            x.Rfc = reader.GetString(2);
            x.PaginaWeb = reader.GetString(3);
            return x;

        }

    }
}