using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class VentasController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todas las ventas
                [HttpGet]
                public List<Venta> GetVentas() {
                        List<Venta> lst = new List<Venta>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Ventas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Venta venta = new Venta {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                ClienteId = (int)(Reader.IsDBNull(Reader.GetOrdinal("ClienteId")) ? 0 : Reader.GetInt32(Reader.GetOrdinal("ClienteId"))),
                                                                Fecha = Reader.GetDateTime(Reader.GetOrdinal("Fecha")),
                                                                Total = Reader.GetDecimal(Reader.GetOrdinal("Total"))
                                                        };
                                                        lst.Add(venta);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener una venta por ID
                [HttpGet("{id}")]
                public Venta? GetVenta( [FromRoute] int id ) {
                        Venta ventaReturn;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Venta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        ventaReturn = new Venta {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                ClienteId = (int)(Reader.IsDBNull(Reader.GetOrdinal("ClienteId")) ? 0 : Reader.GetInt32(Reader.GetOrdinal("ClienteId"))),
                                                                Fecha = Reader.GetDateTime(Reader.GetOrdinal("Fecha")),
                                                                Total = Reader.GetDecimal(Reader.GetOrdinal("Total"))
                                                        };

                                                        return ventaReturn;
                                                }
                                        }
                                }
                        }

                        return null;
                }

                // 3. Insertar una nueva venta
                [HttpPost]
                public JsonResult PostVenta( [FromBody] Venta venta ) {
                       
                                               if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Ventas' :", error }) { StatusCode = 404 };
                        }
                       
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Ventas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                                        cmd.Parameters.AddWithValue("@MetodoPagoId", 1); // Asegúrate de que esta propiedad esté en la clase Venta
                                        cmd.Parameters.AddWithValue("@Total", venta.Total);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Venta registrada correctamente" : "Error al registrar la venta" });
                                }
                        }
                }

                // 4. Actualizar una venta
                [HttpPut("{id}")]
                public JsonResult PutVenta( [FromRoute] int id, [FromBody] Venta venta ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Ventas' :", error }) { StatusCode = 404 };
                        }

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Ventas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                                        cmd.Parameters.AddWithValue("@MetodoPagoId", 1); // Asegúrate de que esta propiedad esté en la clase Venta
                                        cmd.Parameters.AddWithValue("@Total", venta.Total);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Venta actualizada correctamente" : "Error al actualizar la venta" });
                                }
                        }
                } 
                
                [HttpDelete("{id}")]
                public JsonResult DeleteVenta( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Ventas", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Venta Eliminada correctamente" : "Error al Eliminada la venta" });
                                }
                        }
                }

                // 5. Eliminar una venta
        }
}