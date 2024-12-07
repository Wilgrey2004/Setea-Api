using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class RolesController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todos los roles
                [HttpGet]
                public List<Roles> GetRoles() {
                        List<Roles> lst = new List<Roles>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Roles", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Roles rol = new Roles {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre"))
                                                        };
                                                        lst.Add(rol);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener un rol por ID
                [HttpGet("{id}")]
                public Roles? GetRol( [FromRoute] int id ) {
                        Roles? rolReturn = null;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Rol", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        rolReturn = new Roles {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre"))
                                                        };
                                                }
                                        }
                                }
                        }

                        return rolReturn;
                }

                // 3. Insertar un nuevo rol
                [HttpPost]
                public JsonResult PostRol( [FromBody] Roles rol ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Roles", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Rol registrado correctamente" : "Error al registrar el rol" });
                                }
                        }
                }

                // 4. Actualizar un rol
                [HttpPut("{id}")]
                public JsonResult PutRol( [FromRoute] int id, [FromBody] Roles rol ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Roles", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Rol actualizado correctamente" : "Error al actualizar el rol" });
                                }
                        }
                }

                // 5. Eliminar un rol
                [HttpDelete("{id}")]
                public JsonResult DeleteRol( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Roles", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Rol eliminado correctamente" : "Error al eliminar el rol" });
                                }
                        }
                }
        }
}