using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class AuditoriaController : ControllerBase
        {
                private readonly Conexion _conexion;

                public AuditoriaController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar un nuevo registro de auditoría
                [HttpPost]
                public JsonResult PostAuditoria( [FromBody] Auditoria auditoria ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Auditoria' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Auditoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@UsuarioId", auditoria.UsuarioId);
                                        cmd.Parameters.AddWithValue("@Accion", auditoria.Accion);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Registro de auditoría creado correctamente" : "Error al crear el registro de auditoría" });
                                }
                        }
                }

                // 2. Actualizar un registro de auditoría
                [HttpPut("{id}")]
                public JsonResult PutAuditoria( [FromRoute] int id, [FromBody] Auditoria auditoria ) {

                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Auditoria' :", error }) { StatusCode = 404 };
                        }

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Auditoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@UsuarioId", auditoria.UsuarioId);
                                        cmd.Parameters.AddWithValue("@Accion", auditoria.Accion);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Registro de auditoría actualizado correctamente" : "Error al actualizar el registro de auditoría" });
                                }
                        }
                }

                // 3. Eliminar un registro de auditoría
                [HttpDelete("{id}")]
                public JsonResult DeleteAuditoria( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Auditoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Registro de auditoría eliminado correctamente" : "Error al eliminar el registro de auditoría" });
                                }
                        }
                }

                // 4. Obtener todos los registros de auditoría
                [HttpGet]
                public List<Auditoria> GetAuditorias() {
                        List<Auditoria> lst = new List<Auditoria>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Auditoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        Auditoria auditoria = new Auditoria {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                                                Accion = reader.GetString(reader.GetOrdinal("Accion")),
                                                                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha"))
                                                        };
                                                        lst.Add(auditoria);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 5. Obtener un registro de auditoría por ID
                [HttpGet("{id}")]
                public ActionResult<Auditoria> GetAuditoria( [FromRoute] int id ) {
                        Auditoria? auditoriaReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Auditoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        auditoriaReturn = new Auditoria {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                                                Accion = reader.GetString(reader.GetOrdinal("Accion")),
                                                                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha"))
                                                        };
                                                }
                                        }
                                }
                        }

                        if (auditoriaReturn == null)
                        {
                                return NotFound(new { message = "Registro de auditoría no encontrado" });
                        }

                        return Ok(auditoriaReturn);
                }

        }
}