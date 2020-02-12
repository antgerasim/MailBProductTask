using MailBProductTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{

    public class ProductService : IProductService
    {
        private List<Product> _users = new List<Product>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public Task<Product> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get()
        {
            throw new NotImplementedException();
        }

        public Task<int> Post()
        {
            throw new NotImplementedException();
        }
    }
}
