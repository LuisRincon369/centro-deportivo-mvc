using CentroDeportivo.Models;

namespace CentroDeportivo.Data;

// No usamos base de datos: la información vive en listas en memoria.
// Si la aplicación se reinicia, las inscripciones se borran y los cupos
// vuelven a su valor inicial.
public static class DatosGimnasio
{
    public static List<string> Dias { get; } = new()
    {
        "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"
    };

    public static List<Plan> Planes { get; } = new()
    {
        new Plan { Id = 1, Nombre = "Básico", Precio = 80000m, Beneficios = new() { "Acceso a zona de máquinas", "Zona cardiovascular", "Casillero personal" } },
        new Plan { Id = 2, Nombre = "Premium", Precio = 130000m, Destacado = true, Beneficios = new() { "Acceso total a máquinas", "Clases grupales", "Sauna" } },
        new Plan { Id = 3, Nombre = "VIP Élite", Precio = 210000m, Beneficios = new() { "Entrenador personal", "Plan nutricional", "Todo lo del plan Premium" } },
    };

    public static List<Clase> Clases { get; } = new()
    {
        new Clase { Id = 1, Dia = "Lunes", Hora = "06:00 - 07:00", Nombre = "Entrenamiento Funcional", Entrenador = "Andrea Vélez", Cupos = 12 },
        new Clase { Id = 2, Dia = "Lunes", Hora = "07:00 - 09:00", Nombre = "CrossFit Intensivo", Entrenador = "Diego Salazar", Cupos = 8 },
        new Clase { Id = 3, Dia = "Lunes", Hora = "18:00 - 19:00", Nombre = "Boxeo y Kickboxing", Entrenador = "Mario Peña", Cupos = 10 },
        new Clase { Id = 4, Dia = "Martes", Hora = "06:00 - 07:00", Nombre = "Spinning", Entrenador = "Laura Ríos", Cupos = 15 },
        new Clase { Id = 5, Dia = "Martes", Hora = "07:00 - 08:00", Nombre = "Pilates Matutino", Entrenador = "Andrea Vélez", Cupos = 14 },
        new Clase { Id = 6, Dia = "Martes", Hora = "19:00 - 20:00", Nombre = "Zumba Fitness", Entrenador = "Camila Ortiz", Cupos = 20 },
        new Clase { Id = 7, Dia = "Miércoles", Hora = "06:00 - 07:00", Nombre = "Entrenamiento Funcional", Entrenador = "Andrea Vélez", Cupos = 12 },
        new Clase { Id = 8, Dia = "Miércoles", Hora = "07:00 - 09:00", Nombre = "CrossFit Intensivo", Entrenador = "Diego Salazar", Cupos = 8 },
        new Clase { Id = 9, Dia = "Miércoles", Hora = "09:00 - 10:00", Nombre = "Calistenia", Entrenador = "Mario Peña", Cupos = 10 },
        new Clase { Id = 10, Dia = "Jueves", Hora = "06:00 - 07:00", Nombre = "Spinning", Entrenador = "Laura Ríos", Cupos = 15 },
        new Clase { Id = 11, Dia = "Jueves", Hora = "09:00 - 10:00", Nombre = "HIIT Cardio", Entrenador = "Camila Ortiz", Cupos = 16 },
        new Clase { Id = 12, Dia = "Viernes", Hora = "06:00 - 07:00", Nombre = "Entrenamiento Funcional", Entrenador = "Andrea Vélez", Cupos = 12 },
        new Clase { Id = 13, Dia = "Viernes", Hora = "07:00 - 08:00", Nombre = "Power Lifting", Entrenador = "Diego Salazar", Cupos = 6 },
        new Clase { Id = 14, Dia = "Viernes", Hora = "18:00 - 19:00", Nombre = "Funcional Nocturno", Entrenador = "Mario Peña", Cupos = 10 },
        new Clase { Id = 15, Dia = "Sábado", Hora = "06:00 - 07:00", Nombre = "Yoga Matutino", Entrenador = "Laura Ríos", Cupos = 18 },
        new Clase { Id = 16, Dia = "Sábado", Hora = "09:00 - 10:00", Nombre = "Taller de Fuerza", Entrenador = "Diego Salazar", Cupos = 10 },
    };

    // Empieza vacía y crece con cada inscripción que llega por el formulario.
    public static List<Inscripcion> Inscripciones { get; } = new();

    private static int siguienteId = 1;

    public static int NuevoId() => siguienteId++;
}
