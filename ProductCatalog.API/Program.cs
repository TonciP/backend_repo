using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Services;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// 1. Configurar la Base de Datos SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Registrar Repositorios y Servicios (Inyección de Dependencias)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

// 3. Configurar CORS para permitir peticiones desde Flutter
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutter", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ... (Tus registros anteriores de DbContext, Repositorios y Servicios)

builder.Services.AddControllers();

// REEMPLAZA AddEndpointsApiExplorer y AddSwaggerGen por esto:
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // AGREGA ESTA LÍNEA TEMPORALMENTE: Forzar la limpieza absoluta
    context.Database.EnsureDeleted();

    // Esto creará el archivo 'products.db' con la tabla 'Products' y los datos semilla
    context.Database.EnsureCreated();
}

// ... (El bloque de EnsureCreated para el Seed de SQLite)

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Genera el JSON técnico de OpenAPI

    // AGREGA ESTA LÍNEA para activar la interfaz web interactiva:
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("AllowFlutter");
app.UseAuthorization();
app.MapControllers();

app.Run();

//builder.Services.AddControllers();
//builder.Services.AddOpenApi();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
