using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEntities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();//note we need read only so when read can't add or update the list

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();//note we need read only so when read can't add or update the list
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();//note we need read only so when read can't add or update the list

    }
}