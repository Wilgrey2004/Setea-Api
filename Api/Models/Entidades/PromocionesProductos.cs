using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;

public class PromocionesProductos
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo PromocionId es requerido")]
    public int PromocionId { get; set; }
    
    [Required(ErrorMessage = "El campo ProductoId es requerido")]
    public int ProductoId { get; set; }
}