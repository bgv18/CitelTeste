using CitelTeste.ProductAPI_Microservice.DTOs;

namespace CitelTeste.ProductAPI_Microservice.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task AddProduct(ProductDTO productDto);
        Task UpdateProduct(ProductDTO productDto);
        Task RemoveProduct(int id);
    }
}
