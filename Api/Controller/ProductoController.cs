using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class ProductosController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todos los productos
                [HttpGet]
                public List<Producto> GetProductos() {
                        List<Producto> lst = new List<Producto>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Productos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Producto producto = new Producto {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre")),
                                                                Descripcion = Reader.IsDBNull(Reader.GetOrdinal("Descripcion")) ? string.Empty : Reader.GetString(Reader.GetOrdinal("Descripcion")),
                                                                Precio = Reader.GetDecimal(Reader.GetOrdinal("Precio")),
                                                                CategoriaId = Reader.GetInt32(Reader.GetOrdinal("CategoriaId"))
                                                        };
                                                        lst.Add(producto);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener un producto por ID
                [HttpGet("{id}")]
                public Producto? GetProducto( [FromRoute] int id ) {
                        Producto? productoReturn = null;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Producto", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        productoReturn = new Producto {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre")),
                                                                Descripcion = Reader.IsDBNull(Reader.GetOrdinal("Descripcion")) ? string.Empty : Reader.GetString(Reader.GetOrdinal("Descripcion")),
                                                                Precio = Reader.GetDecimal(Reader.GetOrdinal("Precio")),
                                                                CategoriaId = Reader.GetInt32(Reader.GetOrdinal("CategoriaId"))
                                                        };
                                                }
                                        }
                                }
                        }

                        return productoReturn;
                }

                // 3. Insertar un nuevo producto
                [HttpPost]
                public JsonResult PostProducto( [FromBody] Producto producto ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Productos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                                        cmd.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Producto registrado correctamente" : "Error al registrar el producto" });
                                }
                        }
                }

                // 4. Actualizar un producto
                [HttpPut("{id}")]
                public JsonResult PutProducto( [FromRoute] int id, [FromBody] Producto producto ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Productos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                                        cmd.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Producto actualizado correctamente" : "Error al actualizar el producto" });
                                }
                        }
                }

                [HttpDelete("{id}")]
                public JsonResult DeleteProducto( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Productos", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Producto eliminado correctamente" : "Error al eliminar el producto" });
                                }
                        }
                }


        }
}