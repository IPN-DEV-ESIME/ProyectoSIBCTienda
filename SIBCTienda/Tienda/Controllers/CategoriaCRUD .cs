using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Tienda.Models;

namespace Tienda.Controllers
{

    public class CategoriaCRUD
    {
        private SqlConnection con;

        public CategoriaCRUD()
        {
            //Conexion
            con = Conexion.GetConexion();

        }

        public List<Categoria> Listar()
        {
            List<Categoria> Categorias = new List<Categoria>();
            string sentencia = "select * from categorias";

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
                        Categorias.Add(ObtieneCategoria(reader)); //agregando Categoria a la lista
                    }

                }
                catch
                {

                }
                finally { con.Close(); }

            }

            return Categorias;

        }

        public void Guardar(Categoria x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "INSERT INTO categorias (descripcion)" +
                  " VALUES ('{0}')";

            if (x.Id > 0) //actualiza
            {
                sentencia = "update Categorias set descripcion='{0}' where id_categoria='{1}'";
                sentencia = string.Format(sentencia, x.Descripcion, x.Id);
            }
            else//guarda
                sentencia = string.Format(sentencia, x.Descripcion);



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

            string sentencia = "delete from categorias where id_categoria='{0}'";

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
                    msg = "Error al eliminar Categoria: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Categoria Buscar(int id, out bool bandera, out string msg)
        {
            Categoria u = new Categoria();
            msg = string.Empty;
            bandera = false;
            string sentencia = "select * from categorias where id_categoria='{0}'";
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
                        u = ObtieneCategoria(reader);
                    }

                }
                catch (SqlException ex)
                {
                    msg = "Error al buscar Categoria: " + ex.Message;

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

        private Categoria ObtieneCategoria(SqlDataReader reader)
        {
            Categoria x = new Categoria();

            x.Id = reader.GetInt32(0);
            x.Descripcion = reader.GetString(1);
            x.Descripcion = reader.GetString(1);
            return x;

        }

    }
}