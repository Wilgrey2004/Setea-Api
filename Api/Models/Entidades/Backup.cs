namespace Api.Models.Entidades;
public class Backups
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now; // Valor por defecto
    public string Tipo { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
}