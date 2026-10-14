# Essenza Backend

Backend de Essenza construido con **ASP.NET Core 9** y una arquitectura basada en **Clean Architecture + DDD**.

## Ramas

- `main`: rama estable del proyecto.
- `develop`: rama de integración con la estructura base y el desarrollo activo.

## Arquitectura

La solución está dividida en cuatro capas principales:

- `Essenza.Domain`: entidades, enumeraciones, interfaces y reglas del dominio.
- `Essenza.Application`: casos de uso, DTOs, validaciones y contratos de aplicación.
- `Essenza.Infrastructure`: persistencia, repositorios e integraciones externas.
- `Essenza.Api`: API HTTP, configuración, middlewares y composición de la aplicación.

También existe `Essenza.Tests` para las pruebas automatizadas.

## Requisitos

- .NET SDK 9

## Desarrollo

La estructura inicial se encuentra en la rama `develop`.
