using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IProductService
    {
        Task<IResponse> CreateProductAsync(ProductRequestVm product);
        Task<ProductResponseVm> GetProductByIdAsync(int id);
        // Task<ProductRequest> ReadRequestBodyStream(Stream requestBody);
        Task<ProductRequestVm> ReadRequestBodyStream(HttpRequest request);
    }
}