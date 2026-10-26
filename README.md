# Centro de Entrenamiento Deportivo — Seguimiento 3

Aplicación web en **ASP.NET Core MVC (.NET 10)** para el Centro de Entrenamiento Deportivo *Rincón Sport*.
Permite consultar los planes, ver el horario de clases e inscribirse; la información se procesa en C# en el servidor.

**Autor:** Luis Esteban Rincón Espinosa

## Funcionalidades

| Página | Ruta | Qué hace |
|---|---|---|
| Inicio | `GET /` | Muestra los planes de membresía (Básico, Premium, VIP Élite). |
| Horario de clases | `GET /Clases?dia=Lunes` | Lista las clases y las filtra por día; muestra los cupos disponibles. |
| Inscripción | `GET /Inscripciones/Crear` | Muestra el formulario de inscripción. |
| Inscripción | `POST /Inscripciones/Crear` | Valida los datos, verifica cupos y correo duplicado, calcula el valor mensual con descuento y descuenta un cupo. |
| Confirmación | `GET /Inscripciones/Confirmacion/{id}` | Muestra el resumen de la inscripción. |
| Inscritos | `GET /Inscripciones` | Lista los inscritos y el ingreso mensual total. |
| Eliminar | `POST /Inscripciones/Eliminar` | Elimina una inscripción (confirmación en un modal) y devuelve el cupo. |

**Regla de negocio:** menores de 18 y mayores de 60 años reciben 15% de descuento sobre el precio del plan.

No se usa base de datos: la información vive en listas en memoria (`Data/DatosGimnasio.cs`),
así que al reiniciar la aplicación las inscripciones vuelven a cero.

## Estructura (Modelo–Vista–Controlador)

```
Models/         Entidades en C#: Plan, Clase, Inscripcion (con reglas de validación)
Controllers/    HomeController, ClasesController, InscripcionesController (acciones GET y POST)
Views/          Vistas Razor (.cshtml) con HTML y Bootstrap; Shared/_Layout.cshtml es la plantilla común
Data/           DatosGimnasio: listas en memoria que reemplazan a la base de datos
wwwroot/        Archivos estáticos: css/site.css, js/site.js, img/ y lib/ (Bootstrap, jQuery)
Program.cs      Configuración de la aplicación y de la ruta {controller}/{action}/{id?}
Dockerfile      Imagen para publicar la aplicación en el servicio de alojamiento
```

## Recorrido de una solicitud

1. El navegador pide una URL, por ejemplo `GET /Clases?dia=Lunes`.
2. El enrutamiento de `Program.cs` decide que la atiende `ClasesController.Index("Lunes")`.
3. El controlador consulta los datos (Modelo) y filtra las clases del lunes.
4. El controlador devuelve `View(clases)`, y la vista `Views/Clases/Index.cshtml` genera el HTML con Bootstrap.
5. El navegador recibe el HTML y lo muestra; `site.css` y `site.js` se cargan desde `wwwroot`.

## Ejecutar en local

Requiere el [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

Luego abrir `http://localhost:5074`.

## Tecnologías

ASP.NET Core MVC · C# · Razor · HTML5 · CSS3 · Bootstrap 5 · JavaScript · Git y GitHub · Docker
