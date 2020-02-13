using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
    }
}