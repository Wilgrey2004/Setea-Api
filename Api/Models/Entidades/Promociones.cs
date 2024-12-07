namespace Api.Models.Entidades;

public class Promociones
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal? DescuentoPorcentaje { get; set; } // Nullable para permitir que sea NULL
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool Activa { get; set; } = false; // Valor por defecto
}
