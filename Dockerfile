# Etapa 1: compilar y publicar la aplicación con el SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY CentroDeportivo.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Etapa 2: imagen liviana que solo trae el runtime de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV TZ=America/Bogota
# El servicio de alojamiento indica el puerto en la variable PORT (8080 si no existe)
ENV PORT=8080
EXPOSE 8080
ENTRYPOINT ["sh", "-c", "ASPNETCORE_HTTP_PORTS=$PORT exec dotnet CentroDeportivo.dll"]
