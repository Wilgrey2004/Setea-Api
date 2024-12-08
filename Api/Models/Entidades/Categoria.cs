using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entidades;

public class Categoria
{
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="El maximo Es 50")]
        [MinLength(1, ErrorMessage = "El Minimo Es 1")]
        public string Nombre { get; set; } = string.Empty;
}