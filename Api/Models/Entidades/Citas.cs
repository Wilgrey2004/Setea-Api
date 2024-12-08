
using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;
public class Citas
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo 'ClienteId' es requerido")]
    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; }
    [Required(ErrorMessage = "El campo 'Servicio' es requerido")]
    public string Servicio { get; set; } = string.Empty;

    public bool RecordatorioEnviado { get; set; } = false; // Valor por defecto
}