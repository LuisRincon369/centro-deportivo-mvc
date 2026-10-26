using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CentroDeportivo.Data;
using CentroDeportivo.Models;

namespace CentroDeportivo.Controllers;

public class InscripcionesController : Controller
{
    // GET: /Inscripciones
    // Lista de inscritos y el ingreso mensual total, calculado en C# con LINQ.
    public IActionResult Index()
    {
        ViewBag.TotalMensual = DatosGimnasio.Inscripciones.Sum(i => i.ValorMensual);
        return View(DatosGimnasio.Inscripciones);
    }

    // GET: /Inscripciones/Crear  (o /Inscripciones/Crear?claseId=3 desde el horario)
    // Solo MUESTRA el formulario vacío; si viene una clase, la deja preseleccionada.
    [HttpGet]
    public IActionResult Crear(int? claseId)
    {
        CargarListas();
        return View(new Inscripcion { ClaseId = claseId });
    }

    // POST: /Inscripciones/Crear
    // RECIBE los datos del formulario, los valida y los procesa en C#.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear([Bind("Nombre,Correo,Edad,ClaseId,PlanId")] Inscripcion inscripcion)
    {
        var clase = DatosGimnasio.Clases.FirstOrDefault(c => c.Id == inscripcion.ClaseId);
        var plan = DatosGimnasio.Planes.FirstOrDefault(p => p.Id == inscripcion.PlanId);

        // ---------- Reglas del negocio (además de las validaciones del modelo) ----------
        if (clase == null)
        {
            ModelState.AddModelError(nameof(Inscripcion.ClaseId), "Selecciona una clase válida.");
        }
        else if (clase.Cupos == 0)
        {
            ModelState.AddModelError(nameof(Inscripcion.ClaseId), "Esta clase ya no tiene cupos disponibles.");
        }

        if (plan == null)
        {
            ModelState.AddModelError(nameof(Inscripcion.PlanId), "Selecciona un plan válido.");
        }

        bool yaInscrito = DatosGimnasio.Inscripciones.Any(i =>
            i.ClaseId == inscripcion.ClaseId &&
            string.Equals(i.Correo, inscripcion.Correo, StringComparison.OrdinalIgnoreCase));

        if (yaInscrito)
        {
            ModelState.AddModelError(nameof(Inscripcion.Correo), "Este correo ya está inscrito en esa clase.");
        }

        // Si algo falló, se vuelve a mostrar el mismo formulario con los mensajes de error
        if (!ModelState.IsValid || clase == null || plan == null)
        {
            CargarListas();
            return View(inscripcion);
        }

        // ---------- Procesamiento de la información en C# ----------
        decimal descuento = CalcularDescuento(inscripcion.Edad!.Value);

        inscripcion.Id = DatosGimnasio.NuevoId();
        inscripcion.NombreClase = $"{clase.Nombre} ({clase.Dia} {clase.Hora})";
        inscripcion.NombrePlan = plan.Nombre;
        inscripcion.Descuento = descuento;
        inscripcion.ValorMensual = plan.Precio * (1 - descuento);
        inscripcion.FechaRegistro = DateTime.Now;

        clase.Cupos--;
        DatosGimnasio.Inscripciones.Add(inscripcion);

        // Patrón POST-Redirect-GET: evita que al recargar la página se inscriba dos veces
        return RedirectToAction(nameof(Confirmacion), new { id = inscripcion.Id });
    }

    // GET: /Inscripciones/Confirmacion/5
    public IActionResult Confirmacion(int id)
    {
        var inscripcion = DatosGimnasio.Inscripciones.FirstOrDefault(i => i.Id == id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        return View(inscripcion);
    }

    // POST: /Inscripciones/Eliminar  (lo envía el formulario del modal de confirmación)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        var inscripcion = DatosGimnasio.Inscripciones.FirstOrDefault(i => i.Id == id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        // Al eliminar la inscripción, el cupo vuelve a quedar disponible en la clase
        var clase = DatosGimnasio.Clases.FirstOrDefault(c => c.Id == inscripcion.ClaseId);
        if (clase != null)
        {
            clase.Cupos++;
        }

        DatosGimnasio.Inscripciones.Remove(inscripcion);
        TempData["Mensaje"] = $"Se eliminó la inscripción de {inscripcion.Nombre}.";

        return RedirectToAction(nameof(Index));
    }

    // Menores de 18 y mayores de 60 años tienen 15% de descuento
    private static decimal CalcularDescuento(int edad)
    {
        return edad < 18 || edad >= 60 ? 0.15m : 0m;
    }

    // Opciones que necesita el formulario: lista de clases (para el <select>) y planes (radio buttons)
    private void CargarListas()
    {
        ViewBag.Clases = DatosGimnasio.Clases
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Dia} · {c.Hora} · {c.Nombre} ({c.Cupos} cupos)",
                Disabled = c.Cupos == 0
            })
            .ToList();

        ViewBag.Planes = DatosGimnasio.Planes;
    }
}
