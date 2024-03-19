using AutoMapper;
using Microservice.Client.ProjectProduct.DataDb;
using Microservice.Client.ProjectProduct.Domain;
using Microservice.Client.ProjectProduct.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Client.ProjectProduct.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            var res=await _context.Products.AddAsync(product);
            //Adding Categories in Categories Table
            ProductCategories categories = new ProductCategories()
            {
                CategoryName = product.CategoryName,
            };
            await _context.Categories.AddAsync(categories);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            var productDetails=await _context.Products.FirstOrDefaultAsync(p=>p.Id==product.Id);
            if (productDetails!=null)
            {
                productDetails.ImageUrl = product.ImageUrl;
                productDetails.ImageLocalPath = product.ImageLocalPath;
            }
            //var res=_context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        //Deleting Product
        public async Task<Product> FindProductByGuid(int guid)
        {
            var res=await _context.Products.FirstOrDefaultAsync(p=>p.Id == guid);
            return res;
        }
        //public async void Remove(Product product)
        //{
        //    var res=_context.Products.Remove(product);
        //   await _context.SaveChangesAsync();
        //}
        public async Task Remove(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        //Updating Product
        public async Task<ProductDTO> UpdateProductAsync(ProductDTO existingProduct, int productId, ProductDTO updatedProductDTO)
        {
            var product=await _context.Products.FirstOrDefaultAsync(p=>p.Id==productId);
            if (product!=null)
            {
                if(updatedProductDTO.Name!=null) product.Name = updatedProductDTO.Name;
                if(updatedProductDTO.Quantity!=0) product.Quantity = updatedProductDTO.Quantity;
                if(updatedProductDTO.Description!=null) product.Description = updatedProductDTO.Description;
                if(existingProduct.ImageUrl!=product.ImageUrl) product.ImageUrl = existingProduct.ImageUrl;
                if(existingProduct.ImageLocalPath!=product.ImageLocalPath) product.ImageLocalPath = existingProduct.ImageLocalPath;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Product,ProductDTO>(product);
        }
        //Get-All-Products
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            IEnumerable<Product> products=await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDTO>>(products);
        }
        public async Task<ProductDTO> OrderPlaced(OrderDTO orderDTO)
        {
            var product=await _context.Products.FirstOrDefaultAsync(p=>p.Id == orderDTO.ProductId);
            if (product!=null)
            {
                product.Quantity = (product.Quantity-orderDTO.Quantity);
                if (product.Quantity >= 0)
                {
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<Product,ProductDTO>(product);
        }
    }
}
