namespace Api.Models.Entidades;
public class Citas
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; }
    public string Servicio { get; set; } = string.Empty;
    public bool RecordatorioEnviado { get; set; } = false; // Valor por defecto
}