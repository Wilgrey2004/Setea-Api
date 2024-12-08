using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class CitasController : ControllerBase
        {
                private readonly Conexion _conexion;

                public CitasController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar una nueva cita
                [HttpPost]
                public JsonResult PostCita( [FromBody] Citas cita ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Citas' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Citas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@ClienteId", cita.ClienteId);
                                        cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                                        cmd.Parameters.AddWithValue("@Servicio", cita.Servicio);
                                        cmd.Parameters.AddWithValue("@RecordatorioEnviado", cita.RecordatorioEnviado);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cita creada correctamente" : "Error al crear la cita" });
                                }
                        }
                }

                // 2. Actualizar una cita
                [HttpPut("{id}")]
                public JsonResult PutCita( [FromRoute] int id, [FromBody] Citas cita ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Citas' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Citas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@ClienteId", cita.ClienteId);
                                        cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                                        cmd.Parameters.AddWithValue("@Servicio", cita.Servicio);
                                        cmd.Parameters.AddWithValue("@RecordatorioEnviado", cita.RecordatorioEnviado);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cita actualizada correctamente" : "Error al actualizar la cita" });
                                }
                        }
                }

                // 3. Eliminar una cita
                [HttpDelete("{id}")]
                public JsonResult DeleteCita( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Citas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cita eliminada correctamente" : "Error al eliminar la cita" });
                                }
                        }
                }

                // 4. Obtener todas las citas
                [HttpGet]
                public List<Citas> GetCitas() {
                        List<Citas> lst = new List<Citas>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Citas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        Citas cita = new Citas {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                                                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                                                Servicio = reader.GetString(reader.GetOrdinal("Servicio")),
                                                                RecordatorioEnviado = reader.GetBoolean(reader.GetOrdinal("RecordatorioEnviado"))
                                                        };
                                                        lst.Add(cita);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 5. Obtener una cita por ID
                [HttpGet("{id}")]
                public ActionResult<Citas> GetCita( [FromRoute] int id ) {
                        Citas? citaReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Cita", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        citaReturn = new Citas {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                                                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                                                Servicio = reader.GetString(reader.GetOrdinal("Servicio")),
                                                                RecordatorioEnviado = reader.GetBoolean(reader.GetOrdinal("RecordatorioEnviado"))
                                                        };
                                                }
                                        }
                                }
                        }

                        if (citaReturn == null)
                        {
                                return NotFound(new { message = "Cita no encontrada" });
                        }

                        return Ok(citaReturn);
                }
        }
}