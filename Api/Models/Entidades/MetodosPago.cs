using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;


public class MetodosPago
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo Metodo de pago es obligatorio")] 
    [MaxLength(50, ErrorMessage = "El campo Metodo de pago no debe superar los 50 caracteres")]
    public string Metodo { get; set; } = string.Empty;
}