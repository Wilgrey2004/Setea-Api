namespace Api.Models.Entidades;
public class Auditoria
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Accion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now; // Valor por defecto
}