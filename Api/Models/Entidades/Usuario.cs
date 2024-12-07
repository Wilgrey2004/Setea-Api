namespace Api.Models.Entidades;
public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public int RolId { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now; // Valor por defecto
}