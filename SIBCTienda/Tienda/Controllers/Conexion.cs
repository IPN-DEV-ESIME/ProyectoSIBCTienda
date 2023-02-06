using System.Configuration;
using System.Data.SqlClient;

namespace Tienda.Controllers
{

    public class Conexion
    {
        private static string StrConexion = ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;

        public static SqlConnection GetConexion()
        {
            return new SqlConnection(StrConexion);
        }

    }
}