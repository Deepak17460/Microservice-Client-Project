using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;
using System;

namespace Microservice.Client.ProjectProduct.Services
{
    public interface IProductService
    {
        public Task<ProductDTO> CreateProduct(ProductDTO productDTO);
        public Task<ProductDTO> UpdateProduct(ProductDTO productDTO);
        public Task<bool> RemoveProductAsync(int guid);
        public Task<ProductDTO> GetProductByIdAsync(int productId);

        public Task<ProductDTO> UpdateProductAsync(ProductDTO existingProduct,int productId,ProductDTO updatedProductDTO);
        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        public Task<ProductDTO> GetPlacedOrder(OrderDTO orderDTO);
    }
}
