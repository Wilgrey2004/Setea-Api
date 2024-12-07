using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class PromocionesProductosController : ControllerBase
        {
                private readonly Conexion _conexion;

                public PromocionesProductosController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar una nueva relación entre promoción y producto
                [HttpPost]
                public JsonResult PostPromocionProducto( [FromBody] PromocionesProductos promocionProducto ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_PromocionesProductos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@PromocionId", promocionProducto.PromocionId);
                                        cmd.Parameters.AddWithValue("@ProductoId", promocionProducto.ProductoId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Relación creada correctamente" : "Error al crear la relación" });
                                }
                        }
                }

                // 2. Actualizar una relación entre promoción y producto
                [HttpPut("{id}")]
                public JsonResult PutPromocionProducto( [FromRoute] int id, [FromBody] PromocionesProductos promocionProducto ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_PromocionesProductos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@PromocionId", promocionProducto.PromocionId);
                                        cmd.Parameters.AddWithValue("@ProductoId", promocionProducto.ProductoId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Relación actualizada correctamente" : "Error al actualizar la relación" });
                                }
                        }
                }

                // 3. Eliminar una relación entre promoción y producto
                [HttpDelete("{id}")]
                public JsonResult DeletePromocionProducto( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_PromocionesProductos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Relación eliminada correctamente" : "Error al eliminar la relación" });
                                }
                        }
                }

                // 4. Obtener todas las relaciones entre promociones y productos
                [HttpGet]
                public List<PromocionesProductos> GetPromocionesProductos() {
                        List<PromocionesProductos> lst = new List<PromocionesProductos>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_PromocionesProductos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        PromocionesProductos promocionProducto = new PromocionesProductos {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                PromocionId = reader.GetInt32(reader.GetOrdinal("PromocionId")),
                                                                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId"))
                                                        };
                                                        lst.Add(promocionProducto);
                                                }
                                        }
                                }
                        }

                        return lst;
                }
                // 5. Obtener una relación por ID
                [HttpGet("{id}")]
                public ActionResult<PromocionesProductos> GetPromocionProducto( [FromRoute] int id ) {
                        PromocionesProductos? promocionProductoReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_PromocionProducto", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        promocionProductoReturn = new PromocionesProductos {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                PromocionId = reader.GetInt32(reader.GetOrdinal("PromocionId")),
                                                                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId"))
                                                        };
                                                }
                                        }
                                }
                        }

                        if (promocionProductoReturn == null)
                        {
                                return NotFound(new { message = "Relación no encontrada" });
                        }

                        return Ok(promocionProductoReturn);
                }
        }
}