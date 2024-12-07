namespace Api.Models.Entidades;

public class Venta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now; // Valor por defecto
    public decimal Total { get; set; }
}