using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CentroDeportivo.Data;
using CentroDeportivo.Models;

namespace CentroDeportivo.Controllers;

public class HomeController : Controller
{
    // GET: /  (o /Home/Index)
    // Toma la lista de planes (Modelo) y se la entrega a la Vista Index.cshtml
    public IActionResult Index()
    {
        return View(DatosGimnasio.Planes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
