using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Sku { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public int Stock { get; private set; }

        // Constructor para inicializar la entidad de forma segura
        public Product(int id, string sku, string name, decimal price, string currency, int stock)
        {
            Id = id;
            Sku = sku;
            Name = name;
            Currency = !string.IsNullOrWhiteSpace(currency) ? currency : throw new ArgumentException("La moneda no puede estar vacía.");
            Stock = stock;
            UpdatePrice(price); // Validamos el precio desde el nacimiento del objeto
        }

        // Lógica de negocio encapsulada: El precio solo se puede cambiar desde aquí
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");

            Price = newPrice;
        }
    }
}