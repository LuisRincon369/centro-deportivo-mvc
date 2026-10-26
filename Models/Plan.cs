namespace CentroDeportivo.Models;

// Entidad que representa una membresía del centro deportivo.
public class Plan
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public List<string> Beneficios { get; set; } = new();
    public bool Destacado { get; set; }
}
