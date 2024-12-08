using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entidades;
public class Usuario
{
    public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El correo es requerido")]
    [MaxLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres")]
    public string Correo { get; set; } = string.Empty;
    [Required(ErrorMessage = "La contraseña es requerida")]
    [MaxLength(100, ErrorMessage = "La contraseña no puede tener más de 100 caracteres")]
    public string Contraseña { get; set; } = string.Empty;
    [Required(ErrorMessage = "El rol es requerido")]
    public int RolId { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now; // Valor por defecto
}