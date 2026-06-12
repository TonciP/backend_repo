using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo explícito para respetar el encapsulamiento de la Entidad de Dominio
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Sku)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Price)
                    .HasConversion<double>() // SQLite maneja mejor los tipos reales/dobles para decimales simples
                    .IsRequired();

                entity.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(p => p.Stock)
                    .IsRequired();
            });

            // Sembrado de Datos (Seed Data) con el JSON base y datos extra para probar el buscador
            modelBuilder.Entity<Product>().HasData(
                new { Id = 1, Sku = "SKU-1001", Name = "Auriculares Bluetooth", Price = 199.90m, Currency = "BOB", Stock = 25 },
                new { Id = 2, Sku = "SKU-1002", Name = "Teclado Mecánico RGB", Price = 350.00m, Currency = "BOB", Stock = 15 },
                new { Id = 3, Sku = "SKU-1003", Name = "Mouse Gamer Inalámbrico", Price = 250.50m, Currency = "BOB", Stock = 40 },
                new { Id = 4, Sku = "SKU-1004", Name = "Monitor 24'' Full HD", Price = 1200.00m, Currency = "BOB", Stock = 8 },
                new { Id = 5, Sku = "SKU-1005", Name = "Cargador Rápido Tipo C", Price = 85.00m, Currency = "BOB", Stock = 100 }
            );
        }
    }
}