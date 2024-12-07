using Api.Db;
using Api.Models.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriasController : ControllerBase
        {
                Conexion conexion = new Conexion();

                // 1. Obtener todas las categorías
                [HttpGet]
                public List<Categoria> GetCategorias() {
                        List<Categoria> lst = new List<Categoria>();

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_In_Categorias", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                while (Reader.Read())
                                                {
                                                        Categoria categoria = new Categoria {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre"))
                                                        };
                                                        lst.Add(categoria);
                                                }
                                        }
                                }
                        }

                        return lst;
                }

                // 2. Obtener una categoría por ID
                [HttpGet("{id}")]
                public Categoria? GetCategoria( [FromRoute] int id ) {
                        Categoria? categoriaReturn = null;

                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Get_un_Categoria", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        using (var Reader = cmd.ExecuteReader())
                                        {
                                                if (Reader.Read())
                                                {
                                                        categoriaReturn = new Categoria {
                                                                Id = Reader.GetInt32(Reader.GetOrdinal("Id")),
                                                                Nombre = Reader.GetString(Reader.GetOrdinal("Nombre"))
                                                        };
                                                }
                                        }
                                }
                        }

                        return categoriaReturn;
                }

                // 3. Insertar una nueva categoría
                [HttpPost]
                public JsonResult PostCategoria( [FromBody] Categoria categoria ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Insert_In_Categorias", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Categoría registrada correctamente" : "Error al registrar la categoría" });
                                }
                        }
                }

                // 4. Actualizar una categoría
                [HttpPut("{id}")]
                public JsonResult PutCategoria( [FromRoute] int id, [FromBody] Categoria categoria ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Update_In_Categorias", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);
                                        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Categoría actualizada correctamente" : "Error al actualizar la categoría" });
                                }
                        }
                }

                // 5. Eliminar una categoría
                [HttpDelete("{id}")]
                public JsonResult DeleteCategoria( [FromRoute] int id ) {
                        using (Microsoft.Data.SqlClient.SqlConnection cn = conexion.GetConnection())
                        {
                                cn.Open();
                                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Sp_Delete_In_Categorias", cn))
                                {
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Id", id);

                                        int resultado = cmd.ExecuteNonQuery();
                                        return new JsonResult(new { success = resultado > 0, message = resultado > 0 ? "Categoría eliminada correctamente" : "Error al eliminar la categoría" });
                                }
                        }
                }
        }
}