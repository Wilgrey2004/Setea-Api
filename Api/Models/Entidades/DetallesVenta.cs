
using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;

public class DetallesVenta
{
    public int Id { get; set; }
    [Required(ErrorMessage = "La venta es requerida")]
    public int VentaId { get; set; }
    [Required(ErrorMessage = "El producto es requerido")]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "La cantidad es requerida")]
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "El precio unitario es requerido")]
    public decimal PrecioUnitario { get; set; }
}