using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entidades;

public class Producto
{
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre no agregado")]
        [MaxLength(100, ErrorMessage = "Tienes mas de 100 Caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(255, ErrorMessage = "Tienes mas de 255 Caracteres")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "Precio vacio")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Categoria no seleccionada")]
        public int CategoriaId { get; set; }
}