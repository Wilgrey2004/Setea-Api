using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;

public class Promociones
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo Nombre es requerido")]
    [MaxLength(100, ErrorMessage = "El campo Nombre debe tener una longitud máxima de 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo DescuentoPorcentaje es requerido")]
    public decimal? DescuentoPorcentaje { get; set; } // Nullable para permitir que sea NULL
    [Required(ErrorMessage = "El campo FechaInicio es requerido")]
    public DateTime FechaInicio { get; set; }
    [Required(ErrorMessage = "El campo FechaFin es requerido")]
    public DateTime FechaFin { get; set; }
    public bool Activa { get; set; } = false; // Valor por defecto
}
