using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Tienda.Models;
using System.Reflection;

namespace Tienda.Controllers
{

    public class UsuarioCRUD
    {
        private SqlConnection con;
        ContactoCRUD contactoCRUD;
        DireccionCRUD direccionCRUD;

        public UsuarioCRUD()
        {
            //Conexion
            con = Conexion.GetConexion();
            //Contactos
            contactoCRUD = new ContactoCRUD();
            //direcciones
            direccionCRUD = new DireccionCRUD();
        }

        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string sentencia = "select * from usuarios";

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
                        usuarios.Add(ObtieneUsuario(reader)); //agregando usuario a la lista
                    }

                }
                catch
                {

                }
                finally { con.Close(); }

            }

            return usuarios;

        }

        public void Guardar(Usuario x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "INSERT INTO usuarios (rfc, username, password, nombre, appaterno, apmaterno, edad, sexo, fechaRegistro)" +
                " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); SELECT SCOPE_IDENTITY()";

            //ingresando los datos
            sentencia = string.Format(sentencia,
               x.rfc,
               x.username,
               x.password,
               x.nombre,
               x.appaterno,
               x.apmaterno,
               x.edad,
               x.sexo,
               x.fechaRegistro.ToString("yyyy-MM-dd HH:mm:ss"));

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
                    msg = "Usuario guardado";
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

        public void Actualizar(Usuario x, out bool bandera, out string msg)
        {
            bandera = false;
            msg = string.Empty;
            string sentencia = "update usuarios set rfc='{0}', username='{1}', password='{2}'," +
                   " nombre='{3}', appaterno='{4}', apmaterno='{5}', edad='{6}', sexo='{7}' where id_usuario='{8}'";

            sentencia = string.Format(sentencia,
               x.rfc,
               x.username,
               x.password,
               x.nombre,
               x.appaterno,
               x.apmaterno,
               x.edad,
               x.sexo,
               x.id);


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
                    msg = "Usuario actualizado";
                    con.Close();
                    //guardando contacto
                    contactoCRUD.Actualizar(x.contacto, out bandera, out msg);
                    direccionCRUD.Actualizar(x.direccion, out bandera, out msg);
                }
                catch (SqlException ex)
                {
                    msg = "Error al actualizar usuario: " + ex.Message;
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

            string sentencia = "delete from usuarios where id_usuario='{0}'";

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
                    msg = "Error al eliminar usuario: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }

            }
            else
                msg = "No hay conexion con la BD";
        }

        public Usuario Buscar(int id, out bool bandera, out string msg)
        {
            Usuario u = new Usuario();
            msg = string.Empty;
            bandera = false;
            string sentencia = "select * from usuarios where id_usuario='{0}'";
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
                        u = ObtieneUsuario(reader);
                    }
                    con.Close() ;

                    //agrega el contacto
                    u.contacto = contactoCRUD.Buscar(u.id, out bandera, out msg);
                    //agrega el domicilio
                    u.direccion = direccionCRUD.Buscar(u.id, out bandera, out msg);


                }
                catch (SqlException ex)
                {
                    msg = "Error al buscar usuario: " + ex.Message;

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

        private Usuario ObtieneUsuario(SqlDataReader reader)
        {
            Usuario u = new Usuario();

            u.id = reader.GetInt32(0);
            u.rfc = reader.GetString(1);
            u.username = reader.GetString(2);
            u.password = reader.GetString(3);
            u.nombre = reader.GetString(4);
            u.appaterno = reader.GetString(5);
            u.apmaterno = reader.GetString(6);
            u.edad = reader.GetString(7);
            u.sexo = reader.GetString(8);
            u.fechaRegistro = reader.GetDateTime(9);

            return u;

        }

        public Usuario Login(string usuario, string pass)
        {
            Usuario u=new Usuario();    
            string sentencia = "select * from usuarios where username='{0}' and password='{1}'";
            sentencia = string.Format(sentencia,
                usuario,
                pass);

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
                        u = ObtieneUsuario(reader);
                    }
                    con.Close();

                    //agrega el contacto
                    u.contacto = contactoCRUD.Buscar(u.id, out bool bandera, out string msg);
                    //agrega el domicilio
                    u.direccion = direccionCRUD.Buscar(u.id, out bool bandera2, out string msg2);

                }
                catch 
                {
                }
                finally
                {
                    con.Close();
                }

            }

            return u;
        }

    }
}