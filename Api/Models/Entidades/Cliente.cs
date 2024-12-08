
using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;

public class Cliente
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo 'Nombre' es requerido")]
    [MaxLength(100, ErrorMessage = "El campo 'Nombre' debe tener menos de 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo 'Correo' es requerido")]
    [EmailAddress(ErrorMessage = "El correo no es valido")]
    [MaxLength(255, ErrorMessage = "El campo 'Correo' debe tener menos de 255 caracteres")]

    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo 'Telefono' es requerido")]

    public string Telefono { get; set; } = string.Empty;
}
