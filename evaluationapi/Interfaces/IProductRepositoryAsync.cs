using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eValuate.WebApi.Models;

namespace eValuate.WebApi.Interfaces
{
    public interface IProductRepositoryAsync
    {
        ValueTask<Product> GetById(int id);
        Task AddProduct(Product entity);
        Task UpdateProduct(Product entity, int id);
        Task RemoveProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
