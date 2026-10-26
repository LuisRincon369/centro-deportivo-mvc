namespace CentroDeportivo.Models;

// Entidad que representa una clase grupal del horario semanal.
public class Clase
{
    public int Id { get; set; }
    public string Dia { get; set; } = string.Empty;
    public string Hora { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Entrenador { get; set; } = string.Empty;
    public int Cupos { get; set; }
}
