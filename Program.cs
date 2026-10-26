using System.Globalization;

// Cultura de Colombia: precios como "$ 80.000" y fechas como "26/10/2026"
var cultura = new CultureInfo("es-CO");
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Ruta por defecto: la URL decide qué controlador y qué acción atienden la petición.
//   /                          -> HomeController.Index()
//   /Clases?dia=Lunes          -> ClasesController.Index("Lunes")
//   /Inscripciones/Crear       -> InscripcionesController.Crear()
//   /Inscripciones/Confirmacion/3 -> InscripcionesController.Confirmacion(3)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
