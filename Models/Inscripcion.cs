using System.ComponentModel.DataAnnotations;

namespace CentroDeportivo.Models;

// Entidad que representa la inscripción de una persona a una clase.
// Los atributos [Required], [Range], etc. son las reglas de validación:
// ASP.NET Core las revisa en el servidor y también genera la validación del navegador.
public class Inscripcion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Escribe un correo válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La edad es obligatoria.")]
    [Range(14, 90, ErrorMessage = "La edad debe estar entre 14 y 90 años.")]
    public int? Edad { get; set; }

    [Required(ErrorMessage = "Selecciona una clase.")]
    [Display(Name = "Clase")]
    public int? ClaseId { get; set; }

    [Required(ErrorMessage = "Selecciona un plan.")]
    [Display(Name = "Plan")]
    public int? PlanId { get; set; }

    // ---------- Datos que calcula el controlador (no los escribe el usuario) ----------
    public string NombreClase { get; set; } = string.Empty;
    public string NombrePlan { get; set; } = string.Empty;
    public decimal Descuento { get; set; }
    public decimal ValorMensual { get; set; }
    public DateTime FechaRegistro { get; set; }
}
