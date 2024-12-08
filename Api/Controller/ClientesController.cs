using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class ClientesController : ControllerBase
        {
                private readonly Conexion _conexion;

                public ClientesController() {
                        _conexion = new Conexion();
                }

                // 1. Insertar un nuevo cliente
                [HttpPost]
                public JsonResult PostCliente( [FromBody] Cliente cliente ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Clientes' :", error }) { StatusCode = 404 };
                        }
                        
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Clientes", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                                        cmd.Parameters.AddWithValue("@Correo", cliente.Email);
                                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cliente creado correctamente" : "Error al crear el cliente" });
                                }
                        }
                }

                // 2. Actualizar un cliente
                [HttpPut("{id}")]
                public JsonResult PutCliente( [FromRoute] int id, [FromBody] Cliente cliente ) {
                        
                        if (!ModelState.IsValid)
                        {
                                var error = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                                return new JsonResult(new { message = "Error en la forma del Modelo 'Clientes' :", error }) { StatusCode = 404 };
                        }

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Clientes", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                                        cmd.Parameters.AddWithValue("@Correo", cliente.Email);
                                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                                       // Asegúrate de que la propiedad Clasificacion esté en la clase Cliente

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cliente actualizado correctamente" : "Error al actualizar el cliente" });
                                }
                        }
                }

                // 3. Eliminar un cliente
                [HttpDelete("{id}")]
                public JsonResult DeleteCliente( [FromRoute] int id ) {
                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Clientes", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Cliente eliminado correctamente" : "Error al eliminar el cliente" });
                                }
                        }
                }

                // 4. Obtener todos los clientes
                [HttpGet]
                public List<Cliente> GetClientes() {
                        List<Cliente> lst = new List<Cliente>();

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Clientes", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                while (reader.Read())
                                                {
                                                        Cliente cliente = new Cliente {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                                                Email = reader.GetString(reader.GetOrdinal("Correo")),
                                                                Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                                        };
                                                        lst.Add(cliente);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 5. Obtener un cliente por ID
                // 5. Obtener un cliente por ID
                [HttpGet("{id}")]
                public ActionResult<Cliente> GetCliente( [FromRoute] int id ) {
                        Cliente? clienteReturn = null;

                        using (var cn = _conexion.GetConnection())
                        {
                                cn.Open();
                                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Cliente", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var reader = cmd.ExecuteReader())
                                        {
                                                if (reader.Read())
                                                {
                                                        clienteReturn = new Cliente {
                                                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                                                Email = reader.GetString(reader.GetOrdinal("Correo")),
                                                                Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                                               
                                                        };
                                                }
                                        }
                                }
                        }

                        if (clienteReturn == null)
                        {
                                return NotFound(new { message = "Cliente no encontrado" });
                        }

                        return Ok(clienteReturn);
                }


        }
}