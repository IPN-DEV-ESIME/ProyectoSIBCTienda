using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Tienda.Models;
using System.Globalization;
using System.Data.Common;

namespace Tienda.Controllers
{

    public class ProdutoCRUD
    {
        private SqlConnection con;

        public ProdutoCRUD()
        {
            //Conexion
            con = Conexion.GetConexion();

        }

        public List<Producto> Listar()
        {
            List<Producto> Productos = new List<Producto>();
            string sentencia = "select * from productos";

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
                        Productos.Add(ObtieneProducto(reader)); //agregando Producto a la lista
                    }

                }
                catch
                {

                }
                finally { con.Close(); }

            }

            return Productos;

        }

        public void Guardar(Producto x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "INSERT INTO productos " +
                "(nombre, categoria_id, proveedor_id, descripcion, sku, fechaRegistro, cantidad, precio)" +
                  " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

            if (x.Id > 0) //actualiza
            {
                sentencia = "update productos set " +
                    "nombre='{0}', categoria_id='{1}', proveedor_id='{2}', descripcion='{3}', sku='{4}', fechaRegistro='{5}', cantidad='{6}', precio='{7}' " +
                    "where id_producto='{8}'";
                sentencia = string.Format(sentencia,
                    x.Nombre,
                    x.CategoriaId,
                    x.ProveedorId,
                    x.Descripcion,
                    x.Sku,
                    x.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss"),
                    x.Cantidad,
                    x.Precio,
                    x.Id);
            }
            else//guarda
            {
                sentencia = string.Format(sentencia, x.Descripcion);
                sentencia = string.Format(sentencia,
                    x.Nombre,
                    x.CategoriaId,
                    x.ProveedorId,
                    x.Descripcion,
                    x.Sku,
                    x.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss"),
                    x.Cantidad,
                    x.Precio);
            }

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
                    sqlCmd.ExecuteNonQuery();

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

        public void Eliminar(int id, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;

            string sentencia = "delete from productos where id_producto='{0}'";

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

                }
                catch (SqlException ex)
                {
                    msg = "Error al eliminar Producto: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Producto Buscar(int id, out bool bandera, out string msg)
        {
            Producto u = new Producto();
            msg = string.Empty;
            bandera = false;
            string sentencia = "select * from productos where id_producto='{0}'";
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
                        u = ObtieneProducto(reader);
                    }

                }
                catch (SqlException ex)
                {
                    msg = "Error al buscar Producto: " + ex.Message;

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

        private Producto ObtieneProducto(SqlDataReader reader)
        {
            Producto x = new Producto();

            x.Id = reader.GetInt32(0);
            x.Nombre = reader.GetString(1);
            x.CategoriaId= reader.GetInt32(2);
            x.ProveedorId = reader.GetInt32(3);
            x.Descripcion = reader.GetString(4);
            x.Sku = reader.GetString(5);
            x.FechaRegistro = reader.GetDateTime(6);
            x.Cantidad = reader.GetInt32(7);
            x.Precio = (float)reader.GetDouble(8);
            return x;

        }

    }
}