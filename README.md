# Catálogo de Productos - Backend REST API (.NET 10)

Este proyecto constituye el núcleo de servicios del sistema. Está desarrollado en **.NET 10** bajo un enfoque de **Clean Architecture (Arquitectura Limpia)** y principios **SOLID**, garantizando un software mantenible, testeable y completamente desacoplado de los motores de persistencia o frameworks externos.

---

## 🚀 1. Guía de Ejecución Paso a Paso

El backend está diseñado para ser **completamente auto-contenido**. No requiere la instalación previa de servidores de bases de datos complejos, contenedores de Docker o scripts de migración manuales, ya que utiliza un motor **SQLite** local.

### Prerrequisitos
* Tener instalado el SDK de .NET 10 en la máquina local.

### Instrucciones para levantar el servidor:
1. **Abrir una terminal** y posicionarse en la carpeta raíz del proyecto ejecutable de la API:
   ```bash
   cd backend/ProductCatalog.API

   dotnet restore

   dotnet run
