using Microsoft.AspNetCore.Mvc;
using CentroDeportivo.Data;

namespace CentroDeportivo.Controllers;

public class ClasesController : Controller
{
    // GET: /Clases            -> todas las clases
    // GET: /Clases?dia=Lunes  -> solo las clases del lunes
    // El parámetro "dia" llega desde la URL (query string) de la petición GET.
    public IActionResult Index(string? dia)
    {
        var clases = string.IsNullOrEmpty(dia)
            ? DatosGimnasio.Clases
            : DatosGimnasio.Clases.Where(clase => clase.Dia == dia).ToList();

        ViewBag.Dias = DatosGimnasio.Dias;
        ViewBag.DiaSeleccionado = dia;

        return View(clases);
    }
}
