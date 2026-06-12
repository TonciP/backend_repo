using System;
using System.Collections.Generic;
using System.Text;

using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            // Mapeo manual simple para evitar dependencias externas como AutoMapper si no es necesario
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Price = p.Price,
                Currency = p.Currency,
                Stock = p.Stock
            });
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var products = await _productRepository.SearchAsync(searchTerm);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Price = p.Price,
                Currency = p.Currency,
                Stock = p.Stock
            });
        }

        public async Task<bool> UpdateProductPriceAsync(int id, decimal newPrice)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false; // Retornamos false si no existe para gatillar el 404 en el controlador
            }

            // Ejecutamos la regla de negocio encapsulada en el Dominio
            product.UpdatePrice(newPrice);

            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}