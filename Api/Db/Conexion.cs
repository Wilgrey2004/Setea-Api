using Microsoft.Data.SqlClient;

namespace Api.Db
{
        public class Conexion
        {
                
                public SqlConnection GetConnection() {

                        var builder = new SqlConnectionStringBuilder();
                        builder.DataSource = "WILDESK\\SQLEXPRESS"; //Este es el nombre de la base de datos En caso de usar otra computadora, cambiar este 
                        builder.IntegratedSecurity = true;
                        builder.InitialCatalog = "SistemaGestion";
                        return new SqlConnection(builder.ConnectionString);

                }


        }
}