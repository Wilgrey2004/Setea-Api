using System.ComponentModel.DataAnnotations;

namespace Api.Models.Entidades
{
    public class Inventario
    {
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "El campo 'Cantidad' es obligatorio")]        
        public int Cantidad { get; set; }
        
        [Required(ErrorMessage = "El campo 'StockMinimo' es obligatorio")]
        public int StockMinimo { get; set; }
    }
}
