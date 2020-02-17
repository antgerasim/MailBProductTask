using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IProductService
    {
        Task<IResponse> CreateProductAsync(ProductRequest product);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<ProductRequest> ReadRequestBodyStream(Stream requestBody);
    }
}