using AutoMapper;
using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;
using Microservice.Client.ProjectProduct.Repository;
using System;

namespace Microservice.Client.ProjectProduct.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            var product=_mapper.Map<ProductDTO,Product>(productDTO);
            var res = await _productRepository.CreateAsync(product);
            return _mapper.Map<Product, ProductDTO>(res);
        }
        public async Task<ProductDTO> UpdateProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<ProductDTO, Product>(productDto);
            var res = await _productRepository.UpdateProductAsync(product);
            return _mapper.Map<Product,ProductDTO>(res);

        }
        public async Task<bool> RemoveProductAsync(int guid)
        {
            Product product = await _productRepository.FindProductByGuid(guid);
            try
            {
               _productRepository.Remove(product);
               // await unitOfWork.SaveAsyc();
               return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            Product product = await _productRepository.FindProductByGuid(productId);
            return _mapper.Map<Product, ProductDTO>(product);
        }
        public async Task<ProductDTO> UpdateProductAsync(ProductDTO existingProduct, int productId, ProductDTO updatedProductDTO)
        {
            //var product = _mapper.Map<ProductDTO, Product>(existingProduct);
            var res = await _productRepository.UpdateProductAsync(existingProduct, productId, updatedProductDTO);
            return res;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<ProductDTO> GetPlacedOrder(OrderDTO orderDTO)
        {
            var res = await _productRepository.OrderPlaced(orderDTO);
            return res;
        }
    }
}
