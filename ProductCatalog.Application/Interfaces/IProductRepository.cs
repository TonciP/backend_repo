using System;
using System.Collections.Generic;
using System.Text;

using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
    }
}