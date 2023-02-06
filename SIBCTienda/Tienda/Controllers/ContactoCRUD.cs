using System.Data.SqlClient;
using System.Data;
using Tienda.Models;

namespace Tienda.Controllers
{

    public class ContactoCRUD
    {
        private SqlConnection con;
        public ContactoCRUD()
        {
            //obteniendo la conexion
            con = Conexion.GetConexion();
        }

        public void Guardar(Contacto x, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;

            string sentencia = "INSERT INTO contactos (telefono1, telefono2, correo, entidad)" +
                " VALUES ('{0}','{1}','{2}','{3}');";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.telefono1,
               x.telefono2,
               x.correo,
               x.entidad);

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
                    msg = "Error al guardar contacto: " + e.Message;
                }
                finally
                {
                    con.Close();
                }


            }
            else
                msg = "No hay conexion con la BD";
        }

        public void Actualizar(Contacto x, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;
            string sentencia = "update contactos set telefono1='{0}', telefono2='{1}', correo='{2}'" +
                " where entidad='{3}'";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.telefono1,
               x.telefono2,
               x.correo,
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
                    msg = "Error al actualizar contacto: " + e.Message;

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
            string sentencia = "delete from contactos where entidad='{0}'";
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
                    msg = "Error al eliminar contacto: " + e.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Contacto Buscar(int id, out bool bandera, out string msg)
        {
            msg = string.Empty;
            bandera = false;
            Contacto c = new Contacto();

            string sentencia = "select * from contactos where entidad='{0}'";

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
                        c = ObtieneContacto(reader);
                    }


                }
                catch (SqlException e)
                {
                    msg = "Error al buscar contacto: " + e.Message;
                }
                finally
                {
                    con.Close();
                }


            }
            else
                msg = "No hay conexion con la BD";

            return c;

        }

        private Contacto ObtieneContacto(SqlDataReader reader)
        {
            Contacto x = new Contacto();

            x.id = reader.GetInt32(0);
            x.telefono1 = reader.GetString(1);
            x.telefono2 = reader.GetString(2);
            x.correo = reader.GetString(3);
            x.entidad = reader.GetInt32(4);

            return x;

        }

    }
}