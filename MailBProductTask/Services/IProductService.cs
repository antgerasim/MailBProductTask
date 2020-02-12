using MailBProductTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
   public interface IProductService
    {
        Task<Product> Authenticate(string username, string password);
        Task<Product> Get();
        Task<int> Post();
    }
}
