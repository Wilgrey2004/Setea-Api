using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entidades;

public class Roles
{
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre Es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede exeder los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
}