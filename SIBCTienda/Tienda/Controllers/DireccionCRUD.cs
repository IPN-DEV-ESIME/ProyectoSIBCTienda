using System.Data.SqlClient;
using System.Data;
using Tienda.Models;

namespace Tienda.Controllers
{

    public class DireccionCRUD
    {
        private SqlConnection con;
        public DireccionCRUD()
        {
            //obteniendo la conexion
            con = Conexion.GetConexion();
        }

        public void Guardar(Direccion x, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;

            string sentencia = "INSERT INTO direcciones (entidad, entidadFederativa, codigoPostal, colonia, calle," +
                " noInterior, noExterior, descripcion) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.entidad,
               x.entidadFederativa,
               x.codigoPostal,
               x.colonia,
               x.calle,
               x.noInterior,
               x.noExterior,
               x.descripcion);

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
                    bandera = true;
                }
                catch (SqlException e)
                {
                    msg = "Error al guardar direccion: " + e.Message;
                }
                finally
                {
                    con.Close();
                }


            }
            else
                msg = "No hay conexion con la BD";
        }

        public void Actualizar(Direccion x, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;
            string sentencia = "update direcciones set entidadFederativa='{0}', codigoPostal='{1}', colonia='{2}', calle='{3}'," +
                " noInterior='{4}', noExterior='{5}', descripcion='{6}' " +
                "where entidad='{7}'";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.entidadFederativa,
               x.codigoPostal,
               x.colonia,
               x.calle,
               x.noInterior,
               x.noExterior,
               x.descripcion,
               x.entidad);

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
                    bandera = true;

                }
                catch (SqlException e)
                {
                    msg = "Error al actualizar direccion: " + e.Message;

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
            msg = string.Empty;
            bandera = false;
            string sentencia = "delete from direcciones where entidad='{0}'";
            sentencia = string.Format(sentencia, id.ToString());

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
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    bandera = true;

                }
                catch (SqlException e)
                {
                    msg = "Error al eliminar direccion: " + e.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Direccion Buscar(int id, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;
            Direccion d = new Direccion();

            string sentencia = "select * from direcciones where entidad='{0}'";

            sentencia = string.Format(sentencia, id.ToString());


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
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    //recorriendo los datos
                    while (reader.Read())
                    {
                        d = ObtieneDireccion(reader);
                    }


                }
                catch (SqlException e)
                {
                    msg = "Error al buscar direccion: " + e.Message;
                }
                finally
                {
                    con.Close();
                }


            }
            else
                msg = "No hay conexion con la BD";

            return d;

        }

        private Direccion ObtieneDireccion(SqlDataReader reader)
        {
            Direccion x = new Direccion();

            x.id = reader.GetInt32(0);
            x.entidad = reader.GetInt32(1);
            x.entidadFederativa = reader.GetString(2);
            x.codigoPostal = reader.GetString(3);
            x.colonia = reader.GetString(4);
            x.calle = reader.GetString(5);
            x.noInterior = reader.GetString(6);
            x.noExterior = reader.GetString(7);
            x.descripcion = reader.GetString(8);

            return x;

        }

    }
}