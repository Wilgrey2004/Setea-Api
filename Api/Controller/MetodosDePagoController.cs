using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class MetodosDePagoController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todos los métodos de pago
                [HttpGet]
                public List<MetodosPago> GetMetodosPago() {
                        List<MetodosPago> lst = new List<MetodosPago>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_MetodosPago", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        MetodosPago?   metodoPago = new MetodosPago {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Metodo = Reader.GetString(Reader.GetOrdinal("Metodo"))
                                                        };
                                                        lst.Add(metodoPago);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener un método de pago por ID
                [HttpGet("{id}")]
                public MetodosPago? GetMetodoPago( [FromRoute] int id ) {
                        MetodosPago? metodoPagoReturn = null;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_MetodoPago", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        metodoPagoReturn = new MetodosPago {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Metodo = Reader.GetString(Reader.GetOrdinal("Metodo"))
                                                        };
                                                }
                                        }
                                }
                        }

                        return metodoPagoReturn;
                }

                // 3. Insertar un nuevo método de pago
                [HttpPost]
                public JsonResult PostMetodoPago( [FromBody] MetodosPago metodoPago ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Metodos De Pago' :", error }) { StatusCode = 404 };
                        }
                        
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_MetodosPago", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Metodo", metodoPago.Metodo);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Método de pago registrado correctamente" : "Error al registrar el método de pago" });
                                }
                        }
                }

                // 4. Actualizar un método de pago
                [HttpPut("{id}")]
                public JsonResult PutMetodoPago( [FromRoute] int id, [FromBody] MetodosPago metodoPago ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Metodos De Pago' :", error }) { StatusCode = 404 };
                        }

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_MetodosPago", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Metodo", metodoPago.Metodo);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Método de pago actualizado correctamente" : "Error al actualizar el método de pago" });
                                }
                        }
                }


                // 5. Eliminar un método de pago
                [HttpDelete("{id}")]
                public JsonResult DeleteMetodoPago( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_MetodosPago", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Método de pago eliminado correctamente" : "Error al eliminar el método de pago" });
                                }
                        }
                }
        }
}