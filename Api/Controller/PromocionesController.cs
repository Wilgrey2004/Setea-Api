using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class PromocionesController : ControllerBase
        {
                private readonly Conexion _conexion;

                public PromocionesController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar una nueva promoción
                [HttpPost]
                public JsonResult PostPromocion( [FromBody] Promociones promocion ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Promociones' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Promociones", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", promocion.Nombre);
                                        cmd.Parameters.AddWithValue("@DescuentoPorcentaje", promocion.DescuentoPorcentaje);
                                        cmd.Parameters.AddWithValue("@FechaInicio", promocion.FechaInicio);
                                        cmd.Parameters.AddWithValue("@FechaFin", promocion.FechaFin);
                                        cmd.Parameters.AddWithValue("@Activa", promocion.Activa);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Promoción creada correctamente" : "Error al crear la promoción" });
                                }
                        }
                }

                // 2. Actualizar una promoción
                [HttpPut("{id}")]
                public JsonResult PutPromocion( [FromRoute] int id, [FromBody] Promociones promocion ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Promociones' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Promociones", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", promocion.Nombre);
                                        cmd.Parameters.AddWithValue("@DescuentoPorcentaje", promocion.DescuentoPorcentaje);
                                        cmd.Parameters.AddWithValue("@FechaInicio", promocion.FechaInicio);
                                        cmd.Parameters.AddWithValue("@FechaFin", promocion.FechaFin);
                                        cmd.Parameters.AddWithValue("@Activa", promocion.Activa);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Promoción actualizada correctamente" : "Error al actualizar la promoción" });
                                }
                        }
                }

                // 3. Eliminar una promoción
                [HttpDelete("{id}")]
                public JsonResult DeletePromocion( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Promociones", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Promoción eliminada correctamente" : "Error al eliminar la promoción" });
                                }
                        }
                }

                // 4. Obtener todas las promociones
                [HttpGet]
                public List<Promociones> GetPromociones() {
                        List<Promociones> lst = new List<Promociones>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Promociones", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        Promociones promocion = new Promociones {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                                                DescuentoPorcentaje = reader.IsDBNull(reader.GetOrdinal("DescuentoPorcentaje")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DescuentoPorcentaje")),
                                                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                                                FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                                                                Activa = reader.GetBoolean(reader.GetOrdinal("Activa"))
                                                        };
                                                        lst.Add(promocion);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 5. Obtener una promoción por ID
                [HttpGet("{id}")]
                public ActionResult<Promociones> GetPromocion( [FromRoute] int id ) {
                        Promociones? promocionReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Promocion", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        promocionReturn = new Promociones {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                                                DescuentoPorcentaje = reader.IsDBNull(reader.GetOrdinal("DescuentoPorcentaje")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DescuentoPorcentaje")),
                                                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                                                FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                                                                Activa = reader.GetBoolean(reader.GetOrdinal("Activa"))
                                                        };
                                                }
                                        }
                                }
                        }

                        if (promocionReturn == null)
                        {
                                return NotFound(new { message = "Promoción no encontrada" });
                        }

                        return Ok(promocionReturn);
                }
        }
}