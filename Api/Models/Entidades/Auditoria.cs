using System.ComponentModel.DataAnnotations;


namespace Api.Models.Entidades;
public class Auditoria
{


        public int Id { get; set; }
        
        [Required(ErrorMessage = "El usuario es requerido")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "La accion es requerida")]
        public string Accion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now; // Valor por defecto
}