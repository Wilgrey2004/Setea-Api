using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class UsuariosController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todos los usuarios
                [HttpGet]
                public List<Usuario> GetUsuarios() {
                        List<Usuario> lst = new List<Usuario>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Usuarios", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Usuario usuario = new Usuario {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre")),
                                                                Correo = Reader.GetString(Reader.GetOrdinal("Correo")),
                                                                Contraseña = Reader.GetString(Reader.GetOrdinal("Contraseña")),
                                                                RolId = Reader.GetInt32(Reader.GetOrdinal("RolId")),
                                                                FechaCreacion = Reader.GetDateTime(Reader.GetOrdinal("FechaCreacion"))
                                                        };
                                                        lst.Add(usuario);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener un usuario por ID
                [HttpGet("{id}")]
                public Usuario? GetUsuario( [FromRoute] int id ) {
                        Usuario? usuarioReturn = null;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Usuario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        usuarioReturn = new Usuario {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre")),
                                                                Correo = Reader.GetString(Reader.GetOrdinal("Correo")),
                                                                Contraseña = Reader.GetString(Reader.GetOrdinal("Contraseña")),
                                                                RolId = Reader.GetInt32(Reader.GetOrdinal("RolId")),
                                                                FechaCreacion = Reader.GetDateTime(Reader.GetOrdinal("FechaCreacion"))
                                                        };
                                                }
                                        }
                                }
                        }

                        return usuarioReturn;
                }

                // 3. Insertar un nuevo usuario
                [HttpPost]
                public JsonResult PostUsuario( [FromBody] Usuario usuario ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Usuarios", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                                        cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                                        cmd.Parameters.AddWithValue("@RolId", usuario.RolId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Usuario registrado correctamente" : "Error al registrar el usuario" });
                                }
                        }
                }

                // 4. Actualizar un usuario
                [HttpPut("{id}")]
                public JsonResult PutUsuario( [FromRoute] int id, [FromBody] Usuario usuario ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Usuarios", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                                        cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                                        cmd.Parameters.AddWithValue("@RolId", usuario.RolId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Usuario actualizado correctamente" : "Error al actualizar el usuario" });
                                }
                        }
                }

                // 5. Eliminar un usuario
                [HttpDelete("{id}")]
                public JsonResult DeleteUsuario( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Usuarios", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Usuario eliminado correctamente" : "Error al eliminar el usuario" });
                                }
                        }
                }
        }
}
        // 5. El