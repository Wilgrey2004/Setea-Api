using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class DetallesVentaController : ControllerBase
        {
                private readonly Conexion _conexion;

                public DetallesVentaController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar un nuevo detalle de venta
                [HttpPost]
                public JsonResult PostDetalleVenta( [FromBody] DetallesVenta detalleVenta ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_DetallesVenta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@VentaId", detalleVenta.VentaId);
                                        cmd.Parameters.AddWithValue("@ProductoId", detalleVenta.ProductoId);
                                        cmd.Parameters.AddWithValue("@Cantidad", detalleVenta.Cantidad);
                                        cmd.Parameters.AddWithValue("@PrecioUnitario", detalleVenta.PrecioUnitario);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Detalle de venta creado correctamente" : "Error al crear el detalle de venta" });
                                }
                        }
                }

                // 2. Actualizar un detalle de venta
                [HttpPut("{id}")]
                public JsonResult PutDetalleVenta( [FromRoute] int id, [FromBody] DetallesVenta detalleVenta ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_DetallesVenta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@VentaId", detalleVenta.VentaId);
                                        cmd.Parameters.AddWithValue("@ProductoId", detalleVenta.ProductoId);
                                        cmd.Parameters.AddWithValue("@Cantidad", detalleVenta.Cantidad);
                                        cmd.Parameters.AddWithValue("@PrecioUnitario", detalleVenta.PrecioUnitario);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Detalle de venta actualizado correctamente" : "Error al actualizar el detalle de venta" });
                                }
                        }
                }

                // 3. Eliminar un detalle de venta
                [HttpDelete("{id}")]
                public JsonResult DeleteDetalleVenta( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_DetallesVenta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Detalle de venta eliminado correctamente" : "Error al eliminar el detalle de venta" });
                                }
                        }
                }

                // 4. Obtener todos los detalles de venta
                [HttpGet]
                public List<DetallesVenta> GetDetallesVenta() {
                        List<DetallesVenta> lst = new List<DetallesVenta>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_DetallesVenta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        DetallesVenta detalleVenta = new DetallesVenta {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                VentaId = reader.GetInt32(reader.GetOrdinal("VentaId")),
                                                                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                                                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                                                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario"))
                                                        };
                                                        lst.Add(detalleVenta);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 5. Obtener un detalle de venta por ID

                [HttpGet("{id}")]
                public ActionResult<DetallesVenta> GetDetalleVenta( [FromRoute] int id ) {
                        DetallesVenta? detalleVentaReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_DetalleVenta", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        detalleVentaReturn = new DetallesVenta {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                VentaId = reader.GetInt32(reader.GetOrdinal("VentaId")),
                                                                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                                                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                                                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario"))
                                                        };
                                                }
                                        }
                                }
                        }

                        if (detalleVentaReturn == null)
                        {
                                return NotFound(new { message = "Detalle de venta no encontrado" });
                        }

                        return Ok(detalleVentaReturn);
                }
        }
}