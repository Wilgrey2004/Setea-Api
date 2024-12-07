using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class InventarioController : ControllerBase
        {
                Conexion conexion = new Conexion();

                [HttpGet]
                public List<Inventario> GetInventario() {
                        List<Inventario> lst = new List<Inventario>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Inventario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Inventario inventario = new Inventario {
                                                                ProductoId = Reader.GetInt32(Reader.GetOrdinal("ProductoId")),
                                                                StockMinimo = Reader.GetInt32(Reader.GetOrdinal("StockMinimo")),
                                                                Cantidad = Reader.GetInt32(Reader.GetOrdinal("Cantidad"))
                                                        };
                                                        lst.Add(inventario);
                                                }
                                        }
                                }
                        }

                        return lst;
                } 
                
                [HttpGet("{idinventario}")]
                public Inventario? GetInventario( [FromRoute] int idinventario ) {
                        
                        Inventario invReturn;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Inventario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", idinventario);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        invReturn = new Inventario {
                                                                ProductoId = Reader.GetInt32(Reader.GetOrdinal("ProductoId")),
                                                                StockMinimo = Reader.GetInt32(Reader.GetOrdinal("StockMinimo")),
                                                                Cantidad = Reader.GetInt32(Reader.GetOrdinal("Cantidad"))
                                                        };

                                                        return invReturn;
                                                }

                                               
                                        }
                                }
                        }

                        return null;
                       
                }

                [HttpPost]
                public JsonResult PostInventario( Inventario inv ) {
                        
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Inventario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@ProductoId", inv.ProductoId);
                                        cmd.Parameters.AddWithValue("@Cantidad", inv.Cantidad);
                                        cmd.Parameters.AddWithValue("@StockMinimo", inv.StockMinimo);

                                        int resultado = cmd.ExecuteNonQuery();
                                       
                                        if (resultado > 0)
                                        {
                                                return new JsonResult(new { success = true, message = "Correcto" });
                                        } else
                                        {
                                                return new JsonResult(new { success = false, message = "Incorrecto" });
                                        }
                                }
                        }                 
                }

                [HttpPut("{IdInventario}")]
                public JsonResult PutInventario( [FromRoute] int IdInventario,[FromBody] Inventario inv ) {
                        
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Inventario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", IdInventario);
                                        cmd.Parameters.AddWithValue("@ProductoId", inv.ProductoId);
                                        cmd.Parameters.AddWithValue("@Cantidad", inv.Cantidad);
                                        cmd.Parameters.AddWithValue("@StockMinimo", inv.StockMinimo);

                                        int resultado = cmd.ExecuteNonQuery();
                                       
                                        if (resultado > 0)
                                        {
                                                return new JsonResult(new { success = true, message = "Correcto" });
                                        } else
                                        {
                                                return new JsonResult(new { success = false, message = "Incorrecto" });
                                        }
                                }
                        }                 
                }
                
                [HttpDelete("{IdInventario}")]
                public JsonResult DestroyInventario( [FromRoute] int IdInventario ) {
                        
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Inventario", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", IdInventario);
                                       
                                        int resultado = cmd.ExecuteNonQuery();
                                       
                                        if (resultado > 0)
                                        {
                                                return new JsonResult(new { success = true, message = "Correcto" });
                                        } else
                                        {
                                                return new JsonResult(new { success = false, message = "Incorrecto" });
                                        }
                                }
                        }                 
                }
        }
}
