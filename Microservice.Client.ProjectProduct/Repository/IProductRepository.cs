using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;
using System;

namespace Microservice.Client.ProjectProduct.Repository
{
    public interface IProductRepository
    {
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);

        public Task<Product> FindProductByGuid(int guid);
        public Task Remove(Product product);

        public Task<ProductDTO> UpdateProductAsync( ProductDTO existingProduct, int productId,ProductDTO updatedProductDTO);
        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        public Task<ProductDTO> OrderPlaced(OrderDTO orderDTO);
    }
}
