using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}